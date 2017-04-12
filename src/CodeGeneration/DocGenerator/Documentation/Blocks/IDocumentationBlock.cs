namespace DocGenerator.Documentation.Blocks
{
    public interface IDocumentationBlock
    {
        int LineNumber { get; }
        string Value { get; }
        string ToAsciiDoc();
    }
}