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

using Elastic.Clients.Elasticsearch.Fluent;
using Elastic.Clients.Elasticsearch.Serialization;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch.QueryDsl;

internal sealed partial class PrefixQueryConverter : JsonConverter<PrefixQuery>
{
	public override PrefixQuery Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		if (reader.TokenType != JsonTokenType.StartObject)
			throw new JsonException("Unexpected JSON detected.");
		reader.Read();
		var fieldName = reader.GetString();
		reader.Read();
		var variant = new PrefixQuery(fieldName);
		while (reader.Read() && reader.TokenType != JsonTokenType.EndObject)
		{
			if (reader.TokenType == JsonTokenType.PropertyName)
			{
				var property = reader.GetString();
				if (property == "boost")
				{
					variant.Boost = JsonSerializer.Deserialize<float?>(ref reader, options);
					continue;
				}

				if (property == "case_insensitive")
				{
					variant.CaseInsensitive = JsonSerializer.Deserialize<bool?>(ref reader, options);
					continue;
				}

				if (property == "_name")
				{
					variant.QueryName = JsonSerializer.Deserialize<string?>(ref reader, options);
					continue;
				}

				if (property == "rewrite")
				{
					variant.Rewrite = JsonSerializer.Deserialize<string?>(ref reader, options);
					continue;
				}

				if (property == "value")
				{
					variant.Value = JsonSerializer.Deserialize<string>(ref reader, options);
					continue;
				}
			}
		}

		reader.Read();
		return variant;
	}

	public override void Write(Utf8JsonWriter writer, PrefixQuery value, JsonSerializerOptions options)
	{
		if (value.Field is null)
			throw new JsonException("Unable to serialize PrefixQuery because the `Field` property is not set. Field name queries must include a valid field name.");
		if (!options.TryGetClientSettings(out var settings))
			throw new JsonException("Unable to retrieve client settings required to infer field.");
		writer.WriteStartObject();
		writer.WritePropertyName(settings.Inferrer.Field(value.Field));
		writer.WriteStartObject();
		if (value.Boost.HasValue)
		{
			writer.WritePropertyName("boost");
			writer.WriteNumberValue(value.Boost.Value);
		}

		if (value.CaseInsensitive.HasValue)
		{
			writer.WritePropertyName("case_insensitive");
			writer.WriteBooleanValue(value.CaseInsensitive.Value);
		}

		if (!string.IsNullOrEmpty(value.QueryName))
		{
			writer.WritePropertyName("_name");
			writer.WriteStringValue(value.QueryName);
		}

		if (!string.IsNullOrEmpty(value.Rewrite))
		{
			writer.WritePropertyName("rewrite");
			writer.WriteStringValue(value.Rewrite);
		}

		writer.WritePropertyName("value");
		writer.WriteStringValue(value.Value);
		writer.WriteEndObject();
		writer.WriteEndObject();
	}
}

[JsonConverter(typeof(PrefixQueryConverter))]
public sealed partial class PrefixQuery
{
	public PrefixQuery(Elastic.Clients.Elasticsearch.Field field)
	{
		if (field is null)
			throw new ArgumentNullException(nameof(field));
		Field = field;
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

	/// <summary>
	/// <para>
	/// Allows ASCII case insensitive matching of the value with the indexed field values when set to <c>true</c>.
	/// Default is <c>false</c> which means the case sensitivity of matching depends on the underlying field’s mapping.
	/// </para>
	/// </summary>
	public bool? CaseInsensitive { get; set; }
	public Elastic.Clients.Elasticsearch.Field Field { get; set; }
	public string? QueryName { get; set; }

	/// <summary>
	/// <para>
	/// Method used to rewrite the query.
	/// </para>
	/// </summary>
	public string? Rewrite { get; set; }

	/// <summary>
	/// <para>
	/// Beginning characters of terms you wish to find in the provided field.
	/// </para>
	/// </summary>
	public string Value { get; set; }

	public static implicit operator Elastic.Clients.Elasticsearch.QueryDsl.Query(PrefixQuery prefixQuery) => Elastic.Clients.Elasticsearch.QueryDsl.Query.Prefix(prefixQuery);
}

public sealed partial class PrefixQueryDescriptor<TDocument> : SerializableDescriptor<PrefixQueryDescriptor<TDocument>>
{
	internal PrefixQueryDescriptor(Action<PrefixQueryDescriptor<TDocument>> configure) => configure.Invoke(this);

	public PrefixQueryDescriptor() : base()
	{
	}

	private float? BoostValue { get; set; }
	private bool? CaseInsensitiveValue { get; set; }
	private Elastic.Clients.Elasticsearch.Field FieldValue { get; set; }
	private string? QueryNameValue { get; set; }
	private string? RewriteValue { get; set; }
	private string ValueValue { get; set; }

