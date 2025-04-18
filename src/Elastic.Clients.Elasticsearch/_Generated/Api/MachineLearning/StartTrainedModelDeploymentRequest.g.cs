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

public sealed partial class StartTrainedModelDeploymentRequestParameters : Elastic.Transport.RequestParameters
{
	/// <summary>
	/// <para>
	/// The inference cache size (in memory outside the JVM heap) per node for the model.
	/// The default value is the same size as the <c>model_size_bytes</c>. To disable the cache,
	/// <c>0b</c> can be provided.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.ByteSize? CacheSize { get => Q<Elastic.Clients.Elasticsearch.ByteSize?>("cache_size"); set => Q("cache_size", value); }

	/// <summary>
	/// <para>
	/// A unique identifier for the deployment of the model.
	/// </para>
	/// </summary>
	public string? DeploymentId { get => Q<string?>("deployment_id"); set => Q("deployment_id", value); }

	/// <summary>
	/// <para>
	/// The number of model allocations on each node where the model is deployed.
	/// All allocations on a node share the same copy of the model in memory but use
	/// a separate set of threads to evaluate the model.
	/// Increasing this value generally increases the throughput.
	/// If this setting is greater than the number of hardware threads
	/// it will automatically be changed to a value less than the number of hardware threads.
	/// If adaptive_allocations is enabled, do not set this value, because it’s automatically set.
	/// </para>
	/// </summary>
	public int? NumberOfAllocations { get => Q<int?>("number_of_allocations"); set => Q("number_of_allocations", value); }

	/// <summary>
	/// <para>
	/// The deployment priority.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.TrainingPriority? Priority { get => Q<Elastic.Clients.Elasticsearch.MachineLearning.TrainingPriority?>("priority"); set => Q("priority", value); }

	/// <summary>
	/// <para>
	/// Specifies the number of inference requests that are allowed in the queue. After the number of requests exceeds
	/// this value, new requests are rejected with a 429 error.
	/// </para>
	/// </summary>
	public int? QueueCapacity { get => Q<int?>("queue_capacity"); set => Q("queue_capacity", value); }

	/// <summary>
	/// <para>
	/// Sets the number of threads used by each model allocation during inference. This generally increases
	/// the inference speed. The inference process is a compute-bound process; any number
	/// greater than the number of available hardware threads on the machine does not increase the
	/// inference speed. If this setting is greater than the number of hardware threads
	/// it will automatically be changed to a value less than the number of hardware threads.
	/// </para>
	/// </summary>
	public int? ThreadsPerAllocation { get => Q<int?>("threads_per_allocation"); set => Q("threads_per_allocation", value); }

	/// <summary>
	/// <para>
	/// Specifies the amount of time to wait for the model to deploy.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Duration? Timeout { get => Q<Elastic.Clients.Elasticsearch.Duration?>("timeout"); set => Q("timeout", value); }

	/// <summary>
	/// <para>
	/// Specifies the allocation status to wait for before returning.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.DeploymentAllocationState? WaitFor { get => Q<Elastic.Clients.Elasticsearch.MachineLearning.DeploymentAllocationState?>("wait_for"); set => Q("wait_for", value); }
}

internal sealed partial class StartTrainedModelDeploymentRequestConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.MachineLearning.StartTrainedModelDeploymentRequest>
{
	private static readonly System.Text.Json.JsonEncodedText PropAdaptiveAllocations = System.Text.Json.JsonEncodedText.Encode("adaptive_allocations");

	public override Elastic.Clients.Elasticsearch.MachineLearning.StartTrainedModelDeploymentRequest Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<Elastic.Clients.Elasticsearch.MachineLearning.AdaptiveAllocationsSettings?> propAdaptiveAllocations = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propAdaptiveAllocations.TryReadProperty(ref reader, options, PropAdaptiveAllocations, null))
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
		return new Elastic.Clients.Elasticsearch.MachineLearning.StartTrainedModelDeploymentRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			AdaptiveAllocations = propAdaptiveAllocations.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.MachineLearning.StartTrainedModelDeploymentRequest value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropAdaptiveAllocations, value.AdaptiveAllocations, null, null);
		writer.WriteEndObject();
	}
}

