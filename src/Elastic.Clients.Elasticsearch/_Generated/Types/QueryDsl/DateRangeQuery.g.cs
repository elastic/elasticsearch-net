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

namespace Elastic.Clients.Elasticsearch.QueryDsl;

internal sealed partial class DateRangeQueryConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.QueryDsl.DateRangeQuery>
{
	private static readonly System.Text.Json.JsonEncodedText PropBoost = System.Text.Json.JsonEncodedText.Encode("boost");
	private static readonly System.Text.Json.JsonEncodedText PropFormat = System.Text.Json.JsonEncodedText.Encode("format");
	private static readonly System.Text.Json.JsonEncodedText PropFrom = System.Text.Json.JsonEncodedText.Encode("from");
	private static readonly System.Text.Json.JsonEncodedText PropGt = System.Text.Json.JsonEncodedText.Encode("gt");
	private static readonly System.Text.Json.JsonEncodedText PropGte = System.Text.Json.JsonEncodedText.Encode("gte");
	private static readonly System.Text.Json.JsonEncodedText PropLt = System.Text.Json.JsonEncodedText.Encode("lt");
	private static readonly System.Text.Json.JsonEncodedText PropLte = System.Text.Json.JsonEncodedText.Encode("lte");
	private static readonly System.Text.Json.JsonEncodedText PropQueryName = System.Text.Json.JsonEncodedText.Encode("_name");
	private static readonly System.Text.Json.JsonEncodedText PropRelation = System.Text.Json.JsonEncodedText.Encode("relation");
	private static readonly System.Text.Json.JsonEncodedText PropTimeZone = System.Text.Json.JsonEncodedText.Encode("time_zone");
	private static readonly System.Text.Json.JsonEncodedText PropTo = System.Text.Json.JsonEncodedText.Encode("to");

	public override Elastic.Clients.Elasticsearch.QueryDsl.DateRangeQuery Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<Elastic.Clients.Elasticsearch.Field> propField = default;
		reader.Read();
		propField.ReadPropertyName(ref reader, options, null);
		reader.Read();
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<float?> propBoost = default;
		LocalJsonValue<string?> propFormat = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.DateMath?> propFrom = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.DateMath?> propGt = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.DateMath?> propGte = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.DateMath?> propLt = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.DateMath?> propLte = default;
		LocalJsonValue<string?> propQueryName = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.QueryDsl.RangeRelation?> propRelation = default;
		LocalJsonValue<string?> propTimeZone = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.DateMath?> propTo = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propBoost.TryReadProperty(ref reader, options, PropBoost, null))
			{
				continue;
			}

			if (propFormat.TryReadProperty(ref reader, options, PropFormat, null))
			{
				continue;
			}

			if (propFrom.TryReadProperty(ref reader, options, PropFrom, null))
			{
				continue;
			}

			if (propGt.TryReadProperty(ref reader, options, PropGt, null))
			{
				continue;
			}

			if (propGte.TryReadProperty(ref reader, options, PropGte, null))
			{
				continue;
			}

			if (propLt.TryReadProperty(ref reader, options, PropLt, null))
			{
				continue;
			}

			if (propLte.TryReadProperty(ref reader, options, PropLte, null))
			{
				continue;
			}

			if (propQueryName.TryReadProperty(ref reader, options, PropQueryName, null))
			{
				continue;
			}

			if (propRelation.TryReadProperty(ref reader, options, PropRelation, null))
			{
				continue;
			}

			if (propTimeZone.TryReadProperty(ref reader, options, PropTimeZone, null))
			{
				continue;
			}

			if (propTo.TryReadProperty(ref reader, options, PropTo, null))
			{
				continue;
			}

			if (options.UnmappedMemberHandling is System.Text.Json.Serialization.JsonUnmappedMemberHandling.Skip)
			{
				reader.Skip();
				continue;
			}

			throw new System.Text.Json.JsonException($"Unknown JSON property '{reader.GetString()}' for type '{typeToConvert.Name}'.");
		}

		reader.ValidateToken(System.Text.Json.JsonTokenType.EndObject);
		reader.Read();
		reader.ValidateToken(System.Text.Json.JsonTokenType.EndObject);
		return new Elastic.Clients.Elasticsearch.QueryDsl.DateRangeQuery(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Boost = propBoost.Value,
			Field = propField.Value,
			Format = propFormat.Value,
			From = propFrom.Value,
			Gt = propGt.Value,
			Gte = propGte.Value,
			Lt = propLt.Value,
			Lte = propLte.Value,
			QueryName = propQueryName.Value,
			Relation = propRelation.Value,
			TimeZone = propTimeZone.Value,
			To = propTo.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.QueryDsl.DateRangeQuery value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WritePropertyName(options, value.Field, null);
		writer.WriteStartObject();
		writer.WriteProperty(options, PropBoost, value.Boost, null, null);
		writer.WriteProperty(options, PropFormat, value.Format, null, null);
		writer.WriteProperty(options, PropFrom, value.From, null, null);
		writer.WriteProperty(options, PropGt, value.Gt, null, null);
		writer.WriteProperty(options, PropGte, value.Gte, null, null);
		writer.WriteProperty(options, PropLt, value.Lt, null, null);
		writer.WriteProperty(options, PropLte, value.Lte, null, null);
		writer.WriteProperty(options, PropQueryName, value.QueryName, null, null);
		writer.WriteProperty(options, PropRelation, value.Relation, null, null);
		writer.WriteProperty(options, PropTimeZone, value.TimeZone, null, null);
		writer.WriteProperty(options, PropTo, value.To, null, null);
		writer.WriteEndObject();
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.QueryDsl.DateRangeQueryConverter))]
public sealed partial class DateRangeQuery : Elastic.Clients.Elasticsearch.QueryDsl.IRangeQuery
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public DateRangeQuery(Elastic.Clients.Elasticsearch.Field field)
	{
		Field = field;
	}