	/// <summary>
	/// <para>
	/// Floating point number used to decrease or increase the relevance scores of the query.
	/// Boost values are relative to the default value of 1.0.
	/// A boost value between 0 and 1.0 decreases the relevance score.
	/// A value greater than 1.0 increases the relevance score.
	/// </para>
	/// </summary>
	public PrefixQueryDescriptor<TDocument> Boost(float? boost)
	{
		BoostValue = boost;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Allows ASCII case insensitive matching of the value with the indexed field values when set to <c>true</c>.
	/// Default is <c>false</c> which means the case sensitivity of matching depends on the underlying field’s mapping.
	/// </para>
	/// </summary>
	public PrefixQueryDescriptor<TDocument> CaseInsensitive(bool? caseInsensitive = true)
	{
		CaseInsensitiveValue = caseInsensitive;
		return Self;
	}

	public PrefixQueryDescriptor<TDocument> Field(Elastic.Clients.Elasticsearch.Field field)
	{
		FieldValue = field;
		return Self;
	}

	public PrefixQueryDescriptor<TDocument> Field<TValue>(Expression<Func<TDocument, TValue>> field)
	{
		FieldValue = field;
		return Self;
	}

	public PrefixQueryDescriptor<TDocument> Field(Expression<Func<TDocument, object>> field)
	{
		FieldValue = field;
		return Self;
	}

	public PrefixQueryDescriptor<TDocument> QueryName(string? queryName)
	{
		QueryNameValue = queryName;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Method used to rewrite the query.
	/// </para>
	/// </summary>
	public PrefixQueryDescriptor<TDocument> Rewrite(string? rewrite)
	{
		RewriteValue = rewrite;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Beginning characters of terms you wish to find in the provided field.
	/// </para>
	/// </summary>
	public PrefixQueryDescriptor<TDocument> Value(string value)
	{
		ValueValue = value;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		if (FieldValue is null)
			throw new JsonException("Unable to serialize field name query descriptor with a null field. Ensure you use a suitable descriptor constructor or call the Field method, passing a non-null value for the field argument.");
		writer.WriteStartObject();
		writer.WritePropertyName(settings.Inferrer.Field(FieldValue));
		writer.WriteStartObject();
		if (BoostValue.HasValue)
		{
			writer.WritePropertyName("boost");
			writer.WriteNumberValue(BoostValue.Value);
		}

		if (CaseInsensitiveValue.HasValue)
		{
			writer.WritePropertyName("case_insensitive");
			writer.WriteBooleanValue(CaseInsensitiveValue.Value);
		}

		if (!string.IsNullOrEmpty(QueryNameValue))
		{
			writer.WritePropertyName("_name");
			writer.WriteStringValue(QueryNameValue);
		}

		if (!string.IsNullOrEmpty(RewriteValue))
		{
			writer.WritePropertyName("rewrite");
			writer.WriteStringValue(RewriteValue);
		}

		writer.WritePropertyName("value");
		writer.WriteStringValue(ValueValue);
		writer.WriteEndObject();
		writer.WriteEndObject();
	}
}

public sealed partial class PrefixQueryDescriptor : SerializableDescriptor<PrefixQueryDescriptor>
{
	internal PrefixQueryDescriptor(Action<PrefixQueryDescriptor> configure) => configure.Invoke(this);

	public PrefixQueryDescriptor() : base()
	{
	}

	private float? BoostValue { get; set; }
	private bool? CaseInsensitiveValue { get; set; }
	private Elastic.Clients.Elasticsearch.Field FieldValue { get; set; }
	private string? QueryNameValue { get; set; }
	private string? RewriteValue { get; set; }
	private string ValueValue { get; set; }

	/// <summary>
	/// <para>
	/// Floating point number used to decrease or increase the relevance scores of the query.
	/// Boost values are relative to the default value of 1.0.
	/// A boost value between 0 and 1.0 decreases the relevance score.
	/// A value greater than 1.0 increases the relevance score.
	/// </para>
	/// </summary>
	public PrefixQueryDescriptor Boost(float? boost)
	{
		BoostValue = boost;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Allows ASCII case insensitive matching of the value with the indexed field values when set to <c>true</c>.
	/// Default is <c>false</c> which means the case sensitivity of matching depends on the underlying field’s mapping.
	/// </para>
	/// </summary>
	public PrefixQueryDescriptor CaseInsensitive(bool? caseInsensitive = true)
	{
		CaseInsensitiveValue = caseInsensitive;
		return Self;
	}

	public PrefixQueryDescriptor Field(Elastic.Clients.Elasticsearch.Field field)
	{
		FieldValue = field;
		return Self;
	}

	public PrefixQueryDescriptor Field<TDocument, TValue>(Expression<Func<TDocument, TValue>> field)
	{
		FieldValue = field;
		return Self;
	}

	public PrefixQueryDescriptor Field<TDocument>(Expression<Func<TDocument, object>> field)
	{
		FieldValue = field;
		return Self;
	}

	public PrefixQueryDescriptor QueryName(string? queryName)
	{
		QueryNameValue = queryName;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Method used to rewrite the query.
	/// </para>
	/// </summary>
	public PrefixQueryDescriptor Rewrite(string? rewrite)
	{
		RewriteValue = rewrite;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Beginning characters of terms you wish to find in the provided field.
	/// </para>
	/// </summary>
	public PrefixQueryDescriptor Value(string value)
	{
		ValueValue = value;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		if (FieldValue is null)
			throw new JsonException("Unable to serialize field name query descriptor with a null field. Ensure you use a suitable descriptor constructor or call the Field method, passing a non-null value for the field argument.");
		writer.WriteStartObject();
		writer.WritePropertyName(settings.Inferrer.Field(FieldValue));
		writer.WriteStartObject();
		if (BoostValue.HasValue)
		{
			writer.WritePropertyName("boost");
			writer.WriteNumberValue(BoostValue.Value);
		}

		if (CaseInsensitiveValue.HasValue)
		{
			writer.WritePropertyName("case_insensitive");
			writer.WriteBooleanValue(CaseInsensitiveValue.Value);
		}

		if (!string.IsNullOrEmpty(QueryNameValue))
		{
			writer.WritePropertyName("_name");
			writer.WriteStringValue(QueryNameValue);
		}

		if (!string.IsNullOrEmpty(RewriteValue))
		{
			writer.WritePropertyName("rewrite");
			writer.WriteStringValue(RewriteValue);
		}

		writer.WritePropertyName("value");
		writer.WriteStringValue(ValueValue);
		writer.WriteEndObject();
		writer.WriteEndObject();
	}
}