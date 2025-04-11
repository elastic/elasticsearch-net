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

namespace Elastic.Clients.Elasticsearch.Cluster;

internal sealed partial class PendingTaskConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Cluster.PendingTask>
{
	private static readonly System.Text.Json.JsonEncodedText PropExecuting = System.Text.Json.JsonEncodedText.Encode("executing");
	private static readonly System.Text.Json.JsonEncodedText PropInsertOrder = System.Text.Json.JsonEncodedText.Encode("insert_order");
	private static readonly System.Text.Json.JsonEncodedText PropPriority = System.Text.Json.JsonEncodedText.Encode("priority");
	private static readonly System.Text.Json.JsonEncodedText PropSource = System.Text.Json.JsonEncodedText.Encode("source");
	private static readonly System.Text.Json.JsonEncodedText PropTimeInQueue = System.Text.Json.JsonEncodedText.Encode("time_in_queue");
	private static readonly System.Text.Json.JsonEncodedText PropTimeInQueueMillis = System.Text.Json.JsonEncodedText.Encode("time_in_queue_millis");

	public override Elastic.Clients.Elasticsearch.Cluster.PendingTask Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<bool> propExecuting = default;
		LocalJsonValue<int> propInsertOrder = default;
		LocalJsonValue<string> propPriority = default;
		LocalJsonValue<string> propSource = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Duration?> propTimeInQueue = default;
		LocalJsonValue<System.TimeSpan> propTimeInQueueMillis = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propExecuting.TryReadProperty(ref reader, options, PropExecuting, null))
			{
				continue;
			}

			if (propInsertOrder.TryReadProperty(ref reader, options, PropInsertOrder, null))
			{
				continue;
			}

			if (propPriority.TryReadProperty(ref reader, options, PropPriority, null))
			{
				continue;
			}

			if (propSource.TryReadProperty(ref reader, options, PropSource, null))
			{
				continue;
			}

			if (propTimeInQueue.TryReadProperty(ref reader, options, PropTimeInQueue, null))
			{
				continue;
			}

			if (propTimeInQueueMillis.TryReadProperty(ref reader, options, PropTimeInQueueMillis, static System.TimeSpan (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadValueEx<System.TimeSpan>(o, typeof(Elastic.Clients.Elasticsearch.Serialization.TimeSpanMillisMarker))))
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
		return new Elastic.Clients.Elasticsearch.Cluster.PendingTask(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Executing = propExecuting.Value,
			InsertOrder = propInsertOrder.Value,
			Priority = propPriority.Value,
			Source = propSource.Value,
			TimeInQueue = propTimeInQueue.Value,
			TimeInQueueMillis = propTimeInQueueMillis.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Cluster.PendingTask value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropExecuting, value.Executing, null, null);
		writer.WriteProperty(options, PropInsertOrder, value.InsertOrder, null, null);
		writer.WriteProperty(options, PropPriority, value.Priority, null, null);
		writer.WriteProperty(options, PropSource, value.Source, null, null);
		writer.WriteProperty(options, PropTimeInQueue, value.TimeInQueue, null, null);
		writer.WriteProperty(options, PropTimeInQueueMillis, value.TimeInQueueMillis, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.TimeSpan v) => w.WriteValueEx<System.TimeSpan>(o, v, typeof(Elastic.Clients.Elasticsearch.Serialization.TimeSpanMillisMarker)));
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Cluster.PendingTaskConverter))]
public sealed partial class PendingTask
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public PendingTask(bool executing, int insertOrder, string priority, string source, System.TimeSpan timeInQueueMillis)
	{
		Executing = executing;
		InsertOrder = insertOrder;
		Priority = priority;
		Source = source;
		TimeInQueueMillis = timeInQueueMillis;
	}
#if NET7_0_OR_GREATER
	public PendingTask()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public PendingTask()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal PendingTask(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	/// <summary>
	/// <para>
	/// Indicates whether the pending tasks are currently executing or not.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	bool Executing { get; set; }

	/// <summary>
	/// <para>
	/// The number that represents when the task has been inserted into the task queue.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	int InsertOrder { get; set; }

	/// <summary>
	/// <para>
	/// The priority of the pending task.
	/// The valid priorities in descending priority order are: <c>IMMEDIATE</c> > <c>URGENT</c> > <c>HIGH</c> > <c>NORMAL</c> > <c>LOW</c> > <c>LANGUID</c>.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	string Priority { get; set; }

	/// <summary>
	/// <para>
	/// A general description of the cluster task that may include a reason and origin.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	string Source { get; set; }

	/// <summary>
	/// <para>
	/// The time since the task is waiting for being performed.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Duration? TimeInQueue { get; set; }

	/// <summary>
	/// <para>
	/// The time expressed in milliseconds since the task is waiting for being performed.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	System.TimeSpan TimeInQueueMillis { get; set; }
}