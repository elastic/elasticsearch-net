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

public sealed partial class ActivateUserProfileResponse : ElasticsearchResponse
{
	[JsonInclude, JsonPropertyName("data")]
	public IReadOnlyDictionary<string, object> Data { get; init; }
	[JsonInclude, JsonPropertyName("_doc")]
	public Elastic.Clients.Elasticsearch.Security.UserProfileHitMetadata Doc { get; init; }
	[JsonInclude, JsonPropertyName("enabled")]
	public bool? Enabled { get; init; }
	[JsonInclude, JsonPropertyName("labels")]
	public IReadOnlyDictionary<string, object> Labels { get; init; }
	[JsonInclude, JsonPropertyName("last_synchronized")]
	public long LastSynchronized { get; init; }
	[JsonInclude, JsonPropertyName("uid")]
	public string Uid { get; init; }
	[JsonInclude, JsonPropertyName("user")]
	public Elastic.Clients.Elasticsearch.Security.UserProfileUser User { get; init; }
}