// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch.Serialization;

internal sealed class IntermediateConverter<TValue> : JsonConverter<IReadOnlyDictionary<IndexName, TValue>>
{
	public override IReadOnlyDictionary<IndexName, TValue>? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		var converter = options.GetConverter(typeof(ReadOnlyIndexNameDictionary<>).MakeGenericType(typeof(TValue)));

		if (converter is ReadOnlyIndexNameDictionaryConverter<TValue> specialisedConverter)
		{
			return specialisedConverter.Read(ref reader, typeToConvert, options);
		}

		return null;
	}

	public override void Write(Utf8JsonWriter writer, IReadOnlyDictionary<IndexName, TValue> value, JsonSerializerOptions options) => throw new NotImplementedException();
}
