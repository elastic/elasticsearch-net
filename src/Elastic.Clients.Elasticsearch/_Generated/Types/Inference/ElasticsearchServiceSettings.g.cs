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

namespace Elastic.Clients.Elasticsearch.Inference;

internal sealed partial class ElasticsearchServiceSettingsConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Inference.ElasticsearchServiceSettings>
{
	private static readonly System.Text.Json.JsonEncodedText PropAdaptiveAllocations = System.Text.Json.JsonEncodedText.Encode("adaptive_allocations");
	private static readonly System.Text.Json.JsonEncodedText PropDeploymentId = System.Text.Json.JsonEncodedText.Encode("deployment_id");
	private static readonly System.Text.Json.JsonEncodedText PropModelId = System.Text.Json.JsonEncodedText.Encode("model_id");
	private static readonly System.Text.Json.JsonEncodedText PropNumAllocations = System.Text.Json.JsonEncodedText.Encode("num_allocations");
	private static readonly System.Text.Json.JsonEncodedText PropNumThreads = System.Text.Json.JsonEncodedText.Encode("num_threads");

	public override Elastic.Clients.Elasticsearch.Inference.ElasticsearchServiceSettings Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<Elastic.Clients.Elasticsearch.Inference.AdaptiveAllocations?> propAdaptiveAllocations = default;
		LocalJsonValue<string?> propDeploymentId = default;
		LocalJsonValue<string> propModelId = default;
		LocalJsonValue<int?> propNumAllocations = default;
		LocalJsonValue<int> propNumThreads = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propAdaptiveAllocations.TryReadProperty(ref reader, options, PropAdaptiveAllocations, null))
			{
				continue;
			}

			if (propDeploymentId.TryReadProperty(ref reader, options, PropDeploymentId, null))
			{
				continue;
			}

			if (propModelId.TryReadProperty(ref reader, options, PropModelId, null))
			{
				continue;
			}

			if (propNumAllocations.TryReadProperty(ref reader, options, PropNumAllocations, static int? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadNullableValue<int>(o)))
			{
				continue;
			}

			if (propNumThreads.TryReadProperty(ref reader, options, PropNumThreads, null))
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
		return new Elastic.Clients.Elasticsearch.Inference.ElasticsearchServiceSettings(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			AdaptiveAllocations = propAdaptiveAllocations.Value,
			DeploymentId = propDeploymentId.Value,
			ModelId = propModelId.Value,
			NumAllocations = propNumAllocations.Value,
			NumThreads = propNumThreads.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Inference.ElasticsearchServiceSettings value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropAdaptiveAllocations, value.AdaptiveAllocations, null, null);
		writer.WriteProperty(options, PropDeploymentId, value.DeploymentId, null, null);
		writer.WriteProperty(options, PropModelId, value.ModelId, null, null);
		writer.WriteProperty(options, PropNumAllocations, value.NumAllocations, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, int? v) => w.WriteNullableValue<int>(o, v));
		writer.WriteProperty(options, PropNumThreads, value.NumThreads, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Inference.ElasticsearchServiceSettingsConverter))]
public sealed partial class ElasticsearchServiceSettings
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public ElasticsearchServiceSettings(string modelId, int numThreads)
	{
		ModelId = modelId;
		NumThreads = numThreads;
	}
#if NET7_0_OR_GREATER
	public ElasticsearchServiceSettings()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public ElasticsearchServiceSettings()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal ElasticsearchServiceSettings(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	/// <summary>
	/// <para>
	/// Adaptive allocations configuration details.
	/// If <c>enabled</c> is true, the number of allocations of the model is set based on the current load the process gets.
	/// When the load is high, a new model allocation is automatically created, respecting the value of <c>max_number_of_allocations</c> if it's set.
	/// When the load is low, a model allocation is automatically removed, respecting the value of <c>min_number_of_allocations</c> if it's set.
	/// If <c>enabled</c> is true, do not set the number of allocations manually.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Inference.AdaptiveAllocations? AdaptiveAllocations { get; set; }

	/// <summary>
	/// <para>
	/// The deployment identifier for a trained model deployment.
	/// When <c>deployment_id</c> is used the <c>model_id</c> is optional.
	/// </para>
	/// </summary>
	public string? DeploymentId { get; set; }

	/// <summary>
	/// <para>
	/// The name of the model to use for the inference task.
	/// It can be the ID of a built-in model (for example, <c>.multilingual-e5-small</c> for E5) or a text embedding model that was uploaded by using the Eland client.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	string ModelId { get; set; }

	/// <summary>
	/// <para>
	/// The total number of allocations that are assigned to the model across machine learning nodes.
	/// Increasing this value generally increases the throughput.
	/// If adaptive allocations are enabled, do not set this value because it's automatically set.
	/// </para>
	/// </summary>
	public int? NumAllocations { get; set; }

	/// <summary>
	/// <para>
	/// The number of threads used by each model allocation during inference.
	/// This setting generally increases the speed per inference request.
	/// The inference process is a compute-bound process; <c>threads_per_allocations</c> must not exceed the number of available allocated processors per node.
	/// The value must be a power of 2.
	/// The maximum value is 32.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	int NumThreads { get; set; }
}

