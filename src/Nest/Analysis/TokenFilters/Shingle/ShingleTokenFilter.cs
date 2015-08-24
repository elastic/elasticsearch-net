using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// A token filter of type shingle that constructs shingles (token n-grams) from a token stream. 
	/// <para>In other words, it creates combinations of tokens as a single token. </para>
	/// </summary>
	public interface IShingleTokenFilter : ITokenFilter
	{
		/// <summary>
		/// The minimum shingle size. Defaults to 2.
		/// </summary>
		[JsonProperty("min_shingle_size")]
		int? MinShingleSize { get; set; }

		/// <summary>
		/// The maximum shingle size. Defaults to 2.
		/// </summary>
		[JsonProperty("max_shingle_size")]
		int? MaxShingleSize { get; set; }

		/// <summary>
		/// If true the output will contain the input tokens (unigrams) as well as the shingles. Defaults to true.
		/// </summary>
		[JsonProperty("output_unigrams")]
		bool? OutputUnigrams { get; set; }

		/// <summary>
		/// If output_unigrams is false the output will contain the input tokens (unigrams) if no shingles are available. 
		/// <para>Note if output_unigrams is set to true this setting has no effect. Defaults to false.</para>
		/// </summary>
		[JsonProperty("output_unigrams_if_no_shingles")]
		bool? OutputUnigramsIfNoShingles { get; set; }

		/// <summary>
		/// The string to use when joining adjacent tokens to form a shingle. Defaults to " ".
		/// </summary>
		[JsonProperty("token_separator")]
		string TokenSeparator { get; set; }

		/// <summary>
		/// The string to use as a replacement for each position at which there is no actual token in the stream. For instance this string is used if the position increment is greater than one when a stop filter is used together with the shingle filter. Defaults to "_"
		/// </summary>
		[JsonProperty("filler_token")]
		string FillerToken { get; set; }
	}

	/// <inheritdoc/>
	public class ShingleTokenFilter : TokenFilterBase, IShingleTokenFilter
	{
		public ShingleTokenFilter() : base("shingle") { }

		/// <inheritdoc/>
		public int? MinShingleSize { get; set; }

		/// <inheritdoc/>
		public int? MaxShingleSize { get; set; }

		/// <inheritdoc/>
		public bool? OutputUnigrams { get; set; }

		/// <inheritdoc/>
		public bool? OutputUnigramsIfNoShingles { get; set; }

		/// <inheritdoc/>
		public string TokenSeparator { get; set; }

		/// <inheritdoc/>
		public string FillerToken { get; set; }
	}
	///<inheritdoc/>
	public class ShingleTokenFilterDescriptor 
		: TokenFilterDescriptorBase<ShingleTokenFilterDescriptor, IShingleTokenFilter>, IShingleTokenFilter
	{
		protected override string Type => "shingle";

		bool? IShingleTokenFilter.OutputUnigrams { get; set; }
		bool? IShingleTokenFilter.OutputUnigramsIfNoShingles { get; set; }
		int? IShingleTokenFilter.MinShingleSize { get; set; }
		int? IShingleTokenFilter.MaxShingleSize { get; set; }
		string IShingleTokenFilter.TokenSeparator { get; set; }
		string IShingleTokenFilter.FillerToken { get; set; }

		///<inheritdoc/>
		public ShingleTokenFilterDescriptor OutputUnigrams(bool? output = true) => Assign(a => a.OutputUnigrams = output);

		///<inheritdoc/>
		public ShingleTokenFilterDescriptor OutputUnigramsIfNoShingles(bool? outputIfNo = true) => Assign(a => a.OutputUnigramsIfNoShingles = outputIfNo);

		///<inheritdoc/>
		public ShingleTokenFilterDescriptor MinShingleSize(int? minShingleSize) => Assign(a => a.MinShingleSize = minShingleSize);

		///<inheritdoc/>
		public ShingleTokenFilterDescriptor MaxShingleSize(int? maxShingleSize) => Assign(a => a.MaxShingleSize = maxShingleSize);

		///<inheritdoc/>
		public ShingleTokenFilterDescriptor TokenSeparator(string separator) => Assign(a => a.TokenSeparator = separator);

		///<inheritdoc/>
		public ShingleTokenFilterDescriptor FillerToken(string filler) => Assign(a => a.FillerToken = filler);

	}

}