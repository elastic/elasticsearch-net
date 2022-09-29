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
	public sealed partial class ShardFailure
	{
		[JsonInclude]
		[JsonPropertyName("index")]
		public string? Index { get; init; }

		[JsonInclude]
		[JsonPropertyName("node")]
		public string? Node { get; init; }

		[JsonInclude]
		[JsonPropertyName("reason")]
		public Elastic.Clients.Elasticsearch.ErrorCause Reason { get; init; }

		[JsonInclude]
		[JsonPropertyName("shard")]
		public int Shard { get; init; }

		[JsonInclude]
		[JsonPropertyName("status")]
		public string? Status { get; init; }
	}
}