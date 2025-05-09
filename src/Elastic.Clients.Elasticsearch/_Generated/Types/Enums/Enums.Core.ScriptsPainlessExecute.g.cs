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

namespace Elastic.Clients.Elasticsearch.Core.ScriptsPainlessExecute;

internal sealed partial class PainlessContextConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Core.ScriptsPainlessExecute.PainlessContext>
{
	private static readonly System.Text.Json.JsonEncodedText MemberBooleanField = System.Text.Json.JsonEncodedText.Encode("boolean_field");
	private static readonly System.Text.Json.JsonEncodedText MemberCompositeField = System.Text.Json.JsonEncodedText.Encode("composite_field");
	private static readonly System.Text.Json.JsonEncodedText MemberDateField = System.Text.Json.JsonEncodedText.Encode("date_field");
	private static readonly System.Text.Json.JsonEncodedText MemberDoubleField = System.Text.Json.JsonEncodedText.Encode("double_field");
	private static readonly System.Text.Json.JsonEncodedText MemberFilter = System.Text.Json.JsonEncodedText.Encode("filter");
	private static readonly System.Text.Json.JsonEncodedText MemberGeoPointField = System.Text.Json.JsonEncodedText.Encode("geo_point_field");
	private static readonly System.Text.Json.JsonEncodedText MemberIpField = System.Text.Json.JsonEncodedText.Encode("ip_field");
	private static readonly System.Text.Json.JsonEncodedText MemberKeywordField = System.Text.Json.JsonEncodedText.Encode("keyword_field");
	private static readonly System.Text.Json.JsonEncodedText MemberLongField = System.Text.Json.JsonEncodedText.Encode("long_field");
	private static readonly System.Text.Json.JsonEncodedText MemberPainlessTest = System.Text.Json.JsonEncodedText.Encode("painless_test");
	private static readonly System.Text.Json.JsonEncodedText MemberScore = System.Text.Json.JsonEncodedText.Encode("score");

	public override Elastic.Clients.Elasticsearch.Core.ScriptsPainlessExecute.PainlessContext Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		if (reader.ValueTextEquals(MemberBooleanField))
		{
			return Elastic.Clients.Elasticsearch.Core.ScriptsPainlessExecute.PainlessContext.BooleanField;
		}

		if (reader.ValueTextEquals(MemberCompositeField))
		{
			return Elastic.Clients.Elasticsearch.Core.ScriptsPainlessExecute.PainlessContext.CompositeField;
		}

		if (reader.ValueTextEquals(MemberDateField))
		{
			return Elastic.Clients.Elasticsearch.Core.ScriptsPainlessExecute.PainlessContext.DateField;
		}

		if (reader.ValueTextEquals(MemberDoubleField))
		{
			return Elastic.Clients.Elasticsearch.Core.ScriptsPainlessExecute.PainlessContext.DoubleField;
		}

		if (reader.ValueTextEquals(MemberFilter))
		{
			return Elastic.Clients.Elasticsearch.Core.ScriptsPainlessExecute.PainlessContext.Filter;
		}

		if (reader.ValueTextEquals(MemberGeoPointField))
		{
			return Elastic.Clients.Elasticsearch.Core.ScriptsPainlessExecute.PainlessContext.GeoPointField;
		}

		if (reader.ValueTextEquals(MemberIpField))
		{
			return Elastic.Clients.Elasticsearch.Core.ScriptsPainlessExecute.PainlessContext.IpField;
		}

		if (reader.ValueTextEquals(MemberKeywordField))
		{
			return Elastic.Clients.Elasticsearch.Core.ScriptsPainlessExecute.PainlessContext.KeywordField;
		}

		if (reader.ValueTextEquals(MemberLongField))
		{
			return Elastic.Clients.Elasticsearch.Core.ScriptsPainlessExecute.PainlessContext.LongField;
		}

		if (reader.ValueTextEquals(MemberPainlessTest))
		{
			return Elastic.Clients.Elasticsearch.Core.ScriptsPainlessExecute.PainlessContext.PainlessTest;
		}

		if (reader.ValueTextEquals(MemberScore))
		{
			return Elastic.Clients.Elasticsearch.Core.ScriptsPainlessExecute.PainlessContext.Score;
		}

