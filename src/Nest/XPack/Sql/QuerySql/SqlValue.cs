// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using Nest.Utf8Json;
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
			{
				reader.ReadNext();
				return null;
			}

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
