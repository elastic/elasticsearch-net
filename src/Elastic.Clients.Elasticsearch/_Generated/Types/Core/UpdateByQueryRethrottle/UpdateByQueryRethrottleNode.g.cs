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

namespace Elastic.Clients.Elasticsearch.Core.UpdateByQueryRethrottle;

internal sealed partial class UpdateByQueryRethrottleNodeConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Core.UpdateByQueryRethrottle.UpdateByQueryRethrottleNode>
{
	private static readonly System.Text.Json.JsonEncodedText PropAttributes = System.Text.Json.JsonEncodedText.Encode("attributes");
	private static readonly System.Text.Json.JsonEncodedText PropHost = System.Text.Json.JsonEncodedText.Encode("host");
	private static readonly System.Text.Json.JsonEncodedText PropIp = System.Text.Json.JsonEncodedText.Encode("ip");
	private static readonly System.Text.Json.JsonEncodedText PropName = System.Text.Json.JsonEncodedText.Encode("name");
	private static readonly System.Text.Json.JsonEncodedText PropRoles = System.Text.Json.JsonEncodedText.Encode("roles");
	private static readonly System.Text.Json.JsonEncodedText PropTasks = System.Text.Json.JsonEncodedText.Encode("tasks");
	private static readonly System.Text.Json.JsonEncodedText PropTransportAddress = System.Text.Json.JsonEncodedText.Encode("transport_address");

	public override Elastic.Clients.Elasticsearch.Core.UpdateByQueryRethrottle.UpdateByQueryRethrottleNode Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<System.Collections.Generic.IReadOnlyDictionary<string, string>> propAttributes = default;
		LocalJsonValue<string> propHost = default;
		LocalJsonValue<string> propIp = default;
		LocalJsonValue<string> propName = default;
		LocalJsonValue<System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.NodeRole>?> propRoles = default;
		LocalJsonValue<System.Collections.Generic.IReadOnlyDictionary<Elastic.Clients.Elasticsearch.TaskId, Elastic.Clients.Elasticsearch.Tasks.TaskInfo>> propTasks = default;
		LocalJsonValue<string> propTransportAddress = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propAttributes.TryReadProperty(ref reader, options, PropAttributes, static System.Collections.Generic.IReadOnlyDictionary<string, string> (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadDictionaryValue<string, string>(o, null, null)!))
			{
				continue;
			}

			if (propHost.TryReadProperty(ref reader, options, PropHost, null))
			{
				continue;
			}

			if (propIp.TryReadProperty(ref reader, options, PropIp, null))
			{
				continue;
			}

			if (propName.TryReadProperty(ref reader, options, PropName, null))
			{
				continue;
			}

			if (propRoles.TryReadProperty(ref reader, options, PropRoles, static System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.NodeRole>? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadCollectionValue<Elastic.Clients.Elasticsearch.NodeRole>(o, null)))
			{
				continue;
			}

			if (propTasks.TryReadProperty(ref reader, options, PropTasks, static System.Collections.Generic.IReadOnlyDictionary<Elastic.Clients.Elasticsearch.TaskId, Elastic.Clients.Elasticsearch.Tasks.TaskInfo> (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadDictionaryValue<Elastic.Clients.Elasticsearch.TaskId, Elastic.Clients.Elasticsearch.Tasks.TaskInfo>(o, null, null)!))
			{
				continue;
			}

			if (propTransportAddress.TryReadProperty(ref reader, options, PropTransportAddress, null))
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
		return new Elastic.Clients.Elasticsearch.Core.UpdateByQueryRethrottle.UpdateByQueryRethrottleNode(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Attributes = propAttributes.Value,
			Host = propHost.Value,
			Ip = propIp.Value,
			Name = propName.Value,
			Roles = propRoles.Value,
			Tasks = propTasks.Value,
			TransportAddress = propTransportAddress.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Core.UpdateByQueryRethrottle.UpdateByQueryRethrottleNode value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropAttributes, value.Attributes, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.IReadOnlyDictionary<string, string> v) => w.WriteDictionaryValue<string, string>(o, v, null, null));
		writer.WriteProperty(options, PropHost, value.Host, null, null);
		writer.WriteProperty(options, PropIp, value.Ip, null, null);
		writer.WriteProperty(options, PropName, value.Name, null, null);
		writer.WriteProperty(options, PropRoles, value.Roles, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.NodeRole>? v) => w.WriteCollectionValue<Elastic.Clients.Elasticsearch.NodeRole>(o, v, null));
		writer.WriteProperty(options, PropTasks, value.Tasks, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.IReadOnlyDictionary<Elastic.Clients.Elasticsearch.TaskId, Elastic.Clients.Elasticsearch.Tasks.TaskInfo> v) => w.WriteDictionaryValue<Elastic.Clients.Elasticsearch.TaskId, Elastic.Clients.Elasticsearch.Tasks.TaskInfo>(o, v, null, null));
		writer.WriteProperty(options, PropTransportAddress, value.TransportAddress, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Core.UpdateByQueryRethrottle.UpdateByQueryRethrottleNodeConverter))]
public sealed partial class UpdateByQueryRethrottleNode
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public UpdateByQueryRethrottleNode(System.Collections.Generic.IReadOnlyDictionary<string, string> attributes, string host, string ip, string name, System.Collections.Generic.IReadOnlyDictionary<Elastic.Clients.Elasticsearch.TaskId, Elastic.Clients.Elasticsearch.Tasks.TaskInfo> tasks, string transportAddress)
	{
		Attributes = attributes;
		Host = host;
		Ip = ip;
		Name = name;
		Tasks = tasks;
		TransportAddress = transportAddress;
	}
#if NET7_0_OR_GREATER
	public UpdateByQueryRethrottleNode()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains additional required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public UpdateByQueryRethrottleNode()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal UpdateByQueryRethrottleNode(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	public
#if NET7_0_OR_GREATER
	required
#endif
	System.Collections.Generic.IReadOnlyDictionary<string, string> Attributes { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	string Host { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	string Ip { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	string Name { get; set; }
	public System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.NodeRole>? Roles { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	System.Collections.Generic.IReadOnlyDictionary<Elastic.Clients.Elasticsearch.TaskId, Elastic.Clients.Elasticsearch.Tasks.TaskInfo> Tasks { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	string TransportAddress { get; set; }
}