using System.Reflection;

namespace Common.Utilities
{
    public static class MockHelper
    {
        public static T PrivateMock<T>(params object?[] list)
        {
            var types = new Type?[list.Length];
            var i = 0;
            foreach (var o in list)
            {
                types[i] = o?.GetType();
                i++;
            }
            return (T)typeof(T)
                .GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic, null, types!, null)?
                .Invoke(list)!;
        }
    }
}
