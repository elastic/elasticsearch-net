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

using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Runtime.Serialization;
using Elastic.Transport;
using Elastic.Clients.Elasticsearch.Serialization;

#nullable restore
namespace Elastic.Clients.Elasticsearch.QueryDsl;
[JsonConverter(typeof(ChildScoreModeConverter))]
public enum ChildScoreMode
{
	[EnumMember(Value = "sum")]
	Sum,
	[EnumMember(Value = "none")]
	None,
	[EnumMember(Value = "min")]
	Min,
	[EnumMember(Value = "max")]
	Max,
	[EnumMember(Value = "avg")]
	Avg
}

internal sealed class ChildScoreModeConverter : JsonConverter<ChildScoreMode>
{
	public override ChildScoreMode Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		var enumString = reader.GetString();
		switch (enumString)
		{
			case "sum":
				return ChildScoreMode.Sum;
			case "none":
				return ChildScoreMode.None;
			case "min":
				return ChildScoreMode.Min;
			case "max":
				return ChildScoreMode.Max;
			case "avg":
				return ChildScoreMode.Avg;
		}

		ThrowHelper.ThrowJsonException();
		return default;
	}

	public override void Write(Utf8JsonWriter writer, ChildScoreMode value, JsonSerializerOptions options)
	{
		switch (value)
		{
			case ChildScoreMode.Sum:
				writer.WriteStringValue("sum");
				return;
			case ChildScoreMode.None:
				writer.WriteStringValue("none");
				return;
			case ChildScoreMode.Min:
				writer.WriteStringValue("min");
				return;
			case ChildScoreMode.Max:
				writer.WriteStringValue("max");
				return;
			case ChildScoreMode.Avg:
				writer.WriteStringValue("avg");
				return;
		}

		writer.WriteNullValue();
	}
}

[JsonConverter(typeof(CombinedFieldsOperatorConverter))]
public enum CombinedFieldsOperator
{
	[EnumMember(Value = "or")]
	Or,
	[EnumMember(Value = "and")]
	And
}

internal sealed class CombinedFieldsOperatorConverter : JsonConverter<CombinedFieldsOperator>
{
	public override CombinedFieldsOperator Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		var enumString = reader.GetString();
		switch (enumString)
		{
			case "or":
				return CombinedFieldsOperator.Or;
			case "and":
				return CombinedFieldsOperator.And;
		}

		ThrowHelper.ThrowJsonException();
		return default;
	}

	public override void Write(Utf8JsonWriter writer, CombinedFieldsOperator value, JsonSerializerOptions options)
	{
		switch (value)
		{
			case CombinedFieldsOperator.Or:
				writer.WriteStringValue("or");
				return;
			case CombinedFieldsOperator.And:
				writer.WriteStringValue("and");
				return;
		}

		writer.WriteNullValue();
	}
}

[JsonConverter(typeof(CombinedFieldsZeroTermsConverter))]
public enum CombinedFieldsZeroTerms
{
	[EnumMember(Value = "none")]
	None,
	[EnumMember(Value = "all")]
	All
}

internal sealed class CombinedFieldsZeroTermsConverter : JsonConverter<CombinedFieldsZeroTerms>
{
	public override CombinedFieldsZeroTerms Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		var enumString = reader.GetString();
		switch (enumString)
		{
			case "none":
				return CombinedFieldsZeroTerms.None;
			case "all":
				return CombinedFieldsZeroTerms.All;
		}

		ThrowHelper.ThrowJsonException();
		return default;
	}

	public override void Write(Utf8JsonWriter writer, CombinedFieldsZeroTerms value, JsonSerializerOptions options)
	{
		switch (value)
		{
			case CombinedFieldsZeroTerms.None:
				writer.WriteStringValue("none");
				return;
			case CombinedFieldsZeroTerms.All:
				writer.WriteStringValue("all");
				return;
		}

		writer.WriteNullValue();
	}
}

[JsonConverter(typeof(FieldValueFactorModifierConverter))]
public enum FieldValueFactorModifier
{
	[EnumMember(Value = "square")]
	Square,
	[EnumMember(Value = "sqrt")]
	Sqrt,
	[EnumMember(Value = "reciprocal")]
	Reciprocal,
	[EnumMember(Value = "none")]
	None,
	[EnumMember(Value = "log2p")]
	Log2p,
	[EnumMember(Value = "log1p")]
	Log1p,
	[EnumMember(Value = "log")]
	Log,
	[EnumMember(Value = "ln2p")]
	Ln2p,
	[EnumMember(Value = "ln1p")]
	Ln1p,
	[EnumMember(Value = "ln")]
	Ln
}

