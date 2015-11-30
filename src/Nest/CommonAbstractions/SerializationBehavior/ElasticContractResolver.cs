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

			if (typeof(IDictionary).IsAssignableFrom(objectType) && !typeof(IIsADictionary).IsAssignableFrom(objectType))
				contract.Converter = new VerbatimDictionaryKeysJsonConverter();
			else if (objectType == typeof(DateTime) || objectType == typeof(DateTime?))
				contract.Converter = new IsoDateTimeConverter();
			else if (!objectType.FullName.StartsWith("Nest.", StringComparison.OrdinalIgnoreCase)) return contract;

			else if (ApplyExactContractJsonAttribute(objectType, contract)) return contract;
			else if (ApplyContractJsonAttribute(objectType, contract)) return contract;

			//TODO these should not be necessary here
			else if (objectType == typeof(MultiGetResponse)) contract.Converter = new MultiGetHitJsonConverter();

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

		private bool ApplyExactContractJsonAttribute(Type objectType, JsonContract contract)
		{
			var attribute = objectType.GetCustomAttributes(typeof(ExactContractJsonConverterAttribute)).FirstOrDefault() as ExactContractJsonConverterAttribute;
			if (attribute?.Converter == null) return false;
			contract.Converter = attribute.Converter;
			return true;
		}
		private bool ApplyContractJsonAttribute(Type objectType, JsonContract contract)
		{
			foreach (var t in this.TypeWithInterfaces(objectType))
			{
				var attribute = t.GetCustomAttributes(typeof(ContractJsonConverterAttribute), true).FirstOrDefault() as ContractJsonConverterAttribute;
				if (attribute?.Converter == null) continue;
				contract.Converter = attribute.Converter;
				return true;
			}
			return false;
		}

		private IEnumerable<Type> TypeWithInterfaces(Type objectType)
		{
			yield return objectType;
			foreach (var i in objectType.GetInterfaces()) yield return i;
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
			defaultProperties = PropertiesOf<IFieldLookup>(type, memberSerialization, defaultProperties, lookup);
			defaultProperties = PropertiesOf<IQueryContainer>(type, memberSerialization, defaultProperties, lookup);
			defaultProperties = PropertiesOf<IRequest>(type, memberSerialization, defaultProperties, lookup);
			defaultProperties = PropertiesOf<IQueryContainer>(type, memberSerialization, defaultProperties, lookup);
			defaultProperties = PropertiesOf<IRandomScoreFunction>(type, memberSerialization, defaultProperties, lookup);
			defaultProperties = PropertiesOf<IHighlight>(type, memberSerialization, defaultProperties, lookup);
			defaultProperties = PropertiesOf<IHighlightField>(type, memberSerialization, defaultProperties, lookup);
			defaultProperties = PropertiesOf<IRescore>(type, memberSerialization, defaultProperties, lookup);
			defaultProperties = PropertiesOf<IRescoreQuery>(type, memberSerialization, defaultProperties, lookup);
			defaultProperties = PropertiesOf<IIndexedGeoShape>(type, memberSerialization, defaultProperties, lookup);
			defaultProperties = PropertiesOf<IAggregationContainer>(type, memberSerialization, defaultProperties, lookup);
			defaultProperties = PropertiesOf<IMetricAggregation>(type, memberSerialization, defaultProperties, lookup);
			defaultProperties = PropertiesOf<IBucketAggregation>(type, memberSerialization, defaultProperties, lookup);
			defaultProperties = PropertiesOf<IPipelineAggregation>(type, memberSerialization, defaultProperties, lookup);
			defaultProperties = PropertiesOf<ISort>(type, memberSerialization, defaultProperties, lookup);
			defaultProperties = PropertiesOf<ISuggestBucket>(type, memberSerialization, defaultProperties, lookup);
			defaultProperties = PropertiesOf<ISuggester>(type, memberSerialization, defaultProperties, lookup);
			defaultProperties = PropertiesOf<IFuzzySuggester>(type, memberSerialization, defaultProperties, lookup);
			defaultProperties = PropertiesOf<IDirectGenerator>(type, memberSerialization, defaultProperties, lookup);
			defaultProperties = PropertiesOf<IAliasAction>(type, memberSerialization, defaultProperties, lookup);
			defaultProperties = PropertiesOf<IBulkOperation>(type, memberSerialization, defaultProperties, lookup);
			defaultProperties = PropertiesOf<IMultiGetOperation>(type, memberSerialization, defaultProperties, lookup);
			defaultProperties = PropertiesOf<IAlias>(type, memberSerialization, defaultProperties, lookup);
			defaultProperties = PropertiesOf<IInnerHitsContainer>(type, memberSerialization, defaultProperties, lookup);
			defaultProperties = PropertiesOf<IInnerHits>(type, memberSerialization, defaultProperties, lookup);
			defaultProperties = PropertiesOf<IBoundingBox>(type, memberSerialization, defaultProperties, lookup);
			defaultProperties = PropertiesOf<IClusterRerouteCommand>(type, memberSerialization, defaultProperties, lookup);
			defaultProperties = PropertiesOf<IMultiTermVectorOperation>(type, memberSerialization, defaultProperties, lookup);
			defaultProperties = PropertiesOf<IRepository>(type, memberSerialization, defaultProperties, lookup);
			defaultProperties = PropertiesOf<IRepositorySettings>(type, memberSerialization, defaultProperties, lookup);
			defaultProperties = PropertiesOf<IScript>(type, memberSerialization, defaultProperties, lookup);
			defaultProperties = PropertiesOf<IScriptField>(type, memberSerialization, defaultProperties, lookup);
			defaultProperties = PropertiesOf<ISourceFilter>(type, memberSerialization, defaultProperties, lookup);
			defaultProperties = PropertiesOf<ILikeDocument>(type, memberSerialization, defaultProperties, lookup);
			defaultProperties = PropertiesOf<INestSerializable>(type, memberSerialization, defaultProperties, lookup);

			defaultProperties = PropertiesOf<IProperty>(type, memberSerialization, defaultProperties, lookup);

			return defaultProperties.GroupBy(p => p.PropertyName).Select(p => p.First()).ToList();
		}

		public IList<JsonProperty> PropertiesOfAllInterfaces(Type t, MemberSerialization memberSerialization)
		{
			return (
				from i in t.GetInterfaces()
				select base.CreateProperties(i, memberSerialization)
				)
				.SelectMany(interfaceProps => interfaceProps)
				.DistinctBy(p => p.PropertyName)
				.ToList();

		}

		public IList<JsonProperty> PropertiesOfAll(Type t, MemberSerialization memberSerialization)
		{
			return base.CreateProperties(t, memberSerialization)
				.Concat(PropertiesOfAllInterfaces(t, memberSerialization))
				.DistinctBy(p => p.PropertyName)
				.ToList();

		}

		private IList<JsonProperty> PropertiesOf<T>(Type type, MemberSerialization memberSerialization, IList<JsonProperty> defaultProperties, ILookup<string, JsonProperty> lookup, bool append = false)
		{
			if (!typeof(T).IsAssignableFrom(type)) return defaultProperties;
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
