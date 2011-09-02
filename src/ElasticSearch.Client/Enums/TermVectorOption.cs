using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ElasticSearch.Client
{
	public enum TermVectorOption
	{
		No,
		Yes,
		WithOffsets,
		WithPositions,
		WithPositionsOffsets

	}
}
