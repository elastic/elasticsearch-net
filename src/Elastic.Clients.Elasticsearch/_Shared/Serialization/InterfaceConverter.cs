// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch.Serialization;

internal sealed class InterfaceConverter<TInterface, TConcrete> : JsonConverter<TInterface>
	where TConcrete : class, TInterface
{
	public override TInterface Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) =>
		JsonSerializer.Deserialize<TConcrete>(ref reader, options);

	public override void Write(Utf8JsonWriter writer, TInterface value, JsonSerializerOptions options) =>
		JsonSerializer.Serialize(writer, value, value.GetType(), options);
}
