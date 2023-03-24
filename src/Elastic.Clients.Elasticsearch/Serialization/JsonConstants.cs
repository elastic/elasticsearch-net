// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;

namespace Elastic.Clients.Elasticsearch.Serialization;

internal static class JsonConstants
{
#pragma warning disable IDE0230 // Use UTF-8 string literal
	public static ReadOnlySpan<byte> NonIntegerBytes => new[] { (byte)'E', (byte)'.' }; // In the future, when we move to the .NET 7 SDK, it would be nice to use u8 literals e.g. "E."u8
#pragma warning restore IDE0230 // Use UTF-8 string literal
}
