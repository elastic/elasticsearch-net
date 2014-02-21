using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using Elasticsearch.Net;
using Nest.Resolvers.Converters;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Nest.Resolvers;

namespace Nest
{
	internal partial class RawDispatch 
	{
		protected IElasticsearch Raw { get; set; }

		public RawDispatch(IElasticsearch rawElasticClient)
		{
			this.Raw = rawElasticClient;
		}
	}
}
