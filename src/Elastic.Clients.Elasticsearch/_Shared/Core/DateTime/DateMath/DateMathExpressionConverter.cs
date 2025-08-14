// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch.Json;

public sealed class DateMathExpressionConverter : JsonConverter<DateMathExpression>
{
	public override DateMathExpression? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		if (reader.TokenType != JsonTokenType.String)
			return null;

		// TODO: Performance - Review potential to avoid allocation on DateTime path and use Span<byte>

		var value = reader.GetString();
		reader.Read();

		if (!value.Contains("|") && DateTime.TryParse(value, CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind, out var dateTime))
			return DateMath.Anchored(dateTime);

		return new DateMathExpression(value);
	}

	public override void Write(Utf8JsonWriter writer, DateMathExpression value, JsonSerializerOptions options)
	{
		if (value is null)
		{
			writer.WriteNullValue();
			return;
		}

		writer.WriteStringValue(value.ToString());
	}
}
