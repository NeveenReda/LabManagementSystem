using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LabManagementSystem.Enums;
using LabManagementSystem.Models;
using LabManagementSystem.Printing;
using LabManagementSystem.Services;
using Microsoft.Extensions.DependencyInjection;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using Xunit;

namespace LabManagementSystem.Tests
{
    public class DoctorManagementTests
    {
        private readonly Func<LabContext> _contextFactory = () => new LabContext();
        private readonly IDoctorService _doctorService;
        private readonly IVisitService _visitService;
        private readonly IPatientService _patientService;

        public DoctorManagementTests()
        {
            QuestPDF.Settings.License = LicenseType.Community;

            var services = new ServiceCollection();
            services.AddTransient<LabContext>();
            services.AddSingleton<Func<LabContext>>(sp => () => sp.GetRequiredService<LabContext>());
            services.AddSingleton<IDoctorService, DoctorService>();
            services.AddSingleton<IVisitService, VisitService>();
            services.AddSingleton<IPatientService, PatientService>();
            services.AddSingleton<ILookupCacheService, LookupCacheService>();

            var provider = services.BuildServiceProvider();
            ServiceLocator.Initialize(provider);

            _doctorService = new DoctorService(_contextFactory);
            _visitService = new VisitService(_contextFactory);
            _patientService = new PatientService(_contextFactory);
        }

        [Fact]
        public async Task DoctorService_CrudAndSearchWorkflow_ShouldWorkCorrectly()
        {
            string uniqueSuffix = Guid.NewGuid().ToString("N")[..8];
            string docName = $"د. اختبار إدارة الأطباء_{uniqueSuffix}";
            string specialty = "أمراض الباطنة والقلب";
            string phone = "01012345678";
            string address = "القاهرة - مدينة نصر";

            // 1. Add doctor
            var doc = new Doctor
            {
                Name = docName,
                Specialty = specialty,
                PhoneNumber = phone,
                Address = address,
                Notes = "طبيب معتمد للاختبارات",
                IsActive = true
            };

            int docId = await _doctorService.SaveDoctorAsync(doc);
            Assert.True(docId > 0);

            try
            {
                // 2. Fetch by ID and Name
                var fetched = await _doctorService.GetDoctorByIdAsync(docId);
                Assert.NotNull(fetched);
                Assert.Equal(docName, fetched.Name);
                Assert.Equal(specialty, fetched.Specialty);
                Assert.Equal(phone, fetched.PhoneNumber);
                Assert.True(fetched.IsActive);

                var fetchedByName = await _doctorService.GetDoctorByNameAsync(docName);
                Assert.NotNull(fetchedByName);
                Assert.Equal(docId, fetchedByName.Id);

                // 3. Duplicate name prevention
                var duplicate = new Doctor
                {
                    Name = docName.ToLower(),
                    Specialty = "جراحة"
                };
                await Assert.ThrowsAsync<InvalidOperationException>(() => _doctorService.SaveDoctorAsync(duplicate));

                // 4. Edit doctor
                fetched.Specialty = "استشاري الباطنة العامة";
                fetched.Notes = "تم تحديث التخصص بنجاح";
                int updatedId = await _doctorService.SaveDoctorAsync(fetched);
                Assert.Equal(docId, updatedId);

                var reFetched = await _doctorService.GetDoctorByIdAsync(docId);
                Assert.NotNull(reFetched);
                Assert.Equal("استشاري الباطنة العامة", reFetched.Specialty);

                // 5. Search
                var searchByName = await _doctorService.GetDoctorsOverviewAsync(uniqueSuffix);
                Assert.Contains(searchByName, d => d.Id == docId);

                var searchByPhone = await _doctorService.GetDoctorsOverviewAsync("01012345678");
                Assert.Contains(searchByPhone, d => d.Id == docId);

                // 6. Toggle Active Status
                bool newStatus = await _doctorService.ToggleDoctorActiveStatusAsync(docId);
                Assert.False(newStatus);

                var inactiveCheck = await _doctorService.GetDoctorByIdAsync(docId);
                Assert.NotNull(inactiveCheck);
                Assert.False(inactiveCheck.IsActive);

                // Reactivate
                await _doctorService.ToggleDoctorActiveStatusAsync(docId);
            }
            finally
            {
                // Cleanup
                using var db = _contextFactory();
                var cleanupDoc = db.Doctors.FirstOrDefault(d => d.Id == docId);
                if (cleanupDoc != null)
                {
                    db.Doctors.Remove(cleanupDoc);
                    db.SaveChanges();
                }
            }
        }

