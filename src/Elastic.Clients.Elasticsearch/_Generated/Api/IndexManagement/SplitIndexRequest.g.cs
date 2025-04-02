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

namespace Elastic.Clients.Elasticsearch.IndexManagement;

public sealed partial class SplitIndexRequestParameters : Elastic.Transport.RequestParameters
{
	/// <summary>
	/// <para>
	/// Period to wait for a connection to the master node.
	/// If no response is received before the timeout expires, the request fails and returns an error.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Duration? MasterTimeout { get => Q<Elastic.Clients.Elasticsearch.Duration?>("master_timeout"); set => Q("master_timeout", value); }

	/// <summary>
	/// <para>
	/// Period to wait for a response.
	/// If no response is received before the timeout expires, the request fails and returns an error.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Duration? Timeout { get => Q<Elastic.Clients.Elasticsearch.Duration?>("timeout"); set => Q("timeout", value); }

	/// <summary>
	/// <para>
	/// The number of shard copies that must be active before proceeding with the operation.
	/// Set to <c>all</c> or any positive integer up to the total number of shards in the index (<c>number_of_replicas+1</c>).
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.WaitForActiveShards? WaitForActiveShards { get => Q<Elastic.Clients.Elasticsearch.WaitForActiveShards?>("wait_for_active_shards"); set => Q("wait_for_active_shards", value); }
}

internal sealed partial class SplitIndexRequestConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.IndexManagement.SplitIndexRequest>
{
	private static readonly System.Text.Json.JsonEncodedText PropAliases = System.Text.Json.JsonEncodedText.Encode("aliases");
	private static readonly System.Text.Json.JsonEncodedText PropSettings = System.Text.Json.JsonEncodedText.Encode("settings");

	public override Elastic.Clients.Elasticsearch.IndexManagement.SplitIndexRequest Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<System.Collections.Generic.IDictionary<Elastic.Clients.Elasticsearch.IndexName, Elastic.Clients.Elasticsearch.IndexManagement.Alias>?> propAliases = default;
		LocalJsonValue<System.Collections.Generic.IDictionary<string, object>?> propSettings = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propAliases.TryReadProperty(ref reader, options, PropAliases, static System.Collections.Generic.IDictionary<Elastic.Clients.Elasticsearch.IndexName, Elastic.Clients.Elasticsearch.IndexManagement.Alias>? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadDictionaryValue<Elastic.Clients.Elasticsearch.IndexName, Elastic.Clients.Elasticsearch.IndexManagement.Alias>(o, null, null)))
			{
				continue;
			}

			if (propSettings.TryReadProperty(ref reader, options, PropSettings, static System.Collections.Generic.IDictionary<string, object>? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadDictionaryValue<string, object>(o, null, null)))
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
		return new Elastic.Clients.Elasticsearch.IndexManagement.SplitIndexRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Aliases = propAliases.Value,
			Settings = propSettings.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.IndexManagement.SplitIndexRequest value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropAliases, value.Aliases, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.IDictionary<Elastic.Clients.Elasticsearch.IndexName, Elastic.Clients.Elasticsearch.IndexManagement.Alias>? v) => w.WriteDictionaryValue<Elastic.Clients.Elasticsearch.IndexName, Elastic.Clients.Elasticsearch.IndexManagement.Alias>(o, v, null, null));
		writer.WriteProperty(options, PropSettings, value.Settings, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.IDictionary<string, object>? v) => w.WriteDictionaryValue<string, object>(o, v, null, null));
		writer.WriteEndObject();
	}
}

