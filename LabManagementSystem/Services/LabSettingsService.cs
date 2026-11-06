using LabManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace LabManagementSystem.Services
{
    public class LabSettingsService : ILabSettingsService
    {
        private readonly Func<LabContext> _contextFactory;

        public LabSettingsService(Func<LabContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public List<Lab> GetAllLabs()
        {
            using var db = _contextFactory();
            return db.Labs
                .AsNoTracking()
                .Include(l => l.LabCategory)
                .Include(l => l.LabParameters)
                .OrderBy(l => l.Name)
                .ToList();
        }

        public Lab? GetLabById(int id)
        {
            using var db = _contextFactory();
            return db.Labs
                .Include(l => l.LabCategory)
                .Include(l => l.LabParameters)
                .FirstOrDefault(l => l.Id == id);
        }

        public void SaveLab(Lab lab)
        {
            using var db = _contextFactory();
            if (lab.Id == 0)
            {
                db.Labs.Add(lab);
            }
            else
            {
                var existing = db.Labs.Find(lab.Id);
                if (existing != null)
                {
                    existing.Name = lab.Name;
                    existing.Price = lab.Price;
                    existing.Days = lab.Days;
                    existing.LabCategoryId = lab.LabCategoryId;
                    existing.IsExternal = lab.IsExternal;
                }
            }
            db.SaveChanges();
        }

        public void DeleteLab(int id)
        {
            using var db = _contextFactory();
            var lab = db.Labs.Include(l => l.VisitLabs).FirstOrDefault(l => l.Id == id);
            if (lab == null) return;
            if (lab.VisitLabs.Any())
                throw new InvalidOperationException("لا يمكن حذف التحليل لوجود زيارات مسجلة مرتبطة به.");

            db.Labs.Remove(lab);
            db.SaveChanges();
        }

        public List<LabCategory> GetLabCategories()
        {
            using var db = _contextFactory();
            return db.LabCategorys.AsNoTracking().OrderBy(c => c.Name).ToList();
        }

        public void SaveCategory(LabCategory category)
        {
            using var db = _contextFactory();
            if (category.Id == 0)
                db.LabCategorys.Add(category);
            else
            {
                var existing = db.LabCategorys.Find(category.Id);
                if (existing != null)
                    existing.Name = category.Name;
            }
            db.SaveChanges();
        }

        public List<LabParameter> GetParametersForLab(int labId)
        {
            using var db = _contextFactory();
            return db.LabParameters
                .AsNoTracking()
                .Include(p => p.Gender)
                .Where(p => p.LabId == labId)
                .OrderBy(p => p.DisplayOrder)
                .ThenBy(p => p.Id)
                .ToList();
        }

        public void SaveParameter(LabParameter param)
        {
            using var db = _contextFactory();
            // Ensure parameter category exists or default
            if (param.LabParametersCategoryId <= 0)
            {
                var defaultCat = db.LabParameterCategories.FirstOrDefault(c => c.LabId == param.LabId);
                if (defaultCat == null)
                {
                    defaultCat = new LabParameterCategory
                    {
                        Name = "عام",
                        LabId = param.LabId ?? 1
                    };
                    db.LabParameterCategories.Add(defaultCat);
                    db.SaveChanges();
                }
                param.LabParametersCategoryId = defaultCat.Id;
            }

            if (param.Id == 0)
            {
                db.LabParameters.Add(param);
            }
            else
            {
                var existing = db.LabParameters.Find(param.Id);
                if (existing != null)
                {
                    existing.Name = param.Name;
                    existing.Unit = param.Unit;
                    existing.IsNumeric = param.IsNumeric;
                    existing.NormalRangeMin = param.NormalRangeMin;
                    existing.NormalRangeMax = param.NormalRangeMax;
                    existing.NormalText = param.NormalText;
                    existing.DisplayOrder = param.DisplayOrder;
                    existing.GenderId = param.GenderId;
                }
            }
            db.SaveChanges();
        }

        public void DeleteParameter(int paramId)
        {
            using var db = _contextFactory();
            var param = db.LabParameters.Include(p => p.VisitLabResults).FirstOrDefault(p => p.Id == paramId);
            if (param == null) return;
            if (param.VisitLabResults.Any())
                throw new InvalidOperationException("لا يمكن حذف هذا الباراميتر لوجود نتائج تحاليل سابقة مسجلة به.");

            db.LabParameters.Remove(param);
            db.SaveChanges();
        }

        public List<BaseExternalLab> GetAllExternalLabs()
        {
            using var db = _contextFactory();
            return db.BaseExternalLabs.AsNoTracking().OrderBy(l => l.Name).ToList();
        }

        public void SaveExternalLab(BaseExternalLab lab)
        {
            using var db = _contextFactory();
            if (lab.Id == 0)
            {
                db.BaseExternalLabs.Add(lab);
            }
            else
            {
                var existing = db.BaseExternalLabs.Find(lab.Id);
                if (existing != null)
                {
                    existing.Name = lab.Name;
                    existing.Phone = lab.Phone;
                    existing.Address = lab.Address;
                }
            }
            db.SaveChanges();
        }

        public void DeleteExternalLab(int id)
        {
            using var db = _contextFactory();
            var lab = db.BaseExternalLabs.Include(l => l.ExternalLabs).FirstOrDefault(l => l.Id == id);
            if (lab == null) return;
            if (lab.ExternalLabs.Any())
                throw new InvalidOperationException("لا يمكن حذف المعمل لوجود تحاليل محالة إليه.");

            db.BaseExternalLabs.Remove(lab);
            db.SaveChanges();
        }

        private static bool _tableEnsured = false;
        private static readonly object _tableLock = new();

        private void EnsureProfileTableCreated(LabContext db)
        {
            if (_tableEnsured) return;
            lock (_tableLock)
            {
                if (_tableEnsured) return;
                try
                {
                    string sql = @"
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'LaboratoryProfiles')
BEGIN
    CREATE TABLE LaboratoryProfiles (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        LabName NVARCHAR(200) NOT NULL,
        LabNameEn NVARCHAR(200) NULL,
        Subtitle NVARCHAR(300) NULL,
        Address NVARCHAR(300) NULL,
        Phone NVARCHAR(50) NULL,
        AdditionalPhone NVARCHAR(50) NULL,
        Email NVARCHAR(100) NULL,
        DirectorName NVARCHAR(100) NULL,
        ReportFooterNote NVARCHAR(500) NULL,
        UpdatedAt DATETIME NOT NULL DEFAULT GETDATE()
    );
    INSERT INTO LaboratoryProfiles (LabName, LabNameEn, Subtitle, Address, Phone, AdditionalPhone, Email, DirectorName, ReportFooterNote, UpdatedAt)
    VALUES (N'معمل الأمل للتحاليل الطبية', N'Al-Amal Clinical Laboratory', N'Al-Amal Clinical Laboratory & Diagnostic Center', N'شارع الجمهورية - برج الأطباء - الدور الثاني', N'01000000000', N'', N'info@alamallab.com', N'د. أخصائي التحاليل الطبية', N'النتائج معتمدة إلكترونياً. تمنياتنا لكم بالشفاء العاجل.', GETDATE());
END";
                    db.Database.ExecuteSqlRaw(sql);
                    _tableEnsured = true;
                }
                catch
                {
                    _tableEnsured = true;
                }
            }
        }

        public LaboratoryProfile GetLaboratoryProfile()
        {
            using var db = _contextFactory();
            EnsureProfileTableCreated(db);
            var profile = db.LaboratoryProfiles.AsNoTracking().FirstOrDefault();
            if (profile == null)
            {
                profile = new LaboratoryProfile();
                db.LaboratoryProfiles.Add(profile);
                db.SaveChanges();
            }
            return profile;
        }

        public async Task<LaboratoryProfile> GetLaboratoryProfileAsync()
        {
            using var db = _contextFactory();
            EnsureProfileTableCreated(db);
            var profile = await db.LaboratoryProfiles.AsNoTracking().FirstOrDefaultAsync();
            if (profile == null)
            {
                profile = new LaboratoryProfile();
                db.LaboratoryProfiles.Add(profile);
                await db.SaveChangesAsync();
            }
            return profile;
        }

        public void SaveLaboratoryProfile(LaboratoryProfile profile)
        {
            if (string.IsNullOrWhiteSpace(profile.LabName))
                throw new InvalidOperationException("اسم المعمل مطلوب ولا يمكن تركه فارغاً.");

            using var db = _contextFactory();
            EnsureProfileTableCreated(db);

            var existing = db.LaboratoryProfiles.FirstOrDefault();
            if (existing == null)
            {
                profile.UpdatedAt = DateTime.Now;
                db.LaboratoryProfiles.Add(profile);
            }
            else
            {
                existing.LabName = profile.LabName.Trim();
                existing.LabNameEn = string.IsNullOrWhiteSpace(profile.LabNameEn) ? null : profile.LabNameEn.Trim();
                existing.Subtitle = string.IsNullOrWhiteSpace(profile.Subtitle) ? null : profile.Subtitle.Trim();
                existing.Address = string.IsNullOrWhiteSpace(profile.Address) ? null : profile.Address.Trim();
                existing.Phone = string.IsNullOrWhiteSpace(profile.Phone) ? null : profile.Phone.Trim();
                existing.AdditionalPhone = string.IsNullOrWhiteSpace(profile.AdditionalPhone) ? null : profile.AdditionalPhone.Trim();
                existing.Email = string.IsNullOrWhiteSpace(profile.Email) ? null : profile.Email.Trim();
                existing.DirectorName = string.IsNullOrWhiteSpace(profile.DirectorName) ? null : profile.DirectorName.Trim();
                existing.ReportFooterNote = string.IsNullOrWhiteSpace(profile.ReportFooterNote) ? null : profile.ReportFooterNote.Trim();
                existing.UpdatedAt = DateTime.Now;
            }

            db.SaveChanges();
            Configuration.AppConfig.UpdateLabSettings(existing ?? profile);
        }

        public async Task SaveLaboratoryProfileAsync(LaboratoryProfile profile)
        {
            if (string.IsNullOrWhiteSpace(profile.LabName))
                throw new InvalidOperationException("اسم المعمل مطلوب ولا يمكن تركه فارغاً.");

            using var db = _contextFactory();
            EnsureProfileTableCreated(db);

            var existing = await db.LaboratoryProfiles.FirstOrDefaultAsync();
            if (existing == null)
            {
                profile.UpdatedAt = DateTime.Now;
                db.LaboratoryProfiles.Add(profile);
            }
            else
            {
                existing.LabName = profile.LabName.Trim();
                existing.LabNameEn = string.IsNullOrWhiteSpace(profile.LabNameEn) ? null : profile.LabNameEn.Trim();
                existing.Subtitle = string.IsNullOrWhiteSpace(profile.Subtitle) ? null : profile.Subtitle.Trim();
                existing.Address = string.IsNullOrWhiteSpace(profile.Address) ? null : profile.Address.Trim();
                existing.Phone = string.IsNullOrWhiteSpace(profile.Phone) ? null : profile.Phone.Trim();
                existing.AdditionalPhone = string.IsNullOrWhiteSpace(profile.AdditionalPhone) ? null : profile.AdditionalPhone.Trim();
                existing.Email = string.IsNullOrWhiteSpace(profile.Email) ? null : profile.Email.Trim();
                existing.DirectorName = string.IsNullOrWhiteSpace(profile.DirectorName) ? null : profile.DirectorName.Trim();
                existing.ReportFooterNote = string.IsNullOrWhiteSpace(profile.ReportFooterNote) ? null : profile.ReportFooterNote.Trim();
                existing.UpdatedAt = DateTime.Now;
            }

            await db.SaveChangesAsync();
            Configuration.AppConfig.UpdateLabSettings(existing ?? profile);
        }
    }
}

