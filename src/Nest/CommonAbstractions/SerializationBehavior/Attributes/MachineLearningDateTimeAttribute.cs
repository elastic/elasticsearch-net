// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using Elasticsearch.Net.Utf8Json;
using Elasticsearch.Net.Utf8Json.Formatters;


namespace Nest
{
	internal static class DateTimeUtil
	{
		public static readonly DateTimeOffset UnixEpoch = new DateTimeOffset(1970, 1, 1, 0, 0, 0, 0, TimeSpan.Zero);
	}

	/// <summary>
	/// Signals that this date time property is used in Machine learning APIs some of which will always return the date as
	/// epoch.
	/// </summary>
	[AttributeUsage(AttributeTargets.Property)]
	public class MachineLearningDateTimeAttribute : Attribute { }

	internal class MachineLearningDateTimeOffsetFormatter : IJsonFormatter<DateTimeOffset>
	{
		public DateTimeOffset Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			var token = reader.GetCurrentJsonToken();

			if (token == JsonToken.String)
			{
				var formatter = formatterResolver.GetFormatter<DateTimeOffset>();
				return formatter.Deserialize(ref reader, formatterResolver);
			}
			if (token == JsonToken.Null)
			{
				reader.ReadNext();
				return default;
			}

			if (token == JsonToken.Number)
			{
				var millisecondsSinceEpoch = reader.ReadDouble();
				var dateTimeOffset = DateTimeUtil.UnixEpoch.AddMilliseconds(millisecondsSinceEpoch);
				return dateTimeOffset;
			}

			throw new Exception($"Cannot deserialize {nameof(DateTimeOffset)} from token {token}");
		}

		public virtual void Serialize(ref JsonWriter writer, DateTimeOffset value, IJsonFormatterResolver formatterResolver) =>
			ISO8601DateTimeOffsetFormatter.Default.Serialize(ref writer, value, formatterResolver);
	}

	internal class MachineLearningDateTimeFormatter : IJsonFormatter<DateTime>
	{
		public static readonly MachineLearningDateTimeFormatter Instance = new MachineLearningDateTimeFormatter();

		public DateTime Deserialize(ref JsonReader reader, IJsonFormatterResolver formatterResolver)
		{
			var token = reader.GetCurrentJsonToken();

			if (token == JsonToken.String)
			{
				var formatter = formatterResolver.GetFormatter<DateTime>();
				return formatter.Deserialize(ref reader, formatterResolver);
			}
			if (token == JsonToken.Null)
			{
				reader.ReadNext();
				return default;
			}

			if (token == JsonToken.Number)
			{
				var millisecondsSinceEpoch = reader.ReadDouble();
				var dateTimeOffset = DateTimeUtil.UnixEpoch.AddMilliseconds(millisecondsSinceEpoch);
				return dateTimeOffset.DateTime;
			}

			throw new Exception($"Cannot deserialize {nameof(DateTimeOffset)} from token {token}");
		}

		public void Serialize(ref JsonWriter writer, DateTime value, IJsonFormatterResolver formatterResolver) =>
			ISO8601DateTimeFormatter.Default.Serialize(ref writer, value, formatterResolver);
	}
}
