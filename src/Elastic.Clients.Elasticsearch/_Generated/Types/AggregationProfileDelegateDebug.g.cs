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
	public partial class AggregationProfileDelegateDebug
	{
		[JsonInclude]
		[JsonPropertyName("filters")]
		public IReadOnlyCollection<Elastic.Clients.Elasticsearch.AggregationProfileDelegateDebugFilter>? Filters { get; init; }

		[JsonInclude]
		[JsonPropertyName("segments_collected")]
		public int? SegmentsCollected { get; init; }

		[JsonInclude]
		[JsonPropertyName("segments_counted")]
		public int? SegmentsCounted { get; init; }

		[JsonInclude]
		[JsonPropertyName("segments_with_deleted_docs")]
		public int? SegmentsWithDeletedDocs { get; init; }

		[JsonInclude]
		[JsonPropertyName("segments_with_doc_count_field")]
		public int? SegmentsWithDocCountField { get; init; }
	}
}