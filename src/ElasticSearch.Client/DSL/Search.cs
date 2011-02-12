using System;
using Newtonsoft.Json;

namespace ElasticSearch.Client.DSL
{
	public class Search
	{
		public Query Query { get; set; }
		//public List<Facet> Facets { get; set; }
		
		[JsonProperty(PropertyName = "from")]
		public int Skipping { get; private set; }
		[JsonProperty(PropertyName = "size")]
		public int Taking { get; private set; }
		
		public int Explain { get; set; }
		
		public Search ()
		{
			
		}
		
		public Search Skip(int skip)
		{
			this.Skipping  = skip;
			return this;
		}
		public Search Take(int take)
		{
			this.Taking = take;
			return this;
		}
		
	}
}

