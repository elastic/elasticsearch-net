using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Elasticsearch.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Nest
{
	internal class StatefulSerializerFactory
	{
		public JsonNetSerializer CreateStateful(IConnectionSettingsValues settings, JsonConverter converter) =>
			new JsonNetSerializer(settings, converter);
	}

	public class SourceConverter : JsonConverter
	{
		public override bool CanRead => true;
		public override bool CanWrite => true;
		public override bool CanConvert(Type objectType) => true;

		public virtual SerializationFormatting Formatting { get; } = SerializationFormatting.Indented;

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var sourceSerializer = serializer.GetConnectionSettings().SourceSerializer;
			var v = sourceSerializer.SerializeToString(value, Formatting);
			writer.WriteRawValue(v);
		}


		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			//TODO pooling of memory?
			using (var ms = new MemoryStream(reader.ReadAsBytes()))
				return serializer.GetConnectionSettings().SourceSerializer.Deserialize(objectType, ms);
		}
	}

	public class CollapsedSourceConverter : SourceConverter
	{
		public override SerializationFormatting Formatting { get; } = SerializationFormatting.None;
	}
}
