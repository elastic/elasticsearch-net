using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// The truncate token filter can be used to truncate tokens into a specific length. This can come in handy with keyword (single token) 
	/// <para> based mapped fields that are used for sorting in order to reduce memory usage.</para>
	/// </summary>
    public class TruncateTokenFilter : TokenFilterBase
    {
		public TruncateTokenFilter()
			: base("truncate")
        {
        }

		/// <summary>
		/// length parameter which control the number of characters to truncate to, defaults to 10.
		/// </summary>
        [JsonProperty("length")]
        public int? Length { get; set; }
    }
}