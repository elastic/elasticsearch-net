namespace Nest
{
	public interface IHyphenationDecompounderTokenFilter : ICompoundWordTokenFilter { }
	/// <inheritdoc/>
	public class HyphenationDecompounderTokenFilter : CompoundWordTokenFilter, IHyphenationDecompounderTokenFilter
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