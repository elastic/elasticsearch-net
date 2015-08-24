using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// A token filter of type edgeNGram.
	/// </summary>
	public interface IEdgeNGramTokenFilter : ITokenFilter
	{
		/// <summary>
		///Defaults to 1. 
		/// </summary>
		[JsonProperty("min_gram")]
		int? MinGram { get; set; }

		/// <summary>
		///Defaults to 2. 
		/// </summary>
		[JsonProperty("max_gram")]
		int? MaxGram { get; set; }

		/// <summary>
		/// Either front or back. Defaults to front.
		/// </summary>
		[JsonProperty("side")]
		EdgeNGramSide? Side { get; set; }

	}
	/// <inheritdoc/>
	public class EdgeNGramTokenFilter : TokenFilterBase, IEdgeNGramTokenFilter
	{
		public EdgeNGramTokenFilter() : base("edgeNGram") { }

		/// <inheritdoc/>
		public int? MinGram { get; set; }

		/// <inheritdoc/>
		public int? MaxGram { get; set; }

		/// <inheritdoc/>
		public EdgeNGramSide? Side { get; set; }
	}
}