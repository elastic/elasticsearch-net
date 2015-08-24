using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// A filter that stems words using a Snowball-generated stemmer.
	/// </summary>
	public interface ISnowballTokenFilter : ITokenFilter
	{
		[JsonProperty("language")]
		SnowballLanguage? Language { get; set; }
	}

	/// <inheritdoc/>
	public class SnowballTokenFilter : TokenFilterBase, ISnowballTokenFilter
	{
		public SnowballTokenFilter() : base("snowball") { }

		///<inheritdoc/>
		[JsonProperty("language")]
		public SnowballLanguage? Language { get; set; }

	}
	///<inheritdoc/>
	public class SnowballTokenFilterDescriptor 
		: TokenFilterDescriptorBase<SnowballTokenFilterDescriptor, ISnowballTokenFilter>, ISnowballTokenFilter
	{
		protected override string Type => "snowball";

		SnowballLanguage? ISnowballTokenFilter.Language { get; set; }

		///<inheritdoc/>
		public SnowballTokenFilterDescriptor Language(SnowballLanguage? language) => Assign(a => a.Language = language);
	}

}