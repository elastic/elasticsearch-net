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

public sealed partial class PutJinaaiRequestParameters : Elastic.Transport.RequestParameters
{
}

internal sealed partial class PutJinaaiRequestConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Inference.PutJinaaiRequest>
{
	private static readonly System.Text.Json.JsonEncodedText PropChunkingSettings = System.Text.Json.JsonEncodedText.Encode("chunking_settings");
	private static readonly System.Text.Json.JsonEncodedText PropService = System.Text.Json.JsonEncodedText.Encode("service");
	private static readonly System.Text.Json.JsonEncodedText PropServiceSettings = System.Text.Json.JsonEncodedText.Encode("service_settings");
	private static readonly System.Text.Json.JsonEncodedText PropTaskSettings = System.Text.Json.JsonEncodedText.Encode("task_settings");

	public override Elastic.Clients.Elasticsearch.Inference.PutJinaaiRequest Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<Elastic.Clients.Elasticsearch.Inference.InferenceChunkingSettings?> propChunkingSettings = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Inference.JinaAIServiceSettings> propServiceSettings = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Inference.JinaAITaskSettings?> propTaskSettings = default;
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
		return new Elastic.Clients.Elasticsearch.Inference.PutJinaaiRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			ChunkingSettings = propChunkingSettings.Value,
			ServiceSettings = propServiceSettings.Value,
			TaskSettings = propTaskSettings.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Inference.PutJinaaiRequest value, System.Text.Json.JsonSerializerOptions options)
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
/// Create an JinaAI inference endpoint.
/// </para>
/// <para>
/// Create an inference endpoint to perform an inference task with the <c>jinaai</c> service.
/// </para>
/// <para>
/// To review the available <c>rerank</c> models, refer to <a href="https://jina.ai/reranker">https://jina.ai/reranker</a>.
/// To review the available <c>text_embedding</c> models, refer to the <a href="https://jina.ai/embeddings/">https://jina.ai/embeddings/</a>.
/// </para>
/// </summary>
[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Inference.PutJinaaiRequestConverter))]
public sealed partial class PutJinaaiRequest : Elastic.Clients.Elasticsearch.Requests.PlainRequest<Elastic.Clients.Elasticsearch.Inference.PutJinaaiRequestParameters>
{
	[System.Obsolete("The request contains additional required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public PutJinaaiRequest(Elastic.Clients.Elasticsearch.Inference.JinaAITaskType taskType, Elastic.Clients.Elasticsearch.Id jinaaiInferenceId) : base(r => r.Required("task_type", taskType).Required("jinaai_inference_id", jinaaiInferenceId))
	{
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public PutJinaaiRequest(Elastic.Clients.Elasticsearch.Inference.JinaAITaskType taskType, Elastic.Clients.Elasticsearch.Id jinaaiInferenceId, Elastic.Clients.Elasticsearch.Inference.JinaAIServiceSettings serviceSettings) : base(r => r.Required("task_type", taskType).Required("jinaai_inference_id", jinaaiInferenceId))
	{
		ServiceSettings = serviceSettings;
	}
#if NET7_0_OR_GREATER
	public PutJinaaiRequest()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal PutJinaaiRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	internal override Elastic.Clients.Elasticsearch.Requests.ApiUrls ApiUrls => Elastic.Clients.Elasticsearch.Requests.ApiUrlLookup.InferencePutJinaai;

	protected override Elastic.Transport.HttpMethod StaticHttpMethod => Elastic.Transport.HttpMethod.PUT;

	internal override bool SupportsBody => true;

	internal override string OperationName => "inference.put_jinaai";

	/// <summary>
	/// <para>
	/// The unique identifier of the inference endpoint.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.Id JinaaiInferenceId { get => P<Elastic.Clients.Elasticsearch.Id>("jinaai_inference_id"); set => PR("jinaai_inference_id", value); }

	/// <summary>
	/// <para>
	/// The type of the inference task that the model will perform.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.Inference.JinaAITaskType TaskType { get => P<Elastic.Clients.Elasticsearch.Inference.JinaAITaskType>("task_type"); set => PR("task_type", value); }

	/// <summary>
	/// <para>
	/// The chunking configuration object.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Inference.InferenceChunkingSettings? ChunkingSettings { get; set; }

	/// <summary>
	/// <para>
	/// The type of service supported for the specified task type. In this case, <c>jinaai</c>.
	/// </para>
	/// </summary>
	public string Service => "jinaai";

	/// <summary>
	/// <para>
	/// Settings used to install the inference model. These settings are specific to the <c>jinaai</c> service.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.Inference.JinaAIServiceSettings ServiceSettings { get; set; }

	/// <summary>
	/// <para>
	/// Settings to configure the inference task.
	/// These settings are specific to the task type you specified.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Inference.JinaAITaskSettings? TaskSettings { get; set; }
}

/// <summary>
/// <para>
/// Create an JinaAI inference endpoint.
/// </para>
/// <para>
/// Create an inference endpoint to perform an inference task with the <c>jinaai</c> service.
/// </para>
/// <para>
/// To review the available <c>rerank</c> models, refer to <a href="https://jina.ai/reranker">https://jina.ai/reranker</a>.
/// To review the available <c>text_embedding</c> models, refer to the <a href="https://jina.ai/embeddings/">https://jina.ai/embeddings/</a>.
/// </para>
/// </summary>
public readonly partial struct PutJinaaiRequestDescriptor
{
	internal Elastic.Clients.Elasticsearch.Inference.PutJinaaiRequest Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public PutJinaaiRequestDescriptor(Elastic.Clients.Elasticsearch.Inference.PutJinaaiRequest instance)
	{
		Instance = instance;
	}

	public PutJinaaiRequestDescriptor(Elastic.Clients.Elasticsearch.Inference.JinaAITaskType taskType, Elastic.Clients.Elasticsearch.Id jinaaiInferenceId)
	{
#pragma warning disable CS0618
		Instance = new Elastic.Clients.Elasticsearch.Inference.PutJinaaiRequest(taskType, jinaaiInferenceId);
#pragma warning restore CS0618
	}

	[System.Obsolete("The use of the parameterless constructor is not permitted for this type.")]
	public PutJinaaiRequestDescriptor()
	{
		throw new System.InvalidOperationException("The use of the parameterless constructor is not permitted for this type.");
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Inference.PutJinaaiRequestDescriptor(Elastic.Clients.Elasticsearch.Inference.PutJinaaiRequest instance) => new Elastic.Clients.Elasticsearch.Inference.PutJinaaiRequestDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Inference.PutJinaaiRequest(Elastic.Clients.Elasticsearch.Inference.PutJinaaiRequestDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// The unique identifier of the inference endpoint.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Inference.PutJinaaiRequestDescriptor JinaaiInferenceId(Elastic.Clients.Elasticsearch.Id value)
	{
		Instance.JinaaiInferenceId = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The type of the inference task that the model will perform.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Inference.PutJinaaiRequestDescriptor TaskType(Elastic.Clients.Elasticsearch.Inference.JinaAITaskType value)
	{
		Instance.TaskType = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The chunking configuration object.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Inference.PutJinaaiRequestDescriptor ChunkingSettings(Elastic.Clients.Elasticsearch.Inference.InferenceChunkingSettings? value)
	{
		Instance.ChunkingSettings = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The chunking configuration object.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Inference.PutJinaaiRequestDescriptor ChunkingSettings()
	{
		Instance.ChunkingSettings = Elastic.Clients.Elasticsearch.Inference.InferenceChunkingSettingsDescriptor.Build(null);
		return this;
	}

	/// <summary>
	/// <para>
	/// The chunking configuration object.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Inference.PutJinaaiRequestDescriptor ChunkingSettings(System.Action<Elastic.Clients.Elasticsearch.Inference.InferenceChunkingSettingsDescriptor>? action)
	{
		Instance.ChunkingSettings = Elastic.Clients.Elasticsearch.Inference.InferenceChunkingSettingsDescriptor.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// Settings used to install the inference model. These settings are specific to the <c>jinaai</c> service.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Inference.PutJinaaiRequestDescriptor ServiceSettings(Elastic.Clients.Elasticsearch.Inference.JinaAIServiceSettings value)
	{
		Instance.ServiceSettings = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Settings used to install the inference model. These settings are specific to the <c>jinaai</c> service.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Inference.PutJinaaiRequestDescriptor ServiceSettings(System.Action<Elastic.Clients.Elasticsearch.Inference.JinaAiServiceSettingsDescriptor> action)
	{
		Instance.ServiceSettings = Elastic.Clients.Elasticsearch.Inference.JinaAiServiceSettingsDescriptor.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// Settings to configure the inference task.
	/// These settings are specific to the task type you specified.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Inference.PutJinaaiRequestDescriptor TaskSettings(Elastic.Clients.Elasticsearch.Inference.JinaAITaskSettings? value)
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
	public Elastic.Clients.Elasticsearch.Inference.PutJinaaiRequestDescriptor TaskSettings()
	{
		Instance.TaskSettings = Elastic.Clients.Elasticsearch.Inference.JinaAiTaskSettingsDescriptor.Build(null);
		return this;
	}

	/// <summary>
	/// <para>
	/// Settings to configure the inference task.
	/// These settings are specific to the task type you specified.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Inference.PutJinaaiRequestDescriptor TaskSettings(System.Action<Elastic.Clients.Elasticsearch.Inference.JinaAiTaskSettingsDescriptor>? action)
	{
		Instance.TaskSettings = Elastic.Clients.Elasticsearch.Inference.JinaAiTaskSettingsDescriptor.Build(action);
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Inference.PutJinaaiRequest Build(System.Action<Elastic.Clients.Elasticsearch.Inference.PutJinaaiRequestDescriptor> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.Inference.PutJinaaiRequestDescriptor(new Elastic.Clients.Elasticsearch.Inference.PutJinaaiRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}

	public Elastic.Clients.Elasticsearch.Inference.PutJinaaiRequestDescriptor ErrorTrace(bool? value)
	{
		Instance.ErrorTrace = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Inference.PutJinaaiRequestDescriptor FilterPath(params string[]? value)
	{
		Instance.FilterPath = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Inference.PutJinaaiRequestDescriptor Human(bool? value)
	{
		Instance.Human = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Inference.PutJinaaiRequestDescriptor Pretty(bool? value)
	{
		Instance.Pretty = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Inference.PutJinaaiRequestDescriptor SourceQueryString(string? value)
	{
		Instance.SourceQueryString = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Inference.PutJinaaiRequestDescriptor RequestConfiguration(Elastic.Transport.IRequestConfiguration? value)
	{
		Instance.RequestConfiguration = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Inference.PutJinaaiRequestDescriptor RequestConfiguration(System.Func<Elastic.Transport.RequestConfigurationDescriptor, Elastic.Transport.IRequestConfiguration>? configurationSelector)
	{
		Instance.RequestConfiguration = configurationSelector.Invoke(Instance.RequestConfiguration is null ? new Elastic.Transport.RequestConfigurationDescriptor() : new Elastic.Transport.RequestConfigurationDescriptor(Instance.RequestConfiguration)) ?? Instance.RequestConfiguration;
		return this;
	}
}