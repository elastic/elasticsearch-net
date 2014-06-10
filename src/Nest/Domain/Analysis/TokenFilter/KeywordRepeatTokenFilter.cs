namespace Nest
{
	/// <summary>
    /// The keyword_repeat token filter Emits each incoming token twice once as keyword and once as a non-keyword to allow an unstemmed version of a term to be indexed side by side with the stemmed version of the term.
	/// </summary>
    public class KeywordRepeatTokenFilter : TokenFilterBase
    {
        public KeywordRepeatTokenFilter()
			: base("keyword_repeat")
        {
        }
    }
}