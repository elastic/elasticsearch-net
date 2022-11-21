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

using Elastic.Clients.Elasticsearch.Fluent;
using Elastic.Clients.Elasticsearch.Serialization;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;

#nullable restore
namespace Elastic.Clients.Elasticsearch.QueryDsl;
internal sealed class DateRangeQueryConverter : JsonConverter<DateRangeQuery>
{
	public override DateRangeQuery Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		if (reader.TokenType != JsonTokenType.StartObject)
			throw new JsonException("Unexpected JSON detected.");
		reader.Read();
		var fieldName = reader.GetString();
		reader.Read();
		var variant = new DateRangeQuery(fieldName);
		while (reader.Read() && reader.TokenType != JsonTokenType.EndObject)
		{
			if (reader.TokenType == JsonTokenType.PropertyName)
			{
				var property = reader.GetString();
				if (property == "_name")
				{
					variant.QueryName = JsonSerializer.Deserialize<string?>(ref reader, options);
					continue;
				}

				if (property == "boost")
				{
					variant.Boost = JsonSerializer.Deserialize<float?>(ref reader, options);
					continue;
				}

				if (property == "format")
				{
					variant.Format = JsonSerializer.Deserialize<string?>(ref reader, options);
					continue;
				}

				if (property == "from")
				{
					variant.From = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.DateMath?>(ref reader, options);
					continue;
				}

				if (property == "gt")
				{
					variant.Gt = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.DateMath?>(ref reader, options);
					continue;
				}

				if (property == "gte")
				{
					variant.Gte = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.DateMath?>(ref reader, options);
					continue;
				}

				if (property == "lt")
				{
					variant.Lt = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.DateMath?>(ref reader, options);
					continue;
				}

				if (property == "lte")
				{
					variant.Lte = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.DateMath?>(ref reader, options);
					continue;
				}

				if (property == "relation")
				{
					variant.Relation = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.QueryDsl.RangeRelation?>(ref reader, options);
					continue;
				}

				if (property == "time_zone")
				{
					variant.TimeZone = JsonSerializer.Deserialize<string?>(ref reader, options);
					continue;
				}

				if (property == "to")
				{
					variant.To = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.DateMath?>(ref reader, options);
					continue;
				}
			}
		}

		reader.Read();
		return variant;
	}

	public override void Write(Utf8JsonWriter writer, DateRangeQuery value, JsonSerializerOptions options)
	{
		if (value.Field is null)
			throw new JsonException("Unable to serialize DateRangeQuery because the `Field` property is not set. Field name queries must include a valid field name.");
		if (options.TryGetClientSettings(out var settings))
		{
			writer.WriteStartObject();
			writer.WritePropertyName(settings.Inferrer.Field(value.Field));
			writer.WriteStartObject();
			if (!string.IsNullOrEmpty(value.QueryName))
			{
				writer.WritePropertyName("_name");
				writer.WriteStringValue(value.QueryName);
			}

			if (value.Boost.HasValue)
			{
				writer.WritePropertyName("boost");
				writer.WriteNumberValue(value.Boost.Value);
			}

			if (value.Format is not null)
			{
				writer.WritePropertyName("format");
				JsonSerializer.Serialize(writer, value.Format, options);
			}

			if (value.From is not null)
			{
				writer.WritePropertyName("from");
				JsonSerializer.Serialize(writer, value.From, options);
			}

			if (value.Gt is not null)
			{
				writer.WritePropertyName("gt");
				JsonSerializer.Serialize(writer, value.Gt, options);
			}

			if (value.Gte is not null)
			{
				writer.WritePropertyName("gte");
				JsonSerializer.Serialize(writer, value.Gte, options);
			}

			if (value.Lt is not null)
			{
				writer.WritePropertyName("lt");
				JsonSerializer.Serialize(writer, value.Lt, options);
			}

			if (value.Lte is not null)
			{
				writer.WritePropertyName("lte");
				JsonSerializer.Serialize(writer, value.Lte, options);
			}

			if (value.Relation is not null)
			{
				writer.WritePropertyName("relation");
				JsonSerializer.Serialize(writer, value.Relation, options);
			}

			if (value.TimeZone is not null)
			{
				writer.WritePropertyName("time_zone");
				JsonSerializer.Serialize(writer, value.TimeZone, options);
			}

			if (value.To is not null)
			{
				writer.WritePropertyName("to");
				JsonSerializer.Serialize(writer, value.To, options);
			}

			writer.WriteEndObject();
			writer.WriteEndObject();
			return;
		}

		throw new JsonException("Unable to retrieve client settings required to infer field.");
	}
}

