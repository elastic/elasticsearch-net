using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Nest
{
	internal class PropertyNameJsonConverter : JsonConverter
	{
		private readonly ElasticInferrer _infer;
		public PropertyNameJsonConverter(IConnectionSettingsValues connectionSettings)
		{
			_infer = new ElasticInferrer(connectionSettings);
		}

		public override bool CanRead => false;

		public override bool CanWrite => true;

		public override bool CanConvert(Type objectType) => true;

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var property = value as PropertyName;
			if (property == null)
			{
				writer.WriteNull();
				return;
			}
			writer.WriteValue(this._infer.PropertyName(property));
		}
		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			return null;
		}
	}
}

