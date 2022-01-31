using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using MathEngine.Context;
using MathEngine.Expressions;

namespace MathEngine.Functions
{
    internal abstract class NumericFunctionCallBaseExpression : Expression
    {
        private protected readonly IReadOnlyCollection<Expression> Arguments;

        private readonly string _name;

        protected NumericFunctionCallBaseExpression(string name, IReadOnlyCollection<Expression> arguments)
        {
            this._name = name;
            this.Arguments = arguments;
        }

        public override double Evaluate(IDataContext dataContext)
        {
            if (this.ValidateArguments())
            {
                return this.ExecuteFunction(this.Arguments.Select(x => x.Evaluate(dataContext)).ToArray());
            }
            else
            {
                throw new SyntaxErrorException($"Invalid call of function {this._name}, please validate arguments.");
            }
        }

        private protected abstract bool ValidateArguments();

        private protected abstract double ExecuteFunction(IList<double> arguments);
    }
}