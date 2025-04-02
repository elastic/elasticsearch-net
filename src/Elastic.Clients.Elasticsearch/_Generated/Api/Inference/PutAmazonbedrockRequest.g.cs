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

public sealed partial class PutAmazonbedrockRequestParameters : Elastic.Transport.RequestParameters
{
}

internal sealed partial class PutAmazonbedrockRequestConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Inference.PutAmazonbedrockRequest>
{
	private static readonly System.Text.Json.JsonEncodedText PropChunkingSettings = System.Text.Json.JsonEncodedText.Encode("chunking_settings");
	private static readonly System.Text.Json.JsonEncodedText PropService = System.Text.Json.JsonEncodedText.Encode("service");
	private static readonly System.Text.Json.JsonEncodedText PropServiceSettings = System.Text.Json.JsonEncodedText.Encode("service_settings");
	private static readonly System.Text.Json.JsonEncodedText PropTaskSettings = System.Text.Json.JsonEncodedText.Encode("task_settings");

	public override Elastic.Clients.Elasticsearch.Inference.PutAmazonbedrockRequest Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<Elastic.Clients.Elasticsearch.Inference.InferenceChunkingSettings?> propChunkingSettings = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Inference.AmazonBedrockServiceType> propService = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Inference.AmazonBedrockServiceSettings> propServiceSettings = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Inference.AmazonBedrockTaskSettings?> propTaskSettings = default;
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
		return new Elastic.Clients.Elasticsearch.Inference.PutAmazonbedrockRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			ChunkingSettings = propChunkingSettings.Value,
			Service = propService.Value,
			ServiceSettings = propServiceSettings.Value,
			TaskSettings = propTaskSettings.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Inference.PutAmazonbedrockRequest value, System.Text.Json.JsonSerializerOptions options)
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
/// Create an Amazon Bedrock inference endpoint.
/// </para>
/// <para>
/// Creates an inference endpoint to perform an inference task with the <c>amazonbedrock</c> service.
/// </para>
/// <para>
/// info
/// You need to provide the access and secret keys only once, during the inference model creation. The get inference API does not retrieve your access or secret keys. After creating the inference model, you cannot change the associated key pairs. If you want to use a different access and secret key pair, delete the inference model and recreate it with the same name and the updated keys.
/// </para>
/// <para>
/// When you create an inference endpoint, the associated machine learning model is automatically deployed if it is not already running.
/// After creating the endpoint, wait for the model deployment to complete before using it.
/// To verify the deployment status, use the get trained model statistics API.
/// Look for <c>"state": "fully_allocated"</c> in the response and ensure that the <c>"allocation_count"</c> matches the <c>"target_allocation_count"</c>.
/// Avoid creating multiple endpoints for the same model unless required, as each endpoint consumes significant resources.
/// </para>
/// </summary>
[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Inference.PutAmazonbedrockRequestConverter))]
public sealed partial class PutAmazonbedrockRequest : Elastic.Clients.Elasticsearch.Requests.PlainRequest<Elastic.Clients.Elasticsearch.Inference.PutAmazonbedrockRequestParameters>
{
	[System.Obsolete("The request contains additional required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public PutAmazonbedrockRequest(Elastic.Clients.Elasticsearch.Inference.AmazonBedrockTaskType taskType, Elastic.Clients.Elasticsearch.Id amazonbedrockInferenceId) : base(r => r.Required("task_type", taskType).Required("amazonbedrock_inference_id", amazonbedrockInferenceId))
	{
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public PutAmazonbedrockRequest(Elastic.Clients.Elasticsearch.Inference.AmazonBedrockTaskType taskType, Elastic.Clients.Elasticsearch.Id amazonbedrockInferenceId, Elastic.Clients.Elasticsearch.Inference.AmazonBedrockServiceType service, Elastic.Clients.Elasticsearch.Inference.AmazonBedrockServiceSettings serviceSettings) : base(r => r.Required("task_type", taskType).Required("amazonbedrock_inference_id", amazonbedrockInferenceId))
	{
		Service = service;
		ServiceSettings = serviceSettings;
	}
#if NET7_0_OR_GREATER
	public PutAmazonbedrockRequest()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal PutAmazonbedrockRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	internal override Elastic.Clients.Elasticsearch.Requests.ApiUrls ApiUrls => Elastic.Clients.Elasticsearch.Requests.ApiUrlLookup.InferencePutAmazonbedrock;

	protected override Elastic.Transport.HttpMethod StaticHttpMethod => Elastic.Transport.HttpMethod.PUT;

	internal override bool SupportsBody => true;

	internal override string OperationName => "inference.put_amazonbedrock";

	/// <summary>
	/// <para>
	/// The unique identifier of the inference endpoint.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.Id AmazonbedrockInferenceId { get => P<Elastic.Clients.Elasticsearch.Id>("amazonbedrock_inference_id"); set => PR("amazonbedrock_inference_id", value); }

	/// <summary>
	/// <para>
	/// The type of the inference task that the model will perform.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.Inference.AmazonBedrockTaskType TaskType { get => P<Elastic.Clients.Elasticsearch.Inference.AmazonBedrockTaskType>("task_type"); set => PR("task_type", value); }

	/// <summary>
	/// <para>
	/// The chunking configuration object.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Inference.InferenceChunkingSettings? ChunkingSettings { get; set; }

	/// <summary>
	/// <para>
	/// The type of service supported for the specified task type. In this case, <c>amazonbedrock</c>.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.Inference.AmazonBedrockServiceType Service { get; set; }

	/// <summary>
	/// <para>
	/// Settings used to install the inference model. These settings are specific to the <c>amazonbedrock</c> service.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.Inference.AmazonBedrockServiceSettings ServiceSettings { get; set; }

	/// <summary>
	/// <para>
	/// Settings to configure the inference task.
	/// These settings are specific to the task type you specified.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Inference.AmazonBedrockTaskSettings? TaskSettings { get; set; }
}

/// <summary>
/// <para>
/// Create an Amazon Bedrock inference endpoint.
/// </para>
/// <para>
/// Creates an inference endpoint to perform an inference task with the <c>amazonbedrock</c> service.
/// </para>
/// <para>
/// info
/// You need to provide the access and secret keys only once, during the inference model creation. The get inference API does not retrieve your access or secret keys. After creating the inference model, you cannot change the associated key pairs. If you want to use a different access and secret key pair, delete the inference model and recreate it with the same name and the updated keys.
/// </para>
/// <para>
/// When you create an inference endpoint, the associated machine learning model is automatically deployed if it is not already running.
/// After creating the endpoint, wait for the model deployment to complete before using it.
/// To verify the deployment status, use the get trained model statistics API.
/// Look for <c>"state": "fully_allocated"</c> in the response and ensure that the <c>"allocation_count"</c> matches the <c>"target_allocation_count"</c>.
/// Avoid creating multiple endpoints for the same model unless required, as each endpoint consumes significant resources.
/// </para>
/// </summary>
public readonly partial struct PutAmazonbedrockRequestDescriptor
{
	internal Elastic.Clients.Elasticsearch.Inference.PutAmazonbedrockRequest Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public PutAmazonbedrockRequestDescriptor(Elastic.Clients.Elasticsearch.Inference.PutAmazonbedrockRequest instance)
	{
		Instance = instance;
	}

	public PutAmazonbedrockRequestDescriptor(Elastic.Clients.Elasticsearch.Inference.AmazonBedrockTaskType taskType, Elastic.Clients.Elasticsearch.Id amazonbedrockInferenceId)
	{
#pragma warning disable CS0618
		Instance = new Elastic.Clients.Elasticsearch.Inference.PutAmazonbedrockRequest(taskType, amazonbedrockInferenceId);
#pragma warning restore CS0618
	}

	[System.Obsolete("TODO")]
	public PutAmazonbedrockRequestDescriptor()
	{
		throw new System.InvalidOperationException("The use of the parameterless constructor is not permitted for this type.");
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Inference.PutAmazonbedrockRequestDescriptor(Elastic.Clients.Elasticsearch.Inference.PutAmazonbedrockRequest instance) => new Elastic.Clients.Elasticsearch.Inference.PutAmazonbedrockRequestDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Inference.PutAmazonbedrockRequest(Elastic.Clients.Elasticsearch.Inference.PutAmazonbedrockRequestDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// The unique identifier of the inference endpoint.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Inference.PutAmazonbedrockRequestDescriptor AmazonbedrockInferenceId(Elastic.Clients.Elasticsearch.Id value)
	{
		Instance.AmazonbedrockInferenceId = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The type of the inference task that the model will perform.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Inference.PutAmazonbedrockRequestDescriptor TaskType(Elastic.Clients.Elasticsearch.Inference.AmazonBedrockTaskType value)
	{
		Instance.TaskType = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The chunking configuration object.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Inference.PutAmazonbedrockRequestDescriptor ChunkingSettings(Elastic.Clients.Elasticsearch.Inference.InferenceChunkingSettings? value)
	{
		Instance.ChunkingSettings = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The chunking configuration object.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Inference.PutAmazonbedrockRequestDescriptor ChunkingSettings()
	{
		Instance.ChunkingSettings = Elastic.Clients.Elasticsearch.Inference.InferenceChunkingSettingsDescriptor.Build(null);
		return this;
	}

	/// <summary>
	/// <para>
	/// The chunking configuration object.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Inference.PutAmazonbedrockRequestDescriptor ChunkingSettings(System.Action<Elastic.Clients.Elasticsearch.Inference.InferenceChunkingSettingsDescriptor>? action)
	{
		Instance.ChunkingSettings = Elastic.Clients.Elasticsearch.Inference.InferenceChunkingSettingsDescriptor.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// The type of service supported for the specified task type. In this case, <c>amazonbedrock</c>.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Inference.PutAmazonbedrockRequestDescriptor Service(Elastic.Clients.Elasticsearch.Inference.AmazonBedrockServiceType value)
	{
		Instance.Service = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Settings used to install the inference model. These settings are specific to the <c>amazonbedrock</c> service.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Inference.PutAmazonbedrockRequestDescriptor ServiceSettings(Elastic.Clients.Elasticsearch.Inference.AmazonBedrockServiceSettings value)
	{
		Instance.ServiceSettings = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Settings used to install the inference model. These settings are specific to the <c>amazonbedrock</c> service.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Inference.PutAmazonbedrockRequestDescriptor ServiceSettings(System.Action<Elastic.Clients.Elasticsearch.Inference.AmazonBedrockServiceSettingsDescriptor> action)
	{
		Instance.ServiceSettings = Elastic.Clients.Elasticsearch.Inference.AmazonBedrockServiceSettingsDescriptor.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// Settings to configure the inference task.
	/// These settings are specific to the task type you specified.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Inference.PutAmazonbedrockRequestDescriptor TaskSettings(Elastic.Clients.Elasticsearch.Inference.AmazonBedrockTaskSettings? value)
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
	public Elastic.Clients.Elasticsearch.Inference.PutAmazonbedrockRequestDescriptor TaskSettings()
	{
		Instance.TaskSettings = Elastic.Clients.Elasticsearch.Inference.AmazonBedrockTaskSettingsDescriptor.Build(null);
		return this;
	}

	/// <summary>
	/// <para>
	/// Settings to configure the inference task.
	/// These settings are specific to the task type you specified.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Inference.PutAmazonbedrockRequestDescriptor TaskSettings(System.Action<Elastic.Clients.Elasticsearch.Inference.AmazonBedrockTaskSettingsDescriptor>? action)
	{
		Instance.TaskSettings = Elastic.Clients.Elasticsearch.Inference.AmazonBedrockTaskSettingsDescriptor.Build(action);
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Inference.PutAmazonbedrockRequest Build(System.Action<Elastic.Clients.Elasticsearch.Inference.PutAmazonbedrockRequestDescriptor> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.Inference.PutAmazonbedrockRequestDescriptor(new Elastic.Clients.Elasticsearch.Inference.PutAmazonbedrockRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}

	public Elastic.Clients.Elasticsearch.Inference.PutAmazonbedrockRequestDescriptor ErrorTrace(bool? value)
	{
		Instance.ErrorTrace = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Inference.PutAmazonbedrockRequestDescriptor FilterPath(params string[]? value)
	{
		Instance.FilterPath = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Inference.PutAmazonbedrockRequestDescriptor Human(bool? value)
	{
		Instance.Human = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Inference.PutAmazonbedrockRequestDescriptor Pretty(bool? value)
	{
		Instance.Pretty = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Inference.PutAmazonbedrockRequestDescriptor SourceQueryString(string? value)
	{
		Instance.SourceQueryString = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Inference.PutAmazonbedrockRequestDescriptor RequestConfiguration(Elastic.Transport.IRequestConfiguration? value)
	{
		Instance.RequestConfiguration = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Inference.PutAmazonbedrockRequestDescriptor RequestConfiguration(System.Func<Elastic.Transport.RequestConfigurationDescriptor, Elastic.Transport.IRequestConfiguration>? configurationSelector)
	{
		Instance.RequestConfiguration = configurationSelector.Invoke(Instance.RequestConfiguration is null ? new Elastic.Transport.RequestConfigurationDescriptor() : new Elastic.Transport.RequestConfigurationDescriptor(Instance.RequestConfiguration)) ?? Instance.RequestConfiguration;
		return this;
	}
}