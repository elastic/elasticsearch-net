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

public sealed partial class ShardStoresRequestParameters : Elastic.Transport.RequestParameters
{
	/// <summary>
	/// <para>
	/// If false, the request returns an error if any wildcard expression, index alias, or _all
	/// value targets only missing or closed indices. This behavior applies even if the request
	/// targets other open indices.
	/// </para>
	/// </summary>
	public bool? AllowNoIndices { get => Q<bool?>("allow_no_indices"); set => Q("allow_no_indices", value); }

	/// <summary>
	/// <para>
	/// Type of index that wildcard patterns can match. If the request can target data streams,
	/// this argument determines whether wildcard expressions match hidden data streams.
	/// </para>
	/// </summary>
	public System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.ExpandWildcard>? ExpandWildcards { get => Q<System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.ExpandWildcard>?>("expand_wildcards"); set => Q("expand_wildcards", value); }

	/// <summary>
	/// <para>
	/// If true, missing or closed indices are not included in the response.
	/// </para>
	/// </summary>
	public bool? IgnoreUnavailable { get => Q<bool?>("ignore_unavailable"); set => Q("ignore_unavailable", value); }

	/// <summary>
	/// <para>
	/// List of shard health statuses used to limit the request.
	/// </para>
	/// </summary>
	public System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.IndexManagement.ShardStoreStatus>? Status { get => Q<System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.IndexManagement.ShardStoreStatus>?>("status"); set => Q("status", value); }
}

internal sealed partial class ShardStoresRequestConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.IndexManagement.ShardStoresRequest>
{
	public override Elastic.Clients.Elasticsearch.IndexManagement.ShardStoresRequest Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (options.UnmappedMemberHandling is System.Text.Json.Serialization.JsonUnmappedMemberHandling.Skip)
			{
				reader.Skip();
				continue;
			}

			throw new System.Text.Json.JsonException($"Unknown JSON property '{reader.GetString()}' for type '{typeToConvert.Name}'.");
		}

		reader.ValidateToken(System.Text.Json.JsonTokenType.EndObject);
		return new Elastic.Clients.Elasticsearch.IndexManagement.ShardStoresRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.IndexManagement.ShardStoresRequest value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteEndObject();
	}
}

/// <summary>
/// <para>
/// Get index shard stores.
/// Get store information about replica shards in one or more indices.
/// For data streams, the API retrieves store information for the stream's backing indices.
/// </para>
/// <para>
/// The index shard stores API returns the following information:
/// </para>
/// <list type="bullet">
/// <item>
/// <para>
/// The node on which each replica shard exists.
/// </para>
/// </item>
/// <item>
/// <para>
/// The allocation ID for each replica shard.
/// </para>
/// </item>
/// <item>
/// <para>
/// A unique ID for each replica shard.
/// </para>
/// </item>
/// <item>
/// <para>
/// Any errors encountered while opening the shard index or from an earlier failure.
/// </para>
/// </item>
/// </list>
/// <para>
/// By default, the API returns store information only for primary shards that are unassigned or have one or more unassigned replica shards.
/// </para>
/// </summary>
[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.IndexManagement.ShardStoresRequestConverter))]
public sealed partial class ShardStoresRequest : Elastic.Clients.Elasticsearch.Requests.PlainRequest<Elastic.Clients.Elasticsearch.IndexManagement.ShardStoresRequestParameters>
{
	public ShardStoresRequest(Elastic.Clients.Elasticsearch.Indices? indices) : base(r => r.Optional("index", indices))
	{
	}
#if NET7_0_OR_GREATER
	public ShardStoresRequest()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	public ShardStoresRequest()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal ShardStoresRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	internal override Elastic.Clients.Elasticsearch.Requests.ApiUrls ApiUrls => Elastic.Clients.Elasticsearch.Requests.ApiUrlLookup.IndexManagementShardStores;

	protected override Elastic.Transport.HttpMethod StaticHttpMethod => Elastic.Transport.HttpMethod.GET;

	internal override bool SupportsBody => false;

	internal override string OperationName => "indices.shard_stores";

	/// <summary>
	/// <para>
	/// List of data streams, indices, and aliases used to limit the request.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Indices? Indices { get => P<Elastic.Clients.Elasticsearch.Indices?>("index"); set => PO("index", value); }

