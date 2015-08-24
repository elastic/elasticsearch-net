using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// A filter that stems words (similar to snowball, but with more options).
	/// </summary>
	public interface IStemmerTokenFilter : ITokenFilter
	{
		[JsonProperty("language")]
		string Language { get; set; }
	}
	public class StemmerTokenFilter : TokenFilterBase, IStemmerTokenFilter
	{
		public StemmerTokenFilter() : base("stemmer") { }

		public string Language { get; set; }

	}
}