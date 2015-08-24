using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// Limits the number of tokens that are indexed per document and field.
	/// </summary>
	public interface ILimitTokenCountTokenFilter : ITokenFilter
	{
		/// <summary>
		/// The maximum number of tokens that should be indexed per document and field.
		/// </summary>
		[JsonProperty("max_token_count")]
		int? MaxTokenCount { get; set; }

		/// <summary>
		/// If set to true the filter exhaust the stream even if max_token_count tokens have been consumed already.
		/// </summary>
		[JsonProperty("consume_all_tokens")]
		bool? ConsumeAllTokens { get; set; }
	}
	/// <inheritdoc/>
	public class LimitTokenCountTokenFilter : TokenFilterBase, ILimitTokenCountTokenFilter
	{
		public LimitTokenCountTokenFilter() : base("limit") { }

		/// <inheritdoc/>
		public int? MaxTokenCount { get; set; }

		/// <inheritdoc/>
		public bool? ConsumeAllTokens { get; set; }
	}
}