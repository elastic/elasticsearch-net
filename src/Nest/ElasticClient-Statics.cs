using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nest.Thrift;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Fasterflect;
using Newtonsoft.Json.Converters;
using Nest.DSL;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Reflection;
using Nest.Domain;
using Nest.Resolvers.Converters;

namespace Nest
{
	public partial class ElasticClient
	{
		internal static readonly JsonSerializerSettings SerializationSettings;
		internal static readonly PropertyNameResolver PropertyNameResolver;

    private static JsonSerializerSettings CreateSettings()
    {
      return new JsonSerializerSettings()
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
					new ShardsSegmentConverter(),
					new RawOrQueryDescriptorConverter()
				}
      };
    }

    public static void AddConverter(JsonConverter converter)
    {
      ElasticClient.SerializationSettings.Converters.Add(converter);
    }



		static ElasticClient()
		{
      SerializationSettings = ElasticClient.CreateSettings();
			PropertyNameResolver = new PropertyNameResolver(SerializationSettings);

		}
		public static string Serialize<T>(T @object)
		{
			return JsonConvert.SerializeObject(@object, Formatting.Indented, SerializationSettings);
		}
	}
}
