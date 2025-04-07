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

using System;
using System.Linq;
using Elastic.Clients.Elasticsearch.Serialization;

namespace Elastic.Clients.Elasticsearch;

internal sealed partial class ReindexResponseConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.ReindexResponse>
{
	private static readonly System.Text.Json.JsonEncodedText PropBatches = System.Text.Json.JsonEncodedText.Encode("batches");
	private static readonly System.Text.Json.JsonEncodedText PropCreated = System.Text.Json.JsonEncodedText.Encode("created");
	private static readonly System.Text.Json.JsonEncodedText PropDeleted = System.Text.Json.JsonEncodedText.Encode("deleted");
	private static readonly System.Text.Json.JsonEncodedText PropFailures = System.Text.Json.JsonEncodedText.Encode("failures");
	private static readonly System.Text.Json.JsonEncodedText PropNoops = System.Text.Json.JsonEncodedText.Encode("noops");
	private static readonly System.Text.Json.JsonEncodedText PropRequestsPerSecond = System.Text.Json.JsonEncodedText.Encode("requests_per_second");
	private static readonly System.Text.Json.JsonEncodedText PropRetries = System.Text.Json.JsonEncodedText.Encode("retries");
	private static readonly System.Text.Json.JsonEncodedText PropSliceId = System.Text.Json.JsonEncodedText.Encode("slice_id");
	private static readonly System.Text.Json.JsonEncodedText PropTask = System.Text.Json.JsonEncodedText.Encode("task");
	private static readonly System.Text.Json.JsonEncodedText PropThrottledMillis = System.Text.Json.JsonEncodedText.Encode("throttled_millis");
	private static readonly System.Text.Json.JsonEncodedText PropThrottledUntilMillis = System.Text.Json.JsonEncodedText.Encode("throttled_until_millis");
	private static readonly System.Text.Json.JsonEncodedText PropTimedOut = System.Text.Json.JsonEncodedText.Encode("timed_out");
	private static readonly System.Text.Json.JsonEncodedText PropTook = System.Text.Json.JsonEncodedText.Encode("took");
	private static readonly System.Text.Json.JsonEncodedText PropTotal = System.Text.Json.JsonEncodedText.Encode("total");
	private static readonly System.Text.Json.JsonEncodedText PropUpdated = System.Text.Json.JsonEncodedText.Encode("updated");
	private static readonly System.Text.Json.JsonEncodedText PropVersionConflicts = System.Text.Json.JsonEncodedText.Encode("version_conflicts");

