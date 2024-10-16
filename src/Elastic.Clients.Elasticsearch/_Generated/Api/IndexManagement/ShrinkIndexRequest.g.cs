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

using Elastic.Clients.Elasticsearch.Fluent;
using Elastic.Clients.Elasticsearch.Requests;
using Elastic.Clients.Elasticsearch.Serialization;
using Elastic.Transport;
using Elastic.Transport.Extensions;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch.IndexManagement;

public sealed partial class ShrinkIndexRequestParameters : RequestParameters
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

/// <summary>
/// <para>
/// Shrinks an existing index into a new index with fewer primary shards.
/// </para>
/// </summary>
public sealed partial class ShrinkIndexRequest : PlainRequest<ShrinkIndexRequestParameters>
{
	public ShrinkIndexRequest(Elastic.Clients.Elasticsearch.IndexName index, Elastic.Clients.Elasticsearch.IndexName target) : base(r => r.Required("index", index).Required("target", target))
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.IndexManagementShrink;

	protected override HttpMethod StaticHttpMethod => HttpMethod.PUT;

	internal override bool SupportsBody => true;

	internal override string OperationName => "indices.shrink";

	/// <summary>
	/// <para>
	/// Period to wait for a connection to the master node.
	/// If no response is received before the timeout expires, the request fails and returns an error.
	/// </para>
	/// </summary>
	[JsonIgnore]
	public Elastic.Clients.Elasticsearch.Duration? MasterTimeout { get => Q<Elastic.Clients.Elasticsearch.Duration?>("master_timeout"); set => Q("master_timeout", value); }

	/// <summary>
	/// <para>
	/// Period to wait for a response.
	/// If no response is received before the timeout expires, the request fails and returns an error.
	/// </para>
	/// </summary>
	[JsonIgnore]
	public Elastic.Clients.Elasticsearch.Duration? Timeout { get => Q<Elastic.Clients.Elasticsearch.Duration?>("timeout"); set => Q("timeout", value); }

	/// <summary>
	/// <para>
	/// The number of shard copies that must be active before proceeding with the operation.
	/// Set to <c>all</c> or any positive integer up to the total number of shards in the index (<c>number_of_replicas+1</c>).
	/// </para>
	/// </summary>
	[JsonIgnore]
	public Elastic.Clients.Elasticsearch.WaitForActiveShards? WaitForActiveShards { get => Q<Elastic.Clients.Elasticsearch.WaitForActiveShards?>("wait_for_active_shards"); set => Q("wait_for_active_shards", value); }

	/// <summary>
	/// <para>
	/// The key is the alias name.
	/// Index alias names support date math.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("aliases")]
	public IDictionary<Elastic.Clients.Elasticsearch.IndexName, Elastic.Clients.Elasticsearch.IndexManagement.Alias>? Aliases { get; set; }

	/// <summary>
	/// <para>
	/// Configuration options for the target index.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("settings")]
	public IDictionary<string, object>? Settings { get; set; }
}

/// <summary>
/// <para>
/// Shrinks an existing index into a new index with fewer primary shards.
/// </para>
/// </summary>
public sealed partial class ShrinkIndexRequestDescriptor<TDocument> : RequestDescriptor<ShrinkIndexRequestDescriptor<TDocument>, ShrinkIndexRequestParameters>
{
	internal ShrinkIndexRequestDescriptor(Action<ShrinkIndexRequestDescriptor<TDocument>> configure) => configure.Invoke(this);

	public ShrinkIndexRequestDescriptor(Elastic.Clients.Elasticsearch.IndexName index, Elastic.Clients.Elasticsearch.IndexName target) : base(r => r.Required("index", index).Required("target", target))
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.IndexManagementShrink;

	protected override HttpMethod StaticHttpMethod => HttpMethod.PUT;

	internal override bool SupportsBody => true;

	internal override string OperationName => "indices.shrink";

	public ShrinkIndexRequestDescriptor<TDocument> MasterTimeout(Elastic.Clients.Elasticsearch.Duration? masterTimeout) => Qs("master_timeout", masterTimeout);
	public ShrinkIndexRequestDescriptor<TDocument> Timeout(Elastic.Clients.Elasticsearch.Duration? timeout) => Qs("timeout", timeout);
	public ShrinkIndexRequestDescriptor<TDocument> WaitForActiveShards(Elastic.Clients.Elasticsearch.WaitForActiveShards? waitForActiveShards) => Qs("wait_for_active_shards", waitForActiveShards);

	public ShrinkIndexRequestDescriptor<TDocument> Index(Elastic.Clients.Elasticsearch.IndexName index)
	{
		RouteValues.Required("index", index);
		return Self;
	}

	public ShrinkIndexRequestDescriptor<TDocument> Target(Elastic.Clients.Elasticsearch.IndexName target)
	{
		RouteValues.Required("target", target);
		return Self;
	}

	private IDictionary<Elastic.Clients.Elasticsearch.IndexName, Elastic.Clients.Elasticsearch.IndexManagement.AliasDescriptor<TDocument>> AliasesValue { get; set; }
	private IDictionary<string, object>? SettingsValue { get; set; }

