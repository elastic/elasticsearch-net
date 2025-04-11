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

internal sealed partial class ConfusionMatrixItemConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.MachineLearning.ConfusionMatrixItem>
{
	private static readonly System.Text.Json.JsonEncodedText PropActualClass = System.Text.Json.JsonEncodedText.Encode("actual_class");
	private static readonly System.Text.Json.JsonEncodedText PropActualClassDocCount = System.Text.Json.JsonEncodedText.Encode("actual_class_doc_count");
	private static readonly System.Text.Json.JsonEncodedText PropOtherPredictedClassDocCount = System.Text.Json.JsonEncodedText.Encode("other_predicted_class_doc_count");
	private static readonly System.Text.Json.JsonEncodedText PropPredictedClasses = System.Text.Json.JsonEncodedText.Encode("predicted_classes");

	public override Elastic.Clients.Elasticsearch.MachineLearning.ConfusionMatrixItem Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<string> propActualClass = default;
		LocalJsonValue<int> propActualClassDocCount = default;
		LocalJsonValue<int> propOtherPredictedClassDocCount = default;
		LocalJsonValue<System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.MachineLearning.ConfusionMatrixPrediction>> propPredictedClasses = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propActualClass.TryReadProperty(ref reader, options, PropActualClass, null))
			{
				continue;
			}

			if (propActualClassDocCount.TryReadProperty(ref reader, options, PropActualClassDocCount, null))
			{
				continue;
			}

			if (propOtherPredictedClassDocCount.TryReadProperty(ref reader, options, PropOtherPredictedClassDocCount, null))
			{
				continue;
			}

			if (propPredictedClasses.TryReadProperty(ref reader, options, PropPredictedClasses, static System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.MachineLearning.ConfusionMatrixPrediction> (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadCollectionValue<Elastic.Clients.Elasticsearch.MachineLearning.ConfusionMatrixPrediction>(o, null)!))
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
		return new Elastic.Clients.Elasticsearch.MachineLearning.ConfusionMatrixItem(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			ActualClass = propActualClass.Value,
			ActualClassDocCount = propActualClassDocCount.Value,
			OtherPredictedClassDocCount = propOtherPredictedClassDocCount.Value,
			PredictedClasses = propPredictedClasses.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.MachineLearning.ConfusionMatrixItem value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropActualClass, value.ActualClass, null, null);
		writer.WriteProperty(options, PropActualClassDocCount, value.ActualClassDocCount, null, null);
		writer.WriteProperty(options, PropOtherPredictedClassDocCount, value.OtherPredictedClassDocCount, null, null);
		writer.WriteProperty(options, PropPredictedClasses, value.PredictedClasses, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.MachineLearning.ConfusionMatrixPrediction> v) => w.WriteCollectionValue<Elastic.Clients.Elasticsearch.MachineLearning.ConfusionMatrixPrediction>(o, v, null));
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.MachineLearning.ConfusionMatrixItemConverter))]
public sealed partial class ConfusionMatrixItem
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public ConfusionMatrixItem(string actualClass, int actualClassDocCount, int otherPredictedClassDocCount, System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.MachineLearning.ConfusionMatrixPrediction> predictedClasses)
	{
		ActualClass = actualClass;
		ActualClassDocCount = actualClassDocCount;
		OtherPredictedClassDocCount = otherPredictedClassDocCount;
		PredictedClasses = predictedClasses;
	}
#if NET7_0_OR_GREATER
	public ConfusionMatrixItem()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public ConfusionMatrixItem()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal ConfusionMatrixItem(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	public
#if NET7_0_OR_GREATER
	required
#endif
	string ActualClass { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	int ActualClassDocCount { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	int OtherPredictedClassDocCount { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.MachineLearning.ConfusionMatrixPrediction> PredictedClasses { get; set; }
}