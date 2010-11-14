using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ElasticSearch.DSL
{
	public class MatchAll : IQuery
	{
		public double Boost { get; private set; }
		
		public MatchAll() {
			this.Boost = 1.0;
		}
		
		public MatchAll(double boost)
		{
			this.Boost = boost;
		}


	}
}