[JsonConverter(typeof(DateRangeQueryConverter))]
public sealed partial class DateRangeQuery : SearchQuery
{
	public DateRangeQuery(Field field)
	{
		if (field is null)
			throw new ArgumentNullException(nameof(field));
		Field = field;
	}

	public string? QueryName { get; set; }

	public float? Boost { get; set; }

	public string? Format { get; set; }

	public Elastic.Clients.Elasticsearch.DateMath? From { get; set; }

	public Elastic.Clients.Elasticsearch.DateMath? Gt { get; set; }

	public Elastic.Clients.Elasticsearch.DateMath? Gte { get; set; }

	public Elastic.Clients.Elasticsearch.DateMath? Lt { get; set; }

	public Elastic.Clients.Elasticsearch.DateMath? Lte { get; set; }

	public Elastic.Clients.Elasticsearch.QueryDsl.RangeRelation? Relation { get; set; }

	public string? TimeZone { get; set; }

	public Elastic.Clients.Elasticsearch.DateMath? To { get; set; }

	public Elastic.Clients.Elasticsearch.Field Field { get; set; }

	public static implicit operator Query(DateRangeQuery dateRangeQuery) => QueryDsl.Query.Range(new RangeQuery(dateRangeQuery));
}

public sealed partial class DateRangeQueryDescriptor<TDocument> : SerializableDescriptor<DateRangeQueryDescriptor<TDocument>>
{
	internal DateRangeQueryDescriptor(Action<DateRangeQueryDescriptor<TDocument>> configure) => configure.Invoke(this);
	internal DateRangeQueryDescriptor() : base()
	{
	}

	public DateRangeQueryDescriptor(Field field)
	{
		if (field is null)
			throw new ArgumentNullException(nameof(field));
		FieldValue = field;
	}

	public DateRangeQueryDescriptor(Expression<Func<TDocument, object>> field)
	{
		if (field is null)
			throw new ArgumentNullException(nameof(field));
		FieldValue = field;
	}

	private string? QueryNameValue { get; set; }

	private float? BoostValue { get; set; }

	private Elastic.Clients.Elasticsearch.Field FieldValue { get; set; }

	private string? FormatValue { get; set; }

	private Elastic.Clients.Elasticsearch.DateMath? FromValue { get; set; }

	private Elastic.Clients.Elasticsearch.DateMath? GtValue { get; set; }

	private Elastic.Clients.Elasticsearch.DateMath? GteValue { get; set; }

	private Elastic.Clients.Elasticsearch.DateMath? LtValue { get; set; }

	private Elastic.Clients.Elasticsearch.DateMath? LteValue { get; set; }

	private Elastic.Clients.Elasticsearch.QueryDsl.RangeRelation? RelationValue { get; set; }

	private string? TimeZoneValue { get; set; }

	private Elastic.Clients.Elasticsearch.DateMath? ToValue { get; set; }

	public DateRangeQueryDescriptor<TDocument> QueryName(string? queryName)
	{
		QueryNameValue = queryName;
		return Self;
	}

	public DateRangeQueryDescriptor<TDocument> Boost(float? boost)
	{
		BoostValue = boost;
		return Self;
	}

	public DateRangeQueryDescriptor<TDocument> Field(Elastic.Clients.Elasticsearch.Field field)
	{
		FieldValue = field;
		return Self;
	}

	public DateRangeQueryDescriptor<TDocument> Field<TValue>(Expression<Func<TDocument, TValue>> field)
	{
		FieldValue = field;
		return Self;
	}

	public DateRangeQueryDescriptor<TDocument> Format(string? format)
	{
		FormatValue = format;
		return Self;
	}

	public DateRangeQueryDescriptor<TDocument> From(Elastic.Clients.Elasticsearch.DateMath? from)
	{
		FromValue = from;
		return Self;
	}

	public DateRangeQueryDescriptor<TDocument> Gt(Elastic.Clients.Elasticsearch.DateMath? gt)
	{
		GtValue = gt;
		return Self;
	}

	public DateRangeQueryDescriptor<TDocument> Gte(Elastic.Clients.Elasticsearch.DateMath? gte)
	{
		GteValue = gte;
		return Self;
	}

