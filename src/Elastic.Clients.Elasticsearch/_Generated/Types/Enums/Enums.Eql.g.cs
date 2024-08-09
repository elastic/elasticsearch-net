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

internal sealed class ResultPositionConverter : JsonConverter<ResultPosition>
{
	public override ResultPosition Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		var enumString = reader.GetString();
		switch (enumString)
		{
			case "tail":
				return ResultPosition.Tail;
			case "head":
				return ResultPosition.Head;
		}

		ThrowHelper.ThrowJsonException();
		return default;
	}

	public override void Write(Utf8JsonWriter writer, ResultPosition value, JsonSerializerOptions options)
	{
		switch (value)
		{
			case ResultPosition.Tail:
				writer.WriteStringValue("tail");
				return;
			case ResultPosition.Head:
				writer.WriteStringValue("head");
				return;
		}

		writer.WriteNullValue();
	}
}