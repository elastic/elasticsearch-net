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

using Elastic.Clients.Elasticsearch.Serverless.Fluent;
using Elastic.Clients.Elasticsearch.Serverless.Requests;
using Elastic.Clients.Elasticsearch.Serverless.Serialization;
using Elastic.Transport;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch.Serverless.MachineLearning;

public sealed partial class InferTrainedModelRequestParameters : RequestParameters
{
	/// <summary>
	/// <para>
	/// Controls the amount of time to wait for inference results.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Serverless.Duration? Timeout { get => Q<Elastic.Clients.Elasticsearch.Serverless.Duration?>("timeout"); set => Q("timeout", value); }
}

/// <summary>
/// <para>
/// Evaluate a trained model.
/// </para>
/// </summary>
public sealed partial class InferTrainedModelRequest : PlainRequest<InferTrainedModelRequestParameters>
{
	public InferTrainedModelRequest(Elastic.Clients.Elasticsearch.Serverless.Id modelId) : base(r => r.Required("model_id", modelId))
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.MachineLearningInferTrainedModel;

	protected override HttpMethod StaticHttpMethod => HttpMethod.POST;

	internal override bool SupportsBody => true;

	internal override string OperationName => "ml.infer_trained_model";

	/// <summary>
	/// <para>
	/// Controls the amount of time to wait for inference results.
	/// </para>
	/// </summary>
	[JsonIgnore]
	public Elastic.Clients.Elasticsearch.Serverless.Duration? Timeout { get => Q<Elastic.Clients.Elasticsearch.Serverless.Duration?>("timeout"); set => Q("timeout", value); }

	/// <summary>
	/// <para>
	/// An array of objects to pass to the model for inference. The objects should contain a fields matching your
	/// configured trained model input. Typically, for NLP models, the field name is <c>text_field</c>.
	/// Currently, for NLP models, only a single value is allowed.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("docs")]
	public ICollection<IDictionary<string, object>> Docs { get; set; }

	/// <summary>
	/// <para>
	/// The inference configuration updates to apply on the API call
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("inference_config")]
	public Elastic.Clients.Elasticsearch.Serverless.MachineLearning.InferenceConfigUpdate? InferenceConfig { get; set; }
}

/// <summary>
/// <para>
/// Evaluate a trained model.
/// </para>
/// </summary>
public sealed partial class InferTrainedModelRequestDescriptor<TDocument> : RequestDescriptor<InferTrainedModelRequestDescriptor<TDocument>, InferTrainedModelRequestParameters>
{
	internal InferTrainedModelRequestDescriptor(Action<InferTrainedModelRequestDescriptor<TDocument>> configure) => configure.Invoke(this);

	public InferTrainedModelRequestDescriptor(Elastic.Clients.Elasticsearch.Serverless.Id modelId) : base(r => r.Required("model_id", modelId))
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.MachineLearningInferTrainedModel;

	protected override HttpMethod StaticHttpMethod => HttpMethod.POST;

	internal override bool SupportsBody => true;

	internal override string OperationName => "ml.infer_trained_model";

	public InferTrainedModelRequestDescriptor<TDocument> Timeout(Elastic.Clients.Elasticsearch.Serverless.Duration? timeout) => Qs("timeout", timeout);

	public InferTrainedModelRequestDescriptor<TDocument> ModelId(Elastic.Clients.Elasticsearch.Serverless.Id modelId)
	{
		RouteValues.Required("model_id", modelId);
		return Self;
	}

