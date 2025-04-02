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

internal sealed partial class EnsembleConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.MachineLearning.Ensemble>
{
	private static readonly System.Text.Json.JsonEncodedText PropAggregateOutput = System.Text.Json.JsonEncodedText.Encode("aggregate_output");
	private static readonly System.Text.Json.JsonEncodedText PropClassificationLabels = System.Text.Json.JsonEncodedText.Encode("classification_labels");
	private static readonly System.Text.Json.JsonEncodedText PropFeatureNames = System.Text.Json.JsonEncodedText.Encode("feature_names");
	private static readonly System.Text.Json.JsonEncodedText PropTargetType = System.Text.Json.JsonEncodedText.Encode("target_type");
	private static readonly System.Text.Json.JsonEncodedText PropTrainedModels = System.Text.Json.JsonEncodedText.Encode("trained_models");

	public override Elastic.Clients.Elasticsearch.MachineLearning.Ensemble Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<Elastic.Clients.Elasticsearch.MachineLearning.AggregateOutput?> propAggregateOutput = default;
		LocalJsonValue<System.Collections.Generic.ICollection<string>?> propClassificationLabels = default;
		LocalJsonValue<System.Collections.Generic.ICollection<string>?> propFeatureNames = default;
		LocalJsonValue<string?> propTargetType = default;
		LocalJsonValue<System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.MachineLearning.TrainedModel>> propTrainedModels = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propAggregateOutput.TryReadProperty(ref reader, options, PropAggregateOutput, null))
			{
				continue;
			}

			if (propClassificationLabels.TryReadProperty(ref reader, options, PropClassificationLabels, static System.Collections.Generic.ICollection<string>? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadCollectionValue<string>(o, null)))
			{
				continue;
			}

			if (propFeatureNames.TryReadProperty(ref reader, options, PropFeatureNames, static System.Collections.Generic.ICollection<string>? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadCollectionValue<string>(o, null)))
			{
				continue;
			}

			if (propTargetType.TryReadProperty(ref reader, options, PropTargetType, null))
			{
				continue;
			}

			if (propTrainedModels.TryReadProperty(ref reader, options, PropTrainedModels, static System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.MachineLearning.TrainedModel> (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadCollectionValue<Elastic.Clients.Elasticsearch.MachineLearning.TrainedModel>(o, null)!))
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
		return new Elastic.Clients.Elasticsearch.MachineLearning.Ensemble(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			AggregateOutput = propAggregateOutput.Value,
			ClassificationLabels = propClassificationLabels.Value,
			FeatureNames = propFeatureNames.Value,
			TargetType = propTargetType.Value,
			TrainedModels = propTrainedModels.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.MachineLearning.Ensemble value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropAggregateOutput, value.AggregateOutput, null, null);
		writer.WriteProperty(options, PropClassificationLabels, value.ClassificationLabels, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.ICollection<string>? v) => w.WriteCollectionValue<string>(o, v, null));
		writer.WriteProperty(options, PropFeatureNames, value.FeatureNames, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.ICollection<string>? v) => w.WriteCollectionValue<string>(o, v, null));
		writer.WriteProperty(options, PropTargetType, value.TargetType, null, null);
		writer.WriteProperty(options, PropTrainedModels, value.TrainedModels, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.MachineLearning.TrainedModel> v) => w.WriteCollectionValue<Elastic.Clients.Elasticsearch.MachineLearning.TrainedModel>(o, v, null));
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.MachineLearning.EnsembleConverter))]
public sealed partial class Ensemble
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public Ensemble(System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.MachineLearning.TrainedModel> trainedModels)
	{
		TrainedModels = trainedModels;
	}
