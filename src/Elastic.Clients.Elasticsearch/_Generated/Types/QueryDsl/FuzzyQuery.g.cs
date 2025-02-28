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

internal sealed partial class FuzzyQueryConverter : System.Text.Json.Serialization.JsonConverter<FuzzyQuery>
{
	private static readonly System.Text.Json.JsonEncodedText PropBoost = System.Text.Json.JsonEncodedText.Encode("boost");
	private static readonly System.Text.Json.JsonEncodedText PropFuzziness = System.Text.Json.JsonEncodedText.Encode("fuzziness");
	private static readonly System.Text.Json.JsonEncodedText PropMaxExpansions = System.Text.Json.JsonEncodedText.Encode("max_expansions");
	private static readonly System.Text.Json.JsonEncodedText PropPrefixLength = System.Text.Json.JsonEncodedText.Encode("prefix_length");
	private static readonly System.Text.Json.JsonEncodedText PropQueryName = System.Text.Json.JsonEncodedText.Encode("_name");
	private static readonly System.Text.Json.JsonEncodedText PropRewrite = System.Text.Json.JsonEncodedText.Encode("rewrite");
	private static readonly System.Text.Json.JsonEncodedText PropTranspositions = System.Text.Json.JsonEncodedText.Encode("transpositions");
	private static readonly System.Text.Json.JsonEncodedText PropValue = System.Text.Json.JsonEncodedText.Encode("value");

	public override FuzzyQuery Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<Elastic.Clients.Elasticsearch.Field> propField = default;
		reader.Read();
		propField.ReadPropertyName(ref reader, options, null);
		reader.Read();
		if (reader.TokenType is not System.Text.Json.JsonTokenType.StartObject)
		{
			var value = reader.ReadValue<object>(options, null);
			reader.Read();
			return new FuzzyQuery { Value = value };
		}

		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<float?> propBoost = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Fuzziness?> propFuzziness = default;
		LocalJsonValue<int?> propMaxExpansions = default;
		LocalJsonValue<int?> propPrefixLength = default;
		LocalJsonValue<string?> propQueryName = default;
		LocalJsonValue<string?> propRewrite = default;
		LocalJsonValue<bool?> propTranspositions = default;
		LocalJsonValue<object> propValue = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propBoost.TryReadProperty(ref reader, options, PropBoost, null))
			{
				continue;
			}

			if (propFuzziness.TryReadProperty(ref reader, options, PropFuzziness, null))
			{
				continue;
			}

			if (propMaxExpansions.TryReadProperty(ref reader, options, PropMaxExpansions, null))
			{
				continue;
			}

			if (propPrefixLength.TryReadProperty(ref reader, options, PropPrefixLength, null))
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

			if (propTranspositions.TryReadProperty(ref reader, options, PropTranspositions, null))
			{
				continue;
			}

			if (propValue.TryReadProperty(ref reader, options, PropValue, null))
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
		return new FuzzyQuery
		{
			Boost = propBoost.Value
,
			Field = propField.Value
,
			Fuzziness = propFuzziness.Value
,
			MaxExpansions = propMaxExpansions.Value
,
			PrefixLength = propPrefixLength.Value
,
			QueryName = propQueryName.Value
,
			Rewrite = propRewrite.Value
,
			Transpositions = propTranspositions.Value
,
			Value = propValue.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, FuzzyQuery value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WritePropertyName(options, value.Field, null);
		writer.WriteStartObject();
		writer.WriteProperty(options, PropBoost, value.Boost, null, null);
		writer.WriteProperty(options, PropFuzziness, value.Fuzziness, null, null);
		writer.WriteProperty(options, PropMaxExpansions, value.MaxExpansions, null, null);
		writer.WriteProperty(options, PropPrefixLength, value.PrefixLength, null, null);
		writer.WriteProperty(options, PropQueryName, value.QueryName, null, null);
		writer.WriteProperty(options, PropRewrite, value.Rewrite, null, null);
		writer.WriteProperty(options, PropTranspositions, value.Transpositions, null, null);
		writer.WriteProperty(options, PropValue, value.Value, null, null);
		writer.WriteEndObject();
		writer.WriteEndObject();
	}
}

[JsonConverter(typeof(FuzzyQueryConverter))]
public sealed partial class FuzzyQuery
{
	public FuzzyQuery(Elastic.Clients.Elasticsearch.Field field)
	{
		if (field is null)
			throw new ArgumentNullException(nameof(field));
		Field = field;
	}

	internal FuzzyQuery()
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
	/// Maximum edit distance allowed for matching.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Fuzziness? Fuzziness { get; set; }

	/// <summary>
	/// <para>
	/// Maximum number of variations created.
	/// </para>
	/// </summary>
	public int? MaxExpansions { get; set; }