	public DateRangeQueryDescriptor<TDocument> Lt(Elastic.Clients.Elasticsearch.DateMath? lt)
	{
		LtValue = lt;
		return Self;
	}

	public DateRangeQueryDescriptor<TDocument> Lte(Elastic.Clients.Elasticsearch.DateMath? lte)
	{
		LteValue = lte;
		return Self;
	}

	public DateRangeQueryDescriptor<TDocument> Relation(Elastic.Clients.Elasticsearch.QueryDsl.RangeRelation? relation)
	{
		RelationValue = relation;
		return Self;
	}

	public DateRangeQueryDescriptor<TDocument> TimeZone(string? timeZone)
	{
		TimeZoneValue = timeZone;
		return Self;
	}

	public DateRangeQueryDescriptor<TDocument> To(Elastic.Clients.Elasticsearch.DateMath? to)
	{
		ToValue = to;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		if (FieldValue is null)
			throw new JsonException("Unable to serialize field name query descriptor with a null field. Ensure you use a suitable descriptor constructor or call the Field method, passing a non-null value for the field argument.");
		writer.WriteStartObject();
		writer.WritePropertyName(settings.Inferrer.Field(FieldValue));
		writer.WriteStartObject();
		if (!string.IsNullOrEmpty(QueryNameValue))
		{
			writer.WritePropertyName("_name");
			writer.WriteStringValue(QueryNameValue);
		}

		if (BoostValue.HasValue)
		{
			writer.WritePropertyName("boost");
			writer.WriteNumberValue(BoostValue.Value);
		}

		if (FormatValue is not null)
		{
			writer.WritePropertyName("format");
			JsonSerializer.Serialize(writer, FormatValue, options);
		}

		if (FromValue is not null)
		{
			writer.WritePropertyName("from");
			JsonSerializer.Serialize(writer, FromValue, options);
		}

		if (GtValue is not null)
		{
			writer.WritePropertyName("gt");
			JsonSerializer.Serialize(writer, GtValue, options);
		}

		if (GteValue is not null)
		{
			writer.WritePropertyName("gte");
			JsonSerializer.Serialize(writer, GteValue, options);
		}

		if (LtValue is not null)
		{
			writer.WritePropertyName("lt");
			JsonSerializer.Serialize(writer, LtValue, options);
		}

		if (LteValue is not null)
		{
			writer.WritePropertyName("lte");
			JsonSerializer.Serialize(writer, LteValue, options);
		}

		if (RelationValue is not null)
		{
			writer.WritePropertyName("relation");
			JsonSerializer.Serialize(writer, RelationValue, options);
		}

		if (TimeZoneValue is not null)
		{
			writer.WritePropertyName("time_zone");
			JsonSerializer.Serialize(writer, TimeZoneValue, options);
		}

		if (ToValue is not null)
		{
			writer.WritePropertyName("to");
			JsonSerializer.Serialize(writer, ToValue, options);
		}

		writer.WriteEndObject();
		writer.WriteEndObject();
	}
}

public sealed partial class DateRangeQueryDescriptor : SerializableDescriptor<DateRangeQueryDescriptor>
{
	internal DateRangeQueryDescriptor(Action<DateRangeQueryDescriptor> configure) => configure.Invoke(this);
	internal DateRangeQueryDescriptor() : base()
	{
	}

	public DateRangeQueryDescriptor(Field field)
	{
		if (field is null)
			throw new ArgumentNullException(nameof(field));
		FieldValue = field;
	}

	private string? QueryNameValue { get; set; }

	private float? BoostValue { get; set; }

	private Elastic.Clients.Elasticsearch.Field FieldValue { get; set; }

	private string? FormatValue { get; set; }

	private Elastic.Clients.Elasticsearch.DateMath? FromValue { get; set; }

	private Elastic.Clients.Elasticsearch.DateMath? GtValue { get; set; }

	private Elastic.Clients.Elasticsearch.DateMath? GteValue { get; set; }

	private Elastic.Clients.Elasticsearch.DateMath? LtValue { get; set; }

	private Elastic.Clients.Elasticsearch.DateMath? LteValue { get; set; }

	private Elastic.Clients.Elasticsearch.QueryDsl.RangeRelation? RelationValue { get; set; }

	private string? TimeZoneValue { get; set; }

