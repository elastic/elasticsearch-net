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

using Elastic.Clients.Elasticsearch.Core;
using Elastic.Clients.Elasticsearch.Serialization;
using Elastic.Transport;
using System;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch.Eql;

[JsonConverter(typeof(ResultPositionConverter))]
public enum ResultPosition
{
	/// <summary>
	/// <para>
	/// Return the most recent matches, similar to the Unix tail command.
	/// </para>
	/// </summary>
	[EnumMember(Value = "tail")]
	Tail,
	/// <summary>
	/// <para>
	/// Return the earliest matches, similar to the Unix head command.
	/// </para>
	/// </summary>
	[EnumMember(Value = "head")]
	Head
}

internal sealed partial class ResultPositionConverter : System.Text.Json.Serialization.JsonConverter<ResultPosition>
{
	private static readonly System.Text.Json.JsonEncodedText MemberTail = System.Text.Json.JsonEncodedText.Encode("tail");
	private static readonly System.Text.Json.JsonEncodedText MemberHead = System.Text.Json.JsonEncodedText.Encode("head");

	public override ResultPosition Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.String);
		if (reader.ValueTextEquals(MemberTail))
		{
			return ResultPosition.Tail;
		}

		if (reader.ValueTextEquals(MemberHead))
		{
			return ResultPosition.Head;
		}

		throw new System.Text.Json.JsonException($"Unknown value '{reader.GetString()}' for enum '{nameof(ResultPosition)}'.");
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, ResultPosition value, System.Text.Json.JsonSerializerOptions options)
	{
		switch (value)
		{
			case ResultPosition.Tail:
				writer.WriteStringValue(MemberTail);
				break;
			case ResultPosition.Head:
				writer.WriteStringValue(MemberHead);
				break;
			default:
				throw new System.Text.Json.JsonException($"Invalid value '{value}' for enum '{nameof(ResultPosition)}'.");
		}
	}
}