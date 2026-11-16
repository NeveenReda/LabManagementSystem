using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using LabManagementSystem.Models;

namespace LabManagementSystem.Services
{
    public class DoctorListItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string? Specialty { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public string? Notes { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public int TotalVisits { get; set; }
    }

    public class DoctorVisitReportDto
    {
        public int? DoctorId { get; set; }
        public string DoctorName { get; set; } = "";
        public string? Specialty { get; set; }
        public string? PhoneNumber { get; set; }
        public int TotalVisits { get; set; }
        public DateTime? FirstVisitDate { get; set; }
        public DateTime? LastVisitDate { get; set; }
    }

    public interface IDoctorService
    {
        Task<List<Doctor>> GetAllDoctorsAsync(bool onlyActive = false, CancellationToken ct = default);
        Task<List<DoctorListItemDto>> GetDoctorsOverviewAsync(string? searchTerm = null, bool? onlyActive = null, CancellationToken ct = default);
        Task<Doctor?> GetDoctorByIdAsync(int id, CancellationToken ct = default);
        Task<Doctor?> GetDoctorByNameAsync(string name, CancellationToken ct = default);
        Task<int> SaveDoctorAsync(Doctor doctor, CancellationToken ct = default);
        Task<bool> ToggleDoctorActiveStatusAsync(int id, CancellationToken ct = default);
        Task<bool> DeleteDoctorAsync(int id, CancellationToken ct = default);
        Task<List<DoctorVisitReportDto>> GetDoctorVisitReportAsync(DateTime fromDate, DateTime toDate, int? doctorId = null, CancellationToken ct = default);
    }
}
