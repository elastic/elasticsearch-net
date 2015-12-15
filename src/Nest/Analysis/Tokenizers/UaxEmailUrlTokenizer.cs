using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// A tokenizer of type uax_url_email which works exactly like the standard tokenizer, but tokenizes emails and urls as single tokens
	/// </summary>
	public interface IUaxEmailUrlTokenizer : ITokenizer
	{
		/// <summary>
		/// The maximum token length. If a token is seen that exceeds this length then it is discarded. Defaults to 255.
		/// </summary>
		[JsonProperty("max_token_length")]
		int? MaxTokenLength { get; set; }		
	}
	/// <summary/>
	public class UaxEmailUrlTokenizer : TokenizerBase, IUaxEmailUrlTokenizer
    {
		public UaxEmailUrlTokenizer() { Type = "uax_url_email"; }

		/// <summary/>
		public int? MaxTokenLength { get; set; }		
    }
	/// <summary/>
	public class UaxEmailUrlTokenizerDescriptor 
		: TokenizerDescriptorBase<UaxEmailUrlTokenizerDescriptor, IUaxEmailUrlTokenizer>, IUaxEmailUrlTokenizer
	{
		protected override string Type => "uax_url_email";

		int? IUaxEmailUrlTokenizer.MaxTokenLength { get; set; }

		/// <inheritdoc/>
		public UaxEmailUrlTokenizerDescriptor MaxTokenLength(int? maxLength) => Assign(a => a.MaxTokenLength = maxLength);
	}
}