	/// <summary>
	/// <para>
	/// If false, the request returns an error if any wildcard expression, index alias, or _all
	/// value targets only missing or closed indices. This behavior applies even if the request
	/// targets other open indices.
	/// </para>
	/// </summary>
	public bool? AllowNoIndices { get => Q<bool?>("allow_no_indices"); set => Q("allow_no_indices", value); }

	/// <summary>
	/// <para>
	/// Type of index that wildcard patterns can match. If the request can target data streams,
	/// this argument determines whether wildcard expressions match hidden data streams.
	/// </para>
	/// </summary>
	public System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.ExpandWildcard>? ExpandWildcards { get => Q<System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.ExpandWildcard>?>("expand_wildcards"); set => Q("expand_wildcards", value); }

	/// <summary>
	/// <para>
	/// If true, missing or closed indices are not included in the response.
	/// </para>
	/// </summary>
	public bool? IgnoreUnavailable { get => Q<bool?>("ignore_unavailable"); set => Q("ignore_unavailable", value); }

	/// <summary>
	/// <para>
	/// List of shard health statuses used to limit the request.
	/// </para>
	/// </summary>
	public System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.IndexManagement.ShardStoreStatus>? Status { get => Q<System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.IndexManagement.ShardStoreStatus>?>("status"); set => Q("status", value); }
}

