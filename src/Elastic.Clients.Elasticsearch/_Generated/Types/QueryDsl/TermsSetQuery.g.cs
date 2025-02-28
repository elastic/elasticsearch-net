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

internal sealed partial class TermsSetQueryConverter : System.Text.Json.Serialization.JsonConverter<TermsSetQuery>
{
	private static readonly System.Text.Json.JsonEncodedText PropBoost = System.Text.Json.JsonEncodedText.Encode("boost");
	private static readonly System.Text.Json.JsonEncodedText PropMinimumShouldMatch = System.Text.Json.JsonEncodedText.Encode("minimum_should_match");
	private static readonly System.Text.Json.JsonEncodedText PropMinimumShouldMatchField = System.Text.Json.JsonEncodedText.Encode("minimum_should_match_field");
	private static readonly System.Text.Json.JsonEncodedText PropMinimumShouldMatchScript = System.Text.Json.JsonEncodedText.Encode("minimum_should_match_script");
	private static readonly System.Text.Json.JsonEncodedText PropQueryName = System.Text.Json.JsonEncodedText.Encode("_name");
	private static readonly System.Text.Json.JsonEncodedText PropTerms = System.Text.Json.JsonEncodedText.Encode("terms");

	public override TermsSetQuery Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<Elastic.Clients.Elasticsearch.Field> propField = default;
		reader.Read();
		propField.ReadPropertyName(ref reader, options, null);
		reader.Read();
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<float?> propBoost = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.MinimumShouldMatch?> propMinimumShouldMatch = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Field?> propMinimumShouldMatchField = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Script?> propMinimumShouldMatchScript = default;
		LocalJsonValue<string?> propQueryName = default;
		LocalJsonValue<ICollection<Elastic.Clients.Elasticsearch.FieldValue>> propTerms = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propBoost.TryReadProperty(ref reader, options, PropBoost, null))
			{
				continue;
			}

			if (propMinimumShouldMatch.TryReadProperty(ref reader, options, PropMinimumShouldMatch, null))
			{
				continue;
			}

			if (propMinimumShouldMatchField.TryReadProperty(ref reader, options, PropMinimumShouldMatchField, null))
			{
				continue;
			}

			if (propMinimumShouldMatchScript.TryReadProperty(ref reader, options, PropMinimumShouldMatchScript, null))
			{
				continue;
			}

			if (propQueryName.TryReadProperty(ref reader, options, PropQueryName, null))
			{
				continue;
			}

			if (propTerms.TryReadProperty(ref reader, options, PropTerms, static ICollection<Elastic.Clients.Elasticsearch.FieldValue> (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadCollectionValue<Elastic.Clients.Elasticsearch.FieldValue>(o, null)!))
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
		return new TermsSetQuery
		{
			Boost = propBoost.Value
,
			Field = propField.Value
,
			MinimumShouldMatch = propMinimumShouldMatch.Value
,
			MinimumShouldMatchField = propMinimumShouldMatchField.Value
,
			MinimumShouldMatchScript = propMinimumShouldMatchScript.Value
,
			QueryName = propQueryName.Value
,
			Terms = propTerms.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, TermsSetQuery value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WritePropertyName(options, value.Field, null);
		writer.WriteStartObject();
		writer.WriteProperty(options, PropBoost, value.Boost, null, null);
		writer.WriteProperty(options, PropMinimumShouldMatch, value.MinimumShouldMatch, null, null);
		writer.WriteProperty(options, PropMinimumShouldMatchField, value.MinimumShouldMatchField, null, null);
		writer.WriteProperty(options, PropMinimumShouldMatchScript, value.MinimumShouldMatchScript, null, null);
		writer.WriteProperty(options, PropQueryName, value.QueryName, null, null);
		writer.WriteProperty(options, PropTerms, value.Terms, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, ICollection<Elastic.Clients.Elasticsearch.FieldValue> v) => w.WriteCollectionValue<Elastic.Clients.Elasticsearch.FieldValue>(o, v, null));
		writer.WriteEndObject();
		writer.WriteEndObject();
	}
}

[JsonConverter(typeof(TermsSetQueryConverter))]
public sealed partial class TermsSetQuery
{
	public TermsSetQuery(Elastic.Clients.Elasticsearch.Field field)
	{
		if (field is null)
			throw new ArgumentNullException(nameof(field));
		Field = field;
	}

	internal TermsSetQuery()
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
	public Elastic.Clients.Elasticsearch.Field Field { get; set; }

	/// <summary>
	/// <para>
	/// Specification describing number of matching terms required to return a document.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MinimumShouldMatch? MinimumShouldMatch { get; set; }

