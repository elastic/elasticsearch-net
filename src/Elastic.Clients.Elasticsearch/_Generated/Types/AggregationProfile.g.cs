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
namespace Elastic.Clients.Elasticsearch
{
	public partial class AggregationProfile
	{
		[JsonInclude]
		[JsonPropertyName("breakdown")]
		public Elastic.Clients.Elasticsearch.AggregationBreakdown Breakdown { get; init; }

		[JsonInclude]
		[JsonPropertyName("description")]
		public string Description { get; init; }

		[JsonInclude]
		[JsonPropertyName("time_in_nanos")]
		public long TimeInNanos { get; init; }

		[JsonInclude]
		[JsonPropertyName("type")]
		public string Type { get; init; }

		[JsonInclude]
		[JsonPropertyName("debug")]
		public Elastic.Clients.Elasticsearch.AggregationProfileDebug? Debug { get; init; }

		[JsonInclude]
		[JsonPropertyName("children")]
		public IReadOnlyCollection<Elastic.Clients.Elasticsearch.AggregationProfile>? Children { get; init; }
	}
}