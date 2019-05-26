using System.Collections.Generic;

namespace Sourcelyzer.GitHub.Extensions
{
    public static class LinqExtensions
    {
        public static IEnumerable<T> Yield<T>(this T @object)
        {
            yield return @object;
        }
    }
}
