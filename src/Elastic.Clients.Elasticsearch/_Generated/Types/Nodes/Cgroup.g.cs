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

internal sealed partial class CgroupConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Nodes.Cgroup>
{
	private static readonly System.Text.Json.JsonEncodedText PropCpu = System.Text.Json.JsonEncodedText.Encode("cpu");
	private static readonly System.Text.Json.JsonEncodedText PropCpuacct = System.Text.Json.JsonEncodedText.Encode("cpuacct");
	private static readonly System.Text.Json.JsonEncodedText PropMemory = System.Text.Json.JsonEncodedText.Encode("memory");

	public override Elastic.Clients.Elasticsearch.Nodes.Cgroup Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<Elastic.Clients.Elasticsearch.Nodes.CgroupCpu?> propCpu = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Nodes.CpuAcct?> propCpuacct = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Nodes.CgroupMemory?> propMemory = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propCpu.TryReadProperty(ref reader, options, PropCpu, null))
			{
				continue;
			}

			if (propCpuacct.TryReadProperty(ref reader, options, PropCpuacct, null))
			{
				continue;
			}

			if (propMemory.TryReadProperty(ref reader, options, PropMemory, null))
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
		return new Elastic.Clients.Elasticsearch.Nodes.Cgroup(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Cpu = propCpu.Value,
			Cpuacct = propCpuacct.Value,
			Memory = propMemory.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Nodes.Cgroup value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropCpu, value.Cpu, null, null);
		writer.WriteProperty(options, PropCpuacct, value.Cpuacct, null, null);
		writer.WriteProperty(options, PropMemory, value.Memory, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Nodes.CgroupConverter))]
public sealed partial class Cgroup
{
#if NET7_0_OR_GREATER
	public Cgroup()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	public Cgroup()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal Cgroup(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	/// <summary>
	/// <para>
	/// Contains statistics about <c>cpu</c> control group for the node.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Nodes.CgroupCpu? Cpu { get; set; }

	/// <summary>
	/// <para>
	/// Contains statistics about <c>cpuacct</c> control group for the node.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Nodes.CpuAcct? Cpuacct { get; set; }

	/// <summary>
	/// <para>
	/// Contains statistics about the memory control group for the node.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Nodes.CgroupMemory? Memory { get; set; }
}