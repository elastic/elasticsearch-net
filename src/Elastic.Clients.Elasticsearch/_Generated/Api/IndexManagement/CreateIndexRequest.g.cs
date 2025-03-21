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

public sealed partial class CreateIndexRequestParameters : RequestParameters
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
/// Create an index.
/// You can use the create index API to add a new index to an Elasticsearch cluster.
/// When creating an index, you can specify the following:
/// </para>
/// <list type="bullet">
/// <item>
/// <para>
/// Settings for the index.
/// </para>
/// </item>
/// <item>
/// <para>
/// Mappings for fields in the index.
/// </para>
/// </item>
/// <item>
/// <para>
/// Index aliases
/// </para>
/// </item>
/// </list>
/// <para>
/// <strong>Wait for active shards</strong>
/// </para>
/// <para>
/// By default, index creation will only return a response to the client when the primary copies of each shard have been started, or the request times out.
/// The index creation response will indicate what happened.
/// For example, <c>acknowledged</c> indicates whether the index was successfully created in the cluster, <c>while shards_acknowledged</c> indicates whether the requisite number of shard copies were started for each shard in the index before timing out.
/// Note that it is still possible for either <c>acknowledged</c> or <c>shards_acknowledged</c> to be <c>false</c>, but for the index creation to be successful.
/// These values simply indicate whether the operation completed before the timeout.
/// If <c>acknowledged</c> is false, the request timed out before the cluster state was updated with the newly created index, but it probably will be created sometime soon.
/// If <c>shards_acknowledged</c> is false, then the request timed out before the requisite number of shards were started (by default just the primaries), even if the cluster state was successfully updated to reflect the newly created index (that is to say, <c>acknowledged</c> is <c>true</c>).
/// </para>
/// <para>
/// You can change the default of only waiting for the primary shards to start through the index setting <c>index.write.wait_for_active_shards</c>.
/// Note that changing this setting will also affect the <c>wait_for_active_shards</c> value on all subsequent write operations.
/// </para>
/// </summary>
public sealed partial class CreateIndexRequest : PlainRequest<CreateIndexRequestParameters>
{
	public CreateIndexRequest(Elastic.Clients.Elasticsearch.IndexName index) : base(r => r.Required("index", index))
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.IndexManagementCreate;

	protected override HttpMethod StaticHttpMethod => HttpMethod.PUT;

	internal override bool SupportsBody => true;

	internal override string OperationName => "indices.create";

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
	/// Aliases for the index.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("aliases")]
	public IDictionary<Elastic.Clients.Elasticsearch.Name, Elastic.Clients.Elasticsearch.IndexManagement.Alias>? Aliases { get; set; }

	/// <summary>
	/// <para>
	/// Mapping for fields in the index. If specified, this mapping can include:
	/// </para>
	/// <list type="bullet">
	/// <item>
	/// <para>
	/// Field names
	/// </para>
	/// </item>
	/// <item>
	/// <para>
	/// Field data types
	/// </para>
	/// </item>
	/// <item>
	/// <para>
	/// Mapping parameters
	/// </para>
	/// </item>
	/// </list>
	/// </summary>
	[JsonInclude, JsonPropertyName("mappings")]
	public Elastic.Clients.Elasticsearch.Mapping.TypeMapping? Mappings { get; set; }

	/// <summary>
	/// <para>
	/// Configuration options for the index.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("settings")]
	public Elastic.Clients.Elasticsearch.IndexManagement.IndexSettings? Settings { get; set; }
}

