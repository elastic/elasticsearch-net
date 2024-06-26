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

public sealed partial class Cpu
{
	[JsonInclude, JsonPropertyName("load_average")]
	public IReadOnlyDictionary<string, double>? LoadAverage { get; init; }
	[JsonInclude, JsonPropertyName("percent")]
	public int? Percent { get; init; }
	[JsonInclude, JsonPropertyName("sys")]
	public Elastic.Clients.Elasticsearch.Serverless.Duration? Sys { get; init; }
	[JsonInclude, JsonPropertyName("sys_in_millis")]
	public long? SysInMillis { get; init; }
	[JsonInclude, JsonPropertyName("total")]
	public Elastic.Clients.Elasticsearch.Serverless.Duration? Total { get; init; }
	[JsonInclude, JsonPropertyName("total_in_millis")]
	public long? TotalInMillis { get; init; }
	[JsonInclude, JsonPropertyName("user")]
	public Elastic.Clients.Elasticsearch.Serverless.Duration? User { get; init; }
	[JsonInclude, JsonPropertyName("user_in_millis")]
	public long? UserInMillis { get; init; }
}