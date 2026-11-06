using LabManagementSystem.Models;

namespace LabManagementSystem.Services
{
    public interface ILookupCacheService
    {
        List<Gender> GetGenders();
        List<Lab> GetLabs();
        List<LabCategory> GetLabCategories();
        List<Unit> GetUnits();
        List<ProductCategory> GetProductCategories();
        List<BaseExternalLab> GetExternalLabs();

        void InvalidateLabs();
        void InvalidateLabCategories();
        void InvalidateUnits();
        void InvalidateProductCategories();
        void InvalidateExternalLabs();
        void InvalidateAll();
    }

    public class LookupCacheService : ILookupCacheService
    {
        private readonly Func<LabContext> _contextFactory;

        private List<Gender>? _gendersCache;
        private List<Lab>? _labsCache;
        private List<LabCategory>? _labCategoriesCache;
        private List<Unit>? _unitsCache;
        private List<ProductCategory>? _productCategoriesCache;
        private List<BaseExternalLab>? _externalLabsCache;

        private readonly object _syncLock = new();

        public LookupCacheService(Func<LabContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public List<Gender> GetGenders()
        {
            if (_gendersCache != null) return _gendersCache;
            lock (_syncLock)
            {
                if (_gendersCache == null)
                {
                    using var db = _contextFactory();
                    _gendersCache = db.Genders.ToList();
                }
                return _gendersCache;
            }
        }

        public List<Lab> GetLabs()
        {
            if (_labsCache != null) return _labsCache;
            lock (_syncLock)
            {
                if (_labsCache == null)
                {
                    using var db = _contextFactory();
                    _labsCache = db.Labs.OrderBy(l => l.Name).ToList();
                }
                return _labsCache;
            }
        }

        public List<LabCategory> GetLabCategories()
        {
            if (_labCategoriesCache != null) return _labCategoriesCache;
            lock (_syncLock)
            {
                if (_labCategoriesCache == null)
                {
                    using var db = _contextFactory();
                    _labCategoriesCache = db.LabCategorys.OrderBy(c => c.Name).ToList();
                }
                return _labCategoriesCache;
            }
        }

        public List<Unit> GetUnits()
        {
            if (_unitsCache != null) return _unitsCache;
            lock (_syncLock)
            {
                if (_unitsCache == null)
                {
                    using var db = _contextFactory();
                    _unitsCache = db.Units.OrderBy(u => u.Name).ToList();
                }
                return _unitsCache;
            }
        }

        public List<ProductCategory> GetProductCategories()
        {
            if (_productCategoriesCache != null) return _productCategoriesCache;
            lock (_syncLock)
            {
                if (_productCategoriesCache == null)
                {
                    using var db = _contextFactory();
                    _productCategoriesCache = db.ProductCategorys.OrderBy(c => c.Name).ToList();
                }
                return _productCategoriesCache;
            }
        }

        public List<BaseExternalLab> GetExternalLabs()
        {
            if (_externalLabsCache != null) return _externalLabsCache;
            lock (_syncLock)
            {
                if (_externalLabsCache == null)
                {
                    using var db = _contextFactory();
                    _externalLabsCache = db.BaseExternalLabs.OrderBy(l => l.Name).ToList();
                }
                return _externalLabsCache;
            }
        }

        public void InvalidateLabs()
        {
            lock (_syncLock) { _labsCache = null; }
        }

        public void InvalidateLabCategories()
        {
            lock (_syncLock) { _labCategoriesCache = null; }
        }

        public void InvalidateUnits()
        {
            lock (_syncLock) { _unitsCache = null; }
        }

        public void InvalidateProductCategories()
        {
            lock (_syncLock) { _productCategoriesCache = null; }
        }

        public void InvalidateExternalLabs()
        {
            lock (_syncLock) { _externalLabsCache = null; }
        }

        public void InvalidateAll()
        {
            lock (_syncLock)
            {
                _gendersCache = null;
                _labsCache = null;
                _labCategoriesCache = null;
                _unitsCache = null;
                _productCategoriesCache = null;
                _externalLabsCache = null;
            }
        }
    }
}

