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

namespace Elastic.Clients.Elasticsearch.TransformManagement;

public sealed partial class UpgradeTransformsResponse : ElasticsearchResponse
{
	/// <summary>
	/// <para>
	/// The number of transforms that need to be upgraded.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("needs_update")]
	public int NeedsUpdate { get; init; }

	/// <summary>
	/// <para>
	/// The number of transforms that don’t require upgrading.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("no_action")]
	public int NoAction { get; init; }

	/// <summary>
	/// <para>
	/// The number of transforms that have been upgraded.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("updated")]
	public int Updated { get; init; }
}