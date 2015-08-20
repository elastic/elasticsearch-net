using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// A filter that stems words using a Snowball-generated stemmer.
	/// </summary>
	public class SnowballTokenFilter : TokenFilterBase
	{
		public SnowballTokenFilter()
			: base("snowball")
		{

		}

		[JsonProperty("language")]
		public string Language { get; set; }

	}
}