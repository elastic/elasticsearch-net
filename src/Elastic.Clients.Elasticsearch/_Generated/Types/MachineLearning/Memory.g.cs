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

namespace Elastic.Clients.Elasticsearch.MachineLearning;

internal sealed partial class MemoryConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.MachineLearning.Memory>
{
	private static readonly System.Text.Json.JsonEncodedText PropAttributes = System.Text.Json.JsonEncodedText.Encode("attributes");
	private static readonly System.Text.Json.JsonEncodedText PropEphemeralId = System.Text.Json.JsonEncodedText.Encode("ephemeral_id");
	private static readonly System.Text.Json.JsonEncodedText PropJvm = System.Text.Json.JsonEncodedText.Encode("jvm");
	private static readonly System.Text.Json.JsonEncodedText PropMem = System.Text.Json.JsonEncodedText.Encode("mem");
	private static readonly System.Text.Json.JsonEncodedText PropName = System.Text.Json.JsonEncodedText.Encode("name");
	private static readonly System.Text.Json.JsonEncodedText PropRoles = System.Text.Json.JsonEncodedText.Encode("roles");
	private static readonly System.Text.Json.JsonEncodedText PropTransportAddress = System.Text.Json.JsonEncodedText.Encode("transport_address");

	public override Elastic.Clients.Elasticsearch.MachineLearning.Memory Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<System.Collections.Generic.IReadOnlyDictionary<string, string>> propAttributes = default;
		LocalJsonValue<string> propEphemeralId = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.MachineLearning.JvmStats> propJvm = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.MachineLearning.MemStats> propMem = default;
		LocalJsonValue<string> propName = default;
		LocalJsonValue<System.Collections.Generic.IReadOnlyCollection<string>> propRoles = default;
		LocalJsonValue<string> propTransportAddress = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propAttributes.TryReadProperty(ref reader, options, PropAttributes, static System.Collections.Generic.IReadOnlyDictionary<string, string> (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadDictionaryValue<string, string>(o, null, null)!))
			{
				continue;
			}

			if (propEphemeralId.TryReadProperty(ref reader, options, PropEphemeralId, null))
			{
				continue;
			}

			if (propJvm.TryReadProperty(ref reader, options, PropJvm, null))
			{
				continue;
			}

			if (propMem.TryReadProperty(ref reader, options, PropMem, null))
			{
				continue;
			}

			if (propName.TryReadProperty(ref reader, options, PropName, null))
			{
				continue;
			}

			if (propRoles.TryReadProperty(ref reader, options, PropRoles, static System.Collections.Generic.IReadOnlyCollection<string> (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadCollectionValue<string>(o, null)!))
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
		return new Elastic.Clients.Elasticsearch.MachineLearning.Memory(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Attributes = propAttributes.Value,
			EphemeralId = propEphemeralId.Value,
			Jvm = propJvm.Value,
			Mem = propMem.Value,
			Name = propName.Value,
			Roles = propRoles.Value,
			TransportAddress = propTransportAddress.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.MachineLearning.Memory value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropAttributes, value.Attributes, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.IReadOnlyDictionary<string, string> v) => w.WriteDictionaryValue<string, string>(o, v, null, null));
		writer.WriteProperty(options, PropEphemeralId, value.EphemeralId, null, null);
		writer.WriteProperty(options, PropJvm, value.Jvm, null, null);
		writer.WriteProperty(options, PropMem, value.Mem, null, null);
		writer.WriteProperty(options, PropName, value.Name, null, null);
		writer.WriteProperty(options, PropRoles, value.Roles, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.IReadOnlyCollection<string> v) => w.WriteCollectionValue<string>(o, v, null));
		writer.WriteProperty(options, PropTransportAddress, value.TransportAddress, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.MachineLearning.MemoryConverter))]
public sealed partial class Memory
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public Memory(System.Collections.Generic.IReadOnlyDictionary<string, string> attributes, string ephemeralId, Elastic.Clients.Elasticsearch.MachineLearning.JvmStats jvm, Elastic.Clients.Elasticsearch.MachineLearning.MemStats mem, string name, System.Collections.Generic.IReadOnlyCollection<string> roles, string transportAddress)
	{
		Attributes = attributes;
		EphemeralId = ephemeralId;
		Jvm = jvm;
		Mem = mem;
		Name = name;
		Roles = roles;
		TransportAddress = transportAddress;
	}
#if NET7_0_OR_GREATER
	public Memory()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains additional required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public Memory()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal Memory(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
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
	string EphemeralId { get; set; }

	/// <summary>
	/// <para>
	/// Contains Java Virtual Machine (JVM) statistics for the node.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.MachineLearning.JvmStats Jvm { get; set; }

	/// <summary>
	/// <para>
	/// Contains statistics about memory usage for the node.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.MachineLearning.MemStats Mem { get; set; }

	/// <summary>
	/// <para>
	/// Human-readable identifier for the node. Based on the Node name setting setting.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	string Name { get; set; }

	/// <summary>
	/// <para>
	/// Roles assigned to the node.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	System.Collections.Generic.IReadOnlyCollection<string> Roles { get; set; }

	/// <summary>
	/// <para>
	/// The host and port where transport HTTP connections are accepted.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	string TransportAddress { get; set; }
}