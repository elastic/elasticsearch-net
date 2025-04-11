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

public sealed partial class PutAnthropicRequestParameters : Elastic.Transport.RequestParameters
{
}

internal sealed partial class PutAnthropicRequestConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Inference.PutAnthropicRequest>
{
	private static readonly System.Text.Json.JsonEncodedText PropChunkingSettings = System.Text.Json.JsonEncodedText.Encode("chunking_settings");
	private static readonly System.Text.Json.JsonEncodedText PropService = System.Text.Json.JsonEncodedText.Encode("service");
	private static readonly System.Text.Json.JsonEncodedText PropServiceSettings = System.Text.Json.JsonEncodedText.Encode("service_settings");
	private static readonly System.Text.Json.JsonEncodedText PropTaskSettings = System.Text.Json.JsonEncodedText.Encode("task_settings");

	public override Elastic.Clients.Elasticsearch.Inference.PutAnthropicRequest Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<Elastic.Clients.Elasticsearch.Inference.InferenceChunkingSettings?> propChunkingSettings = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Inference.AnthropicServiceSettings> propServiceSettings = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Inference.AnthropicTaskSettings?> propTaskSettings = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propChunkingSettings.TryReadProperty(ref reader, options, PropChunkingSettings, null))
			{
				continue;
			}

			if (reader.ValueTextEquals(PropService))
			{
				reader.Skip();
				continue;
			}

			if (propServiceSettings.TryReadProperty(ref reader, options, PropServiceSettings, null))
			{
				continue;
			}

			if (propTaskSettings.TryReadProperty(ref reader, options, PropTaskSettings, null))
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
		return new Elastic.Clients.Elasticsearch.Inference.PutAnthropicRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			ChunkingSettings = propChunkingSettings.Value,
			ServiceSettings = propServiceSettings.Value,
			TaskSettings = propTaskSettings.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Inference.PutAnthropicRequest value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropChunkingSettings, value.ChunkingSettings, null, null);
		writer.WriteProperty(options, PropService, value.Service, null, null);
		writer.WriteProperty(options, PropServiceSettings, value.ServiceSettings, null, null);
		writer.WriteProperty(options, PropTaskSettings, value.TaskSettings, null, null);
		writer.WriteEndObject();
	}
}

