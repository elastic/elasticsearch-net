using Newtonsoft.Json;

namespace Nest
{
	/// <summary>
	/// The unique token filter can be used to only index unique tokens during analysis. By default it is applied on all the token stream
	/// </summary>
	public interface IUniqueTokenFilter : ITokenFilter
	{
		/// <summary>
		///  If only_on_same_position is set to true, it will only remove duplicate tokens on the same position.
		/// </summary>
		[JsonProperty("only_on_same_position")]
		bool? OnlyOnSamePosition { get; set; }
	}

	/// <inheritdoc/>
	public class UniqueTokenFilter : TokenFilterBase, IUniqueTokenFilter
	{
		public UniqueTokenFilter() : base("unique") { }

		/// <inheritdoc/>
		public bool? OnlyOnSamePosition { get; set; }
	}

	///<inheritdoc/>
	public class UniqueTokenFilterDescriptor 
		: TokenFilterDescriptorBase<UniqueTokenFilterDescriptor, IUniqueTokenFilter>, IUniqueTokenFilter
	{
		protected override string Type => "unique";

		bool? IUniqueTokenFilter.OnlyOnSamePosition { get; set; }

		///<inheritdoc/>
		public UniqueTokenFilterDescriptor OnlyOnSamePosition(bool? samePositionOnly = true) => Assign(a => a.OnlyOnSamePosition = samePositionOnly);
	}

}