	public override Elastic.Clients.Elasticsearch.ReindexResponse Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<long?> propBatches = default;
		LocalJsonValue<long?> propCreated = default;
		LocalJsonValue<long?> propDeleted = default;
		LocalJsonValue<System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.BulkIndexByScrollFailure>?> propFailures = default;
		LocalJsonValue<long?> propNoops = default;
		LocalJsonValue<float?> propRequestsPerSecond = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Retries?> propRetries = default;
		LocalJsonValue<int?> propSliceId = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.TaskId?> propTask = default;
		LocalJsonValue<System.DateTimeOffset?> propThrottledMillis = default;
		LocalJsonValue<System.DateTimeOffset?> propThrottledUntilMillis = default;
		LocalJsonValue<bool?> propTimedOut = default;
		LocalJsonValue<System.TimeSpan?> propTook = default;
		LocalJsonValue<long?> propTotal = default;
		LocalJsonValue<long?> propUpdated = default;
		LocalJsonValue<long?> propVersionConflicts = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propBatches.TryReadProperty(ref reader, options, PropBatches, null))
			{
				continue;
			}

			if (propCreated.TryReadProperty(ref reader, options, PropCreated, null))
			{
				continue;
			}

			if (propDeleted.TryReadProperty(ref reader, options, PropDeleted, null))
			{
				continue;
			}

			if (propFailures.TryReadProperty(ref reader, options, PropFailures, static System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.BulkIndexByScrollFailure>? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadCollectionValue<Elastic.Clients.Elasticsearch.BulkIndexByScrollFailure>(o, null)))
			{
				continue;
			}

			if (propNoops.TryReadProperty(ref reader, options, PropNoops, null))
			{
				continue;
			}

			if (propRequestsPerSecond.TryReadProperty(ref reader, options, PropRequestsPerSecond, null))
			{
				continue;
			}

			if (propRetries.TryReadProperty(ref reader, options, PropRetries, null))
			{
				continue;
			}

			if (propSliceId.TryReadProperty(ref reader, options, PropSliceId, null))
			{
				continue;
			}

			if (propTask.TryReadProperty(ref reader, options, PropTask, null))
			{
				continue;
			}

			if (propThrottledMillis.TryReadProperty(ref reader, options, PropThrottledMillis, static System.DateTimeOffset? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadValueEx<System.DateTimeOffset>(o, typeof(Elastic.Clients.Elasticsearch.Serialization.DateTimeMillisMarker))))
			{
				continue;
			}

			if (propThrottledUntilMillis.TryReadProperty(ref reader, options, PropThrottledUntilMillis, static System.DateTimeOffset? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadValueEx<System.DateTimeOffset>(o, typeof(Elastic.Clients.Elasticsearch.Serialization.DateTimeMillisMarker))))
			{
				continue;
			}

			if (propTimedOut.TryReadProperty(ref reader, options, PropTimedOut, null))
			{
				continue;
			}

			if (propTook.TryReadProperty(ref reader, options, PropTook, static System.TimeSpan? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadValueEx<System.TimeSpan>(o, typeof(Elastic.Clients.Elasticsearch.Serialization.TimeSpanMillisMarker))))
			{
				continue;
			}

			if (propTotal.TryReadProperty(ref reader, options, PropTotal, null))
			{
				continue;
			}

			if (propUpdated.TryReadProperty(ref reader, options, PropUpdated, null))
			{
				continue;
			}

			if (propVersionConflicts.TryReadProperty(ref reader, options, PropVersionConflicts, null))
			{
				continue;
			}

			if (options.UnmappedMemberHandling is System.Text.Json.Serialization.JsonUnmappedMemberHandling.Skip)
			{
				reader.Skip();
				continue;
			}

			throw new System.Text.Json.JsonException($"Unknown JSON property '{reader.GetString()}' for type '{typeToConvert.Name}'.");
		}

		reader.ValidateToken(System.Text.Json.JsonTokenType.EndObject);
		return new Elastic.Clients.Elasticsearch.ReindexResponse(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Batches = propBatches.Value,
			Created = propCreated.Value,
			Deleted = propDeleted.Value,
			Failures = propFailures.Value,
			Noops = propNoops.Value,
			RequestsPerSecond = propRequestsPerSecond.Value,
			Retries = propRetries.Value,
			SliceId = propSliceId.Value,
			Task = propTask.Value,
			ThrottledMillis = propThrottledMillis.Value,
			ThrottledUntilMillis = propThrottledUntilMillis.Value,
			TimedOut = propTimedOut.Value,
			Took = propTook.Value,
			Total = propTotal.Value,
			Updated = propUpdated.Value,
			VersionConflicts = propVersionConflicts.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.ReindexResponse value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropBatches, value.Batches, null, null);
		writer.WriteProperty(options, PropCreated, value.Created, null, null);
		writer.WriteProperty(options, PropDeleted, value.Deleted, null, null);
		writer.WriteProperty(options, PropFailures, value.Failures, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.BulkIndexByScrollFailure>? v) => w.WriteCollectionValue<Elastic.Clients.Elasticsearch.BulkIndexByScrollFailure>(o, v, null));
		writer.WriteProperty(options, PropNoops, value.Noops, null, null);
		writer.WriteProperty(options, PropRequestsPerSecond, value.RequestsPerSecond, null, null);
		writer.WriteProperty(options, PropRetries, value.Retries, null, null);
		writer.WriteProperty(options, PropSliceId, value.SliceId, null, null);
		writer.WriteProperty(options, PropTask, value.Task, null, null);
		writer.WriteProperty(options, PropThrottledMillis, value.ThrottledMillis, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.DateTimeOffset? v) => w.WriteValueEx<System.DateTimeOffset?>(o, v, typeof(Elastic.Clients.Elasticsearch.Serialization.DateTimeMillisMarker)));
		writer.WriteProperty(options, PropThrottledUntilMillis, value.ThrottledUntilMillis, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.DateTimeOffset? v) => w.WriteValueEx<System.DateTimeOffset?>(o, v, typeof(Elastic.Clients.Elasticsearch.Serialization.DateTimeMillisMarker)));
		writer.WriteProperty(options, PropTimedOut, value.TimedOut, null, null);
		writer.WriteProperty(options, PropTook, value.Took, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.TimeSpan? v) => w.WriteValueEx<System.TimeSpan?>(o, v, typeof(Elastic.Clients.Elasticsearch.Serialization.TimeSpanMillisMarker)));
		writer.WriteProperty(options, PropTotal, value.Total, null, null);
		writer.WriteProperty(options, PropUpdated, value.Updated, null, null);
		writer.WriteProperty(options, PropVersionConflicts, value.VersionConflicts, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.ReindexResponseConverter))]
