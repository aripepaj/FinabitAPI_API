using Microsoft.AspNetCore.Mvc;
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using FinabitAPI.Finabit.Partner.dto;
using FinabitAPI.Core.Utilis;
using FinabitAPI.Core.Global.dto;
using FinabitAPI.Utilis;

namespace FinabitAPI
{
    [ApiController]
    [Route("api/[controller]")]
    public class PartnersController : ControllerBase
    {
        private readonly DBAccess _dbAccess;
        private readonly PartnerRepository _partnerRepository;

        public PartnersController(DBAccess dbAccess, PartnerRepository partnerRepository)
        {
            _dbAccess = dbAccess;
            _partnerRepository = partnerRepository;
        }

        [HttpGet("GetCustomerAnalyticsAll")]
        public ActionResult<DataTable> GetCustomerAnalyticsAll(DateTime FromDate, DateTime ToDate)
        {
            return _dbAccess.GetCustomerAnalyticsAll(FromDate, ToDate);
        }

        [HttpGet("GetCustomerAnalyticsByPartnerID")]
        public ActionResult<DataTable> GetCustomerAnalyticsByPartnerID(DateTime FromDate, DateTime ToDate, int PartnerID)
        {
            return _dbAccess.GetCustomerAnalyticsByPartnerID(FromDate, ToDate, PartnerID);
        }

        [HttpGet("GetCustomerAnalyticsByPartnerID_MAR")]
        public ActionResult<DataTable> GetCustomerAnalyticsByPartnerID_MAR(int PartnerID, int ProfileID, int TransactionType, int PageId, int RowsForPage, int Language)
        {
            return _dbAccess.GetCustomerAnalyticsByPartnerID_MAR(PartnerID, ProfileID, TransactionType, PageId, RowsForPage, Language);
        }

        [HttpGet("GetPartners")]
        public ActionResult<List<PartnerModel>> GetPartners_API_Model(
            int partnerTypeID = 2,
            string partnerName = "%",
            string partnerGroup = "%",
            string partnerCategory = "%",
            string placeName = "%",
            string stateName = "%")
        {
            var result = _partnerRepository.GetPartners(partnerTypeID, partnerName, partnerGroup, partnerCategory, placeName, stateName);
            return Ok(result);
        }

        [HttpGet("Search")]
        public ActionResult<PagedResult<PartnerModel>> Search(
            [FromQuery] int partnerTypeId = 2,
            [FromQuery] string name = null,
            [FromQuery] string group = null,
            [FromQuery] string category = null,
            [FromQuery] string place = null,
            [FromQuery] string state = null,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 50)
        {
            if (pageNumber <= 0) pageNumber = 1;
            if (pageSize <= 0 || pageSize > 500) pageSize = 50;

            // Use existing stored proc through repository with '%' wildcards
            string partnerName = string.IsNullOrWhiteSpace(name) ? "%" : name;
            string partnerGroup = string.IsNullOrWhiteSpace(group) ? "%" : group;
            string partnerCategory = string.IsNullOrWhiteSpace(category) ? "%" : category;
            string placeName = string.IsNullOrWhiteSpace(place) ? "%" : place;
            string stateName = string.IsNullOrWhiteSpace(state) ? "%" : state;

            var list = _partnerRepository.GetPartners(partnerTypeId, partnerName, partnerGroup, partnerCategory, placeName, stateName) ?? new List<PartnerModel>();

            // Additional in-memory filtering for partial contains (stored proc likely already does LIKE but we ensure)
            IEnumerable<PartnerModel> query = list;
            if (!string.IsNullOrWhiteSpace(name))
            {
                var lower = name.ToLower();
                query = query.Where(p => (p.PartnerName ?? string.Empty).ToLower().Contains(lower));
            }
            if (!string.IsNullOrWhiteSpace(group))
            {
                var lower = group.ToLower();
                query = query.Where(p => (p.Group ?? string.Empty).ToLower().Contains(lower));
            }
            if (!string.IsNullOrWhiteSpace(category))
            {
                var lower = category.ToLower();
                query = query.Where(p => (p.Category ?? string.Empty).ToLower().Contains(lower));
            }
            if (!string.IsNullOrWhiteSpace(place))
            {
                var lower = place.ToLower();
                query = query.Where(p => (p.PlaceName ?? string.Empty).ToLower().Contains(lower));
            }
            if (!string.IsNullOrWhiteSpace(state))
            {
                var lower = state.ToLower();
                query = query.Where(p => (p.StateName ?? string.Empty).ToLower().Contains(lower));
            }

            var total = query.Count();
            var items = query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            var result = new PagedResult<PartnerModel>
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalCount = total,
                TotalPages = (int)Math.Ceiling(total / (double)pageSize),
                Items = items
            };
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public ActionResult<Partner> GetById(int id)
        {
            var p = PartnerRepository.SelectByID(new Partner { ID = id });
            if (p == null || p.ID == 0) return NotFound();
            return Ok(p);
        }

