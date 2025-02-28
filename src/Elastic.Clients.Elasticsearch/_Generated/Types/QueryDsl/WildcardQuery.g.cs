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

internal sealed partial class WildcardQueryConverter : System.Text.Json.Serialization.JsonConverter<WildcardQuery>
{
	private static readonly System.Text.Json.JsonEncodedText PropBoost = System.Text.Json.JsonEncodedText.Encode("boost");
	private static readonly System.Text.Json.JsonEncodedText PropCaseInsensitive = System.Text.Json.JsonEncodedText.Encode("case_insensitive");
	private static readonly System.Text.Json.JsonEncodedText PropQueryName = System.Text.Json.JsonEncodedText.Encode("_name");
	private static readonly System.Text.Json.JsonEncodedText PropRewrite = System.Text.Json.JsonEncodedText.Encode("rewrite");
	private static readonly System.Text.Json.JsonEncodedText PropValue = System.Text.Json.JsonEncodedText.Encode("value");
	private static readonly System.Text.Json.JsonEncodedText PropWildcard = System.Text.Json.JsonEncodedText.Encode("wildcard");

	public override WildcardQuery Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<Elastic.Clients.Elasticsearch.Field> propField = default;
		reader.Read();
		propField.ReadPropertyName(ref reader, options, null);
		reader.Read();
		if (reader.TokenType is not System.Text.Json.JsonTokenType.StartObject)
		{
			var value = reader.ReadValue<string?>(options, null);
			reader.Read();
			return new WildcardQuery { Value = value };
		}

		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<float?> propBoost = default;
		LocalJsonValue<bool?> propCaseInsensitive = default;
		LocalJsonValue<string?> propQueryName = default;
		LocalJsonValue<string?> propRewrite = default;
		LocalJsonValue<string?> propValue = default;
		LocalJsonValue<string?> propWildcard = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propBoost.TryReadProperty(ref reader, options, PropBoost, null))
			{
				continue;
			}

			if (propCaseInsensitive.TryReadProperty(ref reader, options, PropCaseInsensitive, null))
			{
				continue;
			}

			if (propQueryName.TryReadProperty(ref reader, options, PropQueryName, null))
			{
				continue;
			}

			if (propRewrite.TryReadProperty(ref reader, options, PropRewrite, null))
			{
				continue;
			}

			if (propValue.TryReadProperty(ref reader, options, PropValue, null))
			{
				continue;
			}

			if (propWildcard.TryReadProperty(ref reader, options, PropWildcard, null))
			{
				continue;
			}

			if (options.UnmappedMemberHandling is System.Text.Json.Serialization.JsonUnmappedMemberHandling.Skip)
			{
				reader.Skip();
			}

			throw new System.Text.Json.JsonException($"Unknown JSON property '{reader.GetString()}' for type '{typeToConvert.Name}'.");
		}

		reader.ValidateToken(System.Text.Json.JsonTokenType.EndObject);
		reader.Read();
		reader.ValidateToken(System.Text.Json.JsonTokenType.EndObject);
		return new WildcardQuery
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
,
			Wildcard = propWildcard.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, WildcardQuery value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WritePropertyName(options, value.Field, null);
		writer.WriteStartObject();
		writer.WriteProperty(options, PropBoost, value.Boost, null, null);
		writer.WriteProperty(options, PropCaseInsensitive, value.CaseInsensitive, null, null);
		writer.WriteProperty(options, PropQueryName, value.QueryName, null, null);
		writer.WriteProperty(options, PropRewrite, value.Rewrite, null, null);
		writer.WriteProperty(options, PropValue, value.Value, null, null);
		writer.WriteProperty(options, PropWildcard, value.Wildcard, null, null);
		writer.WriteEndObject();
		writer.WriteEndObject();
	}
}

[JsonConverter(typeof(WildcardQueryConverter))]
public sealed partial class WildcardQuery
{
	public WildcardQuery(Elastic.Clients.Elasticsearch.Field field)
	{
		if (field is null)
			throw new ArgumentNullException(nameof(field));
		Field = field;
	}

