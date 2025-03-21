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

using Elastic.Clients.Elasticsearch.Fluent;
using Elastic.Clients.Elasticsearch.Serialization;
using Elastic.Transport.Products.Elasticsearch;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch;

public sealed partial class ReindexResponse : ElasticsearchResponse
{
	/// <summary>
	/// <para>
	/// The number of scroll responses that were pulled back by the reindex.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("batches")]
	public long? Batches { get; init; }

	/// <summary>
	/// <para>
	/// The number of documents that were successfully created.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("created")]
	public long? Created { get; init; }

	/// <summary>
	/// <para>
	/// The number of documents that were successfully deleted.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("deleted")]
	public long? Deleted { get; init; }

	/// <summary>
	/// <para>
	/// If there were any unrecoverable errors during the process, it is an array of those failures.
	/// If this array is not empty, the request ended because of those failures.
	/// Reindex is implemented using batches and any failure causes the entire process to end but all failures in the current batch are collected into the array.
	/// You can use the <c>conflicts</c> option to prevent the reindex from ending on version conflicts.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("failures")]
	public IReadOnlyCollection<Elastic.Clients.Elasticsearch.BulkIndexByScrollFailure>? Failures { get; init; }

	/// <summary>
	/// <para>
	/// The number of documents that were ignored because the script used for the reindex returned a <c>noop</c> value for <c>ctx.op</c>.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("noops")]
	public long? Noops { get; init; }

	/// <summary>
	/// <para>
	/// The number of requests per second effectively run during the reindex.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("requests_per_second")]
	public float? RequestsPerSecond { get; init; }

	/// <summary>
	/// <para>
	/// The number of retries attempted by reindex.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("retries")]
	public Elastic.Clients.Elasticsearch.Retries? Retries { get; init; }
	[JsonInclude, JsonPropertyName("slice_id")]
	public int? SliceId { get; init; }
	[JsonInclude, JsonPropertyName("task")]
	public Elastic.Clients.Elasticsearch.TaskId? Task { get; init; }

	/// <summary>
	/// <para>
	/// The number of milliseconds the request slept to conform to <c>requests_per_second</c>.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("throttled_millis")]
	public long? ThrottledMillis { get; init; }

	/// <summary>
	/// <para>
	/// This field should always be equal to zero in a reindex response.
	/// It has meaning only when using the task API, where it indicates the next time (in milliseconds since epoch) that a throttled request will be run again in order to conform to <c>requests_per_second</c>.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("throttled_until_millis")]
	public long? ThrottledUntilMillis { get; init; }

	/// <summary>
	/// <para>
	/// If any of the requests that ran during the reindex timed out, it is <c>true</c>.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("timed_out")]
	public bool? TimedOut { get; init; }

	/// <summary>
	/// <para>
	/// The total milliseconds the entire operation took.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("took")]
	public long? Took { get; init; }

	/// <summary>
	/// <para>
	/// The number of documents that were successfully processed.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("total")]
	public long? Total { get; init; }

	/// <summary>
	/// <para>
	/// The number of documents that were successfully updated.
	/// That is to say, a document with the same ID already existed before the reindex updated it.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("updated")]
	public long? Updated { get; init; }

	/// <summary>
	/// <para>
	/// The number of version conflicts that occurred.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("version_conflicts")]
	public long? VersionConflicts { get; init; }
}