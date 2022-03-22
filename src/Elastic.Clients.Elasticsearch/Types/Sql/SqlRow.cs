// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch.Sql;

[JsonConverter(typeof(SqlRowConverter))]
public sealed class SqlRow : ReadOnlyCollection<SqlValue>
{
	public SqlRow(IList<SqlValue> list) : base(list) { }
}

internal sealed class SqlRowConverter : JsonConverter<SqlRow>
{
	public override SqlRow? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		if (reader.TokenType == JsonTokenType.Null)
		{
			reader.Read();
			return null;
		}

		if (reader.TokenType == JsonTokenType.StartArray)
		{
			var values = new List<SqlValue>();

			while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
			{
				var value = JsonSerializer.Deserialize<SqlValue>(ref reader, options);
				values.Add(value);
			}

			return new SqlRow(values);
		}

		throw new JsonException($"Unexpected JSON token when deserializing {nameof(SqlRow)}.");
	}

	public override void Write(Utf8JsonWriter writer, SqlRow value, JsonSerializerOptions options) => throw new NotImplementedException();
}
