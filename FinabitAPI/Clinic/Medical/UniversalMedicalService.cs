using FinabitAPI.Clinic;
using Microsoft.EntityFrameworkCore;

namespace FinabitAPI.Clinic.Medical
{
    public class UniversalMedicalService
    {
        private readonly ClinicDbContext _db;

        public UniversalMedicalService(ClinicDbContext db)
        {
            _db = db;
        }

        // Module Types
        public async Task<List<MedicalModuleType>> GetModuleTypesAsync(CancellationToken ct = default)
            => await _db.ModuleTypes.Where(t => t.IsActive).OrderBy(t => t.Id).ToListAsync(ct);

        public async Task<MedicalModuleType?> GetModuleTypeAsync(int id, CancellationToken ct = default)
            => await _db.ModuleTypes.FirstOrDefaultAsync(t => t.Id == id && t.IsActive, ct);

        // Field definitions
        public async Task<List<MedicalFieldDefinition>> GetFieldDefinitionsAsync(string moduleType, int? departmentId, CancellationToken ct = default)
        {
            return await _db.MedicalFieldDefinitions
                .Where(f => f.ModuleType == moduleType && (f.DepartmentID == null || f.DepartmentID == departmentId) && f.IsEnabled)
                .OrderBy(f => f.DisplayOrder)
                .ToListAsync(ct);
        }

        // Records
        public async Task<MedicalRecord> CreateRecordAsync(int patientId, int moduleTypeId, string title, int? departmentId, DateTime? date, string? code, CancellationToken ct = default)
        {
            var type = await GetModuleTypeAsync(moduleTypeId, ct) ?? throw new InvalidOperationException("Invalid ModuleTypeId");

            var record = new MedicalRecord
            {
                PatientID = patientId,
                ModuleTypeId = type.Id,
                ModuleType = type.Name,
                Titulli = title,
                Departamenti = departmentId,
                Data = date,
                Kodi = code,
                CreatedDate = DateTime.UtcNow,
                Status = "Active",
                Priority = "Normal"
            };

            _db.MedicalRecords.Add(record);
            await _db.SaveChangesAsync(ct);
            return record;
        }

        public async Task<MedicalRecord?> GetRecordAsync(int id, CancellationToken ct = default)
        {
            return await _db.MedicalRecords
                .Include(r => r.ModuleTypeRef)
                .FirstOrDefaultAsync(r => r.Id == id, ct);
        }

        public async Task SaveFieldValuesAsync(int recordId, IEnumerable<(string key, object? value)> values, CancellationToken ct = default)
        {
            var record = await GetRecordAsync(recordId, ct) ?? throw new InvalidOperationException("Record not found");
            var defs = await GetFieldDefinitionsAsync(record.ModuleType, record.Departamenti, ct);
            var defsByKey = defs.ToDictionary(d => d.FieldKey, StringComparer.OrdinalIgnoreCase);

            foreach (var (key, value) in values)
            {
                if (!defsByKey.TryGetValue(key, out var def))
                    continue; // skip unknown keys

                var valEntity = new MedicalFieldValue
                {
                    MedicalRecordID = record.Id,
                    ModuleType = record.ModuleType,
                    FieldDefinitionID = def.Id,
                    FieldKey = def.FieldKey,
                    CreatedDate = DateTime.UtcNow
                };

                if (value is null)
                {
                    // leave all typed values null
                }
                else if (value is bool b)
                {
                    valEntity.BooleanValue = b;
                }
                else if (value is DateTime dt)
                {
                    valEntity.DateValue = dt;
                }
                else if (value is IFormattable && decimal.TryParse(Convert.ToString(value), out var dec))
                {
                    valEntity.NumericValue = dec;
                    valEntity.FieldValue = Convert.ToString(value);
                }
                else
                {
                    valEntity.FieldValue = Convert.ToString(value);
                }

                _db.MedicalFieldValues.Add(valEntity);
            }

            await _db.SaveChangesAsync(ct);
        }
    }
}