        [Fact]
        public async Task VisitIntegration_WithDoctor_ShouldSaveAndLinkDualData()
        {
            string uniqueSuffix = Guid.NewGuid().ToString("N")[..8];
            string docName = $"د. محول الزيارة_{uniqueSuffix}";

            var doctor = new Doctor
            {
                Name = docName,
                Specialty = "أطفال",
                PhoneNumber = "01122334455",
                IsActive = true
            };
            int docId = await _doctorService.SaveDoctorAsync(doctor);

            Patient? patient = null;
            Lab? tempLab = null;
            int visitId = 0;

            try
            {
                using (var db = _contextFactory())
                {
                    var cat = db.LabCategorys.First();
                    var gender = db.Genders.First();

                    tempLab = new Lab
                    {
                        Name = $"فحص تجريبي_{uniqueSuffix}",
                        Price = 250m,
                        Days = 1,
                        LabCategoryId = cat.Id,
                        IsExternal = false
                    };
                    db.Labs.Add(tempLab);

                    patient = new Patient
                    {
                        Name = $"مريض طبيب_{uniqueSuffix}",
                        MedicalCode = $"DOC_{DateTime.Now:MMddHHmmss}",
                        Phone = "01000000000",
                        AgeAtRecord = 30,
                        GenderId = gender.Id
                    };
                    db.Patients.Add(patient);
                    db.SaveChanges();
                }

                // Create visit with DoctorId
                var visit = new Visit
                {
                    PatientId = patient.Id,
                    DoctorId = docId,
                    VisitDate = DateTime.Now,
                    Notes = "زيارة محولة من طبيب"
                };

                visitId = await _visitService.CreateVisitAsync(visit, new List<int> { tempLab.Id }, 250m, (int)PaymentMethodEnum.Cash, "سداد كامل");
                Assert.True(visitId > 0);

                // Verify visit has DoctorId and DrName populated
                var loadedVisit = await _visitService.GetVisitByIdAsync(visitId);
                Assert.NotNull(loadedVisit);
                Assert.Equal(docId, loadedVisit.DoctorId);
                Assert.Equal(docName, loadedVisit.DrName);
                Assert.NotNull(loadedVisit.Doctor);
                Assert.Equal(docName, loadedVisit.Doctor.Name);

                // Verify Soft Delete protection: deleting a doctor who has visits does NOT crash, but deactivates
                bool deleted = await _doctorService.DeleteDoctorAsync(docId);
                Assert.False(deleted); // Soft deactivated, visits protected!

                var protectedDoc = await _doctorService.GetDoctorByIdAsync(docId);
                Assert.NotNull(protectedDoc);
                Assert.False(protectedDoc.IsActive);
            }
            finally
            {
                // Cleanup
                using var db = _contextFactory();
                if (visitId > 0)
                {
                    var v = db.Visits.Find(visitId);
                    if (v != null)
                    {
                        var payments = db.Payments.Where(p => p.VisitId == visitId).ToList();
                        db.Payments.RemoveRange(payments);
                        var vlabs = db.VisitLabs.Where(vl => vl.VisitId == visitId).ToList();
                        db.VisitLabs.RemoveRange(vlabs);
                        db.Visits.Remove(v);
                    }
                }
                if (patient != null)
                {
                    var p = db.Patients.Find(patient.Id);
                    if (p != null) db.Patients.Remove(p);
                }
                if (tempLab != null)
                {
                    var l = db.Labs.Find(tempLab.Id);
                    if (l != null) db.Labs.Remove(l);
                }
                var d = db.Doctors.Find(docId);
                if (d != null) db.Doctors.Remove(d);
                db.SaveChanges();
            }
        }

