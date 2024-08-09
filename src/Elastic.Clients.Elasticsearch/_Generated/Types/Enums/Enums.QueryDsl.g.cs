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
	/// <summary>
	/// <para>
	/// No documents are returned if the analyzer removes all tokens.
	/// </para>
	/// </summary>
	[EnumMember(Value = "none")]
	None,
	/// <summary>
	/// <para>
	/// Returns all documents, similar to a <c>match_all</c> query.
	/// </para>
	/// </summary>
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
	/// <summary>
	/// <para>
	/// Square the field value (multiply it by itself).
	/// </para>
	/// </summary>
	[EnumMember(Value = "square")]
	Square,
	/// <summary>
	/// <para>
	/// Take the square root of the field value.
	/// </para>
	/// </summary>
	[EnumMember(Value = "sqrt")]
	Sqrt,
	/// <summary>
	/// <para>
	/// Reciprocate the field value, same as <c>1/x</c> where <c>x</c> is the field’s value.
	/// </para>
	/// </summary>
	[EnumMember(Value = "reciprocal")]
	Reciprocal,
	/// <summary>
	/// <para>
	/// Do not apply any multiplier to the field value.
	/// </para>
	/// </summary>
	[EnumMember(Value = "none")]
	None,
	/// <summary>
	/// <para>
	/// Add 2 to the field value and take the common logarithm.
	/// </para>
	/// </summary>
	[EnumMember(Value = "log2p")]
	Log2p,
	/// <summary>
	/// <para>
	/// Add 1 to the field value and take the common logarithm.
	/// </para>
	/// </summary>
	[EnumMember(Value = "log1p")]
	Log1p,
	/// <summary>
	/// <para>
	/// Take the common logarithm of the field value.
	/// Because this function will return a negative value and cause an error if used on values between 0 and 1, it is recommended to use <c>log1p</c> instead.
	/// </para>
	/// </summary>
	[EnumMember(Value = "log")]
	Log,
	/// <summary>
	/// <para>
	/// Add 2 to the field value and take the natural logarithm.
	/// </para>
	/// </summary>
	[EnumMember(Value = "ln2p")]
	Ln2p,
	/// <summary>
	/// <para>
	/// Add 1 to the field value and take the natural logarithm.
	/// </para>
	/// </summary>
	[EnumMember(Value = "ln1p")]
	Ln1p,
	/// <summary>
	/// <para>
	/// Take the natural logarithm of the field value.
	/// Because this function will return a negative value and cause an error if used on values between 0 and 1, it is recommended to use <c>ln1p</c> instead.
	/// </para>
	/// </summary>
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
	/// <summary>
	/// <para>
	/// Query score and function score are added
	/// </para>
	/// </summary>
	[EnumMember(Value = "sum")]
	Sum,
	/// <summary>
	/// <para>
	/// Only the function score is used.
	/// The query score is ignored.
	/// </para>
	/// </summary>
	[EnumMember(Value = "replace")]
	Replace,
	/// <summary>
	/// <para>
	/// Query score and function score are multiplied
	/// </para>
	/// </summary>
	[EnumMember(Value = "multiply")]
	Multiply,
	/// <summary>
	/// <para>
	/// Min of query score and function score
	/// </para>
	/// </summary>
	[EnumMember(Value = "min")]
	Min,
	/// <summary>
	/// <para>
	/// Max of query score and function score
	/// </para>
	/// </summary>
	[EnumMember(Value = "max")]
	Max,
	/// <summary>
	/// <para>
	/// Query score and function score are averaged
	/// </para>
	/// </summary>
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
	/// <summary>
	/// <para>
	/// Scores are summed.
	/// </para>
	/// </summary>
	[EnumMember(Value = "sum")]
	Sum,
	/// <summary>
	/// <para>
	/// Scores are multiplied.
	/// </para>
	/// </summary>
	[EnumMember(Value = "multiply")]
	Multiply,
	/// <summary>
	/// <para>
	/// Minimum score is used.
	/// </para>
	/// </summary>
	[EnumMember(Value = "min")]
	Min,
	/// <summary>
	/// <para>
	/// Maximum score is used.
	/// </para>
	/// </summary>
	[EnumMember(Value = "max")]
	Max,
	/// <summary>
	/// <para>
	/// The first function that has a matching filter is applied.
	/// </para>
	/// </summary>
	[EnumMember(Value = "first")]
	First,
	/// <summary>
	/// <para>
	/// Scores are averaged.
	/// </para>
	/// </summary>
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

