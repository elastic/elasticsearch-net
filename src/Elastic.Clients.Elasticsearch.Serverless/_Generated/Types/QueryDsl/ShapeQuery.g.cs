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

using Elastic.Clients.Elasticsearch.Serverless.Fluent;
using Elastic.Clients.Elasticsearch.Serverless.Serialization;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch.Serverless.QueryDsl;

internal sealed partial class ShapeQueryConverter : JsonConverter<ShapeQuery>
{
	public override ShapeQuery Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		if (reader.TokenType != JsonTokenType.StartObject)
			throw new JsonException("Unexpected JSON detected.");
		var variant = new ShapeQuery();
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

				if (property == "ignore_unmapped")
				{
					variant.IgnoreUnmapped = JsonSerializer.Deserialize<bool?>(ref reader, options);
					continue;
				}

				if (property == "_name")
				{
					variant.QueryName = JsonSerializer.Deserialize<string?>(ref reader, options);
					continue;
				}

				variant.Field = property;
				reader.Read();
				variant.Shape = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.Serverless.QueryDsl.ShapeFieldQuery>(ref reader, options);
			}
		}

		return variant;
	}

	public override void Write(Utf8JsonWriter writer, ShapeQuery value, JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		if (value.Field is not null && value.Shape is not null)
		{
			if (!options.TryGetClientSettings(out var settings))
			{
				ThrowHelper.ThrowJsonExceptionForMissingSettings();
			}

			var propertyName = settings.Inferrer.Field(value.Field);
			writer.WritePropertyName(propertyName);
			JsonSerializer.Serialize(writer, value.Shape, options);
		}

		if (value.Boost.HasValue)
		{
			writer.WritePropertyName("boost");
			writer.WriteNumberValue(value.Boost.Value);
		}

		if (value.IgnoreUnmapped.HasValue)
		{
			writer.WritePropertyName("ignore_unmapped");
			writer.WriteBooleanValue(value.IgnoreUnmapped.Value);
		}

		if (!string.IsNullOrEmpty(value.QueryName))
		{
			writer.WritePropertyName("_name");
			writer.WriteStringValue(value.QueryName);
		}

		writer.WriteEndObject();
	}
}

[JsonConverter(typeof(ShapeQueryConverter))]
public sealed partial class ShapeQuery
{
	/// <summary>
	/// <para>
	/// Floating point number used to decrease or increase the relevance scores of the query.
	/// Boost values are relative to the default value of 1.0.
	/// A boost value between 0 and 1.0 decreases the relevance score.
	/// A value greater than 1.0 increases the relevance score.
	/// </para>
	/// </summary>
	public float? Boost { get; set; }
	public Elastic.Clients.Elasticsearch.Serverless.Field Field { get; set; }

	/// <summary>
	/// <para>
	/// When set to <c>true</c> the query ignores an unmapped field and will not match any documents.
	/// </para>
	/// </summary>
	public bool? IgnoreUnmapped { get; set; }
	public string? QueryName { get; set; }
	public Elastic.Clients.Elasticsearch.Serverless.QueryDsl.ShapeFieldQuery Shape { get; set; }

	public static implicit operator Elastic.Clients.Elasticsearch.Serverless.QueryDsl.Query(ShapeQuery shapeQuery) => Elastic.Clients.Elasticsearch.Serverless.QueryDsl.Query.Shape(shapeQuery);
}

public sealed partial class ShapeQueryDescriptor<TDocument> : SerializableDescriptor<ShapeQueryDescriptor<TDocument>>
{
	internal ShapeQueryDescriptor(Action<ShapeQueryDescriptor<TDocument>> configure) => configure.Invoke(this);

	public ShapeQueryDescriptor() : base()
	{
	}

