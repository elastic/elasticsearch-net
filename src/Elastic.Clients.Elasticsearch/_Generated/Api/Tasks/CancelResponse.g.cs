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

namespace Elastic.Clients.Elasticsearch.Tasks;

internal sealed partial class CancelResponseConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Tasks.CancelResponse>
{
	private static readonly System.Text.Json.JsonEncodedText PropNodeFailures = System.Text.Json.JsonEncodedText.Encode("node_failures");
	private static readonly System.Text.Json.JsonEncodedText PropNodes = System.Text.Json.JsonEncodedText.Encode("nodes");
	private static readonly System.Text.Json.JsonEncodedText PropTaskFailures = System.Text.Json.JsonEncodedText.Encode("task_failures");
	private static readonly System.Text.Json.JsonEncodedText PropTasks = System.Text.Json.JsonEncodedText.Encode("tasks");

	public override Elastic.Clients.Elasticsearch.Tasks.CancelResponse Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.ErrorCause>?> propNodeFailures = default;
		LocalJsonValue<System.Collections.Generic.IReadOnlyDictionary<string, Elastic.Clients.Elasticsearch.Tasks.NodeTasks>?> propNodes = default;
		LocalJsonValue<System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.TaskFailure>?> propTaskFailures = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Tasks.TaskInfos?> propTasks = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propNodeFailures.TryReadProperty(ref reader, options, PropNodeFailures, static System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.ErrorCause>? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadCollectionValue<Elastic.Clients.Elasticsearch.ErrorCause>(o, null)))
			{
				continue;
			}

			if (propNodes.TryReadProperty(ref reader, options, PropNodes, static System.Collections.Generic.IReadOnlyDictionary<string, Elastic.Clients.Elasticsearch.Tasks.NodeTasks>? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadDictionaryValue<string, Elastic.Clients.Elasticsearch.Tasks.NodeTasks>(o, null, null)))
			{
				continue;
			}

			if (propTaskFailures.TryReadProperty(ref reader, options, PropTaskFailures, static System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.TaskFailure>? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadCollectionValue<Elastic.Clients.Elasticsearch.TaskFailure>(o, null)))
			{
				continue;
			}

			if (propTasks.TryReadProperty(ref reader, options, PropTasks, null))
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
		return new Elastic.Clients.Elasticsearch.Tasks.CancelResponse(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			NodeFailures = propNodeFailures.Value,
			Nodes = propNodes.Value,
			TaskFailures = propTaskFailures.Value,
			Tasks = propTasks.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Tasks.CancelResponse value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropNodeFailures, value.NodeFailures, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.ErrorCause>? v) => w.WriteCollectionValue<Elastic.Clients.Elasticsearch.ErrorCause>(o, v, null));
		writer.WriteProperty(options, PropNodes, value.Nodes, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.IReadOnlyDictionary<string, Elastic.Clients.Elasticsearch.Tasks.NodeTasks>? v) => w.WriteDictionaryValue<string, Elastic.Clients.Elasticsearch.Tasks.NodeTasks>(o, v, null, null));
		writer.WriteProperty(options, PropTaskFailures, value.TaskFailures, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.TaskFailure>? v) => w.WriteCollectionValue<Elastic.Clients.Elasticsearch.TaskFailure>(o, v, null));
		writer.WriteProperty(options, PropTasks, value.Tasks, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Tasks.CancelResponseConverter))]
public sealed partial class CancelResponse : Elastic.Transport.Products.Elasticsearch.ElasticsearchResponse
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public CancelResponse()
	{
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal CancelResponse(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	public System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.ErrorCause>? NodeFailures { get; set; }

	/// <summary>
	/// <para>
	/// Task information grouped by node, if <c>group_by</c> was set to <c>node</c> (the default).
	/// </para>
	/// </summary>
	public System.Collections.Generic.IReadOnlyDictionary<string, Elastic.Clients.Elasticsearch.Tasks.NodeTasks>? Nodes { get; set; }
	public System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.TaskFailure>? TaskFailures { get; set; }

	/// <summary>
	/// <para>
	/// Either a flat list of tasks if <c>group_by</c> was set to <c>none</c>, or grouped by parents if
	/// <c>group_by</c> was set to <c>parents</c>.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Tasks.TaskInfos? Tasks { get; set; }
}