/// <summary>
/// <para>
/// Get index shard stores.
/// Get store information about replica shards in one or more indices.
/// For data streams, the API retrieves store information for the stream's backing indices.
/// </para>
/// <para>
/// The index shard stores API returns the following information:
/// </para>
/// <list type="bullet">
/// <item>
/// <para>
/// The node on which each replica shard exists.
/// </para>
/// </item>
/// <item>
/// <para>
/// The allocation ID for each replica shard.
/// </para>
/// </item>
/// <item>
/// <para>
/// A unique ID for each replica shard.
/// </para>
/// </item>
/// <item>
/// <para>
/// Any errors encountered while opening the shard index or from an earlier failure.
/// </para>
/// </item>
/// </list>
/// <para>
/// By default, the API returns store information only for primary shards that are unassigned or have one or more unassigned replica shards.
/// </para>
/// </summary>
public readonly partial struct ShardStoresRequestDescriptor
{
	internal Elastic.Clients.Elasticsearch.IndexManagement.ShardStoresRequest Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public ShardStoresRequestDescriptor(Elastic.Clients.Elasticsearch.IndexManagement.ShardStoresRequest instance)
	{
		Instance = instance;
	}

	public ShardStoresRequestDescriptor(Elastic.Clients.Elasticsearch.Indices? indices)
	{
		Instance = new Elastic.Clients.Elasticsearch.IndexManagement.ShardStoresRequest(indices);
	}

	public ShardStoresRequestDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.IndexManagement.ShardStoresRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.IndexManagement.ShardStoresRequestDescriptor(Elastic.Clients.Elasticsearch.IndexManagement.ShardStoresRequest instance) => new Elastic.Clients.Elasticsearch.IndexManagement.ShardStoresRequestDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.IndexManagement.ShardStoresRequest(Elastic.Clients.Elasticsearch.IndexManagement.ShardStoresRequestDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// List of data streams, indices, and aliases used to limit the request.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndexManagement.ShardStoresRequestDescriptor Indices(Elastic.Clients.Elasticsearch.Indices? value)
	{
		Instance.Indices = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// If false, the request returns an error if any wildcard expression, index alias, or _all
	/// value targets only missing or closed indices. This behavior applies even if the request
	/// targets other open indices.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndexManagement.ShardStoresRequestDescriptor AllowNoIndices(bool? value = true)
	{
		Instance.AllowNoIndices = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Type of index that wildcard patterns can match. If the request can target data streams,
	/// this argument determines whether wildcard expressions match hidden data streams.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndexManagement.ShardStoresRequestDescriptor ExpandWildcards(System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.ExpandWildcard>? value)
	{
		Instance.ExpandWildcards = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Type of index that wildcard patterns can match. If the request can target data streams,
	/// this argument determines whether wildcard expressions match hidden data streams.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndexManagement.ShardStoresRequestDescriptor ExpandWildcards(params Elastic.Clients.Elasticsearch.ExpandWildcard[] values)
	{
		Instance.ExpandWildcards = [.. values];
		return this;
	}

	/// <summary>
	/// <para>
	/// If true, missing or closed indices are not included in the response.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndexManagement.ShardStoresRequestDescriptor IgnoreUnavailable(bool? value = true)
	{
		Instance.IgnoreUnavailable = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// List of shard health statuses used to limit the request.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndexManagement.ShardStoresRequestDescriptor Status(System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.IndexManagement.ShardStoreStatus>? value)
	{
		Instance.Status = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// List of shard health statuses used to limit the request.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndexManagement.ShardStoresRequestDescriptor Status(params Elastic.Clients.Elasticsearch.IndexManagement.ShardStoreStatus[] values)
	{
		Instance.Status = [.. values];
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.IndexManagement.ShardStoresRequest Build(System.Action<Elastic.Clients.Elasticsearch.IndexManagement.ShardStoresRequestDescriptor>? action)
	{
		if (action is null)
		{
			return new Elastic.Clients.Elasticsearch.IndexManagement.ShardStoresRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
		}

		var builder = new Elastic.Clients.Elasticsearch.IndexManagement.ShardStoresRequestDescriptor(new Elastic.Clients.Elasticsearch.IndexManagement.ShardStoresRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}

	public Elastic.Clients.Elasticsearch.IndexManagement.ShardStoresRequestDescriptor ErrorTrace(bool? value)
	{
		Instance.ErrorTrace = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.IndexManagement.ShardStoresRequestDescriptor FilterPath(params string[]? value)
	{
		Instance.FilterPath = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.IndexManagement.ShardStoresRequestDescriptor Human(bool? value)
	{
		Instance.Human = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.IndexManagement.ShardStoresRequestDescriptor Pretty(bool? value)
	{
		Instance.Pretty = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.IndexManagement.ShardStoresRequestDescriptor SourceQueryString(string? value)
	{
		Instance.SourceQueryString = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.IndexManagement.ShardStoresRequestDescriptor RequestConfiguration(Elastic.Transport.IRequestConfiguration? value)
	{
		Instance.RequestConfiguration = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.IndexManagement.ShardStoresRequestDescriptor RequestConfiguration(System.Func<Elastic.Transport.RequestConfigurationDescriptor, Elastic.Transport.IRequestConfiguration>? configurationSelector)
	{
		Instance.RequestConfiguration = configurationSelector.Invoke(Instance.RequestConfiguration is null ? new Elastic.Transport.RequestConfigurationDescriptor() : new Elastic.Transport.RequestConfigurationDescriptor(Instance.RequestConfiguration)) ?? Instance.RequestConfiguration;
		return this;
	}
}

/// <summary>
/// <para>
/// Get index shard stores.
/// Get store information about replica shards in one or more indices.
/// For data streams, the API retrieves store information for the stream's backing indices.
/// </para>
/// <para>
/// The index shard stores API returns the following information:
/// </para>
/// <list type="bullet">
/// <item>
/// <para>
/// The node on which each replica shard exists.
/// </para>
/// </item>
/// <item>
/// <para>
/// The allocation ID for each replica shard.
/// </para>
/// </item>
/// <item>
/// <para>
/// A unique ID for each replica shard.
/// </para>
/// </item>
/// <item>
/// <para>
/// Any errors encountered while opening the shard index or from an earlier failure.
/// </para>
/// </item>
/// </list>
/// <para>
/// By default, the API returns store information only for primary shards that are unassigned or have one or more unassigned replica shards.
/// </para>
/// </summary>
public readonly partial struct ShardStoresRequestDescriptor<TDocument>
{
	internal Elastic.Clients.Elasticsearch.IndexManagement.ShardStoresRequest Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public ShardStoresRequestDescriptor(Elastic.Clients.Elasticsearch.IndexManagement.ShardStoresRequest instance)
	{
		Instance = instance;
	}

	public ShardStoresRequestDescriptor(Elastic.Clients.Elasticsearch.Indices? indices)
	{
		Instance = new Elastic.Clients.Elasticsearch.IndexManagement.ShardStoresRequest(indices);
	}

	public ShardStoresRequestDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.IndexManagement.ShardStoresRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.IndexManagement.ShardStoresRequestDescriptor<TDocument>(Elastic.Clients.Elasticsearch.IndexManagement.ShardStoresRequest instance) => new Elastic.Clients.Elasticsearch.IndexManagement.ShardStoresRequestDescriptor<TDocument>(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.IndexManagement.ShardStoresRequest(Elastic.Clients.Elasticsearch.IndexManagement.ShardStoresRequestDescriptor<TDocument> descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// List of data streams, indices, and aliases used to limit the request.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndexManagement.ShardStoresRequestDescriptor<TDocument> Indices(Elastic.Clients.Elasticsearch.Indices? value)
	{
		Instance.Indices = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// If false, the request returns an error if any wildcard expression, index alias, or _all
	/// value targets only missing or closed indices. This behavior applies even if the request
	/// targets other open indices.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndexManagement.ShardStoresRequestDescriptor<TDocument> AllowNoIndices(bool? value = true)
	{
		Instance.AllowNoIndices = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Type of index that wildcard patterns can match. If the request can target data streams,
	/// this argument determines whether wildcard expressions match hidden data streams.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndexManagement.ShardStoresRequestDescriptor<TDocument> ExpandWildcards(System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.ExpandWildcard>? value)
	{
		Instance.ExpandWildcards = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Type of index that wildcard patterns can match. If the request can target data streams,
	/// this argument determines whether wildcard expressions match hidden data streams.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndexManagement.ShardStoresRequestDescriptor<TDocument> ExpandWildcards(params Elastic.Clients.Elasticsearch.ExpandWildcard[] values)
	{
		Instance.ExpandWildcards = [.. values];
		return this;
	}

	/// <summary>
	/// <para>
	/// If true, missing or closed indices are not included in the response.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndexManagement.ShardStoresRequestDescriptor<TDocument> IgnoreUnavailable(bool? value = true)
	{
		Instance.IgnoreUnavailable = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// List of shard health statuses used to limit the request.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndexManagement.ShardStoresRequestDescriptor<TDocument> Status(System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.IndexManagement.ShardStoreStatus>? value)
	{
		Instance.Status = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// List of shard health statuses used to limit the request.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndexManagement.ShardStoresRequestDescriptor<TDocument> Status(params Elastic.Clients.Elasticsearch.IndexManagement.ShardStoreStatus[] values)
	{
		Instance.Status = [.. values];
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.IndexManagement.ShardStoresRequest Build(System.Action<Elastic.Clients.Elasticsearch.IndexManagement.ShardStoresRequestDescriptor<TDocument>>? action)
	{
		if (action is null)
		{
			return new Elastic.Clients.Elasticsearch.IndexManagement.ShardStoresRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
		}

		var builder = new Elastic.Clients.Elasticsearch.IndexManagement.ShardStoresRequestDescriptor<TDocument>(new Elastic.Clients.Elasticsearch.IndexManagement.ShardStoresRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}

	public Elastic.Clients.Elasticsearch.IndexManagement.ShardStoresRequestDescriptor<TDocument> ErrorTrace(bool? value)
	{
		Instance.ErrorTrace = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.IndexManagement.ShardStoresRequestDescriptor<TDocument> FilterPath(params string[]? value)
	{
		Instance.FilterPath = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.IndexManagement.ShardStoresRequestDescriptor<TDocument> Human(bool? value)
	{
		Instance.Human = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.IndexManagement.ShardStoresRequestDescriptor<TDocument> Pretty(bool? value)
	{
		Instance.Pretty = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.IndexManagement.ShardStoresRequestDescriptor<TDocument> SourceQueryString(string? value)
	{
		Instance.SourceQueryString = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.IndexManagement.ShardStoresRequestDescriptor<TDocument> RequestConfiguration(Elastic.Transport.IRequestConfiguration? value)
	{
		Instance.RequestConfiguration = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.IndexManagement.ShardStoresRequestDescriptor<TDocument> RequestConfiguration(System.Func<Elastic.Transport.RequestConfigurationDescriptor, Elastic.Transport.IRequestConfiguration>? configurationSelector)
	{
		Instance.RequestConfiguration = configurationSelector.Invoke(Instance.RequestConfiguration is null ? new Elastic.Transport.RequestConfigurationDescriptor() : new Elastic.Transport.RequestConfigurationDescriptor(Instance.RequestConfiguration)) ?? Instance.RequestConfiguration;
		return this;
	}
}