using Common.DependencyLifeTime;

namespace Application.Common.DataInitializer;

public interface IDataInitializer : IScopedService
{
    void InitializeData();
}