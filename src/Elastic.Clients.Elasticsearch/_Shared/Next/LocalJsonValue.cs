// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.Runtime.CompilerServices;
using System.Text.Json;

namespace Elastic.Clients.Elasticsearch.Serialization;

internal ref struct LocalJsonValue<T>
{
	public T? Value;
	public bool Initialized;

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public bool TryReadProperty(ref Utf8JsonReader reader, JsonSerializerOptions options, JsonEncodedText name,
		JsonReadFunc<T>? readValue)
	{
		var success = reader.TryReadProperty(options, name, ref Value, readValue);
		Initialized |= success;

		return success;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void ReadValue(ref Utf8JsonReader reader, JsonSerializerOptions options,
		JsonReadFunc<T>? readValue)
	{
		Initialized = true;
		Value = reader.ReadValue(options, readValue);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void ReadPropertyName(ref Utf8JsonReader reader, JsonSerializerOptions options,
		JsonReadFunc<T>? readValue)
	{
		Initialized = true;
		Value = reader.ReadPropertyName(options, readValue);
	}
}
