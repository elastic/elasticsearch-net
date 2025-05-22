// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Text.Json;
using System.Text.Json.Serialization;

using Elastic.Clients.Elasticsearch.Serialization;

namespace Elastic.Clients.Elasticsearch;

[JsonConverter(typeof(MinimumShouldMatchConverter))]
public sealed class MinimumShouldMatch : Union<int, string>
{
	public MinimumShouldMatch(int count) : base(count)
	{
	}

	public MinimumShouldMatch(string percentage) : base(percentage)
	{
	}

	public static MinimumShouldMatch Fixed(int count) => count;

	public static MinimumShouldMatch Percentage(double percentage) => $"{percentage}%";

	public static implicit operator MinimumShouldMatch(string first) => new(first);

	public static implicit operator MinimumShouldMatch(int second) => new(second);

	public static implicit operator MinimumShouldMatch(double second) => Percentage(second);
}

internal sealed class MinimumShouldMatchConverter :
	JsonConverter<MinimumShouldMatch>
{
	public override MinimumShouldMatch Read(ref Utf8JsonReader reader, Type typeToConvert,
		JsonSerializerOptions options)
	{
		return reader.TokenType switch
		{
			JsonTokenType.Number => new MinimumShouldMatch(reader.GetInt32()),
			JsonTokenType.String => new MinimumShouldMatch(reader.GetString()!),
			_ => throw reader.UnexpectedTokenException(JsonTokenType.Number, JsonTokenType.String)
		};
	}

	public override void Write(Utf8JsonWriter writer, MinimumShouldMatch value, JsonSerializerOptions options)
	{
		try
		{
			value.Match(
				writer.WriteNumberValue,
				writer.WriteStringValue
			);
		}
		catch (InvalidOperationException e)
		{
			throw new JsonException("Invalid union variant.", e);
		}
	}
}
