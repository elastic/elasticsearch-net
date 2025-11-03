// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Text.Json;
using System.Text.Json.Serialization;

using Elastic.Clients.Elasticsearch.Serialization;

namespace Elastic.Clients.Elasticsearch.Sql.Json;

public sealed class SqlRowConverter : JsonConverter<SqlRow>
{
	public override SqlRow? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		var values = reader.ReadCollectionValue<SqlValue>(options, null);
		if (values is null)
		{
			return null;
		}

		return new SqlRow(values);
	}

	public override void Write(Utf8JsonWriter writer, SqlRow value, JsonSerializerOptions options) => throw new NotImplementedException();
}
