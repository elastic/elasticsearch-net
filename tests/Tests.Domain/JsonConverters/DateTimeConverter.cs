/*
 * Licensed to Elasticsearch B.V. under one or more contributor
 * license agreements. See the NOTICE file distributed with
 * this work for additional information regarding copyright
 * ownership. Elasticsearch B.V. licenses this file to you under
 * the Apache License, Version 2.0 (the "License"); you may
 * not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

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