internal sealed class FieldValueFactorModifierConverter : JsonConverter<FieldValueFactorModifier>
{
	public override FieldValueFactorModifier Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		var enumString = reader.GetString();
		switch (enumString)
		{
			case "square":
				return FieldValueFactorModifier.Square;
			case "sqrt":
				return FieldValueFactorModifier.Sqrt;
			case "reciprocal":
				return FieldValueFactorModifier.Reciprocal;
			case "none":
				return FieldValueFactorModifier.None;
			case "log2p":
				return FieldValueFactorModifier.Log2p;
			case "log1p":
				return FieldValueFactorModifier.Log1p;
			case "log":
				return FieldValueFactorModifier.Log;
			case "ln2p":
				return FieldValueFactorModifier.Ln2p;
			case "ln1p":
				return FieldValueFactorModifier.Ln1p;
			case "ln":
				return FieldValueFactorModifier.Ln;
		}

		ThrowHelper.ThrowJsonException();
		return default;
	}

	public override void Write(Utf8JsonWriter writer, FieldValueFactorModifier value, JsonSerializerOptions options)
	{
		switch (value)
		{
			case FieldValueFactorModifier.Square:
				writer.WriteStringValue("square");
				return;
			case FieldValueFactorModifier.Sqrt:
				writer.WriteStringValue("sqrt");
				return;
			case FieldValueFactorModifier.Reciprocal:
				writer.WriteStringValue("reciprocal");
				return;
			case FieldValueFactorModifier.None:
				writer.WriteStringValue("none");
				return;
			case FieldValueFactorModifier.Log2p:
				writer.WriteStringValue("log2p");
				return;
			case FieldValueFactorModifier.Log1p:
				writer.WriteStringValue("log1p");
				return;
			case FieldValueFactorModifier.Log:
				writer.WriteStringValue("log");
				return;
			case FieldValueFactorModifier.Ln2p:
				writer.WriteStringValue("ln2p");
				return;
			case FieldValueFactorModifier.Ln1p:
				writer.WriteStringValue("ln1p");
				return;
			case FieldValueFactorModifier.Ln:
				writer.WriteStringValue("ln");
				return;
		}

		writer.WriteNullValue();
	}
}

[JsonConverter(typeof(FunctionBoostModeConverter))]
public enum FunctionBoostMode
{
	[EnumMember(Value = "sum")]
	Sum,
	[EnumMember(Value = "replace")]
	Replace,
	[EnumMember(Value = "multiply")]
	Multiply,
	[EnumMember(Value = "min")]
	Min,
	[EnumMember(Value = "max")]
	Max,
	[EnumMember(Value = "avg")]
	Avg
}

internal sealed class FunctionBoostModeConverter : JsonConverter<FunctionBoostMode>
{
	public override FunctionBoostMode Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		var enumString = reader.GetString();
		switch (enumString)
		{
			case "sum":
				return FunctionBoostMode.Sum;
			case "replace":
				return FunctionBoostMode.Replace;
			case "multiply":
				return FunctionBoostMode.Multiply;
			case "min":
				return FunctionBoostMode.Min;
			case "max":
				return FunctionBoostMode.Max;
			case "avg":
				return FunctionBoostMode.Avg;
		}

		ThrowHelper.ThrowJsonException();
		return default;
	}

	public override void Write(Utf8JsonWriter writer, FunctionBoostMode value, JsonSerializerOptions options)
	{
		switch (value)
		{
			case FunctionBoostMode.Sum:
				writer.WriteStringValue("sum");
				return;
			case FunctionBoostMode.Replace:
				writer.WriteStringValue("replace");
				return;
			case FunctionBoostMode.Multiply:
				writer.WriteStringValue("multiply");
				return;
			case FunctionBoostMode.Min:
				writer.WriteStringValue("min");
				return;
			case FunctionBoostMode.Max:
				writer.WriteStringValue("max");
				return;
			case FunctionBoostMode.Avg:
				writer.WriteStringValue("avg");
				return;
		}

		writer.WriteNullValue();
	}
}

[JsonConverter(typeof(FunctionScoreModeConverter))]
public enum FunctionScoreMode
{
	[EnumMember(Value = "sum")]
	Sum,
	[EnumMember(Value = "multiply")]
	Multiply,
	[EnumMember(Value = "min")]
	Min,
	[EnumMember(Value = "max")]
	Max,
	[EnumMember(Value = "first")]
	First,
	[EnumMember(Value = "avg")]
	Avg
}

