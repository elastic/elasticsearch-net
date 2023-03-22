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

internal sealed partial class RegexpQueryConverter : JsonConverter<RegexpQuery>
{
	public override RegexpQuery Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		if (reader.TokenType != JsonTokenType.StartObject)
			throw new JsonException("Unexpected JSON detected.");
		reader.Read();
		var fieldName = reader.GetString();
		reader.Read();
		var variant = new RegexpQuery(fieldName);
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

				if (property == "case_insensitive")
				{
					variant.CaseInsensitive = JsonSerializer.Deserialize<bool?>(ref reader, options);
					continue;
				}

				if (property == "flags")
				{
					variant.Flags = JsonSerializer.Deserialize<string?>(ref reader, options);
					continue;
				}

				if (property == "max_determinized_states")
				{
					variant.MaxDeterminizedStates = JsonSerializer.Deserialize<int?>(ref reader, options);
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

	public override void Write(Utf8JsonWriter writer, RegexpQuery value, JsonSerializerOptions options)
	{
		if (value.Field is null)
			throw new JsonException("Unable to serialize RegexpQuery because the `Field` property is not set. Field name queries must include a valid field name.");
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

			if (value.CaseInsensitive.HasValue)
			{
				writer.WritePropertyName("case_insensitive");
				writer.WriteBooleanValue(value.CaseInsensitive.Value);
			}

			if (!string.IsNullOrEmpty(value.Flags))
			{
				writer.WritePropertyName("flags");
				writer.WriteStringValue(value.Flags);
			}

			if (value.MaxDeterminizedStates.HasValue)
			{
				writer.WritePropertyName("max_determinized_states");
				writer.WriteNumberValue(value.MaxDeterminizedStates.Value);
			}

			if (value.Rewrite is not null)
			{
				writer.WritePropertyName("rewrite");
				JsonSerializer.Serialize(writer, value.Rewrite, options);
			}

			writer.WritePropertyName("value");
			writer.WriteStringValue(value.Value);
			writer.WriteEndObject();
			writer.WriteEndObject();
			return;
		}

		throw new JsonException("Unable to retrieve client settings required to infer field.");
	}
}

[JsonConverter(typeof(RegexpQueryConverter))]
public sealed partial class RegexpQuery : SearchQuery
{
	public RegexpQuery(Field field)
	{
		if (field is null)
			throw new ArgumentNullException(nameof(field));
		Field = field;
	}

	public string? QueryName { get; set; }
	public float? Boost { get; set; }
	public bool? CaseInsensitive { get; set; }
	public string? Flags { get; set; }
	public int? MaxDeterminizedStates { get; set; }
	public string? Rewrite { get; set; }
	public string Value { get; set; }
	public Elastic.Clients.Elasticsearch.Field Field { get; set; }

	public static implicit operator Query(RegexpQuery regexpQuery) => QueryDsl.Query.Regexp(regexpQuery);
}

public sealed partial class RegexpQueryDescriptor<TDocument> : SerializableDescriptor<RegexpQueryDescriptor<TDocument>>
{
	internal RegexpQueryDescriptor(Action<RegexpQueryDescriptor<TDocument>> configure) => configure.Invoke(this);

	internal RegexpQueryDescriptor() : base()
	{
	}

	public RegexpQueryDescriptor(Field field)
	{
		if (field is null)
			throw new ArgumentNullException(nameof(field));
		FieldValue = field;
	}

	public RegexpQueryDescriptor(Expression<Func<TDocument, object>> field)
	{
		if (field is null)
			throw new ArgumentNullException(nameof(field));
		FieldValue = field;
	}

	private string? QueryNameValue { get; set; }
	private float? BoostValue { get; set; }
	private bool? CaseInsensitiveValue { get; set; }
	private Elastic.Clients.Elasticsearch.Field FieldValue { get; set; }
	private string? FlagsValue { get; set; }
	private int? MaxDeterminizedStatesValue { get; set; }
	private string? RewriteValue { get; set; }
	private string ValueValue { get; set; }

	public RegexpQueryDescriptor<TDocument> QueryName(string? queryName)
	{
		QueryNameValue = queryName;
		return Self;
	}

	public RegexpQueryDescriptor<TDocument> Boost(float? boost)
	{
		BoostValue = boost;
		return Self;
	}

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

	public RegexpQueryDescriptor<TDocument> Flags(string? flags)
	{
		FlagsValue = flags;
		return Self;
	}

	public RegexpQueryDescriptor<TDocument> MaxDeterminizedStates(int? maxDeterminizedStates)
	{
		MaxDeterminizedStatesValue = maxDeterminizedStates;
		return Self;
	}

	public RegexpQueryDescriptor<TDocument> Rewrite(string? rewrite)
	{
		RewriteValue = rewrite;
		return Self;
	}

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

		if (RewriteValue is not null)
		{
			writer.WritePropertyName("rewrite");
			JsonSerializer.Serialize(writer, RewriteValue, options);
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

	internal RegexpQueryDescriptor() : base()
	{
	}

	public RegexpQueryDescriptor(Field field)
	{
		if (field is null)
			throw new ArgumentNullException(nameof(field));
		FieldValue = field;
	}

	private string? QueryNameValue { get; set; }
	private float? BoostValue { get; set; }
	private bool? CaseInsensitiveValue { get; set; }
	private Elastic.Clients.Elasticsearch.Field FieldValue { get; set; }
	private string? FlagsValue { get; set; }
	private int? MaxDeterminizedStatesValue { get; set; }
	private string? RewriteValue { get; set; }
	private string ValueValue { get; set; }

	public RegexpQueryDescriptor QueryName(string? queryName)
	{
		QueryNameValue = queryName;
		return Self;
	}

	public RegexpQueryDescriptor Boost(float? boost)
	{
		BoostValue = boost;
		return Self;
	}

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

	public RegexpQueryDescriptor Flags(string? flags)
	{
		FlagsValue = flags;
		return Self;
	}

	public RegexpQueryDescriptor MaxDeterminizedStates(int? maxDeterminizedStates)
	{
		MaxDeterminizedStatesValue = maxDeterminizedStates;
		return Self;
	}

	public RegexpQueryDescriptor Rewrite(string? rewrite)
	{
		RewriteValue = rewrite;
		return Self;
	}

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

		if (RewriteValue is not null)
		{
			writer.WritePropertyName("rewrite");
			JsonSerializer.Serialize(writer, RewriteValue, options);
		}

		writer.WritePropertyName("value");
		writer.WriteStringValue(ValueValue);
		writer.WriteEndObject();
		writer.WriteEndObject();
	}
}