using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Nest.Resolvers.Converters;
using Nest.Resolvers;

namespace Nest
{
	public partial class ElasticClient
	{
		internal static readonly JsonSerializerSettings DeserializeSettings;
		internal static readonly JsonSerializerSettings SerializationSettings;
		internal static readonly JsonSerializerSettings IndexSerializationSettings;
		public static readonly PropertyNameResolver PropertyNameResolver;

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
		private static JsonSerializerSettings CreateDeserializeSettings()
		{
			return new JsonSerializerSettings()
			{
				ContractResolver = new ElasticResolver(),
				NullValueHandling = NullValueHandling.Ignore,
				DefaultValueHandling = DefaultValueHandling.Ignore,
				Converters = new List<JsonConverter> 
				{ 
					new IsoDateTimeConverter(), 
					new FacetConverter(),
					new ShardsSegmentConverter()
				}
			};
		}
		public static void AddConverter(JsonConverter converter)
		{
			ElasticClient.IndexSerializationSettings.Converters.Add(converter);
			ElasticClient.SerializationSettings.Converters.Add(converter);
			ElasticClient.DeserializeSettings.Converters.Add(converter);
		}

		static ElasticClient()
		{
			DeserializeSettings = ElasticClient.CreateDeserializeSettings();
			SerializationSettings = ElasticClient.CreateSettings();
			var indexSettings = ElasticClient.CreateSettings();
			indexSettings.ContractResolver = new ElasticCamelCaseResolver();
			IndexSerializationSettings = indexSettings;

			PropertyNameResolver = new PropertyNameResolver(SerializationSettings);

		}
		public static string Serialize<T>(T @object)
		{
			return JsonConvert.SerializeObject(@object, Formatting.Indented, SerializationSettings);
		}
		public static T Deserialize<T>(string value)
		{
			return JsonConvert.DeserializeObject<T>(value, DeserializeSettings);
		}
	}
}