[JsonConverter(typeof(GeoValidationMethodConverter))]
public enum GeoValidationMethod
{
	[EnumMember(Value = "strict")]
	Strict,
	/// <summary>
	/// <para>
	/// Accept geo points with invalid latitude or longitude.
	/// </para>
	/// </summary>
	[EnumMember(Value = "ignore_malformed")]
	IgnoreMalformed,
	/// <summary>
	/// <para>
	/// Accept geo points with invalid latitude or longitude and additionally try and infer correct coordinates.
	/// </para>
	/// </summary>
	[EnumMember(Value = "coerce")]
	Coerce
}

internal sealed class GeoValidationMethodConverter : JsonConverter<GeoValidationMethod>
{
	public override GeoValidationMethod Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		var enumString = reader.GetString();
		switch (enumString)
		{
			case "strict":
				return GeoValidationMethod.Strict;
			case "ignore_malformed":
				return GeoValidationMethod.IgnoreMalformed;
			case "coerce":
				return GeoValidationMethod.Coerce;
		}

		ThrowHelper.ThrowJsonException();
		return default;
	}

	public override void Write(Utf8JsonWriter writer, GeoValidationMethod value, JsonSerializerOptions options)
	{
		switch (value)
		{
			case GeoValidationMethod.Strict:
				writer.WriteStringValue("strict");
				return;
			case GeoValidationMethod.IgnoreMalformed:
				writer.WriteStringValue("ignore_malformed");
				return;
			case GeoValidationMethod.Coerce:
				writer.WriteStringValue("coerce");
				return;
		}

		writer.WriteNullValue();
	}
}

[JsonConverter(typeof(MultiValueModeConverter))]
public enum MultiValueMode
{
	/// <summary>
	/// <para>
	/// Distance is the sum of all distances.
	/// </para>
	/// </summary>
	[EnumMember(Value = "sum")]
	Sum,
	/// <summary>
	/// <para>
	/// Distance is the minimum distance.
	/// </para>
	/// </summary>
	[EnumMember(Value = "min")]
	Min,
	/// <summary>
	/// <para>
	/// Distance is the maximum distance.
	/// </para>
	/// </summary>
	[EnumMember(Value = "max")]
	Max,
	/// <summary>
	/// <para>
	/// Distance is the average distance.
	/// </para>
	/// </summary>
	[EnumMember(Value = "avg")]
	Avg
}

internal sealed class MultiValueModeConverter : JsonConverter<MultiValueMode>
{
	public override MultiValueMode Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		var enumString = reader.GetString();
		switch (enumString)
		{
			case "sum":
				return MultiValueMode.Sum;
			case "min":
				return MultiValueMode.Min;
			case "max":
				return MultiValueMode.Max;
			case "avg":
				return MultiValueMode.Avg;
		}