/// <summary>
/// <para>
/// Start a trained model deployment.
/// It allocates the model to every machine learning node.
/// </para>
/// </summary>
[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.MachineLearning.StartTrainedModelDeploymentRequestConverter))]
public sealed partial class StartTrainedModelDeploymentRequest : Elastic.Clients.Elasticsearch.Requests.PlainRequest<Elastic.Clients.Elasticsearch.MachineLearning.StartTrainedModelDeploymentRequestParameters>
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public StartTrainedModelDeploymentRequest(Elastic.Clients.Elasticsearch.Id modelId) : base(r => r.Required("model_id", modelId))
	{
	}
#if NET7_0_OR_GREATER
	public StartTrainedModelDeploymentRequest()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal StartTrainedModelDeploymentRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	internal override Elastic.Clients.Elasticsearch.Requests.ApiUrls ApiUrls => Elastic.Clients.Elasticsearch.Requests.ApiUrlLookup.MachineLearningStartTrainedModelDeployment;

	protected override Elastic.Transport.HttpMethod StaticHttpMethod => Elastic.Transport.HttpMethod.POST;

	internal override bool SupportsBody => true;

	internal override string OperationName => "ml.start_trained_model_deployment";

	/// <summary>
	/// <para>
	/// The unique identifier of the trained model. Currently, only PyTorch models are supported.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.Id ModelId { get => P<Elastic.Clients.Elasticsearch.Id>("model_id"); set => PR("model_id", value); }

	/// <summary>
	/// <para>
	/// The inference cache size (in memory outside the JVM heap) per node for the model.
	/// The default value is the same size as the <c>model_size_bytes</c>. To disable the cache,
	/// <c>0b</c> can be provided.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.ByteSize? CacheSize { get => Q<Elastic.Clients.Elasticsearch.ByteSize?>("cache_size"); set => Q("cache_size", value); }

	/// <summary>
	/// <para>
	/// A unique identifier for the deployment of the model.
	/// </para>
	/// </summary>
	public string? DeploymentId { get => Q<string?>("deployment_id"); set => Q("deployment_id", value); }

	/// <summary>
	/// <para>
	/// The number of model allocations on each node where the model is deployed.
	/// All allocations on a node share the same copy of the model in memory but use
	/// a separate set of threads to evaluate the model.
	/// Increasing this value generally increases the throughput.
	/// If this setting is greater than the number of hardware threads
	/// it will automatically be changed to a value less than the number of hardware threads.
	/// If adaptive_allocations is enabled, do not set this value, because it’s automatically set.
	/// </para>
	/// </summary>
	public int? NumberOfAllocations { get => Q<int?>("number_of_allocations"); set => Q("number_of_allocations", value); }

	/// <summary>
	/// <para>
	/// The deployment priority.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.TrainingPriority? Priority { get => Q<Elastic.Clients.Elasticsearch.MachineLearning.TrainingPriority?>("priority"); set => Q("priority", value); }

	/// <summary>
	/// <para>
	/// Specifies the number of inference requests that are allowed in the queue. After the number of requests exceeds
	/// this value, new requests are rejected with a 429 error.
	/// </para>
	/// </summary>
	public int? QueueCapacity { get => Q<int?>("queue_capacity"); set => Q("queue_capacity", value); }

	/// <summary>
	/// <para>
	/// Sets the number of threads used by each model allocation during inference. This generally increases
	/// the inference speed. The inference process is a compute-bound process; any number
	/// greater than the number of available hardware threads on the machine does not increase the
	/// inference speed. If this setting is greater than the number of hardware threads
	/// it will automatically be changed to a value less than the number of hardware threads.
	/// </para>
	/// </summary>
	public int? ThreadsPerAllocation { get => Q<int?>("threads_per_allocation"); set => Q("threads_per_allocation", value); }

	/// <summary>
	/// <para>
	/// Specifies the amount of time to wait for the model to deploy.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Duration? Timeout { get => Q<Elastic.Clients.Elasticsearch.Duration?>("timeout"); set => Q("timeout", value); }

	/// <summary>
	/// <para>
	/// Specifies the allocation status to wait for before returning.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.DeploymentAllocationState? WaitFor { get => Q<Elastic.Clients.Elasticsearch.MachineLearning.DeploymentAllocationState?>("wait_for"); set => Q("wait_for", value); }

	/// <summary>
	/// <para>
	/// Adaptive allocations configuration. When enabled, the number of allocations
	/// is set based on the current load.
	/// If adaptive_allocations is enabled, do not set the number of allocations manually.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.AdaptiveAllocationsSettings? AdaptiveAllocations { get; set; }
}

