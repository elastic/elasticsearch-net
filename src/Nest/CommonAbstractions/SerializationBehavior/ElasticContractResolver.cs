using System;
using System.Collections;
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
	internal class ElasticContractResolver : DefaultContractResolver
	{
		private readonly Lazy<bool> _usingSourceSerializer;

		public static JsonSerializer Empty { get; } = new JsonSerializer();

		/// <summary>
		/// ConnectionSettings can be requested by JsonConverters.
		/// </summary>
		public IConnectionSettingsValues ConnectionSettings { get; }

		public bool UsingSourceSerializer => _usingSourceSerializer.Value;

		/// <summary>
		/// Signals to custom converter that it can get serialization state from one of the converters. Ugly but massive performance gain
		/// </summary>
		internal JsonConverterPiggyBackState PiggyBackState { get; set; }

		public ElasticContractResolver(IConnectionSettingsValues connectionSettings)
		{
			this.ConnectionSettings = connectionSettings;
			_usingSourceSerializer = new Lazy<bool>(() => connectionSettings.RequestResponseSerializer != connectionSettings.SourceSerializer);
		}

		protected bool CanRemoveSourceConverter(JsonConverter converter)
		{
			if (UsingSourceSerializer || converter == null) return false;
			return converter is SourceConverter;
		}

		protected override JsonContract CreateContract(Type objectType)
		{
			// cache contracts per connection settings
			return this.ConnectionSettings.Inferrer.Contracts.GetOrAdd(objectType, o =>
			{
				var contract = base.CreateContract(o);

				if (o == typeof(Error)) contract.Converter = new ErrorJsonConverter();
				else if (o == typeof(ErrorCause)) contract.Converter = new ErrorCauseJsonConverter();
				else if (CanRemoveSourceConverter(contract.Converter)) contract.Converter = null; //rely on defaults
				else if (o.IsGeneric() && o.GetGenericTypeDefinition() == typeof(SuggestDictionary<>))
					contract.Converter =
						(JsonConverter)typeof(SuggestDictionaryConverter<>).CreateGenericInstance(o.GetGenericArguments());

				else if (contract.Converter == null &&
					(typeof(IDictionary).IsAssignableFrom(o) || o.IsGenericDictionary()) && !typeof(IIsADictionary).IsAssignableFrom(o))
				{
					if (!o.TryGetGenericDictionaryArguments(out var genericArguments))
						contract.Converter = new VerbatimDictionaryKeysJsonConverter();
					else
						contract.Converter =
							(JsonConverter)typeof(VerbatimDictionaryKeysJsonConverter<,>).CreateGenericInstance(genericArguments);
				}

				else if (o == typeof(DateTime) || o == typeof(DateTime?))
					contract.Converter = new IsoDateTimeConverter { Culture = CultureInfo.InvariantCulture };
				else if (o == typeof(TimeSpan) || o == typeof(TimeSpan?))
					contract.Converter = new TimeSpanToStringConverter();
				else if (typeof(IEnumerable<QueryContainer>).IsAssignableFrom(o))
					contract.Converter = new QueryContainerCollectionJsonConverter();

				ApplyBuildInSerializersForType(o, contract);

				if (!o.FullName.StartsWith("Nest.", StringComparison.OrdinalIgnoreCase)) return contract;
				if (ApplyExactContractJsonAttribute(o, contract)) return contract;
				if (ApplyContractJsonAttribute(o, contract)) return contract;

				return contract;
			});
		}

		private static Type _readAsType = typeof(ReadAsTypeJsonConverter<>);
		protected override JsonConverter ResolveContractConverter(Type objectType)
		{
			var info = objectType.GetTypeInfo();
			var attribute = info.GetCustomAttribute<ReadAsAttribute>();
			if (attribute == null)
				return base.ResolveContractConverter(objectType);

			var readAsType = attribute.Type;
			var readAsTypeInfo = readAsType.GetTypeInfo();
			if (!readAsTypeInfo.IsGenericType || !readAsTypeInfo.IsGenericTypeDefinition)
				return (JsonConverter) _readAsType.CreateGenericInstance(objectType);

			var targetType = objectType;
			if (info.IsGenericType) targetType = targetType.GetGenericArguments().First();

			var concreteType = readAsType.MakeGenericType(targetType);
			return (JsonConverter) _readAsType.CreateGenericInstance(concreteType);

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
			foreach (var t in TypeWithInterfaces(objectType))
			{
				var attribute = (ContractJsonConverterAttribute)t.GetTypeInfo().GetCustomAttributes(typeof(ContractJsonConverterAttribute), true).FirstOrDefault();
				if (attribute?.Converter == null) continue;
				contract.Converter = attribute.Converter;
				return true;
			}
			return false;
		}

		private static IEnumerable<Type> TypeWithInterfaces(Type objectType)
		{
			yield return objectType;
			foreach (var i in objectType.GetInterfaces()) yield return i;
		}

		private static readonly Assembly ThisAssembly = typeof(ElasticContractResolver).Assembly();

		protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
		{
			var property = base.CreateProperty(member, memberSerialization);

			if (CanRemoveSourceConverter(property.Converter)) property.Converter = null; //rely on defaults

			//we don't have a chance to ignore this in the low level client
			if (member.Name == nameof(IResponse.ApiCall) && typeof(IResponse).IsAssignableFrom(member.DeclaringType))
				property.Ignored = true;

			ApplyShouldSerializer(property);
			ApplyPropertyOverrides(member, property);
			ApplyBuildInSerializers(member, property);
			return property;
		}


		protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
		{
			// Only serialize explicitly implemented IProperty properties on attribute types
			if (typeof(PropertyBase).IsAssignableFrom(type) || typeof(ElasticsearchPropertyAttributeBase).IsAssignableFrom(type))
				return PropertiesOfInterface<IProperty>(type, memberSerialization);

			// Descriptors implement properties explicitly, these are not picked up by default
			if (typeof(IDescriptor).IsAssignableFrom(type))
				return PropertiesOfAll(type, memberSerialization);

			var nativeType = type.Assembly() == ThisAssembly;
			if (nativeType)
				return base.CreateProperties(type, memberSerialization);

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
			if (!(prop.ValueProvider.GetValue(o) is QueryContainer q)) return false;
			return q.IsWritable;
		}

		protected static bool ShouldSerializeQueryContainers(object o, JsonProperty prop)
		{
			if (o == null) return false;
			var q = prop.ValueProvider.GetValue(o) as IEnumerable<QueryContainer>;
			return (q.AsInstanceOrToListOrNull()?.Any(qq=>qq != null && qq.IsWritable)).GetValueOrDefault(false);
		}
		protected bool ShouldSerializeRouting(object o, JsonProperty prop)
		{
			if (o == null) return false;
			var q = prop.ValueProvider.GetValue(o) as Routing;
			if (q == null) return false;
			//not ideal to resolve twice but for now the only way not to send routing: null
			var resolved = this.ConnectionSettings.Inferrer.Resolve(q);
			return !resolved.IsNullOrEmpty();
		}

		private static readonly StringEnumConverter StringEnumConverter = new StringEnumConverter();
		private static readonly StringTimeSpanConverter StringTimeSpanConverter = new StringTimeSpanConverter();
		private static readonly MachineLearningDateTimeConverter MachineLearningDateTimeConverter = new MachineLearningDateTimeConverter();
		private static readonly Type[] StringSignalTypes = {typeof(KeywordAttribute), typeof(TextAttribute)};
		private static void ApplyBuildInSerializers(MemberInfo member, JsonProperty property)
		{
			var attributes = member.GetCustomAttributes().ToList();
			var stringy = attributes.Any(a => StringSignalTypes.Contains(a.GetType()));
			if (attributes.OfType<StringEnumAttribute>().Any() || (property.PropertyType.IsEnumType() && stringy))
				property.Converter = StringEnumConverter;

			if ((property.PropertyType == typeof(TimeSpan) || property.PropertyType == typeof(TimeSpan?))
			    && (attributes.OfType<StringTimeSpanAttribute>().Any() || stringy))
				property.Converter = StringTimeSpanConverter;

			if ((property.PropertyType == typeof(DateTime) || property.PropertyType == typeof(DateTime?))
			    && (attributes.OfType<MachineLearningDateTimeAttribute>().Any()))
				property.Converter = MachineLearningDateTimeConverter;
		}

		private static void ApplyBuildInSerializersForType(Type type, JsonContract contract)
		{
			if (type.GetTypeInfo().GetCustomAttribute<StringEnumAttribute>() != null)
				contract.Converter = StringEnumConverter;
		}

		/// <summary> Renames/Ignores a property based on the connection settings mapping or custom attributes for the property </summary>
		private void ApplyPropertyOverrides(MemberInfo member, JsonProperty property)
		{
			if (!this.ConnectionSettings.PropertyMappings.TryGetValue(member, out var propertyMapping))
				propertyMapping = ElasticsearchPropertyAttributeBase.From(member);

			var serializerMapping = this.ConnectionSettings.PropertyMappingProvider?.CreatePropertyMapping(member);

			var nameOverride = propertyMapping?.Name ?? serializerMapping?.Name;
			if (!nameOverride.IsNullOrEmpty()) property.PropertyName = nameOverride;

			var overrideIgnore = propertyMapping?.Ignore ?? serializerMapping?.Ignore;
			if (overrideIgnore.HasValue)
				property.Ignored = overrideIgnore.Value;
		}

		private void ApplyShouldSerializer(JsonProperty property)
		{
			if (property.PropertyType == typeof(QueryContainer))
				property.ShouldSerialize = o => ShouldSerializeQueryContainer(o, property);
			else if (property.PropertyType == typeof(IEnumerable<QueryContainer>))
				property.ShouldSerialize = o => ShouldSerializeQueryContainers(o, property);
			else if (property.PropertyType == typeof(Routing))
				property.ShouldSerialize = o => ShouldSerializeRouting(o, property);

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
		}
	}
}
