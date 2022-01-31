using System;
using System.IO;
using System.Text;

namespace MathEngine.Engine
{
    internal class Tokenizer
    {
        private readonly TextReader _reader;
        private char _currentChar;
        private char _prevMeaningChar;
        private int _prevMeaningCharPos;

        public Tokenizer(TextReader reader)
        {
            this._reader = reader;
            this.NextChar();
            this.NextToken();
        }

        public int PositionIndex { get; private set; }

        public Token Token { get; private set; }

        public double Number { get; private set; }

        public string Identifier { get; private set; }

        private void NextChar()
        {
            var ch = this._reader.Read();
            this.PositionIndex++;
            this._currentChar = ch < 0 ? '\0' : (char) ch;
        }

        public void NextToken()
        {
            while (char.IsWhiteSpace(this._currentChar))
            {
                this.NextChar();
            }

            this._prevMeaningChar = this._currentChar;
            this._prevMeaningCharPos = this.PositionIndex;

            if (this.TryGetSpecialCharacterToken())
            {
                return;
            }

            if (this.TryGetIdentifier())
            {
                return;
            }

            if (this.TryGetNumberConstant())
            {
                return;
            }

            this.ThrowSyntaxException();
        }

        internal void ThrowSyntaxException()
        {
            throw new ExpressionSyntaxException($"Unexpected character: '{this._prevMeaningChar}' at position {this._prevMeaningCharPos}");
        }

        private bool TryGetSpecialCharacterToken()
        {
            switch (this._currentChar)
            {
                case '\0':
                    this.Token = Token.EOF;
                    return true;
                case '+':
                    this.NextChar();
                    this.Token = Token.Add;
                    return true;
                case '-':
                    this.NextChar();
                    this.Token = Token.Subtract;
                    return true;
                case '*':
                    this.NextChar();
                    this.Token = Token.Multiply;
                    return true;
                case '/':
                    this.NextChar();
                    this.Token = Token.Divide;
                    return true;
                case '(':
                    this.NextChar();
                    this.Token = Token.OpenParenthesis;
                    return true;
                case ')':
                    this.NextChar();
                    this.Token = Token.CloseParenthesis;
                    return true;
                case ',':
                    this.NextChar();
                    this.Token = Token.Comma;
                    return true;
            }

            return false;
        }

        private bool TryGetNumberConstant()
        {
            if (char.IsDigit(this._currentChar) || this._currentChar == '.')
            {
                var inPoint = false;
                var current = 0d;
                var inPointCounter = 0;
                var afterPointNumber = 0L;
                var haveDigits = false;

                var valid = true;
                do
                {
                    if (char.IsDigit(this._currentChar))
                    {
                        if (!inPoint)
                        {
                            current = current * 10 + (this._currentChar - '0');
                        }
                        else
                        {
                            inPointCounter++;
                            afterPointNumber = afterPointNumber * 10 + (this._currentChar - '0');
                        }

                        haveDigits = true;
                    }
                    else if (this._currentChar == '.' && !inPoint)
                    {
                        inPoint = true;
                    }
                    else
                    {
                        valid = false;
                    }

                    if (valid)
                    {
                        this.NextChar();
                    }
                } while (valid);

                this.Number = current + afterPointNumber / Math.Pow(10, inPointCounter);

                this.Token = Token.Number;
                return haveDigits;
            }

            return false;
        }

        private bool TryGetIdentifier()
        {
            if (char.IsLetter(this._currentChar) || this._currentChar == '_')
            {
                var sb = new StringBuilder();

                while (char.IsLetterOrDigit(this._currentChar) || this._currentChar == '_')
                {
                    sb.Append(this._currentChar);
                    this.NextChar();
                }

                this.Identifier = sb.ToString();
                this.Token = Token.Identifier;
                return true;
            }

            return false;
        }
    }
}