// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Runtime.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;
using Elastic.Clients.Elasticsearch.Serialization;

namespace Elastic.Clients.Elasticsearch;

[StringEnum]
[JsonConverter(typeof(DateMathTimeUnitConverter))]
public enum DateMathTimeUnit
{
	[EnumMember(Value = "s")]
	Second,

	[EnumMember(Value = "m")]
	Minute,

	[EnumMember(Value = "h")]
	Hour,

	[EnumMember(Value = "d")]
	Day,

	[EnumMember(Value = "w")]
	Week,

	[EnumMember(Value = "M")]
	Month,

	[EnumMember(Value = "y")]
	Year
}

internal sealed class DateMathTimeUnitConverter : JsonConverter<DateMathTimeUnit>
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

public static class DateMathTimeUnitExtensions
{
	public static string GetStringValue(this DateMathTimeUnit value) =>
		value switch
		{
			DateMathTimeUnit.Second => "s",
			DateMathTimeUnit.Minute => "m",
			DateMathTimeUnit.Hour => "h",
			DateMathTimeUnit.Day => "d",
			DateMathTimeUnit.Week => "w",
			DateMathTimeUnit.Month => "M",
			DateMathTimeUnit.Year => "y",
			_ => throw new ArgumentOutOfRangeException(nameof(value), value, null),
		};
}
