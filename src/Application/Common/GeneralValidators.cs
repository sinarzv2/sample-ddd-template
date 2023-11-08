using Domain.SeedWork;

namespace Application.Common;

public class GeneralValidators
{
    public static Func<int?, bool> ValidEnum<T>() where T : Enumeration
    {
        return d => d != null && Enumeration.FromValue<T>(d.Value) != null;
    }

}