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

internal sealed partial class TermQueryConverter : System.Text.Json.Serialization.JsonConverter<TermQuery>
{
	private static readonly System.Text.Json.JsonEncodedText PropBoost = System.Text.Json.JsonEncodedText.Encode("boost");
	private static readonly System.Text.Json.JsonEncodedText PropCaseInsensitive = System.Text.Json.JsonEncodedText.Encode("case_insensitive");
	private static readonly System.Text.Json.JsonEncodedText PropQueryName = System.Text.Json.JsonEncodedText.Encode("_name");
	private static readonly System.Text.Json.JsonEncodedText PropValue = System.Text.Json.JsonEncodedText.Encode("value");

	public override TermQuery Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<Elastic.Clients.Elasticsearch.Field> propField = default;
		reader.Read();
		propField.ReadPropertyName(ref reader, options);
		reader.Read();
		if (reader.TokenType is not System.Text.Json.JsonTokenType.StartObject)
		{
			var value = reader.ReadValue<Elastic.Clients.Elasticsearch.FieldValue>(options);
			reader.Read();
			return new TermQuery { Value = value };
		}

		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<float?> propBoost = default;
		LocalJsonValue<bool?> propCaseInsensitive = default;
		LocalJsonValue<string?> propQueryName = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.FieldValue> propValue = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propBoost.TryRead(ref reader, options, PropBoost))
			{
				continue;
			}

			if (propCaseInsensitive.TryRead(ref reader, options, PropCaseInsensitive))
			{
				continue;
			}

			if (propQueryName.TryRead(ref reader, options, PropQueryName))
			{
				continue;
			}

			if (propValue.TryRead(ref reader, options, PropValue))
			{
				continue;
			}

			throw new System.Text.Json.JsonException($"Unknown JSON property '{reader.GetString()}' for type '{typeToConvert.Name}'.");
		}

		reader.ValidateToken(System.Text.Json.JsonTokenType.EndObject);
		reader.Read();
		reader.ValidateToken(System.Text.Json.JsonTokenType.EndObject);
		return new TermQuery
		{
			Boost = propBoost.Value
,
			CaseInsensitive = propCaseInsensitive.Value
,
			Field = propField.Value
,
			QueryName = propQueryName.Value
,
			Value = propValue.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, TermQuery value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WritePropertyName(options, value.Field);
		writer.WriteStartObject();
		writer.WriteProperty(options, PropBoost, value.Boost);
		writer.WriteProperty(options, PropCaseInsensitive, value.CaseInsensitive);
		writer.WriteProperty(options, PropQueryName, value.QueryName);
		writer.WriteProperty(options, PropValue, value.Value);
		writer.WriteEndObject();
		writer.WriteEndObject();
	}
}

[JsonConverter(typeof(TermQueryConverter))]
public sealed partial class TermQuery
{
	public TermQuery(Elastic.Clients.Elasticsearch.Field field)
	{
		if (field is null)
			throw new ArgumentNullException(nameof(field));
		Field = field;
	}

	internal TermQuery()
	{
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
	/// When <c>false</c>, the case sensitivity of matching depends on the underlying field’s mapping.
	/// </para>
	/// </summary>
	public bool? CaseInsensitive { get; set; }
	public Elastic.Clients.Elasticsearch.Field Field { get; set; }
	public string? QueryName { get; set; }

	/// <summary>
	/// <para>
	/// Term you wish to find in the provided field.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.FieldValue Value { get; set; }

	public static implicit operator Elastic.Clients.Elasticsearch.QueryDsl.Query(TermQuery termQuery) => Elastic.Clients.Elasticsearch.QueryDsl.Query.Term(termQuery);
	public static implicit operator Elastic.Clients.Elasticsearch.Security.ApiKeyQuery(TermQuery termQuery) => Elastic.Clients.Elasticsearch.Security.ApiKeyQuery.Term(termQuery);
	public static implicit operator Elastic.Clients.Elasticsearch.Security.RoleQuery(TermQuery termQuery) => Elastic.Clients.Elasticsearch.Security.RoleQuery.Term(termQuery);
	public static implicit operator Elastic.Clients.Elasticsearch.Security.UserQuery(TermQuery termQuery) => Elastic.Clients.Elasticsearch.Security.UserQuery.Term(termQuery);
}

public sealed partial class TermQueryDescriptor<TDocument> : SerializableDescriptor<TermQueryDescriptor<TDocument>>
{
	internal TermQueryDescriptor(Action<TermQueryDescriptor<TDocument>> configure) => configure.Invoke(this);

