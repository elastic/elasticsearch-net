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

public sealed partial class GetFiltersRequestParameters : RequestParameters
{
	/// <summary>
	/// <para>
	/// Skips the specified number of filters.
	/// </para>
	/// </summary>
	public int? From { get => Q<int?>("from"); set => Q("from", value); }

	/// <summary>
	/// <para>
	/// Specifies the maximum number of filters to obtain.
	/// </para>
	/// </summary>
	public int? Size { get => Q<int?>("size"); set => Q("size", value); }
}

/// <summary>
/// <para>
/// Get filters.
/// You can get a single filter or all filters.
/// </para>
/// </summary>
public sealed partial class GetFiltersRequest : PlainRequest<GetFiltersRequestParameters>
{
	public GetFiltersRequest()
	{
	}

	public GetFiltersRequest(Elastic.Clients.Elasticsearch.Serverless.Ids? filterId) : base(r => r.Optional("filter_id", filterId))
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.MachineLearningGetFilters;

	protected override HttpMethod StaticHttpMethod => HttpMethod.GET;

	internal override bool SupportsBody => false;

	internal override string OperationName => "ml.get_filters";

	/// <summary>
	/// <para>
	/// Skips the specified number of filters.
	/// </para>
	/// </summary>
	[JsonIgnore]
	public int? From { get => Q<int?>("from"); set => Q("from", value); }

	/// <summary>
	/// <para>
	/// Specifies the maximum number of filters to obtain.
	/// </para>
	/// </summary>
	[JsonIgnore]
	public int? Size { get => Q<int?>("size"); set => Q("size", value); }
}

/// <summary>
/// <para>
/// Get filters.
/// You can get a single filter or all filters.
/// </para>
/// </summary>
public sealed partial class GetFiltersRequestDescriptor : RequestDescriptor<GetFiltersRequestDescriptor, GetFiltersRequestParameters>
{
	internal GetFiltersRequestDescriptor(Action<GetFiltersRequestDescriptor> configure) => configure.Invoke(this);

	public GetFiltersRequestDescriptor(Elastic.Clients.Elasticsearch.Serverless.Ids? filterId) : base(r => r.Optional("filter_id", filterId))
	{
	}

	public GetFiltersRequestDescriptor()
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.MachineLearningGetFilters;

	protected override HttpMethod StaticHttpMethod => HttpMethod.GET;

	internal override bool SupportsBody => false;

	internal override string OperationName => "ml.get_filters";

	public GetFiltersRequestDescriptor From(int? from) => Qs("from", from);
	public GetFiltersRequestDescriptor Size(int? size) => Qs("size", size);

	public GetFiltersRequestDescriptor FilterId(Elastic.Clients.Elasticsearch.Serverless.Ids? filterId)
	{
		RouteValues.Optional("filter_id", filterId);
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
	}
}