	/// <summary>
	/// <para>
	/// Numeric field containing the number of matching terms required to return a document.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Field? MinimumShouldMatchField { get; set; }

	/// <summary>
	/// <para>
	/// Custom script containing the number of matching terms required to return a document.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Script? MinimumShouldMatchScript { get; set; }
	public string? QueryName { get; set; }

	/// <summary>
	/// <para>
	/// Array of terms you wish to find in the provided field.
	/// </para>
	/// </summary>
	public ICollection<Elastic.Clients.Elasticsearch.FieldValue> Terms { get; set; }

	public static implicit operator Elastic.Clients.Elasticsearch.QueryDsl.Query(TermsSetQuery termsSetQuery) => Elastic.Clients.Elasticsearch.QueryDsl.Query.TermsSet(termsSetQuery);
}

public sealed partial class TermsSetQueryDescriptor<TDocument> : SerializableDescriptor<TermsSetQueryDescriptor<TDocument>>
{
	internal TermsSetQueryDescriptor(Action<TermsSetQueryDescriptor<TDocument>> configure) => configure.Invoke(this);

	public TermsSetQueryDescriptor() : base()
	{
	}

	private float? BoostValue { get; set; }
	private Elastic.Clients.Elasticsearch.Field FieldValue { get; set; }
	private Elastic.Clients.Elasticsearch.MinimumShouldMatch? MinimumShouldMatchValue { get; set; }
	private Elastic.Clients.Elasticsearch.Field? MinimumShouldMatchFieldValue { get; set; }
	private Elastic.Clients.Elasticsearch.Script? MinimumShouldMatchScriptValue { get; set; }
	private Elastic.Clients.Elasticsearch.ScriptDescriptor MinimumShouldMatchScriptDescriptor { get; set; }
	private Action<Elastic.Clients.Elasticsearch.ScriptDescriptor> MinimumShouldMatchScriptDescriptorAction { get; set; }
	private string? QueryNameValue { get; set; }
	private ICollection<Elastic.Clients.Elasticsearch.FieldValue> TermsValue { get; set; }

	/// <summary>
	/// <para>
	/// Floating point number used to decrease or increase the relevance scores of the query.
	/// Boost values are relative to the default value of 1.0.
	/// A boost value between 0 and 1.0 decreases the relevance score.
	/// A value greater than 1.0 increases the relevance score.
	/// </para>
	/// </summary>
	public TermsSetQueryDescriptor<TDocument> Boost(float? boost)
	{
		BoostValue = boost;
		return Self;
	}

	public TermsSetQueryDescriptor<TDocument> Field(Elastic.Clients.Elasticsearch.Field field)
	{
		FieldValue = field;
		return Self;
	}

	public TermsSetQueryDescriptor<TDocument> Field<TValue>(Expression<Func<TDocument, TValue>> field)
	{
		FieldValue = field;
		return Self;
	}

