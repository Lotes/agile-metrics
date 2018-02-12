using System;
using Antlr4.Runtime;

namespace MetricEditor.Services.TypeParser
{
    public class ThrowExceptionListener : IAntlrErrorListener<IToken>
    {
        public void SyntaxError(IRecognizer recognizer, IToken offendingSymbol, int line, int charPositionInLine, string msg, RecognitionException e)
        {
            throw e;
        }
    }
}