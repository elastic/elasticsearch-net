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
namespace Elastic.Clients.Elasticsearch.IndexManagement.GetDataStream
{
	public partial class IndicesGetDataStreamItem
	{
		[JsonInclude]
		[JsonPropertyName("name")]
		public Elastic.Clients.Elasticsearch.DataStreamName Name { get; init; }

		[JsonInclude]
		[JsonPropertyName("timestamp_field")]
		public Elastic.Clients.Elasticsearch.IndexManagement.GetDataStream.IndicesGetDataStreamItemTimestampField TimestampField { get; init; }

		[JsonInclude]
		[JsonPropertyName("indices")]
		public IReadOnlyCollection<Elastic.Clients.Elasticsearch.IndexManagement.GetDataStream.IndicesGetDataStreamItemIndex> Indices { get; init; }

		[JsonInclude]
		[JsonPropertyName("generation")]
		public int Generation { get; init; }

		[JsonInclude]
		[JsonPropertyName("template")]
		public Elastic.Clients.Elasticsearch.Name Template { get; init; }

		[JsonInclude]
		[JsonPropertyName("hidden")]
		public bool Hidden { get; init; }

		[JsonInclude]
		[JsonPropertyName("system")]
		public bool? System { get; init; }

		[JsonInclude]
		[JsonPropertyName("status")]
		public Elastic.Clients.Elasticsearch.HealthStatus Status { get; init; }

		[JsonInclude]
		[JsonPropertyName("ilm_policy")]
		public Elastic.Clients.Elasticsearch.Name? IlmPolicy { get; init; }

		[JsonInclude]
		[JsonPropertyName("_meta")]
		public Dictionary<string, object>? Meta { get; init; }
	}
}