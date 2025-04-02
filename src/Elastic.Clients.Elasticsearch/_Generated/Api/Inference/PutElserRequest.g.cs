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

public sealed partial class PutElserRequestParameters : Elastic.Transport.RequestParameters
{
}

internal sealed partial class PutElserRequestConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Inference.PutElserRequest>
{
	private static readonly System.Text.Json.JsonEncodedText PropChunkingSettings = System.Text.Json.JsonEncodedText.Encode("chunking_settings");
	private static readonly System.Text.Json.JsonEncodedText PropService = System.Text.Json.JsonEncodedText.Encode("service");
	private static readonly System.Text.Json.JsonEncodedText PropServiceSettings = System.Text.Json.JsonEncodedText.Encode("service_settings");

	public override Elastic.Clients.Elasticsearch.Inference.PutElserRequest Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<Elastic.Clients.Elasticsearch.Inference.InferenceChunkingSettings?> propChunkingSettings = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Inference.ElserServiceType> propService = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Inference.ElserServiceSettings> propServiceSettings = default;
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

			if (options.UnmappedMemberHandling is System.Text.Json.Serialization.JsonUnmappedMemberHandling.Skip)
			{
				reader.Skip();
				continue;
			}

			throw new System.Text.Json.JsonException($"Unknown JSON property '{reader.GetString()}' for type '{typeToConvert.Name}'.");
		}

		reader.ValidateToken(System.Text.Json.JsonTokenType.EndObject);
		return new Elastic.Clients.Elasticsearch.Inference.PutElserRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			ChunkingSettings = propChunkingSettings.Value,
			Service = propService.Value,
			ServiceSettings = propServiceSettings.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Inference.PutElserRequest value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropChunkingSettings, value.ChunkingSettings, null, null);
		writer.WriteProperty(options, PropService, value.Service, null, null);
		writer.WriteProperty(options, PropServiceSettings, value.ServiceSettings, null, null);
		writer.WriteEndObject();
	}
}

