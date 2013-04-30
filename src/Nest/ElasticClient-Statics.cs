using System;
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
		private readonly List<JsonConverter> _extraConverters = new List<JsonConverter>();

		private readonly List<JsonConverter> _defaultConverters = new List<JsonConverter>
		{
			new IsoDateTimeConverter(),
			new FacetConverter()
		};

		private JsonSerializerSettings CreateSettings()
		{
			return new JsonSerializerSettings()
			{
				ContractResolver = new ElasticResolver(this.Settings),
				NullValueHandling = NullValueHandling.Ignore,
				DefaultValueHandling = DefaultValueHandling.Include,
				Converters = _defaultConverters.Concat(_extraConverters).ToList()
			};
		}
		public void AddConverter(JsonConverter converter)
		{
			this.IndexSerializationSettings.Converters.Add(converter);
			this.SerializationSettings.Converters.Add(converter);
			_extraConverters.Add(converter);
		}

		public void ModifyJsonSerializationSettings(Action<JsonSerializerSettings> modifier)
		{
			modifier(this.IndexSerializationSettings);
			modifier(this.SerializationSettings);
		}

		/// <summary>
		/// serialize an object using the internal registered converters without camelcasing properties as is done 
		/// while indexing objects
		/// </summary>
		public string Serialize(object @object)
		{
			return JsonConvert.SerializeObject(@object, Formatting.Indented, this.SerializationSettings);
		}

		/// <summary>
		/// Serialize an object using the default camelCasing used while indexing objects
		/// </summary>
		public string SerializeCamelCase(object @object)
		{
			return JsonConvert.SerializeObject(@object, Formatting.Indented, this.IndexSerializationSettings);
		}
		
		/// <summary>
		/// Deserialize an object 
		/// </summary>
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

		public string GetTypeNameFor<T>()
		{
			return GetTypeNameFor(typeof (T));
		}
		public string GetTypeNameFor(Type type)
		{
			return this.TypeNameResolver.GetTypeNameFor(type).Resolve(this.Settings);
		}

		private string ResolveTypeName(TypeNameMarker typeNameMarker, string defaultIndexName = null)
		{
			return typeNameMarker != null ? typeNameMarker.Resolve(this.Settings) : defaultIndexName;
		}

	}
}
