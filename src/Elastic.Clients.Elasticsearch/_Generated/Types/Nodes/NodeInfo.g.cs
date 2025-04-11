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

internal sealed partial class NodeInfoConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Nodes.NodeInfo>
{
	private static readonly System.Text.Json.JsonEncodedText PropAggregations = System.Text.Json.JsonEncodedText.Encode("aggregations");
	private static readonly System.Text.Json.JsonEncodedText PropAttributes = System.Text.Json.JsonEncodedText.Encode("attributes");
	private static readonly System.Text.Json.JsonEncodedText PropBuildFlavor = System.Text.Json.JsonEncodedText.Encode("build_flavor");
	private static readonly System.Text.Json.JsonEncodedText PropBuildHash = System.Text.Json.JsonEncodedText.Encode("build_hash");
	private static readonly System.Text.Json.JsonEncodedText PropBuildType = System.Text.Json.JsonEncodedText.Encode("build_type");
	private static readonly System.Text.Json.JsonEncodedText PropHost = System.Text.Json.JsonEncodedText.Encode("host");
	private static readonly System.Text.Json.JsonEncodedText PropHttp = System.Text.Json.JsonEncodedText.Encode("http");
	private static readonly System.Text.Json.JsonEncodedText PropIngest = System.Text.Json.JsonEncodedText.Encode("ingest");
	private static readonly System.Text.Json.JsonEncodedText PropIp = System.Text.Json.JsonEncodedText.Encode("ip");
	private static readonly System.Text.Json.JsonEncodedText PropJvm = System.Text.Json.JsonEncodedText.Encode("jvm");
	private static readonly System.Text.Json.JsonEncodedText PropModules = System.Text.Json.JsonEncodedText.Encode("modules");
	private static readonly System.Text.Json.JsonEncodedText PropName = System.Text.Json.JsonEncodedText.Encode("name");
	private static readonly System.Text.Json.JsonEncodedText PropNetwork = System.Text.Json.JsonEncodedText.Encode("network");
	private static readonly System.Text.Json.JsonEncodedText PropOs = System.Text.Json.JsonEncodedText.Encode("os");
	private static readonly System.Text.Json.JsonEncodedText PropPlugins = System.Text.Json.JsonEncodedText.Encode("plugins");
	private static readonly System.Text.Json.JsonEncodedText PropProcess = System.Text.Json.JsonEncodedText.Encode("process");
	private static readonly System.Text.Json.JsonEncodedText PropRoles = System.Text.Json.JsonEncodedText.Encode("roles");
	private static readonly System.Text.Json.JsonEncodedText PropSettings = System.Text.Json.JsonEncodedText.Encode("settings");
	private static readonly System.Text.Json.JsonEncodedText PropThreadPool = System.Text.Json.JsonEncodedText.Encode("thread_pool");
	private static readonly System.Text.Json.JsonEncodedText PropTotalIndexingBuffer = System.Text.Json.JsonEncodedText.Encode("total_indexing_buffer");
	private static readonly System.Text.Json.JsonEncodedText PropTotalIndexingBufferInBytes = System.Text.Json.JsonEncodedText.Encode("total_indexing_buffer_in_bytes");
	private static readonly System.Text.Json.JsonEncodedText PropTransport = System.Text.Json.JsonEncodedText.Encode("transport");
	private static readonly System.Text.Json.JsonEncodedText PropTransportAddress = System.Text.Json.JsonEncodedText.Encode("transport_address");
	private static readonly System.Text.Json.JsonEncodedText PropVersion = System.Text.Json.JsonEncodedText.Encode("version");

