using System;
using System.Collections.Generic;
using System.Linq;

namespace Nest
{
	/// <summary>
	/// A Query that matches documents containing a particular sequence of terms. A PhraseQuery is built by QueryParser for input like "new york".
	/// </summary>
	/// <typeparam name="T">Type of document</typeparam>
	public class MatchPhraseQueryDescriptor<T> : MatchQueryDescriptor<T>
		where T : class
	{

		protected override string _type { get { return "phrase"; } }
		
	}
}
