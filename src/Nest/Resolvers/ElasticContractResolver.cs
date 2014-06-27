using System;
using System.Collections.Generic;
using Nest.DSL.Descriptors;
using Nest.DSL.Search;
using Nest.Resolvers.Converters.Aggregations;
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

		public ElasticContractResolver(IConnectionSettingsValues connectionSettings)
			: base(true)
		{
			this.ConnectionSettings = connectionSettings;
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
			defaultProperties = PropertiesOf<IQueryContainer>(type, memberSerialization, defaultProperties, lookup);
			defaultProperties = PropertiesOf<IRequest>(type, memberSerialization, defaultProperties, lookup);
			//defaultProperties = PropertiesOf<ISearchRequest>(type, memberSerialization, defaultProperties, lookup);
			defaultProperties = PropertiesOf<IFilter>(type, memberSerialization, defaultProperties, lookup, append: true);
			defaultProperties = PropertiesOf<IFilterContainer>(type, memberSerialization, defaultProperties, lookup);
			defaultProperties = PropertiesOf<IRandomScoreFunction>(type, memberSerialization, defaultProperties, lookup);
			defaultProperties = PropertiesOf<IFacetRequest>(type, memberSerialization, defaultProperties, lookup);
			defaultProperties = PropertiesOf<IHighlightRequest>(type, memberSerialization, defaultProperties, lookup);
			defaultProperties = PropertiesOf<IHighlightField>(type, memberSerialization, defaultProperties, lookup);
			defaultProperties = PropertiesOf<IRescore>(type, memberSerialization, defaultProperties, lookup);
			defaultProperties = PropertiesOf<IRescoreQuery>(type, memberSerialization, defaultProperties, lookup);
			defaultProperties = PropertiesOf<IAggregationContainer>(type, memberSerialization, defaultProperties, lookup);
			defaultProperties = PropertiesOf<IMetricAggregator>(type, memberSerialization, defaultProperties, lookup);
			defaultProperties = PropertiesOf<IBucketAggregator>(type, memberSerialization, defaultProperties, lookup);
			defaultProperties = PropertiesOf<ISort>(type, memberSerialization, defaultProperties, lookup);
			defaultProperties = PropertiesOf<ISuggestBucket>(type, memberSerialization, defaultProperties, lookup);
			defaultProperties = PropertiesOf<ISuggester>(type, memberSerialization, defaultProperties, lookup);
			defaultProperties = PropertiesOf<IFuzzySuggester>(type, memberSerialization, defaultProperties, lookup);
			defaultProperties = PropertiesOf<IDirectGenerator>(type, memberSerialization, defaultProperties, lookup);
			defaultProperties = PropertiesOf<IAliasAction>(type, memberSerialization, defaultProperties, lookup);
			//defaultProperties = PropertiesOf<ISourceFilter>(type, memberSerialization, defaultProperties, lookup);
			return defaultProperties;
		}

		private IList<JsonProperty> PropertiesOf<T>(Type type, MemberSerialization memberSerialization, IList<JsonProperty> defaultProperties, ILookup<string, JsonProperty> lookup, bool append = false)
		{
			if (!typeof (T).IsAssignableFrom(type)) return defaultProperties;
			var jsonProperties = (
				from i in type.GetInterfaces()
				//where i != typeof (T)
				select base.CreateProperties(i, memberSerialization)
				)
				.SelectMany(interfaceProps => interfaceProps)
				.Where(p => !lookup.Contains(p.PropertyName));
			if (!append)
			{
				foreach (var p in jsonProperties)
				{
					defaultProperties.Add(p);
				}
				return defaultProperties;
			}
			return jsonProperties.Concat(defaultProperties).GroupBy(p=>p.PropertyName).Select(g=>g.First()).ToList();
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
