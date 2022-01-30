using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using Expression = System.Linq.Expressions.Expression;
using NumericExpression = MathEngine.Expressions.Expression;

namespace MathEngine.Functions
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

            var types = currentAssembly?.GetTypes();
            var targetType = typeof(NumericFunctionCallBaseExpression);
            foreach (var type in types ?? new Type[0])
            {
                if (targetType.IsAssignableFrom(type) && !type.IsInterface && !type.IsAbstract)
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

                    var funcNameAttribute = type.GetCustomAttribute<FunctionNameAttribute>()?.Name ?? "<UNKNOWN>";

                    TypeDictionary.Add(funcNameAttribute, (s, collection) => compiledConstructor(s, collection));
                }
            }

            _initialized = true;
        }

        private delegate NumericFunctionCallBaseExpression ObjectActivator(params object[] args);
    }
}