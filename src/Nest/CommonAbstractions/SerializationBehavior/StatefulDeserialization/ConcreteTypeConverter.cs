using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Elasticsearch.Net;
using Nest.Domain;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Nest.Resolvers;

namespace Nest
{
	//TODO rethink IHIT<T> deserialization where T is covariant
	internal class DefaultHitJsonConverter : JsonConverter
	{
		private static readonly ConcurrentDictionary<Type, JsonConverter> _hitTypes = new ConcurrentDictionary<Type, JsonConverter>();

		public override bool CanWrite { get { return false; } }
		public override bool CanRead { get { return true; } }


		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			throw new NotImplementedException();
		}

		public override bool CanConvert(Type objectType)
		{
			return true;
		}

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

	}

	internal class FieldsSetter
	{
		//This method is used through reflection (cached though)
		// do not remove
		private static void SetFields<TFieldsType>(
			Hit<TFieldsType> hit, FieldSelection<TFieldsType> fieldSelection)
			where TFieldsType : class
		{
			hit.Fields = fieldSelection;
		}
	}

	internal class ConcreteTypeConverter<T> : JsonConverter where T : class
	{
		internal readonly Type _baseType;
		internal readonly Func<dynamic, Hit<dynamic>, Type> _concreteTypeSelector;

		public override bool CanWrite { get { return false; } }
		public override bool CanRead { get { return true; } }

		public ConcreteTypeConverter() {}


		public ConcreteTypeConverter(Func<dynamic, Hit<dynamic>, Type> concreteTypeSelector)
		{
			concreteTypeSelector.ThrowIfNull("concreteTypeSelector");

			this._baseType = typeof(T);
			this._concreteTypeSelector = concreteTypeSelector;
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			var elasticContractResolver = serializer.ContractResolver as SettingsContractResolver;
			if (elasticContractResolver != null && elasticContractResolver.PiggyBackState != null
				&& elasticContractResolver.PiggyBackState.ActualJsonConverter != null)
			{
				var realConcreteConverter = elasticContractResolver.PiggyBackState.ActualJsonConverter as ConcreteTypeConverter<T>;
				if (realConcreteConverter != null)
					return ConcreteTypeConverter.GetUsingConcreteTypeConverter<T>(reader, serializer, realConcreteConverter);
			}

			var instance = (Hit<T>)(typeof(Hit<T>).CreateInstance());
			serializer.Populate(reader, instance);
			instance.Fields = new FieldSelection<T>(elasticContractResolver.ConnectionSettings, instance._fields);
			return instance;
		}


	
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			throw new NotImplementedException();
		}

		public override bool CanConvert(Type objectType)
		{
			return typeof(IHit<object>).IsAssignableFrom(objectType);
		}
	}

	internal static class ConcreteTypeConverter 
	{
		internal static ConcurrentDictionary<Type, Action<object, object>> 
			FieldDelegates = new ConcurrentDictionary<Type, Action<object, object>>();

		internal static ConcurrentDictionary<Type, Type> TypeToFieldTypes = 
			new ConcurrentDictionary<Type, Type>();

		internal static MethodInfo MakeDelegateMethodInfo = 
			typeof(FieldsSetter).GetMethod("SetFields", BindingFlags.Static | BindingFlags.NonPublic);

		internal static object GetUsingConcreteTypeConverter<T>(
			JsonReader reader, JsonSerializer serializer, ConcreteTypeConverter<T> realConcreteConverter)
			where T : class
		{
			var jObject = CreateIntermediateJObject(reader);
			object fieldSelection;
			var concreteType = GetConcreteTypeUsingSelector(serializer, realConcreteConverter, jObject, out fieldSelection);
			var hit = GetHitTypeInstance(concreteType);
			PopulateHit(serializer, jObject.CreateReader(), hit);

			Action<object, object> cachedLookup;
			if (ConcreteTypeConverter.FieldDelegates.TryGetValue(concreteType, out cachedLookup))
			{
				cachedLookup(hit, fieldSelection);
				return hit;
			}
			
			var generic = ConcreteTypeConverter.MakeDelegateMethodInfo.MakeGenericMethod(concreteType);
			cachedLookup = (h, f) => generic.Invoke(null, new[] { h, f });
			cachedLookup(hit, fieldSelection);
			ConcreteTypeConverter.FieldDelegates.TryAdd(concreteType, cachedLookup);
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
			JObject jObject, out object selection)
			where T: class
		{
			var elasticContractResolver = serializer.ContractResolver as SettingsContractResolver;
			var baseType = realConcreteConverter._baseType;
			var selector = realConcreteConverter._concreteTypeSelector;

			//Hit<dynamic> hitDynamic = new Hit<dynamic>();
			dynamic d = jObject;
			var fields = jObject["fields"];
			var fieldSelectionData = fields != null ? fields.ToObject<IDictionary<string, object>>() : null;
			var sel = new FieldSelection<T>(elasticContractResolver.ConnectionSettings, fieldSelectionData);
			var hitDynamic = new Hit<dynamic>();
			//favor manual mapping over doing Populate twice.
			hitDynamic._fields = fieldSelectionData;
			hitDynamic.Fields = sel;
			hitDynamic.Source = d._source;
			hitDynamic.Index = d._index;
			hitDynamic._score = (d._score is double) ? d._score : default(double);
			hitDynamic.Type = d._type;
			hitDynamic.Version = d._version;
			hitDynamic.Id = d._id;
			hitDynamic.Sorts = d.sort;
			hitDynamic._Highlight = d.highlight is Dictionary<string, List<string>> ? d.highlight : null;
			hitDynamic.Explanation = d._explanation is Explanation ? d._explanation : null;
			object o = d._source ?? DynamicResponse.Create(fieldSelectionData) ?? new object {};
			var concreteType = selector(o, hitDynamic);
			
			Type fieldSelectionType;
			if (!ConcreteTypeConverter.TypeToFieldTypes.TryGetValue(concreteType, out fieldSelectionType))
			{
				fieldSelectionType = typeof(FieldSelection<>).MakeGenericType(concreteType);
				ConcreteTypeConverter.TypeToFieldTypes.TryAdd(concreteType, fieldSelectionType);
			}
			selection = fieldSelectionType.CreateInstance(elasticContractResolver.ConnectionSettings, fieldSelectionData);
			return concreteType;
		}
	}
}