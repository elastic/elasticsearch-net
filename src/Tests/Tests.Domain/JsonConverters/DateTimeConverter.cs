using System;
using System.Globalization;
using System.Text;
using Newtonsoft.Json;

namespace Tests.Domain.JsonConverters
{
	/// <summary>
	/// DateTime/DateTimeOffset converter that always serializes values with a minimum of three sub second fractions.
	/// This is to fix a bug in Elastisearch < 7.1.0: https://github.com/elastic/elasticsearch/pull/41871
	/// </summary>
	public class DateTimeConverter : Newtonsoft.Json.Converters.IsoDateTimeConverter
	{
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			if (value == null)
			{
				writer.WriteNull();
				return;
			}

			const string format = "yyyy'-'MM'-'dd'T'HH':'mm':'ss.FFFFFFF";
			var builder = new StringBuilder(33);

			if (value is DateTime dateTime)
			{
				if ((DateTimeStyles & DateTimeStyles.AdjustToUniversal) == DateTimeStyles.AdjustToUniversal
					|| (DateTimeStyles & DateTimeStyles.AssumeUniversal) == DateTimeStyles.AssumeUniversal)
				{
					dateTime = dateTime.ToUniversalTime();
				}

				builder.Append(dateTime.ToString(format, CultureInfo.InvariantCulture));
			}
			else if (value is DateTimeOffset dateTimeOffset)
			{
				if ((DateTimeStyles & DateTimeStyles.AdjustToUniversal) == DateTimeStyles.AdjustToUniversal
					|| (DateTimeStyles & DateTimeStyles.AssumeUniversal) == DateTimeStyles.AssumeUniversal)
				{
					dateTimeOffset = dateTimeOffset.ToUniversalTime();
				}

				builder.Append(dateTimeOffset.ToString(format, CultureInfo.InvariantCulture));
				dateTime = dateTimeOffset.DateTime;
			}
			else
				throw new JsonSerializationException(
					$"Unexpected value when converting date. Expected DateTime or DateTimeOffset, got {value.GetType()}.");

			if (builder.Length > 20 && builder.Length < 23)
			{
				var diff = 23 - builder.Length;
				for (var i = 0; i < diff; i++)
					builder.Append('0');
			}

			switch (dateTime.Kind)
			{
				case DateTimeKind.Local:
					var offset = TimeZoneInfo.Local.GetUtcOffset(dateTime);
					if (offset >= TimeSpan.Zero)
						builder.Append('+');
					else
					{
						builder.Append('-');
						offset = offset.Negate();
					}

					AppendTwoDigitNumber(builder, offset.Hours);
					builder.Append(':');
					AppendTwoDigitNumber(builder, offset.Minutes);
					break;
				case DateTimeKind.Utc:
					builder.Append('Z');
					break;
			}

			writer.WriteValue(builder.ToString());
		}

		private static void AppendTwoDigitNumber(StringBuilder result, int val)
		{
			result.Append((char)('0' + (val / 10)));
			result.Append((char)('0' + (val % 10)));
		}
	}
}