#if NET7_0_OR_GREATER
	public DateRangeQuery()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal DateRangeQuery(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	/// <summary>
	/// <para>
	/// Floating point number used to decrease or increase the relevance scores of the query.
	/// Boost values are relative to the default value of 1.0.
	/// A boost value between 0 and 1.0 decreases the relevance score.
	/// A value greater than 1.0 increases the relevance score.
	/// </para>
	/// </summary>
	public float? Boost { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.Field Field { get; set; }

	/// <summary>
	/// <para>
	/// Date format used to convert <c>date</c> values in the query.
	/// </para>
	/// </summary>
	public string? Format { get; set; }
	public Elastic.Clients.Elasticsearch.DateMath? From { get; set; }

	/// <summary>
	/// <para>
	/// Greater than.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.DateMath? Gt { get; set; }

	/// <summary>
	/// <para>
	/// Greater than or equal to.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.DateMath? Gte { get; set; }

	/// <summary>
	/// <para>
	/// Less than.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.DateMath? Lt { get; set; }

	/// <summary>
	/// <para>
	/// Less than or equal to.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.DateMath? Lte { get; set; }
	public string? QueryName { get; set; }

	/// <summary>
	/// <para>
	/// Indicates how the range query matches values for <c>range</c> fields.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.RangeRelation? Relation { get; set; }

	/// <summary>
	/// <para>
	/// Coordinated Universal Time (UTC) offset or IANA time zone used to convert <c>date</c> values in the query to UTC.
	/// </para>
	/// </summary>
	public string? TimeZone { get; set; }
	public Elastic.Clients.Elasticsearch.DateMath? To { get; set; }

	string Elastic.Clients.Elasticsearch.QueryDsl.IRangeQuery.Type => "date";
}

public readonly partial struct DateRangeQueryDescriptor<TDocument>
{
	internal Elastic.Clients.Elasticsearch.QueryDsl.DateRangeQuery Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public DateRangeQueryDescriptor(Elastic.Clients.Elasticsearch.QueryDsl.DateRangeQuery instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public DateRangeQueryDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.QueryDsl.DateRangeQuery(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.QueryDsl.DateRangeQueryDescriptor<TDocument>(Elastic.Clients.Elasticsearch.QueryDsl.DateRangeQuery instance) => new Elastic.Clients.Elasticsearch.QueryDsl.DateRangeQueryDescriptor<TDocument>(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.QueryDsl.DateRangeQuery(Elastic.Clients.Elasticsearch.QueryDsl.DateRangeQueryDescriptor<TDocument> descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// Floating point number used to decrease or increase the relevance scores of the query.
	/// Boost values are relative to the default value of 1.0.
	/// A boost value between 0 and 1.0 decreases the relevance score.
	/// A value greater than 1.0 increases the relevance score.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.DateRangeQueryDescriptor<TDocument> Boost(float? value)
	{
		Instance.Boost = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.QueryDsl.DateRangeQueryDescriptor<TDocument> Field(Elastic.Clients.Elasticsearch.Field value)
	{
		Instance.Field = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.QueryDsl.DateRangeQueryDescriptor<TDocument> Field(System.Linq.Expressions.Expression<System.Func<TDocument, object?>> value)
	{
		Instance.Field = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Date format used to convert <c>date</c> values in the query.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.DateRangeQueryDescriptor<TDocument> Format(string? value)
	{
		Instance.Format = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.QueryDsl.DateRangeQueryDescriptor<TDocument> From(Elastic.Clients.Elasticsearch.DateMath? value)
	{
		Instance.From = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Greater than.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.DateRangeQueryDescriptor<TDocument> Gt(Elastic.Clients.Elasticsearch.DateMath? value)
	{
		Instance.Gt = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Greater than or equal to.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.DateRangeQueryDescriptor<TDocument> Gte(Elastic.Clients.Elasticsearch.DateMath? value)
	{
		Instance.Gte = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Less than.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.DateRangeQueryDescriptor<TDocument> Lt(Elastic.Clients.Elasticsearch.DateMath? value)
	{
		Instance.Lt = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Less than or equal to.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.DateRangeQueryDescriptor<TDocument> Lte(Elastic.Clients.Elasticsearch.DateMath? value)
	{
		Instance.Lte = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.QueryDsl.DateRangeQueryDescriptor<TDocument> QueryName(string? value)
	{
		Instance.QueryName = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Indicates how the range query matches values for <c>range</c> fields.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.DateRangeQueryDescriptor<TDocument> Relation(Elastic.Clients.Elasticsearch.QueryDsl.RangeRelation? value)
	{
		Instance.Relation = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Coordinated Universal Time (UTC) offset or IANA time zone used to convert <c>date</c> values in the query to UTC.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.DateRangeQueryDescriptor<TDocument> TimeZone(string? value)
	{
		Instance.TimeZone = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.QueryDsl.DateRangeQueryDescriptor<TDocument> To(Elastic.Clients.Elasticsearch.DateMath? value)
	{
		Instance.To = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.QueryDsl.DateRangeQuery Build(System.Action<Elastic.Clients.Elasticsearch.QueryDsl.DateRangeQueryDescriptor<TDocument>> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.QueryDsl.DateRangeQueryDescriptor<TDocument>(new Elastic.Clients.Elasticsearch.QueryDsl.DateRangeQuery(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}

public readonly partial struct DateRangeQueryDescriptor
{
	internal Elastic.Clients.Elasticsearch.QueryDsl.DateRangeQuery Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public DateRangeQueryDescriptor(Elastic.Clients.Elasticsearch.QueryDsl.DateRangeQuery instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public DateRangeQueryDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.QueryDsl.DateRangeQuery(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.QueryDsl.DateRangeQueryDescriptor(Elastic.Clients.Elasticsearch.QueryDsl.DateRangeQuery instance) => new Elastic.Clients.Elasticsearch.QueryDsl.DateRangeQueryDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.QueryDsl.DateRangeQuery(Elastic.Clients.Elasticsearch.QueryDsl.DateRangeQueryDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// Floating point number used to decrease or increase the relevance scores of the query.
	/// Boost values are relative to the default value of 1.0.
	/// A boost value between 0 and 1.0 decreases the relevance score.
	/// A value greater than 1.0 increases the relevance score.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.DateRangeQueryDescriptor Boost(float? value)
	{
		Instance.Boost = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.QueryDsl.DateRangeQueryDescriptor Field(Elastic.Clients.Elasticsearch.Field value)
	{
		Instance.Field = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.QueryDsl.DateRangeQueryDescriptor Field<T>(System.Linq.Expressions.Expression<System.Func<T, object?>> value)
	{
		Instance.Field = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Date format used to convert <c>date</c> values in the query.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.DateRangeQueryDescriptor Format(string? value)
	{
		Instance.Format = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.QueryDsl.DateRangeQueryDescriptor From(Elastic.Clients.Elasticsearch.DateMath? value)
	{
		Instance.From = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Greater than.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.DateRangeQueryDescriptor Gt(Elastic.Clients.Elasticsearch.DateMath? value)
	{
		Instance.Gt = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Greater than or equal to.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.DateRangeQueryDescriptor Gte(Elastic.Clients.Elasticsearch.DateMath? value)
	{
		Instance.Gte = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Less than.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.DateRangeQueryDescriptor Lt(Elastic.Clients.Elasticsearch.DateMath? value)
	{
		Instance.Lt = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Less than or equal to.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.DateRangeQueryDescriptor Lte(Elastic.Clients.Elasticsearch.DateMath? value)
	{
		Instance.Lte = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.QueryDsl.DateRangeQueryDescriptor QueryName(string? value)
	{
		Instance.QueryName = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Indicates how the range query matches values for <c>range</c> fields.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.DateRangeQueryDescriptor Relation(Elastic.Clients.Elasticsearch.QueryDsl.RangeRelation? value)
	{
		Instance.Relation = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Coordinated Universal Time (UTC) offset or IANA time zone used to convert <c>date</c> values in the query to UTC.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.DateRangeQueryDescriptor TimeZone(string? value)
	{
		Instance.TimeZone = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.QueryDsl.DateRangeQueryDescriptor To(Elastic.Clients.Elasticsearch.DateMath? value)
	{
		Instance.To = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.QueryDsl.DateRangeQuery Build(System.Action<Elastic.Clients.Elasticsearch.QueryDsl.DateRangeQueryDescriptor> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.QueryDsl.DateRangeQueryDescriptor(new Elastic.Clients.Elasticsearch.QueryDsl.DateRangeQuery(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}