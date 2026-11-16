using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LabManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace LabManagementSystem.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly Func<LabContext> _contextFactory;
        private static bool _migrationEnsured = false;
        private static readonly object _migrationLock = new();

        public DoctorService(Func<LabContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public static void EnsureSchema(Func<LabContext> contextFactory)
        {
            if (_migrationEnsured) return;
            lock (_migrationLock)
            {
                if (_migrationEnsured) return;

                using var db = contextFactory();
                try
                {
                    // Step 1: Create Doctors table if not exists
                    string createTableSql = @"
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Doctors')
BEGIN
    CREATE TABLE Doctors (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        Name NVARCHAR(150) NOT NULL,
        Specialty NVARCHAR(100) NULL,
        PhoneNumber NVARCHAR(50) NULL,
        Address NVARCHAR(250) NULL,
        Notes NVARCHAR(500) NULL,
        IsActive BIT NOT NULL DEFAULT 1,
        CreatedAt DATETIME NOT NULL DEFAULT GETDATE()
    );
    CREATE INDEX IX_Doctors_Name ON Doctors(Name);
    CREATE INDEX IX_Doctors_PhoneNumber ON Doctors(PhoneNumber);
END";
                    db.Database.ExecuteSqlRaw(createTableSql);

                    // Step 2: Add DoctorId column to Visits if not exists
                    string addColumnSql = @"
IF NOT EXISTS (
    SELECT * FROM sys.columns 
    WHERE object_id = OBJECT_ID('Visits') AND name = 'DoctorId'
)
BEGIN
    ALTER TABLE Visits ADD DoctorId INT NULL;
    ALTER TABLE Visits ADD CONSTRAINT FK_Visits_Doctors_DoctorId 
        FOREIGN KEY (DoctorId) REFERENCES Doctors(Id) ON DELETE SET NULL;
    CREATE INDEX IX_Visits_DoctorId ON Visits(DoctorId);
END";
                    db.Database.ExecuteSqlRaw(addColumnSql);

                    // Step 3: Migrate existing DrName from Visits into Doctors
                    string migrateDoctorsSql = @"
IF EXISTS (SELECT * FROM sys.tables WHERE name = 'Doctors')
BEGIN
    INSERT INTO Doctors (Name, Specialty, PhoneNumber, Address, Notes, IsActive, CreatedAt)
    SELECT DISTINCT LTRIM(RTRIM(DrName)), NULL, NULL, NULL, N'تم استيراد الطبيب تلقائياً من الزيارات السابقة', 1, GETDATE()
    FROM Visits
    WHERE DrName IS NOT NULL AND LTRIM(RTRIM(DrName)) <> ''
      AND NOT EXISTS (
          SELECT 1 FROM Doctors d WHERE d.Name = LTRIM(RTRIM(Visits.DrName))
      );
END";
                    db.Database.ExecuteSqlRaw(migrateDoctorsSql);

                    // Step 4: Link visits to migrated doctors using dynamic execution to prevent parse errors on fresh column
                    string linkVisitsSql = @"
IF EXISTS (
    SELECT * FROM sys.columns 
    WHERE object_id = OBJECT_ID('Visits') AND name = 'DoctorId'
)
BEGIN
    EXEC('UPDATE Visits
          SET DoctorId = (SELECT TOP 1 d.Id FROM Doctors d WHERE d.Name = LTRIM(RTRIM(Visits.DrName)))
          WHERE DoctorId IS NULL AND DrName IS NOT NULL AND LTRIM(RTRIM(DrName)) <> ''''');
END";
                    db.Database.ExecuteSqlRaw(linkVisitsSql);
                }
                catch
                {
                    // Fallback for InMemory test providers where sys.tables doesn't exist
                    try
                    {
                        var visitsWithDr = db.Visits
                            .Where(v => v.DoctorId == null && !string.IsNullOrEmpty(v.DrName))
                            .ToList();

                        if (visitsWithDr.Count > 0)
                        {
                            var distinctNames = visitsWithDr
                                .Select(v => v.DrName!.Trim())
                                .Distinct(StringComparer.OrdinalIgnoreCase)
                                .ToList();

                            foreach (var name in distinctNames)
                            {
                                var existingDoc = db.Doctors.FirstOrDefault(d => d.Name == name);
                                if (existingDoc == null)
                                {
                                    existingDoc = new Doctor
                                    {
                                        Name = name,
                                        IsActive = true,
                                        CreatedAt = DateTime.UtcNow,
                                        Notes = "تم استيراد الطبيب تلقائياً من الزيارات السابقة"
                                    };
                                    db.Doctors.Add(existingDoc);
                                    db.SaveChanges();
                                }

                                foreach (var v in visitsWithDr.Where(v => string.Equals(v.DrName?.Trim(), name, StringComparison.OrdinalIgnoreCase)))
                                {
                                    v.DoctorId = existingDoc.Id;
                                }
                            }
                            db.SaveChanges();
                        }
                    }
                    catch { }
                }

                _migrationEnsured = true;
            }
        }

        public void EnsureDatabaseSchemaAndMigrate()
        {
            EnsureSchema(_contextFactory);
        }

        public async Task<List<Doctor>> GetAllDoctorsAsync(bool onlyActive = false, CancellationToken ct = default)
        {
            EnsureDatabaseSchemaAndMigrate();
            using var db = _contextFactory();
            var query = db.Doctors.AsNoTracking().AsQueryable();
            if (onlyActive)
            {
                query = query.Where(d => d.IsActive);
            }
            return await query.OrderBy(d => d.Name).ToListAsync(ct);
        }

        public async Task<List<DoctorListItemDto>> GetDoctorsOverviewAsync(string? searchTerm = null, bool? onlyActive = null, CancellationToken ct = default)
        {
            EnsureDatabaseSchemaAndMigrate();
            using var db = _contextFactory();
            var query = db.Doctors.AsNoTracking().AsQueryable();

            if (onlyActive.HasValue)
            {
                query = query.Where(d => d.IsActive == onlyActive.Value);
            }

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                string term = searchTerm.Trim();
                query = query.Where(d =>
                    d.Name.Contains(term) ||
                    (d.Specialty != null && d.Specialty.Contains(term)) ||
                    (d.PhoneNumber != null && d.PhoneNumber.Contains(term)));
            }

            var list = await query
                .Select(d => new DoctorListItemDto
                {
                    Id = d.Id,
                    Name = d.Name,
                    Specialty = d.Specialty,
                    PhoneNumber = d.PhoneNumber,
                    Address = d.Address,
                    Notes = d.Notes,
                    IsActive = d.IsActive,
                    CreatedAt = d.CreatedAt,
                    TotalVisits = d.Visits.Count(v => !v.IsDeleted)
                })
                .OrderBy(d => d.Name)
                .ToListAsync(ct);

            return list;
        }

        public async Task<Doctor?> GetDoctorByIdAsync(int id, CancellationToken ct = default)
        {
            EnsureDatabaseSchemaAndMigrate();
            using var db = _contextFactory();
            return await db.Doctors.FirstOrDefaultAsync(d => d.Id == id, ct);
        }

        public async Task<Doctor?> GetDoctorByNameAsync(string name, CancellationToken ct = default)
        {
            EnsureDatabaseSchemaAndMigrate();
            if (string.IsNullOrWhiteSpace(name)) return null;

            string clean = name.Trim();
            using var db = _contextFactory();
            return await db.Doctors.FirstOrDefaultAsync(d => d.Name == clean, ct);
        }

        public async Task<int> SaveDoctorAsync(Doctor doctor, CancellationToken ct = default)
        {
            EnsureDatabaseSchemaAndMigrate();
            if (doctor == null) throw new ArgumentNullException(nameof(doctor));

            string trimmedName = doctor.Name?.Trim() ?? "";
            if (string.IsNullOrWhiteSpace(trimmedName))
            {
                throw new InvalidOperationException("اسم الطبيب مطلوب ولا يمكن تركه فارغاً.");
            }

            using var db = _contextFactory();

            // Prevent duplicate names
            bool duplicateExists = await db.Doctors
                .AnyAsync(d => d.Id != doctor.Id && d.Name.ToLower() == trimmedName.ToLower(), ct);

            if (duplicateExists)
            {
                throw new InvalidOperationException($"يوجد طبيب مسجل مسبقاً باسم '{trimmedName}'. يرجى اختيار اسم مميز.");
            }

            if (doctor.Id == 0)
            {
                doctor.Name = trimmedName;
                doctor.Specialty = doctor.Specialty?.Trim();
                doctor.PhoneNumber = doctor.PhoneNumber?.Trim();
                doctor.Address = doctor.Address?.Trim();
                doctor.Notes = doctor.Notes?.Trim();
                if (doctor.CreatedAt == default) doctor.CreatedAt = DateTime.UtcNow;

                db.Doctors.Add(doctor);
                await db.SaveChangesAsync(ct);
                return doctor.Id;
            }
            else
            {
                var existing = await db.Doctors.FirstOrDefaultAsync(d => d.Id == doctor.Id, ct);
                if (existing == null)
                {
                    throw new InvalidOperationException("بيانات الطبيب غير موجودة.");
                }

                existing.Name = trimmedName;
                existing.Specialty = doctor.Specialty?.Trim();
                existing.PhoneNumber = doctor.PhoneNumber?.Trim();
                existing.Address = doctor.Address?.Trim();
                existing.Notes = doctor.Notes?.Trim();
                existing.IsActive = doctor.IsActive;

                // Also sync DrName in visits to maintain dual consistency
                var linkedVisits = await db.Visits.Where(v => v.DoctorId == existing.Id).ToListAsync(ct);
                foreach (var v in linkedVisits)
                {
                    v.DrName = trimmedName;
                }

                await db.SaveChangesAsync(ct);
                return existing.Id;
            }
        }

        public async Task<bool> ToggleDoctorActiveStatusAsync(int id, CancellationToken ct = default)
        {
            EnsureDatabaseSchemaAndMigrate();
            using var db = _contextFactory();
            var doctor = await db.Doctors.FirstOrDefaultAsync(d => d.Id == id, ct);
            if (doctor == null) return false;

            doctor.IsActive = !doctor.IsActive;
            await db.SaveChangesAsync(ct);
            return doctor.IsActive;
        }

        public async Task<bool> DeleteDoctorAsync(int id, CancellationToken ct = default)
        {
            EnsureDatabaseSchemaAndMigrate();
            using var db = _contextFactory();
            var doctor = await db.Doctors.Include(d => d.Visits).FirstOrDefaultAsync(d => d.Id == id, ct);
            if (doctor == null) return false;

            // Never delete doctors linked to visits; deactivate them instead
            if (doctor.Visits.Any(v => !v.IsDeleted))
            {
                doctor.IsActive = false;
                await db.SaveChangesAsync(ct);
                return false; // Soft-deactivated
            }

            db.Doctors.Remove(doctor);
            await db.SaveChangesAsync(ct);
            return true;
        }

        public async Task<List<DoctorVisitReportDto>> GetDoctorVisitReportAsync(DateTime fromDate, DateTime toDate, int? doctorId = null, CancellationToken ct = default)
        {
            EnsureDatabaseSchemaAndMigrate();
            using var db = _contextFactory();

            var from = fromDate.Date;
            var toExclusive = toDate.Date.AddDays(1);

            // Fetch non-deleted visits within date range
            var visitsQuery = db.Visits
                .AsNoTracking()
                .Include(v => v.Doctor)
                .Where(v => !v.IsDeleted && v.VisitDate >= from && v.VisitDate < toExclusive);

            if (doctorId.HasValue && doctorId.Value > 0)
            {
                visitsQuery = visitsQuery.Where(v => v.DoctorId == doctorId.Value);
            }

            var visits = await visitsQuery.ToListAsync(ct);

            // Group by Doctor or DrName
            var reportList = new List<DoctorVisitReportDto>();

            // Group 1: Visits with linked Doctor entity
            var doctorGroups = visits
                .Where(v => v.DoctorId.HasValue && v.Doctor != null)
                .GroupBy(v => v.DoctorId!.Value);

            foreach (var group in doctorGroups)
            {
                var doc = group.First().Doctor!;
                reportList.Add(new DoctorVisitReportDto
                {
                    DoctorId = doc.Id,
                    DoctorName = doc.Name,
                    Specialty = doc.Specialty,
                    PhoneNumber = doc.PhoneNumber,
                    TotalVisits = group.Count(),
                    FirstVisitDate = group.Min(v => v.VisitDate),
                    LastVisitDate = group.Max(v => v.VisitDate)
                });
            }

            // Group 2: Historical visits with DrName string but no DoctorId (if any remaining)
            var unlinkedGroups = visits
                .Where(v => !v.DoctorId.HasValue && !string.IsNullOrWhiteSpace(v.DrName))
                .GroupBy(v => v.DrName!.Trim(), StringComparer.OrdinalIgnoreCase);

            foreach (var group in unlinkedGroups)
            {
                string name = group.Key;
                // Check if already in reportList (by name)
                var existing = reportList.FirstOrDefault(r => string.Equals(r.DoctorName, name, StringComparison.OrdinalIgnoreCase));
                if (existing != null)
                {
                    existing.TotalVisits += group.Count();
                    if (group.Min(v => v.VisitDate) < (existing.FirstVisitDate ?? DateTime.MaxValue))
                        existing.FirstVisitDate = group.Min(v => v.VisitDate);
                    if (group.Max(v => v.VisitDate) > (existing.LastVisitDate ?? DateTime.MinValue))
                        existing.LastVisitDate = group.Max(v => v.VisitDate);
                }
                else
                {
                    reportList.Add(new DoctorVisitReportDto
                    {
                        DoctorId = null,
                        DoctorName = name,
                        Specialty = "-",
                        PhoneNumber = "-",
                        TotalVisits = group.Count(),
                        FirstVisitDate = group.Min(v => v.VisitDate),
                        LastVisitDate = group.Max(v => v.VisitDate)
                    });
                }
            }

            // If a specific doctor was selected but had 0 visits in this date range, include them with 0 visits
            if (doctorId.HasValue && doctorId.Value > 0 && reportList.Count == 0)
            {
                var doc = await db.Doctors.AsNoTracking().FirstOrDefaultAsync(d => d.Id == doctorId.Value, ct);
                if (doc != null)
                {
                    reportList.Add(new DoctorVisitReportDto
                    {
                        DoctorId = doc.Id,
                        DoctorName = doc.Name,
                        Specialty = doc.Specialty,
                        PhoneNumber = doc.PhoneNumber,
                        TotalVisits = 0,
                        FirstVisitDate = null,
                        LastVisitDate = null
                    });
                }
            }

            // Sort descending by number of visits, then by name
            return reportList
                .OrderByDescending(r => r.TotalVisits)
                .ThenBy(r => r.DoctorName)
                .ToList();
        }
    }
}
