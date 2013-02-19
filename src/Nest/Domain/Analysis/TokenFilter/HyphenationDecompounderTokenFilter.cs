namespace Nest
{
	public class HyphenationDecompounderTokenFilter : CompoundWordTokenFilter
	{
		public HyphenationDecompounderTokenFilter()
			: base("hyphenation_decompounder")
		{
		}
	}
}