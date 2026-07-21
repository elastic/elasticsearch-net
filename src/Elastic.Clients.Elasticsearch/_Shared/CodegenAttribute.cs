// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;

namespace Elastic.Clients.Elasticsearch;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = false)]
internal sealed class CodegenAttribute : Attribute
{
	public bool ShouldGenerate { get; set; }

	/// <summary>
	/// When <c>false</c>, suppresses generation of the request-converter <c>FormatCode</c> method for this type
	/// while still generating the type itself (params, ctors, converters). Use for types whose request-converter
	/// <c>FormatCode</c> is hand-crafted but which are otherwise generated (e.g. <c>BulkRequest</c>). Defaults to <c>true</c>.
	/// </summary>
	public bool GenerateFormatCode { get; set; } = true;
}
