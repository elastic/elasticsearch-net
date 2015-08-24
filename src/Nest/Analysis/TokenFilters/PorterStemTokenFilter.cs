namespace Nest
{
	/// <summary>
	/// A token filter of type porterStem that transforms the token stream as per the Porter stemming algorithm.
	/// </summary>
	public interface IPorterStemTokenFilter : ITokenFilter { }
	/// <inheritdoc/>
	public class PorterStemTokenFilter : TokenFilterBase, IPorterStemTokenFilter
	{
		public PorterStemTokenFilter() : base("porterStem") { }
	}
}