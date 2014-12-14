using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nest.Resolvers.Converters
{
	public class ClusterRerouteCommandCollectionConverter : JsonConverter
	{
		public override bool CanConvert(Type objectType)
		{
			return typeof(IClusterRerouteCommand).IsAssignableFrom(objectType);
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			throw new NotImplementedException();
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var commands = (IList<IClusterRerouteCommand>)value;
			writer.WriteStartArray();
			foreach (var command in commands)
			{
				writer.WriteStartObject();
				writer.WritePropertyName(command.Name);
				serializer.Serialize(writer, command);
				writer.WriteEndObject();
			}
			writer.WriteEndArray();
		}

		public override bool CanRead { get { return false; } }
	}
}
