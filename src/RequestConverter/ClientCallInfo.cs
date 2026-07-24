// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

namespace RequestConverter;

/// <summary>
/// The strongly-typed client method that executes a converted request.
/// </summary>
/// <param name="SubClient">The sub-client accessor on <c>ElasticsearchClient</c> (e.g. <c>Esql</c>), or an empty
/// string for a method on the root client.</param>
/// <param name="Method">The synchronous method name (e.g. <c>Query</c>); the asynchronous flavor appends
/// <c>Async</c>.</param>
/// <param name="ResponseGenericArity">The number of generic type parameters on the client method that are not
/// inferrable from the request argument and must be spelled explicitly (e.g. <c>1</c> for
/// <c>SearchAsync&lt;TDocument&gt;(SearchRequest)</c>).</param>
public readonly record struct ClientCallInfo(string SubClient, string Method, int ResponseGenericArity);
