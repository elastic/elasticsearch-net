using System;
using System.Collections.Generic;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;

using Newtonsoft.Json.Linq;
using System.Linq;
using Elasticsearch.Net;
using System.Reflection;
using System.Globalization;
using System.Collections;
using Nest.Resolvers.Converters;

namespace Nest.Resolvers
{
	public class ElasticContractResolver : DefaultContractResolver
	{
		/// <summary>
		/// ConnectionSettings can be requested by JsonConverter's.
		/// </summary>
		public IConnectionSettingsValues ConnectionSettings { get; private set; }

		public ElasticInferrer Infer { get; private set; }

		/// <summary>
		/// Signals to custom converter that it can get serialization state from one of the converters
		/// Ugly but massive performance gain
		/// </summary>
		internal JsonConverterPiggyBackState PiggyBackState { get; set; }

		public ElasticContractResolver(IConnectionSettingsValues connectionSettings)
			: base(true)
		{
			this.ConnectionSettings = connectionSettings;
			this.Infer = new ElasticInferrer(this.ConnectionSettings);
		}

		protected override JsonConverter ResolveContractConverter(Type objectType)
		{
			return base.ResolveContractConverter(objectType);
		}

		protected override JsonContract CreateContract(Type objectType)
		{
			JsonContract contract = base.CreateContract(objectType);

			// this will only be called once and then cached
			if (typeof(IDictionary).IsAssignableFrom(objectType))
				contract.Converter = new DictionaryKeysAreNotPropertyNamesJsonConverter();

			if (objectType == typeof(Facet))
				contract.Converter = new FacetConverter();
			
			if (objectType == typeof(Uri))
				contract.Converter = new UriJsonConverter();
			
			if (objectType == typeof(IAggregation))
				contract.Converter = new AggregationConverter();
			
			if (objectType == typeof(DateTime) || objectType == typeof(DateTime?))
				contract.Converter = new IsoDateTimeConverter();

			if (typeof(IHit<object>).IsAssignableFrom(objectType))
				contract.Converter = new DefaultHitConverter();
			
			if (objectType == typeof(MultiGetResponse))
				contract.Converter = new MultiGetHitConverter();

			if (objectType == typeof(PropertyNameMarker))
				contract.Converter = new PropertyNameMarkerConverter(this.ConnectionSettings);
			
			if (objectType == typeof(PropertyPathMarker))
				contract.Converter = new PropertyPathMarkerConverter(this.ConnectionSettings);

			if (objectType == typeof(SuggestResponse))
				contract.Converter = new SuggestResponseConverter();

			if (objectType == typeof(MultiSearchResponse))
				contract.Converter = new MultiSearchConverter();

			if (objectType == typeof(IDictionary<string, AnalyzerBase>))
				contract.Converter = new AnalyzerCollectionConverter();

			if (objectType == typeof(IDictionary<string, TokenFilterBase>))
				contract.Converter = new TokenFilterCollectionConverter();

			if (objectType == typeof(IDictionary<string, TokenizerBase>))
				contract.Converter = new TokenizerCollectionConverter();

			if (objectType == typeof(IDictionary<string, CharFilterBase>))
				contract.Converter = new CharFilterCollectionConverter();

			if (this.ConnectionSettings.ContractConverters.HasAny())
			{
				foreach (var c in this.ConnectionSettings.ContractConverters)
				{
					var converter = c(objectType);
					if (converter == null)
						continue;
					contract.Converter = converter;
					break;
				}
			}

			return contract;
		}

		protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
		{
			var defaultProperties = base.CreateProperties(type, memberSerialization);
			var lookup = defaultProperties.ToLookup(p => p.PropertyName);

			defaultProperties = PropertiesOf<IQuery>(type, memberSerialization, defaultProperties, lookup);
			defaultProperties = PropertiesOf<IQueryDescriptor>(type, memberSerialization, defaultProperties, lookup);
			return defaultProperties;
		}

		private IList<JsonProperty> PropertiesOf<T>(Type type, MemberSerialization memberSerialization, IList<JsonProperty> defaultProperties, ILookup<string, JsonProperty> lookup)
		{
			if (!typeof (T).IsAssignableFrom(type)) return defaultProperties;
			var jsonProperties = (
				from i in type.GetInterfaces()
				//where i != typeof (T)
				select base.CreateProperties(i, memberSerialization)
				)
				.SelectMany(interfaceProps => interfaceProps)
				.Where(p => !lookup.Contains(p.PropertyName));
			foreach (var p in jsonProperties)
			{
				defaultProperties.Add(p);
			}
			return defaultProperties;
		}

		protected override string ResolvePropertyName(string propertyName)
		{
			if (this.ConnectionSettings.DefaultPropertyNameInferrer != null)
				return this.ConnectionSettings.DefaultPropertyNameInferrer(propertyName);

			return propertyName.ToCamelCase();
		}

		protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
		{
			var property = base.CreateProperty(member, memberSerialization);

			var attributes = member.GetCustomAttributes(typeof(IElasticPropertyAttribute), false);
			if (attributes == null || !attributes.Any())
				return property;

			var att = attributes.First() as IElasticPropertyAttribute;
			if (!att.Name.IsNullOrEmpty())
				property.PropertyName = att.Name;

			property.Ignored = att.OptOut;
			return property;
		}

	}
}
