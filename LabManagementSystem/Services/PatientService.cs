using LabManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace LabManagementSystem.Services
{
    public class PatientService : IPatientService
    {
        private readonly Func<LabContext> _contextFactory;

        public PatientService(Func<LabContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public string GenerateNextMedicalCode()
        {
            using var db = _contextFactory();
            string today = DateTime.Today.ToString("yyyyMMdd");

            var codes = db.Patients
                .AsNoTracking()
                .Where(p => p.MedicalCode.StartsWith(today))
                .Select(p => p.MedicalCode)
                .ToList();

            return CalculateNextCode(today, codes);
        }

        public async Task<string> GenerateNextMedicalCodeAsync(CancellationToken ct = default)
        {
            using var db = _contextFactory();
            string today = DateTime.Today.ToString("yyyyMMdd");

            var codes = await db.Patients
                .AsNoTracking()
                .Where(p => p.MedicalCode.StartsWith(today))
                .Select(p => p.MedicalCode)
                .ToListAsync(ct);

            return CalculateNextCode(today, codes);
        }

        private static string CalculateNextCode(string today, List<string> codes)
        {
            int maxSerial = 0;
            foreach (var code in codes)
            {
                if (code.Length >= 11 && int.TryParse(code.Substring(8), out int serial))
                {
                    if (serial > maxSerial)
                        maxSerial = serial;
                }
            }

            int nextSerial = maxSerial + 1;
            return $"{today}{nextSerial:D3}";
        }

        public List<Patient> SearchPatients(string? searchTerm)
        {
            using var db = _contextFactory();
            var query = db.Patients
                .AsNoTracking()
                .Include(p => p.Gender)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                string term = searchTerm.Trim();
                query = query.Where(p => p.Name.Contains(term) || p.MedicalCode.Contains(term) || (p.Phone != null && p.Phone.Contains(term)));
            }

            return query.OrderByDescending(p => p.Id).ToList();
        }

        public async Task<List<PatientSearchResultDto>> SearchPatientsAsync(string? searchTerm, int maxResults = 100, CancellationToken ct = default)
        {
            using var db = _contextFactory();
            var query = db.Patients.AsNoTracking().AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                string term = searchTerm.Trim();
                query = query.Where(p => p.Name.Contains(term) || p.MedicalCode.Contains(term) || (p.Phone != null && p.Phone.Contains(term)));
            }

            return await query
                .OrderByDescending(p => p.Id)
                .Take(maxResults)
                .Select(p => new PatientSearchResultDto
                {
                    Id = p.Id,
                    MedicalCode = p.MedicalCode,
                    Name = p.Name,
                    Phone = p.Phone,
                    Gender = p.Gender != null ? p.Gender.Name : "-",
                    AgeAtRecord = p.AgeAtRecord
                })
                .ToListAsync(ct);
        }

        public Patient? GetPatientById(int id)
        {
            using var db = _contextFactory();
            return db.Patients
                .Include(p => p.Gender)
                .Include(p => p.Visits)
                .FirstOrDefault(p => p.Id == id);
        }

        public async Task<Patient?> GetPatientByIdAsync(int id, CancellationToken ct = default)
        {
            using var db = _contextFactory();
            return await db.Patients
                .Include(p => p.Gender)
                .Include(p => p.Visits)
                .FirstOrDefaultAsync(p => p.Id == id, ct);
        }

        public Patient? GetPatientByCode(string code)
        {
            using var db = _contextFactory();
            string clean = code.Trim();
            return db.Patients
                .AsNoTracking()
                .Include(p => p.Gender)
                .FirstOrDefault(p => p.MedicalCode == clean);
        }

        public async Task<Patient?> GetPatientByCodeAsync(string code, CancellationToken ct = default)
        {
            using var db = _contextFactory();
            string clean = code.Trim();
            return await db.Patients
                .AsNoTracking()
                .Include(p => p.Gender)
                .FirstOrDefaultAsync(p => p.MedicalCode == clean, ct);
        }

        public void AddPatient(Patient patient)
        {
            using var db = _contextFactory();
            ValidateAndPreparePatient(db, patient);
            db.Patients.Add(patient);
            db.SaveChanges();
        }

        public async Task AddPatientAsync(Patient patient, CancellationToken ct = default)
        {
            using var db = _contextFactory();
            ValidateAndPreparePatient(db, patient);
            db.Patients.Add(patient);
            await db.SaveChangesAsync(ct);
        }

        private void ValidateAndPreparePatient(LabContext db, Patient patient)
        {
            if (string.IsNullOrWhiteSpace(patient.Name))
                throw new InvalidOperationException("اسم المريض مطلوب ولا يمكن تركه فارغاً.");

            if (string.IsNullOrWhiteSpace(patient.MedicalCode))
                patient.MedicalCode = GenerateNextMedicalCode();

            // Check code uniqueness
            if (db.Patients.Any(p => p.MedicalCode == patient.MedicalCode))
            {
                patient.MedicalCode = GenerateNextMedicalCode();
            }

            patient.CreateDate = DateTime.Now;
        }

        public void UpdatePatient(Patient patient)
        {
            using var db = _contextFactory();
            var existing = db.Patients.Find(patient.Id);
            if (existing == null)
                throw new InvalidOperationException("المريض غير موجود بقاعدة البيانات.");

            if (string.IsNullOrWhiteSpace(patient.Name))
                throw new InvalidOperationException("اسم المريض مطلوب.");

            existing.Name = patient.Name.Trim();
            existing.Phone = patient.Phone?.Trim();
            existing.Address = patient.Address?.Trim();
            existing.DateOfBirth = patient.DateOfBirth;
            existing.AgeAtRecord = patient.AgeAtRecord;
            existing.GenderId = patient.GenderId;

            db.SaveChanges();
        }

        public async Task UpdatePatientAsync(Patient patient, CancellationToken ct = default)
        {
            using var db = _contextFactory();
            var existing = await db.Patients.FindAsync(new object[] { patient.Id }, ct);
            if (existing == null)
                throw new InvalidOperationException("المريض غير موجود بقاعدة البيانات.");

            if (string.IsNullOrWhiteSpace(patient.Name))
                throw new InvalidOperationException("اسم المريض مطلوب.");

            existing.Name = patient.Name.Trim();
            existing.Phone = patient.Phone?.Trim();
            existing.Address = patient.Address?.Trim();
            existing.DateOfBirth = patient.DateOfBirth;
            existing.AgeAtRecord = patient.AgeAtRecord;
            existing.GenderId = patient.GenderId;

            await db.SaveChangesAsync(ct);
        }

        public bool CanDeletePatient(int patientId, out string reason)
        {
            using var db = _contextFactory();
            int visitCount = db.Visits.Count(v => v.PatientId == patientId && !v.IsDeleted);
            if (visitCount > 0)
            {
                reason = $"لا يمكن حذف هذا المريض لوجود ({visitCount}) زيارة مسجلة باسمه. يرجى حذف أو أرشفة الزيارات أولاً.";
                return false;
            }

            reason = string.Empty;
            return true;
        }

        public void DeletePatient(int patientId)
        {
            using var db = _contextFactory();
            var patient = db.Patients.Include(p => p.Visits).FirstOrDefault(p => p.Id == patientId);
            if (patient == null) return;

            if (patient.Visits.Any(v => !v.IsDeleted))
                throw new InvalidOperationException("لا يمكن حذف المريض لوجود زيارات مسجلة باسمه في النظام.");

            db.Patients.Remove(patient);
            db.SaveChanges();
        }

        public async Task DeletePatientAsync(int patientId, CancellationToken ct = default)
        {
            using var db = _contextFactory();
            var patient = await db.Patients.Include(p => p.Visits).FirstOrDefaultAsync(p => p.Id == patientId, ct);
            if (patient == null) return;

            if (patient.Visits.Any(v => !v.IsDeleted))
                throw new InvalidOperationException("لا يمكن حذف المريض لوجود زيارات مسجلة باسمه في النظام.");

            db.Patients.Remove(patient);
            await db.SaveChangesAsync(ct);
        }

        public List<Gender> GetGenders()
        {
            using var db = _contextFactory();
            return db.Genders.AsNoTracking().ToList();
        }
    }
}
