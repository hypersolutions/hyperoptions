using System;
using System.Reflection;

namespace HyperOptions
{
    public sealed class TranslateActivator
    {
        public object Activate(Type translatorType, Type targetType, string argValue)
        {
            const string methodName = "Translate";
            object instance;
            MethodInfo method;
            
            if (translatorType.ContainsGenericParameters)
            {
                var genericType = translatorType.MakeGenericType(targetType);
                instance = Activator.CreateInstance(genericType);
                method = genericType.GetMethod(methodName);
            }
            else
            {
                instance = Activator.CreateInstance(translatorType);
                method = translatorType.GetMethod(methodName);
            }
            
            return method?.Invoke(instance, new object[] {argValue});
        }
    }
}