/// <summary>
/// <para>
/// Create an Anthropic inference endpoint.
/// </para>
/// <para>
/// Create an inference endpoint to perform an inference task with the <c>anthropic</c> service.
/// </para>
/// <para>
/// When you create an inference endpoint, the associated machine learning model is automatically deployed if it is not already running.
/// After creating the endpoint, wait for the model deployment to complete before using it.
/// To verify the deployment status, use the get trained model statistics API.
/// Look for <c>"state": "fully_allocated"</c> in the response and ensure that the <c>"allocation_count"</c> matches the <c>"target_allocation_count"</c>.
/// Avoid creating multiple endpoints for the same model unless required, as each endpoint consumes significant resources.
/// </para>
/// </summary>
[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Inference.PutAnthropicRequestConverter))]
public sealed partial class PutAnthropicRequest : Elastic.Clients.Elasticsearch.Requests.PlainRequest<Elastic.Clients.Elasticsearch.Inference.PutAnthropicRequestParameters>
{
	[System.Obsolete("The request contains additional required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public PutAnthropicRequest(Elastic.Clients.Elasticsearch.Inference.AnthropicTaskType taskType, Elastic.Clients.Elasticsearch.Id anthropicInferenceId) : base(r => r.Required("task_type", taskType).Required("anthropic_inference_id", anthropicInferenceId))
	{
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public PutAnthropicRequest(Elastic.Clients.Elasticsearch.Inference.AnthropicTaskType taskType, Elastic.Clients.Elasticsearch.Id anthropicInferenceId, Elastic.Clients.Elasticsearch.Inference.AnthropicServiceSettings serviceSettings) : base(r => r.Required("task_type", taskType).Required("anthropic_inference_id", anthropicInferenceId))
	{
		ServiceSettings = serviceSettings;
	}
#if NET7_0_OR_GREATER
	public PutAnthropicRequest()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal PutAnthropicRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	internal override Elastic.Clients.Elasticsearch.Requests.ApiUrls ApiUrls => Elastic.Clients.Elasticsearch.Requests.ApiUrlLookup.InferencePutAnthropic;

	protected override Elastic.Transport.HttpMethod StaticHttpMethod => Elastic.Transport.HttpMethod.PUT;

	internal override bool SupportsBody => true;

	internal override string OperationName => "inference.put_anthropic";

	/// <summary>
	/// <para>
	/// The unique identifier of the inference endpoint.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.Id AnthropicInferenceId { get => P<Elastic.Clients.Elasticsearch.Id>("anthropic_inference_id"); set => PR("anthropic_inference_id", value); }

	/// <summary>
	/// <para>
	/// The task type.
	/// The only valid task type for the model to perform is <c>completion</c>.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.Inference.AnthropicTaskType TaskType { get => P<Elastic.Clients.Elasticsearch.Inference.AnthropicTaskType>("task_type"); set => PR("task_type", value); }

	/// <summary>
	/// <para>
	/// The chunking configuration object.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Inference.InferenceChunkingSettings? ChunkingSettings { get; set; }

	/// <summary>
	/// <para>
	/// The type of service supported for the specified task type. In this case, <c>anthropic</c>.
	/// </para>
	/// </summary>
	public string Service => "anthropic";

	/// <summary>
	/// <para>
	/// Settings used to install the inference model. These settings are specific to the <c>watsonxai</c> service.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.Inference.AnthropicServiceSettings ServiceSettings { get; set; }

	/// <summary>
	/// <para>
	/// Settings to configure the inference task.
	/// These settings are specific to the task type you specified.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Inference.AnthropicTaskSettings? TaskSettings { get; set; }
}

/// <summary>
/// <para>
/// Create an Anthropic inference endpoint.
/// </para>
/// <para>
/// Create an inference endpoint to perform an inference task with the <c>anthropic</c> service.
/// </para>
/// <para>
/// When you create an inference endpoint, the associated machine learning model is automatically deployed if it is not already running.
/// After creating the endpoint, wait for the model deployment to complete before using it.
/// To verify the deployment status, use the get trained model statistics API.
/// Look for <c>"state": "fully_allocated"</c> in the response and ensure that the <c>"allocation_count"</c> matches the <c>"target_allocation_count"</c>.
/// Avoid creating multiple endpoints for the same model unless required, as each endpoint consumes significant resources.
/// </para>
/// </summary>
public readonly partial struct PutAnthropicRequestDescriptor
{
	internal Elastic.Clients.Elasticsearch.Inference.PutAnthropicRequest Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public PutAnthropicRequestDescriptor(Elastic.Clients.Elasticsearch.Inference.PutAnthropicRequest instance)
	{
		Instance = instance;
	}

	public PutAnthropicRequestDescriptor(Elastic.Clients.Elasticsearch.Inference.AnthropicTaskType taskType, Elastic.Clients.Elasticsearch.Id anthropicInferenceId)
	{
#pragma warning disable CS0618
		Instance = new Elastic.Clients.Elasticsearch.Inference.PutAnthropicRequest(taskType, anthropicInferenceId);
#pragma warning restore CS0618
	}

	[System.Obsolete("The use of the parameterless constructor is not permitted for this type.")]
	public PutAnthropicRequestDescriptor()
	{
		throw new System.InvalidOperationException("The use of the parameterless constructor is not permitted for this type.");
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Inference.PutAnthropicRequestDescriptor(Elastic.Clients.Elasticsearch.Inference.PutAnthropicRequest instance) => new Elastic.Clients.Elasticsearch.Inference.PutAnthropicRequestDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Inference.PutAnthropicRequest(Elastic.Clients.Elasticsearch.Inference.PutAnthropicRequestDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// The unique identifier of the inference endpoint.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Inference.PutAnthropicRequestDescriptor AnthropicInferenceId(Elastic.Clients.Elasticsearch.Id value)
	{
		Instance.AnthropicInferenceId = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The task type.
	/// The only valid task type for the model to perform is <c>completion</c>.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Inference.PutAnthropicRequestDescriptor TaskType(Elastic.Clients.Elasticsearch.Inference.AnthropicTaskType value)
	{
		Instance.TaskType = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The chunking configuration object.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Inference.PutAnthropicRequestDescriptor ChunkingSettings(Elastic.Clients.Elasticsearch.Inference.InferenceChunkingSettings? value)
	{
		Instance.ChunkingSettings = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The chunking configuration object.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Inference.PutAnthropicRequestDescriptor ChunkingSettings()
	{
		Instance.ChunkingSettings = Elastic.Clients.Elasticsearch.Inference.InferenceChunkingSettingsDescriptor.Build(null);
		return this;
	}

	/// <summary>
	/// <para>
	/// The chunking configuration object.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Inference.PutAnthropicRequestDescriptor ChunkingSettings(System.Action<Elastic.Clients.Elasticsearch.Inference.InferenceChunkingSettingsDescriptor>? action)
	{
		Instance.ChunkingSettings = Elastic.Clients.Elasticsearch.Inference.InferenceChunkingSettingsDescriptor.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// Settings used to install the inference model. These settings are specific to the <c>watsonxai</c> service.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Inference.PutAnthropicRequestDescriptor ServiceSettings(Elastic.Clients.Elasticsearch.Inference.AnthropicServiceSettings value)
	{
		Instance.ServiceSettings = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Settings used to install the inference model. These settings are specific to the <c>watsonxai</c> service.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Inference.PutAnthropicRequestDescriptor ServiceSettings(System.Action<Elastic.Clients.Elasticsearch.Inference.AnthropicServiceSettingsDescriptor> action)
	{
		Instance.ServiceSettings = Elastic.Clients.Elasticsearch.Inference.AnthropicServiceSettingsDescriptor.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// Settings to configure the inference task.
	/// These settings are specific to the task type you specified.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Inference.PutAnthropicRequestDescriptor TaskSettings(Elastic.Clients.Elasticsearch.Inference.AnthropicTaskSettings? value)
	{
		Instance.TaskSettings = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Settings to configure the inference task.
	/// These settings are specific to the task type you specified.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Inference.PutAnthropicRequestDescriptor TaskSettings(System.Action<Elastic.Clients.Elasticsearch.Inference.AnthropicTaskSettingsDescriptor> action)
	{
		Instance.TaskSettings = Elastic.Clients.Elasticsearch.Inference.AnthropicTaskSettingsDescriptor.Build(action);
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Inference.PutAnthropicRequest Build(System.Action<Elastic.Clients.Elasticsearch.Inference.PutAnthropicRequestDescriptor> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.Inference.PutAnthropicRequestDescriptor(new Elastic.Clients.Elasticsearch.Inference.PutAnthropicRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}

	public Elastic.Clients.Elasticsearch.Inference.PutAnthropicRequestDescriptor ErrorTrace(bool? value)
	{
		Instance.ErrorTrace = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Inference.PutAnthropicRequestDescriptor FilterPath(params string[]? value)
	{
		Instance.FilterPath = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Inference.PutAnthropicRequestDescriptor Human(bool? value)
	{
		Instance.Human = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Inference.PutAnthropicRequestDescriptor Pretty(bool? value)
	{
		Instance.Pretty = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Inference.PutAnthropicRequestDescriptor SourceQueryString(string? value)
	{
		Instance.SourceQueryString = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Inference.PutAnthropicRequestDescriptor RequestConfiguration(Elastic.Transport.IRequestConfiguration? value)
	{
		Instance.RequestConfiguration = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Inference.PutAnthropicRequestDescriptor RequestConfiguration(System.Func<Elastic.Transport.RequestConfigurationDescriptor, Elastic.Transport.IRequestConfiguration>? configurationSelector)
	{
		Instance.RequestConfiguration = configurationSelector.Invoke(Instance.RequestConfiguration is null ? new Elastic.Transport.RequestConfigurationDescriptor() : new Elastic.Transport.RequestConfigurationDescriptor(Instance.RequestConfiguration)) ?? Instance.RequestConfiguration;
		return this;
	}
}