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
	}
}