/// <summary>
/// <para>
/// Create an ELSER inference endpoint.
/// </para>
/// <para>
/// Create an inference endpoint to perform an inference task with the <c>elser</c> service.
/// You can also deploy ELSER by using the Elasticsearch inference integration.
/// </para>
/// <para>
/// info
/// Your Elasticsearch deployment contains a preconfigured ELSER inference endpoint, you only need to create the enpoint using the API if you want to customize the settings.
/// </para>
/// <para>
/// The API request will automatically download and deploy the ELSER model if it isn't already downloaded.
/// </para>
/// <para>
/// info
/// You might see a 502 bad gateway error in the response when using the Kibana Console. This error usually just reflects a timeout, while the model downloads in the background. You can check the download progress in the Machine Learning UI. If using the Python client, you can set the timeout parameter to a higher value.
/// </para>
/// <para>
/// After creating the endpoint, wait for the model deployment to complete before using it.
/// To verify the deployment status, use the get trained model statistics API.
/// Look for <c>"state": "fully_allocated"</c> in the response and ensure that the <c>"allocation_count"</c> matches the <c>"target_allocation_count"</c>.
/// Avoid creating multiple endpoints for the same model unless required, as each endpoint consumes significant resources.
/// </para>
/// </summary>
[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Inference.PutElserRequestConverter))]
public sealed partial class PutElserRequest : Elastic.Clients.Elasticsearch.Requests.PlainRequest<Elastic.Clients.Elasticsearch.Inference.PutElserRequestParameters>
{
	[System.Obsolete("The request contains additional required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public PutElserRequest(Elastic.Clients.Elasticsearch.Inference.ElserTaskType taskType, Elastic.Clients.Elasticsearch.Id elserInferenceId) : base(r => r.Required("task_type", taskType).Required("elser_inference_id", elserInferenceId))
	{
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public PutElserRequest(Elastic.Clients.Elasticsearch.Inference.ElserTaskType taskType, Elastic.Clients.Elasticsearch.Id elserInferenceId, Elastic.Clients.Elasticsearch.Inference.ElserServiceType service, Elastic.Clients.Elasticsearch.Inference.ElserServiceSettings serviceSettings) : base(r => r.Required("task_type", taskType).Required("elser_inference_id", elserInferenceId))
	{
		Service = service;
		ServiceSettings = serviceSettings;
	}
#if NET7_0_OR_GREATER
	public PutElserRequest()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal PutElserRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	internal override Elastic.Clients.Elasticsearch.Requests.ApiUrls ApiUrls => Elastic.Clients.Elasticsearch.Requests.ApiUrlLookup.InferencePutElser;

	protected override Elastic.Transport.HttpMethod StaticHttpMethod => Elastic.Transport.HttpMethod.PUT;

	internal override bool SupportsBody => true;

	internal override string OperationName => "inference.put_elser";

	/// <summary>
	/// <para>
	/// The unique identifier of the inference endpoint.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.Id ElserInferenceId { get => P<Elastic.Clients.Elasticsearch.Id>("elser_inference_id"); set => PR("elser_inference_id", value); }

	/// <summary>
	/// <para>
	/// The type of the inference task that the model will perform.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.Inference.ElserTaskType TaskType { get => P<Elastic.Clients.Elasticsearch.Inference.ElserTaskType>("task_type"); set => PR("task_type", value); }

	/// <summary>
	/// <para>
	/// The chunking configuration object.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Inference.InferenceChunkingSettings? ChunkingSettings { get; set; }

	/// <summary>
	/// <para>
	/// The type of service supported for the specified task type. In this case, <c>elser</c>.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.Inference.ElserServiceType Service { get; set; }

	/// <summary>
	/// <para>
	/// Settings used to install the inference model. These settings are specific to the <c>elser</c> service.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.Inference.ElserServiceSettings ServiceSettings { get; set; }
}

/// <summary>
/// <para>
/// Create an ELSER inference endpoint.
/// </para>
/// <para>
/// Create an inference endpoint to perform an inference task with the <c>elser</c> service.
/// You can also deploy ELSER by using the Elasticsearch inference integration.
/// </para>
/// <para>
/// info
/// Your Elasticsearch deployment contains a preconfigured ELSER inference endpoint, you only need to create the enpoint using the API if you want to customize the settings.
/// </para>
/// <para>
/// The API request will automatically download and deploy the ELSER model if it isn't already downloaded.
/// </para>
/// <para>
/// info
/// You might see a 502 bad gateway error in the response when using the Kibana Console. This error usually just reflects a timeout, while the model downloads in the background. You can check the download progress in the Machine Learning UI. If using the Python client, you can set the timeout parameter to a higher value.
/// </para>
/// <para>
/// After creating the endpoint, wait for the model deployment to complete before using it.
/// To verify the deployment status, use the get trained model statistics API.
/// Look for <c>"state": "fully_allocated"</c> in the response and ensure that the <c>"allocation_count"</c> matches the <c>"target_allocation_count"</c>.
/// Avoid creating multiple endpoints for the same model unless required, as each endpoint consumes significant resources.
/// </para>
/// </summary>
public readonly partial struct PutElserRequestDescriptor
{
	internal Elastic.Clients.Elasticsearch.Inference.PutElserRequest Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public PutElserRequestDescriptor(Elastic.Clients.Elasticsearch.Inference.PutElserRequest instance)
	{
		Instance = instance;
	}

	public PutElserRequestDescriptor(Elastic.Clients.Elasticsearch.Inference.ElserTaskType taskType, Elastic.Clients.Elasticsearch.Id elserInferenceId)
	{
#pragma warning disable CS0618
		Instance = new Elastic.Clients.Elasticsearch.Inference.PutElserRequest(taskType, elserInferenceId);
#pragma warning restore CS0618
	}

	[System.Obsolete("TODO")]
	public PutElserRequestDescriptor()
	{
		throw new System.InvalidOperationException("The use of the parameterless constructor is not permitted for this type.");
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Inference.PutElserRequestDescriptor(Elastic.Clients.Elasticsearch.Inference.PutElserRequest instance) => new Elastic.Clients.Elasticsearch.Inference.PutElserRequestDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Inference.PutElserRequest(Elastic.Clients.Elasticsearch.Inference.PutElserRequestDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// The unique identifier of the inference endpoint.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Inference.PutElserRequestDescriptor ElserInferenceId(Elastic.Clients.Elasticsearch.Id value)
	{
		Instance.ElserInferenceId = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The type of the inference task that the model will perform.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Inference.PutElserRequestDescriptor TaskType(Elastic.Clients.Elasticsearch.Inference.ElserTaskType value)
	{
		Instance.TaskType = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The chunking configuration object.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Inference.PutElserRequestDescriptor ChunkingSettings(Elastic.Clients.Elasticsearch.Inference.InferenceChunkingSettings? value)
	{
		Instance.ChunkingSettings = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The chunking configuration object.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Inference.PutElserRequestDescriptor ChunkingSettings()
	{
		Instance.ChunkingSettings = Elastic.Clients.Elasticsearch.Inference.InferenceChunkingSettingsDescriptor.Build(null);
		return this;
	}

	/// <summary>
	/// <para>
	/// The chunking configuration object.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Inference.PutElserRequestDescriptor ChunkingSettings(System.Action<Elastic.Clients.Elasticsearch.Inference.InferenceChunkingSettingsDescriptor>? action)
	{
		Instance.ChunkingSettings = Elastic.Clients.Elasticsearch.Inference.InferenceChunkingSettingsDescriptor.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// The type of service supported for the specified task type. In this case, <c>elser</c>.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Inference.PutElserRequestDescriptor Service(Elastic.Clients.Elasticsearch.Inference.ElserServiceType value)
	{
		Instance.Service = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Settings used to install the inference model. These settings are specific to the <c>elser</c> service.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Inference.PutElserRequestDescriptor ServiceSettings(Elastic.Clients.Elasticsearch.Inference.ElserServiceSettings value)
	{
		Instance.ServiceSettings = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Settings used to install the inference model. These settings are specific to the <c>elser</c> service.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Inference.PutElserRequestDescriptor ServiceSettings(System.Action<Elastic.Clients.Elasticsearch.Inference.ElserServiceSettingsDescriptor> action)
	{
		Instance.ServiceSettings = Elastic.Clients.Elasticsearch.Inference.ElserServiceSettingsDescriptor.Build(action);
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Inference.PutElserRequest Build(System.Action<Elastic.Clients.Elasticsearch.Inference.PutElserRequestDescriptor> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.Inference.PutElserRequestDescriptor(new Elastic.Clients.Elasticsearch.Inference.PutElserRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}

	public Elastic.Clients.Elasticsearch.Inference.PutElserRequestDescriptor ErrorTrace(bool? value)
	{
		Instance.ErrorTrace = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Inference.PutElserRequestDescriptor FilterPath(params string[]? value)
	{
		Instance.FilterPath = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Inference.PutElserRequestDescriptor Human(bool? value)
	{
		Instance.Human = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Inference.PutElserRequestDescriptor Pretty(bool? value)
	{
		Instance.Pretty = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Inference.PutElserRequestDescriptor SourceQueryString(string? value)
	{
		Instance.SourceQueryString = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Inference.PutElserRequestDescriptor RequestConfiguration(Elastic.Transport.IRequestConfiguration? value)
	{
		Instance.RequestConfiguration = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Inference.PutElserRequestDescriptor RequestConfiguration(System.Func<Elastic.Transport.RequestConfigurationDescriptor, Elastic.Transport.IRequestConfiguration>? configurationSelector)
	{
		Instance.RequestConfiguration = configurationSelector.Invoke(Instance.RequestConfiguration is null ? new Elastic.Transport.RequestConfigurationDescriptor() : new Elastic.Transport.RequestConfigurationDescriptor(Instance.RequestConfiguration)) ?? Instance.RequestConfiguration;
		return this;
	}
}