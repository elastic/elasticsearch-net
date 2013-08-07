using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
	[Flags]
	public enum RewriteMultiTerm
	{
		constant_score_default,
		scoring_boolean,
		constant_score_boolean,
		constant_score_filter,
		top_terms_N,
		top_terms_boost_N
	}
}
