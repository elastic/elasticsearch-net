using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Nest
{
	internal class FieldMappingJsonConverter : JsonConverter
	{
		private readonly VerbatimDictionaryKeysJsonConverter _dictionaryConverter =
		new VerbatimDictionaryKeysJsonConverter();

		private readonly PropertyJsonConverter _elasticTypeConverter = new PropertyJsonConverter();

		public override bool CanConvert(Type objectType) => objectType == typeof(IDictionary<string, IFieldMapping>);

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			_dictionaryConverter.WriteJson(writer, value, serializer);
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			var r = new Dictionary<string, IFieldMapping>();

			JObject o = JObject.Load(reader);

			foreach (var p in o.Properties())
			{
				var name = p.Name;
				var po = p.First as JObject;
				if (po == null)
					continue;

				var mapping = _elasticTypeConverter.ReadJson(po.CreateReader(), objectType, existingValue, serializer)
				as IFieldMapping;
				if (mapping == null)
				{
					if (name == "_all") mapping = po.ToObject<AllField>();
					if (name == "_source") mapping = po.ToObject<SourceField>();
					if (name == "_routing") mapping = po.ToObject<RoutingField>();
					if (name == "_index") mapping = po.ToObject<IndexField>();
					if (name == "_size") mapping = po.ToObject<SizeField>();
#pragma warning disable 618
					if (name == "_timestamp") mapping = po.ToObject<TimestampField>();
					if (name == "_ttl") mapping = po.ToObject<TtlField>();
#pragma warning restore 618
					if (name == "_parent") mapping = po.ToObject<ParentField>();
					//TODO _field_names does not seem to have a special mapping (just returns like _uid) needs CONFIRMATION
				}
				if (mapping == null) continue;

				var esType = mapping as IProperty;
				if (esType != null)
					esType.Name = name;

				r.Add(name, mapping);

			}
			return r;
		}
	}
}
