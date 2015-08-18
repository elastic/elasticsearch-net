using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace Nest.Resolvers.Converters
{
	public class FieldMappingJsonConverter : JsonConverter
	{
		public override bool CanConvert(Type objectType) => objectType == typeof(IDictionary<string, IFieldMapping>);

		private readonly DictionaryKeysAreNotFieldNamesJsonConverter _dictionaryConverter =
		new DictionaryKeysAreNotFieldNamesJsonConverter();

		private readonly ElasticTypeJsonConverter _elasticTypeConverter = new ElasticTypeJsonConverter();

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
					if (name == "_all") mapping = po.ToObject<AllFieldMapping>();
					if (name == "_id") mapping = po.ToObject<IdFieldMapping>();
					if (name == "_type") mapping = po.ToObject<TypeFieldMapping>();
					if (name == "_source") mapping = po.ToObject<SourceFieldMapping>();
					if (name == "_analyzer") mapping = po.ToObject<AnalyzerFieldMapping>();
					if (name == "_routing") mapping = po.ToObject<RoutingFieldMapping>();
					if (name == "_index") mapping = po.ToObject<IndexFieldMapping>();
					if (name == "_size") mapping = po.ToObject<SizeFieldMapping>();
					if (name == "_timestamp") mapping = po.ToObject<TimestampFieldMapping>();
					if (name == "_ttl") mapping = po.ToObject<TtlFieldMapping>();
					if (name == "_parent") mapping = po.ToObject<ParentFieldMapping>();
					//TODO _field_names does not seem to have a special mapping (just returns like _uid) needs CONFIRMATION
				}
				if (mapping == null) continue;

				var esType = mapping as IElasticType;
				if (esType != null)
					esType.Name = name;

				r.Add(name, mapping);

			}
			return r;
		}

	}
}