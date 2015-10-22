using System;
using System.Collections.Generic;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using System.Linq;
using System.Reflection;
using System.Collections;

namespace Nest.Resolvers
{
	public class ElasticContractResolver : DefaultContractResolver
	{
		public static JsonSerializer Empty { get; } = new JsonSerializer();


		/// <summary>
		/// ConnectionSettings can be requested by JsonConverter's.
		/// </summary>
		public IConnectionSettingsValues ConnectionSettings { get; private set; }

		public ElasticContractResolver(IConnectionSettingsValues connectionSettings)
		{
			this.ConnectionSettings = connectionSettings;
		}

		protected override JsonContract CreateContract(Type objectType)
		{
			JsonContract contract = base.CreateContract(objectType);

			// this will only be called once and then cached

			if (typeof(IDictionary).IsAssignableFrom(objectType)  
				&& !typeof(IMappings).IsAssignableFrom(objectType)
				&& !typeof(IProperties).IsAssignableFrom(objectType)
				&& !typeof(IDynamicIndexSettings).IsAssignableFrom(objectType)
				)
				contract.Converter = new VerbatimDictionaryKeysJsonConverter();

			else if (objectType == typeof(IAggregation)) contract.Converter = new AggregationJsonConverter();
			else if (objectType == typeof(ISimilarity)) contract.Converter = new SimilarityJsonConverter();
			else if (objectType == typeof(ICharFilter)) contract.Converter = new CharFilterJsonConverter();
			else if (objectType == typeof(IAnalyzer)) contract.Converter = new AnalyzerJsonConverter();
			else if (objectType == typeof(ITokenizer)) contract.Converter = new TokenizerJsonConverter();
			else if (objectType == typeof(ITokenFilter)) contract.Converter = new TokenFilterJsonConverter();

			else if (typeof(IClusterRerouteCommand).IsAssignableFrom(objectType))
				contract.Converter = new ClusterRerouteCommandJsonConverter();

			else if (objectType == typeof(DateTime) || objectType == typeof(DateTime?))
				contract.Converter = new IsoDateTimeConverter();

			else if (objectType == typeof(TypeName)) contract.Converter = new TypeNameJsonConverter();
			else if (objectType == typeof(IndexName)) contract.Converter = new IndexNameJsonConverter();
			else if (objectType == typeof(FieldName)) contract.Converter = new FieldNameJsonConverter(this.ConnectionSettings);
			else if (objectType == typeof(PropertyName)) contract.Converter = new PropertyNameJsonConverter(this.ConnectionSettings);

			//TODO these should not be necessary here
			else if (objectType == typeof(MultiSearchResponse)) contract.Converter = new MultiSearchJsonConverter();
			else if (objectType == typeof(MultiGetResponse)) contract.Converter = new MultiGetHitJsonConverter();
			else if (typeof(IHit<object>).IsAssignableFrom(objectType)) contract.Converter = new DefaultHitJsonConverter();

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

			defaultProperties = PropertiesOf<IIndexState>(type, memberSerialization, defaultProperties, lookup);
			defaultProperties = PropertiesOf<IIndexSettings>(type, memberSerialization, defaultProperties, lookup);

			defaultProperties = PropertiesOf<IAnalysis>(type, memberSerialization, defaultProperties, lookup);
			defaultProperties = PropertiesOf<ISimilarity>(type, memberSerialization, defaultProperties, lookup);
			defaultProperties = PropertiesOf<ICharFilter>(type, memberSerialization, defaultProperties, lookup);
			defaultProperties = PropertiesOf<IAnalyzer>(type, memberSerialization, defaultProperties, lookup);
			defaultProperties = PropertiesOf<ITokenizer>(type, memberSerialization, defaultProperties, lookup);
			defaultProperties = PropertiesOf<ITokenFilter>(type, memberSerialization, defaultProperties, lookup);



			defaultProperties = PropertiesOf<ITypeMapping>(type, memberSerialization, defaultProperties, lookup);

			defaultProperties = PropertiesOf<IQuery>(type, memberSerialization, defaultProperties, lookup);
			defaultProperties = PropertiesOf<ISpecialField>(type, memberSerialization, defaultProperties, lookup);
			defaultProperties = PropertiesOf<IExternalFieldDeclaration>(type, memberSerialization, defaultProperties, lookup);
			defaultProperties = PropertiesOf<IQueryContainer>(type, memberSerialization, defaultProperties, lookup);
			defaultProperties = PropertiesOf<IRequest>(type, memberSerialization, defaultProperties, lookup);
			defaultProperties = PropertiesOf<IQueryContainer>(type, memberSerialization, defaultProperties, lookup);
			defaultProperties = PropertiesOf<IRandomScoreFunction>(type, memberSerialization, defaultProperties, lookup);
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
			defaultProperties = PropertiesOf<IBulkOperation>(type, memberSerialization, defaultProperties, lookup);
			defaultProperties = PropertiesOf<IMultiGetOperation>(type, memberSerialization, defaultProperties, lookup);
			defaultProperties = PropertiesOf<IRepository>(type, memberSerialization, defaultProperties, lookup);
			defaultProperties = PropertiesOf<IAlias>(type, memberSerialization, defaultProperties, lookup);
			defaultProperties = PropertiesOf<IInnerHitsContainer>(type, memberSerialization, defaultProperties, lookup);
			defaultProperties = PropertiesOf<IInnerHits>(type, memberSerialization, defaultProperties, lookup);
			defaultProperties = PropertiesOf<IProperty>(type, memberSerialization, defaultProperties, lookup);


			defaultProperties = PropertiesOf<IClusterRerouteCommand>(type, memberSerialization, defaultProperties, lookup);
			defaultProperties = PropertiesOf<IMultiTermVectorOperation>(type, memberSerialization, defaultProperties, lookup);
			defaultProperties = PropertiesOf<IRepository>(type, memberSerialization, defaultProperties, lookup);

			defaultProperties = PropertiesOf<INestSerializable>(type, memberSerialization, defaultProperties, lookup);

			return defaultProperties.GroupBy(p => p.PropertyName).Select(p => p.First()).ToList();
		}

