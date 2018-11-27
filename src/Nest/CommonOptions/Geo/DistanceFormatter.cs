using System;
using System.IO;
using Elasticsearch.Net;
using System.Runtime.Serialization;
using System.Text;
using Utf8Json;

namespace Nest
{
	internal class DistanceFormatter : IJsonFormatter<Distance>
	{
		public void Serialize(ref JsonWriter writer, Distance value, IJsonFormatterResolver formatterResolver)
		{
			if (value == null)
			{
				writer.WriteNull();
				return;
			}

			writer.WriteQuotation();
			writer.WriteRaw(Encoding.UTF8.GetBytes(value.Precision.ToString()));
			writer.WriteRaw(Encoding.UTF8.GetBytes(value.Unit.GetStringValue()));
			writer.WriteQuotation();
		}

		public Distance Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			if (reader.GetCurrentJsonToken() != JsonToken.String)
				return null;
			var value = reader.ReadString();
			return value == null
				? null
				: new Distance(value);
		}
	}
}
