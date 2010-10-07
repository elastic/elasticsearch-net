using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ElasticSearch.DSL
{
	interface IRange
	{
		string Field { get; }
		int? From { get; }
		int? To { get; }
		double Boost { get; }
		bool Exclusive { get; }
	}
}
