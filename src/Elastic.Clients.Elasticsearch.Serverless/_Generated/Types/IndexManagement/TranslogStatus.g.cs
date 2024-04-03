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

namespace Elastic.Clients.Elasticsearch.Serverless.IndexManagement;

public sealed partial class TranslogStatus
{
	[JsonInclude, JsonPropertyName("percent")]
	public double Percent { get; init; }
	[JsonInclude, JsonPropertyName("recovered")]
	public long Recovered { get; init; }
	[JsonInclude, JsonPropertyName("total")]
	public long Total { get; init; }
	[JsonInclude, JsonPropertyName("total_on_start")]
	public long TotalOnStart { get; init; }
	[JsonInclude, JsonPropertyName("total_time")]
	public Elastic.Clients.Elasticsearch.Serverless.Duration? TotalTime { get; init; }
	[JsonInclude, JsonPropertyName("total_time_in_millis")]
	public long TotalTimeInMillis { get; init; }
}