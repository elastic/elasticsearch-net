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

internal sealed partial class TrainedModelTreeConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.MachineLearning.TrainedModelTree>
{
	private static readonly System.Text.Json.JsonEncodedText PropClassificationLabels = System.Text.Json.JsonEncodedText.Encode("classification_labels");
	private static readonly System.Text.Json.JsonEncodedText PropFeatureNames = System.Text.Json.JsonEncodedText.Encode("feature_names");
	private static readonly System.Text.Json.JsonEncodedText PropTargetType = System.Text.Json.JsonEncodedText.Encode("target_type");
	private static readonly System.Text.Json.JsonEncodedText PropTreeStructure = System.Text.Json.JsonEncodedText.Encode("tree_structure");

	public override Elastic.Clients.Elasticsearch.MachineLearning.TrainedModelTree Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<System.Collections.Generic.ICollection<string>?> propClassificationLabels = default;
		LocalJsonValue<System.Collections.Generic.ICollection<string>> propFeatureNames = default;
		LocalJsonValue<string?> propTargetType = default;
		LocalJsonValue<System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.MachineLearning.TrainedModelTreeNode>> propTreeStructure = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propClassificationLabels.TryReadProperty(ref reader, options, PropClassificationLabels, static System.Collections.Generic.ICollection<string>? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadCollectionValue<string>(o, null)))
			{
				continue;
			}

			if (propFeatureNames.TryReadProperty(ref reader, options, PropFeatureNames, static System.Collections.Generic.ICollection<string> (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadCollectionValue<string>(o, null)!))
			{
				continue;
			}

			if (propTargetType.TryReadProperty(ref reader, options, PropTargetType, null))
			{
				continue;
			}

			if (propTreeStructure.TryReadProperty(ref reader, options, PropTreeStructure, static System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.MachineLearning.TrainedModelTreeNode> (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadCollectionValue<Elastic.Clients.Elasticsearch.MachineLearning.TrainedModelTreeNode>(o, null)!))
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
		return new Elastic.Clients.Elasticsearch.MachineLearning.TrainedModelTree(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			ClassificationLabels = propClassificationLabels.Value,
			FeatureNames = propFeatureNames.Value,
			TargetType = propTargetType.Value,
			TreeStructure = propTreeStructure.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.MachineLearning.TrainedModelTree value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropClassificationLabels, value.ClassificationLabels, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.ICollection<string>? v) => w.WriteCollectionValue<string>(o, v, null));
		writer.WriteProperty(options, PropFeatureNames, value.FeatureNames, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.ICollection<string> v) => w.WriteCollectionValue<string>(o, v, null));
		writer.WriteProperty(options, PropTargetType, value.TargetType, null, null);
		writer.WriteProperty(options, PropTreeStructure, value.TreeStructure, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.MachineLearning.TrainedModelTreeNode> v) => w.WriteCollectionValue<Elastic.Clients.Elasticsearch.MachineLearning.TrainedModelTreeNode>(o, v, null));
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.MachineLearning.TrainedModelTreeConverter))]
public sealed partial class TrainedModelTree
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public TrainedModelTree(System.Collections.Generic.ICollection<string> featureNames, System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.MachineLearning.TrainedModelTreeNode> treeStructure)
	{
		FeatureNames = featureNames;
		TreeStructure = treeStructure;
	}
#if NET7_0_OR_GREATER
	public TrainedModelTree()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public TrainedModelTree()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal TrainedModelTree(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	public System.Collections.Generic.ICollection<string>? ClassificationLabels { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	System.Collections.Generic.ICollection<string> FeatureNames { get; set; }
	public string? TargetType { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.MachineLearning.TrainedModelTreeNode> TreeStructure { get; set; }
}

public readonly partial struct TrainedModelTreeDescriptor
{
	internal Elastic.Clients.Elasticsearch.MachineLearning.TrainedModelTree Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public TrainedModelTreeDescriptor(Elastic.Clients.Elasticsearch.MachineLearning.TrainedModelTree instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public TrainedModelTreeDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.MachineLearning.TrainedModelTree(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.MachineLearning.TrainedModelTreeDescriptor(Elastic.Clients.Elasticsearch.MachineLearning.TrainedModelTree instance) => new Elastic.Clients.Elasticsearch.MachineLearning.TrainedModelTreeDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.MachineLearning.TrainedModelTree(Elastic.Clients.Elasticsearch.MachineLearning.TrainedModelTreeDescriptor descriptor) => descriptor.Instance;

	public Elastic.Clients.Elasticsearch.MachineLearning.TrainedModelTreeDescriptor ClassificationLabels(System.Collections.Generic.ICollection<string>? value)
	{
		Instance.ClassificationLabels = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.TrainedModelTreeDescriptor ClassificationLabels(params string[] values)
	{
		Instance.ClassificationLabels = [.. values];
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.TrainedModelTreeDescriptor FeatureNames(System.Collections.Generic.ICollection<string> value)
	{
		Instance.FeatureNames = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.TrainedModelTreeDescriptor FeatureNames(params string[] values)
	{
		Instance.FeatureNames = [.. values];
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.TrainedModelTreeDescriptor TargetType(string? value)
	{
		Instance.TargetType = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.TrainedModelTreeDescriptor TreeStructure(System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.MachineLearning.TrainedModelTreeNode> value)
	{
		Instance.TreeStructure = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.TrainedModelTreeDescriptor TreeStructure(params Elastic.Clients.Elasticsearch.MachineLearning.TrainedModelTreeNode[] values)
	{
		Instance.TreeStructure = [.. values];
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.TrainedModelTreeDescriptor TreeStructure(params System.Action<Elastic.Clients.Elasticsearch.MachineLearning.TrainedModelTreeNodeDescriptor>[] actions)
	{
		var items = new System.Collections.Generic.List<Elastic.Clients.Elasticsearch.MachineLearning.TrainedModelTreeNode>();
		foreach (var action in actions)
		{
			items.Add(Elastic.Clients.Elasticsearch.MachineLearning.TrainedModelTreeNodeDescriptor.Build(action));
		}

		Instance.TreeStructure = items;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.MachineLearning.TrainedModelTree Build(System.Action<Elastic.Clients.Elasticsearch.MachineLearning.TrainedModelTreeDescriptor> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.MachineLearning.TrainedModelTreeDescriptor(new Elastic.Clients.Elasticsearch.MachineLearning.TrainedModelTree(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}