using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// The truncate token filter can be used to truncate tokens into a specific length. This can come in handy with keyword (single token) 
	/// <para> based mapped fields that are used for sorting in order to reduce memory usage.</para>
	/// </summary>
	public interface ITruncateTokenFilter : ITokenFilter
	{
		/// <summary>
		/// length parameter which control the number of characters to truncate to, defaults to 10.
		/// </summary>
		[JsonProperty("length")]
		int? Length { get; set; }
	} 

	/// <inheritdoc/>
	public class TruncateTokenFilter : TokenFilterBase, ITruncateTokenFilter
	{
		public TruncateTokenFilter() : base("truncate") { }

		/// <inheritdoc/>
		public int? Length { get; set; }
	}
	///<inheritdoc/>
	public class TruncateTokenFilterDescriptor 
		: TokenFilterDescriptorBase<TruncateTokenFilterDescriptor, ITruncateTokenFilter>, ITruncateTokenFilter
	{
		protected override string Type => "truncate";

		int? ITruncateTokenFilter.Length { get; set; }

		///<inheritdoc/>
		public TruncateTokenFilterDescriptor Length(int? length) => Assign(a => a.Length = length);
	}

}