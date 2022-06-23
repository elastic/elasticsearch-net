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
namespace Elastic.Clients.Elasticsearch.Sql
{
	public sealed partial class SqlGetAsyncStatusResponse : ElasticsearchResponseBase
	{
		[JsonInclude]
		[JsonPropertyName("completion_status")]
		public int? CompletionStatus { get; init; }

		[JsonInclude]
		[JsonPropertyName("expiration_time_in_millis")]
		public long ExpirationTimeInMillis { get; init; }

		[JsonInclude]
		[JsonPropertyName("id")]
		public string Id { get; init; }

		[JsonInclude]
		[JsonPropertyName("is_partial")]
		public bool IsPartial { get; init; }

		[JsonInclude]
		[JsonPropertyName("is_running")]
		public bool IsRunning { get; init; }

		[JsonInclude]
		[JsonPropertyName("start_time_in_millis")]
		public long StartTimeInMillis { get; init; }
	}
}