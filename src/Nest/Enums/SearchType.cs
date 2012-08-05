using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest
{
	public enum SearchType
	{
		QueryAndFetch,
		QueryThenFetch,
		DfsQueryThenFetch,
		DfsQueryAndFetch,
		Count,
		Scan
	}
}
