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

public sealed partial class DeleteTrainedModelAliasRequestParameters : RequestParameters
{
}

/// <summary>
/// <para>Delete a trained model alias.<br/>This API deletes an existing model alias that refers to a trained model. If<br/>the model alias is missing or refers to a model other than the one identified<br/>by the `model_id`, this API returns an error.</para>
/// </summary>
public sealed partial class DeleteTrainedModelAliasRequest : PlainRequest<DeleteTrainedModelAliasRequestParameters>
{
	public DeleteTrainedModelAliasRequest(Elastic.Clients.Elasticsearch.Serverless.Id modelId, Elastic.Clients.Elasticsearch.Serverless.Name modelAlias) : base(r => r.Required("model_id", modelId).Required("model_alias", modelAlias))
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.MachineLearningDeleteTrainedModelAlias;

	protected override HttpMethod StaticHttpMethod => HttpMethod.DELETE;

	internal override bool SupportsBody => false;

	internal override string OperationName => "ml.delete_trained_model_alias";
}

/// <summary>
/// <para>Delete a trained model alias.<br/>This API deletes an existing model alias that refers to a trained model. If<br/>the model alias is missing or refers to a model other than the one identified<br/>by the `model_id`, this API returns an error.</para>
/// </summary>
public sealed partial class DeleteTrainedModelAliasRequestDescriptor : RequestDescriptor<DeleteTrainedModelAliasRequestDescriptor, DeleteTrainedModelAliasRequestParameters>
{
	internal DeleteTrainedModelAliasRequestDescriptor(Action<DeleteTrainedModelAliasRequestDescriptor> configure) => configure.Invoke(this);

	public DeleteTrainedModelAliasRequestDescriptor(Elastic.Clients.Elasticsearch.Serverless.Id modelId, Elastic.Clients.Elasticsearch.Serverless.Name modelAlias) : base(r => r.Required("model_id", modelId).Required("model_alias", modelAlias))
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.MachineLearningDeleteTrainedModelAlias;

	protected override HttpMethod StaticHttpMethod => HttpMethod.DELETE;

	internal override bool SupportsBody => false;

	internal override string OperationName => "ml.delete_trained_model_alias";

	public DeleteTrainedModelAliasRequestDescriptor ModelAlias(Elastic.Clients.Elasticsearch.Serverless.Name modelAlias)
	{
		RouteValues.Required("model_alias", modelAlias);
		return Self;
	}

	public DeleteTrainedModelAliasRequestDescriptor ModelId(Elastic.Clients.Elasticsearch.Serverless.Id modelId)
	{
		RouteValues.Required("model_id", modelId);
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
	}
}