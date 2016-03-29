namespace Nest
{
	/// <summary>
	/// A tokenizer of type whitespace that divides text at whitespace.
	/// </summary>
	public interface IWhitespaceTokenizer : ITokenizer { }
	/// <inheritdoc/>
	public class WhitespaceTokenizer : TokenizerBase, IWhitespaceTokenizer
    {
		public WhitespaceTokenizer() { Type = "whitespace"; }
    }
	/// <inheritdoc/>
	public class WhitespaceTokenizerDescriptor 
		: TokenizerDescriptorBase<WhitespaceTokenizerDescriptor, IWhitespaceTokenizer>, IWhitespaceTokenizer
	{
		protected override string Type => "whitespace";
	}
}