	private Elastic.Clients.Elasticsearch.DateMath? ToValue { get; set; }

	public DateRangeQueryDescriptor QueryName(string? queryName)
	{
		QueryNameValue = queryName;
		return Self;
	}

	public DateRangeQueryDescriptor Boost(float? boost)
	{
		BoostValue = boost;
		return Self;
	}

	public DateRangeQueryDescriptor Field(Elastic.Clients.Elasticsearch.Field field)
	{
		FieldValue = field;
		return Self;
	}

	public DateRangeQueryDescriptor Field<TDocument, TValue>(Expression<Func<TDocument, TValue>> field)
	{
		FieldValue = field;
		return Self;
	}

	public DateRangeQueryDescriptor Field<TDocument>(Expression<Func<TDocument, object>> field)
	{
		FieldValue = field;
		return Self;
	}

	public DateRangeQueryDescriptor Format(string? format)
	{
		FormatValue = format;
		return Self;
	}

	public DateRangeQueryDescriptor From(Elastic.Clients.Elasticsearch.DateMath? from)
	{
		FromValue = from;
		return Self;
	}

	public DateRangeQueryDescriptor Gt(Elastic.Clients.Elasticsearch.DateMath? gt)
	{
		GtValue = gt;
		return Self;
	}

	public DateRangeQueryDescriptor Gte(Elastic.Clients.Elasticsearch.DateMath? gte)
	{
		GteValue = gte;
		return Self;
	}

	public DateRangeQueryDescriptor Lt(Elastic.Clients.Elasticsearch.DateMath? lt)
	{
		LtValue = lt;
		return Self;
	}

	public DateRangeQueryDescriptor Lte(Elastic.Clients.Elasticsearch.DateMath? lte)
	{
		LteValue = lte;
		return Self;
	}

	public DateRangeQueryDescriptor Relation(Elastic.Clients.Elasticsearch.QueryDsl.RangeRelation? relation)
	{
		RelationValue = relation;
		return Self;
	}

	public DateRangeQueryDescriptor TimeZone(string? timeZone)
	{
		TimeZoneValue = timeZone;
		return Self;
	}

	public DateRangeQueryDescriptor To(Elastic.Clients.Elasticsearch.DateMath? to)
	{
		ToValue = to;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		if (FieldValue is null)
			throw new JsonException("Unable to serialize field name query descriptor with a null field. Ensure you use a suitable descriptor constructor or call the Field method, passing a non-null value for the field argument.");
		writer.WriteStartObject();
		writer.WritePropertyName(settings.Inferrer.Field(FieldValue));
		writer.WriteStartObject();
		if (!string.IsNullOrEmpty(QueryNameValue))
		{
			writer.WritePropertyName("_name");
			writer.WriteStringValue(QueryNameValue);
		}

		if (BoostValue.HasValue)
		{
			writer.WritePropertyName("boost");
			writer.WriteNumberValue(BoostValue.Value);
		}

		if (FormatValue is not null)
		{
			writer.WritePropertyName("format");
			JsonSerializer.Serialize(writer, FormatValue, options);
		}

		if (FromValue is not null)
		{
			writer.WritePropertyName("from");
			JsonSerializer.Serialize(writer, FromValue, options);
		}

		if (GtValue is not null)
		{
			writer.WritePropertyName("gt");
			JsonSerializer.Serialize(writer, GtValue, options);
		}

		if (GteValue is not null)
		{
			writer.WritePropertyName("gte");
			JsonSerializer.Serialize(writer, GteValue, options);
		}

		if (LtValue is not null)
		{
			writer.WritePropertyName("lt");
			JsonSerializer.Serialize(writer, LtValue, options);
		}

		if (LteValue is not null)
		{
			writer.WritePropertyName("lte");
			JsonSerializer.Serialize(writer, LteValue, options);
		}

		if (RelationValue is not null)
		{
			writer.WritePropertyName("relation");
			JsonSerializer.Serialize(writer, RelationValue, options);
		}

		if (TimeZoneValue is not null)
		{
			writer.WritePropertyName("time_zone");
			JsonSerializer.Serialize(writer, TimeZoneValue, options);
		}

		if (ToValue is not null)
		{
			writer.WritePropertyName("to");
			JsonSerializer.Serialize(writer, ToValue, options);
		}

		writer.WriteEndObject();
		writer.WriteEndObject();
	}
}