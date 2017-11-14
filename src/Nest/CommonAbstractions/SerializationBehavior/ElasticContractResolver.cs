using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Threading;
using Elasticsearch.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace Nest
{
	internal class ElasticContractResolver : DefaultContractResolver
	{
		public static JsonSerializer Empty { get; } = new JsonSerializer();

		/// <summary>
		/// ConnectionSettings can be requested by JsonConverters.
		/// </summary>
		public IConnectionSettingsValues ConnectionSettings { get; }

		/// <summary>
		/// Signals to custom converter that it can get serialization state from one of the converters. Ugly but massive performance gain
		/// </summary>
		internal JsonConverterPiggyBackState PiggyBackState { get; set; }

		public ElasticContractResolver(IConnectionSettingsValues connectionSettings)
		{
			this.ConnectionSettings = connectionSettings;
		}

		private static bool TypeWeAreInterestedIn(Type o)
		{
			if ((typeof(IDictionary).IsAssignableFrom(o) || o.IsGenericDictionary()) && !typeof(IIsADictionary).IsAssignableFrom(o))
				return true;
			if (typeof(IEnumerable<QueryContainer>).IsAssignableFrom(o)) return true;
			if (o == typeof(ServerError)) return true;
			if (o == typeof(DateTime) || o == typeof(DateTime?)) return true;
			if (o == typeof(TimeSpan) || o == typeof(TimeSpan?)) return true;
			return o.IsNestType();
		}

		protected override JsonContract CreateContract(Type objectType)
		{
			if (!TypeWeAreInterestedIn(objectType)) return base.CreateContract(objectType);

			// cache contracts per connection settings
			return this.ConnectionSettings.Inferrer.Contracts.GetOrAdd(objectType, o =>
			{
				var contract = base.CreateContract(o);

				if ((typeof(IDictionary).IsAssignableFrom(o) || o.IsGenericDictionary()) && !typeof(IIsADictionary).IsAssignableFrom(o))
				{
					if (!o.TryGetGenericDictionaryArguments(out var genericArguments))
						contract.Converter = new VerbatimDictionaryKeysJsonConverter();
					else
						contract.Converter =
							(JsonConverter)typeof(VerbatimDictionaryKeysJsonConverter<,>).CreateGenericInstance(genericArguments);
				}
				else if (typeof(IEnumerable<QueryContainer>).IsAssignableFrom(o))
					contract.Converter = new QueryContainerCollectionJsonConverter();
				else if (o == typeof(ServerError))
					contract.Converter = new ServerErrorJsonConverter();
				else if (o == typeof(DateTime) || o == typeof(DateTime?))
					contract.Converter = new IsoDateTimeConverter { Culture = CultureInfo.InvariantCulture };
				else if (o == typeof(TimeSpan) || o == typeof(TimeSpan?))
					contract.Converter = new TimeSpanConverter();

				if (!o.IsNestType()) return contract;
				if (ApplyExactContractJsonAttribute(o, contract)) return contract;
				if (ApplyContractJsonAttribute(o, contract)) return contract;

				return contract;
			});
		}

		private static bool ApplyExactContractJsonAttribute(Type objectType, JsonContract contract)
		{
			var attribute = (ExactContractJsonConverterAttribute)objectType.GetTypeInfo().GetCustomAttributes(typeof(ExactContractJsonConverterAttribute)).FirstOrDefault();
			if (attribute?.Converter == null) return false;
			contract.Converter = attribute.Converter;
			return true;
		}

		private static bool ApplyContractJsonAttribute(Type objectType, JsonContract contract)
		{
			foreach (var t in objectType.AllInterfacesAndSelf())
			{
				var attribute = (ContractJsonConverterAttribute)t.GetTypeInfo().GetCustomAttributes(typeof(ContractJsonConverterAttribute), true).FirstOrDefault();
				if (attribute?.Converter == null) continue;
				contract.Converter = attribute.Converter;
				return true;
			}
			return false;
		}

		protected override List<MemberInfo> GetSerializableMembers(Type type)
		{
			var isNestType = type.IsNestType();
			var members = !isNestType
				? base.GetSerializableMembers(type)
				: type.GetSerializeMembers().Select(m => m.MemberInfo).ToList();;

			return members;
		}

		protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
		{
			var isNestType = type.IsNestType();
			var properties = !isNestType
				? base.CreateProperties(type, memberSerialization)
				: PropertiesOfAll(type, memberSerialization);
			return properties;
		}

		private static ConcurrentDictionary<Type, List<JsonProperty>> JsonPropertiesForType { get; }
			= new ConcurrentDictionary<Type, List<JsonProperty>>();

		public IList<JsonProperty> PropertiesOfAll(Type type, MemberSerialization memberSerialization)
		{
			if (JsonPropertiesForType.TryGetValue(type, out var list)) return list;
			return JsonPropertiesForType.GetOrAdd(type, (t) =>
			{
				return type.GetSerializeMembers()
					.Select(j => this.CreateProperty(j.MemberInfo, memberSerialization))
					.ToList();
			});
		}

		protected override string ResolvePropertyName(string fieldName) =>
			this.ConnectionSettings.DefaultFieldNameInferrer != null
				? this.ConnectionSettings.DefaultFieldNameInferrer(fieldName)
				: fieldName.ToCamelCase();

		protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
		{
			var property = base.CreateProperty(member, memberSerialization);
			if (property.PropertyType == typeof(QueryContainer))
				property.ShouldSerialize = o => ShouldSerializeQueryContainer(o, property);
			else if (property.PropertyType == typeof(IEnumerable<QueryContainer>))
				property.ShouldSerialize = o => ShouldSerializeQueryContainers(o, property);

			// Skip serialization of empty collections that have DefaultValueHandling set to Ignore.
			else if (property.DefaultValueHandling.HasValue
				&& property.DefaultValueHandling.Value == DefaultValueHandling.Ignore
				&& !typeof(string).IsAssignableFrom(property.PropertyType)
				&& typeof(IEnumerable).IsAssignableFrom(property.PropertyType))
			{
				bool ShouldSerialize(object obj)
				{
					if (!(property.ValueProvider.GetValue(obj) is ICollection collection))
						return true;
					return collection.Count != 0 && collection.Cast<object>().Any(item => item != null);
				}

				property.ShouldSerialize = property.ShouldSerialize == null
					? (Predicate<object>) ShouldSerialize
					: (o => property.ShouldSerialize(o) && ShouldSerialize(o));
			}

			if (!this.ConnectionSettings.PropertyMappings.TryGetValue(member, out var propertyMapping))
				propertyMapping = ElasticsearchPropertyAttributeBase.From(member);

			var serializerMapping = this.ConnectionSettings.PropertyMappingProvider?.CreatePropertyMapping(member);

			var nameOverride = propertyMapping?.Name ?? serializerMapping?.Name;
			if (!nameOverride.IsNullOrEmpty()) property.PropertyName = nameOverride;

			var overrideIgnore = propertyMapping?.Ignore ?? serializerMapping?.Ignore;
			if (overrideIgnore.HasValue)
				property.Ignored = overrideIgnore.Value;

			return property;
		}

		protected static bool ShouldSerializeQueryContainer(object o, JsonProperty prop)
		{
			if (o == null) return false;
			if (!(prop.ValueProvider.GetValue(o) is QueryContainer q)) return false;
			if (q.IsWritable) return true;
			var nq = q as NoMatchQueryContainer;
			return nq?.Shortcut != null;
		}

		protected static bool ShouldSerializeQueryContainers(object o, JsonProperty prop)
		{
			if (o == null) return false;
			var q = prop.ValueProvider.GetValue(o) as IEnumerable<QueryContainer>;
			return (q.AsInstanceOrToListOrNull()?.Any(qq=>qq != null && qq.IsWritable)).GetValueOrDefault(false);
		}

	}
}
