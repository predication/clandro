using System;
using System.Collections.Generic;
using System.Text;

namespace Predication.Clandro
{
    internal enum TokenType
    {
        Whitespace,
        Operator,
        Identifier,
        Bracket,
        String,
        Integer,
        Decimal,
        Comma,
        Unsafe,
        Unknown
    }

    internal class TokenizerState
    {
        private TokenType? currentToken;
        private TokenType? previousToken;
        private string currentString;
        private char? currentChar;
        private char? previousChar;

        private string numbers = "0123456789";
        private string brackets = "{[()]}";
        private string[] operators = {
                                         // single character operators
                                         "&", "|", "=", "!", ">", "<", "?", "%", "^", "*", "+", "-", "/", ":" 
                                         // two character operators
                                         ,"&&", "||", "==", "!=", ">=", "<=", "??"
                                         // three character operators
                                         ,"&&&"
                                     };
        private string[] keywords = { "true", "false", "this", "as", "is", "null" };
        private bool isString = false;
        private bool isOperator = false;
        private bool isEscaped = false;

        private List<string> specialWords = new List<string>();
        private List<char> specialWordChars = new List<char>();
        private string currentStringString = null;

        public TokenizerState()
        {
            currentToken = null;
            previousChar = null;
            previousToken = null;
            previousChar = null;
            currentString = string.Empty;

            foreach (string s in operators)
                specialWords.Add(s);
            foreach (string s in keywords)
                specialWords.Add(s);
            foreach (string s in specialWords)
                foreach (char c in s.ToCharArray())
                    if (!specialWordChars.Contains(c))
                        specialWordChars.Add(c);
        }

        public bool IsFirstPartOfAKeyPerator(string candidate)
        {
            foreach (string op in operators)
                if (op.StartsWith(candidate))
                    return true;
            foreach (string keyword in keywords)
                if (keyword.StartsWith(candidate))
                    return true;
            return false;
        }

        public TokenType CheckKeyPerator(string candidate)
        {
            foreach (string op in operators)
                if (op == candidate)
                    return TokenType.Operator;
            foreach (string keyword in keywords)
                if (keyword == candidate)
                    return TokenType.Operator;
            return TokenType.Unknown;
        }

        public bool IsValidIdentifierCharacter(char character)
        {
            return (char.IsLetterOrDigit(character) || character == '_');
        }

