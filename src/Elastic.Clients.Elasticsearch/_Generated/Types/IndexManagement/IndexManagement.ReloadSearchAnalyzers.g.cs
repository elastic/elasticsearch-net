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

using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

#nullable restore
namespace Elastic.Clients.Elasticsearch.IndexManagement.ReloadSearchAnalyzers
{
	public partial class ReloadDetails
	{
		[JsonInclude]
		[JsonPropertyName("index")]
		public string Index { get; init; }

		[JsonInclude]
		[JsonPropertyName("reloaded_analyzers")]
		public IReadOnlyCollection<string> ReloadedAnalyzers { get; init; }

		[JsonInclude]
		[JsonPropertyName("reloaded_node_ids")]
		public IReadOnlyCollection<string> ReloadedNodeIds { get; init; }
	}
}