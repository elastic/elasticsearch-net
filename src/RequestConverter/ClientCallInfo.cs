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
/// <param name="DescriptorGenericArity">The full generic-parameter count of the descriptor-action overload whose
/// value parameters match the request's chain-head constructor - the count an inline descriptor call must spell
/// explicitly, since a lambda argument lets the compiler infer nothing on its own (e.g. <c>2</c> for
/// <c>UpdateAsync&lt;TDocument, TPartialDocument&gt;(IndexName, Id, Action&lt;…&gt;)</c>). <c>-1</c> means no such
/// overload exists, so the inline descriptor call falls back to the inline request form; <c>0</c> means a
/// non-generic overload, so nothing is spelled.</param>
public readonly record struct ClientCallInfo(string SubClient, string Method, int ResponseGenericArity, int DescriptorGenericArity);
