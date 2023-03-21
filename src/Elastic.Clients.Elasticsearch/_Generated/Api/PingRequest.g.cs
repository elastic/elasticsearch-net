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

namespace Elastic.Clients.Elasticsearch;

public sealed class PingRequestParameters : RequestParameters
{
}

public sealed partial class PingRequest : PlainRequest<PingRequestParameters>
{
	internal override ApiUrls ApiUrls => ApiUrlsLookups.NoNamespacePing;

	protected override HttpMethod StaticHttpMethod => HttpMethod.HEAD;

	internal override bool SupportsBody => false;
}

public sealed partial class PingRequestDescriptor : RequestDescriptor<PingRequestDescriptor, PingRequestParameters>
{
	internal PingRequestDescriptor(Action<PingRequestDescriptor> configure) => configure.Invoke(this);

	public PingRequestDescriptor()
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlsLookups.NoNamespacePing;

	protected override HttpMethod StaticHttpMethod => HttpMethod.HEAD;

	internal override bool SupportsBody => false;

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
	}
}