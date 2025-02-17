// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch.Serialization;

/// <summary>
/// Allows the code generator to use specialized converters on a per-property basis in custom converters.
/// </summary>
/// <remarks>
/// See e.g. <see cref="SourceConverter{T}"/> for an example.
/// </remarks>
internal interface IMarkerTypeConverter
{
	public JsonConverter WrappedConverter { get; }
}