	internal WildcardQuery()
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
	/// Allows case insensitive matching of the pattern with the indexed field values when set to true. Default is false which means the case sensitivity of matching depends on the underlying field’s mapping.
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
	/// Wildcard pattern for terms you wish to find in the provided field. Required, when wildcard is not set.
	/// </para>
	/// </summary>
	public string? Value { get; set; }

	/// <summary>
	/// <para>
	/// Wildcard pattern for terms you wish to find in the provided field. Required, when value is not set.
	/// </para>
	/// </summary>
	public string? Wildcard { get; set; }

	public static implicit operator Elastic.Clients.Elasticsearch.QueryDsl.Query(WildcardQuery wildcardQuery) => Elastic.Clients.Elasticsearch.QueryDsl.Query.Wildcard(wildcardQuery);
	public static implicit operator Elastic.Clients.Elasticsearch.Security.ApiKeyQuery(WildcardQuery wildcardQuery) => Elastic.Clients.Elasticsearch.Security.ApiKeyQuery.Wildcard(wildcardQuery);
	public static implicit operator Elastic.Clients.Elasticsearch.Security.RoleQuery(WildcardQuery wildcardQuery) => Elastic.Clients.Elasticsearch.Security.RoleQuery.Wildcard(wildcardQuery);
	public static implicit operator Elastic.Clients.Elasticsearch.Security.UserQuery(WildcardQuery wildcardQuery) => Elastic.Clients.Elasticsearch.Security.UserQuery.Wildcard(wildcardQuery);
}

public sealed partial class WildcardQueryDescriptor<TDocument> : SerializableDescriptor<WildcardQueryDescriptor<TDocument>>
{
	internal WildcardQueryDescriptor(Action<WildcardQueryDescriptor<TDocument>> configure) => configure.Invoke(this);

	public WildcardQueryDescriptor() : base()
	{
	}

	private float? BoostValue { get; set; }
	private bool? CaseInsensitiveValue { get; set; }
	private Elastic.Clients.Elasticsearch.Field FieldValue { get; set; }
	private string? QueryNameValue { get; set; }
	private string? RewriteValue { get; set; }
	private string? ValueValue { get; set; }
	private string? WildcardValue { get; set; }

