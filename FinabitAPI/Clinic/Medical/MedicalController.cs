using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace FinabitAPI.Clinic.Medical
{
    [ApiController]
    [Route("api/clinic/medical")] 
    public class MedicalController : ControllerBase
    {
        private readonly UniversalMedicalService _svc;

        public MedicalController(UniversalMedicalService svc)
        {
            _svc = svc;
        }

        [HttpGet("module-types")] 
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<MedicalModuleType>>> GetModuleTypes()
        {
            var items = await _svc.GetModuleTypesAsync();
            return Ok(items);
        }

        public record CreateRecordDto(int PatientId, int TypeId, string Title, int? DepartmentId, DateTime? Date, string? Code, Dictionary<string, object?>? Values);

        [HttpPost("records")] 
        public async Task<ActionResult> CreateRecord([FromBody] CreateRecordDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Title))
                return BadRequest("Title is required");

            var record = await _svc.CreateRecordAsync(dto.PatientId, dto.TypeId, dto.Title, dto.DepartmentId, dto.Date, dto.Code);

            if (dto.Values is not null && dto.Values.Count > 0)
            {
                await _svc.SaveFieldValuesAsync(record.Id, dto.Values.Select(kv => (kv.Key, kv.Value)));
            }

            return Ok(new { record.Id });
        }

        [HttpGet("records/{id:int}")] 
        public async Task<ActionResult<MedicalRecord>> GetRecord(int id)
        {
            var record = await _svc.GetRecordAsync(id);
            return record is null ? NotFound() : Ok(record);
        }

        [HttpGet("definitions/{moduleType}")] 
        public async Task<ActionResult<IEnumerable<MedicalFieldDefinition>>> GetDefinitions(string moduleType, [FromQuery] int? departmentId)
        {
            var defs = await _svc.GetFieldDefinitionsAsync(moduleType, departmentId);
            return Ok(defs);
        }
    }
}
