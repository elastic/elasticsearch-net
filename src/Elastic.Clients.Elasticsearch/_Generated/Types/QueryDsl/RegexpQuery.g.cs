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

internal sealed partial class RegexpQueryConverter : System.Text.Json.Serialization.JsonConverter<RegexpQuery>
{
	private static readonly System.Text.Json.JsonEncodedText PropBoost = System.Text.Json.JsonEncodedText.Encode("boost");
	private static readonly System.Text.Json.JsonEncodedText PropCaseInsensitive = System.Text.Json.JsonEncodedText.Encode("case_insensitive");
	private static readonly System.Text.Json.JsonEncodedText PropFlags = System.Text.Json.JsonEncodedText.Encode("flags");
	private static readonly System.Text.Json.JsonEncodedText PropMaxDeterminizedStates = System.Text.Json.JsonEncodedText.Encode("max_determinized_states");
	private static readonly System.Text.Json.JsonEncodedText PropQueryName = System.Text.Json.JsonEncodedText.Encode("_name");
	private static readonly System.Text.Json.JsonEncodedText PropRewrite = System.Text.Json.JsonEncodedText.Encode("rewrite");
	private static readonly System.Text.Json.JsonEncodedText PropValue = System.Text.Json.JsonEncodedText.Encode("value");

	public override RegexpQuery Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
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
			return new RegexpQuery { Value = value };
		}

		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<float?> propBoost = default;
		LocalJsonValue<bool?> propCaseInsensitive = default;
		LocalJsonValue<string?> propFlags = default;
		LocalJsonValue<int?> propMaxDeterminizedStates = default;
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

			if (propFlags.TryRead(ref reader, options, PropFlags))
			{
				continue;
			}

			if (propMaxDeterminizedStates.TryRead(ref reader, options, PropMaxDeterminizedStates))
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
		return new RegexpQuery
		{
			Boost = propBoost.Value
,
			CaseInsensitive = propCaseInsensitive.Value
,
			Field = propField.Value
,
			Flags = propFlags.Value
,
			MaxDeterminizedStates = propMaxDeterminizedStates.Value
,
			QueryName = propQueryName.Value
,
			Rewrite = propRewrite.Value
,
			Value = propValue.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, RegexpQuery value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WritePropertyName(options, value.Field);
		writer.WriteStartObject();
		writer.WriteProperty(options, PropBoost, value.Boost);
		writer.WriteProperty(options, PropCaseInsensitive, value.CaseInsensitive);
		writer.WriteProperty(options, PropFlags, value.Flags);
		writer.WriteProperty(options, PropMaxDeterminizedStates, value.MaxDeterminizedStates);
		writer.WriteProperty(options, PropQueryName, value.QueryName);
		writer.WriteProperty(options, PropRewrite, value.Rewrite);
		writer.WriteProperty(options, PropValue, value.Value);
		writer.WriteEndObject();
		writer.WriteEndObject();
	}
}

[JsonConverter(typeof(RegexpQueryConverter))]
public sealed partial class RegexpQuery
{
	public RegexpQuery(Elastic.Clients.Elasticsearch.Field field)
	{
		if (field is null)
			throw new ArgumentNullException(nameof(field));
		Field = field;
	}

	internal RegexpQuery()
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
	/// Allows case insensitive matching of the regular expression value with the indexed field values when set to <c>true</c>.
	/// When <c>false</c>, case sensitivity of matching depends on the underlying field’s mapping.
	/// </para>
	/// </summary>
	public bool? CaseInsensitive { get; set; }
	public Elastic.Clients.Elasticsearch.Field Field { get; set; }

	/// <summary>
	/// <para>
	/// Enables optional operators for the regular expression.
	/// </para>
	/// </summary>
	public string? Flags { get; set; }

	/// <summary>
	/// <para>
	/// Maximum number of automaton states required for the query.
	/// </para>
	/// </summary>
	public int? MaxDeterminizedStates { get; set; }
	public string? QueryName { get; set; }

	/// <summary>
	/// <para>
	/// Method used to rewrite the query.
	/// </para>
	/// </summary>
	public string? Rewrite { get; set; }

	/// <summary>
	/// <para>
	/// Regular expression for terms you wish to find in the provided field.
	/// </para>
	/// </summary>
	public string Value { get; set; }

	public static implicit operator Elastic.Clients.Elasticsearch.QueryDsl.Query(RegexpQuery regexpQuery) => Elastic.Clients.Elasticsearch.QueryDsl.Query.Regexp(regexpQuery);
}

public sealed partial class RegexpQueryDescriptor<TDocument> : SerializableDescriptor<RegexpQueryDescriptor<TDocument>>
{
	internal RegexpQueryDescriptor(Action<RegexpQueryDescriptor<TDocument>> configure) => configure.Invoke(this);

	public RegexpQueryDescriptor() : base()
	{
	}

