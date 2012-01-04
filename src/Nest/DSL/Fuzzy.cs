using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest.DSL
{
	public class Fuzzy : IQuery
	{
		internal string Field { get; set; }
		internal string Value { get; set; }
		internal double Boost { get; set; }
		internal double SimilarityScore { get; set; }
		internal int PrefixLength { get; set; }
	}
}
