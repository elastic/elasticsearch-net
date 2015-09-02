using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Nest
{
	internal class PropertiesJsonConverter : JsonConverter
	{
		private readonly VerbatimDictionaryKeysJsonConverter _dictionaryConverter = new VerbatimDictionaryKeysJsonConverter();
		private readonly PropertyJsonConverter _elasticTypeConverter = new PropertyJsonConverter();

		public override bool CanConvert(Type objectType) => objectType == typeof(IProperties);
		public override bool CanWrite => true;

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			_dictionaryConverter.WriteJson(writer, value, serializer);
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			var r = new Properties();
			JObject o = JObject.Load(reader);

			foreach (var p in o.Properties())
			{
				var name = p.Name;
				var po = p.First as JObject;
				if (po == null)
					continue;

				var mapping = _elasticTypeConverter.ReadJson(po.CreateReader(), objectType, existingValue, serializer)
				as IProperty;
				if (mapping == null)
					continue;
				mapping.Name = name;

				r.Add(name, mapping);

			}
			return r;
		}

	}
}