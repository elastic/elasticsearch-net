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
namespace Elastic.Clients.Elasticsearch.Cluster.AllocationExplain
{
	public partial class DiskUsage
	{
		[JsonInclude]
		[JsonPropertyName("path")]
		public string Path { get; init; }

		[JsonInclude]
		[JsonPropertyName("total_bytes")]
		public long TotalBytes { get; init; }

		[JsonInclude]
		[JsonPropertyName("used_bytes")]
		public long UsedBytes { get; init; }

		[JsonInclude]
		[JsonPropertyName("free_bytes")]
		public long FreeBytes { get; init; }

		[JsonInclude]
		[JsonPropertyName("free_disk_percent")]
		public double FreeDiskPercent { get; init; }

		[JsonInclude]
		[JsonPropertyName("used_disk_percent")]
		public double UsedDiskPercent { get; init; }
	}
}