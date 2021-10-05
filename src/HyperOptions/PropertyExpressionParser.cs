using System;
using System.Linq.Expressions;
using System.Reflection;

namespace HyperOptions
{
    public static class PropertyExpressionParser
    {
        public static OptionInfo GetOptionInfo<TOptions, TTarget>(Expression<Func<TOptions, TTarget>> value)
        {
            var property = GetPropertyInfoExpression(value);
            return new OptionInfo(property.Name, property.PropertyType);
        }

        private static PropertyInfo GetPropertyInfoExpression<TOptions, TTarget>(
            Expression<Func<TOptions, TTarget>> value)
        {
            var body = (MemberExpression)value.Body;
            var propInfo = (PropertyInfo)body.Member;
            
            if (!propInfo.CanWrite)
                throw new ArgumentException($"Property {propInfo.Name} is readonly.");
            
            return propInfo;
        }
    }
}
