using System;
using System.Linq;
using Utf8Json;
using Utf8Json.Internal;

namespace Nest
{
	[JsonFormatter(typeof(SqlValueFormatter))]
	public class SqlValue : LazyDocument
	{
		internal SqlValue(byte[] bytes, IJsonFormatterResolver formatterResolver)
			: base(bytes, formatterResolver) { }
	}

	internal class SqlValueFormatter : IJsonFormatter<SqlValue>
	{
		public SqlValue Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			if (reader.GetCurrentJsonToken() == JsonToken.Null)
				return null;

			var arraySegment = reader.ReadNextBlockSegment();
			return new SqlValue(BinaryUtil.ToArray(ref arraySegment), formatterResolver);
		}

		public void Serialize(ref JsonWriter writer, SqlValue value, IJsonFormatterResolver formatterResolver)
		{
			if (value == null)
			{
				writer.WriteNull();
				return;
			}

			for (var i = 0; i < value.Bytes.Length; i++)
				writer.WriteByte(value.Bytes[i]);
		}
	}
}