/// <summary>
/// <para>
/// Create an index.
/// You can use the create index API to add a new index to an Elasticsearch cluster.
/// When creating an index, you can specify the following:
/// </para>
/// <list type="bullet">
/// <item>
/// <para>
/// Settings for the index.
/// </para>
/// </item>
/// <item>
/// <para>
/// Mappings for fields in the index.
/// </para>
/// </item>
/// <item>
/// <para>
/// Index aliases
/// </para>
/// </item>
/// </list>
/// <para>
/// <strong>Wait for active shards</strong>
/// </para>
/// <para>
/// By default, index creation will only return a response to the client when the primary copies of each shard have been started, or the request times out.
/// The index creation response will indicate what happened.
/// For example, <c>acknowledged</c> indicates whether the index was successfully created in the cluster, <c>while shards_acknowledged</c> indicates whether the requisite number of shard copies were started for each shard in the index before timing out.
/// Note that it is still possible for either <c>acknowledged</c> or <c>shards_acknowledged</c> to be <c>false</c>, but for the index creation to be successful.
/// These values simply indicate whether the operation completed before the timeout.
/// If <c>acknowledged</c> is false, the request timed out before the cluster state was updated with the newly created index, but it probably will be created sometime soon.
/// If <c>shards_acknowledged</c> is false, then the request timed out before the requisite number of shards were started (by default just the primaries), even if the cluster state was successfully updated to reflect the newly created index (that is to say, <c>acknowledged</c> is <c>true</c>).
/// </para>
/// <para>
/// You can change the default of only waiting for the primary shards to start through the index setting <c>index.write.wait_for_active_shards</c>.
/// Note that changing this setting will also affect the <c>wait_for_active_shards</c> value on all subsequent write operations.
/// </para>
/// </summary>
public sealed partial class CreateIndexRequestDescriptor<TDocument> : RequestDescriptor<CreateIndexRequestDescriptor<TDocument>, CreateIndexRequestParameters>
{
	internal CreateIndexRequestDescriptor(Action<CreateIndexRequestDescriptor<TDocument>> configure) => configure.Invoke(this);

	public CreateIndexRequestDescriptor(Elastic.Clients.Elasticsearch.IndexName index) : base(r => r.Required("index", index))
	{
	}

	public CreateIndexRequestDescriptor() : this(typeof(TDocument))
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.IndexManagementCreate;

	protected override HttpMethod StaticHttpMethod => HttpMethod.PUT;

	internal override bool SupportsBody => true;

	internal override string OperationName => "indices.create";

	public CreateIndexRequestDescriptor<TDocument> MasterTimeout(Elastic.Clients.Elasticsearch.Duration? masterTimeout) => Qs("master_timeout", masterTimeout);
	public CreateIndexRequestDescriptor<TDocument> Timeout(Elastic.Clients.Elasticsearch.Duration? timeout) => Qs("timeout", timeout);
	public CreateIndexRequestDescriptor<TDocument> WaitForActiveShards(Elastic.Clients.Elasticsearch.WaitForActiveShards? waitForActiveShards) => Qs("wait_for_active_shards", waitForActiveShards);

	public CreateIndexRequestDescriptor<TDocument> Index(Elastic.Clients.Elasticsearch.IndexName index)
	{
		RouteValues.Required("index", index);
		return Self;
	}

	private IDictionary<Elastic.Clients.Elasticsearch.Name, Elastic.Clients.Elasticsearch.IndexManagement.AliasDescriptor<TDocument>> AliasesValue { get; set; }
	private Elastic.Clients.Elasticsearch.Mapping.TypeMapping? MappingsValue { get; set; }
	private Elastic.Clients.Elasticsearch.Mapping.TypeMappingDescriptor<TDocument> MappingsDescriptor { get; set; }
	private Action<Elastic.Clients.Elasticsearch.Mapping.TypeMappingDescriptor<TDocument>> MappingsDescriptorAction { get; set; }
	private Elastic.Clients.Elasticsearch.IndexManagement.IndexSettings? SettingsValue { get; set; }
	private Elastic.Clients.Elasticsearch.IndexManagement.IndexSettingsDescriptor<TDocument> SettingsDescriptor { get; set; }
	private Action<Elastic.Clients.Elasticsearch.IndexManagement.IndexSettingsDescriptor<TDocument>> SettingsDescriptorAction { get; set; }

