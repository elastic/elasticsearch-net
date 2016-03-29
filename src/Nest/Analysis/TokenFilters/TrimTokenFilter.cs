namespace Nest
{
	/// <summary>
	/// The trim token filter trims surrounding whitespaces around a token.
	/// </summary>
	public interface ITrimTokenFilter : ITokenFilter { }

	/// <inheritdoc/>
	public class TrimTokenFilter : TokenFilterBase, ITrimTokenFilter
	{
		public TrimTokenFilter() : base("trim") { }
	}
	///<inheritdoc/>
	public class TrimTokenFilterDescriptor 
		: TokenFilterDescriptorBase<TrimTokenFilterDescriptor, ITrimTokenFilter>, ITrimTokenFilter
	{
		protected override string Type => "trim";
	}
}