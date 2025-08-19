// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Text.Json;
using System.Text.Json.Serialization;

using Elastic.Clients.Elasticsearch.Serialization;

namespace Elastic.Clients.Elasticsearch.Json;

public sealed class IdsConverter : JsonConverter<Ids>
{
	public override Ids? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		var ids = reader.ReadCollectionValue<Id>(options, null)!;

		return new Ids(ids);
	}

	public override void Write(Utf8JsonWriter writer, Ids value, JsonSerializerOptions options)
	{
		if (value is null)
		{
			throw new ArgumentNullException(nameof(value));
		}

		writer.WriteCollectionValue(options, value.IdsToSerialize, null);
	}
}
