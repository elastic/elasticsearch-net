using System;
using System.Collections.Generic;
using System.Linq;

namespace Nest
{
	/// <summary>
	/// A Query that matches documents containing a particular sequence of terms.
	/// It allows for prefix matches on the last term in the text.
	/// </summary>
	/// <typeparam name="T">Type of document</typeparam>
	public class MatchPhrasePrefixQueryDescriptor<T> : MatchQueryDescriptor<T> where T : class
	{
		protected override string MatchQueryType { get { return "phrase_prefix"; } }
	}
}
