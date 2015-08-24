using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// A token filter of type length that removes words that are too long or too short for the stream.
	/// </summary>
	public interface ILengthTokenFilter : ITokenFilter
	{
		/// <summary>
		/// The minimum number. Defaults to 0. 
		/// </summary>
		[JsonProperty("min")]
		int? Minimum { get; set; }

		/// <summary>
		/// The maximum number. Defaults to Integer.MAX_VALUE.
		/// </summary>
		[JsonProperty("max")]
		int? Maximum { get; set; }
	}
	/// <inheritdoc/>
	public class LengthTokenFilter : TokenFilterBase, ILengthTokenFilter
	{
		public LengthTokenFilter() : base("length") { }

		/// <inheritdoc/>
		public int? Minimum { get; set; }

		/// <inheritdoc/>
		public int? Maximum { get; set; }
	}
}