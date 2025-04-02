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

namespace Elastic.Clients.Elasticsearch.Esql;

internal sealed partial class EsqlFormatConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Esql.EsqlFormat>
{
	private static readonly System.Text.Json.JsonEncodedText MemberCsv = System.Text.Json.JsonEncodedText.Encode("csv");
	private static readonly System.Text.Json.JsonEncodedText MemberJson = System.Text.Json.JsonEncodedText.Encode("json");
	private static readonly System.Text.Json.JsonEncodedText MemberTsv = System.Text.Json.JsonEncodedText.Encode("tsv");
	private static readonly System.Text.Json.JsonEncodedText MemberTxt = System.Text.Json.JsonEncodedText.Encode("txt");
	private static readonly System.Text.Json.JsonEncodedText MemberYaml = System.Text.Json.JsonEncodedText.Encode("yaml");
	private static readonly System.Text.Json.JsonEncodedText MemberCbor = System.Text.Json.JsonEncodedText.Encode("cbor");
	private static readonly System.Text.Json.JsonEncodedText MemberSmile = System.Text.Json.JsonEncodedText.Encode("smile");
	private static readonly System.Text.Json.JsonEncodedText MemberArrow = System.Text.Json.JsonEncodedText.Encode("arrow");

	public override Elastic.Clients.Elasticsearch.Esql.EsqlFormat Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		if (reader.ValueTextEquals(MemberCsv))
		{
			return Elastic.Clients.Elasticsearch.Esql.EsqlFormat.Csv;
		}

		if (reader.ValueTextEquals(MemberJson))
		{
			return Elastic.Clients.Elasticsearch.Esql.EsqlFormat.Json;
		}

		if (reader.ValueTextEquals(MemberTsv))
		{
			return Elastic.Clients.Elasticsearch.Esql.EsqlFormat.Tsv;
		}

		if (reader.ValueTextEquals(MemberTxt))
		{
			return Elastic.Clients.Elasticsearch.Esql.EsqlFormat.Txt;
		}

		if (reader.ValueTextEquals(MemberYaml))
		{
			return Elastic.Clients.Elasticsearch.Esql.EsqlFormat.Yaml;
		}

		if (reader.ValueTextEquals(MemberCbor))
		{
			return Elastic.Clients.Elasticsearch.Esql.EsqlFormat.Cbor;
		}

		if (reader.ValueTextEquals(MemberSmile))
		{
			return Elastic.Clients.Elasticsearch.Esql.EsqlFormat.Smile;
		}

		if (reader.ValueTextEquals(MemberArrow))
		{
			return Elastic.Clients.Elasticsearch.Esql.EsqlFormat.Arrow;
		}

		var value = reader.GetString()!;
		if (string.Equals(value, MemberCsv.Value, System.StringComparison.OrdinalIgnoreCase))
		{
			return Elastic.Clients.Elasticsearch.Esql.EsqlFormat.Csv;
		}

		if (string.Equals(value, MemberJson.Value, System.StringComparison.OrdinalIgnoreCase))
		{
			return Elastic.Clients.Elasticsearch.Esql.EsqlFormat.Json;
		}

		if (string.Equals(value, MemberTsv.Value, System.StringComparison.OrdinalIgnoreCase))
		{
			return Elastic.Clients.Elasticsearch.Esql.EsqlFormat.Tsv;
		}

		if (string.Equals(value, MemberTxt.Value, System.StringComparison.OrdinalIgnoreCase))
		{
			return Elastic.Clients.Elasticsearch.Esql.EsqlFormat.Txt;
		}

		if (string.Equals(value, MemberYaml.Value, System.StringComparison.OrdinalIgnoreCase))
		{
			return Elastic.Clients.Elasticsearch.Esql.EsqlFormat.Yaml;
		}

		if (string.Equals(value, MemberCbor.Value, System.StringComparison.OrdinalIgnoreCase))
		{
			return Elastic.Clients.Elasticsearch.Esql.EsqlFormat.Cbor;
		}

		if (string.Equals(value, MemberSmile.Value, System.StringComparison.OrdinalIgnoreCase))
		{
			return Elastic.Clients.Elasticsearch.Esql.EsqlFormat.Smile;
		}

		if (string.Equals(value, MemberArrow.Value, System.StringComparison.OrdinalIgnoreCase))
		{
			return Elastic.Clients.Elasticsearch.Esql.EsqlFormat.Arrow;
		}

		throw new System.Text.Json.JsonException($"Unknown member '{value}' for enum '{nameof(Elastic.Clients.Elasticsearch.Esql.EsqlFormat)}'.");
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Esql.EsqlFormat value, System.Text.Json.JsonSerializerOptions options)
	{
		switch (value)
		{
			case Elastic.Clients.Elasticsearch.Esql.EsqlFormat.Csv:
				writer.WriteStringValue(MemberCsv);
				break;
			case Elastic.Clients.Elasticsearch.Esql.EsqlFormat.Json:
				writer.WriteStringValue(MemberJson);
				break;
			case Elastic.Clients.Elasticsearch.Esql.EsqlFormat.Tsv:
				writer.WriteStringValue(MemberTsv);
				break;
			case Elastic.Clients.Elasticsearch.Esql.EsqlFormat.Txt:
				writer.WriteStringValue(MemberTxt);
				break;
			case Elastic.Clients.Elasticsearch.Esql.EsqlFormat.Yaml:
				writer.WriteStringValue(MemberYaml);
				break;
			case Elastic.Clients.Elasticsearch.Esql.EsqlFormat.Cbor:
				writer.WriteStringValue(MemberCbor);
				break;
			case Elastic.Clients.Elasticsearch.Esql.EsqlFormat.Smile:
				writer.WriteStringValue(MemberSmile);
				break;
			case Elastic.Clients.Elasticsearch.Esql.EsqlFormat.Arrow:
				writer.WriteStringValue(MemberArrow);
				break;
			default:
				throw new System.Text.Json.JsonException($"Invalid value '{value}' for enum '{nameof(Elastic.Clients.Elasticsearch.Esql.EsqlFormat)}'.");
		}
	}

	public override Elastic.Clients.Elasticsearch.Esql.EsqlFormat ReadAsPropertyName(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		return Read(ref reader, typeToConvert, options);
	}

	public override void WriteAsPropertyName(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Esql.EsqlFormat value, System.Text.Json.JsonSerializerOptions options)
	{
		Write(writer, value, options);
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Esql.EsqlFormatConverter))]
public enum EsqlFormat
{
	[System.Runtime.Serialization.EnumMember(Value = "csv")]
	Csv,
	[System.Runtime.Serialization.EnumMember(Value = "json")]
	Json,
	[System.Runtime.Serialization.EnumMember(Value = "tsv")]
	Tsv,
	[System.Runtime.Serialization.EnumMember(Value = "txt")]
	Txt,
	[System.Runtime.Serialization.EnumMember(Value = "yaml")]
	Yaml,
	[System.Runtime.Serialization.EnumMember(Value = "cbor")]
	Cbor,
	[System.Runtime.Serialization.EnumMember(Value = "smile")]
	Smile,
	[System.Runtime.Serialization.EnumMember(Value = "arrow")]
	Arrow
}