using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// A tokenizer of type whitespace that divides text at whitespace.
	/// </summary>
	public interface IWhitespaceTokenizer : ITokenizer
	{
		/// <summary>
		/// The maximum token length. If a token is seen that exceeds this length then it is split at
		/// <see cref="MaxTokenLength"/> intervals. Defaults to 255.
		/// </summary>
		/// <remarks>
		/// Valid for Elasticsearch 6.1.0+
		/// </remarks>
		[JsonProperty("max_token_length")]
		int? MaxTokenLength { get; set; }
	}

	/// <inheritdoc cref="IWhitespaceTokenizer"/>
	public class WhitespaceTokenizer : TokenizerBase, IWhitespaceTokenizer
    {
		public WhitespaceTokenizer() { Type = "whitespace"; }

	    /// <inheritdoc />
	    public int? MaxTokenLength { get; set; }
    }

	/// <inheritdoc cref="IWhitespaceTokenizer"/>
	public class WhitespaceTokenizerDescriptor
		: TokenizerDescriptorBase<WhitespaceTokenizerDescriptor, IWhitespaceTokenizer>, IWhitespaceTokenizer
	{
		protected override string Type => "whitespace";

		int? IWhitespaceTokenizer.MaxTokenLength { get; set; }

		/// <inheritdoc cref="IWhitespaceTokenizer.MaxTokenLength"/>
		public WhitespaceTokenizerDescriptor MaxTokenLength(int? maxTokenLength) =>
			Assign(a => a.MaxTokenLength = maxTokenLength);
	}
}
