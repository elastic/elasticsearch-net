using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Nest.Resolvers;

namespace Nest
{
	internal class ConcreteTypeConverter : JsonConverter
	{
		internal readonly Type _baseType;
		internal readonly Func<dynamic, Hit<dynamic>, Type> _concreteTypeSelector;
		internal readonly IEnumerable<string> _partialFields;

		public override bool CanWrite { get { return false; } }
		public override bool CanRead { get { return true; } }

		public ConcreteTypeConverter() {}

		public ConcreteTypeConverter(Type baseType, Func<dynamic, Hit<dynamic>, Type> concreteTypeSelector)
			: this(baseType, concreteTypeSelector, new string[0]) { }

		public ConcreteTypeConverter(Type baseType, Func<dynamic, Hit<dynamic>, Type> concreteTypeSelector, IEnumerable<string> partialFields)
		{
			concreteTypeSelector.ThrowIfNull("concreteTypeSelector");

			this._baseType = baseType;
			this._concreteTypeSelector = concreteTypeSelector;
			this._partialFields = partialFields;
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			var realConcreteConverter = ((ElasticResolver) serializer.ContractResolver).ConcreteTypeConverter;

			// Load JObject from stream
			JObject jObject = JObject.Load(reader);
			Type concreteType;
			if (realConcreteConverter != null)
				concreteType = GetConcreteTypeUsingSelector(reader, realConcreteConverter, jObject);
			else
				concreteType = objectType.GetGenericArguments()[0];
			var hitType = typeof(Hit<>).MakeGenericType(concreteType);
			var hit = Activator.CreateInstance(hitType);

			serializer.Populate(jObject.CreateReader(), hit);

			AppendPartialFields(serializer, realConcreteConverter, concreteType, hit, jObject);

			return hit;
		}

		private static void AppendPartialFields(JsonSerializer serializer, 
			ConcreteTypeConverter realConcreteConverter,
		    Type concreteType, dynamic hit, JObject jObject)
		{
			if (realConcreteConverter == null)
				return;
			dynamic d = jObject;
			var partialFields = realConcreteConverter._partialFields;
			if (partialFields.Any())
			{
				var itemType = typeof (CovariantItem<>).MakeGenericType(concreteType);
				var listType = typeof (List<>).MakeGenericType(itemType);
				var dictType = typeof (CovariantDictionary<>).MakeGenericType(concreteType);

				dynamic items = Activator.CreateInstance(listType);
				foreach (var pf in partialFields)
				{
					dynamic partial = Activator.CreateInstance(concreteType);

					serializer.Populate(d.fields[pf].CreateReader(), partial);

					dynamic dictItem = Activator.CreateInstance(itemType);
					dictItem.Key = pf;
					dictItem.Value = partial;
					items.Add(dictItem);
				}

				dynamic dict = Activator.CreateInstance(dictType);
				dict.Items = items;
				hit.PartialFields = dict;
			}
		}

		private static dynamic GetConcreteTypeUsingSelector(
			JsonReader reader, 
			ConcreteTypeConverter realConcreteConverter,
			JObject jObject)
		{
			var baseType = realConcreteConverter._baseType;
			var selector = realConcreteConverter._concreteTypeSelector;

			Hit<dynamic> hitDynamic = new Hit<dynamic>();
			dynamic d = jObject;

			//favor manual mapping over doing Populate twice.
			hitDynamic.Fields = d.fields;
			hitDynamic.Source = d._source;
			hitDynamic.Index = d._index;
			hitDynamic.Score = (d._score is double) ? d._score : default(double);
			hitDynamic.Type = d._type;
			hitDynamic.Version = d._version;
			hitDynamic.Id = d._id;
			hitDynamic.Sorts = d.sort;
			hitDynamic.Highlight = d.highlight is Dictionary<string, List<string>> ? d.highlight : null;
			hitDynamic.Explanation = d._explanation is Explanation ? d._explanation : null;

			var concreteType = selector(hitDynamic.Source, hitDynamic);
			return concreteType;
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
}