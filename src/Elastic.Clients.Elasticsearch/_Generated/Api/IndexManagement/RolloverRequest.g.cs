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

using Elastic.Clients.Elasticsearch.Fluent;
using Elastic.Clients.Elasticsearch.Requests;
using Elastic.Clients.Elasticsearch.Serialization;
using Elastic.Transport;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;

#nullable restore
namespace Elastic.Clients.Elasticsearch.IndexManagement;
public sealed class RolloverRequestParameters : RequestParameters
{
	[JsonIgnore]
	public bool? DryRun { get => Q<bool?>("dry_run"); set => Q("dry_run", value); }

	[JsonIgnore]
	public Elastic.Clients.Elasticsearch.Duration? MasterTimeout { get => Q<Elastic.Clients.Elasticsearch.Duration?>("master_timeout"); set => Q("master_timeout", value); }

	[JsonIgnore]
	public Elastic.Clients.Elasticsearch.Duration? Timeout { get => Q<Elastic.Clients.Elasticsearch.Duration?>("timeout"); set => Q("timeout", value); }

	[JsonIgnore]
	public Elastic.Clients.Elasticsearch.WaitForActiveShards? WaitForActiveShards { get => Q<Elastic.Clients.Elasticsearch.WaitForActiveShards?>("wait_for_active_shards"); set => Q("wait_for_active_shards", value); }
}

public sealed partial class RolloverRequest : PlainRequest<RolloverRequestParameters>
{
	public RolloverRequest(Elastic.Clients.Elasticsearch.IndexAlias alias) : base(r => r.Required("alias", alias))
	{
	}

	public RolloverRequest(Elastic.Clients.Elasticsearch.IndexAlias alias, Elastic.Clients.Elasticsearch.IndexName? new_index) : base(r => r.Required("alias", alias).Optional("new_index", new_index))
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlsLookups.IndexManagementRollover;
	protected override HttpMethod StaticHttpMethod => HttpMethod.POST;
	internal override bool SupportsBody => true;
	[JsonIgnore]
	public bool? DryRun { get => Q<bool?>("dry_run"); set => Q("dry_run", value); }

	[JsonIgnore]
	public Elastic.Clients.Elasticsearch.Duration? MasterTimeout { get => Q<Elastic.Clients.Elasticsearch.Duration?>("master_timeout"); set => Q("master_timeout", value); }

	[JsonIgnore]
	public Elastic.Clients.Elasticsearch.Duration? Timeout { get => Q<Elastic.Clients.Elasticsearch.Duration?>("timeout"); set => Q("timeout", value); }

	[JsonIgnore]
	public Elastic.Clients.Elasticsearch.WaitForActiveShards? WaitForActiveShards { get => Q<Elastic.Clients.Elasticsearch.WaitForActiveShards?>("wait_for_active_shards"); set => Q("wait_for_active_shards", value); }

	[JsonInclude]
	[JsonPropertyName("aliases")]
	public IDictionary<Elastic.Clients.Elasticsearch.IndexName, Elastic.Clients.Elasticsearch.IndexManagement.Alias>? Aliases { get; set; }

	[JsonInclude]
	[JsonPropertyName("conditions")]
	public Elastic.Clients.Elasticsearch.IndexManagement.RolloverConditions? Conditions { get; set; }

	[JsonInclude]
	[JsonPropertyName("mappings")]
	public Elastic.Clients.Elasticsearch.Mapping.TypeMapping? Mappings { get; set; }

	[JsonInclude]
	[JsonPropertyName("settings")]
	public IDictionary<string, object>? Settings { get; set; }
}

public sealed partial class RolloverRequestDescriptor : RequestDescriptor<RolloverRequestDescriptor, RolloverRequestParameters>
{
	internal RolloverRequestDescriptor(Action<RolloverRequestDescriptor> configure) => configure.Invoke(this);
	public RolloverRequestDescriptor(Elastic.Clients.Elasticsearch.IndexAlias alias) : base(r => r.Required("alias", alias))
	{
	}

	public RolloverRequestDescriptor(Elastic.Clients.Elasticsearch.IndexAlias alias, Elastic.Clients.Elasticsearch.IndexName? new_index) : base(r => r.Required("alias", alias).Optional("new_index", new_index))
	{
	}

	internal RolloverRequestDescriptor()
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlsLookups.IndexManagementRollover;
	protected override HttpMethod StaticHttpMethod => HttpMethod.POST;
	internal override bool SupportsBody => true;
	public RolloverRequestDescriptor DryRun(bool? dryRun = true) => Qs("dry_run", dryRun);
	public RolloverRequestDescriptor MasterTimeout(Elastic.Clients.Elasticsearch.Duration? masterTimeout) => Qs("master_timeout", masterTimeout);
	public RolloverRequestDescriptor Timeout(Elastic.Clients.Elasticsearch.Duration? timeout) => Qs("timeout", timeout);
	public RolloverRequestDescriptor WaitForActiveShards(Elastic.Clients.Elasticsearch.WaitForActiveShards? waitForActiveShards) => Qs("wait_for_active_shards", waitForActiveShards);
	public RolloverRequestDescriptor Alias(Elastic.Clients.Elasticsearch.IndexAlias alias)
	{
		RouteValues.Required("alias", alias);
		return Self;
	}

