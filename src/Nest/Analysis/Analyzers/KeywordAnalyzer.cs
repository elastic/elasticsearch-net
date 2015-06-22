using System.Collections.Generic;

namespace Nest
{
	/// <summary>
	/// An analyzer of type keyword that “tokenizes” an entire stream as a single token. This is useful for data like zip codes, ids and so on. 
	/// <para>Note, when using mapping definitions, it make more sense to simply mark the field as not_analyzed.</para>
	/// </summary>
	public class KeywordAnalyzer : AnalyzerBase
    {
		public KeywordAnalyzer()
        {
            Type = "keyword";
        }
    }
}