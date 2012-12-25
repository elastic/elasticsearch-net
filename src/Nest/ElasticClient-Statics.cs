using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Nest.Resolvers.Converters;
using Nest.Resolvers;

namespace Nest
{
	public partial class ElasticClient
	{
		internal readonly JsonSerializerSettings SerializationSettings;
		internal readonly JsonSerializerSettings IndexSerializationSettings;
		internal readonly PropertyNameResolver PropertyNameResolver;

		private JsonSerializerSettings CreateSettings()
		{
			return new JsonSerializerSettings()
			{
				ContractResolver = new ElasticResolver(),
				NullValueHandling = NullValueHandling.Ignore,
				DefaultValueHandling = DefaultValueHandling.Include,
				Converters = new List<JsonConverter> 
				{ 
					new IsoDateTimeConverter(), new FacetConverter(), new BulkOperationResponseItemConverter()
				}
			};
		}
		public void AddConverter(JsonConverter converter)
		{
			this.IndexSerializationSettings.Converters.Add(converter);
			this.SerializationSettings.Converters.Add(converter);
		}
		public string Serialize<T>(T @object)
		{
			return JsonConvert.SerializeObject(@object, Formatting.Indented, this.SerializationSettings);
		}
		public T Deserialize<T>(string value, IEnumerable<JsonConverter> extraConverters = null)
		{
			var settings = this.SerializationSettings;
			if (extraConverters.HasAny())
			{
				settings = this.CreateSettings();
				settings.Converters = settings.Converters.Concat(extraConverters).ToList();
			}
			return JsonConvert.DeserializeObject<T>(value, settings);
		}

	}
}
