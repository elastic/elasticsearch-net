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

public sealed partial class PutOpenaiRequestParameters : Elastic.Transport.RequestParameters
{
}

internal sealed partial class PutOpenaiRequestConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Inference.PutOpenaiRequest>
{
	private static readonly System.Text.Json.JsonEncodedText PropChunkingSettings = System.Text.Json.JsonEncodedText.Encode("chunking_settings");
	private static readonly System.Text.Json.JsonEncodedText PropService = System.Text.Json.JsonEncodedText.Encode("service");
	private static readonly System.Text.Json.JsonEncodedText PropServiceSettings = System.Text.Json.JsonEncodedText.Encode("service_settings");
	private static readonly System.Text.Json.JsonEncodedText PropTaskSettings = System.Text.Json.JsonEncodedText.Encode("task_settings");

	public override Elastic.Clients.Elasticsearch.Inference.PutOpenaiRequest Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<Elastic.Clients.Elasticsearch.Inference.InferenceChunkingSettings?> propChunkingSettings = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Inference.OpenAIServiceType> propService = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Inference.OpenAIServiceSettings> propServiceSettings = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Inference.OpenAITaskSettings?> propTaskSettings = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propChunkingSettings.TryReadProperty(ref reader, options, PropChunkingSettings, null))
			{
				continue;
			}

			if (propService.TryReadProperty(ref reader, options, PropService, null))
			{
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
		return new Elastic.Clients.Elasticsearch.Inference.PutOpenaiRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			ChunkingSettings = propChunkingSettings.Value,
			Service = propService.Value,
			ServiceSettings = propServiceSettings.Value,
			TaskSettings = propTaskSettings.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Inference.PutOpenaiRequest value, System.Text.Json.JsonSerializerOptions options)
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
/// Create an OpenAI inference endpoint.
/// </para>
/// <para>
/// Create an inference endpoint to perform an inference task with the <c>openai</c> service.
/// </para>
/// <para>
/// When you create an inference endpoint, the associated machine learning model is automatically deployed if it is not already running.
/// After creating the endpoint, wait for the model deployment to complete before using it.
/// To verify the deployment status, use the get trained model statistics API.
/// Look for <c>"state": "fully_allocated"</c> in the response and ensure that the <c>"allocation_count"</c> matches the <c>"target_allocation_count"</c>.
/// Avoid creating multiple endpoints for the same model unless required, as each endpoint consumes significant resources.
/// </para>
/// </summary>
[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Inference.PutOpenaiRequestConverter))]
public sealed partial class PutOpenaiRequest : Elastic.Clients.Elasticsearch.Requests.PlainRequest<Elastic.Clients.Elasticsearch.Inference.PutOpenaiRequestParameters>
{
	[System.Obsolete("The request contains additional required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public PutOpenaiRequest(Elastic.Clients.Elasticsearch.Inference.OpenAITaskType taskType, Elastic.Clients.Elasticsearch.Id openaiInferenceId) : base(r => r.Required("task_type", taskType).Required("openai_inference_id", openaiInferenceId))
	{
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public PutOpenaiRequest(Elastic.Clients.Elasticsearch.Inference.OpenAITaskType taskType, Elastic.Clients.Elasticsearch.Id openaiInferenceId, Elastic.Clients.Elasticsearch.Inference.OpenAIServiceType service, Elastic.Clients.Elasticsearch.Inference.OpenAIServiceSettings serviceSettings) : base(r => r.Required("task_type", taskType).Required("openai_inference_id", openaiInferenceId))
	{
		Service = service;
		ServiceSettings = serviceSettings;
	}
#if NET7_0_OR_GREATER
	public PutOpenaiRequest()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal PutOpenaiRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	internal override Elastic.Clients.Elasticsearch.Requests.ApiUrls ApiUrls => Elastic.Clients.Elasticsearch.Requests.ApiUrlLookup.InferencePutOpenai;

	protected override Elastic.Transport.HttpMethod StaticHttpMethod => Elastic.Transport.HttpMethod.PUT;

	internal override bool SupportsBody => true;

	internal override string OperationName => "inference.put_openai";

	/// <summary>
	/// <para>
	/// The unique identifier of the inference endpoint.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.Id OpenaiInferenceId { get => P<Elastic.Clients.Elasticsearch.Id>("openai_inference_id"); set => PR("openai_inference_id", value); }

	/// <summary>
	/// <para>
	/// The type of the inference task that the model will perform.
	/// NOTE: The <c>chat_completion</c> task type only supports streaming and only through the _stream API.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.Inference.OpenAITaskType TaskType { get => P<Elastic.Clients.Elasticsearch.Inference.OpenAITaskType>("task_type"); set => PR("task_type", value); }

	/// <summary>
	/// <para>
	/// The chunking configuration object.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Inference.InferenceChunkingSettings? ChunkingSettings { get; set; }

	/// <summary>
	/// <para>
	/// The type of service supported for the specified task type. In this case, <c>openai</c>.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.Inference.OpenAIServiceType Service { get; set; }

	/// <summary>
	/// <para>
	/// Settings used to install the inference model. These settings are specific to the <c>openai</c> service.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.Inference.OpenAIServiceSettings ServiceSettings { get; set; }

	/// <summary>
	/// <para>
	/// Settings to configure the inference task.
	/// These settings are specific to the task type you specified.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Inference.OpenAITaskSettings? TaskSettings { get; set; }
}

/// <summary>
/// <para>
/// Create an OpenAI inference endpoint.
/// </para>
/// <para>
/// Create an inference endpoint to perform an inference task with the <c>openai</c> service.
/// </para>
/// <para>
/// When you create an inference endpoint, the associated machine learning model is automatically deployed if it is not already running.
/// After creating the endpoint, wait for the model deployment to complete before using it.
/// To verify the deployment status, use the get trained model statistics API.
/// Look for <c>"state": "fully_allocated"</c> in the response and ensure that the <c>"allocation_count"</c> matches the <c>"target_allocation_count"</c>.
/// Avoid creating multiple endpoints for the same model unless required, as each endpoint consumes significant resources.
/// </para>
/// </summary>
public readonly partial struct PutOpenaiRequestDescriptor
{
	internal Elastic.Clients.Elasticsearch.Inference.PutOpenaiRequest Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public PutOpenaiRequestDescriptor(Elastic.Clients.Elasticsearch.Inference.PutOpenaiRequest instance)
	{
		Instance = instance;
	}

	public PutOpenaiRequestDescriptor(Elastic.Clients.Elasticsearch.Inference.OpenAITaskType taskType, Elastic.Clients.Elasticsearch.Id openaiInferenceId)
	{
#pragma warning disable CS0618
		Instance = new Elastic.Clients.Elasticsearch.Inference.PutOpenaiRequest(taskType, openaiInferenceId);
#pragma warning restore CS0618
	}

	[System.Obsolete("TODO")]
	public PutOpenaiRequestDescriptor()
	{
		throw new System.InvalidOperationException("The use of the parameterless constructor is not permitted for this type.");
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Inference.PutOpenaiRequestDescriptor(Elastic.Clients.Elasticsearch.Inference.PutOpenaiRequest instance) => new Elastic.Clients.Elasticsearch.Inference.PutOpenaiRequestDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Inference.PutOpenaiRequest(Elastic.Clients.Elasticsearch.Inference.PutOpenaiRequestDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// The unique identifier of the inference endpoint.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Inference.PutOpenaiRequestDescriptor OpenaiInferenceId(Elastic.Clients.Elasticsearch.Id value)
	{
		Instance.OpenaiInferenceId = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The type of the inference task that the model will perform.
	/// NOTE: The <c>chat_completion</c> task type only supports streaming and only through the _stream API.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Inference.PutOpenaiRequestDescriptor TaskType(Elastic.Clients.Elasticsearch.Inference.OpenAITaskType value)
	{
		Instance.TaskType = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The chunking configuration object.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Inference.PutOpenaiRequestDescriptor ChunkingSettings(Elastic.Clients.Elasticsearch.Inference.InferenceChunkingSettings? value)
	{
		Instance.ChunkingSettings = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The chunking configuration object.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Inference.PutOpenaiRequestDescriptor ChunkingSettings()
	{
		Instance.ChunkingSettings = Elastic.Clients.Elasticsearch.Inference.InferenceChunkingSettingsDescriptor.Build(null);
		return this;
	}

	/// <summary>
	/// <para>
	/// The chunking configuration object.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Inference.PutOpenaiRequestDescriptor ChunkingSettings(System.Action<Elastic.Clients.Elasticsearch.Inference.InferenceChunkingSettingsDescriptor>? action)
	{
		Instance.ChunkingSettings = Elastic.Clients.Elasticsearch.Inference.InferenceChunkingSettingsDescriptor.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// The type of service supported for the specified task type. In this case, <c>openai</c>.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Inference.PutOpenaiRequestDescriptor Service(Elastic.Clients.Elasticsearch.Inference.OpenAIServiceType value)
	{
		Instance.Service = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Settings used to install the inference model. These settings are specific to the <c>openai</c> service.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Inference.PutOpenaiRequestDescriptor ServiceSettings(Elastic.Clients.Elasticsearch.Inference.OpenAIServiceSettings value)
	{
		Instance.ServiceSettings = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Settings used to install the inference model. These settings are specific to the <c>openai</c> service.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Inference.PutOpenaiRequestDescriptor ServiceSettings(System.Action<Elastic.Clients.Elasticsearch.Inference.OpenAiServiceSettingsDescriptor> action)
	{
		Instance.ServiceSettings = Elastic.Clients.Elasticsearch.Inference.OpenAiServiceSettingsDescriptor.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// Settings to configure the inference task.
	/// These settings are specific to the task type you specified.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Inference.PutOpenaiRequestDescriptor TaskSettings(Elastic.Clients.Elasticsearch.Inference.OpenAITaskSettings? value)
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
	public Elastic.Clients.Elasticsearch.Inference.PutOpenaiRequestDescriptor TaskSettings()
	{
		Instance.TaskSettings = Elastic.Clients.Elasticsearch.Inference.OpenAiTaskSettingsDescriptor.Build(null);
		return this;
	}

	/// <summary>
	/// <para>
	/// Settings to configure the inference task.
	/// These settings are specific to the task type you specified.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Inference.PutOpenaiRequestDescriptor TaskSettings(System.Action<Elastic.Clients.Elasticsearch.Inference.OpenAiTaskSettingsDescriptor>? action)
	{
		Instance.TaskSettings = Elastic.Clients.Elasticsearch.Inference.OpenAiTaskSettingsDescriptor.Build(action);
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Inference.PutOpenaiRequest Build(System.Action<Elastic.Clients.Elasticsearch.Inference.PutOpenaiRequestDescriptor> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.Inference.PutOpenaiRequestDescriptor(new Elastic.Clients.Elasticsearch.Inference.PutOpenaiRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}

	public Elastic.Clients.Elasticsearch.Inference.PutOpenaiRequestDescriptor ErrorTrace(bool? value)
	{
		Instance.ErrorTrace = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Inference.PutOpenaiRequestDescriptor FilterPath(params string[]? value)
	{
		Instance.FilterPath = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Inference.PutOpenaiRequestDescriptor Human(bool? value)
	{
		Instance.Human = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Inference.PutOpenaiRequestDescriptor Pretty(bool? value)
	{
		Instance.Pretty = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Inference.PutOpenaiRequestDescriptor SourceQueryString(string? value)
	{
		Instance.SourceQueryString = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Inference.PutOpenaiRequestDescriptor RequestConfiguration(Elastic.Transport.IRequestConfiguration? value)
	{
		Instance.RequestConfiguration = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Inference.PutOpenaiRequestDescriptor RequestConfiguration(System.Func<Elastic.Transport.RequestConfigurationDescriptor, Elastic.Transport.IRequestConfiguration>? configurationSelector)
	{
		Instance.RequestConfiguration = configurationSelector.Invoke(Instance.RequestConfiguration is null ? new Elastic.Transport.RequestConfigurationDescriptor() : new Elastic.Transport.RequestConfigurationDescriptor(Instance.RequestConfiguration)) ?? Instance.RequestConfiguration;
		return this;
	}
}