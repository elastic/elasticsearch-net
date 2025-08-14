// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Text.Json;
using System.Text.Json.Serialization;

using Elastic.Clients.Elasticsearch.Serialization;

namespace Elastic.Clients.Elasticsearch.Json;

public sealed class FieldsConverter :
	JsonConverter<Fields>
{
	public override Fields Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		var fields = reader.ReadCollectionValue<Field>(options, null)!;

		return new Fields(fields);
	}

	public override void Write(Utf8JsonWriter writer, Fields value, JsonSerializerOptions options)
	{
		if (value is null)
		{
			throw new ArgumentNullException(nameof(value));
		}

		writer.WriteCollectionValue(options, value.ListOfFields, null);
	}
}
