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

namespace Elastic.Clients.Elasticsearch.IndexManagement;

public sealed partial class DownsampleRequestParameters : RequestParameters
{
}

/// <summary>
/// <para>
/// Aggregates a time series (TSDS) index and stores pre-computed statistical summaries (<c>min</c>, <c>max</c>, <c>sum</c>, <c>value_count</c> and <c>avg</c>) for each metric field grouped by a configured time interval.
/// </para>
/// </summary>
public sealed partial class DownsampleRequest : PlainRequest<DownsampleRequestParameters>, ISelfSerializable
{
	public DownsampleRequest(Elastic.Clients.Elasticsearch.IndexName index, Elastic.Clients.Elasticsearch.IndexName targetIndex) : base(r => r.Required("index", index).Required("target_index", targetIndex))
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.IndexManagementDownsample;

	protected override HttpMethod StaticHttpMethod => HttpMethod.POST;

	internal override bool SupportsBody => true;

	internal override string OperationName => "indices.downsample";

	[JsonIgnore]
	public Elastic.Clients.Elasticsearch.IndexManagement.DownsampleConfig Config { get; set; }

	void ISelfSerializable.Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		JsonSerializer.Serialize(writer, Config, options);
	}
}

/// <summary>
/// <para>
/// Aggregates a time series (TSDS) index and stores pre-computed statistical summaries (<c>min</c>, <c>max</c>, <c>sum</c>, <c>value_count</c> and <c>avg</c>) for each metric field grouped by a configured time interval.
/// </para>
/// </summary>
public sealed partial class DownsampleRequestDescriptor<TDocument> : RequestDescriptor<DownsampleRequestDescriptor<TDocument>, DownsampleRequestParameters>
{
	internal DownsampleRequestDescriptor(Action<DownsampleRequestDescriptor<TDocument>> configure) => configure.Invoke(this);
	public DownsampleRequestDescriptor(Elastic.Clients.Elasticsearch.IndexManagement.DownsampleConfig config, Elastic.Clients.Elasticsearch.IndexName index, Elastic.Clients.Elasticsearch.IndexName targetIndex) : base(r => r.Required("index", index).Required("target_index", targetIndex)) => ConfigValue = config;

	internal override ApiUrls ApiUrls => ApiUrlLookup.IndexManagementDownsample;

	protected override HttpMethod StaticHttpMethod => HttpMethod.POST;

	internal override bool SupportsBody => true;

	internal override string OperationName => "indices.downsample";

	public DownsampleRequestDescriptor<TDocument> Index(Elastic.Clients.Elasticsearch.IndexName index)
	{
		RouteValues.Required("index", index);
		return Self;
	}

	public DownsampleRequestDescriptor<TDocument> TargetIndex(Elastic.Clients.Elasticsearch.IndexName targetIndex)
	{
		RouteValues.Required("target_index", targetIndex);
		return Self;
	}

	private Elastic.Clients.Elasticsearch.IndexManagement.DownsampleConfig ConfigValue { get; set; }
	private Elastic.Clients.Elasticsearch.IndexManagement.DownsampleConfigDescriptor ConfigDescriptor { get; set; }
	private Action<Elastic.Clients.Elasticsearch.IndexManagement.DownsampleConfigDescriptor> ConfigDescriptorAction { get; set; }

	public DownsampleRequestDescriptor<TDocument> Config(Elastic.Clients.Elasticsearch.IndexManagement.DownsampleConfig config)
	{
		ConfigDescriptor = null;
		ConfigDescriptorAction = null;
		ConfigValue = config;
		return Self;
	}

	public DownsampleRequestDescriptor<TDocument> Config(Elastic.Clients.Elasticsearch.IndexManagement.DownsampleConfigDescriptor descriptor)
	{
		ConfigValue = null;
		ConfigDescriptorAction = null;
		ConfigDescriptor = descriptor;
		return Self;
	}

	public DownsampleRequestDescriptor<TDocument> Config(Action<Elastic.Clients.Elasticsearch.IndexManagement.DownsampleConfigDescriptor> configure)
	{
		ConfigValue = null;
		ConfigDescriptor = null;
		ConfigDescriptorAction = configure;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		JsonSerializer.Serialize(writer, ConfigValue, options);
	}
}

/// <summary>
/// <para>
/// Aggregates a time series (TSDS) index and stores pre-computed statistical summaries (<c>min</c>, <c>max</c>, <c>sum</c>, <c>value_count</c> and <c>avg</c>) for each metric field grouped by a configured time interval.
/// </para>
/// </summary>
public sealed partial class DownsampleRequestDescriptor : RequestDescriptor<DownsampleRequestDescriptor, DownsampleRequestParameters>
{
	internal DownsampleRequestDescriptor(Action<DownsampleRequestDescriptor> configure) => configure.Invoke(this);
	public DownsampleRequestDescriptor(Elastic.Clients.Elasticsearch.IndexManagement.DownsampleConfig config, Elastic.Clients.Elasticsearch.IndexName index, Elastic.Clients.Elasticsearch.IndexName targetIndex) : base(r => r.Required("index", index).Required("target_index", targetIndex)) => ConfigValue = config;

	internal override ApiUrls ApiUrls => ApiUrlLookup.IndexManagementDownsample;

	protected override HttpMethod StaticHttpMethod => HttpMethod.POST;

	internal override bool SupportsBody => true;

	internal override string OperationName => "indices.downsample";

	public DownsampleRequestDescriptor Index(Elastic.Clients.Elasticsearch.IndexName index)
	{
		RouteValues.Required("index", index);
		return Self;
	}

	public DownsampleRequestDescriptor TargetIndex(Elastic.Clients.Elasticsearch.IndexName targetIndex)
	{
		RouteValues.Required("target_index", targetIndex);
		return Self;
	}

	private Elastic.Clients.Elasticsearch.IndexManagement.DownsampleConfig ConfigValue { get; set; }
	private Elastic.Clients.Elasticsearch.IndexManagement.DownsampleConfigDescriptor ConfigDescriptor { get; set; }
	private Action<Elastic.Clients.Elasticsearch.IndexManagement.DownsampleConfigDescriptor> ConfigDescriptorAction { get; set; }

	public DownsampleRequestDescriptor Config(Elastic.Clients.Elasticsearch.IndexManagement.DownsampleConfig config)
	{
		ConfigDescriptor = null;
		ConfigDescriptorAction = null;
		ConfigValue = config;
		return Self;
	}

	public DownsampleRequestDescriptor Config(Elastic.Clients.Elasticsearch.IndexManagement.DownsampleConfigDescriptor descriptor)
	{
		ConfigValue = null;
		ConfigDescriptorAction = null;
		ConfigDescriptor = descriptor;
		return Self;
	}

	public DownsampleRequestDescriptor Config(Action<Elastic.Clients.Elasticsearch.IndexManagement.DownsampleConfigDescriptor> configure)
	{
		ConfigValue = null;
		ConfigDescriptor = null;
		ConfigDescriptorAction = configure;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		JsonSerializer.Serialize(writer, ConfigValue, options);
	}
}