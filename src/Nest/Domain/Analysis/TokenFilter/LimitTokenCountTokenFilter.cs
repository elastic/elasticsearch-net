using Newtonsoft.Json;

namespace Nest
{
    /// <summary>
    /// Limits the number of tokens that are indexed per document and field.
    /// </summary>
    public class LimitTokenCountTokenFilter : TokenFilterBase
    {
        public LimitTokenCountTokenFilter()
            : base("limit")
        {

        }

        /// <summary>
        /// The maximum number of tokens that should be indexed per document and field.
        /// </summary>
        [JsonProperty("max_token_count")]
        public int? MaxTokenCount { get; set; }

        /// <summary>
        /// If set to true the filter exhaust the stream even if max_token_count tokens have been consumed already.
        /// </summary>
        [JsonProperty("consume_all_tokens")]
        public bool? ConsumeAllTokens { get; set; }
    }
}