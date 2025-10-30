using FinabitAPI.Core.Global.dto;
using FinabitAPI.Finabit.Items.dto;
using FinabitAPI.Utilis;
using Microsoft.AspNetCore.Mvc;
using ItemExistenceResponse = FinabitAPI.Finabit.Items.dto.ItemExistenceResponse;

namespace FinabitAPI.Finabit.Items
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItemsManagementController : ControllerBase
    {
        private readonly DBAccess _dbAccess;
        private readonly ItemsMasterImportRepository _importRepo;
        private readonly ItemRepository _itemRepository;

        public ItemsManagementController(
            DBAccess dbAccess,
            ItemRepository itemRepository,
            ItemsMasterImportRepository importRepo
        )
        {
            _dbAccess = dbAccess;
            _itemRepository = itemRepository;
            _importRepo = importRepo;
        }

        [HttpPost("CreateNewItem")]
        public ActionResult<CreateItemResponse> CreateNewItem([FromBody] CreateItemRequest request)
        {
            if (
                request == null
                || string.IsNullOrWhiteSpace(request.ItemID)
                || string.IsNullOrWhiteSpace(request.ItemName)
            )
            {
                return BadRequest("ItemID and ItemName are required");
            }
            var response = new CreateItemResponse
            {
                ItemID = request.ItemID,
                ErrorID = 0,
                ErrorDescription = string.Empty,
            };
            return Created($"api/ItemsManagement/{request.ItemID}", response);
        }

        [HttpPost("ImportItemsMaster")]
        public async Task<ActionResult<ImportItemsMasterResponse>> ImportItemsMaster(
            [FromBody] ImportItemsMasterRequest request,
            [FromQuery] int newTransactionId
        )
        {
            if (request?.Items == null || request.Items.Count == 0)
                return BadRequest("Items payload required");

            var (inserted, error, items) = await _importRepo.ImportItemsAsync(
                request.Items,
                newTransactionId
            );

            var resp = new ImportItemsMasterResponse
            {
                Inserted = inserted,
                Error = error,
                InsertedItems = items, // <-- full rows returned by SQL
            };

            if (!string.IsNullOrEmpty(error))
                return StatusCode(500, resp);

            return Ok(resp);
        }

        [HttpGet("PosItems")]
        public ActionResult<List<ItemsLookup>> GetPosItems(
            [FromQuery] int departmentId,
            [FromQuery] int priceMenuId = 0
        )
        {
            var list = _dbAccess.GetItemsForPOS(priceMenuId, departmentId);
            return Ok(list);
        }

        [HttpGet("{itemId}")]
        public ActionResult<ItemsLookup> GetItem(
            string itemId,
            [FromQuery] int departmentId,
            [FromQuery] int priceMenuId = 0
        )
        {
            var list =
                _dbAccess.GetItemsForPOS(priceMenuId, departmentId) ?? new List<ItemsLookup>();
            var item = list.FirstOrDefault(i => i.ItemID == itemId);
            if (item == null)
                return NotFound();
            return Ok(item);
        }

        [HttpGet("exists")]
        public ActionResult<ItemExistenceResponse> Exists(
            [FromQuery] int departmentId,
            [FromQuery] string itemId = null,
            [FromQuery] string name = null,
            [FromQuery] string barcode = null,
            [FromQuery] bool returnDetails = true
        )
        {
            if (departmentId <= 0)
                return BadRequest("departmentId required");
            if (
                string.IsNullOrWhiteSpace(itemId)
                && string.IsNullOrWhiteSpace(name)
                && string.IsNullOrWhiteSpace(barcode)
            )
                return BadRequest("Provide at least one of itemId, name or barcode");

            if (!returnDetails)
            {
                int foundId = _dbAccess.ItemsAdvancedExists(departmentId, itemId, name, barcode);
                return Ok(
                    new ItemExistenceResponse
                    {
                        Exists = foundId > 0,
                        ItemID = itemId,
                        Name = name,
                        Barcode = barcode,
                        FoundID = foundId,
                    }
                );
            }
            else
            {
                var det = _dbAccess.ItemsAdvancedFind(departmentId, itemId, name, barcode);
                return Ok(
                    new ItemExistenceResponse
                    {
                        Exists = det != null && det.ID > 0,
                        ItemID = itemId,
                        Name = name,
                        Barcode = barcode,
                        FoundID = det?.ID ?? 0,
                        Details = det,
                    }
                );
            }
        }

        [HttpPost("exists/batch")]
        public async Task<ActionResult<List<ItemExistenceBatchResponse>>> ExistsBatch(
            [FromQuery] int departmentId,
            [FromBody] List<ItemExistenceProbe> items,
            [FromQuery] bool returnDetails = true,
            CancellationToken ct = default
        )
        {
            if (departmentId <= 0)
                return BadRequest("departmentId required");
            if (items == null || items.Count == 0)
                return BadRequest("Items required");

            foreach (var it in items)
            {
                if (
                    string.IsNullOrWhiteSpace(it.ItemID)
                    && string.IsNullOrWhiteSpace(it.Name)
                    && string.IsNullOrWhiteSpace(it.Barcode)
                )
                    return BadRequest(
                        $"Row Index {it.Index}: provide at least one of itemId, name or barcode."
                    );
            }

            var res = await _dbAccess.ItemsAdvancedExistsBatchAsync(
                departmentId,
                items,
                returnDetails,
                ct
            );
            return Ok(res);
        }

        [HttpGet("Search")]
        public ActionResult<PagedResult<ItemsLookup>> Search(
            [FromQuery] int departmentId,
            [FromQuery] int priceMenuId = 0,
            [FromQuery] string itemName = null,
            [FromQuery] string barcode = null,
            [FromQuery] string itemGroup = null,
            [FromQuery] bool? active = null,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 50
        )
        {
            if (departmentId <= 0)
                return BadRequest("departmentId is required");
            if (pageNumber <= 0)
                pageNumber = 1;
            if (pageSize <= 0 || pageSize > 500)
                pageSize = 50;

            var list =
                _dbAccess.GetItemsForPOS(priceMenuId, departmentId) ?? new List<ItemsLookup>();
            var query = list.AsQueryable();

            if (!string.IsNullOrWhiteSpace(itemName))
            {
                var lower = itemName.ToLower();
                query = query.Where(i => (i.ItemName ?? "").ToLower().Contains(lower));
            }
            if (!string.IsNullOrWhiteSpace(barcode))
            {
                var lower = barcode.ToLower();
                query = query.Where(i => (i.Barcode ?? "").ToLower().Contains(lower));
            }
            if (!string.IsNullOrWhiteSpace(itemGroup))
            {
                var lower = itemGroup.ToLower();
                query = query.Where(i => (i.ItemGroup ?? "").ToLower().Contains(lower));
            }
            if (active.HasValue)
            {
                // placeholder for Active flag filter
            }

            var total = query.Count();
            var items = query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            var result = new PagedResult<ItemsLookup>
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = total,
                TotalPages = (int)Math.Ceiling(total / (double)pageSize),
                Items = items,
            };
            return Ok(result);
        }

        [HttpGet("searchItem")]
        public async Task<ActionResult<IReadOnlyList<DistinctItemNameDto>>> GetDistinctNames(
            [FromQuery] string itemId = "",
            [FromQuery] string itemName = ""
        )
        {
            var data = await _dbAccess.GetDistinctItemNamesAsync(itemId, itemName);
            return Ok(data);
        }

        [HttpPost("searchItemsBatch")]
        public async Task<ActionResult<IReadOnlyList<DistinctItemProbeResult>>> SearchItemsBatch(
            [FromBody] IReadOnlyList<DistinctItemProbe> probes,
            CancellationToken cancellationToken
        )
        {
            if (probes == null || probes.Count == 0)
                return Ok(Array.Empty<DistinctItemProbeResult>());

            const int maxDegreeOfParallelism = 8;
            using var gate = new SemaphoreSlim(maxDegreeOfParallelism);

            var tasks = probes.Select(async p =>
            {
                await gate.WaitAsync(cancellationToken).ConfigureAwait(false);
                try
                {
                    var results = await _dbAccess
                        .GetDistinctItemNamesAsync(
                            p.ItemId ?? "",
                            p.ItemName ?? "",
                            cancellationToken
                        )
                        .ConfigureAwait(false);

                    return new DistinctItemProbeResult
                    {
                        Index = p.Index,
                        ItemId = p.ItemId,
                        ItemName = p.ItemName,
                        Results = results,
                    };
                }
                catch
                {
                    return new DistinctItemProbeResult
                    {
                        Index = p.Index,
                        ItemId = p.ItemId,
                        ItemName = p.ItemName,
                        Results = Array.Empty<DistinctItemNameDto>(),
                    };
                }
                finally
                {
                    gate.Release();
                }
            });

            var resultsArr = await Task.WhenAll(tasks).ConfigureAwait(false);

            var byIndex = resultsArr.ToDictionary(x => x.Index);
            var ordered = probes.Select(p => byIndex[p.Index]).ToList();

            return Ok(ordered);
        }

        [HttpGet("lastDistinctItems")]
        public async Task<ActionResult<IReadOnlyList<DistinctItemNameDto>>> GetLastDistinctItems(
            [FromQuery] int limit = 400,
            CancellationToken cancellationToken = default
        )
        {
            limit = Math.Clamp(limit, 1, 2000);

            var rows = await _dbAccess
                .GetLastDistinctItemNamesAsync(limit, cancellationToken)
                .ConfigureAwait(false);
            return Ok(rows);
        }
    }
}