	public RolloverRequestDescriptor NewIndex(Elastic.Clients.Elasticsearch.IndexName? new_index)
	{
		RouteValues.Optional("new_index", new_index);
		return Self;
	}

	private IDictionary<Elastic.Clients.Elasticsearch.IndexName, Elastic.Clients.Elasticsearch.IndexManagement.Alias>? AliasesValue { get; set; }

	private Elastic.Clients.Elasticsearch.IndexManagement.RolloverConditions? ConditionsValue { get; set; }

	private RolloverConditionsDescriptor ConditionsDescriptor { get; set; }

	private Action<RolloverConditionsDescriptor> ConditionsDescriptorAction { get; set; }

	private Elastic.Clients.Elasticsearch.Mapping.TypeMapping? MappingsValue { get; set; }

	private Mapping.TypeMappingDescriptor MappingsDescriptor { get; set; }

	private Action<Mapping.TypeMappingDescriptor> MappingsDescriptorAction { get; set; }

	private IDictionary<string, object>? SettingsValue { get; set; }

	public RolloverRequestDescriptor Aliases(Func<FluentDictionary<Elastic.Clients.Elasticsearch.IndexName, Elastic.Clients.Elasticsearch.IndexManagement.Alias>, FluentDictionary<Elastic.Clients.Elasticsearch.IndexName, Elastic.Clients.Elasticsearch.IndexManagement.Alias>> selector)
	{
		AliasesValue = selector?.Invoke(new FluentDictionary<Elastic.Clients.Elasticsearch.IndexName, Elastic.Clients.Elasticsearch.IndexManagement.Alias>());
		return Self;
	}

	public RolloverRequestDescriptor Conditions(Elastic.Clients.Elasticsearch.IndexManagement.RolloverConditions? conditions)
	{
		ConditionsDescriptor = null;
		ConditionsDescriptorAction = null;
		ConditionsValue = conditions;
		return Self;
	}

	public RolloverRequestDescriptor Conditions(RolloverConditionsDescriptor descriptor)
	{
		ConditionsValue = null;
		ConditionsDescriptorAction = null;
		ConditionsDescriptor = descriptor;
		return Self;
	}

	public RolloverRequestDescriptor Conditions(Action<RolloverConditionsDescriptor> configure)
	{
		ConditionsValue = null;
		ConditionsDescriptor = null;
		ConditionsDescriptorAction = configure;
		return Self;
	}

	public RolloverRequestDescriptor Mappings(Elastic.Clients.Elasticsearch.Mapping.TypeMapping? mappings)
	{
		MappingsDescriptor = null;
		MappingsDescriptorAction = null;
		MappingsValue = mappings;
		return Self;
	}

	public RolloverRequestDescriptor Mappings(Mapping.TypeMappingDescriptor descriptor)
	{
		MappingsValue = null;
		MappingsDescriptorAction = null;
		MappingsDescriptor = descriptor;
		return Self;
	}

	public RolloverRequestDescriptor Mappings(Action<Mapping.TypeMappingDescriptor> configure)
	{
		MappingsValue = null;
		MappingsDescriptor = null;
		MappingsDescriptorAction = configure;
		return Self;
	}

	public RolloverRequestDescriptor Settings(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> selector)
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

		if (ConditionsDescriptor is not null)
		{
			writer.WritePropertyName("conditions");
			JsonSerializer.Serialize(writer, ConditionsDescriptor, options);
		}
		else if (ConditionsDescriptorAction is not null)
		{
			writer.WritePropertyName("conditions");
			JsonSerializer.Serialize(writer, new RolloverConditionsDescriptor(ConditionsDescriptorAction), options);
		}
		else if (ConditionsValue is not null)
		{
			writer.WritePropertyName("conditions");
			JsonSerializer.Serialize(writer, ConditionsValue, options);
		}

		if (MappingsDescriptor is not null)
		{
			writer.WritePropertyName("mappings");
			JsonSerializer.Serialize(writer, MappingsDescriptor, options);
		}
		else if (MappingsDescriptorAction is not null)
		{
			writer.WritePropertyName("mappings");
			JsonSerializer.Serialize(writer, new Mapping.TypeMappingDescriptor(MappingsDescriptorAction), options);
		}
		else if (MappingsValue is not null)
		{
			writer.WritePropertyName("mappings");
			JsonSerializer.Serialize(writer, MappingsValue, options);
		}

		if (SettingsValue is not null)
		{
			writer.WritePropertyName("settings");
			JsonSerializer.Serialize(writer, SettingsValue, options);
		}

		writer.WriteEndObject();
	}
}