// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

namespace Elastic.Clients.Elasticsearch;

/// <summary>
/// Request-converter formatting helpers for nullable hand-crafted value types (readonly structs).
/// The generated FormatCode call sites invoke <c>instance.Property.FormatCode(writer)</c> where the
/// property is a <c>T?</c>; these extensions unwrap the nullable value type. Defined in the root client
/// namespace so they are visible to every generated formatter (which live in nested namespaces), and
/// only in the request-converter compilation (they depend on <see cref="RequestConverter.CodeWriter"/>).
/// </summary>
public static class NullableStructFormatCodeExtensions
{
	public static void FormatCode(this FieldValue? value, RequestConverter.CodeWriter writer)
	{
		if (value.HasValue)
			value.Value.FormatCode(writer);
		else
			writer.Write("null");
	}

	public static void FormatCode(this WaitForActiveShards? value, RequestConverter.CodeWriter writer)
	{
		if (value.HasValue)
			value.Value.FormatCode(writer);
		else
			writer.Write("null");
	}

	public static void FormatCode(this Cluster.WaitForNodes? value, RequestConverter.CodeWriter writer)
	{
		if (value.HasValue)
			value.Value.FormatCode(writer);
		else
			writer.Write("null");
	}
}
