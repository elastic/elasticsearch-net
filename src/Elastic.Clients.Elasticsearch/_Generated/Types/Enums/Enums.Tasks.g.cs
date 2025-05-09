// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.
//
// ███╗   ██╗ ██████╗ ████████╗██╗ ██████╗███████╗
// ████╗  ██║██╔═══██╗╚══██╔══╝██║██╔════╝██╔════╝
// ██╔██╗ ██║██║   ██║   ██║   ██║██║     █████╗
// ██║╚██╗██║██║   ██║   ██║   ██║██║     ██╔══╝
// ██║ ╚████║╚██████╔╝   ██║   ██║╚██████╗███████╗
// ╚═╝  ╚═══╝ ╚═════╝    ╚═╝   ╚═╝ ╚═════╝╚══════╝
// ------------------------------------------------
//
// This file is automatically generated.
// Please do not edit these files manually.
//
// ------------------------------------------------

#nullable restore

using System;
using System.Linq;
using Elastic.Clients.Elasticsearch.Serialization;

namespace Elastic.Clients.Elasticsearch.Tasks;

internal sealed partial class GroupByConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Tasks.GroupBy>
{
	private static readonly System.Text.Json.JsonEncodedText MemberNodes = System.Text.Json.JsonEncodedText.Encode("nodes");
	private static readonly System.Text.Json.JsonEncodedText MemberNone = System.Text.Json.JsonEncodedText.Encode("none");
	private static readonly System.Text.Json.JsonEncodedText MemberParents = System.Text.Json.JsonEncodedText.Encode("parents");

	public override Elastic.Clients.Elasticsearch.Tasks.GroupBy Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		if (reader.ValueTextEquals(MemberNodes))
		{
			return Elastic.Clients.Elasticsearch.Tasks.GroupBy.Nodes;
		}

		if (reader.ValueTextEquals(MemberNone))
		{
			return Elastic.Clients.Elasticsearch.Tasks.GroupBy.None;
		}

		if (reader.ValueTextEquals(MemberParents))
		{
			return Elastic.Clients.Elasticsearch.Tasks.GroupBy.Parents;
		}

		var value = reader.GetString()!;
		if (string.Equals(value, MemberNodes.Value, System.StringComparison.OrdinalIgnoreCase))
		{
			return Elastic.Clients.Elasticsearch.Tasks.GroupBy.Nodes;
		}

		if (string.Equals(value, MemberNone.Value, System.StringComparison.OrdinalIgnoreCase))
		{
			return Elastic.Clients.Elasticsearch.Tasks.GroupBy.None;
		}

		if (string.Equals(value, MemberParents.Value, System.StringComparison.OrdinalIgnoreCase))
		{
			return Elastic.Clients.Elasticsearch.Tasks.GroupBy.Parents;
		}

		throw new System.Text.Json.JsonException($"Unknown member '{value}' for enum '{nameof(Elastic.Clients.Elasticsearch.Tasks.GroupBy)}'.");
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Tasks.GroupBy value, System.Text.Json.JsonSerializerOptions options)
	{
		switch (value)
		{
			case Elastic.Clients.Elasticsearch.Tasks.GroupBy.Nodes:
				writer.WriteStringValue(MemberNodes);
				break;
			case Elastic.Clients.Elasticsearch.Tasks.GroupBy.None:
				writer.WriteStringValue(MemberNone);
				break;
			case Elastic.Clients.Elasticsearch.Tasks.GroupBy.Parents:
				writer.WriteStringValue(MemberParents);
				break;
			default:
				throw new System.Text.Json.JsonException($"Invalid value '{value}' for enum '{nameof(Elastic.Clients.Elasticsearch.Tasks.GroupBy)}'.");
		}
	}

	public override Elastic.Clients.Elasticsearch.Tasks.GroupBy ReadAsPropertyName(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		return Read(ref reader, typeToConvert, options);
	}

	public override void WriteAsPropertyName(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Tasks.GroupBy value, System.Text.Json.JsonSerializerOptions options)
	{
		Write(writer, value, options);
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Tasks.GroupByConverter))]
public enum GroupBy
{
	/// <summary>
	/// <para>
	/// Group tasks by node ID.
	/// </para>
	/// </summary>
	[System.Runtime.Serialization.EnumMember(Value = "nodes")]
	Nodes,
	/// <summary>
	/// <para>
	/// Do not group tasks.
	/// </para>
	/// </summary>
	[System.Runtime.Serialization.EnumMember(Value = "none")]
	None,
	/// <summary>
	/// <para>
	/// Group tasks by parent task ID.
	/// </para>
	/// </summary>
	[System.Runtime.Serialization.EnumMember(Value = "parents")]
	Parents
}