	/// <summary>
	/// <para>
	/// Number of beginning characters left unchanged when creating expansions.
	/// </para>
	/// </summary>
	public int? PrefixLength { get; set; }
	public string? QueryName { get; set; }

	/// <summary>
	/// <para>
	/// Number of beginning characters left unchanged when creating expansions.
	/// </para>
	/// </summary>
	public string? Rewrite { get; set; }

	/// <summary>
	/// <para>
	/// Indicates whether edits include transpositions of two adjacent characters (for example <c>ab</c> to <c>ba</c>).
	/// </para>
	/// </summary>
	public bool? Transpositions { get; set; }

	/// <summary>
	/// <para>
	/// Term you wish to find in the provided field.
	/// </para>
	/// </summary>
	public object Value { get; set; }

	public static implicit operator Elastic.Clients.Elasticsearch.QueryDsl.Query(FuzzyQuery fuzzyQuery) => Elastic.Clients.Elasticsearch.QueryDsl.Query.Fuzzy(fuzzyQuery);
}

public sealed partial class FuzzyQueryDescriptor<TDocument> : SerializableDescriptor<FuzzyQueryDescriptor<TDocument>>
{
	internal FuzzyQueryDescriptor(Action<FuzzyQueryDescriptor<TDocument>> configure) => configure.Invoke(this);

	public FuzzyQueryDescriptor() : base()
	{
	}

	private float? BoostValue { get; set; }
	private Elastic.Clients.Elasticsearch.Field FieldValue { get; set; }
	private Elastic.Clients.Elasticsearch.Fuzziness? FuzzinessValue { get; set; }
	private int? MaxExpansionsValue { get; set; }
	private int? PrefixLengthValue { get; set; }
	private string? QueryNameValue { get; set; }
	private string? RewriteValue { get; set; }
	private bool? TranspositionsValue { get; set; }
	private object ValueValue { get; set; }

	/// <summary>
	/// <para>
	/// Floating point number used to decrease or increase the relevance scores of the query.
	/// Boost values are relative to the default value of 1.0.
	/// A boost value between 0 and 1.0 decreases the relevance score.
	/// A value greater than 1.0 increases the relevance score.
	/// </para>
	/// </summary>
	public FuzzyQueryDescriptor<TDocument> Boost(float? boost)
	{
		BoostValue = boost;
		return Self;
	}

	public FuzzyQueryDescriptor<TDocument> Field(Elastic.Clients.Elasticsearch.Field field)
	{
		FieldValue = field;
		return Self;
	}

	public FuzzyQueryDescriptor<TDocument> Field<TValue>(Expression<Func<TDocument, TValue>> field)
	{
		FieldValue = field;
		return Self;
	}

