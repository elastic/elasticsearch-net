// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using Elasticsearch.Net.Utf8Json;

namespace Nest
{
	internal class DateTimeOffsetEpochMillisecondsFormatter : MachineLearningDateTimeOffsetFormatter
	{
		public override void Serialize(ref JsonWriter writer, DateTimeOffset value, IJsonFormatterResolver formatterResolver)
		{
			writer.WriteQuotation();
			writer.WriteInt64(value.ToUnixTimeMilliseconds());
			writer.WriteQuotation();
		}
	}

	internal class NullableDateTimeOffsetEpochMillisecondsFormatter : IJsonFormatter<DateTimeOffset?>
	{
		public DateTimeOffset? Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			var token = reader.GetCurrentJsonToken();

			switch (token)
			{
				case JsonToken.String:
				{
					var formatter = formatterResolver.GetFormatter<DateTimeOffset>();
					return formatter.Deserialize(ref reader, formatterResolver);
				}
				case JsonToken.Null:
				{
					reader.ReadNext();
					return null;
				}
				case JsonToken.Number:
				{
					var millisecondsSinceEpoch = reader.ReadDouble();
					var dateTimeOffset = DateTimeUtil.UnixEpoch.AddMilliseconds(millisecondsSinceEpoch);
					return dateTimeOffset;
				}
				default:
					throw new Exception($"Cannot deserialize {nameof(DateTimeOffset)} from token {token}");
			}
		}

		public void Serialize(ref JsonWriter writer, DateTimeOffset? value, IJsonFormatterResolver formatterResolver)
		{
			if (value == null)
			{
				writer.WriteNull();
				return;
			}

			writer.WriteQuotation();
			writer.WriteInt64(value.Value.ToUnixTimeMilliseconds());
			writer.WriteQuotation();
		}
	}
}
