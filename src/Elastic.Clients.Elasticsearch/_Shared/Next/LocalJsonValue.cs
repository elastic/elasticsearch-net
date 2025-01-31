// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Text.Json;

namespace Elastic.Clients.Elasticsearch.Serialization;

internal ref struct LocalJsonValue<T>
{
	public T? Value;
	public bool Initialized;

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public bool TryRead(ref Utf8JsonReader reader, JsonSerializerOptions options, JsonEncodedText name,
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] Type? markerType = null)
	{
		var success = reader.TryReadProperty(options, name, ref Value, markerType);
		Initialized |= success;

		return success;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void ReadValue(ref Utf8JsonReader reader, JsonSerializerOptions options,
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] Type? markerType = null)
	{
		Initialized = true;
		Value = reader.ReadValue<T>(options, markerType);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public void ReadPropertyName(ref Utf8JsonReader reader, JsonSerializerOptions options,
		[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] Type? markerType = null)
	{
		Initialized = true;
		Value = reader.ReadPropertyName<T>(options);
	}
}