public readonly partial struct ElasticsearchServiceSettingsDescriptor
{
	internal Elastic.Clients.Elasticsearch.Inference.ElasticsearchServiceSettings Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public ElasticsearchServiceSettingsDescriptor(Elastic.Clients.Elasticsearch.Inference.ElasticsearchServiceSettings instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public ElasticsearchServiceSettingsDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.Inference.ElasticsearchServiceSettings(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Inference.ElasticsearchServiceSettingsDescriptor(Elastic.Clients.Elasticsearch.Inference.ElasticsearchServiceSettings instance) => new Elastic.Clients.Elasticsearch.Inference.ElasticsearchServiceSettingsDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Inference.ElasticsearchServiceSettings(Elastic.Clients.Elasticsearch.Inference.ElasticsearchServiceSettingsDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// Adaptive allocations configuration details.
	/// If <c>enabled</c> is true, the number of allocations of the model is set based on the current load the process gets.
	/// When the load is high, a new model allocation is automatically created, respecting the value of <c>max_number_of_allocations</c> if it's set.
	/// When the load is low, a model allocation is automatically removed, respecting the value of <c>min_number_of_allocations</c> if it's set.
	/// If <c>enabled</c> is true, do not set the number of allocations manually.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Inference.ElasticsearchServiceSettingsDescriptor AdaptiveAllocations(Elastic.Clients.Elasticsearch.Inference.AdaptiveAllocations? value)
	{
		Instance.AdaptiveAllocations = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Adaptive allocations configuration details.
	/// If <c>enabled</c> is true, the number of allocations of the model is set based on the current load the process gets.
	/// When the load is high, a new model allocation is automatically created, respecting the value of <c>max_number_of_allocations</c> if it's set.
	/// When the load is low, a model allocation is automatically removed, respecting the value of <c>min_number_of_allocations</c> if it's set.
	/// If <c>enabled</c> is true, do not set the number of allocations manually.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Inference.ElasticsearchServiceSettingsDescriptor AdaptiveAllocations()
	{
		Instance.AdaptiveAllocations = Elastic.Clients.Elasticsearch.Inference.AdaptiveAllocationsDescriptor.Build(null);
		return this;
	}

	/// <summary>
	/// <para>
	/// Adaptive allocations configuration details.
	/// If <c>enabled</c> is true, the number of allocations of the model is set based on the current load the process gets.
	/// When the load is high, a new model allocation is automatically created, respecting the value of <c>max_number_of_allocations</c> if it's set.
	/// When the load is low, a model allocation is automatically removed, respecting the value of <c>min_number_of_allocations</c> if it's set.
	/// If <c>enabled</c> is true, do not set the number of allocations manually.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Inference.ElasticsearchServiceSettingsDescriptor AdaptiveAllocations(System.Action<Elastic.Clients.Elasticsearch.Inference.AdaptiveAllocationsDescriptor>? action)
	{
		Instance.AdaptiveAllocations = Elastic.Clients.Elasticsearch.Inference.AdaptiveAllocationsDescriptor.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// The deployment identifier for a trained model deployment.
	/// When <c>deployment_id</c> is used the <c>model_id</c> is optional.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Inference.ElasticsearchServiceSettingsDescriptor DeploymentId(string? value)
	{
		Instance.DeploymentId = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The name of the model to use for the inference task.
	/// It can be the ID of a built-in model (for example, <c>.multilingual-e5-small</c> for E5) or a text embedding model that was uploaded by using the Eland client.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Inference.ElasticsearchServiceSettingsDescriptor ModelId(string value)
	{
		Instance.ModelId = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The total number of allocations that are assigned to the model across machine learning nodes.
	/// Increasing this value generally increases the throughput.
	/// If adaptive allocations are enabled, do not set this value because it's automatically set.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Inference.ElasticsearchServiceSettingsDescriptor NumAllocations(int? value)
	{
		Instance.NumAllocations = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The number of threads used by each model allocation during inference.
	/// This setting generally increases the speed per inference request.
	/// The inference process is a compute-bound process; <c>threads_per_allocations</c> must not exceed the number of available allocated processors per node.
	/// The value must be a power of 2.
	/// The maximum value is 32.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Inference.ElasticsearchServiceSettingsDescriptor NumThreads(int value)
	{
		Instance.NumThreads = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Inference.ElasticsearchServiceSettings Build(System.Action<Elastic.Clients.Elasticsearch.Inference.ElasticsearchServiceSettingsDescriptor> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.Inference.ElasticsearchServiceSettingsDescriptor(new Elastic.Clients.Elasticsearch.Inference.ElasticsearchServiceSettings(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}