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

namespace Elastic.Clients.Elasticsearch.Nodes;

internal sealed partial class JvmConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Nodes.Jvm>
{
	private static readonly System.Text.Json.JsonEncodedText PropBufferPools = System.Text.Json.JsonEncodedText.Encode("buffer_pools");
	private static readonly System.Text.Json.JsonEncodedText PropClasses = System.Text.Json.JsonEncodedText.Encode("classes");
	private static readonly System.Text.Json.JsonEncodedText PropGc = System.Text.Json.JsonEncodedText.Encode("gc");
	private static readonly System.Text.Json.JsonEncodedText PropMem = System.Text.Json.JsonEncodedText.Encode("mem");
	private static readonly System.Text.Json.JsonEncodedText PropThreads = System.Text.Json.JsonEncodedText.Encode("threads");
	private static readonly System.Text.Json.JsonEncodedText PropTimestamp = System.Text.Json.JsonEncodedText.Encode("timestamp");
	private static readonly System.Text.Json.JsonEncodedText PropUptime = System.Text.Json.JsonEncodedText.Encode("uptime");
	private static readonly System.Text.Json.JsonEncodedText PropUptimeInMillis = System.Text.Json.JsonEncodedText.Encode("uptime_in_millis");

	public override Elastic.Clients.Elasticsearch.Nodes.Jvm Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<System.Collections.Generic.IReadOnlyDictionary<string, Elastic.Clients.Elasticsearch.Nodes.NodeBufferPool>?> propBufferPools = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Nodes.JvmClasses?> propClasses = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Nodes.GarbageCollector?> propGc = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Nodes.JvmMemoryStats?> propMem = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Nodes.JvmThreads?> propThreads = default;
		LocalJsonValue<long?> propTimestamp = default;
		LocalJsonValue<string?> propUptime = default;
		LocalJsonValue<long?> propUptimeInMillis = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propBufferPools.TryReadProperty(ref reader, options, PropBufferPools, static System.Collections.Generic.IReadOnlyDictionary<string, Elastic.Clients.Elasticsearch.Nodes.NodeBufferPool>? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadDictionaryValue<string, Elastic.Clients.Elasticsearch.Nodes.NodeBufferPool>(o, null, null)))
			{
				continue;
			}

			if (propClasses.TryReadProperty(ref reader, options, PropClasses, null))
			{
				continue;
			}

			if (propGc.TryReadProperty(ref reader, options, PropGc, null))
			{
				continue;
			}

			if (propMem.TryReadProperty(ref reader, options, PropMem, null))
			{
				continue;
			}

			if (propThreads.TryReadProperty(ref reader, options, PropThreads, null))
			{
				continue;
			}

			if (propTimestamp.TryReadProperty(ref reader, options, PropTimestamp, null))
			{
				continue;
			}

			if (propUptime.TryReadProperty(ref reader, options, PropUptime, null))
			{
				continue;
			}

			if (propUptimeInMillis.TryReadProperty(ref reader, options, PropUptimeInMillis, null))
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
		return new Elastic.Clients.Elasticsearch.Nodes.Jvm(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			BufferPools = propBufferPools.Value,
			Classes = propClasses.Value,
			Gc = propGc.Value,
			Mem = propMem.Value,
			Threads = propThreads.Value,
			Timestamp = propTimestamp.Value,
			Uptime = propUptime.Value,
			UptimeInMillis = propUptimeInMillis.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Nodes.Jvm value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropBufferPools, value.BufferPools, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.IReadOnlyDictionary<string, Elastic.Clients.Elasticsearch.Nodes.NodeBufferPool>? v) => w.WriteDictionaryValue<string, Elastic.Clients.Elasticsearch.Nodes.NodeBufferPool>(o, v, null, null));
		writer.WriteProperty(options, PropClasses, value.Classes, null, null);
		writer.WriteProperty(options, PropGc, value.Gc, null, null);
		writer.WriteProperty(options, PropMem, value.Mem, null, null);
		writer.WriteProperty(options, PropThreads, value.Threads, null, null);
		writer.WriteProperty(options, PropTimestamp, value.Timestamp, null, null);
		writer.WriteProperty(options, PropUptime, value.Uptime, null, null);
		writer.WriteProperty(options, PropUptimeInMillis, value.UptimeInMillis, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Nodes.JvmConverter))]
public sealed partial class Jvm
{
#if NET7_0_OR_GREATER
	public Jvm()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	public Jvm()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal Jvm(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	/// <summary>
	/// <para>
	/// Contains statistics about JVM buffer pools for the node.
	/// </para>
	/// </summary>
	public System.Collections.Generic.IReadOnlyDictionary<string, Elastic.Clients.Elasticsearch.Nodes.NodeBufferPool>? BufferPools { get; set; }

	/// <summary>
	/// <para>
	/// Contains statistics about classes loaded by JVM for the node.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Nodes.JvmClasses? Classes { get; set; }

	/// <summary>
	/// <para>
	/// Contains statistics about JVM garbage collectors for the node.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Nodes.GarbageCollector? Gc { get; set; }

	/// <summary>
	/// <para>
	/// Contains JVM memory usage statistics for the node.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Nodes.JvmMemoryStats? Mem { get; set; }

	/// <summary>
	/// <para>
	/// Contains statistics about JVM thread usage for the node.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Nodes.JvmThreads? Threads { get; set; }

	/// <summary>
	/// <para>
	/// Last time JVM statistics were refreshed.
	/// </para>
	/// </summary>
	public long? Timestamp { get; set; }

	/// <summary>
	/// <para>
	/// Human-readable JVM uptime.
	/// Only returned if the <c>human</c> query parameter is <c>true</c>.
	/// </para>
	/// </summary>
	public string? Uptime { get; set; }

	/// <summary>
	/// <para>
	/// JVM uptime in milliseconds.
	/// </para>
	/// </summary>
	public long? UptimeInMillis { get; set; }
}