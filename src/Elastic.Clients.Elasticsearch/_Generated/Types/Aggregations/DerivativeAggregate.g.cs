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

using System;
using System.Linq;
using Elastic.Clients.Elasticsearch.Serialization;

namespace Elastic.Clients.Elasticsearch.Aggregations;

internal sealed partial class DerivativeAggregateConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Aggregations.DerivativeAggregate>
{
	private static readonly System.Text.Json.JsonEncodedText PropMeta = System.Text.Json.JsonEncodedText.Encode("meta");
	private static readonly System.Text.Json.JsonEncodedText PropNormalizedValue = System.Text.Json.JsonEncodedText.Encode("normalized_value");
	private static readonly System.Text.Json.JsonEncodedText PropNormalizedValueAsString = System.Text.Json.JsonEncodedText.Encode("normalized_value_as_string");
	private static readonly System.Text.Json.JsonEncodedText PropValue = System.Text.Json.JsonEncodedText.Encode("value");
	private static readonly System.Text.Json.JsonEncodedText PropValueAsString = System.Text.Json.JsonEncodedText.Encode("value_as_string");

	public override Elastic.Clients.Elasticsearch.Aggregations.DerivativeAggregate Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<System.Collections.Generic.IReadOnlyDictionary<string, object>?> propMeta = default;
		LocalJsonValue<double?> propNormalizedValue = default;
		LocalJsonValue<string?> propNormalizedValueAsString = default;
		LocalJsonValue<double?> propValue = default;
		LocalJsonValue<string?> propValueAsString = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propMeta.TryReadProperty(ref reader, options, PropMeta, static System.Collections.Generic.IReadOnlyDictionary<string, object>? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadDictionaryValue<string, object>(o, null, null)))
			{
				continue;
			}

			if (propNormalizedValue.TryReadProperty(ref reader, options, PropNormalizedValue, null))
			{
				continue;
			}

			if (propNormalizedValueAsString.TryReadProperty(ref reader, options, PropNormalizedValueAsString, null))
			{
				continue;
			}

			if (propValue.TryReadProperty(ref reader, options, PropValue, null))
			{
				continue;
			}

			if (propValueAsString.TryReadProperty(ref reader, options, PropValueAsString, null))
			{
				continue;
			}

			if (options.UnmappedMemberHandling is System.Text.Json.Serialization.JsonUnmappedMemberHandling.Skip)
			{
				reader.Skip();
				continue;
			}

			throw new System.Text.Json.JsonException($"Unknown JSON property '{reader.GetString()}' for type '{typeToConvert.Name}'.");
		}

		reader.ValidateToken(System.Text.Json.JsonTokenType.EndObject);
		return new Elastic.Clients.Elasticsearch.Aggregations.DerivativeAggregate(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Meta = propMeta.Value,
			NormalizedValue = propNormalizedValue.Value,
			NormalizedValueAsString = propNormalizedValueAsString.Value,
			Value = propValue.Value,
			ValueAsString = propValueAsString.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Aggregations.DerivativeAggregate value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropMeta, value.Meta, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.IReadOnlyDictionary<string, object>? v) => w.WriteDictionaryValue<string, object>(o, v, null, null));
		writer.WriteProperty(options, PropNormalizedValue, value.NormalizedValue, null, null);
		writer.WriteProperty(options, PropNormalizedValueAsString, value.NormalizedValueAsString, null, null);
		writer.WriteProperty(options, PropValue, value.Value, null, null);
		writer.WriteProperty(options, PropValueAsString, value.ValueAsString, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Aggregations.DerivativeAggregateConverter))]
public sealed partial class DerivativeAggregate : Elastic.Clients.Elasticsearch.Aggregations.IAggregate
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public DerivativeAggregate(double? value)
	{
		Value = value;
	}
#if NET7_0_OR_GREATER
	public DerivativeAggregate()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public DerivativeAggregate()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal DerivativeAggregate(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	public System.Collections.Generic.IReadOnlyDictionary<string, object>? Meta { get; set; }
	public double? NormalizedValue { get; set; }
	public string? NormalizedValueAsString { get; set; }

	string Elastic.Clients.Elasticsearch.Aggregations.IAggregate.Type => "derivative";

	/// <summary>
	/// <para>
	/// The metric value. A missing value generally means that there was no data to aggregate,
	/// unless specified otherwise.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	double? Value { get; set; }
	public string? ValueAsString { get; set; }
}