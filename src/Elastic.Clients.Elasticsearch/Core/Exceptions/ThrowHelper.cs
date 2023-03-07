// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Runtime.CompilerServices;
using System.Text.Json;

namespace Elastic.Clients.Elasticsearch;

internal static class ThrowHelper
{
	[MethodImpl(MethodImplOptions.NoInlining)]
	internal static void ThrowJsonException(string? message = null) => throw new JsonException(message);

	[MethodImpl(MethodImplOptions.NoInlining)]
	internal static void ThrowUnknownTaggedUnionVariantJsonException(string variantTag, Type interfaceType) =>
		throw new JsonException($"Encountered an unsupported variant tag '{variantTag}' on '{SimplifiedFullName(interfaceType)}', which could not be deserialized.");

	[MethodImpl(MethodImplOptions.NoInlining)]
	internal static void ThrowInvalidOperationException(string message) =>
		throw new InvalidOperationException(message);

	[MethodImpl(MethodImplOptions.NoInlining)]
#pragma warning disable IDE0057 // Use range operator
	private static string SimplifiedFullName(Type type) => type.FullName.Substring(30);
#pragma warning restore IDE0057 // Use range operator

	[MethodImpl(MethodImplOptions.NoInlining)]
	internal static void ThrowJsonExceptionForMissingSettings() => throw new JsonException("Unable to retrieve client settings for JsonSerializerOptions.");

	[MethodImpl(MethodImplOptions.NoInlining)]
	internal static void ThrowInvalidOperationForBulkWhenNotIStreamSerializable() => throw new InvalidOperationException("Operation must implement IStreamSerializable.");
}
