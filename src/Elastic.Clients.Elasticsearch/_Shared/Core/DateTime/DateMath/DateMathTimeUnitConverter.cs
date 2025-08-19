// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch.Json;

public sealed class DateMathTimeUnitConverter : JsonConverter<DateMathTimeUnit>
{
	public override DateMathTimeUnit Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		var enumString = reader.GetString();
		switch (enumString)
		{
			case "h":
				return DateMathTimeUnit.Hour;

			case "m":
				return DateMathTimeUnit.Minute;

			case "s":
				return DateMathTimeUnit.Second;

			case "d":
				return DateMathTimeUnit.Day;

			case "w":
				return DateMathTimeUnit.Week;

			case "M":
				return DateMathTimeUnit.Month;

			case "y":
				return DateMathTimeUnit.Year;
		}

		ThrowHelper.ThrowJsonException();
		return default;
	}

	public override void Write(Utf8JsonWriter writer, DateMathTimeUnit value, JsonSerializerOptions options)
	{
		switch (value)
		{
			case DateMathTimeUnit.Hour:
				writer.WriteStringValue("h");
				return;

			case DateMathTimeUnit.Minute:
				writer.WriteStringValue("m");
				return;

			case DateMathTimeUnit.Second:
				writer.WriteStringValue("s");
				return;

			case DateMathTimeUnit.Day:
				writer.WriteStringValue("d");
				return;

			case DateMathTimeUnit.Week:
				writer.WriteStringValue("w");
				return;

			case DateMathTimeUnit.Month:
				writer.WriteStringValue("M");
				return;

			case DateMathTimeUnit.Year:
				writer.WriteStringValue("y");
				return;
		}

		writer.WriteNullValue();
	}
}
