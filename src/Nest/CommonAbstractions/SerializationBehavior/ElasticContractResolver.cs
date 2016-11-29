using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using Elasticsearch.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace Nest
{
	public class ElasticContractResolver : DefaultContractResolver
	{
		private readonly IList<Func<Type, JsonConverter>> _contractConverters;
		public static JsonSerializer Empty { get; } = new JsonSerializer();

		/// <summary>
		/// ConnectionSettings can be requested by JsonConverter's.
		/// </summary>
		public IConnectionSettingsValues ConnectionSettings { get; private set; }

		/// <summary>
		/// Signals to custom converter that it can get serialization state from one of the converters. Ugly but massive performance gain
		/// </summary>
		internal JsonConverterPiggyBackState PiggyBackState { get; set; }

		public ElasticContractResolver(IConnectionSettingsValues connectionSettings, IList<Func<Type, JsonConverter>> contractConverters)
		{
			this._contractConverters = contractConverters;
			this.ConnectionSettings = connectionSettings;
		}

		protected override JsonContract CreateContract(Type objectType)
		{
			// cache contracts per connection settings
			return this.ConnectionSettings.Inferrer.Contracts.GetOrAdd(objectType, o =>
			{
				var contract = base.CreateContract(o);

				if (typeof(IDictionary).IsAssignableFrom(o) && !typeof(IIsADictionary).IsAssignableFrom(o))
					contract.Converter = new VerbatimDictionaryKeysJsonConverter();
				if (typeof(IEnumerable<QueryContainer>).IsAssignableFrom(o))
					contract.Converter = new QueryContainerCollectionJsonConverter();
				else if (o == typeof(ServerError))
					contract.Converter = new ServerErrorJsonConverter();
				else if (o == typeof(DateTime) ||
						 o == typeof(DateTime?))
					contract.Converter = new IsoDateTimeConverter { Culture = CultureInfo.InvariantCulture };
				else if (o == typeof(TimeSpan) ||
						 o == typeof(TimeSpan?))
					contract.Converter = new TimeSpanConverter();

				if (this._contractConverters.HasAny())
				{
					foreach (var c in this._contractConverters)
					{
						var converter = c(o);
						if (converter == null)
							continue;
						contract.Converter = converter;
						break;
					}
				}

				if (!o.FullName.StartsWith("Nest.", StringComparison.OrdinalIgnoreCase)) return contract;
				if (ApplyExactContractJsonAttribute(o, contract)) return contract;
				if (ApplyContractJsonAttribute(o, contract)) return contract;

				return contract;
			});
		}

		private bool ApplyExactContractJsonAttribute(Type objectType, JsonContract contract)
		{

			var attribute = objectType.GetTypeInfo().GetCustomAttributes(typeof(ExactContractJsonConverterAttribute)).FirstOrDefault() as ExactContractJsonConverterAttribute;
			if (attribute?.Converter == null) return false;
			contract.Converter = attribute.Converter;
			return true;
		}

		private bool ApplyContractJsonAttribute(Type objectType, JsonContract contract)
		{
			foreach (var t in this.TypeWithInterfaces(objectType))
			{
				var attribute = t.GetTypeInfo().GetCustomAttributes(typeof(ContractJsonConverterAttribute), true).FirstOrDefault() as ContractJsonConverterAttribute;
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
			// Only serialize explicitly implemented IProperty properties on attribute types
			if (typeof(ElasticsearchPropertyAttributeBase).IsAssignableFrom(type))
				return PropertiesOfInterface<IProperty>(type, memberSerialization);

			// Descriptors implement properties explicitly, these are not picked up by default
			if (typeof(IDescriptor).IsAssignableFrom(type))
				return PropertiesOfAll(type, memberSerialization);

			return base.CreateProperties(type, memberSerialization);
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

		public IList<JsonProperty> PropertiesOfInterface<TInterface>(Type t, MemberSerialization memberSerialization)
			where TInterface : class
		{
			return (
				from i in t.GetInterfaces().Where(i => typeof(TInterface).IsAssignableFrom(i))
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

		protected override string ResolvePropertyName(string fieldName)
		{
			if (this.ConnectionSettings.DefaultFieldNameInferrer != null)
				return this.ConnectionSettings.DefaultFieldNameInferrer(fieldName);

			return fieldName.ToCamelCase();
		}

		protected static bool ShouldSerializeQueryContainer(object o, JsonProperty prop)
		{
			if (o == null) return false;
			var q = prop.ValueProvider.GetValue(o) as QueryContainer;
			if (q == null) return false;
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

		protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
		{
			var property = base.CreateProperty(member, memberSerialization);
			if (property.PropertyType == typeof(QueryContainer))
				property.ShouldSerialize = o => ElasticContractResolver.ShouldSerializeQueryContainer(o, property);
			else if (property.PropertyType == typeof(IEnumerable<QueryContainer>))
				property.ShouldSerialize = o => ElasticContractResolver.ShouldSerializeQueryContainers(o, property);

			// Skip serialization of empty collections that have DefaultValueHandling set to Ignore.
			else if (property.DefaultValueHandling.HasValue
				&& property.DefaultValueHandling.Value == DefaultValueHandling.Ignore
				&& !typeof(string).IsAssignableFrom(property.PropertyType)
				&& typeof(IEnumerable).IsAssignableFrom(property.PropertyType))
			{
				Predicate<object> shouldSerialize = obj =>
				{
					var collection = property.ValueProvider.GetValue(obj) as ICollection;
					if (collection == null)
					{
						return true;
					}
					return collection.Count != 0 && collection.Cast<object>().Any(item => item != null);
				};
				property.ShouldSerialize = property.ShouldSerialize == null ? shouldSerialize : (o => property.ShouldSerialize(o) && shouldSerialize(o));
			}

			IPropertyMapping propertyMapping = null;
			if (!this.ConnectionSettings.PropertyMappings.TryGetValue(member, out propertyMapping))
				propertyMapping = ElasticsearchPropertyAttributeBase.From(member);

			var serializerMapping = this.ConnectionSettings.Serializer?.CreatePropertyMapping(member);

			var nameOverride = propertyMapping?.Name ?? serializerMapping?.Name;
			if (!nameOverride.IsNullOrEmpty()) property.PropertyName = nameOverride;

			var overrideIgnore = propertyMapping?.Ignore ?? serializerMapping?.Ignore;
			if (overrideIgnore.HasValue)
				property.Ignored = overrideIgnore.Value;

			return property;
		}
	}
}
