using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// The char_group tokenizer breaks text into terms whenever it encounters a character which is in a defined set. It is mostly useful
	/// for cases where a simple custom tokenization is desired, and the overhead of use of the pattern tokenizer is not acceptable.
	/// </summary>
	public interface ICharGroupTokenizer : ITokenizer
	{
		/// <summary>
		/// The maximum token length. If a token is seen that exceeds this length then it is discarded. Defaults to 255.
		/// </summary>
		[JsonProperty("tokenize_on_chars")]
		IEnumerable<string> TokenizeOnCharacters { get; set; }
	}

	/// <inheritdoc cref="ICharGroupTokenizer"/>>
	public class CharGroupTokenizer : TokenizerBase, ICharGroupTokenizer
    {
		public CharGroupTokenizer() => this.Type = "char_group";

	    /// <inheritdoc cref="ICharGroupTokenizer.TokenizeOnCharacters"/>>
		public IEnumerable<string> TokenizeOnCharacters { get; set; }
    }

	/// <inheritdoc cref="ICharGroupTokenizer"/>>
	public class CharGroupTokenizerDescriptor
		: TokenizerDescriptorBase<CharGroupTokenizerDescriptor, ICharGroupTokenizer>, ICharGroupTokenizer
	{
		protected override string Type => "char_group";

		IEnumerable<string> ICharGroupTokenizer.TokenizeOnCharacters { get; set; }

	    /// <inheritdoc cref="ICharGroupTokenizer.TokenizeOnCharacters"/>>
		public CharGroupTokenizerDescriptor TokenizeOnCharacters(params string[] characters) =>
		    Assign(a => a.TokenizeOnCharacters = characters);

	    /// <inheritdoc cref="ICharGroupTokenizer.TokenizeOnCharacters"/>>
		public CharGroupTokenizerDescriptor TokenizeOnCharacters(IEnumerable<string> characters) =>
		    Assign(a => a.TokenizeOnCharacters = characters);
	}
}
