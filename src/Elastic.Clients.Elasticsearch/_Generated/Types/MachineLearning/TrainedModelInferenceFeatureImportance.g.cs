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

internal sealed partial class TrainedModelInferenceFeatureImportanceConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.MachineLearning.TrainedModelInferenceFeatureImportance>
{
	private static readonly System.Text.Json.JsonEncodedText PropClasses = System.Text.Json.JsonEncodedText.Encode("classes");
	private static readonly System.Text.Json.JsonEncodedText PropFeatureName = System.Text.Json.JsonEncodedText.Encode("feature_name");
	private static readonly System.Text.Json.JsonEncodedText PropImportance = System.Text.Json.JsonEncodedText.Encode("importance");

	public override Elastic.Clients.Elasticsearch.MachineLearning.TrainedModelInferenceFeatureImportance Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.MachineLearning.TrainedModelInferenceClassImportance>?> propClasses = default;
		LocalJsonValue<string> propFeatureName = default;
		LocalJsonValue<double?> propImportance = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propClasses.TryReadProperty(ref reader, options, PropClasses, static System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.MachineLearning.TrainedModelInferenceClassImportance>? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadCollectionValue<Elastic.Clients.Elasticsearch.MachineLearning.TrainedModelInferenceClassImportance>(o, null)))
			{
				continue;
			}

			if (propFeatureName.TryReadProperty(ref reader, options, PropFeatureName, null))
			{
				continue;
			}

			if (propImportance.TryReadProperty(ref reader, options, PropImportance, null))
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
		return new Elastic.Clients.Elasticsearch.MachineLearning.TrainedModelInferenceFeatureImportance(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Classes = propClasses.Value,
			FeatureName = propFeatureName.Value,
			Importance = propImportance.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.MachineLearning.TrainedModelInferenceFeatureImportance value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropClasses, value.Classes, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.MachineLearning.TrainedModelInferenceClassImportance>? v) => w.WriteCollectionValue<Elastic.Clients.Elasticsearch.MachineLearning.TrainedModelInferenceClassImportance>(o, v, null));
		writer.WriteProperty(options, PropFeatureName, value.FeatureName, null, null);
		writer.WriteProperty(options, PropImportance, value.Importance, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.MachineLearning.TrainedModelInferenceFeatureImportanceConverter))]
public sealed partial class TrainedModelInferenceFeatureImportance
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public TrainedModelInferenceFeatureImportance(string featureName)
	{
		FeatureName = featureName;
	}
#if NET7_0_OR_GREATER
	public TrainedModelInferenceFeatureImportance()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public TrainedModelInferenceFeatureImportance()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal TrainedModelInferenceFeatureImportance(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	public System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.MachineLearning.TrainedModelInferenceClassImportance>? Classes { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	string FeatureName { get; set; }
	public double? Importance { get; set; }
}