/// <summary>
/// <para>
/// Split an index.
/// Split an index into a new index with more primary shards.
/// </para>
/// <list type="bullet">
/// <item>
/// <para>
/// Before you can split an index:
/// </para>
/// </item>
/// <item>
/// <para>
/// The index must be read-only.
/// </para>
/// </item>
/// <item>
/// <para>
/// The cluster health status must be green.
/// </para>
/// </item>
/// </list>
/// <para>
/// You can do make an index read-only with the following request using the add index block API:
/// </para>
/// <code>
/// PUT /my_source_index/_block/write
/// </code>
/// <para>
/// The current write index on a data stream cannot be split.
/// In order to split the current write index, the data stream must first be rolled over so that a new write index is created and then the previous write index can be split.
/// </para>
/// <para>
/// The number of times the index can be split (and the number of shards that each original shard can be split into) is determined by the <c>index.number_of_routing_shards</c> setting.
/// The number of routing shards specifies the hashing space that is used internally to distribute documents across shards with consistent hashing.
/// For instance, a 5 shard index with <c>number_of_routing_shards</c> set to 30 (5 x 2 x 3) could be split by a factor of 2 or 3.
/// </para>
/// <para>
/// A split operation:
/// </para>
/// <list type="bullet">
/// <item>
/// <para>
/// Creates a new target index with the same definition as the source index, but with a larger number of primary shards.
/// </para>
/// </item>
/// <item>
/// <para>
/// Hard-links segments from the source index into the target index. If the file system doesn't support hard-linking, all segments are copied into the new index, which is a much more time consuming process.
/// </para>
/// </item>
/// <item>
/// <para>
/// Hashes all documents again, after low level files are created, to delete documents that belong to a different shard.
/// </para>
/// </item>
/// <item>
/// <para>
/// Recovers the target index as though it were a closed index which had just been re-opened.
/// </para>
/// </item>
/// </list>
/// <para>
/// IMPORTANT: Indices can only be split if they satisfy the following requirements:
/// </para>
/// <list type="bullet">
/// <item>
/// <para>
/// The target index must not exist.
/// </para>
/// </item>
/// <item>
/// <para>
/// The source index must have fewer primary shards than the target index.
/// </para>
/// </item>
/// <item>
/// <para>
/// The number of primary shards in the target index must be a multiple of the number of primary shards in the source index.
/// </para>
/// </item>
/// <item>
/// <para>
/// The node handling the split process must have sufficient free disk space to accommodate a second copy of the existing index.
/// </para>
/// </item>
/// </list>
/// </summary>
[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.IndexManagement.SplitIndexRequestConverter))]
public sealed partial class SplitIndexRequest : Elastic.Clients.Elasticsearch.Requests.PlainRequest<Elastic.Clients.Elasticsearch.IndexManagement.SplitIndexRequestParameters>
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public SplitIndexRequest(Elastic.Clients.Elasticsearch.IndexName index, Elastic.Clients.Elasticsearch.IndexName target) : base(r => r.Required("index", index).Required("target", target))
	{
	}
#if NET7_0_OR_GREATER
	public SplitIndexRequest()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal SplitIndexRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	internal override Elastic.Clients.Elasticsearch.Requests.ApiUrls ApiUrls => Elastic.Clients.Elasticsearch.Requests.ApiUrlLookup.IndexManagementSplit;

	protected override Elastic.Transport.HttpMethod StaticHttpMethod => Elastic.Transport.HttpMethod.PUT;

	internal override bool SupportsBody => true;

	internal override string OperationName => "indices.split";

	/// <summary>
	/// <para>
	/// Name of the source index to split.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.IndexName Index { get => P<Elastic.Clients.Elasticsearch.IndexName>("index"); set => PR("index", value); }

	/// <summary>
	/// <para>
	/// Name of the target index to create.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.IndexName Target { get => P<Elastic.Clients.Elasticsearch.IndexName>("target"); set => PR("target", value); }

	/// <summary>
	/// <para>
	/// Period to wait for a connection to the master node.
	/// If no response is received before the timeout expires, the request fails and returns an error.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Duration? MasterTimeout { get => Q<Elastic.Clients.Elasticsearch.Duration?>("master_timeout"); set => Q("master_timeout", value); }

	/// <summary>
	/// <para>
	/// Period to wait for a response.
	/// If no response is received before the timeout expires, the request fails and returns an error.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Duration? Timeout { get => Q<Elastic.Clients.Elasticsearch.Duration?>("timeout"); set => Q("timeout", value); }

	/// <summary>
	/// <para>
	/// The number of shard copies that must be active before proceeding with the operation.
	/// Set to <c>all</c> or any positive integer up to the total number of shards in the index (<c>number_of_replicas+1</c>).
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.WaitForActiveShards? WaitForActiveShards { get => Q<Elastic.Clients.Elasticsearch.WaitForActiveShards?>("wait_for_active_shards"); set => Q("wait_for_active_shards", value); }

	/// <summary>
	/// <para>
	/// Aliases for the resulting index.
	/// </para>
	/// </summary>
	public System.Collections.Generic.IDictionary<Elastic.Clients.Elasticsearch.IndexName, Elastic.Clients.Elasticsearch.IndexManagement.Alias>? Aliases { get; set; }

	/// <summary>
	/// <para>
	/// Configuration options for the target index.
	/// </para>
	/// </summary>
	public System.Collections.Generic.IDictionary<string, object>? Settings { get; set; }
}

