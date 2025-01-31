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

internal sealed partial class GeoDecayFunctionConverter : System.Text.Json.Serialization.JsonConverter<GeoDecayFunction>
{
	private static readonly System.Text.Json.JsonEncodedText PropMultiValueMode = System.Text.Json.JsonEncodedText.Encode("multi_value_mode");

	public override GeoDecayFunction Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<Elastic.Clients.Elasticsearch.Field> propField = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.QueryDsl.DecayPlacement<Elastic.Clients.Elasticsearch.GeoLocation, string>> propPlacement = default;
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
		return new GeoDecayFunction
		{
			Field = propField.Value
,
			Placement = propPlacement.Value
,
			MultiValueMode = propMultiValueMode.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, GeoDecayFunction value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropMultiValueMode, value.MultiValueMode);
		writer.WriteProperty(options, value.Field, value.Placement);
		writer.WriteEndObject();
	}
}

[JsonConverter(typeof(GeoDecayFunctionConverter))]
public sealed partial class GeoDecayFunction : IDecayFunction
{
	public Elastic.Clients.Elasticsearch.Field Field { get; set; }

	/// <summary>
	/// <para>
	/// Determines how the distance is calculated when a field used for computing the decay contains multiple values.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryDsl.MultiValueMode? MultiValueMode { get; set; }
	public Elastic.Clients.Elasticsearch.QueryDsl.DecayPlacement<Elastic.Clients.Elasticsearch.GeoLocation, string> Placement { get; set; }

	public string Type => "geodecayfunction";
}

public sealed partial class GeoDecayFunctionDescriptor<TDocument> : SerializableDescriptor<GeoDecayFunctionDescriptor<TDocument>>
{
	internal GeoDecayFunctionDescriptor(Action<GeoDecayFunctionDescriptor<TDocument>> configure) => configure.Invoke(this);

	public GeoDecayFunctionDescriptor() : base()
	{
	}

	private Elastic.Clients.Elasticsearch.Field FieldValue { get; set; }
	private Elastic.Clients.Elasticsearch.QueryDsl.MultiValueMode? MultiValueModeValue { get; set; }
	private Elastic.Clients.Elasticsearch.QueryDsl.DecayPlacement<Elastic.Clients.Elasticsearch.GeoLocation, string> PlacementValue { get; set; }

	public GeoDecayFunctionDescriptor<TDocument> Field(Elastic.Clients.Elasticsearch.Field field)
	{
		FieldValue = field;
		return Self;
	}

	public GeoDecayFunctionDescriptor<TDocument> Field<TValue>(Expression<Func<TDocument, TValue>> field)
	{
		FieldValue = field;
		return Self;
	}

	public GeoDecayFunctionDescriptor<TDocument> Field(Expression<Func<TDocument, object>> field)
	{
		FieldValue = field;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Determines how the distance is calculated when a field used for computing the decay contains multiple values.
	/// </para>
	/// </summary>
	public GeoDecayFunctionDescriptor<TDocument> MultiValueMode(Elastic.Clients.Elasticsearch.QueryDsl.MultiValueMode? multiValueMode)
	{
		MultiValueModeValue = multiValueMode;
		return Self;
	}

	public GeoDecayFunctionDescriptor<TDocument> Placement(Elastic.Clients.Elasticsearch.QueryDsl.DecayPlacement<Elastic.Clients.Elasticsearch.GeoLocation, string> placement)
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

public sealed partial class GeoDecayFunctionDescriptor : SerializableDescriptor<GeoDecayFunctionDescriptor>
{
	internal GeoDecayFunctionDescriptor(Action<GeoDecayFunctionDescriptor> configure) => configure.Invoke(this);

	public GeoDecayFunctionDescriptor() : base()
	{
	}

	private Elastic.Clients.Elasticsearch.Field FieldValue { get; set; }
	private Elastic.Clients.Elasticsearch.QueryDsl.MultiValueMode? MultiValueModeValue { get; set; }
	private Elastic.Clients.Elasticsearch.QueryDsl.DecayPlacement<Elastic.Clients.Elasticsearch.GeoLocation, string> PlacementValue { get; set; }

	public GeoDecayFunctionDescriptor Field(Elastic.Clients.Elasticsearch.Field field)
	{
		FieldValue = field;
		return Self;
	}

	public GeoDecayFunctionDescriptor Field<TDocument, TValue>(Expression<Func<TDocument, TValue>> field)
	{
		FieldValue = field;
		return Self;
	}

	public GeoDecayFunctionDescriptor Field<TDocument>(Expression<Func<TDocument, object>> field)
	{
		FieldValue = field;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Determines how the distance is calculated when a field used for computing the decay contains multiple values.
	/// </para>
	/// </summary>
	public GeoDecayFunctionDescriptor MultiValueMode(Elastic.Clients.Elasticsearch.QueryDsl.MultiValueMode? multiValueMode)
	{
		MultiValueModeValue = multiValueMode;
		return Self;
	}

	public GeoDecayFunctionDescriptor Placement(Elastic.Clients.Elasticsearch.QueryDsl.DecayPlacement<Elastic.Clients.Elasticsearch.GeoLocation, string> placement)
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