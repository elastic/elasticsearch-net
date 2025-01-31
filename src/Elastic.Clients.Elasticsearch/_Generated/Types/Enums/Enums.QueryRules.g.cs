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

namespace Elastic.Clients.Elasticsearch.QueryRules;

[JsonConverter(typeof(QueryRuleCriteriaTypeConverter))]
public enum QueryRuleCriteriaType
{
	[EnumMember(Value = "suffix")]
	Suffix,
	[EnumMember(Value = "prefix")]
	Prefix,
	[EnumMember(Value = "lte")]
	Lte,
	[EnumMember(Value = "lt")]
	Lt,
	[EnumMember(Value = "gte")]
	Gte,
	[EnumMember(Value = "gt")]
	Gt,
	[EnumMember(Value = "global")]
	Global,
	[EnumMember(Value = "fuzzy")]
	Fuzzy,
	[EnumMember(Value = "exact_fuzzy")]
	ExactFuzzy,
	[EnumMember(Value = "exact")]
	Exact,
	[EnumMember(Value = "contains")]
	Contains,
	[EnumMember(Value = "always")]
	Always
}

internal sealed partial class QueryRuleCriteriaTypeConverter : System.Text.Json.Serialization.JsonConverter<QueryRuleCriteriaType>
{
	private static readonly System.Text.Json.JsonEncodedText MemberSuffix = System.Text.Json.JsonEncodedText.Encode("suffix");
	private static readonly System.Text.Json.JsonEncodedText MemberPrefix = System.Text.Json.JsonEncodedText.Encode("prefix");
	private static readonly System.Text.Json.JsonEncodedText MemberLte = System.Text.Json.JsonEncodedText.Encode("lte");
	private static readonly System.Text.Json.JsonEncodedText MemberLt = System.Text.Json.JsonEncodedText.Encode("lt");
	private static readonly System.Text.Json.JsonEncodedText MemberGte = System.Text.Json.JsonEncodedText.Encode("gte");
	private static readonly System.Text.Json.JsonEncodedText MemberGt = System.Text.Json.JsonEncodedText.Encode("gt");
	private static readonly System.Text.Json.JsonEncodedText MemberGlobal = System.Text.Json.JsonEncodedText.Encode("global");
	private static readonly System.Text.Json.JsonEncodedText MemberFuzzy = System.Text.Json.JsonEncodedText.Encode("fuzzy");
	private static readonly System.Text.Json.JsonEncodedText MemberExactFuzzy = System.Text.Json.JsonEncodedText.Encode("exact_fuzzy");
	private static readonly System.Text.Json.JsonEncodedText MemberExact = System.Text.Json.JsonEncodedText.Encode("exact");
	private static readonly System.Text.Json.JsonEncodedText MemberContains = System.Text.Json.JsonEncodedText.Encode("contains");
	private static readonly System.Text.Json.JsonEncodedText MemberAlways = System.Text.Json.JsonEncodedText.Encode("always");