	public TermsSetQueryDescriptor<TDocument> Field(Expression<Func<TDocument, object>> field)
	{
		FieldValue = field;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Specification describing number of matching terms required to return a document.
	/// </para>
	/// </summary>
	public TermsSetQueryDescriptor<TDocument> MinimumShouldMatch(Elastic.Clients.Elasticsearch.MinimumShouldMatch? minimumShouldMatch)
	{
		MinimumShouldMatchValue = minimumShouldMatch;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Numeric field containing the number of matching terms required to return a document.
	/// </para>
	/// </summary>
	public TermsSetQueryDescriptor<TDocument> MinimumShouldMatchField(Elastic.Clients.Elasticsearch.Field? minimumShouldMatchField)
	{
		MinimumShouldMatchFieldValue = minimumShouldMatchField;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Numeric field containing the number of matching terms required to return a document.
	/// </para>
	/// </summary>
	public TermsSetQueryDescriptor<TDocument> MinimumShouldMatchField<TValue>(Expression<Func<TDocument, TValue>> minimumShouldMatchField)
	{
		MinimumShouldMatchFieldValue = minimumShouldMatchField;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Numeric field containing the number of matching terms required to return a document.
	/// </para>
	/// </summary>
	public TermsSetQueryDescriptor<TDocument> MinimumShouldMatchField(Expression<Func<TDocument, object>> minimumShouldMatchField)
	{
		MinimumShouldMatchFieldValue = minimumShouldMatchField;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Custom script containing the number of matching terms required to return a document.
	/// </para>
	/// </summary>
	public TermsSetQueryDescriptor<TDocument> MinimumShouldMatchScript(Elastic.Clients.Elasticsearch.Script? minimumShouldMatchScript)
	{
		MinimumShouldMatchScriptDescriptor = null;
		MinimumShouldMatchScriptDescriptorAction = null;
		MinimumShouldMatchScriptValue = minimumShouldMatchScript;
		return Self;
	}

	public TermsSetQueryDescriptor<TDocument> MinimumShouldMatchScript(Elastic.Clients.Elasticsearch.ScriptDescriptor descriptor)
	{
		MinimumShouldMatchScriptValue = null;
		MinimumShouldMatchScriptDescriptorAction = null;
		MinimumShouldMatchScriptDescriptor = descriptor;
		return Self;
	}

	public TermsSetQueryDescriptor<TDocument> MinimumShouldMatchScript(Action<Elastic.Clients.Elasticsearch.ScriptDescriptor> configure)
	{
		MinimumShouldMatchScriptValue = null;
		MinimumShouldMatchScriptDescriptor = null;
		MinimumShouldMatchScriptDescriptorAction = configure;
		return Self;
	}

	public TermsSetQueryDescriptor<TDocument> QueryName(string? queryName)
	{
		QueryNameValue = queryName;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Array of terms you wish to find in the provided field.
	/// </para>
	/// </summary>
	public TermsSetQueryDescriptor<TDocument> Terms(ICollection<Elastic.Clients.Elasticsearch.FieldValue> terms)
	{
		TermsValue = terms;
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

		if (MinimumShouldMatchValue is not null)
		{
			writer.WritePropertyName("minimum_should_match");
			JsonSerializer.Serialize(writer, MinimumShouldMatchValue, options);
		}

		if (MinimumShouldMatchFieldValue is not null)
		{
			writer.WritePropertyName("minimum_should_match_field");
			JsonSerializer.Serialize(writer, MinimumShouldMatchFieldValue, options);
		}

		if (MinimumShouldMatchScriptDescriptor is not null)
		{
			writer.WritePropertyName("minimum_should_match_script");
			JsonSerializer.Serialize(writer, MinimumShouldMatchScriptDescriptor, options);
		}
		else if (MinimumShouldMatchScriptDescriptorAction is not null)
		{
			writer.WritePropertyName("minimum_should_match_script");
			JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.ScriptDescriptor(MinimumShouldMatchScriptDescriptorAction), options);
		}
		else if (MinimumShouldMatchScriptValue is not null)
		{
			writer.WritePropertyName("minimum_should_match_script");
			JsonSerializer.Serialize(writer, MinimumShouldMatchScriptValue, options);
		}

		if (!string.IsNullOrEmpty(QueryNameValue))
		{
			writer.WritePropertyName("_name");
			writer.WriteStringValue(QueryNameValue);
		}

		writer.WritePropertyName("terms");
		JsonSerializer.Serialize(writer, TermsValue, options);
		writer.WriteEndObject();
		writer.WriteEndObject();
	}
}

public sealed partial class TermsSetQueryDescriptor : SerializableDescriptor<TermsSetQueryDescriptor>
{
	internal TermsSetQueryDescriptor(Action<TermsSetQueryDescriptor> configure) => configure.Invoke(this);

	public TermsSetQueryDescriptor() : base()
	{
	}

	private float? BoostValue { get; set; }
	private Elastic.Clients.Elasticsearch.Field FieldValue { get; set; }
	private Elastic.Clients.Elasticsearch.MinimumShouldMatch? MinimumShouldMatchValue { get; set; }
	private Elastic.Clients.Elasticsearch.Field? MinimumShouldMatchFieldValue { get; set; }
	private Elastic.Clients.Elasticsearch.Script? MinimumShouldMatchScriptValue { get; set; }
	private Elastic.Clients.Elasticsearch.ScriptDescriptor MinimumShouldMatchScriptDescriptor { get; set; }
	private Action<Elastic.Clients.Elasticsearch.ScriptDescriptor> MinimumShouldMatchScriptDescriptorAction { get; set; }
	private string? QueryNameValue { get; set; }
	private ICollection<Elastic.Clients.Elasticsearch.FieldValue> TermsValue { get; set; }

	/// <summary>
	/// <para>
	/// Floating point number used to decrease or increase the relevance scores of the query.
	/// Boost values are relative to the default value of 1.0.
	/// A boost value between 0 and 1.0 decreases the relevance score.
	/// A value greater than 1.0 increases the relevance score.
	/// </para>
	/// </summary>
	public TermsSetQueryDescriptor Boost(float? boost)
	{
		BoostValue = boost;
		return Self;
	}

	public TermsSetQueryDescriptor Field(Elastic.Clients.Elasticsearch.Field field)
	{
		FieldValue = field;
		return Self;
	}

	public TermsSetQueryDescriptor Field<TDocument, TValue>(Expression<Func<TDocument, TValue>> field)
	{
		FieldValue = field;
		return Self;
	}

	public TermsSetQueryDescriptor Field<TDocument>(Expression<Func<TDocument, object>> field)
	{
		FieldValue = field;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Specification describing number of matching terms required to return a document.
	/// </para>
	/// </summary>
	public TermsSetQueryDescriptor MinimumShouldMatch(Elastic.Clients.Elasticsearch.MinimumShouldMatch? minimumShouldMatch)
	{
		MinimumShouldMatchValue = minimumShouldMatch;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Numeric field containing the number of matching terms required to return a document.
	/// </para>
	/// </summary>
	public TermsSetQueryDescriptor MinimumShouldMatchField(Elastic.Clients.Elasticsearch.Field? minimumShouldMatchField)
	{
		MinimumShouldMatchFieldValue = minimumShouldMatchField;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Numeric field containing the number of matching terms required to return a document.
	/// </para>
	/// </summary>
	public TermsSetQueryDescriptor MinimumShouldMatchField<TDocument, TValue>(Expression<Func<TDocument, TValue>> minimumShouldMatchField)
	{
		MinimumShouldMatchFieldValue = minimumShouldMatchField;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Numeric field containing the number of matching terms required to return a document.
	/// </para>
	/// </summary>
	public TermsSetQueryDescriptor MinimumShouldMatchField<TDocument>(Expression<Func<TDocument, object>> minimumShouldMatchField)
	{
		MinimumShouldMatchFieldValue = minimumShouldMatchField;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Custom script containing the number of matching terms required to return a document.
	/// </para>
	/// </summary>
	public TermsSetQueryDescriptor MinimumShouldMatchScript(Elastic.Clients.Elasticsearch.Script? minimumShouldMatchScript)
	{
		MinimumShouldMatchScriptDescriptor = null;
		MinimumShouldMatchScriptDescriptorAction = null;
		MinimumShouldMatchScriptValue = minimumShouldMatchScript;
		return Self;
	}

	public TermsSetQueryDescriptor MinimumShouldMatchScript(Elastic.Clients.Elasticsearch.ScriptDescriptor descriptor)
	{
		MinimumShouldMatchScriptValue = null;
		MinimumShouldMatchScriptDescriptorAction = null;
		MinimumShouldMatchScriptDescriptor = descriptor;
		return Self;
	}

	public TermsSetQueryDescriptor MinimumShouldMatchScript(Action<Elastic.Clients.Elasticsearch.ScriptDescriptor> configure)
	{
		MinimumShouldMatchScriptValue = null;
		MinimumShouldMatchScriptDescriptor = null;
		MinimumShouldMatchScriptDescriptorAction = configure;
		return Self;
	}

	public TermsSetQueryDescriptor QueryName(string? queryName)
	{
		QueryNameValue = queryName;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Array of terms you wish to find in the provided field.
	/// </para>
	/// </summary>
	public TermsSetQueryDescriptor Terms(ICollection<Elastic.Clients.Elasticsearch.FieldValue> terms)
	{
		TermsValue = terms;
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

		if (MinimumShouldMatchValue is not null)
		{
			writer.WritePropertyName("minimum_should_match");
			JsonSerializer.Serialize(writer, MinimumShouldMatchValue, options);
		}

		if (MinimumShouldMatchFieldValue is not null)
		{
			writer.WritePropertyName("minimum_should_match_field");
			JsonSerializer.Serialize(writer, MinimumShouldMatchFieldValue, options);
		}

		if (MinimumShouldMatchScriptDescriptor is not null)
		{
			writer.WritePropertyName("minimum_should_match_script");
			JsonSerializer.Serialize(writer, MinimumShouldMatchScriptDescriptor, options);
		}
		else if (MinimumShouldMatchScriptDescriptorAction is not null)
		{
			writer.WritePropertyName("minimum_should_match_script");
			JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.ScriptDescriptor(MinimumShouldMatchScriptDescriptorAction), options);
		}
		else if (MinimumShouldMatchScriptValue is not null)
		{
			writer.WritePropertyName("minimum_should_match_script");
			JsonSerializer.Serialize(writer, MinimumShouldMatchScriptValue, options);
		}

		if (!string.IsNullOrEmpty(QueryNameValue))
		{
			writer.WritePropertyName("_name");
			writer.WriteStringValue(QueryNameValue);
		}

		writer.WritePropertyName("terms");
		JsonSerializer.Serialize(writer, TermsValue, options);
		writer.WriteEndObject();
		writer.WriteEndObject();
	}
}