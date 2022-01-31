using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace MathEngine.Helpers
{
    internal static class ReflectionHelper
    {
        internal static IEnumerable<Type> GetInheritedTypes<T>(this Assembly assembly)
        {
            if (assembly == null)
            {
                throw new ArgumentNullException(nameof(assembly));
            }

            var types = assembly.GetTypes();
            var targetType = typeof(T);
            foreach (var type in types)
            {
                if (targetType.IsAssignableFrom(type) && !type.IsInterface && !type.IsAbstract)
                {
                    yield return type;
                }
            }
        }
    }
}