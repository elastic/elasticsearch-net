namespace Nest
{
	public interface IDictionaryDecompounderTokenFilter : ICompoundWordTokenFilter { }
	/// <inheritdoc/>
	public class DictionaryDecompounderTokenFilter : CompoundWordTokenFilterBase, IDictionaryDecompounderTokenFilter
	{
		public DictionaryDecompounderTokenFilter() : base("dictionary_decompounder") { }
	}
	///<inheritdoc/>
	public class DictionaryDecompounderTokenFilterDescriptor
		: CompoundWordTokenFilterDescriptorBase<DictionaryDecompounderTokenFilterDescriptor, IDictionaryDecompounderTokenFilter>
		, IDictionaryDecompounderTokenFilter
	{
		protected override string Type => "dictionary_decompounder";
	}
}