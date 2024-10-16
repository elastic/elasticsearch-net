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

namespace Elastic.Clients.Elasticsearch.Serverless.Ingest;

public sealed partial class GeoIpStatsRequestParameters : RequestParameters
{
}

/// <summary>
/// <para>
/// Gets download statistics for GeoIP2 databases used with the geoip processor.
/// </para>
/// </summary>
public sealed partial class GeoIpStatsRequest : PlainRequest<GeoIpStatsRequestParameters>
{
	internal override ApiUrls ApiUrls => ApiUrlLookup.IngestGeoIpStats;

	protected override HttpMethod StaticHttpMethod => HttpMethod.GET;

	internal override bool SupportsBody => false;

	internal override string OperationName => "ingest.geo_ip_stats";
}

/// <summary>
/// <para>
/// Gets download statistics for GeoIP2 databases used with the geoip processor.
/// </para>
/// </summary>
public sealed partial class GeoIpStatsRequestDescriptor : RequestDescriptor<GeoIpStatsRequestDescriptor, GeoIpStatsRequestParameters>
{
	internal GeoIpStatsRequestDescriptor(Action<GeoIpStatsRequestDescriptor> configure) => configure.Invoke(this);

	public GeoIpStatsRequestDescriptor()
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.IngestGeoIpStats;

	protected override HttpMethod StaticHttpMethod => HttpMethod.GET;

	internal override bool SupportsBody => false;

	internal override string OperationName => "ingest.geo_ip_stats";

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
	}
}