		var value = reader.GetString()!;
		if (string.Equals(value, MemberBooleanField.Value, System.StringComparison.OrdinalIgnoreCase))
		{
			return Elastic.Clients.Elasticsearch.Core.ScriptsPainlessExecute.PainlessContext.BooleanField;
		}

		if (string.Equals(value, MemberCompositeField.Value, System.StringComparison.OrdinalIgnoreCase))
		{
			return Elastic.Clients.Elasticsearch.Core.ScriptsPainlessExecute.PainlessContext.CompositeField;
		}

		if (string.Equals(value, MemberDateField.Value, System.StringComparison.OrdinalIgnoreCase))
		{
			return Elastic.Clients.Elasticsearch.Core.ScriptsPainlessExecute.PainlessContext.DateField;
		}

		if (string.Equals(value, MemberDoubleField.Value, System.StringComparison.OrdinalIgnoreCase))
		{
			return Elastic.Clients.Elasticsearch.Core.ScriptsPainlessExecute.PainlessContext.DoubleField;
		}

		if (string.Equals(value, MemberFilter.Value, System.StringComparison.OrdinalIgnoreCase))
		{
			return Elastic.Clients.Elasticsearch.Core.ScriptsPainlessExecute.PainlessContext.Filter;
		}

		if (string.Equals(value, MemberGeoPointField.Value, System.StringComparison.OrdinalIgnoreCase))
		{
			return Elastic.Clients.Elasticsearch.Core.ScriptsPainlessExecute.PainlessContext.GeoPointField;
		}

		if (string.Equals(value, MemberIpField.Value, System.StringComparison.OrdinalIgnoreCase))
		{
			return Elastic.Clients.Elasticsearch.Core.ScriptsPainlessExecute.PainlessContext.IpField;
		}

		if (string.Equals(value, MemberKeywordField.Value, System.StringComparison.OrdinalIgnoreCase))
		{
			return Elastic.Clients.Elasticsearch.Core.ScriptsPainlessExecute.PainlessContext.KeywordField;
		}

		if (string.Equals(value, MemberLongField.Value, System.StringComparison.OrdinalIgnoreCase))
		{
			return Elastic.Clients.Elasticsearch.Core.ScriptsPainlessExecute.PainlessContext.LongField;
		}

		if (string.Equals(value, MemberPainlessTest.Value, System.StringComparison.OrdinalIgnoreCase))
		{
			return Elastic.Clients.Elasticsearch.Core.ScriptsPainlessExecute.PainlessContext.PainlessTest;
		}

		if (string.Equals(value, MemberScore.Value, System.StringComparison.OrdinalIgnoreCase))
		{
			return Elastic.Clients.Elasticsearch.Core.ScriptsPainlessExecute.PainlessContext.Score;
		}

