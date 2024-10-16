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
using Elastic.Transport.Extensions;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch.Serverless.MachineLearning;

public sealed partial class GetTrainedModelsStatsRequestParameters : RequestParameters
{
	/// <summary>
	/// <para>
	/// Specifies what to do when the request:
	/// </para>
	/// <list type="bullet">
	/// <item>
	/// <para>
	/// Contains wildcard expressions and there are no models that match.
	/// </para>
	/// </item>
	/// <item>
	/// <para>
	/// Contains the _all string or no identifiers and there are no matches.
	/// </para>
	/// </item>
	/// <item>
	/// <para>
	/// Contains wildcard expressions and there are only partial matches.
	/// </para>
	/// </item>
	/// </list>
	/// <para>
	/// If true, it returns an empty array when there are no matches and the
	/// subset of results when there are partial matches.
	/// </para>
	/// </summary>
	public bool? AllowNoMatch { get => Q<bool?>("allow_no_match"); set => Q("allow_no_match", value); }

	/// <summary>
	/// <para>
	/// Skips the specified number of models.
	/// </para>
	/// </summary>
	public int? From { get => Q<int?>("from"); set => Q("from", value); }

	/// <summary>
	/// <para>
	/// Specifies the maximum number of models to obtain.
	/// </para>
	/// </summary>
	public int? Size { get => Q<int?>("size"); set => Q("size", value); }
}

/// <summary>
/// <para>
/// Get trained models usage info.
/// You can get usage information for multiple trained
/// models in a single API request by using a comma-separated list of model IDs or a wildcard expression.
/// </para>
/// </summary>
public sealed partial class GetTrainedModelsStatsRequest : PlainRequest<GetTrainedModelsStatsRequestParameters>
{
	public GetTrainedModelsStatsRequest()
	{
	}

	public GetTrainedModelsStatsRequest(Elastic.Clients.Elasticsearch.Serverless.Ids? modelId) : base(r => r.Optional("model_id", modelId))
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.MachineLearningGetTrainedModelsStats;

	protected override HttpMethod StaticHttpMethod => HttpMethod.GET;

	internal override bool SupportsBody => false;

	internal override string OperationName => "ml.get_trained_models_stats";

	/// <summary>
	/// <para>
	/// Specifies what to do when the request:
	/// </para>
	/// <list type="bullet">
	/// <item>
	/// <para>
	/// Contains wildcard expressions and there are no models that match.
	/// </para>
	/// </item>
	/// <item>
	/// <para>
	/// Contains the _all string or no identifiers and there are no matches.
	/// </para>
	/// </item>
	/// <item>
	/// <para>
	/// Contains wildcard expressions and there are only partial matches.
	/// </para>
	/// </item>
	/// </list>
	/// <para>
	/// If true, it returns an empty array when there are no matches and the
	/// subset of results when there are partial matches.
	/// </para>
	/// </summary>
	[JsonIgnore]
	public bool? AllowNoMatch { get => Q<bool?>("allow_no_match"); set => Q("allow_no_match", value); }

	/// <summary>
	/// <para>
	/// Skips the specified number of models.
	/// </para>
	/// </summary>
	[JsonIgnore]
	public int? From { get => Q<int?>("from"); set => Q("from", value); }

	/// <summary>
	/// <para>
	/// Specifies the maximum number of models to obtain.
	/// </para>
	/// </summary>
	[JsonIgnore]
	public int? Size { get => Q<int?>("size"); set => Q("size", value); }
}

/// <summary>
/// <para>
/// Get trained models usage info.
/// You can get usage information for multiple trained
/// models in a single API request by using a comma-separated list of model IDs or a wildcard expression.
/// </para>
/// </summary>
public sealed partial class GetTrainedModelsStatsRequestDescriptor : RequestDescriptor<GetTrainedModelsStatsRequestDescriptor, GetTrainedModelsStatsRequestParameters>
{
	internal GetTrainedModelsStatsRequestDescriptor(Action<GetTrainedModelsStatsRequestDescriptor> configure) => configure.Invoke(this);

	public GetTrainedModelsStatsRequestDescriptor(Elastic.Clients.Elasticsearch.Serverless.Ids? modelId) : base(r => r.Optional("model_id", modelId))
	{
	}

	public GetTrainedModelsStatsRequestDescriptor()
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.MachineLearningGetTrainedModelsStats;

	protected override HttpMethod StaticHttpMethod => HttpMethod.GET;

	internal override bool SupportsBody => false;

	internal override string OperationName => "ml.get_trained_models_stats";

	public GetTrainedModelsStatsRequestDescriptor AllowNoMatch(bool? allowNoMatch = true) => Qs("allow_no_match", allowNoMatch);
	public GetTrainedModelsStatsRequestDescriptor From(int? from) => Qs("from", from);
	public GetTrainedModelsStatsRequestDescriptor Size(int? size) => Qs("size", size);

	public GetTrainedModelsStatsRequestDescriptor ModelId(Elastic.Clients.Elasticsearch.Serverless.Ids? modelId)
	{
		RouteValues.Optional("model_id", modelId);
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
	}
}