internal sealed class FunctionScoreModeConverter : JsonConverter<FunctionScoreMode>
{
	public override FunctionScoreMode Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		var enumString = reader.GetString();
		switch (enumString)
		{
			case "sum":
				return FunctionScoreMode.Sum;
			case "multiply":
				return FunctionScoreMode.Multiply;
			case "min":
				return FunctionScoreMode.Min;
			case "max":
				return FunctionScoreMode.Max;
			case "first":
				return FunctionScoreMode.First;
			case "avg":
				return FunctionScoreMode.Avg;
		}

		ThrowHelper.ThrowJsonException();
		return default;
	}

	public override void Write(Utf8JsonWriter writer, FunctionScoreMode value, JsonSerializerOptions options)
	{
		switch (value)
		{
			case FunctionScoreMode.Sum:
				writer.WriteStringValue("sum");
				return;
			case FunctionScoreMode.Multiply:
				writer.WriteStringValue("multiply");
				return;
			case FunctionScoreMode.Min:
				writer.WriteStringValue("min");
				return;
			case FunctionScoreMode.Max:
				writer.WriteStringValue("max");
				return;
			case FunctionScoreMode.First:
				writer.WriteStringValue("first");
				return;
			case FunctionScoreMode.Avg:
				writer.WriteStringValue("avg");
				return;
		}

		writer.WriteNullValue();
	}
}

[JsonConverter(typeof(OperatorConverter))]
public enum Operator
{
	[EnumMember(Value = "or")]
	Or,
	[EnumMember(Value = "and")]
	And
}

internal sealed class OperatorConverter : JsonConverter<Operator>
{
	public override Operator Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		var enumString = reader.GetString();
		switch (enumString)
		{
			case "or":
				return Operator.Or;
			case "and":
				return Operator.And;
		}

		ThrowHelper.ThrowJsonException();
		return default;
	}

	public override void Write(Utf8JsonWriter writer, Operator value, JsonSerializerOptions options)
	{
		switch (value)
		{
			case Operator.Or:
				writer.WriteStringValue("or");
				return;
			case Operator.And:
				writer.WriteStringValue("and");
				return;
		}

		writer.WriteNullValue();
	}
}

[JsonConverter(typeof(SimpleQueryStringFlagConverter))]
public enum SimpleQueryStringFlag
{
	[EnumMember(Value = "WHITESPACE")]
	Whitespace,
	[EnumMember(Value = "SLOP")]
	Slop,
	[EnumMember(Value = "PREFIX")]
	Prefix,
	[EnumMember(Value = "PRECEDENCE")]
	Precedence,
	[EnumMember(Value = "PHRASE")]
	Phrase,
	[EnumMember(Value = "OR")]
	Or,
	[EnumMember(Value = "NOT")]
	Not,
	[EnumMember(Value = "NONE")]
	None,
	[EnumMember(Value = "NEAR")]
	Near,
	[EnumMember(Value = "FUZZY")]
	Fuzzy,
	[EnumMember(Value = "ESCAPE")]
	Escape,
	[EnumMember(Value = "AND")]
	And,
	[EnumMember(Value = "ALL")]
	All
}

internal sealed class SimpleQueryStringFlagConverter : JsonConverter<SimpleQueryStringFlag>
{
	public override SimpleQueryStringFlag Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		var enumString = reader.GetString();
		switch (enumString)
		{
			case "WHITESPACE":
				return SimpleQueryStringFlag.Whitespace;
			case "SLOP":
				return SimpleQueryStringFlag.Slop;
			case "PREFIX":
				return SimpleQueryStringFlag.Prefix;
			case "PRECEDENCE":
				return SimpleQueryStringFlag.Precedence;
			case "PHRASE":
				return SimpleQueryStringFlag.Phrase;
			case "OR":
				return SimpleQueryStringFlag.Or;
			case "NOT":
				return SimpleQueryStringFlag.Not;
			case "NONE":
				return SimpleQueryStringFlag.None;
			case "NEAR":
				return SimpleQueryStringFlag.Near;
			case "FUZZY":
				return SimpleQueryStringFlag.Fuzzy;
			case "ESCAPE":
				return SimpleQueryStringFlag.Escape;
			case "AND":
				return SimpleQueryStringFlag.And;
			case "ALL":
				return SimpleQueryStringFlag.All;
		}

