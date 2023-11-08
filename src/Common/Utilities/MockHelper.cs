using System.Reflection;

namespace Common.Utilities;

public static class MockHelper
{
    public static T PrivateMock<T>(params object?[] parameters)
    {
        var types = new Type?[parameters.Length];
        var i = 0;
        foreach (var o in parameters)
        {
            types[i] = o?.GetType();
            i++;
        }
        return (T)typeof(T)
            .GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic, null, types!, null)?
            .Invoke(parameters)!;
    }
}