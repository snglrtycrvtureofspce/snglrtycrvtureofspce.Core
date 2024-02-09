using System.Linq.Expressions;

namespace snglrtycrvtureofspce.Core.Helpers;

public static class ExpressionHelpers
{
    public static Expression<Func<TEntity, object>> GetSortLambda<TEntity>(string propertyName)
    {
        var parameter = Expression.Parameter(typeof(TEntity), "x");
        var property = Expression.Property(parameter, propertyName);
        var lambda = Expression.Lambda<Func<TEntity, object>>(
            Expression.Convert(property, typeof(object)), parameter);
        
        return lambda;
    }
}