using System;
using System.Collections.Generic;
using System.Linq;
using MathEngine.Expressions;

namespace MathEngine.Functions
{
    [FunctionName(FunctionName)]
    internal class LogarithmFunctionExpression : NumericFunctionCallBaseExpression
    {
        internal const string FunctionName = MathFunctionNames.Logarithm;

        public LogarithmFunctionExpression(string name, IReadOnlyCollection<Expression> arguments) : base(name, arguments)
        {
        }

        private protected override bool ValidateArguments()
        {
            return this.Arguments.Count == 2;
        }

        private protected override double ExecuteFunction(IList<double> arguments)
        {
            var logResult = Math.Log(arguments[0], arguments[1]);
            var ret = logResult;
            return ret;
        }

        public override string GetDisplayString()
        {
            return $"{FunctionName}({string.Join(", ", this.Arguments.Select(x => x.GetDisplayString()))})";
        }
    }
}