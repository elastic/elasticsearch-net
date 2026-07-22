// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.IO;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Elastic.Clients.Elasticsearch.Serialization;

/// <summary>
/// Implemented by a type's <see cref="System.Text.Json.Serialization.JsonConverter"/> to declare that the type's
/// NDJSON body can be read by streaming one value at a time instead of buffering the whole body. The serializer
/// discovers the capability through the type's registered converter, so no per-type dispatch is hard-coded.
/// </summary>
internal interface INdjsonStreamReadable
{
	object Read(Stream stream, JsonSerializerOptions options);

	ValueTask<object> ReadAsync(Stream stream, JsonSerializerOptions options, CancellationToken cancellationToken);
}
