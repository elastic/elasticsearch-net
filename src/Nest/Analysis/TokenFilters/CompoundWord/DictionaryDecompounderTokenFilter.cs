namespace Nest
{
	public interface IDictionaryDecompounderTokenFilter : ITokenFilter { }
	/// <inheritdoc/>
	public class DictionaryDecompounderTokenFilter : CompoundWordTokenFilter, IDictionaryDecompounderTokenFilter
	{
		public DictionaryDecompounderTokenFilter() : base("dictionary_decompounder") { }
	}
}