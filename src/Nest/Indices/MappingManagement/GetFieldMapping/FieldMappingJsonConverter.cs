using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Nest
{
	internal class FieldMappingJsonConverter : JsonConverter
	{
		private readonly VerbatimDictionaryKeysJsonConverter<Field, IFieldMapping> _dictionaryConverter =
			new VerbatimDictionaryKeysJsonConverter<Field, IFieldMapping>();

		private readonly PropertyJsonConverter _propertyJsonConverter = new PropertyJsonConverter();

		public override bool CanConvert(Type objectType) => objectType == typeof(IDictionary<string, IFieldMapping>);

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			_dictionaryConverter.WriteJson(writer, value, serializer);
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			var r = new Dictionary<Field, IFieldMapping>();

			var o = JObject.Load(reader);

			foreach (var p in o.Properties())
			{
				var name = p.Name;
				var po = p.First as JObject;
				if (po == null)
					continue;

				var mapping = _propertyJsonConverter.ReadJson(po.CreateReader(), objectType, existingValue, serializer) as IFieldMapping;
				if (mapping == null)
				{
					if (name == "_all") mapping = po.ToObject<AllField>();
					if (name == "_source") mapping = po.ToObject<SourceField>();
					if (name == "_routing") mapping = po.ToObject<RoutingField>();
					if (name == "_index") mapping = po.ToObject<IndexField>();
					if (name == "_size") mapping = po.ToObject<SizeField>();
					//TODO _field_names does not seem to have a special mapping (just returns like _uid) needs CONFIRMATION
				}
				if (mapping == null) continue;

				if (mapping is IProperty esType) esType.Name = name;
				r.Add(name, mapping);

			}
			var settings = serializer.GetConnectionSettings();
			return new ResolvableDictionaryProxy<Field, IFieldMapping>(settings, r);
		}
	}
}
