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

internal sealed partial class ClusterOperatingSystemConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Cluster.ClusterOperatingSystem>
{
	private static readonly System.Text.Json.JsonEncodedText PropAllocatedProcessors = System.Text.Json.JsonEncodedText.Encode("allocated_processors");
	private static readonly System.Text.Json.JsonEncodedText PropArchitectures = System.Text.Json.JsonEncodedText.Encode("architectures");
	private static readonly System.Text.Json.JsonEncodedText PropAvailableProcessors = System.Text.Json.JsonEncodedText.Encode("available_processors");
	private static readonly System.Text.Json.JsonEncodedText PropMem = System.Text.Json.JsonEncodedText.Encode("mem");
	private static readonly System.Text.Json.JsonEncodedText PropNames = System.Text.Json.JsonEncodedText.Encode("names");
	private static readonly System.Text.Json.JsonEncodedText PropPrettyNames = System.Text.Json.JsonEncodedText.Encode("pretty_names");

	public override Elastic.Clients.Elasticsearch.Cluster.ClusterOperatingSystem Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<int> propAllocatedProcessors = default;
		LocalJsonValue<System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.Cluster.ClusterOperatingSystemArchitecture>?> propArchitectures = default;
		LocalJsonValue<int> propAvailableProcessors = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Cluster.OperatingSystemMemoryInfo> propMem = default;
		LocalJsonValue<System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.Cluster.ClusterOperatingSystemName>> propNames = default;
		LocalJsonValue<System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.Cluster.ClusterOperatingSystemPrettyName>> propPrettyNames = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propAllocatedProcessors.TryReadProperty(ref reader, options, PropAllocatedProcessors, null))
			{
				continue;
			}

			if (propArchitectures.TryReadProperty(ref reader, options, PropArchitectures, static System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.Cluster.ClusterOperatingSystemArchitecture>? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadCollectionValue<Elastic.Clients.Elasticsearch.Cluster.ClusterOperatingSystemArchitecture>(o, null)))
			{
				continue;
			}

			if (propAvailableProcessors.TryReadProperty(ref reader, options, PropAvailableProcessors, null))
			{
				continue;
			}

			if (propMem.TryReadProperty(ref reader, options, PropMem, null))
			{
				continue;
			}

			if (propNames.TryReadProperty(ref reader, options, PropNames, static System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.Cluster.ClusterOperatingSystemName> (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadCollectionValue<Elastic.Clients.Elasticsearch.Cluster.ClusterOperatingSystemName>(o, null)!))
			{
				continue;
			}

			if (propPrettyNames.TryReadProperty(ref reader, options, PropPrettyNames, static System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.Cluster.ClusterOperatingSystemPrettyName> (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadCollectionValue<Elastic.Clients.Elasticsearch.Cluster.ClusterOperatingSystemPrettyName>(o, null)!))
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
		return new Elastic.Clients.Elasticsearch.Cluster.ClusterOperatingSystem(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			AllocatedProcessors = propAllocatedProcessors.Value,
			Architectures = propArchitectures.Value,
			AvailableProcessors = propAvailableProcessors.Value,
			Mem = propMem.Value,
			Names = propNames.Value,
			PrettyNames = propPrettyNames.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Cluster.ClusterOperatingSystem value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropAllocatedProcessors, value.AllocatedProcessors, null, null);
		writer.WriteProperty(options, PropArchitectures, value.Architectures, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.Cluster.ClusterOperatingSystemArchitecture>? v) => w.WriteCollectionValue<Elastic.Clients.Elasticsearch.Cluster.ClusterOperatingSystemArchitecture>(o, v, null));
		writer.WriteProperty(options, PropAvailableProcessors, value.AvailableProcessors, null, null);
		writer.WriteProperty(options, PropMem, value.Mem, null, null);
		writer.WriteProperty(options, PropNames, value.Names, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.Cluster.ClusterOperatingSystemName> v) => w.WriteCollectionValue<Elastic.Clients.Elasticsearch.Cluster.ClusterOperatingSystemName>(o, v, null));
		writer.WriteProperty(options, PropPrettyNames, value.PrettyNames, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.Cluster.ClusterOperatingSystemPrettyName> v) => w.WriteCollectionValue<Elastic.Clients.Elasticsearch.Cluster.ClusterOperatingSystemPrettyName>(o, v, null));
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Cluster.ClusterOperatingSystemConverter))]
public sealed partial class ClusterOperatingSystem
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public ClusterOperatingSystem(int allocatedProcessors, int availableProcessors, Elastic.Clients.Elasticsearch.Cluster.OperatingSystemMemoryInfo mem, System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.Cluster.ClusterOperatingSystemName> names, System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.Cluster.ClusterOperatingSystemPrettyName> prettyNames)
	{
		AllocatedProcessors = allocatedProcessors;
		AvailableProcessors = availableProcessors;
		Mem = mem;
		Names = names;
		PrettyNames = prettyNames;
	}
#if NET7_0_OR_GREATER
	public ClusterOperatingSystem()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains additional required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public ClusterOperatingSystem()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal ClusterOperatingSystem(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	/// <summary>
	/// <para>
	/// Number of processors used to calculate thread pool size across all selected nodes.
	/// This number can be set with the processors setting of a node and defaults to the number of processors reported by the operating system.
	/// In both cases, this number will never be larger than 32.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	int AllocatedProcessors { get; set; }

	/// <summary>
	/// <para>
	/// Contains statistics about processor architectures (for example, x86_64 or aarch64) used by selected nodes.
	/// </para>
	/// </summary>
	public System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.Cluster.ClusterOperatingSystemArchitecture>? Architectures { get; set; }

	/// <summary>
	/// <para>
	/// Number of processors available to JVM across all selected nodes.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	int AvailableProcessors { get; set; }

	/// <summary>
	/// <para>
	/// Contains statistics about memory used by selected nodes.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.Cluster.OperatingSystemMemoryInfo Mem { get; set; }

	/// <summary>
	/// <para>
	/// Contains statistics about operating systems used by selected nodes.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.Cluster.ClusterOperatingSystemName> Names { get; set; }

	/// <summary>
	/// <para>
	/// Contains statistics about operating systems used by selected nodes.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.Cluster.ClusterOperatingSystemPrettyName> PrettyNames { get; set; }
}