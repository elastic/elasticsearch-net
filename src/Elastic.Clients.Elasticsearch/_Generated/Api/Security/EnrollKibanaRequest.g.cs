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

namespace Elastic.Clients.Elasticsearch.Security;

public sealed partial class EnrollKibanaRequestParameters : RequestParameters
{
}

/// <summary>
/// <para>
/// Enroll Kibana.
/// </para>
/// <para>
/// Enable a Kibana instance to configure itself for communication with a secured Elasticsearch cluster.
/// </para>
/// </summary>
public sealed partial class EnrollKibanaRequest : PlainRequest<EnrollKibanaRequestParameters>
{
	[JsonConstructor]
	internal EnrollKibanaRequest()
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.SecurityEnrollKibana;

	protected override HttpMethod StaticHttpMethod => HttpMethod.GET;

	internal override bool SupportsBody => false;

	internal override string OperationName => "security.enroll_kibana";
}

/// <summary>
/// <para>
/// Enroll Kibana.
/// </para>
/// <para>
/// Enable a Kibana instance to configure itself for communication with a secured Elasticsearch cluster.
/// </para>
/// </summary>
public sealed partial class EnrollKibanaRequestDescriptor : RequestDescriptor<EnrollKibanaRequestDescriptor, EnrollKibanaRequestParameters>
{
	internal EnrollKibanaRequestDescriptor(Action<EnrollKibanaRequestDescriptor> configure) => configure.Invoke(this);

	public EnrollKibanaRequestDescriptor()
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.SecurityEnrollKibana;

	protected override HttpMethod StaticHttpMethod => HttpMethod.GET;

	internal override bool SupportsBody => false;

	internal override string OperationName => "security.enroll_kibana";

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
	}
}