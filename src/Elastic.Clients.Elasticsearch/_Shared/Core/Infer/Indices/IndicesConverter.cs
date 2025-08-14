// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Text.Json;
using System.Text.Json.Serialization;

using Elastic.Clients.Elasticsearch.Serialization;

namespace Elastic.Clients.Elasticsearch.Json;

public sealed class IndicesJsonConverter :
	JsonConverter<Indices>
{
	public override Indices Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		var indices = reader.ReadSingleOrManyCollectionValue<IndexName>(options, null)!;

		return new Indices(indices);
	}

	public override void Write(Utf8JsonWriter writer, Indices value, JsonSerializerOptions options)
	{
		writer.WriteSingleOrManyCollectionValue(options, value.IndexNames, null);
	}
}
