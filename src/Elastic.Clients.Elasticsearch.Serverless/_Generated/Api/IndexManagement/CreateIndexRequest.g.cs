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

using Elastic.Clients.Elasticsearch.Serverless.Fluent;
using Elastic.Clients.Elasticsearch.Serverless.Requests;
using Elastic.Clients.Elasticsearch.Serverless.Serialization;
using Elastic.Transport;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch.Serverless.IndexManagement;

public sealed partial class CreateIndexRequestParameters : RequestParameters
{
	/// <summary>
	/// <para>Period to wait for a connection to the master node.<br/>If no response is received before the timeout expires, the request fails and returns an error.</para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Serverless.Duration? MasterTimeout { get => Q<Elastic.Clients.Elasticsearch.Serverless.Duration?>("master_timeout"); set => Q("master_timeout", value); }

	/// <summary>
	/// <para>Period to wait for a response.<br/>If no response is received before the timeout expires, the request fails and returns an error.</para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Serverless.Duration? Timeout { get => Q<Elastic.Clients.Elasticsearch.Serverless.Duration?>("timeout"); set => Q("timeout", value); }

	/// <summary>
	/// <para>The number of shard copies that must be active before proceeding with the operation.<br/>Set to `all` or any positive integer up to the total number of shards in the index (`number_of_replicas+1`).</para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Serverless.WaitForActiveShards? WaitForActiveShards { get => Q<Elastic.Clients.Elasticsearch.Serverless.WaitForActiveShards?>("wait_for_active_shards"); set => Q("wait_for_active_shards", value); }
}

/// <summary>
/// <para>Creates a new index.</para>
/// </summary>
public sealed partial class CreateIndexRequest : PlainRequest<CreateIndexRequestParameters>
{
	public CreateIndexRequest(Elastic.Clients.Elasticsearch.Serverless.IndexName index) : base(r => r.Required("index", index))
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.IndexManagementCreate;

	protected override HttpMethod StaticHttpMethod => HttpMethod.PUT;

	internal override bool SupportsBody => true;

	internal override string OperationName => "indices.create";

	/// <summary>
	/// <para>Period to wait for a connection to the master node.<br/>If no response is received before the timeout expires, the request fails and returns an error.</para>
	/// </summary>
	[JsonIgnore]
	public Elastic.Clients.Elasticsearch.Serverless.Duration? MasterTimeout { get => Q<Elastic.Clients.Elasticsearch.Serverless.Duration?>("master_timeout"); set => Q("master_timeout", value); }

	/// <summary>
	/// <para>Period to wait for a response.<br/>If no response is received before the timeout expires, the request fails and returns an error.</para>
	/// </summary>
	[JsonIgnore]
	public Elastic.Clients.Elasticsearch.Serverless.Duration? Timeout { get => Q<Elastic.Clients.Elasticsearch.Serverless.Duration?>("timeout"); set => Q("timeout", value); }

	/// <summary>
	/// <para>The number of shard copies that must be active before proceeding with the operation.<br/>Set to `all` or any positive integer up to the total number of shards in the index (`number_of_replicas+1`).</para>
	/// </summary>
	[JsonIgnore]
	public Elastic.Clients.Elasticsearch.Serverless.WaitForActiveShards? WaitForActiveShards { get => Q<Elastic.Clients.Elasticsearch.Serverless.WaitForActiveShards?>("wait_for_active_shards"); set => Q("wait_for_active_shards", value); }

	/// <summary>
	/// <para>Aliases for the index.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("aliases")]
	public IDictionary<Elastic.Clients.Elasticsearch.Serverless.Name, Elastic.Clients.Elasticsearch.Serverless.IndexManagement.Alias>? Aliases { get; set; }

	/// <summary>
	/// <para>Mapping for fields in the index. If specified, this mapping can include:<br/>- Field names<br/>- Field data types<br/>- Mapping parameters</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("mappings")]
	public Elastic.Clients.Elasticsearch.Serverless.Mapping.TypeMapping? Mappings { get; set; }

