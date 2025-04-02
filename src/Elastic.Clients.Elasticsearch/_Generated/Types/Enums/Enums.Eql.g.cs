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

namespace Elastic.Clients.Elasticsearch.Eql;

internal sealed partial class ResultPositionConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Eql.ResultPosition>
{
	private static readonly System.Text.Json.JsonEncodedText MemberTail = System.Text.Json.JsonEncodedText.Encode("tail");
	private static readonly System.Text.Json.JsonEncodedText MemberHead = System.Text.Json.JsonEncodedText.Encode("head");

	public override Elastic.Clients.Elasticsearch.Eql.ResultPosition Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		if (reader.ValueTextEquals(MemberTail))
		{
			return Elastic.Clients.Elasticsearch.Eql.ResultPosition.Tail;
		}

		if (reader.ValueTextEquals(MemberHead))
		{
			return Elastic.Clients.Elasticsearch.Eql.ResultPosition.Head;
		}

		var value = reader.GetString()!;
		if (string.Equals(value, MemberTail.Value, System.StringComparison.OrdinalIgnoreCase))
		{
			return Elastic.Clients.Elasticsearch.Eql.ResultPosition.Tail;
		}

		if (string.Equals(value, MemberHead.Value, System.StringComparison.OrdinalIgnoreCase))
		{
			return Elastic.Clients.Elasticsearch.Eql.ResultPosition.Head;
		}

		throw new System.Text.Json.JsonException($"Unknown member '{value}' for enum '{nameof(Elastic.Clients.Elasticsearch.Eql.ResultPosition)}'.");
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Eql.ResultPosition value, System.Text.Json.JsonSerializerOptions options)
	{
		switch (value)
		{
			case Elastic.Clients.Elasticsearch.Eql.ResultPosition.Tail:
				writer.WriteStringValue(MemberTail);
				break;
			case Elastic.Clients.Elasticsearch.Eql.ResultPosition.Head:
				writer.WriteStringValue(MemberHead);
				break;
			default:
				throw new System.Text.Json.JsonException($"Invalid value '{value}' for enum '{nameof(Elastic.Clients.Elasticsearch.Eql.ResultPosition)}'.");
		}
	}

	public override Elastic.Clients.Elasticsearch.Eql.ResultPosition ReadAsPropertyName(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		return Read(ref reader, typeToConvert, options);
	}

	public override void WriteAsPropertyName(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Eql.ResultPosition value, System.Text.Json.JsonSerializerOptions options)
	{
		Write(writer, value, options);
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Eql.ResultPositionConverter))]
public enum ResultPosition
{
	/// <summary>
	/// <para>
	/// Return the most recent matches, similar to the Unix tail command.
	/// </para>
	/// </summary>
	[System.Runtime.Serialization.EnumMember(Value = "tail")]
	Tail,
	/// <summary>
	/// <para>
	/// Return the earliest matches, similar to the Unix head command.
	/// </para>
	/// </summary>
	[System.Runtime.Serialization.EnumMember(Value = "head")]
	Head
}