	public TermQueryDescriptor() : base()
	{
	}

	private float? BoostValue { get; set; }
	private bool? CaseInsensitiveValue { get; set; }
	private Elastic.Clients.Elasticsearch.Field FieldValue { get; set; }
	private string? QueryNameValue { get; set; }
	private Elastic.Clients.Elasticsearch.FieldValue ValueValue { get; set; }

	/// <summary>
	/// <para>
	/// Floating point number used to decrease or increase the relevance scores of the query.
	/// Boost values are relative to the default value of 1.0.
	/// A boost value between 0 and 1.0 decreases the relevance score.
	/// A value greater than 1.0 increases the relevance score.
	/// </para>
	/// </summary>
	public TermQueryDescriptor<TDocument> Boost(float? boost)
	{
		BoostValue = boost;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Allows ASCII case insensitive matching of the value with the indexed field values when set to <c>true</c>.
	/// When <c>false</c>, the case sensitivity of matching depends on the underlying field’s mapping.
	/// </para>
	/// </summary>
	public TermQueryDescriptor<TDocument> CaseInsensitive(bool? caseInsensitive = true)
	{
		CaseInsensitiveValue = caseInsensitive;
		return Self;
	}

	public TermQueryDescriptor<TDocument> Field(Elastic.Clients.Elasticsearch.Field field)
	{
		FieldValue = field;
		return Self;
	}

	public TermQueryDescriptor<TDocument> Field<TValue>(Expression<Func<TDocument, TValue>> field)
	{
		FieldValue = field;
		return Self;
	}

	public TermQueryDescriptor<TDocument> Field(Expression<Func<TDocument, object>> field)
	{
		FieldValue = field;
		return Self;
	}

	public TermQueryDescriptor<TDocument> QueryName(string? queryName)
	{
		QueryNameValue = queryName;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Term you wish to find in the provided field.
	/// </para>
	/// </summary>
	public TermQueryDescriptor<TDocument> Value(Elastic.Clients.Elasticsearch.FieldValue value)
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

		writer.WritePropertyName("value");
		JsonSerializer.Serialize(writer, ValueValue, options);
		writer.WriteEndObject();
		writer.WriteEndObject();
	}
}

public sealed partial class TermQueryDescriptor : SerializableDescriptor<TermQueryDescriptor>
{
	internal TermQueryDescriptor(Action<TermQueryDescriptor> configure) => configure.Invoke(this);

	public TermQueryDescriptor() : base()
	{
	}

	private float? BoostValue { get; set; }
	private bool? CaseInsensitiveValue { get; set; }
	private Elastic.Clients.Elasticsearch.Field FieldValue { get; set; }
	private string? QueryNameValue { get; set; }
	private Elastic.Clients.Elasticsearch.FieldValue ValueValue { get; set; }

	/// <summary>
	/// <para>
	/// Floating point number used to decrease or increase the relevance scores of the query.
	/// Boost values are relative to the default value of 1.0.
	/// A boost value between 0 and 1.0 decreases the relevance score.
	/// A value greater than 1.0 increases the relevance score.
	/// </para>
	/// </summary>
	public TermQueryDescriptor Boost(float? boost)
	{
		BoostValue = boost;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Allows ASCII case insensitive matching of the value with the indexed field values when set to <c>true</c>.
	/// When <c>false</c>, the case sensitivity of matching depends on the underlying field’s mapping.
	/// </para>
	/// </summary>
	public TermQueryDescriptor CaseInsensitive(bool? caseInsensitive = true)
	{
		CaseInsensitiveValue = caseInsensitive;
		return Self;
	}

	public TermQueryDescriptor Field(Elastic.Clients.Elasticsearch.Field field)
	{
		FieldValue = field;
		return Self;
	}

	public TermQueryDescriptor Field<TDocument, TValue>(Expression<Func<TDocument, TValue>> field)
	{
		FieldValue = field;
		return Self;
	}

	public TermQueryDescriptor Field<TDocument>(Expression<Func<TDocument, object>> field)
	{
		FieldValue = field;
		return Self;
	}

	public TermQueryDescriptor QueryName(string? queryName)
	{
		QueryNameValue = queryName;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Term you wish to find in the provided field.
	/// </para>
	/// </summary>
	public TermQueryDescriptor Value(Elastic.Clients.Elasticsearch.FieldValue value)
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

		writer.WritePropertyName("value");
		JsonSerializer.Serialize(writer, ValueValue, options);
		writer.WriteEndObject();
		writer.WriteEndObject();
	}
}