/// <summary>
/// <para>
/// Start a trained model deployment.
/// It allocates the model to every machine learning node.
/// </para>
/// </summary>
public readonly partial struct StartTrainedModelDeploymentRequestDescriptor
{
	internal Elastic.Clients.Elasticsearch.MachineLearning.StartTrainedModelDeploymentRequest Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public StartTrainedModelDeploymentRequestDescriptor(Elastic.Clients.Elasticsearch.MachineLearning.StartTrainedModelDeploymentRequest instance)
	{
		Instance = instance;
	}

	public StartTrainedModelDeploymentRequestDescriptor(Elastic.Clients.Elasticsearch.Id modelId)
	{
		Instance = new Elastic.Clients.Elasticsearch.MachineLearning.StartTrainedModelDeploymentRequest(modelId);
	}

	[System.Obsolete("The use of the parameterless constructor is not permitted for this type.")]
	public StartTrainedModelDeploymentRequestDescriptor()
	{
		throw new System.InvalidOperationException("The use of the parameterless constructor is not permitted for this type.");
	}

	public static explicit operator Elastic.Clients.Elasticsearch.MachineLearning.StartTrainedModelDeploymentRequestDescriptor(Elastic.Clients.Elasticsearch.MachineLearning.StartTrainedModelDeploymentRequest instance) => new Elastic.Clients.Elasticsearch.MachineLearning.StartTrainedModelDeploymentRequestDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.MachineLearning.StartTrainedModelDeploymentRequest(Elastic.Clients.Elasticsearch.MachineLearning.StartTrainedModelDeploymentRequestDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// The unique identifier of the trained model. Currently, only PyTorch models are supported.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.StartTrainedModelDeploymentRequestDescriptor ModelId(Elastic.Clients.Elasticsearch.Id value)
	{
		Instance.ModelId = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The inference cache size (in memory outside the JVM heap) per node for the model.
	/// The default value is the same size as the <c>model_size_bytes</c>. To disable the cache,
	/// <c>0b</c> can be provided.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.StartTrainedModelDeploymentRequestDescriptor CacheSize(Elastic.Clients.Elasticsearch.ByteSize? value)
	{
		Instance.CacheSize = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The inference cache size (in memory outside the JVM heap) per node for the model.
	/// The default value is the same size as the <c>model_size_bytes</c>. To disable the cache,
	/// <c>0b</c> can be provided.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.StartTrainedModelDeploymentRequestDescriptor CacheSize(System.Func<Elastic.Clients.Elasticsearch.ByteSizeFactory, Elastic.Clients.Elasticsearch.ByteSize> action)
	{
		Instance.CacheSize = Elastic.Clients.Elasticsearch.ByteSizeFactory.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// A unique identifier for the deployment of the model.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.StartTrainedModelDeploymentRequestDescriptor DeploymentId(string? value)
	{
		Instance.DeploymentId = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The number of model allocations on each node where the model is deployed.
	/// All allocations on a node share the same copy of the model in memory but use
	/// a separate set of threads to evaluate the model.
	/// Increasing this value generally increases the throughput.
	/// If this setting is greater than the number of hardware threads
	/// it will automatically be changed to a value less than the number of hardware threads.
	/// If adaptive_allocations is enabled, do not set this value, because it’s automatically set.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.StartTrainedModelDeploymentRequestDescriptor NumberOfAllocations(int? value)
	{
		Instance.NumberOfAllocations = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The deployment priority.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.StartTrainedModelDeploymentRequestDescriptor Priority(Elastic.Clients.Elasticsearch.MachineLearning.TrainingPriority? value)
	{
		Instance.Priority = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Specifies the number of inference requests that are allowed in the queue. After the number of requests exceeds
	/// this value, new requests are rejected with a 429 error.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.StartTrainedModelDeploymentRequestDescriptor QueueCapacity(int? value)
	{
		Instance.QueueCapacity = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Sets the number of threads used by each model allocation during inference. This generally increases
	/// the inference speed. The inference process is a compute-bound process; any number
	/// greater than the number of available hardware threads on the machine does not increase the
	/// inference speed. If this setting is greater than the number of hardware threads
	/// it will automatically be changed to a value less than the number of hardware threads.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.StartTrainedModelDeploymentRequestDescriptor ThreadsPerAllocation(int? value)
	{
		Instance.ThreadsPerAllocation = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Specifies the amount of time to wait for the model to deploy.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.StartTrainedModelDeploymentRequestDescriptor Timeout(Elastic.Clients.Elasticsearch.Duration? value)
	{
		Instance.Timeout = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Specifies the allocation status to wait for before returning.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.StartTrainedModelDeploymentRequestDescriptor WaitFor(Elastic.Clients.Elasticsearch.MachineLearning.DeploymentAllocationState? value)
	{
		Instance.WaitFor = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Adaptive allocations configuration. When enabled, the number of allocations
	/// is set based on the current load.
	/// If adaptive_allocations is enabled, do not set the number of allocations manually.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.StartTrainedModelDeploymentRequestDescriptor AdaptiveAllocations(Elastic.Clients.Elasticsearch.MachineLearning.AdaptiveAllocationsSettings? value)
	{
		Instance.AdaptiveAllocations = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Adaptive allocations configuration. When enabled, the number of allocations
	/// is set based on the current load.
	/// If adaptive_allocations is enabled, do not set the number of allocations manually.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.StartTrainedModelDeploymentRequestDescriptor AdaptiveAllocations(System.Action<Elastic.Clients.Elasticsearch.MachineLearning.AdaptiveAllocationsSettingsDescriptor> action)
	{
		Instance.AdaptiveAllocations = Elastic.Clients.Elasticsearch.MachineLearning.AdaptiveAllocationsSettingsDescriptor.Build(action);
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.MachineLearning.StartTrainedModelDeploymentRequest Build(System.Action<Elastic.Clients.Elasticsearch.MachineLearning.StartTrainedModelDeploymentRequestDescriptor> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.MachineLearning.StartTrainedModelDeploymentRequestDescriptor(new Elastic.Clients.Elasticsearch.MachineLearning.StartTrainedModelDeploymentRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.StartTrainedModelDeploymentRequestDescriptor ErrorTrace(bool? value)
	{
		Instance.ErrorTrace = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.StartTrainedModelDeploymentRequestDescriptor FilterPath(params string[]? value)
	{
		Instance.FilterPath = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.StartTrainedModelDeploymentRequestDescriptor Human(bool? value)
	{
		Instance.Human = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.StartTrainedModelDeploymentRequestDescriptor Pretty(bool? value)
	{
		Instance.Pretty = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.StartTrainedModelDeploymentRequestDescriptor SourceQueryString(string? value)
	{
		Instance.SourceQueryString = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.StartTrainedModelDeploymentRequestDescriptor RequestConfiguration(Elastic.Transport.IRequestConfiguration? value)
	{
		Instance.RequestConfiguration = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.MachineLearning.StartTrainedModelDeploymentRequestDescriptor RequestConfiguration(System.Func<Elastic.Transport.RequestConfigurationDescriptor, Elastic.Transport.IRequestConfiguration>? configurationSelector)
	{
		Instance.RequestConfiguration = configurationSelector.Invoke(Instance.RequestConfiguration is null ? new Elastic.Transport.RequestConfigurationDescriptor() : new Elastic.Transport.RequestConfigurationDescriptor(Instance.RequestConfiguration)) ?? Instance.RequestConfiguration;
		return this;
	}
}