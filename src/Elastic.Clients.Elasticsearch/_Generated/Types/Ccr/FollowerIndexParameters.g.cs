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
namespace Elastic.Clients.Elasticsearch.Ccr
{
	public sealed partial class FollowerIndexParameters
	{
		[JsonInclude]
		[JsonPropertyName("max_outstanding_read_requests")]
		public int MaxOutstandingReadRequests { get; init; }

		[JsonInclude]
		[JsonPropertyName("max_outstanding_write_requests")]
		public int MaxOutstandingWriteRequests { get; init; }

		[JsonInclude]
		[JsonPropertyName("max_read_request_operation_count")]
		public int MaxReadRequestOperationCount { get; init; }

		[JsonInclude]
		[JsonPropertyName("max_read_request_size")]
		public string MaxReadRequestSize { get; init; }

		[JsonInclude]
		[JsonPropertyName("max_retry_delay")]
		public Elastic.Clients.Elasticsearch.Duration MaxRetryDelay { get; init; }

		[JsonInclude]
		[JsonPropertyName("max_write_buffer_count")]
		public int MaxWriteBufferCount { get; init; }

		[JsonInclude]
		[JsonPropertyName("max_write_buffer_size")]
		public string MaxWriteBufferSize { get; init; }

		[JsonInclude]
		[JsonPropertyName("max_write_request_operation_count")]
		public int MaxWriteRequestOperationCount { get; init; }

		[JsonInclude]
		[JsonPropertyName("max_write_request_size")]
		public string MaxWriteRequestSize { get; init; }

		[JsonInclude]
		[JsonPropertyName("read_poll_timeout")]
		public Elastic.Clients.Elasticsearch.Duration ReadPollTimeout { get; init; }
	}
}