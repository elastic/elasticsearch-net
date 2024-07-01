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

internal sealed partial class TermsQueryConverter : JsonConverter<TermsQuery>
{
	public override TermsQuery Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		if (reader.TokenType != JsonTokenType.StartObject)
			throw new JsonException("Unexpected JSON detected.");
		var variant = new TermsQuery();
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

				if (property == "_name")
				{
					variant.QueryName = JsonSerializer.Deserialize<string?>(ref reader, options);
					continue;
				}

				variant.Field = property;
				reader.Read();
				variant.Term = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.QueryDsl.TermsQueryField>(ref reader, options);
			}
		}

		return variant;
	}

	public override void Write(Utf8JsonWriter writer, TermsQuery value, JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		if (value.Field is not null && value.Term is not null)
		{
			if (!options.TryGetClientSettings(out var settings))
			{
				ThrowHelper.ThrowJsonExceptionForMissingSettings();
			}

			var propertyName = settings.Inferrer.Field(value.Field);
			writer.WritePropertyName(propertyName);
			JsonSerializer.Serialize(writer, value.Term, options);
		}

		if (value.Boost.HasValue)
		{
			writer.WritePropertyName("boost");
			writer.WriteNumberValue(value.Boost.Value);
		}

		if (!string.IsNullOrEmpty(value.QueryName))
		{
			writer.WritePropertyName("_name");
			writer.WriteStringValue(value.QueryName);
		}

		writer.WriteEndObject();
	}
}

[JsonConverter(typeof(TermsQueryConverter))]
public sealed partial class TermsQuery
{
	/// <summary>
	/// <para>Floating point number used to decrease or increase the relevance scores of the query.<br/>Boost values are relative to the default value of 1.0.<br/>A boost value between 0 and 1.0 decreases the relevance score.<br/>A value greater than 1.0 increases the relevance score.</para>
	/// </summary>
	public float? Boost { get; set; }
	public Elastic.Clients.Elasticsearch.Field Field { get; set; }
	public string? QueryName { get; set; }
	public Elastic.Clients.Elasticsearch.QueryDsl.TermsQueryField Term { get; set; }

	public static implicit operator Elastic.Clients.Elasticsearch.QueryDsl.Query(TermsQuery termsQuery) => Elastic.Clients.Elasticsearch.QueryDsl.Query.Terms(termsQuery);
}

public sealed partial class TermsQueryDescriptor<TDocument> : SerializableDescriptor<TermsQueryDescriptor<TDocument>>
{
	internal TermsQueryDescriptor(Action<TermsQueryDescriptor<TDocument>> configure) => configure.Invoke(this);

	public TermsQueryDescriptor() : base()
	{
	}

	private float? BoostValue { get; set; }
	private Elastic.Clients.Elasticsearch.Field FieldValue { get; set; }
	private string? QueryNameValue { get; set; }
	private Elastic.Clients.Elasticsearch.QueryDsl.TermsQueryField TermValue { get; set; }

	/// <summary>
	/// <para>Floating point number used to decrease or increase the relevance scores of the query.<br/>Boost values are relative to the default value of 1.0.<br/>A boost value between 0 and 1.0 decreases the relevance score.<br/>A value greater than 1.0 increases the relevance score.</para>
	/// </summary>
	public TermsQueryDescriptor<TDocument> Boost(float? boost)
	{
		BoostValue = boost;
		return Self;
	}

	public TermsQueryDescriptor<TDocument> Field(Elastic.Clients.Elasticsearch.Field field)
	{
		FieldValue = field;
		return Self;
	}

	public TermsQueryDescriptor<TDocument> Field<TValue>(Expression<Func<TDocument, TValue>> field)
	{
		FieldValue = field;
		return Self;
	}

	public TermsQueryDescriptor<TDocument> Field(Expression<Func<TDocument, object>> field)
	{
		FieldValue = field;
		return Self;
	}

	public TermsQueryDescriptor<TDocument> QueryName(string? queryName)
	{
		QueryNameValue = queryName;
		return Self;
	}

	public TermsQueryDescriptor<TDocument> Term(Elastic.Clients.Elasticsearch.QueryDsl.TermsQueryField term)
	{
		TermValue = term;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (FieldValue is not null && TermValue is not null)
		{
			var propertyName = settings.Inferrer.Field(FieldValue);
			writer.WritePropertyName(propertyName);
			JsonSerializer.Serialize(writer, TermValue, options);
		}

		if (BoostValue.HasValue)
		{
			writer.WritePropertyName("boost");
			writer.WriteNumberValue(BoostValue.Value);
		}

		if (!string.IsNullOrEmpty(QueryNameValue))
		{
			writer.WritePropertyName("_name");
			writer.WriteStringValue(QueryNameValue);
		}

		writer.WriteEndObject();
	}
}

public sealed partial class TermsQueryDescriptor : SerializableDescriptor<TermsQueryDescriptor>
{
	internal TermsQueryDescriptor(Action<TermsQueryDescriptor> configure) => configure.Invoke(this);

	public TermsQueryDescriptor() : base()
	{
	}

	private float? BoostValue { get; set; }
	private Elastic.Clients.Elasticsearch.Field FieldValue { get; set; }
	private string? QueryNameValue { get; set; }
	private Elastic.Clients.Elasticsearch.QueryDsl.TermsQueryField TermValue { get; set; }

	/// <summary>
	/// <para>Floating point number used to decrease or increase the relevance scores of the query.<br/>Boost values are relative to the default value of 1.0.<br/>A boost value between 0 and 1.0 decreases the relevance score.<br/>A value greater than 1.0 increases the relevance score.</para>
	/// </summary>
	public TermsQueryDescriptor Boost(float? boost)
	{
		BoostValue = boost;
		return Self;
	}

	public TermsQueryDescriptor Field(Elastic.Clients.Elasticsearch.Field field)
	{
		FieldValue = field;
		return Self;
	}

	public TermsQueryDescriptor Field<TDocument, TValue>(Expression<Func<TDocument, TValue>> field)
	{
		FieldValue = field;
		return Self;
	}

	public TermsQueryDescriptor Field<TDocument>(Expression<Func<TDocument, object>> field)
	{
		FieldValue = field;
		return Self;
	}

	public TermsQueryDescriptor QueryName(string? queryName)
	{
		QueryNameValue = queryName;
		return Self;
	}

	public TermsQueryDescriptor Term(Elastic.Clients.Elasticsearch.QueryDsl.TermsQueryField term)
	{
		TermValue = term;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (FieldValue is not null && TermValue is not null)
		{
			var propertyName = settings.Inferrer.Field(FieldValue);
			writer.WritePropertyName(propertyName);
			JsonSerializer.Serialize(writer, TermValue, options);
		}

		if (BoostValue.HasValue)
		{
			writer.WritePropertyName("boost");
			writer.WriteNumberValue(BoostValue.Value);
		}

		if (!string.IsNullOrEmpty(QueryNameValue))
		{
			writer.WritePropertyName("_name");
			writer.WriteStringValue(QueryNameValue);
		}

		writer.WriteEndObject();
	}
}