		ThrowHelper.ThrowJsonException();
		return default;
	}

	public override void Write(Utf8JsonWriter writer, MultiValueMode value, JsonSerializerOptions options)
	{
		switch (value)
		{
			case MultiValueMode.Sum:
				writer.WriteStringValue("sum");
				return;
			case MultiValueMode.Min:
				writer.WriteStringValue("min");
				return;
			case MultiValueMode.Max:
				writer.WriteStringValue("max");
				return;
			case MultiValueMode.Avg:
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
			case "OR":
				return Operator.Or;
			case "and":
			case "AND":
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

[JsonConverter(typeof(RangeRelationConverter))]
public enum RangeRelation
{
	/// <summary>
	/// <para>
	/// Matches documents with a range field value entirely within the query’s range.
	/// </para>
	/// </summary>
	[EnumMember(Value = "within")]
	Within,
	/// <summary>
	/// <para>
	/// Matches documents with a range field value that intersects the query’s range.
	/// </para>
	/// </summary>
	[EnumMember(Value = "intersects")]
	Intersects,
	/// <summary>
	/// <para>
	/// Matches documents with a range field value that entirely contains the query’s range.
	/// </para>
	/// </summary>
	[EnumMember(Value = "contains")]
	Contains
}

internal sealed class RangeRelationConverter : JsonConverter<RangeRelation>
{
	public override RangeRelation Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		var enumString = reader.GetString();
		switch (enumString)
		{
			case "within":
				return RangeRelation.Within;
			case "intersects":
				return RangeRelation.Intersects;
			case "contains":
				return RangeRelation.Contains;
		}

		ThrowHelper.ThrowJsonException();
		return default;
	}

	public override void Write(Utf8JsonWriter writer, RangeRelation value, JsonSerializerOptions options)
	{
		switch (value)
		{
			case RangeRelation.Within:
				writer.WriteStringValue("within");
				return;
			case RangeRelation.Intersects:
				writer.WriteStringValue("intersects");
				return;
			case RangeRelation.Contains:
				writer.WriteStringValue("contains");
				return;
		}

		writer.WriteNullValue();
	}
}

[JsonConverter(typeof(SimpleQueryStringFlagConverter))]
[Flags]
public enum SimpleQueryStringFlag
{
	/// <summary>
	/// <para>
	/// Enables whitespace as split characters.
	/// </para>
	/// </summary>
	[EnumMember(Value = "WHITESPACE")]
	Whitespace = 1 << 0,
	/// <summary>
	/// <para>
	/// Enables the <c>~N</c> operator, after a phrase where <c>N</c> is maximum number of positions allowed between matching tokens.
	/// Synonymous to <c>NEAR</c>.
	/// </para>
	/// </summary>
	[EnumMember(Value = "SLOP")]
	Slop = 1 << 1,
	/// <summary>
	/// <para>
	/// Enables the <c>*</c> prefix operator.
	/// </para>
	/// </summary>
	[EnumMember(Value = "PREFIX")]
	Prefix = 1 << 2,
	/// <summary>
	/// <para>
	/// Enables the <c>(</c> and <c>)</c> operators to control operator precedence.
	/// </para>
	/// </summary>
	[EnumMember(Value = "PRECEDENCE")]
	Precedence = 1 << 3,
	/// <summary>
	/// <para>
	/// Enables the <c>"</c> quotes operator used to search for phrases.
	/// </para>
	/// </summary>
	[EnumMember(Value = "PHRASE")]
	Phrase = 1 << 4,
	/// <summary>
	/// <para>
	/// Enables the <c>\|</c> OR operator.
	/// </para>
	/// </summary>
	[EnumMember(Value = "OR")]
	Or = 1 << 5,
	/// <summary>
	/// <para>
	/// Enables the <c>-</c> NOT operator.
	/// </para>
	/// </summary>
	[EnumMember(Value = "NOT")]
	Not = 1 << 6,
	/// <summary>
	/// <para>
	/// Disables all operators.
	/// </para>
	/// </summary>
	[EnumMember(Value = "NONE")]
	None = 1 << 7,
	/// <summary>
	/// <para>
	/// Enables the <c>~N</c> operator, after a phrase where <c>N</c> is the maximum number of positions allowed between matching tokens.
	/// Synonymous to <c>SLOP</c>.
	/// </para>
	/// </summary>
	[EnumMember(Value = "NEAR")]
	Near = 1 << 8,
	/// <summary>
	/// <para>
	/// Enables the <c>~N</c> operator after a word, where <c>N</c> is an integer denoting the allowed edit distance for matching.
	/// </para>
	/// </summary>
	[EnumMember(Value = "FUZZY")]
	Fuzzy = 1 << 9,
	/// <summary>
	/// <para>
	/// Enables <c>\</c> as an escape character.
	/// </para>
	/// </summary>
	[EnumMember(Value = "ESCAPE")]
	Escape = 1 << 10,
	/// <summary>
	/// <para>
	/// Enables the <c>+</c> AND operator.
	/// </para>
	/// </summary>
	[EnumMember(Value = "AND")]
	And = 1 << 11,
	/// <summary>
	/// <para>
	/// Enables all optional operators.
	/// </para>
	/// </summary>
	[EnumMember(Value = "ALL")]
	All = 1 << 12
}

internal sealed class SimpleQueryStringFlagConverter : JsonConverter<SimpleQueryStringFlag>
{
	public override SimpleQueryStringFlag Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		var s = reader.GetString();
		if (string.IsNullOrEmpty(s))
		{
			return default;
		}

		var flags = s.Split('|');
		var result = default(SimpleQueryStringFlag);
		foreach (var flag in flags)
		{
			result |= flag switch
			{
				"WHITESPACE" => SimpleQueryStringFlag.Whitespace,
				"SLOP" => SimpleQueryStringFlag.Slop,
				"PREFIX" => SimpleQueryStringFlag.Prefix,
				"PRECEDENCE" => SimpleQueryStringFlag.Precedence,
				"PHRASE" => SimpleQueryStringFlag.Phrase,
				"OR" => SimpleQueryStringFlag.Or,
				"NOT" => SimpleQueryStringFlag.Not,
				"NONE" => SimpleQueryStringFlag.None,
				"NEAR" => SimpleQueryStringFlag.Near,
				"FUZZY" => SimpleQueryStringFlag.Fuzzy,
				"ESCAPE" => SimpleQueryStringFlag.Escape,
				"AND" => SimpleQueryStringFlag.And,
				"ALL" => SimpleQueryStringFlag.All,
				_ => throw new JsonException($"Invalid flag value '{flag}' for type '{typeToConvert.Name}'.")
			};
		}

		return result;
	}

	public override void Write(Utf8JsonWriter writer, SimpleQueryStringFlag value, JsonSerializerOptions options)
	{
		if (value == default)
		{
			writer.WriteStringValue(string.Empty);
			return;
		}

		var sb = new StringBuilder();
		if (value.HasFlag(SimpleQueryStringFlag.Whitespace))
			sb.Append("WHITESPACE|");
		if (value.HasFlag(SimpleQueryStringFlag.Slop))
			sb.Append("SLOP|");
		if (value.HasFlag(SimpleQueryStringFlag.Prefix))
			sb.Append("PREFIX|");
		if (value.HasFlag(SimpleQueryStringFlag.Precedence))
			sb.Append("PRECEDENCE|");
		if (value.HasFlag(SimpleQueryStringFlag.Phrase))
			sb.Append("PHRASE|");
		if (value.HasFlag(SimpleQueryStringFlag.Or))
			sb.Append("OR|");
		if (value.HasFlag(SimpleQueryStringFlag.Not))
			sb.Append("NOT|");
		if (value.HasFlag(SimpleQueryStringFlag.None))
			sb.Append("NONE|");
		if (value.HasFlag(SimpleQueryStringFlag.Near))
			sb.Append("NEAR|");
		if (value.HasFlag(SimpleQueryStringFlag.Fuzzy))
			sb.Append("FUZZY|");
		if (value.HasFlag(SimpleQueryStringFlag.Escape))
			sb.Append("ESCAPE|");
		if (value.HasFlag(SimpleQueryStringFlag.And))
			sb.Append("AND|");
		if (value.HasFlag(SimpleQueryStringFlag.All))
			sb.Append("ALL|");
		if (sb.Length == 0)
		{
			writer.WriteStringValue(string.Empty);
			return;
		}

		sb.Remove(sb.Length - 1, 1);
		writer.WriteStringValue(sb.ToString());
	}
}