	private float? BoostValue { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.Field FieldValue { get; set; }
	private bool? IgnoreUnmappedValue { get; set; }
	private string? QueryNameValue { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.QueryDsl.ShapeFieldQuery ShapeValue { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.QueryDsl.ShapeFieldQueryDescriptor<TDocument> ShapeDescriptor { get; set; }
	private Action<Elastic.Clients.Elasticsearch.Serverless.QueryDsl.ShapeFieldQueryDescriptor<TDocument>> ShapeDescriptorAction { get; set; }

	/// <summary>
	/// <para>
	/// Floating point number used to decrease or increase the relevance scores of the query.
	/// Boost values are relative to the default value of 1.0.
	/// A boost value between 0 and 1.0 decreases the relevance score.
	/// A value greater than 1.0 increases the relevance score.
	/// </para>
	/// </summary>
	public ShapeQueryDescriptor<TDocument> Boost(float? boost)
	{
		BoostValue = boost;
		return Self;
	}

	public ShapeQueryDescriptor<TDocument> Field(Elastic.Clients.Elasticsearch.Serverless.Field field)
	{
		FieldValue = field;
		return Self;
	}

	public ShapeQueryDescriptor<TDocument> Field<TValue>(Expression<Func<TDocument, TValue>> field)
	{
		FieldValue = field;
		return Self;
	}

	public ShapeQueryDescriptor<TDocument> Field(Expression<Func<TDocument, object>> field)
	{
		FieldValue = field;
		return Self;
	}

	/// <summary>
	/// <para>
	/// When set to <c>true</c> the query ignores an unmapped field and will not match any documents.
	/// </para>
	/// </summary>
	public ShapeQueryDescriptor<TDocument> IgnoreUnmapped(bool? ignoreUnmapped = true)
	{
		IgnoreUnmappedValue = ignoreUnmapped;
		return Self;
	}

	public ShapeQueryDescriptor<TDocument> QueryName(string? queryName)
	{
		QueryNameValue = queryName;
		return Self;
	}

	public ShapeQueryDescriptor<TDocument> Shape(Elastic.Clients.Elasticsearch.Serverless.QueryDsl.ShapeFieldQuery shape)
	{
		ShapeDescriptor = null;
		ShapeDescriptorAction = null;
		ShapeValue = shape;
		return Self;
	}

	public ShapeQueryDescriptor<TDocument> Shape(Elastic.Clients.Elasticsearch.Serverless.QueryDsl.ShapeFieldQueryDescriptor<TDocument> descriptor)
	{
		ShapeValue = null;
		ShapeDescriptorAction = null;
		ShapeDescriptor = descriptor;
		return Self;
	}

	public ShapeQueryDescriptor<TDocument> Shape(Action<Elastic.Clients.Elasticsearch.Serverless.QueryDsl.ShapeFieldQueryDescriptor<TDocument>> configure)
	{
		ShapeValue = null;
		ShapeDescriptor = null;
		ShapeDescriptorAction = configure;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (FieldValue is not null && ShapeValue is not null)
		{
			var propertyName = settings.Inferrer.Field(FieldValue);
			writer.WritePropertyName(propertyName);
			JsonSerializer.Serialize(writer, ShapeValue, options);
		}

		if (BoostValue.HasValue)
		{
			writer.WritePropertyName("boost");
			writer.WriteNumberValue(BoostValue.Value);
		}

		if (IgnoreUnmappedValue.HasValue)
		{
			writer.WritePropertyName("ignore_unmapped");
			writer.WriteBooleanValue(IgnoreUnmappedValue.Value);
		}

		if (!string.IsNullOrEmpty(QueryNameValue))
		{
			writer.WritePropertyName("_name");
			writer.WriteStringValue(QueryNameValue);
		}

		writer.WriteEndObject();
	}
}

public sealed partial class ShapeQueryDescriptor : SerializableDescriptor<ShapeQueryDescriptor>
{
	internal ShapeQueryDescriptor(Action<ShapeQueryDescriptor> configure) => configure.Invoke(this);

	public ShapeQueryDescriptor() : base()
	{
	}

	private float? BoostValue { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.Field FieldValue { get; set; }
	private bool? IgnoreUnmappedValue { get; set; }
	private string? QueryNameValue { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.QueryDsl.ShapeFieldQuery ShapeValue { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.QueryDsl.ShapeFieldQueryDescriptor ShapeDescriptor { get; set; }
	private Action<Elastic.Clients.Elasticsearch.Serverless.QueryDsl.ShapeFieldQueryDescriptor> ShapeDescriptorAction { get; set; }

	/// <summary>
	/// <para>
	/// Floating point number used to decrease or increase the relevance scores of the query.
	/// Boost values are relative to the default value of 1.0.
	/// A boost value between 0 and 1.0 decreases the relevance score.
	/// A value greater than 1.0 increases the relevance score.
	/// </para>
	/// </summary>
	public ShapeQueryDescriptor Boost(float? boost)
	{
		BoostValue = boost;
		return Self;
	}

	public ShapeQueryDescriptor Field(Elastic.Clients.Elasticsearch.Serverless.Field field)
	{
		FieldValue = field;
		return Self;
	}

	public ShapeQueryDescriptor Field<TDocument, TValue>(Expression<Func<TDocument, TValue>> field)
	{
		FieldValue = field;
		return Self;
	}

	public ShapeQueryDescriptor Field<TDocument>(Expression<Func<TDocument, object>> field)
	{
		FieldValue = field;
		return Self;
	}

	/// <summary>
	/// <para>
	/// When set to <c>true</c> the query ignores an unmapped field and will not match any documents.
	/// </para>
	/// </summary>
	public ShapeQueryDescriptor IgnoreUnmapped(bool? ignoreUnmapped = true)
	{
		IgnoreUnmappedValue = ignoreUnmapped;
		return Self;
	}

	public ShapeQueryDescriptor QueryName(string? queryName)
	{
		QueryNameValue = queryName;
		return Self;
	}

	public ShapeQueryDescriptor Shape(Elastic.Clients.Elasticsearch.Serverless.QueryDsl.ShapeFieldQuery shape)
	{
		ShapeDescriptor = null;
		ShapeDescriptorAction = null;
		ShapeValue = shape;
		return Self;
	}

	public ShapeQueryDescriptor Shape(Elastic.Clients.Elasticsearch.Serverless.QueryDsl.ShapeFieldQueryDescriptor descriptor)
	{
		ShapeValue = null;
		ShapeDescriptorAction = null;
		ShapeDescriptor = descriptor;
		return Self;
	}

	public ShapeQueryDescriptor Shape(Action<Elastic.Clients.Elasticsearch.Serverless.QueryDsl.ShapeFieldQueryDescriptor> configure)
	{
		ShapeValue = null;
		ShapeDescriptor = null;
		ShapeDescriptorAction = configure;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (FieldValue is not null && ShapeValue is not null)
		{
			var propertyName = settings.Inferrer.Field(FieldValue);
			writer.WritePropertyName(propertyName);
			JsonSerializer.Serialize(writer, ShapeValue, options);
		}

		if (BoostValue.HasValue)
		{
			writer.WritePropertyName("boost");
			writer.WriteNumberValue(BoostValue.Value);
		}

		if (IgnoreUnmappedValue.HasValue)
		{
			writer.WritePropertyName("ignore_unmapped");
			writer.WriteBooleanValue(IgnoreUnmappedValue.Value);
		}

		if (!string.IsNullOrEmpty(QueryNameValue))
		{
			writer.WritePropertyName("_name");
			writer.WriteStringValue(QueryNameValue);
		}

		writer.WriteEndObject();
	}
}