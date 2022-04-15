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

using Elastic.Transport.Products.Elasticsearch;
using System.Collections.Generic;
using System.Text.Json.Serialization;

#nullable restore
namespace Elastic.Clients.Elasticsearch.TransformManagement
{
	public partial class TransformUpdateTransformResponse : ElasticsearchResponseBase
	{
		[JsonInclude]
		[JsonPropertyName("create_time")]
		public long CreateTime { get; init; }

		[JsonInclude]
		[JsonPropertyName("description")]
		public string Description { get; init; }

		[JsonInclude]
		[JsonPropertyName("dest")]
		public Elastic.Clients.Elasticsearch.Destination Dest { get; init; }

		[JsonInclude]
		[JsonPropertyName("frequency")]
		public Elastic.Clients.Elasticsearch.Time? Frequency { get; init; }

		[JsonInclude]
		[JsonPropertyName("id")]
		public string Id { get; init; }

		[JsonInclude]
		[JsonPropertyName("_meta")]
		public Dictionary<string, object>? Meta { get; init; }

		[JsonInclude]
		[JsonPropertyName("pivot")]
		public Elastic.Clients.Elasticsearch.TransformManagement.Pivot Pivot { get; init; }

		[JsonInclude]
		[JsonPropertyName("settings")]
		public Elastic.Clients.Elasticsearch.TransformManagement.Settings Settings { get; init; }

		[JsonInclude]
		[JsonPropertyName("source")]
		public Elastic.Clients.Elasticsearch.Source Source { get; init; }

		[JsonInclude]
		[JsonPropertyName("sync")]
		public Elastic.Clients.Elasticsearch.TransformManagement.SyncContainer? Sync { get; init; }

		[JsonInclude]
		[JsonPropertyName("version")]
		public string Version { get; init; }
	}
}