	/// <summary>
	/// <para>
	/// Aliases for the index.
	/// </para>
	/// </summary>
	public CreateIndexRequestDescriptor<TDocument> Aliases(Func<FluentDescriptorDictionary<Elastic.Clients.Elasticsearch.Name, Elastic.Clients.Elasticsearch.IndexManagement.AliasDescriptor<TDocument>>, FluentDescriptorDictionary<Elastic.Clients.Elasticsearch.Name, Elastic.Clients.Elasticsearch.IndexManagement.AliasDescriptor<TDocument>>> selector)
	{
		AliasesValue = selector?.Invoke(new FluentDescriptorDictionary<Elastic.Clients.Elasticsearch.Name, Elastic.Clients.Elasticsearch.IndexManagement.AliasDescriptor<TDocument>>());
		return Self;
	}

	/// <summary>
	/// <para>
	/// Mapping for fields in the index. If specified, this mapping can include:
	/// </para>
	/// <list type="bullet">
	/// <item>
	/// <para>
	/// Field names
	/// </para>
	/// </item>
	/// <item>
	/// <para>
	/// Field data types
	/// </para>
	/// </item>
	/// <item>
	/// <para>
	/// Mapping parameters
	/// </para>
	/// </item>
	/// </list>
	/// </summary>
	public CreateIndexRequestDescriptor<TDocument> Mappings(Elastic.Clients.Elasticsearch.Mapping.TypeMapping? mappings)
	{
		MappingsDescriptor = null;
		MappingsDescriptorAction = null;
		MappingsValue = mappings;
		return Self;
	}

	public CreateIndexRequestDescriptor<TDocument> Mappings(Elastic.Clients.Elasticsearch.Mapping.TypeMappingDescriptor<TDocument> descriptor)
	{
		MappingsValue = null;
		MappingsDescriptorAction = null;
		MappingsDescriptor = descriptor;
		return Self;
	}

	public CreateIndexRequestDescriptor<TDocument> Mappings(Action<Elastic.Clients.Elasticsearch.Mapping.TypeMappingDescriptor<TDocument>> configure)
	{
		MappingsValue = null;
		MappingsDescriptor = null;
		MappingsDescriptorAction = configure;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Configuration options for the index.
	/// </para>
	/// </summary>
	public CreateIndexRequestDescriptor<TDocument> Settings(Elastic.Clients.Elasticsearch.IndexManagement.IndexSettings? settings)
	{
		SettingsDescriptor = null;
		SettingsDescriptorAction = null;
		SettingsValue = settings;
		return Self;
	}

	public CreateIndexRequestDescriptor<TDocument> Settings(Elastic.Clients.Elasticsearch.IndexManagement.IndexSettingsDescriptor<TDocument> descriptor)
	{
		SettingsValue = null;
		SettingsDescriptorAction = null;
		SettingsDescriptor = descriptor;
		return Self;
	}

	public CreateIndexRequestDescriptor<TDocument> Settings(Action<Elastic.Clients.Elasticsearch.IndexManagement.IndexSettingsDescriptor<TDocument>> configure)
	{
		SettingsValue = null;
		SettingsDescriptor = null;
		SettingsDescriptorAction = configure;
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

		if (MappingsDescriptor is not null)
		{
			writer.WritePropertyName("mappings");
			JsonSerializer.Serialize(writer, MappingsDescriptor, options);
		}
		else if (MappingsDescriptorAction is not null)
		{
			writer.WritePropertyName("mappings");
			JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.Mapping.TypeMappingDescriptor<TDocument>(MappingsDescriptorAction), options);
		}
		else if (MappingsValue is not null)
		{
			writer.WritePropertyName("mappings");
			JsonSerializer.Serialize(writer, MappingsValue, options);
		}

		if (SettingsDescriptor is not null)
		{
			writer.WritePropertyName("settings");
			JsonSerializer.Serialize(writer, SettingsDescriptor, options);
		}
		else if (SettingsDescriptorAction is not null)
		{
			writer.WritePropertyName("settings");
			JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.IndexManagement.IndexSettingsDescriptor<TDocument>(SettingsDescriptorAction), options);
		}
		else if (SettingsValue is not null)
		{
			writer.WritePropertyName("settings");
			JsonSerializer.Serialize(writer, SettingsValue, options);
		}

		writer.WriteEndObject();
	}
}

