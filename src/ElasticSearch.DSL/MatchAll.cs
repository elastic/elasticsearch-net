using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ElasticSearch.DSL
{
	public class MatchAll : IQuery
	{
		public double Boost { get; private set; }
		
		public MatchAll() {}
		
		public MatchAll(double boost)
		{
			this.Boost = boost;
		}


	}
}
