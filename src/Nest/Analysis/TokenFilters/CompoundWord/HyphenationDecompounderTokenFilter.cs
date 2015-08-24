namespace Nest
{
	public interface IHyphenationDecompounderTokenFilter : ITokenFilter { }
	/// <inheritdoc/>
	public class HyphenationDecompounderTokenFilter : CompoundWordTokenFilter, IHyphenationDecompounderTokenFilter
	{
		public HyphenationDecompounderTokenFilter() : base("hyphenation_decompounder") { }
	}
}