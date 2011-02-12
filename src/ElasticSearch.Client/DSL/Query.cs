using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ElasticSearch.Client.DSL
{
	public class Query
	{
		public Term Term { get; private set; }
		public Fuzzy Fuzzy { get; private set; }

		public Query(IQuery query)
		{
			if(query is Term)
				this.Term = query as Term;

			else if (query is Fuzzy)
				this.Fuzzy = query as Fuzzy;
		
		}

	}
}