	/// <summary>
	/// <para>
	/// The key is the alias name.
	/// Index alias names support date math.
	/// </para>
	/// </summary>
	public ShrinkIndexRequestDescriptor<TDocument> Aliases(Func<FluentDescriptorDictionary<Elastic.Clients.Elasticsearch.IndexName, Elastic.Clients.Elasticsearch.IndexManagement.AliasDescriptor<TDocument>>, FluentDescriptorDictionary<Elastic.Clients.Elasticsearch.IndexName, Elastic.Clients.Elasticsearch.IndexManagement.AliasDescriptor<TDocument>>> selector)
	{
		AliasesValue = selector?.Invoke(new FluentDescriptorDictionary<Elastic.Clients.Elasticsearch.IndexName, Elastic.Clients.Elasticsearch.IndexManagement.AliasDescriptor<TDocument>>());
		return Self;
	}

	/// <summary>
	/// <para>
	/// Configuration options for the target index.
	/// </para>
	/// </summary>
	public ShrinkIndexRequestDescriptor<TDocument> Settings(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> selector)
	{
		SettingsValue = selector?.Invoke(new FluentDictionary<string, object>());
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (AliasesValue is not null)
		{
			writer.WritePropertyName("aliases");
			JsonSerializer.Serialize(writer, AliasesValue, options);
		}

		if (SettingsValue is not null)
		{
			writer.WritePropertyName("settings");
			JsonSerializer.Serialize(writer, SettingsValue, options);
		}

		writer.WriteEndObject();
	}
}

/// <summary>
/// <para>
/// Shrinks an existing index into a new index with fewer primary shards.
/// </para>
/// </summary>
public sealed partial class ShrinkIndexRequestDescriptor : RequestDescriptor<ShrinkIndexRequestDescriptor, ShrinkIndexRequestParameters>
{
	internal ShrinkIndexRequestDescriptor(Action<ShrinkIndexRequestDescriptor> configure) => configure.Invoke(this);

	public ShrinkIndexRequestDescriptor(Elastic.Clients.Elasticsearch.IndexName index, Elastic.Clients.Elasticsearch.IndexName target) : base(r => r.Required("index", index).Required("target", target))
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.IndexManagementShrink;

	protected override HttpMethod StaticHttpMethod => HttpMethod.PUT;

	internal override bool SupportsBody => true;

	internal override string OperationName => "indices.shrink";

	public ShrinkIndexRequestDescriptor MasterTimeout(Elastic.Clients.Elasticsearch.Duration? masterTimeout) => Qs("master_timeout", masterTimeout);
	public ShrinkIndexRequestDescriptor Timeout(Elastic.Clients.Elasticsearch.Duration? timeout) => Qs("timeout", timeout);
	public ShrinkIndexRequestDescriptor WaitForActiveShards(Elastic.Clients.Elasticsearch.WaitForActiveShards? waitForActiveShards) => Qs("wait_for_active_shards", waitForActiveShards);

	public ShrinkIndexRequestDescriptor Index(Elastic.Clients.Elasticsearch.IndexName index)
	{
		RouteValues.Required("index", index);
		return Self;
	}

	public ShrinkIndexRequestDescriptor Target(Elastic.Clients.Elasticsearch.IndexName target)
	{
		RouteValues.Required("target", target);
		return Self;
	}

	private IDictionary<Elastic.Clients.Elasticsearch.IndexName, Elastic.Clients.Elasticsearch.IndexManagement.AliasDescriptor> AliasesValue { get; set; }
	private IDictionary<string, object>? SettingsValue { get; set; }

	/// <summary>
	/// <para>
	/// The key is the alias name.
	/// Index alias names support date math.
	/// </para>
	/// </summary>
	public ShrinkIndexRequestDescriptor Aliases(Func<FluentDescriptorDictionary<Elastic.Clients.Elasticsearch.IndexName, Elastic.Clients.Elasticsearch.IndexManagement.AliasDescriptor>, FluentDescriptorDictionary<Elastic.Clients.Elasticsearch.IndexName, Elastic.Clients.Elasticsearch.IndexManagement.AliasDescriptor>> selector)
	{
		AliasesValue = selector?.Invoke(new FluentDescriptorDictionary<Elastic.Clients.Elasticsearch.IndexName, Elastic.Clients.Elasticsearch.IndexManagement.AliasDescriptor>());
		return Self;
	}

	/// <summary>
	/// <para>
	/// Configuration options for the target index.
	/// </para>
	/// </summary>
	public ShrinkIndexRequestDescriptor Settings(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> selector)
	{
		SettingsValue = selector?.Invoke(new FluentDictionary<string, object>());
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (AliasesValue is not null)
		{
			writer.WritePropertyName("aliases");
			JsonSerializer.Serialize(writer, AliasesValue, options);
		}

		if (SettingsValue is not null)
		{
			writer.WritePropertyName("settings");
			JsonSerializer.Serialize(writer, SettingsValue, options);
		}

		writer.WriteEndObject();
	}
}