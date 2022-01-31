using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using MathEngine.Functions;
using MathEngine.Helpers;
using Expression = System.Linq.Expressions.Expression;
using NumericExpression = MathEngine.Expressions.Expression;

namespace MathEngine.Engine
{
    internal static class FunctionExpressionsRepository
    {
        private static readonly
            Dictionary<string, Func<string, IReadOnlyCollection<NumericExpression>, NumericFunctionCallBaseExpression>>
            TypeDictionary =
                new Dictionary<string,
                    Func<string, IReadOnlyCollection<NumericExpression>, NumericFunctionCallBaseExpression>>(StringComparer
                    .OrdinalIgnoreCase);

        private static bool _initialized;

        public static NumericFunctionCallBaseExpression CreateExpressionByName(string name, IReadOnlyCollection<NumericExpression> arguments)
        {
            Init();
            if (TypeDictionary.TryGetValue(name, out var expCtor))
            {
                return expCtor(name, arguments);
            }

            throw new SyntaxErrorException($"Unknown function {name}");
        }

        private static void Init()
        {
            if (_initialized)
            {
                return;
            }

            var currentAssembly = Assembly.GetAssembly(typeof(FunctionExpressionsRepository));

            var types = currentAssembly.GetInheritedTypes<NumericFunctionCallBaseExpression>();
            foreach (var type in types ?? new Type[0])
            {
                var ctor = type.GetConstructors()[0];
                var paramsInfo = ctor.GetParameters();

                var param = Expression.Parameter(typeof(object[]), "args");

                var argsExp = new Expression[paramsInfo.Length];

                for (var i = 0; i < paramsInfo.Length; i++)
                {
                    Expression index = Expression.Constant(i);
                    var paramType = paramsInfo[i].ParameterType;

                    Expression paramAccessorExp = Expression.ArrayIndex(param, index);

                    Expression paramCastExp = Expression.Convert(paramAccessorExp, paramType);

                    argsExp[i] = paramCastExp;
                }


                var newExp = Expression.New(ctor, argsExp);

                var lambda = Expression.Lambda(typeof(ObjectActivator), newExp, param);

                var compiledConstructor = (ObjectActivator) lambda.Compile();

                var nameAttribute = type.GetCustomAttribute<FunctionNameAttribute>();

                if (nameAttribute == null)
                {
                    throw new ArgumentNullException(nameof(FunctionNameAttribute), $"Attribute is not found on class {type.Name}");
                }

                var funcNameAttribute = nameAttribute.Name;

                TypeDictionary.Add(funcNameAttribute, (s, collection) => compiledConstructor(s, collection));
            }

            _initialized = true;
        }

        private delegate NumericFunctionCallBaseExpression ObjectActivator(params object[] args);

        public static IReadOnlyCollection<string> GetFunctionsList()
        {
            Init();
            return TypeDictionary.Keys;
        }
    }
}