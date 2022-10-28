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
[JsonConverter(typeof(DateMathOperationConverter))]
public enum DateMathOperation
{
	[EnumMember(Value = "+")]
	Add,

	[EnumMember(Value = "-")]
	Subtract
}

internal sealed class DateMathOperationConverter : JsonConverter<DateMathOperation>
{
	public override DateMathOperation Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		var enumString = reader.GetString();
		switch (enumString)
		{
			case "+":
				return DateMathOperation.Add;
			case "-":
				return DateMathOperation.Subtract;
		}

		ThrowHelper.ThrowJsonException();
		return default;
	}

	public override void Write(Utf8JsonWriter writer, DateMathOperation value, JsonSerializerOptions options)
	{
		switch (value)
		{
			case DateMathOperation.Add:
				writer.WriteStringValue("+");
				return;
			case DateMathOperation.Subtract:
				writer.WriteStringValue("-");
				return;
		}

		writer.WriteNullValue();
	}
}

public static class DateMathOperationExtensions
{
	public static string GetStringValue(this DateMathOperation value)
	{
		switch (value)
		{
			case DateMathOperation.Add:
				return "+";
			case DateMathOperation.Subtract:
				return "-";
			default:
				throw new ArgumentOutOfRangeException(nameof(value), value, null);
		}
	}
}
