using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace Nest.Resolvers.Converters
{
	public class PropertiesJsonConverter : JsonConverter
	{
		private readonly DictionaryKeysAreNotFieldNamesJsonConverter _dictionaryConverter = new DictionaryKeysAreNotFieldNamesJsonConverter();
		private readonly ElasticTypeJsonConverter _elasticTypeConverter = new ElasticTypeJsonConverter();

		public override bool CanWrite
		{
			get { return true; }
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			_dictionaryConverter.WriteJson(writer, value, serializer);
		}


		public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
		JsonSerializer serializer)
		{
			var r = new Dictionary<FieldName, IElasticType>();

			JObject o = JObject.Load(reader);

			foreach (var p in o.Properties())
			{
				var name = p.Name;
				var po = p.First as JObject;
				if (po == null)
					continue;

				var mapping = _elasticTypeConverter.ReadJson(po.CreateReader(), objectType, existingValue, serializer)
				as IElasticType;
				if (mapping == null)
					continue;
				mapping.Name = name;

				r.Add(name, mapping);

			}
			return r;
		}

		public override bool CanConvert(Type objectType)
		{
			return objectType == typeof(IDictionary<string, IElasticType>);
		}

	}
}