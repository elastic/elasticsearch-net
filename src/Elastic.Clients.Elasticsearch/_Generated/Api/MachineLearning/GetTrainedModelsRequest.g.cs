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

using Elastic.Clients.Elasticsearch.Fluent;
using Elastic.Clients.Elasticsearch.Requests;
using Elastic.Clients.Elasticsearch.Serialization;
using Elastic.Transport;
using Elastic.Transport.Extensions;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch.MachineLearning;

public sealed partial class GetTrainedModelsRequestParameters : RequestParameters
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
	/// Specifies whether the included model definition should be returned as a
	/// JSON map (true) or in a custom compressed format (false).
	/// </para>
	/// </summary>
	public bool? DecompressDefinition { get => Q<bool?>("decompress_definition"); set => Q("decompress_definition", value); }

	/// <summary>
	/// <para>
	/// Indicates if certain fields should be removed from the configuration on
	/// retrieval. This allows the configuration to be in an acceptable format to
	/// be retrieved and then added to another cluster.
	/// </para>
	/// </summary>
	public bool? ExcludeGenerated { get => Q<bool?>("exclude_generated"); set => Q("exclude_generated", value); }

	/// <summary>
	/// <para>
	/// Skips the specified number of models.
	/// </para>
	/// </summary>
	public int? From { get => Q<int?>("from"); set => Q("from", value); }

	/// <summary>
	/// <para>
	/// A comma delimited string of optional fields to include in the response
	/// body.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.Include? Include { get => Q<Elastic.Clients.Elasticsearch.MachineLearning.Include?>("include"); set => Q("include", value); }

	/// <summary>
	/// <para>
	/// Specifies the maximum number of models to obtain.
	/// </para>
	/// </summary>
	public int? Size { get => Q<int?>("size"); set => Q("size", value); }

	/// <summary>
	/// <para>
	/// A comma delimited string of tags. A trained model can have many tags, or
	/// none. When supplied, only trained models that contain all the supplied
	/// tags are returned.
	/// </para>
	/// </summary>
	public ICollection<string>? Tags { get => Q<ICollection<string>?>("tags"); set => Q("tags", value); }
}

/// <summary>
/// <para>
/// Get trained model configuration info.
/// </para>
/// </summary>
public sealed partial class GetTrainedModelsRequest : PlainRequest<GetTrainedModelsRequestParameters>
{
	public GetTrainedModelsRequest()
	{
	}

	public GetTrainedModelsRequest(Elastic.Clients.Elasticsearch.Ids? modelId) : base(r => r.Optional("model_id", modelId))
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.MachineLearningGetTrainedModels;

	protected override HttpMethod StaticHttpMethod => HttpMethod.GET;

	internal override bool SupportsBody => false;

	internal override string OperationName => "ml.get_trained_models";

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
	/// Specifies whether the included model definition should be returned as a
	/// JSON map (true) or in a custom compressed format (false).
	/// </para>
	/// </summary>
	[JsonIgnore]
	public bool? DecompressDefinition { get => Q<bool?>("decompress_definition"); set => Q("decompress_definition", value); }

	/// <summary>
	/// <para>
	/// Indicates if certain fields should be removed from the configuration on
	/// retrieval. This allows the configuration to be in an acceptable format to
	/// be retrieved and then added to another cluster.
	/// </para>
	/// </summary>
	[JsonIgnore]
	public bool? ExcludeGenerated { get => Q<bool?>("exclude_generated"); set => Q("exclude_generated", value); }

	/// <summary>
	/// <para>
	/// Skips the specified number of models.
	/// </para>
	/// </summary>
	[JsonIgnore]
	public int? From { get => Q<int?>("from"); set => Q("from", value); }

	/// <summary>
	/// <para>
	/// A comma delimited string of optional fields to include in the response
	/// body.
	/// </para>
	/// </summary>
	[JsonIgnore]
	public Elastic.Clients.Elasticsearch.MachineLearning.Include? Include { get => Q<Elastic.Clients.Elasticsearch.MachineLearning.Include?>("include"); set => Q("include", value); }

	/// <summary>
	/// <para>
	/// Specifies the maximum number of models to obtain.
	/// </para>
	/// </summary>
	[JsonIgnore]
	public int? Size { get => Q<int?>("size"); set => Q("size", value); }

	/// <summary>
	/// <para>
	/// A comma delimited string of tags. A trained model can have many tags, or
	/// none. When supplied, only trained models that contain all the supplied
	/// tags are returned.
	/// </para>
	/// </summary>
	[JsonIgnore]
	public ICollection<string>? Tags { get => Q<ICollection<string>?>("tags"); set => Q("tags", value); }
}

/// <summary>
/// <para>
/// Get trained model configuration info.
/// </para>
/// </summary>
public sealed partial class GetTrainedModelsRequestDescriptor : RequestDescriptor<GetTrainedModelsRequestDescriptor, GetTrainedModelsRequestParameters>
{
	internal GetTrainedModelsRequestDescriptor(Action<GetTrainedModelsRequestDescriptor> configure) => configure.Invoke(this);

	public GetTrainedModelsRequestDescriptor(Elastic.Clients.Elasticsearch.Ids? modelId) : base(r => r.Optional("model_id", modelId))
	{
	}

	public GetTrainedModelsRequestDescriptor()
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.MachineLearningGetTrainedModels;

	protected override HttpMethod StaticHttpMethod => HttpMethod.GET;

	internal override bool SupportsBody => false;

	internal override string OperationName => "ml.get_trained_models";

	public GetTrainedModelsRequestDescriptor AllowNoMatch(bool? allowNoMatch = true) => Qs("allow_no_match", allowNoMatch);
	public GetTrainedModelsRequestDescriptor DecompressDefinition(bool? decompressDefinition = true) => Qs("decompress_definition", decompressDefinition);
	public GetTrainedModelsRequestDescriptor ExcludeGenerated(bool? excludeGenerated = true) => Qs("exclude_generated", excludeGenerated);
	public GetTrainedModelsRequestDescriptor From(int? from) => Qs("from", from);
	public GetTrainedModelsRequestDescriptor Include(Elastic.Clients.Elasticsearch.MachineLearning.Include? include) => Qs("include", include);
	public GetTrainedModelsRequestDescriptor Size(int? size) => Qs("size", size);
	public GetTrainedModelsRequestDescriptor Tags(ICollection<string>? tags) => Qs("tags", tags);

	public GetTrainedModelsRequestDescriptor ModelId(Elastic.Clients.Elasticsearch.Ids? modelId)
	{
		RouteValues.Optional("model_id", modelId);
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
	}
}