		public IList<JsonProperty> PropertiesOfAllInterfaces(Type t, MemberSerialization memberSerialization)
		{
			return (
				from i in t.GetInterfaces()
				select base.CreateProperties(i, memberSerialization)
				)
				.SelectMany(interfaceProps => interfaceProps)
				.DistinctBy(p=>p.PropertyName)
				.ToList();

		}

		public IList<JsonProperty> PropertiesOfAll(Type t, MemberSerialization memberSerialization)
		{
			return base.CreateProperties(t, memberSerialization)
				.Concat(PropertiesOfAllInterfaces(t, memberSerialization))
				.DistinctBy(p=>p.PropertyName)
				.ToList();

		}
		private IList<JsonProperty> PropertiesOf<T>(Type type, MemberSerialization memberSerialization, IList<JsonProperty> defaultProperties, ILookup<string, JsonProperty> lookup, bool append = false)
		{
			if (!typeof (T).IsAssignableFrom(type)) return defaultProperties;
			var jsonProperties = (
				from i in type.GetInterfaces()
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
			return jsonProperties.Concat(defaultProperties).ToList();
		}

		protected override string ResolvePropertyName(string fieldName)
		{
			if (this.ConnectionSettings.DefaultFieldNameInferrer != null)
				return this.ConnectionSettings.DefaultFieldNameInferrer(fieldName);

			return fieldName.ToCamelCase();
		}

		protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
		{
			var property = base.CreateProperty(member, memberSerialization);

			// Skip serialization of empty collections that has DefaultValueHandling set to Ignore.
			if (property.DefaultValueHandling.HasValue 
				&& property.DefaultValueHandling.Value == DefaultValueHandling.Ignore 
				&& !typeof(string).IsAssignableFrom(property.PropertyType) 
				&& typeof(IEnumerable).IsAssignableFrom(property.PropertyType))
			{
				Predicate<object> shouldSerialize = obj =>
				{
					var collection = property.ValueProvider.GetValue(obj) as ICollection;
					return collection == null || collection.Count != 0;
				};
				property.ShouldSerialize = property.ShouldSerialize == null ? shouldSerialize : (o => property.ShouldSerialize(o) && shouldSerialize(o));
			}

			IPropertyMapping propertyMapping = null;
			if (!this.ConnectionSettings.PropertyMappings.TryGetValue(member, out propertyMapping))
				propertyMapping = ElasticsearchPropertyAttribute.From(member);

			if (propertyMapping == null)
			{
				var jsonIgnoreAttribute = member.GetCustomAttributes(typeof(JsonIgnoreAttribute), true);
				if (jsonIgnoreAttribute.HasAny())
					property.Ignored = true;
				return property;
			}

			if (!propertyMapping.Name.IsNullOrEmpty())
				property.PropertyName = propertyMapping.Name;
			property.Ignored = propertyMapping.Ignore;

			return property;
		}
	}
}