/// <summary>
/// <para>
/// Create an index.
/// You can use the create index API to add a new index to an Elasticsearch cluster.
/// When creating an index, you can specify the following:
/// </para>
/// <list type="bullet">
/// <item>
/// <para>
/// Settings for the index.
/// </para>
/// </item>
/// <item>
/// <para>
/// Mappings for fields in the index.
/// </para>
/// </item>
/// <item>
/// <para>
/// Index aliases
/// </para>
/// </item>
/// </list>
/// <para>
/// <strong>Wait for active shards</strong>
/// </para>
/// <para>
/// By default, index creation will only return a response to the client when the primary copies of each shard have been started, or the request times out.
/// The index creation response will indicate what happened.
/// For example, <c>acknowledged</c> indicates whether the index was successfully created in the cluster, <c>while shards_acknowledged</c> indicates whether the requisite number of shard copies were started for each shard in the index before timing out.
/// Note that it is still possible for either <c>acknowledged</c> or <c>shards_acknowledged</c> to be <c>false</c>, but for the index creation to be successful.
/// These values simply indicate whether the operation completed before the timeout.
/// If <c>acknowledged</c> is false, the request timed out before the cluster state was updated with the newly created index, but it probably will be created sometime soon.
/// If <c>shards_acknowledged</c> is false, then the request timed out before the requisite number of shards were started (by default just the primaries), even if the cluster state was successfully updated to reflect the newly created index (that is to say, <c>acknowledged</c> is <c>true</c>).
/// </para>
/// <para>
/// You can change the default of only waiting for the primary shards to start through the index setting <c>index.write.wait_for_active_shards</c>.
/// Note that changing this setting will also affect the <c>wait_for_active_shards</c> value on all subsequent write operations.
/// </para>
/// </summary>
public sealed partial class CreateIndexRequestDescriptor : RequestDescriptor<CreateIndexRequestDescriptor, CreateIndexRequestParameters>
{
	internal CreateIndexRequestDescriptor(Action<CreateIndexRequestDescriptor> configure) => configure.Invoke(this);

	public CreateIndexRequestDescriptor(Elastic.Clients.Elasticsearch.IndexName index) : base(r => r.Required("index", index))
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.IndexManagementCreate;

	protected override HttpMethod StaticHttpMethod => HttpMethod.PUT;

	internal override bool SupportsBody => true;

	internal override string OperationName => "indices.create";

	public CreateIndexRequestDescriptor MasterTimeout(Elastic.Clients.Elasticsearch.Duration? masterTimeout) => Qs("master_timeout", masterTimeout);
	public CreateIndexRequestDescriptor Timeout(Elastic.Clients.Elasticsearch.Duration? timeout) => Qs("timeout", timeout);
	public CreateIndexRequestDescriptor WaitForActiveShards(Elastic.Clients.Elasticsearch.WaitForActiveShards? waitForActiveShards) => Qs("wait_for_active_shards", waitForActiveShards);

	public CreateIndexRequestDescriptor Index(Elastic.Clients.Elasticsearch.IndexName index)
	{
		RouteValues.Required("index", index);
		return Self;
	}

	private IDictionary<Elastic.Clients.Elasticsearch.Name, Elastic.Clients.Elasticsearch.IndexManagement.AliasDescriptor> AliasesValue { get; set; }
	private Elastic.Clients.Elasticsearch.Mapping.TypeMapping? MappingsValue { get; set; }
	private Elastic.Clients.Elasticsearch.Mapping.TypeMappingDescriptor MappingsDescriptor { get; set; }
	private Action<Elastic.Clients.Elasticsearch.Mapping.TypeMappingDescriptor> MappingsDescriptorAction { get; set; }
	private Elastic.Clients.Elasticsearch.IndexManagement.IndexSettings? SettingsValue { get; set; }
	private Elastic.Clients.Elasticsearch.IndexManagement.IndexSettingsDescriptor SettingsDescriptor { get; set; }
	private Action<Elastic.Clients.Elasticsearch.IndexManagement.IndexSettingsDescriptor> SettingsDescriptorAction { get; set; }

