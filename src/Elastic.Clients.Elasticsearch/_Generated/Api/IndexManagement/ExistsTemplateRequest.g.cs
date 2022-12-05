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

using Elastic.Clients.Elasticsearch.Fluent;
using Elastic.Clients.Elasticsearch.Requests;
using Elastic.Clients.Elasticsearch.Serialization;
using Elastic.Transport;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;

#nullable restore
namespace Elastic.Clients.Elasticsearch.IndexManagement;
public sealed class ExistsTemplateRequestParameters : RequestParameters
{
	[JsonIgnore]
	public bool? FlatSettings { get => Q<bool?>("flat_settings"); set => Q("flat_settings", value); }

	[JsonIgnore]
	public bool? Local { get => Q<bool?>("local"); set => Q("local", value); }

	[JsonIgnore]
	public Elastic.Clients.Elasticsearch.Duration? MasterTimeout { get => Q<Elastic.Clients.Elasticsearch.Duration?>("master_timeout"); set => Q("master_timeout", value); }
}

public sealed partial class ExistsTemplateRequest : PlainRequest<ExistsTemplateRequestParameters>
{
	public ExistsTemplateRequest(Elastic.Clients.Elasticsearch.Names name) : base(r => r.Required("name", name))
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlsLookups.IndexManagementExistsTemplate;
	protected override HttpMethod StaticHttpMethod => HttpMethod.HEAD;
	internal override bool SupportsBody => false;
	[JsonIgnore]
	public bool? FlatSettings { get => Q<bool?>("flat_settings"); set => Q("flat_settings", value); }

	[JsonIgnore]
	public bool? Local { get => Q<bool?>("local"); set => Q("local", value); }

	[JsonIgnore]
	public Elastic.Clients.Elasticsearch.Duration? MasterTimeout { get => Q<Elastic.Clients.Elasticsearch.Duration?>("master_timeout"); set => Q("master_timeout", value); }
}

public sealed partial class ExistsTemplateRequestDescriptor : RequestDescriptor<ExistsTemplateRequestDescriptor, ExistsTemplateRequestParameters>
{
	internal ExistsTemplateRequestDescriptor(Action<ExistsTemplateRequestDescriptor> configure) => configure.Invoke(this);
	public ExistsTemplateRequestDescriptor(Elastic.Clients.Elasticsearch.Names name) : base(r => r.Required("name", name))
	{
	}

	internal ExistsTemplateRequestDescriptor()
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlsLookups.IndexManagementExistsTemplate;
	protected override HttpMethod StaticHttpMethod => HttpMethod.HEAD;
	internal override bool SupportsBody => false;
	public ExistsTemplateRequestDescriptor FlatSettings(bool? flatSettings = true) => Qs("flat_settings", flatSettings);
	public ExistsTemplateRequestDescriptor Local(bool? local = true) => Qs("local", local);
	public ExistsTemplateRequestDescriptor MasterTimeout(Elastic.Clients.Elasticsearch.Duration? masterTimeout) => Qs("master_timeout", masterTimeout);
	public ExistsTemplateRequestDescriptor Name(Elastic.Clients.Elasticsearch.Names name)
	{
		RouteValues.Required("name", name);
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
	}
}