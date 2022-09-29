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
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;

#nullable restore
namespace Elastic.Clients.Elasticsearch.Nodes
{
	public sealed partial class NodeOperatingSystemInfo
	{
		[JsonInclude]
		[JsonPropertyName("allocated_processors")]
		public int? AllocatedProcessors { get; init; }

		[JsonInclude]
		[JsonPropertyName("arch")]
		public string Arch { get; init; }

		[JsonInclude]
		[JsonPropertyName("available_processors")]
		public int AvailableProcessors { get; init; }

		[JsonInclude]
		[JsonPropertyName("cpu")]
		public Elastic.Clients.Elasticsearch.Nodes.NodeInfoOSCPU? Cpu { get; init; }

		[JsonInclude]
		[JsonPropertyName("mem")]
		public Elastic.Clients.Elasticsearch.Nodes.NodeInfoMemory? Mem { get; init; }

		[JsonInclude]
		[JsonPropertyName("name")]
		public string Name { get; init; }

		[JsonInclude]
		[JsonPropertyName("pretty_name")]
		public string PrettyName { get; init; }

		[JsonInclude]
		[JsonPropertyName("refresh_interval_in_millis")]
		public long RefreshIntervalInMillis { get; init; }

		[JsonInclude]
		[JsonPropertyName("swap")]
		public Elastic.Clients.Elasticsearch.Nodes.NodeInfoMemory? Swap { get; init; }

		[JsonInclude]
		[JsonPropertyName("version")]
		public string Version { get; init; }
	}
}