public sealed partial class ReindexResponse : Elastic.Transport.Products.Elasticsearch.ElasticsearchResponse
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public ReindexResponse()
	{
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal ReindexResponse(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	/// <summary>
	/// <para>
	/// The number of scroll responses that were pulled back by the reindex.
	/// </para>
	/// </summary>
	public long? Batches { get; set; }

	/// <summary>
	/// <para>
	/// The number of documents that were successfully created.
	/// </para>
	/// </summary>
	public long? Created { get; set; }

	/// <summary>
	/// <para>
	/// The number of documents that were successfully deleted.
	/// </para>
	/// </summary>
	public long? Deleted { get; set; }

	/// <summary>
	/// <para>
	/// If there were any unrecoverable errors during the process, it is an array of those failures.
	/// If this array is not empty, the request ended because of those failures.
	/// Reindex is implemented using batches and any failure causes the entire process to end but all failures in the current batch are collected into the array.
	/// You can use the <c>conflicts</c> option to prevent the reindex from ending on version conflicts.
	/// </para>
	/// </summary>
	public System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.BulkIndexByScrollFailure>? Failures { get; set; }

	/// <summary>
	/// <para>
	/// The number of documents that were ignored because the script used for the reindex returned a <c>noop</c> value for <c>ctx.op</c>.
	/// </para>
	/// </summary>
	public long? Noops { get; set; }

	/// <summary>
	/// <para>
	/// The number of requests per second effectively run during the reindex.
	/// </para>
	/// </summary>
	public float? RequestsPerSecond { get; set; }

	/// <summary>
	/// <para>
	/// The number of retries attempted by reindex.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Retries? Retries { get; set; }
	public int? SliceId { get; set; }
	public Elastic.Clients.Elasticsearch.TaskId? Task { get; set; }

	/// <summary>
	/// <para>
	/// The number of milliseconds the request slept to conform to <c>requests_per_second</c>.
	/// </para>
	/// </summary>
	public System.DateTimeOffset? ThrottledMillis { get; set; }

	/// <summary>
	/// <para>
	/// This field should always be equal to zero in a reindex response.
	/// It has meaning only when using the task API, where it indicates the next time (in milliseconds since epoch) that a throttled request will be run again in order to conform to <c>requests_per_second</c>.
	/// </para>
	/// </summary>
	public System.DateTimeOffset? ThrottledUntilMillis { get; set; }

	/// <summary>
	/// <para>
	/// If any of the requests that ran during the reindex timed out, it is <c>true</c>.
	/// </para>
	/// </summary>
	public bool? TimedOut { get; set; }

	/// <summary>
	/// <para>
	/// The total milliseconds the entire operation took.
	/// </para>
	/// </summary>
	public System.TimeSpan? Took { get; set; }

	/// <summary>
	/// <para>
	/// The number of documents that were successfully processed.
	/// </para>
	/// </summary>
	public long? Total { get; set; }

	/// <summary>
	/// <para>
	/// The number of documents that were successfully updated.
	/// That is to say, a document with the same ID already existed before the reindex updated it.
	/// </para>
	/// </summary>
	public long? Updated { get; set; }

	/// <summary>
	/// <para>
	/// The number of version conflicts that occurred.
	/// </para>
	/// </summary>
	public long? VersionConflicts { get; set; }
}