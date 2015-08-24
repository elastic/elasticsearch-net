namespace Nest
{
	/// <summary>
	/// A token filter of type standard that normalizes tokens extracted with the Standard Tokenizer.
	/// </summary>
	public interface IStandardTokenFilter : ITokenFilter { }
	/// <inheritdoc/>
	public class StandardTokenFilter : TokenFilterBase, IStandardTokenFilter
	{
		public StandardTokenFilter() : base("standard") { }
	}

	///<inheritdoc/>
	public class StandardTokenFilterDescriptor 
		: TokenFilterDescriptorBase<StandardTokenFilterDescriptor, IStandardTokenFilter>, IStandardTokenFilter
	{
		protected override string Type => "standard";
	}

}