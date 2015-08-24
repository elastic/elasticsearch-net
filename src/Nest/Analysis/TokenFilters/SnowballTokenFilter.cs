using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// A filter that stems words using a Snowball-generated stemmer.
	/// </summary>
	public interface ISnowballTokenFilter : ITokenFilter
	{
		[JsonProperty("language")]
		SnowballLanguage Language { get; set; }
	}

	/// <inheritdoc/>
	public class SnowballTokenFilter : TokenFilterBase, ISnowballTokenFilter
	{
		public SnowballTokenFilter() : base("snowball") { }

		[JsonProperty("language")]
		public SnowballLanguage Language { get; set; }

	}
}