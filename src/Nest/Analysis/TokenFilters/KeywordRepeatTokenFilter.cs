namespace Nest
{
	/// <summary>
	/// The keyword_repeat token filter Emits each incoming token twice once as keyword and once as a non-keyword to allow an unstemmed version of a term to be indexed side by side with the stemmed version of the term.
	/// </summary>
	public interface IKeywordRepeatTokenFilter : ITokenFilter { }
	/// <inheritdoc/>
	public class KeywordRepeatTokenFilter : TokenFilterBase, IKeywordRepeatTokenFilter
	{
		public KeywordRepeatTokenFilter() : base("keyword_repeat") { }
	}
}