/// <summary>
/// <para>
/// Split an index.
/// Split an index into a new index with more primary shards.
/// </para>
/// <list type="bullet">
/// <item>
/// <para>
/// Before you can split an index:
/// </para>
/// </item>
/// <item>
/// <para>
/// The index must be read-only.
/// </para>
/// </item>
/// <item>
/// <para>
/// The cluster health status must be green.
/// </para>
/// </item>
/// </list>
/// <para>
/// You can do make an index read-only with the following request using the add index block API:
/// </para>
/// <code>
/// PUT /my_source_index/_block/write
/// </code>
/// <para>
/// The current write index on a data stream cannot be split.
/// In order to split the current write index, the data stream must first be rolled over so that a new write index is created and then the previous write index can be split.
/// </para>
/// <para>
/// The number of times the index can be split (and the number of shards that each original shard can be split into) is determined by the <c>index.number_of_routing_shards</c> setting.
/// The number of routing shards specifies the hashing space that is used internally to distribute documents across shards with consistent hashing.
/// For instance, a 5 shard index with <c>number_of_routing_shards</c> set to 30 (5 x 2 x 3) could be split by a factor of 2 or 3.
/// </para>
/// <para>
/// A split operation:
/// </para>
/// <list type="bullet">
/// <item>
/// <para>
/// Creates a new target index with the same definition as the source index, but with a larger number of primary shards.
/// </para>
/// </item>
/// <item>
/// <para>
/// Hard-links segments from the source index into the target index. If the file system doesn't support hard-linking, all segments are copied into the new index, which is a much more time consuming process.
/// </para>
/// </item>
/// <item>
/// <para>
/// Hashes all documents again, after low level files are created, to delete documents that belong to a different shard.
/// </para>
/// </item>
/// <item>
/// <para>
/// Recovers the target index as though it were a closed index which had just been re-opened.
/// </para>
/// </item>
/// </list>
/// <para>
/// IMPORTANT: Indices can only be split if they satisfy the following requirements:
/// </para>
/// <list type="bullet">
/// <item>
/// <para>
/// The target index must not exist.
/// </para>
/// </item>
/// <item>
/// <para>
/// The source index must have fewer primary shards than the target index.
/// </para>
/// </item>
/// <item>
/// <para>
/// The number of primary shards in the target index must be a multiple of the number of primary shards in the source index.
/// </para>
/// </item>
/// <item>
/// <para>
/// The node handling the split process must have sufficient free disk space to accommodate a second copy of the existing index.
/// </para>
/// </item>
/// </list>
/// </summary>
public readonly partial struct SplitIndexRequestDescriptor
{
	internal Elastic.Clients.Elasticsearch.IndexManagement.SplitIndexRequest Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public SplitIndexRequestDescriptor(Elastic.Clients.Elasticsearch.IndexManagement.SplitIndexRequest instance)
	{
		Instance = instance;
	}

	public SplitIndexRequestDescriptor(Elastic.Clients.Elasticsearch.IndexName index, Elastic.Clients.Elasticsearch.IndexName target)
	{
		Instance = new Elastic.Clients.Elasticsearch.IndexManagement.SplitIndexRequest(index, target);
	}

	[System.Obsolete("The type contains additional required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public SplitIndexRequestDescriptor()
	{
		throw new System.InvalidOperationException("The use of the parameterless constructor is not permitted for this type.");
	}

	public static explicit operator Elastic.Clients.Elasticsearch.IndexManagement.SplitIndexRequestDescriptor(Elastic.Clients.Elasticsearch.IndexManagement.SplitIndexRequest instance) => new Elastic.Clients.Elasticsearch.IndexManagement.SplitIndexRequestDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.IndexManagement.SplitIndexRequest(Elastic.Clients.Elasticsearch.IndexManagement.SplitIndexRequestDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// Name of the source index to split.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndexManagement.SplitIndexRequestDescriptor Index(Elastic.Clients.Elasticsearch.IndexName value)
	{
		Instance.Index = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Name of the target index to create.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndexManagement.SplitIndexRequestDescriptor Target(Elastic.Clients.Elasticsearch.IndexName value)
	{
		Instance.Target = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Period to wait for a connection to the master node.
	/// If no response is received before the timeout expires, the request fails and returns an error.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndexManagement.SplitIndexRequestDescriptor MasterTimeout(Elastic.Clients.Elasticsearch.Duration? value)
	{
		Instance.MasterTimeout = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Period to wait for a response.
	/// If no response is received before the timeout expires, the request fails and returns an error.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndexManagement.SplitIndexRequestDescriptor Timeout(Elastic.Clients.Elasticsearch.Duration? value)
	{
		Instance.Timeout = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The number of shard copies that must be active before proceeding with the operation.
	/// Set to <c>all</c> or any positive integer up to the total number of shards in the index (<c>number_of_replicas+1</c>).
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndexManagement.SplitIndexRequestDescriptor WaitForActiveShards(Elastic.Clients.Elasticsearch.WaitForActiveShards? value)
	{
		Instance.WaitForActiveShards = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Aliases for the resulting index.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndexManagement.SplitIndexRequestDescriptor Aliases(System.Collections.Generic.IDictionary<Elastic.Clients.Elasticsearch.IndexName, Elastic.Clients.Elasticsearch.IndexManagement.Alias>? value)
	{
		Instance.Aliases = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Aliases for the resulting index.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndexManagement.SplitIndexRequestDescriptor Aliases()
	{
		Instance.Aliases = Elastic.Clients.Elasticsearch.Fluent.FluentIDictionaryOfIndexNameAlias.Build(null);
		return this;
	}

	/// <summary>
	/// <para>
	/// Aliases for the resulting index.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndexManagement.SplitIndexRequestDescriptor Aliases(System.Action<Elastic.Clients.Elasticsearch.Fluent.FluentIDictionaryOfIndexNameAlias>? action)
	{
		Instance.Aliases = Elastic.Clients.Elasticsearch.Fluent.FluentIDictionaryOfIndexNameAlias.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// Aliases for the resulting index.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndexManagement.SplitIndexRequestDescriptor Aliases<T>(System.Action<Elastic.Clients.Elasticsearch.Fluent.FluentIDictionaryOfIndexNameAlias<T>>? action)
	{
		Instance.Aliases = Elastic.Clients.Elasticsearch.Fluent.FluentIDictionaryOfIndexNameAlias<T>.Build(action);
		return this;
	}

	public Elastic.Clients.Elasticsearch.IndexManagement.SplitIndexRequestDescriptor AddAlias(Elastic.Clients.Elasticsearch.IndexName key, Elastic.Clients.Elasticsearch.IndexManagement.Alias value)
	{
		Instance.Aliases ??= new System.Collections.Generic.Dictionary<Elastic.Clients.Elasticsearch.IndexName, Elastic.Clients.Elasticsearch.IndexManagement.Alias>();
		Instance.Aliases.Add(key, value);
		return this;
	}

	/// <summary>
	/// <para>
	/// Aliases for the resulting index.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndexManagement.SplitIndexRequestDescriptor Aliases(Elastic.Clients.Elasticsearch.IndexName key)
	{
		Instance.Aliases = new System.Collections.Generic.Dictionary<Elastic.Clients.Elasticsearch.IndexName, Elastic.Clients.Elasticsearch.IndexManagement.Alias> { { key, Elastic.Clients.Elasticsearch.IndexManagement.AliasDescriptor.Build(null) } };
		return this;
	}

	/// <summary>
	/// <para>
	/// Aliases for the resulting index.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndexManagement.SplitIndexRequestDescriptor Aliases(params Elastic.Clients.Elasticsearch.IndexName[] keys)
	{
		var items = new System.Collections.Generic.Dictionary<Elastic.Clients.Elasticsearch.IndexName, Elastic.Clients.Elasticsearch.IndexManagement.Alias>();
		foreach (var key in keys)
		{
			items.Add(key, Elastic.Clients.Elasticsearch.IndexManagement.AliasDescriptor.Build(null));
		}

		Instance.Aliases = items;
		return this;
	}

	public Elastic.Clients.Elasticsearch.IndexManagement.SplitIndexRequestDescriptor AddAlias(Elastic.Clients.Elasticsearch.IndexName key)
	{
		Instance.Aliases ??= new System.Collections.Generic.Dictionary<Elastic.Clients.Elasticsearch.IndexName, Elastic.Clients.Elasticsearch.IndexManagement.Alias>();
		Instance.Aliases.Add(key, Elastic.Clients.Elasticsearch.IndexManagement.AliasDescriptor.Build(null));
		return this;
	}

	public Elastic.Clients.Elasticsearch.IndexManagement.SplitIndexRequestDescriptor AddAlias(Elastic.Clients.Elasticsearch.IndexName key, System.Action<Elastic.Clients.Elasticsearch.IndexManagement.AliasDescriptor>? action)
	{
		Instance.Aliases ??= new System.Collections.Generic.Dictionary<Elastic.Clients.Elasticsearch.IndexName, Elastic.Clients.Elasticsearch.IndexManagement.Alias>();
		Instance.Aliases.Add(key, Elastic.Clients.Elasticsearch.IndexManagement.AliasDescriptor.Build(action));
		return this;
	}

	public Elastic.Clients.Elasticsearch.IndexManagement.SplitIndexRequestDescriptor AddAlias<T>(Elastic.Clients.Elasticsearch.IndexName key, System.Action<Elastic.Clients.Elasticsearch.IndexManagement.AliasDescriptor<T>>? action)
	{
		Instance.Aliases ??= new System.Collections.Generic.Dictionary<Elastic.Clients.Elasticsearch.IndexName, Elastic.Clients.Elasticsearch.IndexManagement.Alias>();
		Instance.Aliases.Add(key, Elastic.Clients.Elasticsearch.IndexManagement.AliasDescriptor<T>.Build(action));
		return this;
	}

	/// <summary>
	/// <para>
	/// Configuration options for the target index.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndexManagement.SplitIndexRequestDescriptor Settings(System.Collections.Generic.IDictionary<string, object>? value)
	{
		Instance.Settings = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Configuration options for the target index.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndexManagement.SplitIndexRequestDescriptor Settings()
	{
		Instance.Settings = Elastic.Clients.Elasticsearch.Fluent.FluentIDictionaryOfStringObject.Build(null);
		return this;
	}

	/// <summary>
	/// <para>
	/// Configuration options for the target index.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndexManagement.SplitIndexRequestDescriptor Settings(System.Action<Elastic.Clients.Elasticsearch.Fluent.FluentIDictionaryOfStringObject>? action)
	{
		Instance.Settings = Elastic.Clients.Elasticsearch.Fluent.FluentIDictionaryOfStringObject.Build(action);
		return this;
	}

	public Elastic.Clients.Elasticsearch.IndexManagement.SplitIndexRequestDescriptor AddSetting(string key, object value)
	{
		Instance.Settings ??= new System.Collections.Generic.Dictionary<string, object>();
		Instance.Settings.Add(key, value);
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.IndexManagement.SplitIndexRequest Build(System.Action<Elastic.Clients.Elasticsearch.IndexManagement.SplitIndexRequestDescriptor> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.IndexManagement.SplitIndexRequestDescriptor(new Elastic.Clients.Elasticsearch.IndexManagement.SplitIndexRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}

	public Elastic.Clients.Elasticsearch.IndexManagement.SplitIndexRequestDescriptor ErrorTrace(bool? value)
	{
		Instance.ErrorTrace = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.IndexManagement.SplitIndexRequestDescriptor FilterPath(params string[]? value)
	{
		Instance.FilterPath = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.IndexManagement.SplitIndexRequestDescriptor Human(bool? value)
	{
		Instance.Human = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.IndexManagement.SplitIndexRequestDescriptor Pretty(bool? value)
	{
		Instance.Pretty = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.IndexManagement.SplitIndexRequestDescriptor SourceQueryString(string? value)
	{
		Instance.SourceQueryString = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.IndexManagement.SplitIndexRequestDescriptor RequestConfiguration(Elastic.Transport.IRequestConfiguration? value)
	{
		Instance.RequestConfiguration = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.IndexManagement.SplitIndexRequestDescriptor RequestConfiguration(System.Func<Elastic.Transport.RequestConfigurationDescriptor, Elastic.Transport.IRequestConfiguration>? configurationSelector)
	{
		Instance.RequestConfiguration = configurationSelector.Invoke(Instance.RequestConfiguration is null ? new Elastic.Transport.RequestConfigurationDescriptor() : new Elastic.Transport.RequestConfigurationDescriptor(Instance.RequestConfiguration)) ?? Instance.RequestConfiguration;
		return this;
	}
}

/// <summary>
/// <para>
/// Split an index.
/// Split an index into a new index with more primary shards.
/// </para>
/// <list type="bullet">
/// <item>
/// <para>
/// Before you can split an index:
/// </para>
/// </item>
/// <item>
/// <para>
/// The index must be read-only.
/// </para>
/// </item>
/// <item>
/// <para>
/// The cluster health status must be green.
/// </para>
/// </item>
/// </list>
/// <para>
/// You can do make an index read-only with the following request using the add index block API:
/// </para>
/// <code>
/// PUT /my_source_index/_block/write
/// </code>
/// <para>
/// The current write index on a data stream cannot be split.
/// In order to split the current write index, the data stream must first be rolled over so that a new write index is created and then the previous write index can be split.
/// </para>
/// <para>
/// The number of times the index can be split (and the number of shards that each original shard can be split into) is determined by the <c>index.number_of_routing_shards</c> setting.
/// The number of routing shards specifies the hashing space that is used internally to distribute documents across shards with consistent hashing.
/// For instance, a 5 shard index with <c>number_of_routing_shards</c> set to 30 (5 x 2 x 3) could be split by a factor of 2 or 3.
/// </para>
/// <para>
/// A split operation:
/// </para>
/// <list type="bullet">
/// <item>
/// <para>
/// Creates a new target index with the same definition as the source index, but with a larger number of primary shards.
/// </para>
/// </item>
/// <item>
/// <para>
/// Hard-links segments from the source index into the target index. If the file system doesn't support hard-linking, all segments are copied into the new index, which is a much more time consuming process.
/// </para>
/// </item>
/// <item>
/// <para>
/// Hashes all documents again, after low level files are created, to delete documents that belong to a different shard.
/// </para>
/// </item>
/// <item>
/// <para>
/// Recovers the target index as though it were a closed index which had just been re-opened.
/// </para>
/// </item>
/// </list>
/// <para>
/// IMPORTANT: Indices can only be split if they satisfy the following requirements:
/// </para>
/// <list type="bullet">
/// <item>
/// <para>
/// The target index must not exist.
/// </para>
/// </item>
/// <item>
/// <para>
/// The source index must have fewer primary shards than the target index.
/// </para>
/// </item>
/// <item>
/// <para>
/// The number of primary shards in the target index must be a multiple of the number of primary shards in the source index.
/// </para>
/// </item>
/// <item>
/// <para>
/// The node handling the split process must have sufficient free disk space to accommodate a second copy of the existing index.
/// </para>
/// </item>
/// </list>
/// </summary>
public readonly partial struct SplitIndexRequestDescriptor<TDocument>
{
	internal Elastic.Clients.Elasticsearch.IndexManagement.SplitIndexRequest Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public SplitIndexRequestDescriptor(Elastic.Clients.Elasticsearch.IndexManagement.SplitIndexRequest instance)
	{
		Instance = instance;
	}

	public SplitIndexRequestDescriptor(Elastic.Clients.Elasticsearch.IndexName index, Elastic.Clients.Elasticsearch.IndexName target)
	{
		Instance = new Elastic.Clients.Elasticsearch.IndexManagement.SplitIndexRequest(index, target);
	}

	[System.Obsolete("The type contains additional required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public SplitIndexRequestDescriptor()
	{
		throw new System.InvalidOperationException("The use of the parameterless constructor is not permitted for this type.");
	}

	public static explicit operator Elastic.Clients.Elasticsearch.IndexManagement.SplitIndexRequestDescriptor<TDocument>(Elastic.Clients.Elasticsearch.IndexManagement.SplitIndexRequest instance) => new Elastic.Clients.Elasticsearch.IndexManagement.SplitIndexRequestDescriptor<TDocument>(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.IndexManagement.SplitIndexRequest(Elastic.Clients.Elasticsearch.IndexManagement.SplitIndexRequestDescriptor<TDocument> descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// Name of the source index to split.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndexManagement.SplitIndexRequestDescriptor<TDocument> Index(Elastic.Clients.Elasticsearch.IndexName value)
	{
		Instance.Index = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Name of the target index to create.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndexManagement.SplitIndexRequestDescriptor<TDocument> Target(Elastic.Clients.Elasticsearch.IndexName value)
	{
		Instance.Target = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Period to wait for a connection to the master node.
	/// If no response is received before the timeout expires, the request fails and returns an error.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndexManagement.SplitIndexRequestDescriptor<TDocument> MasterTimeout(Elastic.Clients.Elasticsearch.Duration? value)
	{
		Instance.MasterTimeout = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Period to wait for a response.
	/// If no response is received before the timeout expires, the request fails and returns an error.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndexManagement.SplitIndexRequestDescriptor<TDocument> Timeout(Elastic.Clients.Elasticsearch.Duration? value)
	{
		Instance.Timeout = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The number of shard copies that must be active before proceeding with the operation.
	/// Set to <c>all</c> or any positive integer up to the total number of shards in the index (<c>number_of_replicas+1</c>).
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndexManagement.SplitIndexRequestDescriptor<TDocument> WaitForActiveShards(Elastic.Clients.Elasticsearch.WaitForActiveShards? value)
	{
		Instance.WaitForActiveShards = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Aliases for the resulting index.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndexManagement.SplitIndexRequestDescriptor<TDocument> Aliases(System.Collections.Generic.IDictionary<Elastic.Clients.Elasticsearch.IndexName, Elastic.Clients.Elasticsearch.IndexManagement.Alias>? value)
	{
		Instance.Aliases = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Aliases for the resulting index.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndexManagement.SplitIndexRequestDescriptor<TDocument> Aliases()
	{
		Instance.Aliases = Elastic.Clients.Elasticsearch.Fluent.FluentIDictionaryOfIndexNameAlias<TDocument>.Build(null);
		return this;
	}

	/// <summary>
	/// <para>
	/// Aliases for the resulting index.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndexManagement.SplitIndexRequestDescriptor<TDocument> Aliases(System.Action<Elastic.Clients.Elasticsearch.Fluent.FluentIDictionaryOfIndexNameAlias<TDocument>>? action)
	{
		Instance.Aliases = Elastic.Clients.Elasticsearch.Fluent.FluentIDictionaryOfIndexNameAlias<TDocument>.Build(action);
		return this;
	}

	public Elastic.Clients.Elasticsearch.IndexManagement.SplitIndexRequestDescriptor<TDocument> AddAlias(Elastic.Clients.Elasticsearch.IndexName key, Elastic.Clients.Elasticsearch.IndexManagement.Alias value)
	{
		Instance.Aliases ??= new System.Collections.Generic.Dictionary<Elastic.Clients.Elasticsearch.IndexName, Elastic.Clients.Elasticsearch.IndexManagement.Alias>();
		Instance.Aliases.Add(key, value);
		return this;
	}

	/// <summary>
	/// <para>
	/// Aliases for the resulting index.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndexManagement.SplitIndexRequestDescriptor<TDocument> Aliases(Elastic.Clients.Elasticsearch.IndexName key)
	{
		Instance.Aliases = new System.Collections.Generic.Dictionary<Elastic.Clients.Elasticsearch.IndexName, Elastic.Clients.Elasticsearch.IndexManagement.Alias> { { key, Elastic.Clients.Elasticsearch.IndexManagement.AliasDescriptor<TDocument>.Build(null) } };
		return this;
	}

	/// <summary>
	/// <para>
	/// Aliases for the resulting index.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndexManagement.SplitIndexRequestDescriptor<TDocument> Aliases(params Elastic.Clients.Elasticsearch.IndexName[] keys)
	{
		var items = new System.Collections.Generic.Dictionary<Elastic.Clients.Elasticsearch.IndexName, Elastic.Clients.Elasticsearch.IndexManagement.Alias>();
		foreach (var key in keys)
		{
			items.Add(key, Elastic.Clients.Elasticsearch.IndexManagement.AliasDescriptor<TDocument>.Build(null));
		}

		Instance.Aliases = items;
		return this;
	}

	public Elastic.Clients.Elasticsearch.IndexManagement.SplitIndexRequestDescriptor<TDocument> AddAlias(Elastic.Clients.Elasticsearch.IndexName key)
	{
		Instance.Aliases ??= new System.Collections.Generic.Dictionary<Elastic.Clients.Elasticsearch.IndexName, Elastic.Clients.Elasticsearch.IndexManagement.Alias>();
		Instance.Aliases.Add(key, Elastic.Clients.Elasticsearch.IndexManagement.AliasDescriptor<TDocument>.Build(null));
		return this;
	}

	public Elastic.Clients.Elasticsearch.IndexManagement.SplitIndexRequestDescriptor<TDocument> AddAlias(Elastic.Clients.Elasticsearch.IndexName key, System.Action<Elastic.Clients.Elasticsearch.IndexManagement.AliasDescriptor<TDocument>>? action)
	{
		Instance.Aliases ??= new System.Collections.Generic.Dictionary<Elastic.Clients.Elasticsearch.IndexName, Elastic.Clients.Elasticsearch.IndexManagement.Alias>();
		Instance.Aliases.Add(key, Elastic.Clients.Elasticsearch.IndexManagement.AliasDescriptor<TDocument>.Build(action));
		return this;
	}

	/// <summary>
	/// <para>
	/// Configuration options for the target index.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndexManagement.SplitIndexRequestDescriptor<TDocument> Settings(System.Collections.Generic.IDictionary<string, object>? value)
	{
		Instance.Settings = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Configuration options for the target index.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndexManagement.SplitIndexRequestDescriptor<TDocument> Settings()
	{
		Instance.Settings = Elastic.Clients.Elasticsearch.Fluent.FluentIDictionaryOfStringObject.Build(null);
		return this;
	}

	/// <summary>
	/// <para>
	/// Configuration options for the target index.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndexManagement.SplitIndexRequestDescriptor<TDocument> Settings(System.Action<Elastic.Clients.Elasticsearch.Fluent.FluentIDictionaryOfStringObject>? action)
	{
		Instance.Settings = Elastic.Clients.Elasticsearch.Fluent.FluentIDictionaryOfStringObject.Build(action);
		return this;
	}

	public Elastic.Clients.Elasticsearch.IndexManagement.SplitIndexRequestDescriptor<TDocument> AddSetting(string key, object value)
	{
		Instance.Settings ??= new System.Collections.Generic.Dictionary<string, object>();
		Instance.Settings.Add(key, value);
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.IndexManagement.SplitIndexRequest Build(System.Action<Elastic.Clients.Elasticsearch.IndexManagement.SplitIndexRequestDescriptor<TDocument>> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.IndexManagement.SplitIndexRequestDescriptor<TDocument>(new Elastic.Clients.Elasticsearch.IndexManagement.SplitIndexRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}

	public Elastic.Clients.Elasticsearch.IndexManagement.SplitIndexRequestDescriptor<TDocument> ErrorTrace(bool? value)
	{
		Instance.ErrorTrace = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.IndexManagement.SplitIndexRequestDescriptor<TDocument> FilterPath(params string[]? value)
	{
		Instance.FilterPath = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.IndexManagement.SplitIndexRequestDescriptor<TDocument> Human(bool? value)
	{
		Instance.Human = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.IndexManagement.SplitIndexRequestDescriptor<TDocument> Pretty(bool? value)
	{
		Instance.Pretty = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.IndexManagement.SplitIndexRequestDescriptor<TDocument> SourceQueryString(string? value)
	{
		Instance.SourceQueryString = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.IndexManagement.SplitIndexRequestDescriptor<TDocument> RequestConfiguration(Elastic.Transport.IRequestConfiguration? value)
	{
		Instance.RequestConfiguration = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.IndexManagement.SplitIndexRequestDescriptor<TDocument> RequestConfiguration(System.Func<Elastic.Transport.RequestConfigurationDescriptor, Elastic.Transport.IRequestConfiguration>? configurationSelector)
	{
		Instance.RequestConfiguration = configurationSelector.Invoke(Instance.RequestConfiguration is null ? new Elastic.Transport.RequestConfigurationDescriptor() : new Elastic.Transport.RequestConfigurationDescriptor(Instance.RequestConfiguration)) ?? Instance.RequestConfiguration;
		return this;
	}
}