	/// <summary>
	/// <para>Configuration options for the index.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("settings")]
	public Elastic.Clients.Elasticsearch.Serverless.IndexManagement.IndexSettings? Settings { get; set; }
}

/// <summary>
/// <para>Creates a new index.</para>
/// </summary>
public sealed partial class CreateIndexRequestDescriptor<TDocument> : RequestDescriptor<CreateIndexRequestDescriptor<TDocument>, CreateIndexRequestParameters>
{
	internal CreateIndexRequestDescriptor(Action<CreateIndexRequestDescriptor<TDocument>> configure) => configure.Invoke(this);

	public CreateIndexRequestDescriptor(Elastic.Clients.Elasticsearch.Serverless.IndexName index) : base(r => r.Required("index", index))
	{
	}

	public CreateIndexRequestDescriptor() : this(typeof(TDocument))
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.IndexManagementCreate;

	protected override HttpMethod StaticHttpMethod => HttpMethod.PUT;

	internal override bool SupportsBody => true;

	internal override string OperationName => "indices.create";

	public CreateIndexRequestDescriptor<TDocument> MasterTimeout(Elastic.Clients.Elasticsearch.Serverless.Duration? masterTimeout) => Qs("master_timeout", masterTimeout);
	public CreateIndexRequestDescriptor<TDocument> Timeout(Elastic.Clients.Elasticsearch.Serverless.Duration? timeout) => Qs("timeout", timeout);
	public CreateIndexRequestDescriptor<TDocument> WaitForActiveShards(Elastic.Clients.Elasticsearch.Serverless.WaitForActiveShards? waitForActiveShards) => Qs("wait_for_active_shards", waitForActiveShards);

	public CreateIndexRequestDescriptor<TDocument> Index(Elastic.Clients.Elasticsearch.Serverless.IndexName index)
	{
		RouteValues.Required("index", index);
		return Self;
	}

