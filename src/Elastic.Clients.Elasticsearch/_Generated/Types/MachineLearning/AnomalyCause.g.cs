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

namespace Elastic.Clients.Elasticsearch.MachineLearning;

internal sealed partial class AnomalyCauseConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.MachineLearning.AnomalyCause>
{
	private static readonly System.Text.Json.JsonEncodedText PropActual = System.Text.Json.JsonEncodedText.Encode("actual");
	private static readonly System.Text.Json.JsonEncodedText PropByFieldName = System.Text.Json.JsonEncodedText.Encode("by_field_name");
	private static readonly System.Text.Json.JsonEncodedText PropByFieldValue = System.Text.Json.JsonEncodedText.Encode("by_field_value");
	private static readonly System.Text.Json.JsonEncodedText PropCorrelatedByFieldValue = System.Text.Json.JsonEncodedText.Encode("correlated_by_field_value");
	private static readonly System.Text.Json.JsonEncodedText PropFieldName = System.Text.Json.JsonEncodedText.Encode("field_name");
	private static readonly System.Text.Json.JsonEncodedText PropFunction = System.Text.Json.JsonEncodedText.Encode("function");
	private static readonly System.Text.Json.JsonEncodedText PropFunctionDescription = System.Text.Json.JsonEncodedText.Encode("function_description");
	private static readonly System.Text.Json.JsonEncodedText PropGeoResults = System.Text.Json.JsonEncodedText.Encode("geo_results");
	private static readonly System.Text.Json.JsonEncodedText PropInfluencers = System.Text.Json.JsonEncodedText.Encode("influencers");
	private static readonly System.Text.Json.JsonEncodedText PropOverFieldName = System.Text.Json.JsonEncodedText.Encode("over_field_name");
	private static readonly System.Text.Json.JsonEncodedText PropOverFieldValue = System.Text.Json.JsonEncodedText.Encode("over_field_value");
	private static readonly System.Text.Json.JsonEncodedText PropPartitionFieldName = System.Text.Json.JsonEncodedText.Encode("partition_field_name");
	private static readonly System.Text.Json.JsonEncodedText PropPartitionFieldValue = System.Text.Json.JsonEncodedText.Encode("partition_field_value");
	private static readonly System.Text.Json.JsonEncodedText PropProbability = System.Text.Json.JsonEncodedText.Encode("probability");
	private static readonly System.Text.Json.JsonEncodedText PropTypical = System.Text.Json.JsonEncodedText.Encode("typical");

