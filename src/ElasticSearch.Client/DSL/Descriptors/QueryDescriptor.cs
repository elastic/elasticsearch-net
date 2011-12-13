using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using ElasticSearch.Client.DSL;
using Newtonsoft.Json.Converters;
using ElasticSearch.Client.Resolvers.Converters;

namespace ElasticSearch.Client
{
	public class QueryDescriptor<T> where T : class
	{
		private JsonSerializerSettings SerializationSettings { get; set; }
		private PropertyNameResolver PropertyNameResolver { get; set; }
		public QueryDescriptor()
		{
			this.SerializationSettings = new JsonSerializerSettings()
			{
				ContractResolver = new ElasticResolver(),
				NullValueHandling = NullValueHandling.Ignore,
				Converters = new List<JsonConverter> 
				{ 
					new IsoDateTimeConverter(), 
					new QueryJsonConverter(), 
					new FacetConverter(),
					new IndexSettingsConverter(),
					new ShardsSegmentConverter()
				}
			};
			this.PropertyNameResolver = new PropertyNameResolver(this.SerializationSettings);
		}

	}
}
