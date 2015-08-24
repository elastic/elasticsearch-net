using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// A token filter of type nGram.
	/// </summary>
	public interface INgramTokenFilter : ITokenFilter
	{
		/// <summary>
		/// Defaults to 1. 
		/// </summary>
		[JsonProperty("min_gram")]
		int? MinGram { get; set; }

		/// <summary>
		/// Defaults to 2 
		/// </summary>
		[JsonProperty("max_gram")]
		int? MaxGram { get; set; }
	}

	/// <inheritdoc/>
	public class NgramTokenFilter : TokenFilterBase
	{
		public NgramTokenFilter() : base("nGram") { }

		/// <inheritdoc/>
		public int? MinGram { get; set; }

		/// <inheritdoc/>
		public int? MaxGram { get; set; }
	}
}