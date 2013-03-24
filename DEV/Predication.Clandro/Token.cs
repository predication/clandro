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

        public Token(string type, string text)
        {
            Text = text;
            Type = type;
        }

        public Token(int lineNumber, int linePosition, string text, string type)
        {
            LineNumber = LineNumber;
            LinePosition = linePosition;
            Text = text;
            Type = type;
        }
    }
}
