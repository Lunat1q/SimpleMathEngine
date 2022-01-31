using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FluentAssertions;
using MathEngine.Engine;
using MathEngine.Functions;
using MathEngine.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MathEngine.Test
{
    [TestClass]
    public class FunctionExpressionsRepositoryTest
    {
        [TestMethod]
        public void TestNumberOfFunctionsEqualToRepository()
        {
            var allFunctionsFromRepo = new HashSet<string>(FunctionExpressionsRepository.GetFunctionsList(), StringComparer.OrdinalIgnoreCase);

            var assembly = Assembly.GetAssembly(typeof(FunctionExpressionsRepository));

            var expressionTypes = assembly.GetInheritedTypes<NumericFunctionCallBaseExpression>();

            allFunctionsFromRepo.Should().NotBeEmpty();
            expressionTypes.All(x => allFunctionsFromRepo.Contains(x.GetCustomAttribute<FunctionNameAttribute>().Name)).Should().BeTrue();
        }
    }
}