#if NET7_0_OR_GREATER
	public Ensemble()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public Ensemble()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal Ensemble(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.AggregateOutput? AggregateOutput { get; set; }
	public System.Collections.Generic.ICollection<string>? ClassificationLabels { get; set; }
	public System.Collections.Generic.ICollection<string>? FeatureNames { get; set; }
	public string? TargetType { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.MachineLearning.TrainedModel> TrainedModels { get; set; }
}

public readonly partial struct EnsembleDescriptor
{
	internal Elastic.Clients.Elasticsearch.MachineLearning.Ensemble Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public EnsembleDescriptor(Elastic.Clients.Elasticsearch.MachineLearning.Ensemble instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public EnsembleDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.MachineLearning.Ensemble(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.MachineLearning.EnsembleDescriptor(Elastic.Clients.Elasticsearch.MachineLearning.Ensemble instance) => new Elastic.Clients.Elasticsearch.MachineLearning.EnsembleDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.MachineLearning.Ensemble(Elastic.Clients.Elasticsearch.MachineLearning.EnsembleDescriptor descriptor) => descriptor.Instance;

	public Elastic.Clients.Elasticsearch.MachineLearning.EnsembleDescriptor AggregateOutput(Elastic.Clients.Elasticsearch.MachineLearning.AggregateOutput? value)
	{
		Instance.AggregateOutput = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.EnsembleDescriptor AggregateOutput()
	{
		Instance.AggregateOutput = Elastic.Clients.Elasticsearch.MachineLearning.AggregateOutputDescriptor.Build(null);
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.EnsembleDescriptor AggregateOutput(System.Action<Elastic.Clients.Elasticsearch.MachineLearning.AggregateOutputDescriptor>? action)
	{
		Instance.AggregateOutput = Elastic.Clients.Elasticsearch.MachineLearning.AggregateOutputDescriptor.Build(action);
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.EnsembleDescriptor ClassificationLabels(System.Collections.Generic.ICollection<string>? value)
	{
		Instance.ClassificationLabels = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.EnsembleDescriptor ClassificationLabels()
	{
		Instance.ClassificationLabels = Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfString.Build(null);
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.EnsembleDescriptor ClassificationLabels(System.Action<Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfString>? action)
	{
		Instance.ClassificationLabels = Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfString.Build(action);
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.EnsembleDescriptor ClassificationLabels(params string[] values)
	{
		Instance.ClassificationLabels = [.. values];
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.EnsembleDescriptor FeatureNames(System.Collections.Generic.ICollection<string>? value)
	{
		Instance.FeatureNames = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.EnsembleDescriptor FeatureNames()
	{
		Instance.FeatureNames = Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfString.Build(null);
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.EnsembleDescriptor FeatureNames(System.Action<Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfString>? action)
	{
		Instance.FeatureNames = Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfString.Build(action);
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.EnsembleDescriptor FeatureNames(params string[] values)
	{
		Instance.FeatureNames = [.. values];
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.EnsembleDescriptor TargetType(string? value)
	{
		Instance.TargetType = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.EnsembleDescriptor TrainedModels(System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.MachineLearning.TrainedModel> value)
	{
		Instance.TrainedModels = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.EnsembleDescriptor TrainedModels()
	{
		Instance.TrainedModels = Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfTrainedModel.Build(null);
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.EnsembleDescriptor TrainedModels(System.Action<Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfTrainedModel>? action)
	{
		Instance.TrainedModels = Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfTrainedModel.Build(action);
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.EnsembleDescriptor TrainedModels(params Elastic.Clients.Elasticsearch.MachineLearning.TrainedModel[] values)
	{
		Instance.TrainedModels = [.. values];
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.EnsembleDescriptor TrainedModels(params System.Action<Elastic.Clients.Elasticsearch.MachineLearning.TrainedModelDescriptor>?[] actions)
	{
		var items = new System.Collections.Generic.List<Elastic.Clients.Elasticsearch.MachineLearning.TrainedModel>();
		foreach (var action in actions)
		{
			items.Add(Elastic.Clients.Elasticsearch.MachineLearning.TrainedModelDescriptor.Build(action));
		}

		Instance.TrainedModels = items;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.MachineLearning.Ensemble Build(System.Action<Elastic.Clients.Elasticsearch.MachineLearning.EnsembleDescriptor> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.MachineLearning.EnsembleDescriptor(new Elastic.Clients.Elasticsearch.MachineLearning.Ensemble(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}