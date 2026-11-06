using LabManagementSystem.Models;

namespace LabManagementSystem.Services
{
    public interface ILabSettingsService
    {
        // Labs
        List<Lab> GetAllLabs();
        Lab? GetLabById(int id);
        void SaveLab(Lab lab);
        void DeleteLab(int id);

        // Categories
        List<LabCategory> GetLabCategories();
        void SaveCategory(LabCategory category);

        // Parameters
        List<LabParameter> GetParametersForLab(int labId);
        void SaveParameter(LabParameter param);
        void DeleteParameter(int paramId);

        // External Labs
        List<BaseExternalLab> GetAllExternalLabs();
        void SaveExternalLab(BaseExternalLab lab);
        void DeleteExternalLab(int id);

        // Laboratory Profile (General Settings)
        LaboratoryProfile GetLaboratoryProfile();
        Task<LaboratoryProfile> GetLaboratoryProfileAsync();
        void SaveLaboratoryProfile(LaboratoryProfile profile);
        Task SaveLaboratoryProfileAsync(LaboratoryProfile profile);
    }
}