	private IDictionary<Elastic.Clients.Elasticsearch.Serverless.Name, Elastic.Clients.Elasticsearch.Serverless.IndexManagement.AliasDescriptor<TDocument>> AliasesValue { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.Mapping.TypeMapping? MappingsValue { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.Mapping.TypeMappingDescriptor<TDocument> MappingsDescriptor { get; set; }
	private Action<Elastic.Clients.Elasticsearch.Serverless.Mapping.TypeMappingDescriptor<TDocument>> MappingsDescriptorAction { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.IndexManagement.IndexSettings? SettingsValue { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.IndexManagement.IndexSettingsDescriptor<TDocument> SettingsDescriptor { get; set; }
	private Action<Elastic.Clients.Elasticsearch.Serverless.IndexManagement.IndexSettingsDescriptor<TDocument>> SettingsDescriptorAction { get; set; }

	/// <summary>
	/// <para>Aliases for the index.</para>
	/// </summary>
	public CreateIndexRequestDescriptor<TDocument> Aliases(Func<FluentDescriptorDictionary<Elastic.Clients.Elasticsearch.Serverless.Name, Elastic.Clients.Elasticsearch.Serverless.IndexManagement.AliasDescriptor<TDocument>>, FluentDescriptorDictionary<Elastic.Clients.Elasticsearch.Serverless.Name, Elastic.Clients.Elasticsearch.Serverless.IndexManagement.AliasDescriptor<TDocument>>> selector)
	{
		AliasesValue = selector?.Invoke(new FluentDescriptorDictionary<Elastic.Clients.Elasticsearch.Serverless.Name, Elastic.Clients.Elasticsearch.Serverless.IndexManagement.AliasDescriptor<TDocument>>());
		return Self;
	}

	/// <summary>
	/// <para>Mapping for fields in the index. If specified, this mapping can include:<br/>- Field names<br/>- Field data types<br/>- Mapping parameters</para>
	/// </summary>
	public CreateIndexRequestDescriptor<TDocument> Mappings(Elastic.Clients.Elasticsearch.Serverless.Mapping.TypeMapping? mappings)
	{
		MappingsDescriptor = null;
		MappingsDescriptorAction = null;
		MappingsValue = mappings;
		return Self;
	}

	public CreateIndexRequestDescriptor<TDocument> Mappings(Elastic.Clients.Elasticsearch.Serverless.Mapping.TypeMappingDescriptor<TDocument> descriptor)
	{
		MappingsValue = null;
		MappingsDescriptorAction = null;
		MappingsDescriptor = descriptor;
		return Self;
	}

	public CreateIndexRequestDescriptor<TDocument> Mappings(Action<Elastic.Clients.Elasticsearch.Serverless.Mapping.TypeMappingDescriptor<TDocument>> configure)
	{
		MappingsValue = null;
		MappingsDescriptor = null;
		MappingsDescriptorAction = configure;
		return Self;
	}

	/// <summary>
	/// <para>Configuration options for the index.</para>
	/// </summary>
	public CreateIndexRequestDescriptor<TDocument> Settings(Elastic.Clients.Elasticsearch.Serverless.IndexManagement.IndexSettings? settings)
	{
		SettingsDescriptor = null;
		SettingsDescriptorAction = null;
		SettingsValue = settings;
		return Self;
	}

	public CreateIndexRequestDescriptor<TDocument> Settings(Elastic.Clients.Elasticsearch.Serverless.IndexManagement.IndexSettingsDescriptor<TDocument> descriptor)
	{
		SettingsValue = null;
		SettingsDescriptorAction = null;
		SettingsDescriptor = descriptor;
		return Self;
	}

	public CreateIndexRequestDescriptor<TDocument> Settings(Action<Elastic.Clients.Elasticsearch.Serverless.IndexManagement.IndexSettingsDescriptor<TDocument>> configure)
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
			JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.Serverless.Mapping.TypeMappingDescriptor<TDocument>(MappingsDescriptorAction), options);
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
			JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.Serverless.IndexManagement.IndexSettingsDescriptor<TDocument>(SettingsDescriptorAction), options);
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
/// <para>Creates a new index.</para>
/// </summary>
public sealed partial class CreateIndexRequestDescriptor : RequestDescriptor<CreateIndexRequestDescriptor, CreateIndexRequestParameters>
{
	internal CreateIndexRequestDescriptor(Action<CreateIndexRequestDescriptor> configure) => configure.Invoke(this);

	public CreateIndexRequestDescriptor(Elastic.Clients.Elasticsearch.Serverless.IndexName index) : base(r => r.Required("index", index))
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.IndexManagementCreate;

	protected override HttpMethod StaticHttpMethod => HttpMethod.PUT;

	internal override bool SupportsBody => true;

	internal override string OperationName => "indices.create";

	public CreateIndexRequestDescriptor MasterTimeout(Elastic.Clients.Elasticsearch.Serverless.Duration? masterTimeout) => Qs("master_timeout", masterTimeout);
	public CreateIndexRequestDescriptor Timeout(Elastic.Clients.Elasticsearch.Serverless.Duration? timeout) => Qs("timeout", timeout);
	public CreateIndexRequestDescriptor WaitForActiveShards(Elastic.Clients.Elasticsearch.Serverless.WaitForActiveShards? waitForActiveShards) => Qs("wait_for_active_shards", waitForActiveShards);

	public CreateIndexRequestDescriptor Index(Elastic.Clients.Elasticsearch.Serverless.IndexName index)
	{
		RouteValues.Required("index", index);
		return Self;
	}

	private IDictionary<Elastic.Clients.Elasticsearch.Serverless.Name, Elastic.Clients.Elasticsearch.Serverless.IndexManagement.AliasDescriptor> AliasesValue { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.Mapping.TypeMapping? MappingsValue { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.Mapping.TypeMappingDescriptor MappingsDescriptor { get; set; }
	private Action<Elastic.Clients.Elasticsearch.Serverless.Mapping.TypeMappingDescriptor> MappingsDescriptorAction { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.IndexManagement.IndexSettings? SettingsValue { get; set; }
	private Elastic.Clients.Elasticsearch.Serverless.IndexManagement.IndexSettingsDescriptor SettingsDescriptor { get; set; }
	private Action<Elastic.Clients.Elasticsearch.Serverless.IndexManagement.IndexSettingsDescriptor> SettingsDescriptorAction { get; set; }

	/// <summary>
	/// <para>Aliases for the index.</para>
	/// </summary>
	public CreateIndexRequestDescriptor Aliases(Func<FluentDescriptorDictionary<Elastic.Clients.Elasticsearch.Serverless.Name, Elastic.Clients.Elasticsearch.Serverless.IndexManagement.AliasDescriptor>, FluentDescriptorDictionary<Elastic.Clients.Elasticsearch.Serverless.Name, Elastic.Clients.Elasticsearch.Serverless.IndexManagement.AliasDescriptor>> selector)
	{
		AliasesValue = selector?.Invoke(new FluentDescriptorDictionary<Elastic.Clients.Elasticsearch.Serverless.Name, Elastic.Clients.Elasticsearch.Serverless.IndexManagement.AliasDescriptor>());
		return Self;
	}

	/// <summary>
	/// <para>Mapping for fields in the index. If specified, this mapping can include:<br/>- Field names<br/>- Field data types<br/>- Mapping parameters</para>
	/// </summary>
	public CreateIndexRequestDescriptor Mappings(Elastic.Clients.Elasticsearch.Serverless.Mapping.TypeMapping? mappings)
	{
		MappingsDescriptor = null;
		MappingsDescriptorAction = null;
		MappingsValue = mappings;
		return Self;
	}

	public CreateIndexRequestDescriptor Mappings(Elastic.Clients.Elasticsearch.Serverless.Mapping.TypeMappingDescriptor descriptor)
	{
		MappingsValue = null;
		MappingsDescriptorAction = null;
		MappingsDescriptor = descriptor;
		return Self;
	}

	public CreateIndexRequestDescriptor Mappings(Action<Elastic.Clients.Elasticsearch.Serverless.Mapping.TypeMappingDescriptor> configure)
	{
		MappingsValue = null;
		MappingsDescriptor = null;
		MappingsDescriptorAction = configure;
		return Self;
	}

	/// <summary>
	/// <para>Configuration options for the index.</para>
	/// </summary>
	public CreateIndexRequestDescriptor Settings(Elastic.Clients.Elasticsearch.Serverless.IndexManagement.IndexSettings? settings)
	{
		SettingsDescriptor = null;
		SettingsDescriptorAction = null;
		SettingsValue = settings;
		return Self;
	}

	public CreateIndexRequestDescriptor Settings(Elastic.Clients.Elasticsearch.Serverless.IndexManagement.IndexSettingsDescriptor descriptor)
	{
		SettingsValue = null;
		SettingsDescriptorAction = null;
		SettingsDescriptor = descriptor;
		return Self;
	}

	public CreateIndexRequestDescriptor Settings(Action<Elastic.Clients.Elasticsearch.Serverless.IndexManagement.IndexSettingsDescriptor> configure)
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
			JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.Serverless.Mapping.TypeMappingDescriptor(MappingsDescriptorAction), options);
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
			JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.Serverless.IndexManagement.IndexSettingsDescriptor(SettingsDescriptorAction), options);
		}
		else if (SettingsValue is not null)
		{
			writer.WritePropertyName("settings");
			JsonSerializer.Serialize(writer, SettingsValue, options);
		}

		writer.WriteEndObject();
	}
}