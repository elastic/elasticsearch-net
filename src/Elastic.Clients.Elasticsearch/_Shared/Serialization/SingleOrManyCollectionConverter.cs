// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch.Serialization;

internal class SingleOrManyCollectionConverter<TItem> : JsonConverter<ICollection<TItem>>
{
	public override ICollection<TItem>? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) =>
		SingleOrManySerializationHelper.Deserialize<TItem>(ref reader, options);

	public override void Write(Utf8JsonWriter writer, ICollection<TItem> value, JsonSerializerOptions options) =>
		SingleOrManySerializationHelper.Serialize<TItem>(value, writer, options);

	public override bool CanConvert(Type typeToConvert) => true;
}