	/// <summary>
	/// <para>
	/// Aliases for the index.
	/// </para>
	/// </summary>
	public CreateIndexRequestDescriptor Aliases(Func<FluentDescriptorDictionary<Elastic.Clients.Elasticsearch.Name, Elastic.Clients.Elasticsearch.IndexManagement.AliasDescriptor>, FluentDescriptorDictionary<Elastic.Clients.Elasticsearch.Name, Elastic.Clients.Elasticsearch.IndexManagement.AliasDescriptor>> selector)
	{
		AliasesValue = selector?.Invoke(new FluentDescriptorDictionary<Elastic.Clients.Elasticsearch.Name, Elastic.Clients.Elasticsearch.IndexManagement.AliasDescriptor>());
		return Self;
	}

	/// <summary>
	/// <para>
	/// Mapping for fields in the index. If specified, this mapping can include:
	/// </para>
	/// <list type="bullet">
	/// <item>
	/// <para>
	/// Field names
	/// </para>
	/// </item>
	/// <item>
	/// <para>
	/// Field data types
	/// </para>
	/// </item>
	/// <item>
	/// <para>
	/// Mapping parameters
	/// </para>
	/// </item>
	/// </list>
	/// </summary>
	public CreateIndexRequestDescriptor Mappings(Elastic.Clients.Elasticsearch.Mapping.TypeMapping? mappings)
	{
		MappingsDescriptor = null;
		MappingsDescriptorAction = null;
		MappingsValue = mappings;
		return Self;
	}

	public CreateIndexRequestDescriptor Mappings(Elastic.Clients.Elasticsearch.Mapping.TypeMappingDescriptor descriptor)
	{
		MappingsValue = null;
		MappingsDescriptorAction = null;
		MappingsDescriptor = descriptor;
		return Self;
	}

	public CreateIndexRequestDescriptor Mappings(Action<Elastic.Clients.Elasticsearch.Mapping.TypeMappingDescriptor> configure)
	{
		MappingsValue = null;
		MappingsDescriptor = null;
		MappingsDescriptorAction = configure;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Configuration options for the index.
	/// </para>
	/// </summary>
	public CreateIndexRequestDescriptor Settings(Elastic.Clients.Elasticsearch.IndexManagement.IndexSettings? settings)
	{
		SettingsDescriptor = null;
		SettingsDescriptorAction = null;
		SettingsValue = settings;
		return Self;
	}

	public CreateIndexRequestDescriptor Settings(Elastic.Clients.Elasticsearch.IndexManagement.IndexSettingsDescriptor descriptor)
	{
		SettingsValue = null;
		SettingsDescriptorAction = null;
		SettingsDescriptor = descriptor;
		return Self;
	}

	public CreateIndexRequestDescriptor Settings(Action<Elastic.Clients.Elasticsearch.IndexManagement.IndexSettingsDescriptor> configure)
	{
		SettingsValue = null;
		SettingsDescriptor = null;
		SettingsDescriptorAction = configure;
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

		if (MappingsDescriptor is not null)
		{
			writer.WritePropertyName("mappings");
			JsonSerializer.Serialize(writer, MappingsDescriptor, options);
		}
		else if (MappingsDescriptorAction is not null)
		{
			writer.WritePropertyName("mappings");
			JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.Mapping.TypeMappingDescriptor(MappingsDescriptorAction), options);
		}
		else if (MappingsValue is not null)
		{
			writer.WritePropertyName("mappings");
			JsonSerializer.Serialize(writer, MappingsValue, options);
		}

		if (SettingsDescriptor is not null)
		{
			writer.WritePropertyName("settings");
			JsonSerializer.Serialize(writer, SettingsDescriptor, options);
		}
		else if (SettingsDescriptorAction is not null)
		{
			writer.WritePropertyName("settings");
			JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.IndexManagement.IndexSettingsDescriptor(SettingsDescriptorAction), options);
		}
		else if (SettingsValue is not null)
		{
			writer.WritePropertyName("settings");
			JsonSerializer.Serialize(writer, SettingsValue, options);
		}

		writer.WriteEndObject();
	}
}