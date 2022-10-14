// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.Runtime.CompilerServices;
using System.Text.Json;

namespace Elastic.Clients.Elasticsearch.Serialization;

internal class ThrowHelper
{
	[MethodImpl(MethodImplOptions.NoInlining)]
	public static void ThrowJsonException(string? message = null) => throw new JsonException(message);
}
