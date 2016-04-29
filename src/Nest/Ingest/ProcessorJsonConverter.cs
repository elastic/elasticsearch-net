using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nest
{
	internal class ProcessorJsonConverter<TReadAs> : ReserializeJsonConverter<TReadAs, IProcessor>
		where TReadAs : class, IProcessor, new()
	{
		protected override void SerializeJson(JsonWriter writer, object value, IProcessor castValue, JsonSerializer serializer)
		{
			var name = castValue.Name;
			if (name == null) return;
			writer.WriteStartObject();
			{
				writer.WritePropertyName(name);
				{
					this.Reserialize(writer, value, serializer);
				}
			}
			writer.WriteEndObject();
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			if (reader.TokenType != JsonToken.StartObject) return null;
			reader.Read(); //property name of processor type
			if (reader.TokenType != JsonToken.PropertyName) return null;
			reader.Read();
			return base.ReadJson(reader, objectType, existingValue, serializer);
		}
	}
}
