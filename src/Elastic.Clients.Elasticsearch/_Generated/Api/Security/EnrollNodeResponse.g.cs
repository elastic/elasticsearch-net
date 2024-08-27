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
using Elastic.Clients.Elasticsearch.Serialization;
using Elastic.Transport.Products.Elasticsearch;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch.Security;

public sealed partial class EnrollNodeResponse : ElasticsearchResponse
{
	[JsonInclude, JsonPropertyName("http_ca_cert")]
	public string HttpCaCert { get; init; }
	[JsonInclude, JsonPropertyName("http_ca_key")]
	public string HttpCaKey { get; init; }
	[JsonInclude, JsonPropertyName("nodes_addresses")]
	public IReadOnlyCollection<string> NodesAddresses { get; init; }
	[JsonInclude, JsonPropertyName("transport_ca_cert")]
	public string TransportCaCert { get; init; }
	[JsonInclude, JsonPropertyName("transport_cert")]
	public string TransportCert { get; init; }
	[JsonInclude, JsonPropertyName("transport_key")]
	public string TransportKey { get; init; }
}