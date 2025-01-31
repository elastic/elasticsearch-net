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

internal sealed partial class PrefixQueryConverter : System.Text.Json.Serialization.JsonConverter<PrefixQuery>
{
	private static readonly System.Text.Json.JsonEncodedText PropBoost = System.Text.Json.JsonEncodedText.Encode("boost");
	private static readonly System.Text.Json.JsonEncodedText PropCaseInsensitive = System.Text.Json.JsonEncodedText.Encode("case_insensitive");
	private static readonly System.Text.Json.JsonEncodedText PropQueryName = System.Text.Json.JsonEncodedText.Encode("_name");
	private static readonly System.Text.Json.JsonEncodedText PropRewrite = System.Text.Json.JsonEncodedText.Encode("rewrite");
	private static readonly System.Text.Json.JsonEncodedText PropValue = System.Text.Json.JsonEncodedText.Encode("value");

	public override PrefixQuery Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<Elastic.Clients.Elasticsearch.Field> propField = default;
		reader.Read();
		propField.ReadPropertyName(ref reader, options);
		reader.Read();
		if (reader.TokenType is not System.Text.Json.JsonTokenType.StartObject)
		{
			var value = reader.ReadValue<string>(options);
			reader.Read();
			return new PrefixQuery { Value = value };
		}

		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<float?> propBoost = default;
		LocalJsonValue<bool?> propCaseInsensitive = default;
		LocalJsonValue<string?> propQueryName = default;
		LocalJsonValue<string?> propRewrite = default;
		LocalJsonValue<string> propValue = default;
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

			if (propRewrite.TryRead(ref reader, options, PropRewrite))
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
		return new PrefixQuery
		{
			Boost = propBoost.Value
,
			CaseInsensitive = propCaseInsensitive.Value
,
			Field = propField.Value
,
			QueryName = propQueryName.Value
,
			Rewrite = propRewrite.Value
,
			Value = propValue.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, PrefixQuery value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WritePropertyName(options, value.Field);
		writer.WriteStartObject();
		writer.WriteProperty(options, PropBoost, value.Boost);
		writer.WriteProperty(options, PropCaseInsensitive, value.CaseInsensitive);
		writer.WriteProperty(options, PropQueryName, value.QueryName);
		writer.WriteProperty(options, PropRewrite, value.Rewrite);
		writer.WriteProperty(options, PropValue, value.Value);
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

	internal PrefixQuery()
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
	public static implicit operator Elastic.Clients.Elasticsearch.Security.ApiKeyQuery(PrefixQuery prefixQuery) => Elastic.Clients.Elasticsearch.Security.ApiKeyQuery.Prefix(prefixQuery);
	public static implicit operator Elastic.Clients.Elasticsearch.Security.RoleQuery(PrefixQuery prefixQuery) => Elastic.Clients.Elasticsearch.Security.RoleQuery.Prefix(prefixQuery);
	public static implicit operator Elastic.Clients.Elasticsearch.Security.UserQuery(PrefixQuery prefixQuery) => Elastic.Clients.Elasticsearch.Security.UserQuery.Prefix(prefixQuery);
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