		throw new System.Text.Json.JsonException($"Unknown member '{value}' for enum '{nameof(Elastic.Clients.Elasticsearch.Core.ScriptsPainlessExecute.PainlessContext)}'.");
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Core.ScriptsPainlessExecute.PainlessContext value, System.Text.Json.JsonSerializerOptions options)
	{
		switch (value)
		{
			case Elastic.Clients.Elasticsearch.Core.ScriptsPainlessExecute.PainlessContext.BooleanField:
				writer.WriteStringValue(MemberBooleanField);
				break;
			case Elastic.Clients.Elasticsearch.Core.ScriptsPainlessExecute.PainlessContext.CompositeField:
				writer.WriteStringValue(MemberCompositeField);
				break;
			case Elastic.Clients.Elasticsearch.Core.ScriptsPainlessExecute.PainlessContext.DateField:
				writer.WriteStringValue(MemberDateField);
				break;
			case Elastic.Clients.Elasticsearch.Core.ScriptsPainlessExecute.PainlessContext.DoubleField:
				writer.WriteStringValue(MemberDoubleField);
				break;
			case Elastic.Clients.Elasticsearch.Core.ScriptsPainlessExecute.PainlessContext.Filter:
				writer.WriteStringValue(MemberFilter);
				break;
			case Elastic.Clients.Elasticsearch.Core.ScriptsPainlessExecute.PainlessContext.GeoPointField:
				writer.WriteStringValue(MemberGeoPointField);
				break;
			case Elastic.Clients.Elasticsearch.Core.ScriptsPainlessExecute.PainlessContext.IpField:
				writer.WriteStringValue(MemberIpField);
				break;
			case Elastic.Clients.Elasticsearch.Core.ScriptsPainlessExecute.PainlessContext.KeywordField:
				writer.WriteStringValue(MemberKeywordField);
				break;
			case Elastic.Clients.Elasticsearch.Core.ScriptsPainlessExecute.PainlessContext.LongField:
				writer.WriteStringValue(MemberLongField);
				break;
			case Elastic.Clients.Elasticsearch.Core.ScriptsPainlessExecute.PainlessContext.PainlessTest:
				writer.WriteStringValue(MemberPainlessTest);
				break;
			case Elastic.Clients.Elasticsearch.Core.ScriptsPainlessExecute.PainlessContext.Score:
				writer.WriteStringValue(MemberScore);
				break;
			default:
				throw new System.Text.Json.JsonException($"Invalid value '{value}' for enum '{nameof(Elastic.Clients.Elasticsearch.Core.ScriptsPainlessExecute.PainlessContext)}'.");
		}
	}

	public override Elastic.Clients.Elasticsearch.Core.ScriptsPainlessExecute.PainlessContext ReadAsPropertyName(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		return Read(ref reader, typeToConvert, options);
	}

	public override void WriteAsPropertyName(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Core.ScriptsPainlessExecute.PainlessContext value, System.Text.Json.JsonSerializerOptions options)
	{
		Write(writer, value, options);
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Core.ScriptsPainlessExecute.PainlessContextConverter))]
public enum PainlessContext
{
	/// <summary>
	/// <para>
	/// The context for boolean fields. The script returns a <c>true</c> or <c>false</c> response.
	/// </para>
	/// </summary>
	[System.Runtime.Serialization.EnumMember(Value = "boolean_field")]
	BooleanField,
	/// <summary>
	/// <para>
	/// The context for composite runtime fields. The script returns a map of values.
	/// </para>
	/// </summary>
	[System.Runtime.Serialization.EnumMember(Value = "composite_field")]
	CompositeField,
	/// <summary>
	/// <para>
	/// The context for date fields. <c>emit</c> takes a long value and the script returns a sorted list of dates.
	/// </para>
	/// </summary>
	[System.Runtime.Serialization.EnumMember(Value = "date_field")]
	DateField,
	/// <summary>
	/// <para>
	/// The context for double numeric fields. The script returns a sorted list of double values.
	/// </para>
	/// </summary>
	[System.Runtime.Serialization.EnumMember(Value = "double_field")]
	DoubleField,
	/// <summary>
	/// <para>
	/// Treats scripts as if they were run inside a script query.
	/// </para>
	/// </summary>
	[System.Runtime.Serialization.EnumMember(Value = "filter")]
	Filter,
	/// <summary>
	/// <para>
	/// The context for geo-point fields. <c>emit</c> takes two double parameters, the latitude and longitude values, and the script returns an object in GeoJSON format containing the coordinates for the geo point.
	/// </para>
	/// </summary>
	[System.Runtime.Serialization.EnumMember(Value = "geo_point_field")]
	GeoPointField,
	/// <summary>
	/// <para>
	/// The context for <c>ip</c> fields. The script returns a sorted list of IP addresses.
	/// </para>
	/// </summary>
	[System.Runtime.Serialization.EnumMember(Value = "ip_field")]
	IpField,
	/// <summary>
	/// <para>
	/// The context for keyword fields. The script returns a sorted list of string values.
	/// </para>
	/// </summary>
	[System.Runtime.Serialization.EnumMember(Value = "keyword_field")]
	KeywordField,
	/// <summary>
	/// <para>
	/// The context for long numeric fields. The script returns a sorted list of long values.
	/// </para>
	/// </summary>
	[System.Runtime.Serialization.EnumMember(Value = "long_field")]
	LongField,
	/// <summary>
	/// <para>
	/// The default context if no other context is specified.
	/// </para>
	/// </summary>
	[System.Runtime.Serialization.EnumMember(Value = "painless_test")]
	PainlessTest,
	/// <summary>
	/// <para>
	/// Treats scripts as if they were run inside a <c>script_score</c> function in a <c>function_score</c> query.
	/// </para>
	/// </summary>
	[System.Runtime.Serialization.EnumMember(Value = "score")]
	Score
}