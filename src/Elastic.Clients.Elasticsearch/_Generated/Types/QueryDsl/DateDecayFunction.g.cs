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

internal sealed partial class DateDecayFunctionConverter : System.Text.Json.Serialization.JsonConverter<DateDecayFunction>
{
	private static readonly System.Text.Json.JsonEncodedText PropMultiValueMode = System.Text.Json.JsonEncodedText.Encode("multi_value_mode");

	public override DateDecayFunction Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<Elastic.Clients.Elasticsearch.Field> propField = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.QueryDsl.DecayPlacement<Elastic.Clients.Elasticsearch.DateMath, Elastic.Clients.Elasticsearch.Duration>> propPlacement = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.QueryDsl.MultiValueMode?> propMultiValueMode = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propMultiValueMode.TryRead(ref reader, options, PropMultiValueMode))
			{
				continue;
			}

			propField.Initialized = propPlacement.Initialized = true;
			reader.ReadProperty(options, out propField.Value, out propPlacement.Value);
		}

		reader.ValidateToken(System.Text.Json.JsonTokenType.EndObject);
		return new DateDecayFunction
		{
			Field = propField.Value
,
			Placement = propPlacement.Value
,
			MultiValueMode = propMultiValueMode.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, DateDecayFunction value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropMultiValueMode, value.MultiValueMode);
		writer.WriteProperty(options, value.Field, value.Placement);
		writer.WriteEndObject();
	}
}

[JsonConverter(typeof(DateDecayFunctionConverter))]
public sealed partial class DateDecayFunction : IDecayFunction
{
	public Elastic.Clients.Elasticsearch.Field Field { get; set; }

	/// <summary>
	/// <para>
	/// Determines how the distance is calculated when a field used for computing the decay contains multiple values.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.MultiValueMode? MultiValueMode { get; set; }
	public Elastic.Clients.Elasticsearch.QueryDsl.DecayPlacement<Elastic.Clients.Elasticsearch.DateMath, Elastic.Clients.Elasticsearch.Duration> Placement { get; set; }

	public string Type => "datedecayfunction";
}

public sealed partial class DateDecayFunctionDescriptor<TDocument> : SerializableDescriptor<DateDecayFunctionDescriptor<TDocument>>
{
	internal DateDecayFunctionDescriptor(Action<DateDecayFunctionDescriptor<TDocument>> configure) => configure.Invoke(this);

	public DateDecayFunctionDescriptor() : base()
	{
	}

	private Elastic.Clients.Elasticsearch.Field FieldValue { get; set; }
	private Elastic.Clients.Elasticsearch.QueryDsl.MultiValueMode? MultiValueModeValue { get; set; }
	private Elastic.Clients.Elasticsearch.QueryDsl.DecayPlacement<Elastic.Clients.Elasticsearch.DateMath, Elastic.Clients.Elasticsearch.Duration> PlacementValue { get; set; }

	public DateDecayFunctionDescriptor<TDocument> Field(Elastic.Clients.Elasticsearch.Field field)
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
	public DateDecayFunctionDescriptor<TDocument> MultiValueMode(Elastic.Clients.Elasticsearch.QueryDsl.MultiValueMode? multiValueMode)
	{
		MultiValueModeValue = multiValueMode;
		return Self;
	}

	public DateDecayFunctionDescriptor<TDocument> Placement(Elastic.Clients.Elasticsearch.QueryDsl.DecayPlacement<Elastic.Clients.Elasticsearch.DateMath, Elastic.Clients.Elasticsearch.Duration> placement)
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

	private Elastic.Clients.Elasticsearch.Field FieldValue { get; set; }
	private Elastic.Clients.Elasticsearch.QueryDsl.MultiValueMode? MultiValueModeValue { get; set; }
	private Elastic.Clients.Elasticsearch.QueryDsl.DecayPlacement<Elastic.Clients.Elasticsearch.DateMath, Elastic.Clients.Elasticsearch.Duration> PlacementValue { get; set; }

	public DateDecayFunctionDescriptor Field(Elastic.Clients.Elasticsearch.Field field)
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
	public DateDecayFunctionDescriptor MultiValueMode(Elastic.Clients.Elasticsearch.QueryDsl.MultiValueMode? multiValueMode)
	{
		MultiValueModeValue = multiValueMode;
		return Self;
	}

	public DateDecayFunctionDescriptor Placement(Elastic.Clients.Elasticsearch.QueryDsl.DecayPlacement<Elastic.Clients.Elasticsearch.DateMath, Elastic.Clients.Elasticsearch.Duration> placement)
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