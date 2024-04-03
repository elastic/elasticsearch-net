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
using Elastic.Clients.Elasticsearch.Serialization;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch.IndexLifecycleManagement;

public sealed partial class Configurations
{
	[JsonInclude, JsonPropertyName("forcemerge")]
	public Elastic.Clients.Elasticsearch.IndexLifecycleManagement.ForceMergeConfiguration? Forcemerge { get; set; }
	[JsonInclude, JsonPropertyName("rollover")]
	public Elastic.Clients.Elasticsearch.IndexManagement.RolloverConditions? Rollover { get; set; }
	[JsonInclude, JsonPropertyName("shrink")]
	public Elastic.Clients.Elasticsearch.IndexLifecycleManagement.ShrinkConfiguration? Shrink { get; set; }
}

public sealed partial class ConfigurationsDescriptor : SerializableDescriptor<ConfigurationsDescriptor>
{
	internal ConfigurationsDescriptor(Action<ConfigurationsDescriptor> configure) => configure.Invoke(this);

	public ConfigurationsDescriptor() : base()
	{
	}

	private Elastic.Clients.Elasticsearch.IndexLifecycleManagement.ForceMergeConfiguration? ForcemergeValue { get; set; }
	private Elastic.Clients.Elasticsearch.IndexLifecycleManagement.ForceMergeConfigurationDescriptor ForcemergeDescriptor { get; set; }
	private Action<Elastic.Clients.Elasticsearch.IndexLifecycleManagement.ForceMergeConfigurationDescriptor> ForcemergeDescriptorAction { get; set; }
	private Elastic.Clients.Elasticsearch.IndexManagement.RolloverConditions? RolloverValue { get; set; }
	private Elastic.Clients.Elasticsearch.IndexManagement.RolloverConditionsDescriptor RolloverDescriptor { get; set; }
	private Action<Elastic.Clients.Elasticsearch.IndexManagement.RolloverConditionsDescriptor> RolloverDescriptorAction { get; set; }
	private Elastic.Clients.Elasticsearch.IndexLifecycleManagement.ShrinkConfiguration? ShrinkValue { get; set; }
	private Elastic.Clients.Elasticsearch.IndexLifecycleManagement.ShrinkConfigurationDescriptor ShrinkDescriptor { get; set; }
	private Action<Elastic.Clients.Elasticsearch.IndexLifecycleManagement.ShrinkConfigurationDescriptor> ShrinkDescriptorAction { get; set; }

	public ConfigurationsDescriptor Forcemerge(Elastic.Clients.Elasticsearch.IndexLifecycleManagement.ForceMergeConfiguration? forcemerge)
	{
		ForcemergeDescriptor = null;
		ForcemergeDescriptorAction = null;
		ForcemergeValue = forcemerge;
		return Self;
	}

	public ConfigurationsDescriptor Forcemerge(Elastic.Clients.Elasticsearch.IndexLifecycleManagement.ForceMergeConfigurationDescriptor descriptor)
	{
		ForcemergeValue = null;
		ForcemergeDescriptorAction = null;
		ForcemergeDescriptor = descriptor;
		return Self;
	}

	public ConfigurationsDescriptor Forcemerge(Action<Elastic.Clients.Elasticsearch.IndexLifecycleManagement.ForceMergeConfigurationDescriptor> configure)
	{
		ForcemergeValue = null;
		ForcemergeDescriptor = null;
		ForcemergeDescriptorAction = configure;
		return Self;
	}

	public ConfigurationsDescriptor Rollover(Elastic.Clients.Elasticsearch.IndexManagement.RolloverConditions? rollover)
	{
		RolloverDescriptor = null;
		RolloverDescriptorAction = null;
		RolloverValue = rollover;
		return Self;
	}

	public ConfigurationsDescriptor Rollover(Elastic.Clients.Elasticsearch.IndexManagement.RolloverConditionsDescriptor descriptor)
	{
		RolloverValue = null;
		RolloverDescriptorAction = null;
		RolloverDescriptor = descriptor;
		return Self;
	}

	public ConfigurationsDescriptor Rollover(Action<Elastic.Clients.Elasticsearch.IndexManagement.RolloverConditionsDescriptor> configure)
	{
		RolloverValue = null;
		RolloverDescriptor = null;
		RolloverDescriptorAction = configure;
		return Self;
	}

	public ConfigurationsDescriptor Shrink(Elastic.Clients.Elasticsearch.IndexLifecycleManagement.ShrinkConfiguration? shrink)
	{
		ShrinkDescriptor = null;
		ShrinkDescriptorAction = null;
		ShrinkValue = shrink;
		return Self;
	}

	public ConfigurationsDescriptor Shrink(Elastic.Clients.Elasticsearch.IndexLifecycleManagement.ShrinkConfigurationDescriptor descriptor)
	{
		ShrinkValue = null;
		ShrinkDescriptorAction = null;
		ShrinkDescriptor = descriptor;
		return Self;
	}

	public ConfigurationsDescriptor Shrink(Action<Elastic.Clients.Elasticsearch.IndexLifecycleManagement.ShrinkConfigurationDescriptor> configure)
	{
		ShrinkValue = null;
		ShrinkDescriptor = null;
		ShrinkDescriptorAction = configure;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (ForcemergeDescriptor is not null)
		{
			writer.WritePropertyName("forcemerge");
			JsonSerializer.Serialize(writer, ForcemergeDescriptor, options);
		}
		else if (ForcemergeDescriptorAction is not null)
		{
			writer.WritePropertyName("forcemerge");
			JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.IndexLifecycleManagement.ForceMergeConfigurationDescriptor(ForcemergeDescriptorAction), options);
		}
		else if (ForcemergeValue is not null)
		{
			writer.WritePropertyName("forcemerge");
			JsonSerializer.Serialize(writer, ForcemergeValue, options);
		}

		if (RolloverDescriptor is not null)
		{
			writer.WritePropertyName("rollover");
			JsonSerializer.Serialize(writer, RolloverDescriptor, options);
		}
		else if (RolloverDescriptorAction is not null)
		{
			writer.WritePropertyName("rollover");
			JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.IndexManagement.RolloverConditionsDescriptor(RolloverDescriptorAction), options);
		}
		else if (RolloverValue is not null)
		{
			writer.WritePropertyName("rollover");
			JsonSerializer.Serialize(writer, RolloverValue, options);
		}

		if (ShrinkDescriptor is not null)
		{
			writer.WritePropertyName("shrink");
			JsonSerializer.Serialize(writer, ShrinkDescriptor, options);
		}
		else if (ShrinkDescriptorAction is not null)
		{
			writer.WritePropertyName("shrink");
			JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.IndexLifecycleManagement.ShrinkConfigurationDescriptor(ShrinkDescriptorAction), options);
		}
		else if (ShrinkValue is not null)
		{
			writer.WritePropertyName("shrink");
			JsonSerializer.Serialize(writer, ShrinkValue, options);
		}

		writer.WriteEndObject();
	}
}