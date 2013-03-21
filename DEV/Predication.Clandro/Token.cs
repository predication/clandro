using System;
using System.Collections.Generic;
using System.Text;

namespace Predication.Clandro
{
    /// <summary>
    /// 
    /// </summary>
    public class Token
    {
        public int LinePosition { get; private set; }
        public int LineNumber { get; private set; }
        public string Text { get; private set; }
        public string Type { get; private set; }
        public bool IsWhitespace { get; private set; }

        public Token(int lineNumber, int linePosition, string text, string type, bool isWhitespace)
        {
            LineNumber = LineNumber;
            LinePosition = linePosition;
            Text = text;
            Type = type;
            IsWhitespace = isWhitespace;
        }
    }
}