	public FuzzyQueryDescriptor<TDocument> Field(Expression<Func<TDocument, object>> field)
	{
		FieldValue = field;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Maximum edit distance allowed for matching.
	/// </para>
	/// </summary>
	public FuzzyQueryDescriptor<TDocument> Fuzziness(Elastic.Clients.Elasticsearch.Fuzziness? fuzziness)
	{
		FuzzinessValue = fuzziness;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Maximum number of variations created.
	/// </para>
	/// </summary>
	public FuzzyQueryDescriptor<TDocument> MaxExpansions(int? maxExpansions)
	{
		MaxExpansionsValue = maxExpansions;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Number of beginning characters left unchanged when creating expansions.
	/// </para>
	/// </summary>
	public FuzzyQueryDescriptor<TDocument> PrefixLength(int? prefixLength)
	{
		PrefixLengthValue = prefixLength;
		return Self;
	}

	public FuzzyQueryDescriptor<TDocument> QueryName(string? queryName)
	{
		QueryNameValue = queryName;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Number of beginning characters left unchanged when creating expansions.
	/// </para>
	/// </summary>
	public FuzzyQueryDescriptor<TDocument> Rewrite(string? rewrite)
	{
		RewriteValue = rewrite;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Indicates whether edits include transpositions of two adjacent characters (for example <c>ab</c> to <c>ba</c>).
	/// </para>
	/// </summary>
	public FuzzyQueryDescriptor<TDocument> Transpositions(bool? transpositions = true)
	{
		TranspositionsValue = transpositions;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Term you wish to find in the provided field.
	/// </para>
	/// </summary>
	public FuzzyQueryDescriptor<TDocument> Value(object value)
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

		if (FuzzinessValue is not null)
		{
			writer.WritePropertyName("fuzziness");
			JsonSerializer.Serialize(writer, FuzzinessValue, options);
		}

		if (MaxExpansionsValue.HasValue)
		{
			writer.WritePropertyName("max_expansions");
			writer.WriteNumberValue(MaxExpansionsValue.Value);
		}

		if (PrefixLengthValue.HasValue)
		{
			writer.WritePropertyName("prefix_length");
			writer.WriteNumberValue(PrefixLengthValue.Value);
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

		if (TranspositionsValue.HasValue)
		{
			writer.WritePropertyName("transpositions");
			writer.WriteBooleanValue(TranspositionsValue.Value);
		}

		writer.WritePropertyName("value");
		JsonSerializer.Serialize(writer, ValueValue, options);
		writer.WriteEndObject();
		writer.WriteEndObject();
	}
}

public sealed partial class FuzzyQueryDescriptor : SerializableDescriptor<FuzzyQueryDescriptor>
{
	internal FuzzyQueryDescriptor(Action<FuzzyQueryDescriptor> configure) => configure.Invoke(this);

	public FuzzyQueryDescriptor() : base()
	{
	}

	private float? BoostValue { get; set; }
	private Elastic.Clients.Elasticsearch.Field FieldValue { get; set; }
	private Elastic.Clients.Elasticsearch.Fuzziness? FuzzinessValue { get; set; }
	private int? MaxExpansionsValue { get; set; }
	private int? PrefixLengthValue { get; set; }
	private string? QueryNameValue { get; set; }
	private string? RewriteValue { get; set; }
	private bool? TranspositionsValue { get; set; }
	private object ValueValue { get; set; }

	/// <summary>
	/// <para>
	/// Floating point number used to decrease or increase the relevance scores of the query.
	/// Boost values are relative to the default value of 1.0.
	/// A boost value between 0 and 1.0 decreases the relevance score.
	/// A value greater than 1.0 increases the relevance score.
	/// </para>
	/// </summary>
	public FuzzyQueryDescriptor Boost(float? boost)
	{
		BoostValue = boost;
		return Self;
	}

	public FuzzyQueryDescriptor Field(Elastic.Clients.Elasticsearch.Field field)
	{
		FieldValue = field;
		return Self;
	}

	public FuzzyQueryDescriptor Field<TDocument, TValue>(Expression<Func<TDocument, TValue>> field)
	{
		FieldValue = field;
		return Self;
	}

	public FuzzyQueryDescriptor Field<TDocument>(Expression<Func<TDocument, object>> field)
	{
		FieldValue = field;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Maximum edit distance allowed for matching.
	/// </para>
	/// </summary>
	public FuzzyQueryDescriptor Fuzziness(Elastic.Clients.Elasticsearch.Fuzziness? fuzziness)
	{
		FuzzinessValue = fuzziness;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Maximum number of variations created.
	/// </para>
	/// </summary>
	public FuzzyQueryDescriptor MaxExpansions(int? maxExpansions)
	{
		MaxExpansionsValue = maxExpansions;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Number of beginning characters left unchanged when creating expansions.
	/// </para>
	/// </summary>
	public FuzzyQueryDescriptor PrefixLength(int? prefixLength)
	{
		PrefixLengthValue = prefixLength;
		return Self;
	}

	public FuzzyQueryDescriptor QueryName(string? queryName)
	{
		QueryNameValue = queryName;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Number of beginning characters left unchanged when creating expansions.
	/// </para>
	/// </summary>
	public FuzzyQueryDescriptor Rewrite(string? rewrite)
	{
		RewriteValue = rewrite;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Indicates whether edits include transpositions of two adjacent characters (for example <c>ab</c> to <c>ba</c>).
	/// </para>
	/// </summary>
	public FuzzyQueryDescriptor Transpositions(bool? transpositions = true)
	{
		TranspositionsValue = transpositions;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Term you wish to find in the provided field.
	/// </para>
	/// </summary>
	public FuzzyQueryDescriptor Value(object value)
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

		if (FuzzinessValue is not null)
		{
			writer.WritePropertyName("fuzziness");
			JsonSerializer.Serialize(writer, FuzzinessValue, options);
		}

		if (MaxExpansionsValue.HasValue)
		{
			writer.WritePropertyName("max_expansions");
			writer.WriteNumberValue(MaxExpansionsValue.Value);
		}

		if (PrefixLengthValue.HasValue)
		{
			writer.WritePropertyName("prefix_length");
			writer.WriteNumberValue(PrefixLengthValue.Value);
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

		if (TranspositionsValue.HasValue)
		{
			writer.WritePropertyName("transpositions");
			writer.WriteBooleanValue(TranspositionsValue.Value);
		}

		writer.WritePropertyName("value");
		JsonSerializer.Serialize(writer, ValueValue, options);
		writer.WriteEndObject();
		writer.WriteEndObject();
	}
}