	public override Elastic.Clients.Elasticsearch.Nodes.NodeInfo Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<System.Collections.Generic.IReadOnlyDictionary<string, Elastic.Clients.Elasticsearch.Nodes.NodeInfoAggregation>?> propAggregations = default;
		LocalJsonValue<System.Collections.Generic.IReadOnlyDictionary<string, string>> propAttributes = default;
		LocalJsonValue<string> propBuildFlavor = default;
		LocalJsonValue<string> propBuildHash = default;
		LocalJsonValue<string> propBuildType = default;
		LocalJsonValue<string> propHost = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Nodes.NodeInfoHttp?> propHttp = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Nodes.NodeInfoIngest?> propIngest = default;
		LocalJsonValue<string> propIp = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Nodes.NodeJvmInfo?> propJvm = default;
		LocalJsonValue<System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.PluginStats>?> propModules = default;
		LocalJsonValue<string> propName = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Nodes.NodeInfoNetwork?> propNetwork = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Nodes.NodeOperatingSystemInfo?> propOs = default;
		LocalJsonValue<System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.PluginStats>?> propPlugins = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Nodes.NodeProcessInfo?> propProcess = default;
		LocalJsonValue<System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.NodeRole>> propRoles = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Nodes.NodeInfoSettings?> propSettings = default;
		LocalJsonValue<System.Collections.Generic.IReadOnlyDictionary<string, Elastic.Clients.Elasticsearch.Nodes.NodeThreadPoolInfo>?> propThreadPool = default;
		LocalJsonValue<long?> propTotalIndexingBuffer = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.ByteSize?> propTotalIndexingBufferInBytes = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Nodes.NodeInfoTransport?> propTransport = default;
		LocalJsonValue<string> propTransportAddress = default;
		LocalJsonValue<string> propVersion = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propAggregations.TryReadProperty(ref reader, options, PropAggregations, static System.Collections.Generic.IReadOnlyDictionary<string, Elastic.Clients.Elasticsearch.Nodes.NodeInfoAggregation>? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadDictionaryValue<string, Elastic.Clients.Elasticsearch.Nodes.NodeInfoAggregation>(o, null, null)))
			{
				continue;
			}

			if (propAttributes.TryReadProperty(ref reader, options, PropAttributes, static System.Collections.Generic.IReadOnlyDictionary<string, string> (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadDictionaryValue<string, string>(o, null, null)!))
			{
				continue;
			}

			if (propBuildFlavor.TryReadProperty(ref reader, options, PropBuildFlavor, null))
			{
				continue;
			}

			if (propBuildHash.TryReadProperty(ref reader, options, PropBuildHash, null))
			{
				continue;
			}

			if (propBuildType.TryReadProperty(ref reader, options, PropBuildType, null))
			{
				continue;
			}

			if (propHost.TryReadProperty(ref reader, options, PropHost, null))
			{
				continue;
			}

			if (propHttp.TryReadProperty(ref reader, options, PropHttp, null))
			{
				continue;
			}

			if (propIngest.TryReadProperty(ref reader, options, PropIngest, null))
			{
				continue;
			}

			if (propIp.TryReadProperty(ref reader, options, PropIp, null))
			{
				continue;
			}

			if (propJvm.TryReadProperty(ref reader, options, PropJvm, null))
			{
				continue;
			}

			if (propModules.TryReadProperty(ref reader, options, PropModules, static System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.PluginStats>? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadCollectionValue<Elastic.Clients.Elasticsearch.PluginStats>(o, null)))
			{
				continue;
			}

			if (propName.TryReadProperty(ref reader, options, PropName, null))
			{
				continue;
			}

			if (propNetwork.TryReadProperty(ref reader, options, PropNetwork, null))
			{
				continue;
			}

			if (propOs.TryReadProperty(ref reader, options, PropOs, null))
			{
				continue;
			}

			if (propPlugins.TryReadProperty(ref reader, options, PropPlugins, static System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.PluginStats>? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadCollectionValue<Elastic.Clients.Elasticsearch.PluginStats>(o, null)))
			{
				continue;
			}

			if (propProcess.TryReadProperty(ref reader, options, PropProcess, null))
			{
				continue;
			}

			if (propRoles.TryReadProperty(ref reader, options, PropRoles, static System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.NodeRole> (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadCollectionValue<Elastic.Clients.Elasticsearch.NodeRole>(o, null)!))
			{
				continue;
			}

			if (propSettings.TryReadProperty(ref reader, options, PropSettings, null))
			{
				continue;
			}

			if (propThreadPool.TryReadProperty(ref reader, options, PropThreadPool, static System.Collections.Generic.IReadOnlyDictionary<string, Elastic.Clients.Elasticsearch.Nodes.NodeThreadPoolInfo>? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadDictionaryValue<string, Elastic.Clients.Elasticsearch.Nodes.NodeThreadPoolInfo>(o, null, null)))
			{
				continue;
			}

			if (propTotalIndexingBuffer.TryReadProperty(ref reader, options, PropTotalIndexingBuffer, null))
			{
				continue;
			}

			if (propTotalIndexingBufferInBytes.TryReadProperty(ref reader, options, PropTotalIndexingBufferInBytes, null))
			{
				continue;
			}

			if (propTransport.TryReadProperty(ref reader, options, PropTransport, null))
			{
				continue;
			}

			if (propTransportAddress.TryReadProperty(ref reader, options, PropTransportAddress, null))
			{
				continue;
			}

			if (propVersion.TryReadProperty(ref reader, options, PropVersion, null))
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
		return new Elastic.Clients.Elasticsearch.Nodes.NodeInfo(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Aggregations = propAggregations.Value,
			Attributes = propAttributes.Value,
			BuildFlavor = propBuildFlavor.Value,
			BuildHash = propBuildHash.Value,
			BuildType = propBuildType.Value,
			Host = propHost.Value,
			Http = propHttp.Value,
			Ingest = propIngest.Value,
			Ip = propIp.Value,
			Jvm = propJvm.Value,
			Modules = propModules.Value,
			Name = propName.Value,
			Network = propNetwork.Value,
			Os = propOs.Value,
			Plugins = propPlugins.Value,
			Process = propProcess.Value,
			Roles = propRoles.Value,
			Settings = propSettings.Value,
			ThreadPool = propThreadPool.Value,
			TotalIndexingBuffer = propTotalIndexingBuffer.Value,
			TotalIndexingBufferInBytes = propTotalIndexingBufferInBytes.Value,
			Transport = propTransport.Value,
			TransportAddress = propTransportAddress.Value,
			Version = propVersion.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Nodes.NodeInfo value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropAggregations, value.Aggregations, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.IReadOnlyDictionary<string, Elastic.Clients.Elasticsearch.Nodes.NodeInfoAggregation>? v) => w.WriteDictionaryValue<string, Elastic.Clients.Elasticsearch.Nodes.NodeInfoAggregation>(o, v, null, null));
		writer.WriteProperty(options, PropAttributes, value.Attributes, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.IReadOnlyDictionary<string, string> v) => w.WriteDictionaryValue<string, string>(o, v, null, null));
		writer.WriteProperty(options, PropBuildFlavor, value.BuildFlavor, null, null);
		writer.WriteProperty(options, PropBuildHash, value.BuildHash, null, null);
		writer.WriteProperty(options, PropBuildType, value.BuildType, null, null);
		writer.WriteProperty(options, PropHost, value.Host, null, null);
		writer.WriteProperty(options, PropHttp, value.Http, null, null);
		writer.WriteProperty(options, PropIngest, value.Ingest, null, null);
		writer.WriteProperty(options, PropIp, value.Ip, null, null);
		writer.WriteProperty(options, PropJvm, value.Jvm, null, null);
		writer.WriteProperty(options, PropModules, value.Modules, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.PluginStats>? v) => w.WriteCollectionValue<Elastic.Clients.Elasticsearch.PluginStats>(o, v, null));
		writer.WriteProperty(options, PropName, value.Name, null, null);
		writer.WriteProperty(options, PropNetwork, value.Network, null, null);
		writer.WriteProperty(options, PropOs, value.Os, null, null);
		writer.WriteProperty(options, PropPlugins, value.Plugins, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.PluginStats>? v) => w.WriteCollectionValue<Elastic.Clients.Elasticsearch.PluginStats>(o, v, null));
		writer.WriteProperty(options, PropProcess, value.Process, null, null);
		writer.WriteProperty(options, PropRoles, value.Roles, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.NodeRole> v) => w.WriteCollectionValue<Elastic.Clients.Elasticsearch.NodeRole>(o, v, null));
		writer.WriteProperty(options, PropSettings, value.Settings, null, null);
		writer.WriteProperty(options, PropThreadPool, value.ThreadPool, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.IReadOnlyDictionary<string, Elastic.Clients.Elasticsearch.Nodes.NodeThreadPoolInfo>? v) => w.WriteDictionaryValue<string, Elastic.Clients.Elasticsearch.Nodes.NodeThreadPoolInfo>(o, v, null, null));
		writer.WriteProperty(options, PropTotalIndexingBuffer, value.TotalIndexingBuffer, null, null);
		writer.WriteProperty(options, PropTotalIndexingBufferInBytes, value.TotalIndexingBufferInBytes, null, null);
		writer.WriteProperty(options, PropTransport, value.Transport, null, null);
		writer.WriteProperty(options, PropTransportAddress, value.TransportAddress, null, null);
		writer.WriteProperty(options, PropVersion, value.Version, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Nodes.NodeInfoConverter))]
