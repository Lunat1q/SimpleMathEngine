﻿using System;
using System.Collections.Generic;
using System.Linq;
using MathEngine.Expressions;

namespace MathEngine.Functions
{
    [FunctionName("sin")]
    internal class SineFunctionExpression : NumericFunctionCallBaseExpression
    {
        public SineFunctionExpression(string name, IReadOnlyCollection<Expression> arguments) : base(name, arguments)
        {
        }

        private protected override bool ValidateArguments()
        {
            return this.Arguments.Count == 1;
        }

        private protected override decimal ExecuteFunction(IEnumerable<decimal> arguments)
        {
            var rad = MathHelper.Reg2Rad(arguments.First());
            var res = (decimal)Math.Sin(rad);
            return res;
        }

        public override string GetDisplayString()
        {
            return $"sin({string.Join(", ", this.Arguments.Select(x => x.GetDisplayString()))})";
        }
    }
}