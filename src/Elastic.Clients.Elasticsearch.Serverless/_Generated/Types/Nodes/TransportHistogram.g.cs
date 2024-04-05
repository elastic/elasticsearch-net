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

#nullable restore

using Elastic.Clients.Elasticsearch.Serverless.Fluent;
using Elastic.Clients.Elasticsearch.Serverless.Serialization;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch.Serverless.Nodes;

public sealed partial class TransportHistogram
{
	/// <summary>
	/// <para>The number of times a transport thread took a period of time within the bounds of this bucket to handle an inbound message.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("count")]
	public long? Count { get; init; }

	/// <summary>
	/// <para>The inclusive lower bound of the bucket in milliseconds. May be omitted on the first bucket if this bucket has no lower bound.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("ge_millis")]
	public long? GeMillis { get; init; }

	/// <summary>
	/// <para>The exclusive upper bound of the bucket in milliseconds.<br/>May be omitted on the last bucket if this bucket has no upper bound.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("lt_millis")]
	public long? LtMillis { get; init; }
}