public sealed partial class NodeInfo
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public NodeInfo(System.Collections.Generic.IReadOnlyDictionary<string, string> attributes, string buildFlavor, string buildHash, string buildType, string host, string ip, string name, System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.NodeRole> roles, string transportAddress, string version)
	{
		Attributes = attributes;
		BuildFlavor = buildFlavor;
		BuildHash = buildHash;
		BuildType = buildType;
		Host = host;
		Ip = ip;
		Name = name;
		Roles = roles;
		TransportAddress = transportAddress;
		Version = version;
	}
#if NET7_0_OR_GREATER
	public NodeInfo()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public NodeInfo()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal NodeInfo(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	public System.Collections.Generic.IReadOnlyDictionary<string, Elastic.Clients.Elasticsearch.Nodes.NodeInfoAggregation>? Aggregations { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	System.Collections.Generic.IReadOnlyDictionary<string, string> Attributes { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	string BuildFlavor { get; set; }

	/// <summary>
	/// <para>
	/// Short hash of the last git commit in this release.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	string BuildHash { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	string BuildType { get; set; }

	/// <summary>
	/// <para>
	/// The node’s host name.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	string Host { get; set; }
	public Elastic.Clients.Elasticsearch.Nodes.NodeInfoHttp? Http { get; set; }
	public Elastic.Clients.Elasticsearch.Nodes.NodeInfoIngest? Ingest { get; set; }

	/// <summary>
	/// <para>
	/// The node’s IP address.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	string Ip { get; set; }
	public Elastic.Clients.Elasticsearch.Nodes.NodeJvmInfo? Jvm { get; set; }
	public System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.PluginStats>? Modules { get; set; }

	/// <summary>
	/// <para>
	/// The node's name
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	string Name { get; set; }
	public Elastic.Clients.Elasticsearch.Nodes.NodeInfoNetwork? Network { get; set; }
	public Elastic.Clients.Elasticsearch.Nodes.NodeOperatingSystemInfo? Os { get; set; }
	public System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.PluginStats>? Plugins { get; set; }
	public Elastic.Clients.Elasticsearch.Nodes.NodeProcessInfo? Process { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.NodeRole> Roles { get; set; }
	public Elastic.Clients.Elasticsearch.Nodes.NodeInfoSettings? Settings { get; set; }
	public System.Collections.Generic.IReadOnlyDictionary<string, Elastic.Clients.Elasticsearch.Nodes.NodeThreadPoolInfo>? ThreadPool { get; set; }

	/// <summary>
	/// <para>
	/// Total heap allowed to be used to hold recently indexed documents before they must be written to disk. This size is a shared pool across all shards on this node, and is controlled by Indexing Buffer settings.
	/// </para>
	/// </summary>
	public long? TotalIndexingBuffer { get; set; }

	/// <summary>
	/// <para>
	/// Same as total_indexing_buffer, but expressed in bytes.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.ByteSize? TotalIndexingBufferInBytes { get; set; }
	public Elastic.Clients.Elasticsearch.Nodes.NodeInfoTransport? Transport { get; set; }

	/// <summary>
	/// <para>
	/// Host and port where transport HTTP connections are accepted.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	string TransportAddress { get; set; }

	/// <summary>
	/// <para>
	/// Elasticsearch version running on this node.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	string Version { get; set; }
}