        [HttpPost("ImportNewPartners")]
        public ActionResult<PartnerBatchResponse> ImportPartners([FromBody] List<PartnerBatchItem> partners)
        {
            if (partners == null || partners.Count == 0) return BadRequest("partners payload required");
            var (inserted, failed, error) = _partnerRepository.ImportPartners(partners);
            var result = new PartnerBatchResponse { Inserted = inserted, Failed = failed, Error = error };
            if (!string.IsNullOrEmpty(error)) return StatusCode(500, result);
            return Ok(result);
        }

        [HttpPost("CreateNewPartner")]
        public ActionResult<CreatePartnerResponse> CreateNewPartner([FromBody] CreatePartnerRequest request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.PartnerName))
            {
                return BadRequest("PartnerName is required");
            }

            var partner = new Partner
            {
                PartnerName = request.PartnerName?.Trim(),
                ContactPerson = request.ContactPerson,
                Address = request.Address,
                Tel1 = request.Tel1,
                Tel2 = request.Tel2,
                Email = request.Email,
                BusinessNo = request.BusinessNo,
                BankAccount = request.BankAccount,
                DiscountPercent = request.DiscountPercent,
                PIN = request.PIN,
                ItemID = request.ItemID,
                PriceMenuID = request.PriceMenuID,
                DueDays = request.DueDays,
                DueValueMaximum = request.DueValueMaximum,
                ContractNo = request.ContractNo,
                Longitude = request.Longitude,
                Latitude = request.Latitude,
                VATNO = request.VATNO
            };
            partner.PartnerType.ID = request.PartnerTypeID;
            partner.State.ID = request.StateID;
            partner.Place.ID = request.PlaceID;
            partner.Account.AccountCode = request.AccountCode ?? string.Empty;

            _partnerRepository.Insert(partner);

            var response = new CreatePartnerResponse
            {
                PartnerID = partner.ID,
                ErrorID = partner.ErrorID,
                ErrorDescription = partner.ErrorDescription
            };

            if (partner.ErrorID != 0)
            {
                return StatusCode(500, response);
            }

            return CreatedAtAction(nameof(GetById), new { id = response.PartnerID }, response);
        }

        [HttpGet("exists")]
        public async Task<ActionResult<object>> Exists(
    [FromQuery] int? partnerId = null,
    [FromQuery] string partnerName = null,
    [FromQuery] string email = null,
    [FromQuery] string businessNo = null,
    [FromQuery] string fiscalNo = null)
        {
            if (partnerId == null
                && string.IsNullOrWhiteSpace(partnerName)
                && string.IsNullOrWhiteSpace(email)
                && string.IsNullOrWhiteSpace(businessNo)
                && string.IsNullOrWhiteSpace(fiscalNo))
            {
                return BadRequest("Provide at least one of: partnerId, partnerName, email, businessNo, fiscalNo.");
            }

            var id = await _partnerRepository.PartnerExistsAdvancedAsync(
                partnerId, partnerName, email, businessNo, fiscalNo);

            return Ok(new { partnerId = id });
        }

    }
}
