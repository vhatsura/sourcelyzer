using System.Collections.Generic;

namespace Sourcelyzer.Analyzing.Nuget
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<TEntity> Yield<TEntity>(this TEntity entity)
        {
            yield return entity;
        }
    }
}
