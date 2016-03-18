using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection;
using Elasticsearch.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Nest
{
	//TODO rethink IHIT<T> deserialization where T is covariant
	internal class DefaultHitJsonConverter : JsonConverter
	{
		private static readonly ConcurrentDictionary<Type, JsonConverter> _hitTypes = new ConcurrentDictionary<Type, JsonConverter>();

		public override bool CanWrite => false;
		public override bool CanRead => true;
		public override bool CanConvert(Type objectType) => true;

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			JsonConverter converter;
			if (_hitTypes.TryGetValue(objectType, out converter))
				return converter.ReadJson(reader, objectType, existingValue, serializer);

			var genericType = typeof(ConcreteTypeConverter<>);
			var closedType = genericType.MakeGenericType(objectType.GetGenericArguments()[0]);
			converter = (JsonConverter)closedType.CreateInstance();
			_hitTypes.TryAdd(objectType, converter);
			return converter.ReadJson(reader, objectType, existingValue, serializer);
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			throw new NotSupportedException();
		}
	}

	internal class ConcreteTypeConverter<T> : JsonConverter where T : class
	{
		internal readonly Type _baseType;
		internal readonly Func<dynamic, Hit<dynamic>, Type> _concreteTypeSelector;

		public override bool CanWrite => false;
		public override bool CanRead => true;
		public override bool CanConvert(Type objectType) => typeof(IHit<object>).IsAssignableFrom(objectType);

		public ConcreteTypeConverter() {}

		public ConcreteTypeConverter(Func<dynamic, Hit<dynamic>, Type> concreteTypeSelector)
		{
			concreteTypeSelector.ThrowIfNull(nameof(concreteTypeSelector));

			this._baseType = typeof(T);
			this._concreteTypeSelector = concreteTypeSelector;
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			var realConverter = serializer.GetStatefulConverter<ConcreteTypeConverter<T>>();
			if (realConverter != null)
				return ConcreteTypeConverter.GetUsingConcreteTypeConverter<T>(reader, serializer, realConverter);

			var instance = (Hit<T>)(typeof(Hit<T>).CreateInstance());
			serializer.Populate(reader, instance);
			instance.Fields = new FieldValues(serializer.GetConnectionSettings().Inferrer, instance.Fields);
			return instance;
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			throw new NotSupportedException();
		}
	}

	internal static class ConcreteTypeConverter
	{
		internal static object GetUsingConcreteTypeConverter<T>(
			JsonReader reader, JsonSerializer serializer, ConcreteTypeConverter<T> realConcreteConverter)
			where T : class
		{
			var jObject = CreateIntermediateJObject(reader);
			var concreteType = GetConcreteTypeUsingSelector(serializer, realConcreteConverter, jObject);
			var hit = GetHitTypeInstance(concreteType);
			PopulateHit(serializer, jObject.CreateReader(), hit);
			return hit;
		}

		private static void PopulateHit(JsonSerializer serializer, JsonReader reader, object hit) {
			serializer.Populate(reader, hit);
		}

		private static JObject CreateIntermediateJObject(JsonReader reader)
		{
			JObject jObject = JObject.Load(reader);
			return jObject;
		}

		private static object GetHitTypeInstance(Type concreteType)
		{
			var hitType = typeof (Hit<>).MakeGenericType(concreteType);
			return hitType.CreateInstance();
		}

		internal static Type GetConcreteTypeUsingSelector<T>(
			JsonSerializer serializer,
			ConcreteTypeConverter<T> realConcreteConverter,
			JObject jObject)
			where T: class
		{
			var settings = serializer.GetConnectionSettings();
			var baseType = realConcreteConverter._baseType;
			var selector = realConcreteConverter._concreteTypeSelector;

			//Hit<dynamic> hitDynamic = new Hit<dynamic>();
			dynamic d = jObject;
			var fields = jObject["fields"];
			var fieldsDictionary = fields?.ToObject<IDictionary<string, object>>();
			var fieldValues = new FieldValues(settings.Inferrer, fieldsDictionary);
			var hitDynamic = new Hit<dynamic>();
			//favor manual mapping over doing Populate twice.
			hitDynamic.Fields = fieldValues;
			hitDynamic.Source = d._source;
			hitDynamic.Index = d._index;
			hitDynamic.Score = d._score != null ? (double)d._score : 0;
			hitDynamic.Type = d._type;
			hitDynamic.Version = d._version;
			hitDynamic.Id = d._id;
			hitDynamic.Parent = d._parent;
			hitDynamic.Routing = d._routing;
			hitDynamic.Sorts = d.sort;
			hitDynamic._Highlight = d.highlight is Dictionary<string, List<string>> ? d.highlight : null;
			hitDynamic.Explanation = d._explanation is Explanation ? d._explanation : null;
			object o = d._source ?? DynamicResponse.Create(fieldsDictionary) ?? new object {};
			var concreteType = selector(o, hitDynamic);
			return concreteType;
		}
	}
}
