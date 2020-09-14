// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	internal class NullableDateTimeOffsetEpochSecondsFormatter : IJsonFormatter<DateTimeOffset?>
	{
		public DateTimeOffset? Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			if (reader.GetCurrentJsonToken() != JsonToken.Number)
			{
				reader.ReadNextBlock();
				return null;
			}

			var secondsSinceEpoch = reader.ReadDouble();
			var dateTimeOffset = DateTimeUtil.UnixEpoch.AddSeconds(secondsSinceEpoch);
			return dateTimeOffset;
		}

		public void Serialize(ref JsonWriter writer, DateTimeOffset? value, IJsonFormatterResolver formatterResolver)
		{
			if (value == null)
			{
				writer.WriteNull();
				return;
			}

			var dateTimeOffsetDifference = (value.Value - DateTimeUtil.UnixEpoch).TotalSeconds;
			writer.WriteInt64((long)dateTimeOffsetDifference);
		}
	}
}
