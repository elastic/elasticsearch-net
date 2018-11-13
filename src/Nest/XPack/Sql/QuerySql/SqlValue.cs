using System;
using Utf8Json;

namespace Nest
{
	[JsonFormatter(typeof(SqlValueJsonFormatter))]
	public class SqlValue : LazyDocument
	{
		internal SqlValue(ArraySegment<byte> arraySegment, IJsonFormatterResolver formatterResolver)
			: base(arraySegment, formatterResolver) { }
	}

	internal class SqlValueJsonFormatter : IJsonFormatter<SqlValue>
	{
		public SqlValue Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			if (reader.GetCurrentJsonToken() == JsonToken.Null)
				return null;

			// TODO: ensure this handles all types. May need switch () { reader.ReadNumberSegment(), etc. }
			var arraySegment = reader.ReadNextBlockSegment();

			return new SqlValue(arraySegment, formatterResolver);
		}

		public void Serialize(ref JsonWriter writer, SqlValue value, IJsonFormatterResolver formatterResolver)
		{
			if (value == null)
			{
				writer.WriteNull();
				return;
			}

			for (var i = value.ArraySegment.Offset; i < value.ArraySegment.Count; i++)
				writer.WriteByte(value.ArraySegment.Array[i]);
		}
	}
}
