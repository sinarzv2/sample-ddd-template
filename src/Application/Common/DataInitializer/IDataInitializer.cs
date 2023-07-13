using Common.DependencyLifeTime;

namespace Application.GeneralServices.DataInitializer
{
    public interface IDataInitializer : IScopedService
    {
        void InitializeData();
    }
}