[JsonConverter(typeof(TextQueryTypeConverter))]
public enum TextQueryType
{
	/// <summary>
	/// <para>
	/// Runs a <c>match_phrase_prefix</c> query on each field and uses the <c>_score</c> from the best field.
	/// </para>
	/// </summary>
	[EnumMember(Value = "phrase_prefix")]
	PhrasePrefix,
	/// <summary>
	/// <para>
	/// Runs a <c>match_phrase</c> query on each field and uses the <c>_score</c> from the best field.
	/// </para>
	/// </summary>
	[EnumMember(Value = "phrase")]
	Phrase,
	/// <summary>
	/// <para>
	/// Finds documents that match any field and combines the <c>_score</c> from each field.
	/// </para>
	/// </summary>
	[EnumMember(Value = "most_fields")]
	MostFields,
	/// <summary>
	/// <para>
	/// Treats fields with the same analyzer as though they were one big field.
	/// Looks for each word in any field.
	/// </para>
	/// </summary>
	[EnumMember(Value = "cross_fields")]
	CrossFields,
	/// <summary>
	/// <para>
	/// Creates a <c>match_bool_prefix</c> query on each field and combines the <c>_score</c> from each field.
	/// </para>
	/// </summary>
	[EnumMember(Value = "bool_prefix")]
	BoolPrefix,
	/// <summary>
	/// <para>
	/// Finds documents that match any field, but uses the <c>_score</c> from the best field.
	/// </para>
	/// </summary>
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
	/// <summary>
	/// <para>
	/// No documents are returned if the <c>analyzer</c> removes all tokens.
	/// </para>
	/// </summary>
	[EnumMember(Value = "none")]
	None,
	/// <summary>
	/// <para>
	/// Returns all documents, similar to a <c>match_all</c> query.
	/// </para>
	/// </summary>
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