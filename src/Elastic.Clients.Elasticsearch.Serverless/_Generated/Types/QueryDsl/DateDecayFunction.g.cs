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

internal sealed partial class DateDecayFunctionConverter : JsonConverter<DateDecayFunction>
{
	public override DateDecayFunction Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		if (reader.TokenType != JsonTokenType.StartObject)
			throw new JsonException("Unexpected JSON detected.");
		var variant = new DateDecayFunction();
		while (reader.Read() && reader.TokenType != JsonTokenType.EndObject)
		{
			if (reader.TokenType == JsonTokenType.PropertyName)
			{
				var property = reader.GetString();
				if (property == "multi_value_mode")
				{
					variant.MultiValueMode = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.Serverless.QueryDsl.MultiValueMode?>(ref reader, options);
					continue;
				}

				variant.Field = property;
				reader.Read();
				variant.Placement = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.Serverless.QueryDsl.DecayPlacement<Elastic.Clients.Elasticsearch.Serverless.DateMath, Elastic.Clients.Elasticsearch.Serverless.Duration>>(ref reader, options);
			}
		}

		return variant;
	}

	public override void Write(Utf8JsonWriter writer, DateDecayFunction value, JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		if (value.Field is not null && value.Placement is not null)
		{
			if (!options.TryGetClientSettings(out var settings))
			{
				ThrowHelper.ThrowJsonExceptionForMissingSettings();
			}

			var propertyName = settings.Inferrer.Field(value.Field);
			writer.WritePropertyName(propertyName);
			JsonSerializer.Serialize(writer, value.Placement, options);
		}

		if (value.MultiValueMode is not null)
		{
			writer.WritePropertyName("multi_value_mode");
			JsonSerializer.Serialize(writer, value.MultiValueMode, options);
		}

		writer.WriteEndObject();
	}
}

[JsonConverter(typeof(DateDecayFunctionConverter))]
public sealed partial class DateDecayFunction
{
	public Elastic.Clients.Elasticsearch.Serverless.Field Field { get; set; }

	/// <summary>
	/// <para>
	/// Determines how the distance is calculated when a field used for computing the decay contains multiple values.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Serverless.QueryDsl.MultiValueMode? MultiValueMode { get; set; }
	public Elastic.Clients.Elasticsearch.Serverless.QueryDsl.DecayPlacement<Elastic.Clients.Elasticsearch.Serverless.DateMath, Elastic.Clients.Elasticsearch.Serverless.Duration> Placement { get; set; }
}

public sealed partial class DateDecayFunctionDescriptor<TDocument> : SerializableDescriptor<DateDecayFunctionDescriptor<TDocument>>
{
	internal DateDecayFunctionDescriptor(Action<DateDecayFunctionDescriptor<TDocument>> configure) => configure.Invoke(this);

	public DateDecayFunctionDescriptor() : base()
	{
	}

	private Elastic.Clients.Elasticsearch.Serverless.Field FieldValue { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.QueryDsl.MultiValueMode? MultiValueModeValue { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.QueryDsl.DecayPlacement<Elastic.Clients.Elasticsearch.Serverless.DateMath, Elastic.Clients.Elasticsearch.Serverless.Duration> PlacementValue { get; set; }

	public DateDecayFunctionDescriptor<TDocument> Field(Elastic.Clients.Elasticsearch.Serverless.Field field)
	{
		FieldValue = field;
		return Self;
	}

	public DateDecayFunctionDescriptor<TDocument> Field<TValue>(Expression<Func<TDocument, TValue>> field)
	{
		FieldValue = field;
		return Self;
	}

	public DateDecayFunctionDescriptor<TDocument> Field(Expression<Func<TDocument, object>> field)
	{
		FieldValue = field;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Determines how the distance is calculated when a field used for computing the decay contains multiple values.
	/// </para>
	/// </summary>
	public DateDecayFunctionDescriptor<TDocument> MultiValueMode(Elastic.Clients.Elasticsearch.Serverless.QueryDsl.MultiValueMode? multiValueMode)
	{
		MultiValueModeValue = multiValueMode;
		return Self;
	}

	public DateDecayFunctionDescriptor<TDocument> Placement(Elastic.Clients.Elasticsearch.Serverless.QueryDsl.DecayPlacement<Elastic.Clients.Elasticsearch.Serverless.DateMath, Elastic.Clients.Elasticsearch.Serverless.Duration> placement)
	{
		PlacementValue = placement;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (FieldValue is not null && PlacementValue is not null)
		{
			var propertyName = settings.Inferrer.Field(FieldValue);
			writer.WritePropertyName(propertyName);
			JsonSerializer.Serialize(writer, PlacementValue, options);
		}

		if (MultiValueModeValue is not null)
		{
			writer.WritePropertyName("multi_value_mode");
			JsonSerializer.Serialize(writer, MultiValueModeValue, options);
		}

		writer.WriteEndObject();
	}
}

public sealed partial class DateDecayFunctionDescriptor : SerializableDescriptor<DateDecayFunctionDescriptor>
{
	internal DateDecayFunctionDescriptor(Action<DateDecayFunctionDescriptor> configure) => configure.Invoke(this);

	public DateDecayFunctionDescriptor() : base()
	{
	}

	private Elastic.Clients.Elasticsearch.Serverless.Field FieldValue { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.QueryDsl.MultiValueMode? MultiValueModeValue { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.QueryDsl.DecayPlacement<Elastic.Clients.Elasticsearch.Serverless.DateMath, Elastic.Clients.Elasticsearch.Serverless.Duration> PlacementValue { get; set; }

	public DateDecayFunctionDescriptor Field(Elastic.Clients.Elasticsearch.Serverless.Field field)
	{
		FieldValue = field;
		return Self;
	}

	public DateDecayFunctionDescriptor Field<TDocument, TValue>(Expression<Func<TDocument, TValue>> field)
	{
		FieldValue = field;
		return Self;
	}

	public DateDecayFunctionDescriptor Field<TDocument>(Expression<Func<TDocument, object>> field)
	{
		FieldValue = field;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Determines how the distance is calculated when a field used for computing the decay contains multiple values.
	/// </para>
	/// </summary>
	public DateDecayFunctionDescriptor MultiValueMode(Elastic.Clients.Elasticsearch.Serverless.QueryDsl.MultiValueMode? multiValueMode)
	{
		MultiValueModeValue = multiValueMode;
		return Self;
	}

	public DateDecayFunctionDescriptor Placement(Elastic.Clients.Elasticsearch.Serverless.QueryDsl.DecayPlacement<Elastic.Clients.Elasticsearch.Serverless.DateMath, Elastic.Clients.Elasticsearch.Serverless.Duration> placement)
	{
		PlacementValue = placement;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (FieldValue is not null && PlacementValue is not null)
		{
			var propertyName = settings.Inferrer.Field(FieldValue);
			writer.WritePropertyName(propertyName);
			JsonSerializer.Serialize(writer, PlacementValue, options);
		}

		if (MultiValueModeValue is not null)
		{
			writer.WritePropertyName("multi_value_mode");
			JsonSerializer.Serialize(writer, MultiValueModeValue, options);
		}

		writer.WriteEndObject();
	}
}