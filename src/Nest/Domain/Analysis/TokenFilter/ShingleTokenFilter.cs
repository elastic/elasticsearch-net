using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// A token filter of type shingle that constructs shingles (token n-grams) from a token stream. 
	/// <para>In other words, it creates combinations of tokens as a single token. </para>
	/// </summary>
    public class ShingleTokenFilter : TokenFilterBase
    {
        public ShingleTokenFilter() : base("shingle")
        {

        }

		/// <summary>
		/// The minimum shingle size. Defaults to 2.
		/// </summary>
		[JsonProperty("min_shingle_size")]
		public int? MinShingleSize { get; set; }

		/// <summary>
		/// The maximum shingle size. Defaults to 2.
		/// </summary>
        [JsonProperty("max_shingle_size")]
        public int? MaxShingleSize { get; set; }

		/// <summary>
		/// If true the output will contain the input tokens (unigrams) as well as the shingles. Defaults to true.
		/// </summary>
        [JsonProperty("output_unigrams")]
        public bool? OutputUnigrams { get; set; }

		/// <summary>
		/// If output_unigrams is false the output will contain the input tokens (unigrams) if no shingles are available. 
		/// <para>Note if output_unigrams is set to true this setting has no effect. Defaults to false.</para>
		/// </summary>
		[JsonProperty("output_unigrams_if_no_shingles")]
		public bool? OutputUnigramsIfNoShingles { get; set; }

		/// <summary>
		/// The string to use when joining adjacent tokens to form a shingle. Defaults to " ".
		/// </summary>
		[JsonProperty("token_separator")]
		public string TokenSeparator { get; set; }

          /// <summary>
        /// The string to use as a replacement for each position at which there is no actual token in the stream. For instance this string is used if the position increment is greater than one when a stop filter is used together with the shingle filter. Defaults to "_"
        /// </summary>
        [JsonProperty("filler_token")]
        public string FillerToken { get; set; }
    }
}