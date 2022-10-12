// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch.Sql;

[JsonConverter(typeof(SqlValueConverter))]
public readonly struct SqlValue
{
	private readonly LazyJson _lazyDocument;

	internal SqlValue(LazyJson lazyDocument) => _lazyDocument = lazyDocument;

	public T? As<T>() => _lazyDocument.As<T>();
}

internal sealed class SqlValueConverter : JsonConverter<SqlValue>
{
	public override SqlValue Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		if (reader.TokenType == JsonTokenType.Null)
		{
			reader.Read();
			return default;
		}

		var lazyDoc = JsonSerializer.Deserialize<LazyJson>(ref reader, options);
		return new SqlValue(lazyDoc);
	}

	public override void Write(Utf8JsonWriter writer, SqlValue value, JsonSerializerOptions options) => throw new NotImplementedException();
}