	private ICollection<IDictionary<string, object>> DocsValue { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.MachineLearning.InferenceConfigUpdate? InferenceConfigValue { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.MachineLearning.InferenceConfigUpdateDescriptor<TDocument> InferenceConfigDescriptor { get; set; }
	private Action<Elastic.Clients.Elasticsearch.Serverless.MachineLearning.InferenceConfigUpdateDescriptor<TDocument>> InferenceConfigDescriptorAction { get; set; }

	/// <summary>
	/// <para>
	/// An array of objects to pass to the model for inference. The objects should contain a fields matching your
	/// configured trained model input. Typically, for NLP models, the field name is <c>text_field</c>.
	/// Currently, for NLP models, only a single value is allowed.
	/// </para>
	/// </summary>
	public InferTrainedModelRequestDescriptor<TDocument> Docs(ICollection<IDictionary<string, object>> docs)
	{
		DocsValue = docs;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The inference configuration updates to apply on the API call
	/// </para>
	/// </summary>
	public InferTrainedModelRequestDescriptor<TDocument> InferenceConfig(Elastic.Clients.Elasticsearch.Serverless.MachineLearning.InferenceConfigUpdate? inferenceConfig)
	{
		InferenceConfigDescriptor = null;
		InferenceConfigDescriptorAction = null;
		InferenceConfigValue = inferenceConfig;
		return Self;
	}

	public InferTrainedModelRequestDescriptor<TDocument> InferenceConfig(Elastic.Clients.Elasticsearch.Serverless.MachineLearning.InferenceConfigUpdateDescriptor<TDocument> descriptor)
	{
		InferenceConfigValue = null;
		InferenceConfigDescriptorAction = null;
		InferenceConfigDescriptor = descriptor;
		return Self;
	}

	public InferTrainedModelRequestDescriptor<TDocument> InferenceConfig(Action<Elastic.Clients.Elasticsearch.Serverless.MachineLearning.InferenceConfigUpdateDescriptor<TDocument>> configure)
	{
		InferenceConfigValue = null;
		InferenceConfigDescriptor = null;
		InferenceConfigDescriptorAction = configure;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		writer.WritePropertyName("docs");
		JsonSerializer.Serialize(writer, DocsValue, options);
		if (InferenceConfigDescriptor is not null)
		{
			writer.WritePropertyName("inference_config");
			JsonSerializer.Serialize(writer, InferenceConfigDescriptor, options);
		}
		else if (InferenceConfigDescriptorAction is not null)
		{
			writer.WritePropertyName("inference_config");
			JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.Serverless.MachineLearning.InferenceConfigUpdateDescriptor<TDocument>(InferenceConfigDescriptorAction), options);
		}
		else if (InferenceConfigValue is not null)
		{
			writer.WritePropertyName("inference_config");
			JsonSerializer.Serialize(writer, InferenceConfigValue, options);
		}

		writer.WriteEndObject();
	}
}

/// <summary>
/// <para>
/// Evaluate a trained model.
/// </para>
/// </summary>
public sealed partial class InferTrainedModelRequestDescriptor : RequestDescriptor<InferTrainedModelRequestDescriptor, InferTrainedModelRequestParameters>
{
	internal InferTrainedModelRequestDescriptor(Action<InferTrainedModelRequestDescriptor> configure) => configure.Invoke(this);

	public InferTrainedModelRequestDescriptor(Elastic.Clients.Elasticsearch.Serverless.Id modelId) : base(r => r.Required("model_id", modelId))
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.MachineLearningInferTrainedModel;

	protected override HttpMethod StaticHttpMethod => HttpMethod.POST;

	internal override bool SupportsBody => true;

	internal override string OperationName => "ml.infer_trained_model";

	public InferTrainedModelRequestDescriptor Timeout(Elastic.Clients.Elasticsearch.Serverless.Duration? timeout) => Qs("timeout", timeout);

	public InferTrainedModelRequestDescriptor ModelId(Elastic.Clients.Elasticsearch.Serverless.Id modelId)
	{
		RouteValues.Required("model_id", modelId);
		return Self;
	}

	private ICollection<IDictionary<string, object>> DocsValue { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.MachineLearning.InferenceConfigUpdate? InferenceConfigValue { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.MachineLearning.InferenceConfigUpdateDescriptor InferenceConfigDescriptor { get; set; }
	private Action<Elastic.Clients.Elasticsearch.Serverless.MachineLearning.InferenceConfigUpdateDescriptor> InferenceConfigDescriptorAction { get; set; }

	/// <summary>
	/// <para>
	/// An array of objects to pass to the model for inference. The objects should contain a fields matching your
	/// configured trained model input. Typically, for NLP models, the field name is <c>text_field</c>.
	/// Currently, for NLP models, only a single value is allowed.
	/// </para>
	/// </summary>
	public InferTrainedModelRequestDescriptor Docs(ICollection<IDictionary<string, object>> docs)
	{
		DocsValue = docs;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The inference configuration updates to apply on the API call
	/// </para>
	/// </summary>
	public InferTrainedModelRequestDescriptor InferenceConfig(Elastic.Clients.Elasticsearch.Serverless.MachineLearning.InferenceConfigUpdate? inferenceConfig)
	{
		InferenceConfigDescriptor = null;
		InferenceConfigDescriptorAction = null;
		InferenceConfigValue = inferenceConfig;
		return Self;
	}

	public InferTrainedModelRequestDescriptor InferenceConfig(Elastic.Clients.Elasticsearch.Serverless.MachineLearning.InferenceConfigUpdateDescriptor descriptor)
	{
		InferenceConfigValue = null;
		InferenceConfigDescriptorAction = null;
		InferenceConfigDescriptor = descriptor;
		return Self;
	}

	public InferTrainedModelRequestDescriptor InferenceConfig(Action<Elastic.Clients.Elasticsearch.Serverless.MachineLearning.InferenceConfigUpdateDescriptor> configure)
	{
		InferenceConfigValue = null;
		InferenceConfigDescriptor = null;
		InferenceConfigDescriptorAction = configure;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		writer.WritePropertyName("docs");
		JsonSerializer.Serialize(writer, DocsValue, options);
		if (InferenceConfigDescriptor is not null)
		{
			writer.WritePropertyName("inference_config");
			JsonSerializer.Serialize(writer, InferenceConfigDescriptor, options);
		}
		else if (InferenceConfigDescriptorAction is not null)
		{
			writer.WritePropertyName("inference_config");
			JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.Serverless.MachineLearning.InferenceConfigUpdateDescriptor(InferenceConfigDescriptorAction), options);
		}
		else if (InferenceConfigValue is not null)
		{
			writer.WritePropertyName("inference_config");
			JsonSerializer.Serialize(writer, InferenceConfigValue, options);
		}

		writer.WriteEndObject();
	}
}