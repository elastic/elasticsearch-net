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

namespace Elastic.Clients.Elasticsearch.Serverless.Tasks;

public sealed partial class ParentTaskInfo
{
	[JsonInclude, JsonPropertyName("action")]
	public string Action { get; init; }
	[JsonInclude, JsonPropertyName("cancellable")]
	public bool Cancellable { get; init; }
	[JsonInclude, JsonPropertyName("cancelled")]
	public bool? Cancelled { get; init; }
	[JsonInclude, JsonPropertyName("children")]
	public IReadOnlyCollection<Elastic.Clients.Elasticsearch.Serverless.Tasks.TaskInfo>? Children { get; init; }
	[JsonInclude, JsonPropertyName("description")]
	public string? Description { get; init; }
	[JsonInclude, JsonPropertyName("headers")]
	public IReadOnlyDictionary<string, string> Headers { get; init; }
	[JsonInclude, JsonPropertyName("id")]
	public long Id { get; init; }
	[JsonInclude, JsonPropertyName("node")]
	public string Node { get; init; }
	[JsonInclude, JsonPropertyName("parent_task_id")]
	public Elastic.Clients.Elasticsearch.Serverless.TaskId? ParentTaskId { get; init; }
	[JsonInclude, JsonPropertyName("running_time")]
	public Elastic.Clients.Elasticsearch.Serverless.Duration? RunningTime { get; init; }
	[JsonInclude, JsonPropertyName("running_time_in_nanos")]
	public long RunningTimeInNanos { get; init; }
	[JsonInclude, JsonPropertyName("start_time_in_millis")]
	public long StartTimeInMillis { get; init; }

	/// <summary>
	/// <para>Task status information can vary wildly from task to task.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("status")]
	public object? Status { get; init; }
	[JsonInclude, JsonPropertyName("type")]
	public string Type { get; init; }
}