	public override QueryRuleCriteriaType Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.String);
		if (reader.ValueTextEquals(MemberSuffix))
		{
			return QueryRuleCriteriaType.Suffix;
		}

		if (reader.ValueTextEquals(MemberPrefix))
		{
			return QueryRuleCriteriaType.Prefix;
		}

		if (reader.ValueTextEquals(MemberLte))
		{
			return QueryRuleCriteriaType.Lte;
		}

		if (reader.ValueTextEquals(MemberLt))
		{
			return QueryRuleCriteriaType.Lt;
		}

		if (reader.ValueTextEquals(MemberGte))
		{
			return QueryRuleCriteriaType.Gte;
		}

		if (reader.ValueTextEquals(MemberGt))
		{
			return QueryRuleCriteriaType.Gt;
		}

		if (reader.ValueTextEquals(MemberGlobal))
		{
			return QueryRuleCriteriaType.Global;
		}

		if (reader.ValueTextEquals(MemberFuzzy))
		{
			return QueryRuleCriteriaType.Fuzzy;
		}

		if (reader.ValueTextEquals(MemberExactFuzzy))
		{
			return QueryRuleCriteriaType.ExactFuzzy;
		}

		if (reader.ValueTextEquals(MemberExact))
		{
			return QueryRuleCriteriaType.Exact;
		}

		if (reader.ValueTextEquals(MemberContains))
		{
			return QueryRuleCriteriaType.Contains;
		}

		if (reader.ValueTextEquals(MemberAlways))
		{
			return QueryRuleCriteriaType.Always;
		}

		throw new System.Text.Json.JsonException($"Unknown value '{reader.GetString()}' for enum '{nameof(QueryRuleCriteriaType)}'.");
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, QueryRuleCriteriaType value, System.Text.Json.JsonSerializerOptions options)
	{
		switch (value)
		{
			case QueryRuleCriteriaType.Suffix:
				writer.WriteStringValue(MemberSuffix);
				break;
			case QueryRuleCriteriaType.Prefix:
				writer.WriteStringValue(MemberPrefix);
				break;
			case QueryRuleCriteriaType.Lte:
				writer.WriteStringValue(MemberLte);
				break;
			case QueryRuleCriteriaType.Lt:
				writer.WriteStringValue(MemberLt);
				break;
			case QueryRuleCriteriaType.Gte:
				writer.WriteStringValue(MemberGte);
				break;
			case QueryRuleCriteriaType.Gt:
				writer.WriteStringValue(MemberGt);
				break;
			case QueryRuleCriteriaType.Global:
				writer.WriteStringValue(MemberGlobal);
				break;
			case QueryRuleCriteriaType.Fuzzy:
				writer.WriteStringValue(MemberFuzzy);
				break;
			case QueryRuleCriteriaType.ExactFuzzy:
				writer.WriteStringValue(MemberExactFuzzy);
				break;
			case QueryRuleCriteriaType.Exact:
				writer.WriteStringValue(MemberExact);
				break;
			case QueryRuleCriteriaType.Contains:
				writer.WriteStringValue(MemberContains);
				break;
			case QueryRuleCriteriaType.Always:
				writer.WriteStringValue(MemberAlways);
				break;
			default:
				throw new System.Text.Json.JsonException($"Invalid value '{value}' for enum '{nameof(QueryRuleCriteriaType)}'.");
		}
	}
}

[JsonConverter(typeof(QueryRuleTypeConverter))]
public enum QueryRuleType
{
	[EnumMember(Value = "pinned")]
	Pinned,
	[EnumMember(Value = "exclude")]
	Exclude
}

internal sealed partial class QueryRuleTypeConverter : System.Text.Json.Serialization.JsonConverter<QueryRuleType>
{
	private static readonly System.Text.Json.JsonEncodedText MemberPinned = System.Text.Json.JsonEncodedText.Encode("pinned");
	private static readonly System.Text.Json.JsonEncodedText MemberExclude = System.Text.Json.JsonEncodedText.Encode("exclude");

	public override QueryRuleType Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.String);
		if (reader.ValueTextEquals(MemberPinned))
		{
			return QueryRuleType.Pinned;
		}

		if (reader.ValueTextEquals(MemberExclude))
		{
			return QueryRuleType.Exclude;
		}

		throw new System.Text.Json.JsonException($"Unknown value '{reader.GetString()}' for enum '{nameof(QueryRuleType)}'.");
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, QueryRuleType value, System.Text.Json.JsonSerializerOptions options)
	{
		switch (value)
		{
			case QueryRuleType.Pinned:
				writer.WriteStringValue(MemberPinned);
				break;
			case QueryRuleType.Exclude:
				writer.WriteStringValue(MemberExclude);
				break;
			default:
				throw new System.Text.Json.JsonException($"Invalid value '{value}' for enum '{nameof(QueryRuleType)}'.");
		}
	}
}