	public override Elastic.Clients.Elasticsearch.MachineLearning.AnomalyCause Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<System.Collections.Generic.IReadOnlyCollection<double>?> propActual = default;
		LocalJsonValue<string?> propByFieldName = default;
		LocalJsonValue<string?> propByFieldValue = default;
		LocalJsonValue<string?> propCorrelatedByFieldValue = default;
		LocalJsonValue<string?> propFieldName = default;
		LocalJsonValue<string?> propFunction = default;
		LocalJsonValue<string?> propFunctionDescription = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.MachineLearning.GeoResults?> propGeoResults = default;
		LocalJsonValue<System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.MachineLearning.Influence>?> propInfluencers = default;
		LocalJsonValue<string?> propOverFieldName = default;
		LocalJsonValue<string?> propOverFieldValue = default;
		LocalJsonValue<string?> propPartitionFieldName = default;
		LocalJsonValue<string?> propPartitionFieldValue = default;
		LocalJsonValue<double> propProbability = default;
		LocalJsonValue<System.Collections.Generic.IReadOnlyCollection<double>?> propTypical = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propActual.TryReadProperty(ref reader, options, PropActual, static System.Collections.Generic.IReadOnlyCollection<double>? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadCollectionValue<double>(o, null)))
			{
				continue;
			}

			if (propByFieldName.TryReadProperty(ref reader, options, PropByFieldName, null))
			{
				continue;
			}

			if (propByFieldValue.TryReadProperty(ref reader, options, PropByFieldValue, null))
			{
				continue;
			}

			if (propCorrelatedByFieldValue.TryReadProperty(ref reader, options, PropCorrelatedByFieldValue, null))
			{
				continue;
			}

			if (propFieldName.TryReadProperty(ref reader, options, PropFieldName, null))
			{
				continue;
			}

			if (propFunction.TryReadProperty(ref reader, options, PropFunction, null))
			{
				continue;
			}

			if (propFunctionDescription.TryReadProperty(ref reader, options, PropFunctionDescription, null))
			{
				continue;
			}

			if (propGeoResults.TryReadProperty(ref reader, options, PropGeoResults, null))
			{
				continue;
			}

			if (propInfluencers.TryReadProperty(ref reader, options, PropInfluencers, static System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.MachineLearning.Influence>? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadCollectionValue<Elastic.Clients.Elasticsearch.MachineLearning.Influence>(o, null)))
			{
				continue;
			}

			if (propOverFieldName.TryReadProperty(ref reader, options, PropOverFieldName, null))
			{
				continue;
			}

			if (propOverFieldValue.TryReadProperty(ref reader, options, PropOverFieldValue, null))
			{
				continue;
			}

			if (propPartitionFieldName.TryReadProperty(ref reader, options, PropPartitionFieldName, null))
			{
				continue;
			}

			if (propPartitionFieldValue.TryReadProperty(ref reader, options, PropPartitionFieldValue, null))
			{
				continue;
			}

			if (propProbability.TryReadProperty(ref reader, options, PropProbability, null))
			{
				continue;
			}

			if (propTypical.TryReadProperty(ref reader, options, PropTypical, static System.Collections.Generic.IReadOnlyCollection<double>? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadCollectionValue<double>(o, null)))
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
		return new Elastic.Clients.Elasticsearch.MachineLearning.AnomalyCause(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Actual = propActual.Value,
			ByFieldName = propByFieldName.Value,
			ByFieldValue = propByFieldValue.Value,
			CorrelatedByFieldValue = propCorrelatedByFieldValue.Value,
			FieldName = propFieldName.Value,
			Function = propFunction.Value,
			FunctionDescription = propFunctionDescription.Value,
			GeoResults = propGeoResults.Value,
			Influencers = propInfluencers.Value,
			OverFieldName = propOverFieldName.Value,
			OverFieldValue = propOverFieldValue.Value,
			PartitionFieldName = propPartitionFieldName.Value,
			PartitionFieldValue = propPartitionFieldValue.Value,
			Probability = propProbability.Value,
			Typical = propTypical.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.MachineLearning.AnomalyCause value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropActual, value.Actual, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.IReadOnlyCollection<double>? v) => w.WriteCollectionValue<double>(o, v, null));
		writer.WriteProperty(options, PropByFieldName, value.ByFieldName, null, null);
		writer.WriteProperty(options, PropByFieldValue, value.ByFieldValue, null, null);
		writer.WriteProperty(options, PropCorrelatedByFieldValue, value.CorrelatedByFieldValue, null, null);
		writer.WriteProperty(options, PropFieldName, value.FieldName, null, null);
		writer.WriteProperty(options, PropFunction, value.Function, null, null);
		writer.WriteProperty(options, PropFunctionDescription, value.FunctionDescription, null, null);
		writer.WriteProperty(options, PropGeoResults, value.GeoResults, null, null);
		writer.WriteProperty(options, PropInfluencers, value.Influencers, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.MachineLearning.Influence>? v) => w.WriteCollectionValue<Elastic.Clients.Elasticsearch.MachineLearning.Influence>(o, v, null));
		writer.WriteProperty(options, PropOverFieldName, value.OverFieldName, null, null);
		writer.WriteProperty(options, PropOverFieldValue, value.OverFieldValue, null, null);
		writer.WriteProperty(options, PropPartitionFieldName, value.PartitionFieldName, null, null);
		writer.WriteProperty(options, PropPartitionFieldValue, value.PartitionFieldValue, null, null);
		writer.WriteProperty(options, PropProbability, value.Probability, null, null);
		writer.WriteProperty(options, PropTypical, value.Typical, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.IReadOnlyCollection<double>? v) => w.WriteCollectionValue<double>(o, v, null));
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.MachineLearning.AnomalyCauseConverter))]
public sealed partial class AnomalyCause
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public AnomalyCause(double probability)
	{
		Probability = probability;
	}
#if NET7_0_OR_GREATER
	public AnomalyCause()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains additional required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public AnomalyCause()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal AnomalyCause(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	public System.Collections.Generic.IReadOnlyCollection<double>? Actual { get; set; }
	public string? ByFieldName { get; set; }
	public string? ByFieldValue { get; set; }
	public string? CorrelatedByFieldValue { get; set; }
	public string? FieldName { get; set; }
	public string? Function { get; set; }
	public string? FunctionDescription { get; set; }
	public Elastic.Clients.Elasticsearch.MachineLearning.GeoResults? GeoResults { get; set; }
	public System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.MachineLearning.Influence>? Influencers { get; set; }
	public string? OverFieldName { get; set; }
	public string? OverFieldValue { get; set; }
	public string? PartitionFieldName { get; set; }
	public string? PartitionFieldValue { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	double Probability { get; set; }
	public System.Collections.Generic.IReadOnlyCollection<double>? Typical { get; set; }
}