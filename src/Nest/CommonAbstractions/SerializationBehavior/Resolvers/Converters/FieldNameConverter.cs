using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;


namespace Nest.Resolvers.Converters
{
	public class FieldNameConverter : JsonConverter
	{
		private readonly ElasticInferrer _infer;
		public FieldNameConverter(IConnectionSettingsValues connectionSettings)
		{
			_infer = new ElasticInferrer(connectionSettings);
		}

		public override bool CanRead { get { return false; } }

		public override bool CanWrite { get { return true; } }

		public override bool CanConvert(Type objectType)
		{
			return true; //only to be used with attribute or contract registration.
		}
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

