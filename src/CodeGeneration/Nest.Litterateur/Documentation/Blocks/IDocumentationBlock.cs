namespace Nest.Litterateur.Documentation.Blocks
{
	public interface IDocumentationBlock
	{
		int LineNumber { get; }
		string Value { get; }
	}
}