        [Fact]
        public async Task DoctorVisitReports_And_QuestPdfDocument_ShouldCountVisitsAndGeneratePdf()
        {
            string suffix = Guid.NewGuid().ToString("N")[..8];
            string docAName = $"د. تقارير أ_{suffix}";
            string docBName = $"د. تقارير ب_{suffix}";

            int docAId = await _doctorService.SaveDoctorAsync(new Doctor { Name = docAName, Specialty = "جلدية", IsActive = true });
            int docBId = await _doctorService.SaveDoctorAsync(new Doctor { Name = docBName, Specialty = "عظام", IsActive = true });

            var createdVisits = new List<int>();
            Patient? patient = null;
            Lab? testLab = null;

            try
            {
                using (var db = _contextFactory())
                {
                    var cat = db.LabCategorys.First();
                    var gender = db.Genders.First();

                    testLab = new Lab { Name = $"تحليل_{suffix}", Price = 100m, Days = 1, LabCategoryId = cat.Id };
                    db.Labs.Add(testLab);

                    patient = new Patient
                    {
                        Name = $"مريض تجربة تقرير_{suffix}",
                        MedicalCode = $"RPT_{DateTime.Now:MMddHHmmss}",
                        Phone = "01000000000",
                        AgeAtRecord = 25,
                        GenderId = gender.Id
                    };
                    db.Patients.Add(patient);
                    db.SaveChanges();
                }

                // Create 2 visits for Doctor A
                for (int i = 0; i < 2; i++)
                {
                    int vId = await _visitService.CreateVisitAsync(new Visit
                    {
                        PatientId = patient.Id,
                        DoctorId = docAId,
                        VisitDate = DateTime.Now
                    }, new List<int> { testLab.Id }, 100m, (int)PaymentMethodEnum.Cash, null);
                    createdVisits.Add(vId);
                }

                // Create 1 visit for Doctor B
                int vIdB = await _visitService.CreateVisitAsync(new Visit
                {
                    PatientId = patient.Id,
                    DoctorId = docBId,
                    VisitDate = DateTime.Now
                }, new List<int> { testLab.Id }, 100m, (int)PaymentMethodEnum.Cash, null);
                createdVisits.Add(vIdB);

                // Run Doctor Visit Report
                var report = await _doctorService.GetDoctorVisitReportAsync(DateTime.Today.AddDays(-1), DateTime.Today.AddDays(1));

                var docAReport = report.FirstOrDefault(r => r.DoctorId == docAId);
                var docBReport = report.FirstOrDefault(r => r.DoctorId == docBId);

                Assert.NotNull(docAReport);
                Assert.NotNull(docBReport);
                Assert.Equal(2, docAReport.TotalVisits);
                Assert.Equal(1, docBReport.TotalVisits);

                // Verify sort descending: Doctor A (2 visits) appears before Doctor B (1 visit)
                int indexA = report.IndexOf(docAReport);
                int indexB = report.IndexOf(docBReport);
                Assert.True(indexA < indexB, "Doctor with higher visit count must appear first");

                // Test single doctor filter
                var filtered = await _doctorService.GetDoctorVisitReportAsync(DateTime.Today.AddDays(-1), DateTime.Today.AddDays(1), docBId);
                Assert.Single(filtered);
                Assert.Equal(docBId, filtered[0].DoctorId);
                Assert.Equal(1, filtered[0].TotalVisits);

                // Test QuestPDF generation
                var docPdf = new DoctorVisitReportDocument(report, DateTime.Today.AddDays(-1), DateTime.Today.AddDays(1));
                byte[] pdfBytes = docPdf.GeneratePdf();

                Assert.NotNull(pdfBytes);
                Assert.True(pdfBytes.Length > 1000, "PDF document must be successfully generated with content");
            }
            finally
            {
                // Cleanup
                using var db = _contextFactory();
                foreach (int vid in createdVisits)
                {
                    var v = db.Visits.Find(vid);
                    if (v != null)
                    {
                        var payments = db.Payments.Where(p => p.VisitId == vid).ToList();
                        db.Payments.RemoveRange(payments);
                        var vlabs = db.VisitLabs.Where(vl => vl.VisitId == vid).ToList();
                        db.VisitLabs.RemoveRange(vlabs);
                        db.Visits.Remove(v);
                    }
                }
                if (patient != null)
                {
                    var p = db.Patients.Find(patient.Id);
                    if (p != null) db.Patients.Remove(p);
                }
                if (testLab != null)
                {
                    var l = db.Labs.Find(testLab.Id);
                    if (l != null) db.Labs.Remove(l);
                }
                var da = db.Doctors.Find(docAId);
                if (da != null) db.Doctors.Remove(da);
                var dbDoc = db.Doctors.Find(docBId);
                if (dbDoc != null) db.Doctors.Remove(dbDoc);
                db.SaveChanges();
            }
        }
    }
}
