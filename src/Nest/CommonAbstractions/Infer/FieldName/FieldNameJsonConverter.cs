using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Nest
{
	internal class FieldNameJsonConverter : JsonConverter
	{
		private readonly ElasticInferrer _infer;
		public FieldNameJsonConverter(IConnectionSettingsValues connectionSettings)
		{
			_infer = new ElasticInferrer(connectionSettings);
		}

		public override bool CanRead => false;

		public override bool CanWrite => true;

		public override bool CanConvert(Type objectType) => true;

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var field = value as FieldName;
			if (field == null)
			{
				writer.WriteNull();
				return;
			}
			writer.WriteValue(this._infer.FieldName(field));
		}
		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			return null;
		}
	}
}

