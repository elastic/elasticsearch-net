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

public sealed partial class PutWatsonxRequestParameters : Elastic.Transport.RequestParameters
{
}

internal sealed partial class PutWatsonxRequestConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Inference.PutWatsonxRequest>
{
	private static readonly System.Text.Json.JsonEncodedText PropService = System.Text.Json.JsonEncodedText.Encode("service");
	private static readonly System.Text.Json.JsonEncodedText PropServiceSettings = System.Text.Json.JsonEncodedText.Encode("service_settings");

	public override Elastic.Clients.Elasticsearch.Inference.PutWatsonxRequest Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<Elastic.Clients.Elasticsearch.Inference.WatsonxServiceSettings> propServiceSettings = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (reader.ValueTextEquals(PropService))
			{
				reader.Skip();
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
		return new Elastic.Clients.Elasticsearch.Inference.PutWatsonxRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			ServiceSettings = propServiceSettings.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Inference.PutWatsonxRequest value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropService, value.Service, null, null);
		writer.WriteProperty(options, PropServiceSettings, value.ServiceSettings, null, null);
		writer.WriteEndObject();
	}
}

/// <summary>
/// <para>
/// Create a Watsonx inference endpoint.
/// </para>
/// <para>
/// Create an inference endpoint to perform an inference task with the <c>watsonxai</c> service.
/// You need an IBM Cloud Databases for Elasticsearch deployment to use the <c>watsonxai</c> inference service.
/// You can provision one through the IBM catalog, the Cloud Databases CLI plug-in, the Cloud Databases API, or Terraform.
/// </para>
/// <para>
/// When you create an inference endpoint, the associated machine learning model is automatically deployed if it is not already running.
/// After creating the endpoint, wait for the model deployment to complete before using it.
/// To verify the deployment status, use the get trained model statistics API.
/// Look for <c>"state": "fully_allocated"</c> in the response and ensure that the <c>"allocation_count"</c> matches the <c>"target_allocation_count"</c>.
/// Avoid creating multiple endpoints for the same model unless required, as each endpoint consumes significant resources.
/// </para>
/// </summary>
[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Inference.PutWatsonxRequestConverter))]
public sealed partial class PutWatsonxRequest : Elastic.Clients.Elasticsearch.Requests.PlainRequest<Elastic.Clients.Elasticsearch.Inference.PutWatsonxRequestParameters>
{
	[System.Obsolete("The request contains additional required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public PutWatsonxRequest(Elastic.Clients.Elasticsearch.Inference.WatsonxTaskType taskType, Elastic.Clients.Elasticsearch.Id watsonxInferenceId) : base(r => r.Required("task_type", taskType).Required("watsonx_inference_id", watsonxInferenceId))
	{
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public PutWatsonxRequest(Elastic.Clients.Elasticsearch.Inference.WatsonxTaskType taskType, Elastic.Clients.Elasticsearch.Id watsonxInferenceId, Elastic.Clients.Elasticsearch.Inference.WatsonxServiceSettings serviceSettings) : base(r => r.Required("task_type", taskType).Required("watsonx_inference_id", watsonxInferenceId))
	{
		ServiceSettings = serviceSettings;
	}
#if NET7_0_OR_GREATER
	public PutWatsonxRequest()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal PutWatsonxRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	internal override Elastic.Clients.Elasticsearch.Requests.ApiUrls ApiUrls => Elastic.Clients.Elasticsearch.Requests.ApiUrlLookup.InferencePutWatsonx;

	protected override Elastic.Transport.HttpMethod StaticHttpMethod => Elastic.Transport.HttpMethod.PUT;

	internal override bool SupportsBody => true;

	internal override string OperationName => "inference.put_watsonx";

	/// <summary>
	/// <para>
	/// The task type.
	/// The only valid task type for the model to perform is <c>text_embedding</c>.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.Inference.WatsonxTaskType TaskType { get => P<Elastic.Clients.Elasticsearch.Inference.WatsonxTaskType>("task_type"); set => PR("task_type", value); }

	/// <summary>
	/// <para>
	/// The unique identifier of the inference endpoint.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.Id WatsonxInferenceId { get => P<Elastic.Clients.Elasticsearch.Id>("watsonx_inference_id"); set => PR("watsonx_inference_id", value); }

	/// <summary>
	/// <para>
	/// The type of service supported for the specified task type. In this case, <c>watsonxai</c>.
	/// </para>
	/// </summary>
	public string Service => "watsonxai";

	/// <summary>
	/// <para>
	/// Settings used to install the inference model. These settings are specific to the <c>watsonxai</c> service.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.Inference.WatsonxServiceSettings ServiceSettings { get; set; }
}

/// <summary>
/// <para>
/// Create a Watsonx inference endpoint.
/// </para>
/// <para>
/// Create an inference endpoint to perform an inference task with the <c>watsonxai</c> service.
/// You need an IBM Cloud Databases for Elasticsearch deployment to use the <c>watsonxai</c> inference service.
/// You can provision one through the IBM catalog, the Cloud Databases CLI plug-in, the Cloud Databases API, or Terraform.
/// </para>
/// <para>
/// When you create an inference endpoint, the associated machine learning model is automatically deployed if it is not already running.
/// After creating the endpoint, wait for the model deployment to complete before using it.
/// To verify the deployment status, use the get trained model statistics API.
/// Look for <c>"state": "fully_allocated"</c> in the response and ensure that the <c>"allocation_count"</c> matches the <c>"target_allocation_count"</c>.
/// Avoid creating multiple endpoints for the same model unless required, as each endpoint consumes significant resources.
/// </para>
/// </summary>
public readonly partial struct PutWatsonxRequestDescriptor
{
	internal Elastic.Clients.Elasticsearch.Inference.PutWatsonxRequest Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public PutWatsonxRequestDescriptor(Elastic.Clients.Elasticsearch.Inference.PutWatsonxRequest instance)
	{
		Instance = instance;
	}

	public PutWatsonxRequestDescriptor(Elastic.Clients.Elasticsearch.Inference.WatsonxTaskType taskType, Elastic.Clients.Elasticsearch.Id watsonxInferenceId)
	{
#pragma warning disable CS0618
		Instance = new Elastic.Clients.Elasticsearch.Inference.PutWatsonxRequest(taskType, watsonxInferenceId);
#pragma warning restore CS0618
	}

	[System.Obsolete("The use of the parameterless constructor is not permitted for this type.")]
	public PutWatsonxRequestDescriptor()
	{
		throw new System.InvalidOperationException("The use of the parameterless constructor is not permitted for this type.");
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Inference.PutWatsonxRequestDescriptor(Elastic.Clients.Elasticsearch.Inference.PutWatsonxRequest instance) => new Elastic.Clients.Elasticsearch.Inference.PutWatsonxRequestDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Inference.PutWatsonxRequest(Elastic.Clients.Elasticsearch.Inference.PutWatsonxRequestDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// The task type.
	/// The only valid task type for the model to perform is <c>text_embedding</c>.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Inference.PutWatsonxRequestDescriptor TaskType(Elastic.Clients.Elasticsearch.Inference.WatsonxTaskType value)
	{
		Instance.TaskType = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The unique identifier of the inference endpoint.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Inference.PutWatsonxRequestDescriptor WatsonxInferenceId(Elastic.Clients.Elasticsearch.Id value)
	{
		Instance.WatsonxInferenceId = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Settings used to install the inference model. These settings are specific to the <c>watsonxai</c> service.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Inference.PutWatsonxRequestDescriptor ServiceSettings(Elastic.Clients.Elasticsearch.Inference.WatsonxServiceSettings value)
	{
		Instance.ServiceSettings = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Settings used to install the inference model. These settings are specific to the <c>watsonxai</c> service.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Inference.PutWatsonxRequestDescriptor ServiceSettings(System.Action<Elastic.Clients.Elasticsearch.Inference.WatsonxServiceSettingsDescriptor> action)
	{
		Instance.ServiceSettings = Elastic.Clients.Elasticsearch.Inference.WatsonxServiceSettingsDescriptor.Build(action);
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Inference.PutWatsonxRequest Build(System.Action<Elastic.Clients.Elasticsearch.Inference.PutWatsonxRequestDescriptor> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.Inference.PutWatsonxRequestDescriptor(new Elastic.Clients.Elasticsearch.Inference.PutWatsonxRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}

	public Elastic.Clients.Elasticsearch.Inference.PutWatsonxRequestDescriptor ErrorTrace(bool? value)
	{
		Instance.ErrorTrace = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Inference.PutWatsonxRequestDescriptor FilterPath(params string[]? value)
	{
		Instance.FilterPath = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Inference.PutWatsonxRequestDescriptor Human(bool? value)
	{
		Instance.Human = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Inference.PutWatsonxRequestDescriptor Pretty(bool? value)
	{
		Instance.Pretty = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Inference.PutWatsonxRequestDescriptor SourceQueryString(string? value)
	{
		Instance.SourceQueryString = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Inference.PutWatsonxRequestDescriptor RequestConfiguration(Elastic.Transport.IRequestConfiguration? value)
	{
		Instance.RequestConfiguration = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Inference.PutWatsonxRequestDescriptor RequestConfiguration(System.Func<Elastic.Transport.RequestConfigurationDescriptor, Elastic.Transport.IRequestConfiguration>? configurationSelector)
	{
		Instance.RequestConfiguration = configurationSelector.Invoke(Instance.RequestConfiguration is null ? new Elastic.Transport.RequestConfigurationDescriptor() : new Elastic.Transport.RequestConfigurationDescriptor(Instance.RequestConfiguration)) ?? Instance.RequestConfiguration;
		return this;
	}
}