        public bool IsValidIdentifier(string candidate)
        {
            if (!string.IsNullOrEmpty(candidate))
            {
                if (specialWords.Contains(candidate))
                    return false;
                // can start with a letter or _
                if (char.IsLetter(candidate[0]) || candidate[0] == '_')
                {
                    // thereafter all subsequent characters can be alphanumeric or _
                    int length = candidate.Length;
                    for (int x = 1; x < length; x++)
                    {
                        if (!IsValidIdentifierCharacter(candidate[x]))
                            return false;
                    }
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        private void EmitExistingToken(string curString, TokenType? curType, List<Token> theTokens)
        {
            TokenType type = CheckKeyPerator(curString);
            int integer = 0;
            double number = 0;
            if (type == TokenType.Unknown)
            {
                if (IsValidIdentifier(curString))
                {
                    type = TokenType.Identifier;
                }
                else if (int.TryParse(curString, out integer))
                {
                    type = TokenType.Integer;
                }
                else if (double.TryParse(curString, out number))
                {
                    type = TokenType.Decimal;
                }
            }
            Token token = new Token(type.ToString(), curString);
            theTokens.Add(token);
        }

        public List<Token> Emit(char character)
        {
            List<Token> tokens = new List<Token>();
            previousChar = currentChar;
            currentChar = character;

            if (isString)
            {
                if (character == '"')
                {
                    // terminate the string
                    tokens.Add(new Token(TokenType.String.ToString(), currentStringString ?? string.Empty));
                    currentToken = null;
                    currentStringString = null;
                    currentString = string.Empty;
                    isString = false;
                }
                // process string characters here...
                // we just add the character 
                currentStringString += character;
            }
            else
            {
                // we should check if the character could plausably be part of an operator
                if (specialWordChars.Contains(character))
                {
                    // flush out any tokens that are not unknown
                    if (currentToken != null && currentToken.Value != TokenType.Unknown)
                    {
                        EmitExistingToken(currentString, currentToken, tokens);
                        //tokens.Add(new Token { Value = currentString, Type = currentToken.Value });
                        currentToken = TokenType.Identifier;
                        currentString = "";
                    }
                    // this is potentially a part of a keyword or operator
                    // is there already part of a key-perator?
                    if (!string.IsNullOrEmpty(currentString))
                    {
                        // we need to check 
                        // does the current string equate to a key-perator
                        // does the current string plus this character equate to a key-perator
                        if (specialWords.Contains(currentString + character))
                        {
                            // the combination is a valid key-perator
                            currentString = currentString + character;
                            currentToken = TokenType.Unknown;
                        }
                        else if (specialWords.Contains(currentString) && IsValidIdentifier(currentString + character))
                        {
                            // the combination is possible operator
                            currentString = currentString + character;
                            currentToken = TokenType.Unknown;
                        }
                        else if (specialWords.Contains(currentString))
                        {
                            // the existing bits form a key-perator (but not with this bit)
                            TokenType type = CheckKeyPerator(currentString);
                            if (type == TokenType.Unknown && IsValidIdentifier(currentString))
                                type = TokenType.Identifier;
                            Token token = new Token(type.ToString(), currentString);
                            tokens.Add(token);
                            currentString = character.ToString();
                            currentToken = TokenType.Unknown;
                        }
                        else if (IsFirstPartOfAKeyPerator(currentString + character))
                        {
                            // this could still become a valid key-perator
                            currentString = currentString + character;
                            currentToken = TokenType.Unknown;
                        }
                        else if (IsValidIdentifier(currentString + character))
                        {
                            // the combination is possibly a valid identifier
                            currentString = currentString + character;
                        }
                        else if (IsValidIdentifier(currentString))
                        {
                            // emit the current string
                            Token token = new Token(TokenType.Identifier.ToString(), currentString);
                            tokens.Add(token);
                            currentString = character.ToString();
                        }
                        else
                        {
                            // this is part of an identifier?
                            currentString = currentString + character;
                            currentToken = TokenType.Identifier;
                        }
                    }
                    else
                    {
                        // add this to the current list
                        currentString = character.ToString();
                        currentToken = TokenType.Unknown;
                    }
                }
                else
                {
                    // if there is currently a thing there of type unknown
                    // then emit it as a key-perator
                    if (currentToken != null && !string.IsNullOrEmpty(currentString) && currentToken.Value == TokenType.Unknown)
                    {

                        EmitExistingToken(currentString, currentToken, tokens);
                        //TokenType type = CheckKeyPerator(currentString);
                        //Token token = new Token { Type = type, Value = currentString };
                        //tokens.Add(token);
                        currentString = character.ToString();
                        currentToken = TokenType.Unknown;
                    }
                    if (character == ',')
                    {
                        // emit any token already there
                        if (currentToken.HasValue && !string.IsNullOrEmpty(currentString))
                        {
                            EmitExistingToken(currentString, currentToken, tokens);
                            currentString = string.Empty;
                        }
                        // a white space gets emitted immeadiately
                        currentString = string.Empty;
                        tokens.Add(new Token(TokenType.Comma.ToString(),character.ToString()));
                        currentToken = null;
                    }
                    if (character == '"')
                    {
                        // emit any token already there
                        // and put us into string mode...
                        if (currentToken.HasValue && !string.IsNullOrEmpty(currentString))
                        {
                            //tokens.Add(new Token { Value = currentString, Type = currentToken.Value });
                            EmitExistingToken(currentString, currentToken, tokens);
                            currentString = string.Empty;
                        }
                        // put us into string mode
                        currentString = string.Empty;
                        currentStringString = string.Empty;
                        currentToken = TokenType.String;
                        isString = true;
                    }
                    if (character == ' ')
                    {
                        // emit any token already there
                        if (currentToken.HasValue)
                        {
                            //tokens.Add(new Token { Value = currentString, Type = currentToken.Value });
                            EmitExistingToken(currentString, currentToken, tokens);
                            currentString = string.Empty;
                        }
                        // a white space gets emitted immeadiately
                        currentString = string.Empty;
                        tokens.Add(new Token(TokenType.Whitespace.ToString(), character.ToString()));
                        currentToken = null;
                    }
                    else if (brackets.Contains(character.ToString()))
                    {
                        // emit any token already there
                        if (currentToken.HasValue && !string.IsNullOrEmpty(currentString))
                        {

                            //tokens.Add(new Token { Value = currentString, Type = currentToken.Value });
                            EmitExistingToken(currentString, currentToken, tokens);
                            currentString = string.Empty;
                            currentToken = null;
                        }
                        // a bracket gets emitted immeadiately
                        currentString = string.Empty;
                        tokens.Add(new Token(TokenType.Bracket.ToString(), character.ToString()));
                        currentToken = null;
                        currentString = string.Empty;
                    }
                    else if (numbers.Contains(character.ToString()) && currentToken.HasValue && currentToken.Value == TokenType.Identifier)
                    {
                        currentToken = TokenType.Identifier;
                        currentString += character;
                    }
                    else if (numbers.Contains(character.ToString()))
                    {
                        // it might be a identifier
                        // this would be the case if the current string
                        // does not start with a number
                        if (!string.IsNullOrEmpty(currentString) && !numbers.Contains(currentString[0].ToString()) && currentString[0] != '.')
                        {
                            // does not start with a numeric bit
                            // so this is possibly an indentifier
                            currentToken = TokenType.Identifier;
                            currentString += character;
                        }
                        else
                        {

                            if (currentString.Contains("."))
                            {
                                currentToken = TokenType.Decimal;
                                currentString += character.ToString();
                            }
                            else
                            {
                                currentToken = TokenType.Integer;
                                currentString += character.ToString();
                            }
                        }
                    }
                    else if (character == '.')
                    {
                        if (currentString.Contains("."))
                        {
                            // this is odd: emit the previous character and assume that
                            // this is the start of a new decimal 234..456 is 234.0 and 0.456
                            if (currentToken.HasValue)
                            {
                                tokens.Add(new Token(currentToken.ToString(), currentString));
                                currentString = string.Empty;
                            }
                            currentToken = TokenType.Decimal;
                            currentString += character.ToString();
                        }
                        else
                        {
                            // this is now a decimal
                            currentToken = TokenType.Decimal;
                            currentString += character.ToString();
                        }
                    }
                    else
                    {
                        // not recognised as something else so the beginning of an identifier
                        currentToken = TokenType.Identifier;
                        currentString += character.ToString();
                    }
                }
            }
            return tokens;
        }

        public List<Token> Flush()
        {
            List<Token> tokens = new List<Token>();
            if (currentToken.HasValue)
            {
                EmitExistingToken(currentString, currentToken, tokens);
                /*
                if(currentToken == TokenType.Unknown)
                {
                    // check it an operator
                    currentToken = CheckKeyPerator(currentString);
                    if (currentToken.Value == TokenType.Unknown && IsValidIdentifier(currentString))
                        currentToken = TokenType.Identifier;
                    
                }
                tokens.Add(new Token { Type = currentToken.Value, Value = currentString });
                 * */
            }
            return tokens;
        }
    }
}
