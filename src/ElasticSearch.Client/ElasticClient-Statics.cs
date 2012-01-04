using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ElasticSearch.Client.Thrift;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Fasterflect;
using ElasticSearch;
using Newtonsoft.Json.Converters;
using ElasticSearch.Client.DSL;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Reflection;
using ElasticSearch.Client.Domain;
using ElasticSearch.Client.Resolvers.Converters;

namespace ElasticSearch.Client
{
	public partial class ElasticClient
	{
		internal static readonly JsonSerializerSettings SerializationSettings;
		internal static readonly PropertyNameResolver PropertyNameResolver;

		static ElasticClient()
		{
			SerializationSettings = new JsonSerializerSettings()
				{
					ContractResolver = new ElasticResolver(),
					NullValueHandling = NullValueHandling.Ignore,
					DefaultValueHandling = DefaultValueHandling.Ignore,
					Converters = new List<JsonConverter> 
				{ 
					new IsoDateTimeConverter(), 
					new TermConverter(), 
					new FacetConverter(),
					new IndexSettingsConverter(),
					new ShardsSegmentConverter()
				}
				};
			PropertyNameResolver = new PropertyNameResolver(SerializationSettings);
		}
		public static string Serialize<T>(T @object)
		{
			return JsonConvert.SerializeObject(@object, Formatting.Indented, SerializationSettings);
		}
	}
}
