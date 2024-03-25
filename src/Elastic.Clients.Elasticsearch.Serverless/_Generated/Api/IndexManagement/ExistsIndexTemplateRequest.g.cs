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

namespace Elastic.Clients.Elasticsearch.Serverless.IndexManagement;

public sealed partial class ExistsIndexTemplateRequestParameters : RequestParameters
{
	/// <summary>
	/// <para>Period to wait for a connection to the master node. If no response is received before the timeout expires, the request fails and returns an error.</para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Serverless.Duration? MasterTimeout { get => Q<Elastic.Clients.Elasticsearch.Serverless.Duration?>("master_timeout"); set => Q("master_timeout", value); }
}

/// <summary>
/// <para>Returns information about whether a particular index template exists.</para>
/// </summary>
public sealed partial class ExistsIndexTemplateRequest : PlainRequest<ExistsIndexTemplateRequestParameters>
{
	public ExistsIndexTemplateRequest(Elastic.Clients.Elasticsearch.Serverless.Name name) : base(r => r.Required("name", name))
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.IndexManagementExistsIndexTemplate;

	protected override HttpMethod StaticHttpMethod => HttpMethod.HEAD;

	internal override bool SupportsBody => false;

	internal override string OperationName => "indices.exists_index_template";

	/// <summary>
	/// <para>Period to wait for a connection to the master node. If no response is received before the timeout expires, the request fails and returns an error.</para>
	/// </summary>
	[JsonIgnore]
	public Elastic.Clients.Elasticsearch.Serverless.Duration? MasterTimeout { get => Q<Elastic.Clients.Elasticsearch.Serverless.Duration?>("master_timeout"); set => Q("master_timeout", value); }
}

/// <summary>
/// <para>Returns information about whether a particular index template exists.</para>
/// </summary>
public sealed partial class ExistsIndexTemplateRequestDescriptor : RequestDescriptor<ExistsIndexTemplateRequestDescriptor, ExistsIndexTemplateRequestParameters>
{
	internal ExistsIndexTemplateRequestDescriptor(Action<ExistsIndexTemplateRequestDescriptor> configure) => configure.Invoke(this);

	public ExistsIndexTemplateRequestDescriptor(Elastic.Clients.Elasticsearch.Serverless.Name name) : base(r => r.Required("name", name))
	{
	}

	internal ExistsIndexTemplateRequestDescriptor()
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.IndexManagementExistsIndexTemplate;

	protected override HttpMethod StaticHttpMethod => HttpMethod.HEAD;

	internal override bool SupportsBody => false;

	internal override string OperationName => "indices.exists_index_template";

	public ExistsIndexTemplateRequestDescriptor MasterTimeout(Elastic.Clients.Elasticsearch.Serverless.Duration? masterTimeout) => Qs("master_timeout", masterTimeout);

	public ExistsIndexTemplateRequestDescriptor Name(Elastic.Clients.Elasticsearch.Serverless.Name name)
	{
		RouteValues.Required("name", name);
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
	}
}