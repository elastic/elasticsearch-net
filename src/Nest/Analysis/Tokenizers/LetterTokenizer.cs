namespace Nest
{
	/// <summary>
	/// A tokenizer of type letter that divides text at non-letters. That’s to say, it defines tokens as maximal strings of adjacent letters. 
	/// <para>Note, this does a decent job for most European languages, but does a terrible job for some Asian languages, where words are not separated by spaces.</para>
	/// </summary>
	public interface ILetterTokenizer : ITokenizer { }
	/// <inheritdoc/>
	public class LetterTokenizer : TokenizerBase, ILetterTokenizer
    {
		public LetterTokenizer() { Type = "letter"; }
    }
	/// <inheritdoc/>
	public class LetterTokenizerDescriptor 
		: TokenizerDescriptorBase<LetterTokenizerDescriptor, ILetterTokenizer>, ILetterTokenizer
	{
		protected override string Type => "letter";
	}
}