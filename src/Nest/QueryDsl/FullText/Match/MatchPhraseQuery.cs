namespace Nest
{
	/// <summary>
	/// A Query that matches documents containing a particular sequence of terms. A PhraseQuery is built by QueryParser for input like "new york".
	/// </summary>
	/// <typeparam name="T">Type of document</typeparam>
	public class MatchPhraseQueryDescriptor<T> : MatchQueryDescriptor<T>
		where T : class
	{
		protected override string MatchQueryType => "phrase";
	}

	public class MatchPhraseQuery : MatchQuery
	{
		protected override string MatchQueryType => "phrase";
	}
}