	private float? BoostValue { get; set; }
	private bool? CaseInsensitiveValue { get; set; }
	private Elastic.Clients.Elasticsearch.Field FieldValue { get; set; }
	private string? FlagsValue { get; set; }
	private int? MaxDeterminizedStatesValue { get; set; }
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
	public RegexpQueryDescriptor<TDocument> Boost(float? boost)
	{
		BoostValue = boost;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Allows case insensitive matching of the regular expression value with the indexed field values when set to <c>true</c>.
	/// When <c>false</c>, case sensitivity of matching depends on the underlying field’s mapping.
	/// </para>
	/// </summary>
	public RegexpQueryDescriptor<TDocument> CaseInsensitive(bool? caseInsensitive = true)
	{
		CaseInsensitiveValue = caseInsensitive;
		return Self;
	}

	public RegexpQueryDescriptor<TDocument> Field(Elastic.Clients.Elasticsearch.Field field)
	{
		FieldValue = field;
		return Self;
	}

	public RegexpQueryDescriptor<TDocument> Field<TValue>(Expression<Func<TDocument, TValue>> field)
	{
		FieldValue = field;
		return Self;
	}

	public RegexpQueryDescriptor<TDocument> Field(Expression<Func<TDocument, object>> field)
	{
		FieldValue = field;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Enables optional operators for the regular expression.
	/// </para>
	/// </summary>
	public RegexpQueryDescriptor<TDocument> Flags(string? flags)
	{
		FlagsValue = flags;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Maximum number of automaton states required for the query.
	/// </para>
	/// </summary>
	public RegexpQueryDescriptor<TDocument> MaxDeterminizedStates(int? maxDeterminizedStates)
	{
		MaxDeterminizedStatesValue = maxDeterminizedStates;
		return Self;
	}

	public RegexpQueryDescriptor<TDocument> QueryName(string? queryName)
	{
		QueryNameValue = queryName;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Method used to rewrite the query.
	/// </para>
	/// </summary>
	public RegexpQueryDescriptor<TDocument> Rewrite(string? rewrite)
	{
		RewriteValue = rewrite;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Regular expression for terms you wish to find in the provided field.
	/// </para>
	/// </summary>
	public RegexpQueryDescriptor<TDocument> Value(string value)
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

		if (!string.IsNullOrEmpty(FlagsValue))
		{
			writer.WritePropertyName("flags");
			writer.WriteStringValue(FlagsValue);
		}

		if (MaxDeterminizedStatesValue.HasValue)
		{
			writer.WritePropertyName("max_determinized_states");
			writer.WriteNumberValue(MaxDeterminizedStatesValue.Value);
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

public sealed partial class RegexpQueryDescriptor : SerializableDescriptor<RegexpQueryDescriptor>
{
	internal RegexpQueryDescriptor(Action<RegexpQueryDescriptor> configure) => configure.Invoke(this);

	public RegexpQueryDescriptor() : base()
	{
	}

	private float? BoostValue { get; set; }
	private bool? CaseInsensitiveValue { get; set; }
	private Elastic.Clients.Elasticsearch.Field FieldValue { get; set; }
	private string? FlagsValue { get; set; }
	private int? MaxDeterminizedStatesValue { get; set; }
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
	public RegexpQueryDescriptor Boost(float? boost)
	{
		BoostValue = boost;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Allows case insensitive matching of the regular expression value with the indexed field values when set to <c>true</c>.
	/// When <c>false</c>, case sensitivity of matching depends on the underlying field’s mapping.
	/// </para>
	/// </summary>
	public RegexpQueryDescriptor CaseInsensitive(bool? caseInsensitive = true)
	{
		CaseInsensitiveValue = caseInsensitive;
		return Self;
	}

	public RegexpQueryDescriptor Field(Elastic.Clients.Elasticsearch.Field field)
	{
		FieldValue = field;
		return Self;
	}

	public RegexpQueryDescriptor Field<TDocument, TValue>(Expression<Func<TDocument, TValue>> field)
	{
		FieldValue = field;
		return Self;
	}

	public RegexpQueryDescriptor Field<TDocument>(Expression<Func<TDocument, object>> field)
	{
		FieldValue = field;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Enables optional operators for the regular expression.
	/// </para>
	/// </summary>
	public RegexpQueryDescriptor Flags(string? flags)
	{
		FlagsValue = flags;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Maximum number of automaton states required for the query.
	/// </para>
	/// </summary>
	public RegexpQueryDescriptor MaxDeterminizedStates(int? maxDeterminizedStates)
	{
		MaxDeterminizedStatesValue = maxDeterminizedStates;
		return Self;
	}

	public RegexpQueryDescriptor QueryName(string? queryName)
	{
		QueryNameValue = queryName;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Method used to rewrite the query.
	/// </para>
	/// </summary>
	public RegexpQueryDescriptor Rewrite(string? rewrite)
	{
		RewriteValue = rewrite;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Regular expression for terms you wish to find in the provided field.
	/// </para>
	/// </summary>
	public RegexpQueryDescriptor Value(string value)
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

		if (!string.IsNullOrEmpty(FlagsValue))
		{
			writer.WritePropertyName("flags");
			writer.WriteStringValue(FlagsValue);
		}

		if (MaxDeterminizedStatesValue.HasValue)
		{
			writer.WritePropertyName("max_determinized_states");
			writer.WriteNumberValue(MaxDeterminizedStatesValue.Value);
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