namespace Nest
{
	/// <summary>
	/// Token filters that allow to decompose compound words.
	/// </summary>
	public interface IHyphenationDecompounderTokenFilter : ICompoundWordTokenFilter { }
	/// <inheritdoc/>
	public class HyphenationDecompounderTokenFilter : CompoundWordTokenFilterBase, IHyphenationDecompounderTokenFilter
	{
		public HyphenationDecompounderTokenFilter() : base("hyphenation_decompounder") { }
	}
	///<inheritdoc/>
	public class HyphenationDecompounderTokenFilterDescriptor 
		: CompoundWordTokenFilterDescriptorBase<HyphenationDecompounderTokenFilterDescriptor, IHyphenationDecompounderTokenFilter>
		, IHyphenationDecompounderTokenFilter
	{
		protected override string Type => "hyphenation_decompounder";
	}

}