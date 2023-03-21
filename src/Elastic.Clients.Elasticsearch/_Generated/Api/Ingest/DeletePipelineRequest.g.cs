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
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch.Ingest;

public sealed class DeletePipelineRequestParameters : RequestParameters
{
	[JsonIgnore]
	public Elastic.Clients.Elasticsearch.Duration? MasterTimeout { get => Q<Elastic.Clients.Elasticsearch.Duration?>("master_timeout"); set => Q("master_timeout", value); }
	[JsonIgnore]
	public Elastic.Clients.Elasticsearch.Duration? Timeout { get => Q<Elastic.Clients.Elasticsearch.Duration?>("timeout"); set => Q("timeout", value); }
}

public sealed partial class DeletePipelineRequest : PlainRequest<DeletePipelineRequestParameters>
{
	public DeletePipelineRequest(Elastic.Clients.Elasticsearch.Id id) : base(r => r.Required("id", id))
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlsLookups.IngestDeletePipeline;

	protected override HttpMethod StaticHttpMethod => HttpMethod.DELETE;

	internal override bool SupportsBody => false;

	[JsonIgnore]
	public Elastic.Clients.Elasticsearch.Duration? MasterTimeout { get => Q<Elastic.Clients.Elasticsearch.Duration?>("master_timeout"); set => Q("master_timeout", value); }
	[JsonIgnore]
	public Elastic.Clients.Elasticsearch.Duration? Timeout { get => Q<Elastic.Clients.Elasticsearch.Duration?>("timeout"); set => Q("timeout", value); }
}

public sealed partial class DeletePipelineRequestDescriptor<TDocument> : RequestDescriptor<DeletePipelineRequestDescriptor<TDocument>, DeletePipelineRequestParameters>
{
	internal DeletePipelineRequestDescriptor(Action<DeletePipelineRequestDescriptor<TDocument>> configure) => configure.Invoke(this);

	public DeletePipelineRequestDescriptor(Elastic.Clients.Elasticsearch.Id id) : base(r => r.Required("id", id))
	{
	}

	internal DeletePipelineRequestDescriptor()
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlsLookups.IngestDeletePipeline;

	protected override HttpMethod StaticHttpMethod => HttpMethod.DELETE;

	internal override bool SupportsBody => false;

	public DeletePipelineRequestDescriptor<TDocument> MasterTimeout(Elastic.Clients.Elasticsearch.Duration? masterTimeout) => Qs("master_timeout", masterTimeout);
	public DeletePipelineRequestDescriptor<TDocument> Timeout(Elastic.Clients.Elasticsearch.Duration? timeout) => Qs("timeout", timeout);

	public DeletePipelineRequestDescriptor<TDocument> Id(Elastic.Clients.Elasticsearch.Id id)
	{
		RouteValues.Required("id", id);
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
	}
}

public sealed partial class DeletePipelineRequestDescriptor : RequestDescriptor<DeletePipelineRequestDescriptor, DeletePipelineRequestParameters>
{
	internal DeletePipelineRequestDescriptor(Action<DeletePipelineRequestDescriptor> configure) => configure.Invoke(this);

	public DeletePipelineRequestDescriptor(Elastic.Clients.Elasticsearch.Id id) : base(r => r.Required("id", id))
	{
	}

	internal DeletePipelineRequestDescriptor()
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlsLookups.IngestDeletePipeline;

	protected override HttpMethod StaticHttpMethod => HttpMethod.DELETE;

	internal override bool SupportsBody => false;

	public DeletePipelineRequestDescriptor MasterTimeout(Elastic.Clients.Elasticsearch.Duration? masterTimeout) => Qs("master_timeout", masterTimeout);
	public DeletePipelineRequestDescriptor Timeout(Elastic.Clients.Elasticsearch.Duration? timeout) => Qs("timeout", timeout);

	public DeletePipelineRequestDescriptor Id(Elastic.Clients.Elasticsearch.Id id)
	{
		RouteValues.Required("id", id);
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
	}
}