		ThrowHelper.ThrowJsonException();
		return default;
	}

	public override void Write(Utf8JsonWriter writer, SimpleQueryStringFlag value, JsonSerializerOptions options)
	{
		switch (value)
		{
			case SimpleQueryStringFlag.Whitespace:
				writer.WriteStringValue("WHITESPACE");
				return;
			case SimpleQueryStringFlag.Slop:
				writer.WriteStringValue("SLOP");
				return;
			case SimpleQueryStringFlag.Prefix:
				writer.WriteStringValue("PREFIX");
				return;
			case SimpleQueryStringFlag.Precedence:
				writer.WriteStringValue("PRECEDENCE");
				return;
			case SimpleQueryStringFlag.Phrase:
				writer.WriteStringValue("PHRASE");
				return;
			case SimpleQueryStringFlag.Or:
				writer.WriteStringValue("OR");
				return;
			case SimpleQueryStringFlag.Not:
				writer.WriteStringValue("NOT");
				return;
			case SimpleQueryStringFlag.None:
				writer.WriteStringValue("NONE");
				return;
			case SimpleQueryStringFlag.Near:
				writer.WriteStringValue("NEAR");
				return;
			case SimpleQueryStringFlag.Fuzzy:
				writer.WriteStringValue("FUZZY");
				return;
			case SimpleQueryStringFlag.Escape:
				writer.WriteStringValue("ESCAPE");
				return;
			case SimpleQueryStringFlag.And:
				writer.WriteStringValue("AND");
				return;
			case SimpleQueryStringFlag.All:
				writer.WriteStringValue("ALL");
				return;
		}

		writer.WriteNullValue();
	}
}

[JsonConverter(typeof(TextQueryTypeConverter))]
public enum TextQueryType
{
	[EnumMember(Value = "phrase_prefix")]
	PhrasePrefix,
	[EnumMember(Value = "phrase")]
	Phrase,
	[EnumMember(Value = "most_fields")]
	MostFields,
	[EnumMember(Value = "cross_fields")]
	CrossFields,
	[EnumMember(Value = "bool_prefix")]
	BoolPrefix,
	[EnumMember(Value = "best_fields")]
	BestFields
}

internal sealed class TextQueryTypeConverter : JsonConverter<TextQueryType>
{
	public override TextQueryType Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		var enumString = reader.GetString();
		switch (enumString)
		{
			case "phrase_prefix":
				return TextQueryType.PhrasePrefix;
			case "phrase":
				return TextQueryType.Phrase;
			case "most_fields":
				return TextQueryType.MostFields;
			case "cross_fields":
				return TextQueryType.CrossFields;
			case "bool_prefix":
				return TextQueryType.BoolPrefix;
			case "best_fields":
				return TextQueryType.BestFields;
		}

		ThrowHelper.ThrowJsonException();
		return default;
	}

	public override void Write(Utf8JsonWriter writer, TextQueryType value, JsonSerializerOptions options)
	{
		switch (value)
		{
			case TextQueryType.PhrasePrefix:
				writer.WriteStringValue("phrase_prefix");
				return;
			case TextQueryType.Phrase:
				writer.WriteStringValue("phrase");
				return;
			case TextQueryType.MostFields:
				writer.WriteStringValue("most_fields");
				return;
			case TextQueryType.CrossFields:
				writer.WriteStringValue("cross_fields");
				return;
			case TextQueryType.BoolPrefix:
				writer.WriteStringValue("bool_prefix");
				return;
			case TextQueryType.BestFields:
				writer.WriteStringValue("best_fields");
				return;
		}

		writer.WriteNullValue();
	}
}

[JsonConverter(typeof(ZeroTermsQueryConverter))]
public enum ZeroTermsQuery
{
	[EnumMember(Value = "none")]
	None,
	[EnumMember(Value = "all")]
	All
}

internal sealed class ZeroTermsQueryConverter : JsonConverter<ZeroTermsQuery>
{
	public override ZeroTermsQuery Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		var enumString = reader.GetString();
		switch (enumString)
		{
			case "none":
				return ZeroTermsQuery.None;
			case "all":
				return ZeroTermsQuery.All;
		}

		ThrowHelper.ThrowJsonException();
		return default;
	}

	public override void Write(Utf8JsonWriter writer, ZeroTermsQuery value, JsonSerializerOptions options)
	{
		switch (value)
		{
			case ZeroTermsQuery.None:
				writer.WriteStringValue("none");
				return;
			case ZeroTermsQuery.All:
				writer.WriteStringValue("all");
				return;
		}

		writer.WriteNullValue();
	}
}