	/// <summary>
	/// <para>
	/// Floating point number used to decrease or increase the relevance scores of the query.
	/// Boost values are relative to the default value of 1.0.
	/// A boost value between 0 and 1.0 decreases the relevance score.
	/// A value greater than 1.0 increases the relevance score.
	/// </para>
	/// </summary>
	public WildcardQueryDescriptor<TDocument> Boost(float? boost)
	{
		BoostValue = boost;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Allows case insensitive matching of the pattern with the indexed field values when set to true. Default is false which means the case sensitivity of matching depends on the underlying field’s mapping.
	/// </para>
	/// </summary>
	public WildcardQueryDescriptor<TDocument> CaseInsensitive(bool? caseInsensitive = true)
	{
		CaseInsensitiveValue = caseInsensitive;
		return Self;
	}

	public WildcardQueryDescriptor<TDocument> Field(Elastic.Clients.Elasticsearch.Field field)
	{
		FieldValue = field;
		return Self;
	}

	public WildcardQueryDescriptor<TDocument> Field<TValue>(Expression<Func<TDocument, TValue>> field)
	{
		FieldValue = field;
		return Self;
	}

	public WildcardQueryDescriptor<TDocument> Field(Expression<Func<TDocument, object>> field)
	{
		FieldValue = field;
		return Self;
	}

	public WildcardQueryDescriptor<TDocument> QueryName(string? queryName)
	{
		QueryNameValue = queryName;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Method used to rewrite the query.
	/// </para>
	/// </summary>
	public WildcardQueryDescriptor<TDocument> Rewrite(string? rewrite)
	{
		RewriteValue = rewrite;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Wildcard pattern for terms you wish to find in the provided field. Required, when wildcard is not set.
	/// </para>
	/// </summary>
	public WildcardQueryDescriptor<TDocument> Value(string? value)
	{
		ValueValue = value;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Wildcard pattern for terms you wish to find in the provided field. Required, when value is not set.
	/// </para>
	/// </summary>
	public WildcardQueryDescriptor<TDocument> Wildcard(string? wildcard)
	{
		WildcardValue = wildcard;
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

		if (!string.IsNullOrEmpty(ValueValue))
		{
			writer.WritePropertyName("value");
			writer.WriteStringValue(ValueValue);
		}

		if (!string.IsNullOrEmpty(WildcardValue))
		{
			writer.WritePropertyName("wildcard");
			writer.WriteStringValue(WildcardValue);
		}

		writer.WriteEndObject();
		writer.WriteEndObject();
	}
}

public sealed partial class WildcardQueryDescriptor : SerializableDescriptor<WildcardQueryDescriptor>
{
	internal WildcardQueryDescriptor(Action<WildcardQueryDescriptor> configure) => configure.Invoke(this);

	public WildcardQueryDescriptor() : base()
	{
	}

	private float? BoostValue { get; set; }
	private bool? CaseInsensitiveValue { get; set; }
	private Elastic.Clients.Elasticsearch.Field FieldValue { get; set; }
	private string? QueryNameValue { get; set; }
	private string? RewriteValue { get; set; }
	private string? ValueValue { get; set; }
	private string? WildcardValue { get; set; }

	/// <summary>
	/// <para>
	/// Floating point number used to decrease or increase the relevance scores of the query.
	/// Boost values are relative to the default value of 1.0.
	/// A boost value between 0 and 1.0 decreases the relevance score.
	/// A value greater than 1.0 increases the relevance score.
	/// </para>
	/// </summary>
	public WildcardQueryDescriptor Boost(float? boost)
	{
		BoostValue = boost;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Allows case insensitive matching of the pattern with the indexed field values when set to true. Default is false which means the case sensitivity of matching depends on the underlying field’s mapping.
	/// </para>
	/// </summary>
	public WildcardQueryDescriptor CaseInsensitive(bool? caseInsensitive = true)
	{
		CaseInsensitiveValue = caseInsensitive;
		return Self;
	}

	public WildcardQueryDescriptor Field(Elastic.Clients.Elasticsearch.Field field)
	{
		FieldValue = field;
		return Self;
	}

	public WildcardQueryDescriptor Field<TDocument, TValue>(Expression<Func<TDocument, TValue>> field)
	{
		FieldValue = field;
		return Self;
	}

	public WildcardQueryDescriptor Field<TDocument>(Expression<Func<TDocument, object>> field)
	{
		FieldValue = field;
		return Self;
	}

	public WildcardQueryDescriptor QueryName(string? queryName)
	{
		QueryNameValue = queryName;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Method used to rewrite the query.
	/// </para>
	/// </summary>
	public WildcardQueryDescriptor Rewrite(string? rewrite)
	{
		RewriteValue = rewrite;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Wildcard pattern for terms you wish to find in the provided field. Required, when wildcard is not set.
	/// </para>
	/// </summary>
	public WildcardQueryDescriptor Value(string? value)
	{
		ValueValue = value;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Wildcard pattern for terms you wish to find in the provided field. Required, when value is not set.
	/// </para>
	/// </summary>
	public WildcardQueryDescriptor Wildcard(string? wildcard)
	{
		WildcardValue = wildcard;
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

		if (!string.IsNullOrEmpty(ValueValue))
		{
			writer.WritePropertyName("value");
			writer.WriteStringValue(ValueValue);
		}

		if (!string.IsNullOrEmpty(WildcardValue))
		{
			writer.WritePropertyName("wildcard");
			writer.WriteStringValue(WildcardValue);
		}

		writer.WriteEndObject();
		writer.WriteEndObject();
	}
}