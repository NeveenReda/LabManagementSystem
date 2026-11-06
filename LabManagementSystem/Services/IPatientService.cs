using LabManagementSystem.Models;

namespace LabManagementSystem.Services
{
    public class PatientSearchResultDto
    {
        public int Id { get; set; }
        public string MedicalCode { get; set; } = "";
        public string Name { get; set; } = "";
        public string? Phone { get; set; }
        public string Gender { get; set; } = "";
        public int AgeAtRecord { get; set; }
    }

    public interface IPatientService
    {
        string GenerateNextMedicalCode();
        Task<string> GenerateNextMedicalCodeAsync(CancellationToken ct = default);

        List<Patient> SearchPatients(string? searchTerm);
        Task<List<PatientSearchResultDto>> SearchPatientsAsync(string? searchTerm, int maxResults = 100, CancellationToken ct = default);

        Patient? GetPatientById(int id);
        Task<Patient?> GetPatientByIdAsync(int id, CancellationToken ct = default);

        Patient? GetPatientByCode(string code);
        Task<Patient?> GetPatientByCodeAsync(string code, CancellationToken ct = default);

        void AddPatient(Patient patient);
        Task AddPatientAsync(Patient patient, CancellationToken ct = default);

        void UpdatePatient(Patient patient);
        Task UpdatePatientAsync(Patient patient, CancellationToken ct = default);

        bool CanDeletePatient(int patientId, out string reason);
        void DeletePatient(int patientId);
        Task DeletePatientAsync(int patientId, CancellationToken ct = default);

        List<Gender> GetGenders();
    }
}
