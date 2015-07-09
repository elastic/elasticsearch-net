using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;

namespace Nest
{
	internal partial class RawDispatch 
	{
		protected IElasticsearchClient Raw { get; set; }

		public RawDispatch(IElasticsearchClient rawElasticClient)
		{
			this.Raw = rawElasticClient;
		}

		internal bool AllSet(params string[] pathVariables) => pathVariables.All(p => !p.IsNullOrEmpty());
	}
}
