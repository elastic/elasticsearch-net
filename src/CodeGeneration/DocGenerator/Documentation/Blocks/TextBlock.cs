namespace DocGenerator.Documentation.Blocks
{
    public class TextBlock : IDocumentationBlock
    {
        public TextBlock(string text, int lineNumber)
        {
            Value = text;
            LineNumber = lineNumber;
        }

        public string Value { get; }
        public int LineNumber { get; }

        public string ToAsciiDoc() => Value;
    }
}