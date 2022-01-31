using System;
using System.Collections.Generic;
using System.IO;
using MathEngine.Engine;
using MathEngine.Expressions;
using MathEngine.Functions;

namespace MathEngine
{
    public class ExpressionParser
    {
        private readonly Tokenizer _tokenizer;

        private ExpressionParser(Tokenizer tokenizer)
        {
            this._tokenizer = tokenizer;
        }

        public Expression ParseExpression()
        {
            var expression = this.NextNormalPriorityOperation();

            if (this._tokenizer.Token != Token.EOF)
            {
                this._tokenizer.ThrowSyntaxException();
            }

            return expression;
        }


        private Expression NextUnary()
        {
            while (true)
            {
                if (this._tokenizer.Token == Token.Add)
                {
                    this._tokenizer.NextToken();
                    continue;
                }

                if (this._tokenizer.Token == Token.Subtract)
                {
                    this._tokenizer.NextToken();

                    var expression = this.NextUnary();

                    return new UnaryMinusExpression(expression);
                }

                return this.NextLeaf();
            }
        }

        private Expression NextNormalPriorityOperation()
        {
            var leftExpression = this.NextHighPriorityOperation();

            while (true)
            {
                var token = this._tokenizer.Token;

                if (!IsBinaryNormalPriorityPredicateToken(token))
                {
                    return leftExpression;
                }

                this._tokenizer.NextToken();

                var rightExpression = this.NextHighPriorityOperation();

                leftExpression = CreateBinaryPredicateExpression(leftExpression, rightExpression, token);
            }
        }

        private Expression NextHighPriorityOperation()
        {
            var leftExpression = this.NextUnary();

            while (true)
            {
                var token = this._tokenizer.Token;

                if (!IsBinaryHighPriorityPredicateToken(token))
                {
                    return leftExpression;
                }

                this._tokenizer.NextToken();

                var rightExpression = this.NextUnary();

                leftExpression = CreateBinaryPredicateExpression(leftExpression, rightExpression, token);
            }
        }

        private static bool IsBinaryNormalPriorityPredicateToken(Token token)
        {
            return token == Token.Add || token == Token.Subtract;
        }

        private static bool IsBinaryHighPriorityPredicateToken(Token token)
        {
            return token == Token.Multiply || token == Token.Divide;
        }

        private static Expression CreateBinaryPredicateExpression(Expression e1, Expression e2, Token token)
        {
            switch (token)
            {
                case Token.Add:
                    return new NumberAddExpression(e1, e2);
                case Token.Subtract:
                    return new NumberSubtractExpression(e1, e2);
                case Token.Multiply:
                    return new NumberMultiplyExpression(e1, e2);
                case Token.Divide:
                    return new NumberDivideExpression(e1, e2);
                default:
                    throw new ArgumentOutOfRangeException(nameof(token), token, null);
            }
        }

        private Expression NextLeaf()
        {
            switch (this._tokenizer.Token)
            {
                case Token.Number:
                {
                    var node = new NumberConstantExpression(this._tokenizer.Number);
                    this._tokenizer.NextToken();
                    return node;
                }
                case Token.OpenParenthesis:
                {
                    this._tokenizer.NextToken();

                    var node = this.NextNormalPriorityOperation();

                    if (this._tokenizer.Token != Token.CloseParenthesis)
                    {
                        throw new ExpressionSyntaxException($"Missing close parenthesis at position {this._tokenizer.PositionIndex}");
                    }

                    this._tokenizer.NextToken();

                    return node;
                }
                case Token.Identifier:
                {
                    var identifierName = this._tokenizer.Identifier;
                    this._tokenizer.NextToken();
                    if (this._tokenizer.Token != Token.OpenParenthesis)
                    {
                        return new StringVariableExpression(identifierName);
                    }

                    this._tokenizer.NextToken();

                    var arguments = new List<Expression>();
                    while (true)
                    {
                        arguments.Add(this.NextNormalPriorityOperation());

                        if (this._tokenizer.Token == Token.Comma)
                        {
                            this._tokenizer.NextToken();
                            continue;
                        }

                        break;
                    }

                    if (this._tokenizer.Token != Token.CloseParenthesis)
                    {
                        throw new ExpressionSyntaxException($"Closing parenthesis is missing at position {this._tokenizer.PositionIndex}");
                    }

                    this._tokenizer.NextToken();

                    return FunctionExpressionsRepository.CreateExpressionByName(identifierName, arguments);
                }
                default:
                    throw new ExpressionSyntaxException($"Unexpected token: {this._tokenizer.Token} at position {this._tokenizer.PositionIndex}");
            }
        }

        public static Expression Parse(string input)
        {
            using var reader = new StringReader(input);
            return Parse(reader);
        }

        public static Expression Parse(StringReader reader)
        {
            var parser = new ExpressionParser(new Tokenizer(reader));
            return parser.ParseExpression();
        }
    }
}