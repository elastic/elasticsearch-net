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
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;

#nullable restore
namespace Elastic.Clients.Elasticsearch.AsyncSearch
{
	public abstract partial class AsyncSearchResponseBase : ResponseBase
	{
		[JsonInclude]
		[JsonPropertyName("expiration_time_in_millis")]
		public Elastic.Clients.Elasticsearch.EpochMillis ExpirationTimeInMillis { get; init; }

		[JsonInclude]
		[JsonPropertyName("id")]
		public Elastic.Clients.Elasticsearch.Id? Id { get; init; }

		[JsonInclude]
		[JsonPropertyName("is_partial")]
		public bool IsPartial { get; init; }

		[JsonInclude]
		[JsonPropertyName("is_running")]
		public bool IsRunning { get; init; }

		[JsonInclude]
		[JsonPropertyName("start_time_in_millis")]
		public Elastic.Clients.Elasticsearch.EpochMillis StartTimeInMillis { get; init; }
	}
}