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

public sealed partial class Phases
{
	[JsonInclude, JsonPropertyName("cold")]
	public Elastic.Clients.Elasticsearch.IndexLifecycleManagement.Phase? Cold { get; set; }
	[JsonInclude, JsonPropertyName("delete")]
	public Elastic.Clients.Elasticsearch.IndexLifecycleManagement.Phase? Delete { get; set; }
	[JsonInclude, JsonPropertyName("frozen")]
	public Elastic.Clients.Elasticsearch.IndexLifecycleManagement.Phase? Frozen { get; set; }
	[JsonInclude, JsonPropertyName("hot")]
	public Elastic.Clients.Elasticsearch.IndexLifecycleManagement.Phase? Hot { get; set; }
	[JsonInclude, JsonPropertyName("warm")]
	public Elastic.Clients.Elasticsearch.IndexLifecycleManagement.Phase? Warm { get; set; }
}

public sealed partial class PhasesDescriptor : SerializableDescriptor<PhasesDescriptor>
{
	internal PhasesDescriptor(Action<PhasesDescriptor> configure) => configure.Invoke(this);

	public PhasesDescriptor() : base()
	{
	}

	private Elastic.Clients.Elasticsearch.IndexLifecycleManagement.Phase? ColdValue { get; set; }
	private Elastic.Clients.Elasticsearch.IndexLifecycleManagement.PhaseDescriptor ColdDescriptor { get; set; }
	private Action<Elastic.Clients.Elasticsearch.IndexLifecycleManagement.PhaseDescriptor> ColdDescriptorAction { get; set; }
	private Elastic.Clients.Elasticsearch.IndexLifecycleManagement.Phase? DeleteValue { get; set; }
	private Elastic.Clients.Elasticsearch.IndexLifecycleManagement.PhaseDescriptor DeleteDescriptor { get; set; }
	private Action<Elastic.Clients.Elasticsearch.IndexLifecycleManagement.PhaseDescriptor> DeleteDescriptorAction { get; set; }
	private Elastic.Clients.Elasticsearch.IndexLifecycleManagement.Phase? FrozenValue { get; set; }
	private Elastic.Clients.Elasticsearch.IndexLifecycleManagement.PhaseDescriptor FrozenDescriptor { get; set; }
	private Action<Elastic.Clients.Elasticsearch.IndexLifecycleManagement.PhaseDescriptor> FrozenDescriptorAction { get; set; }
	private Elastic.Clients.Elasticsearch.IndexLifecycleManagement.Phase? HotValue { get; set; }
	private Elastic.Clients.Elasticsearch.IndexLifecycleManagement.PhaseDescriptor HotDescriptor { get; set; }
	private Action<Elastic.Clients.Elasticsearch.IndexLifecycleManagement.PhaseDescriptor> HotDescriptorAction { get; set; }
	private Elastic.Clients.Elasticsearch.IndexLifecycleManagement.Phase? WarmValue { get; set; }
	private Elastic.Clients.Elasticsearch.IndexLifecycleManagement.PhaseDescriptor WarmDescriptor { get; set; }
	private Action<Elastic.Clients.Elasticsearch.IndexLifecycleManagement.PhaseDescriptor> WarmDescriptorAction { get; set; }

	public PhasesDescriptor Cold(Elastic.Clients.Elasticsearch.IndexLifecycleManagement.Phase? cold)
	{
		ColdDescriptor = null;
		ColdDescriptorAction = null;
		ColdValue = cold;
		return Self;
	}

	public PhasesDescriptor Cold(Elastic.Clients.Elasticsearch.IndexLifecycleManagement.PhaseDescriptor descriptor)
	{
		ColdValue = null;
		ColdDescriptorAction = null;
		ColdDescriptor = descriptor;
		return Self;
	}

	public PhasesDescriptor Cold(Action<Elastic.Clients.Elasticsearch.IndexLifecycleManagement.PhaseDescriptor> configure)
	{
		ColdValue = null;
		ColdDescriptor = null;
		ColdDescriptorAction = configure;
		return Self;
	}

	public PhasesDescriptor Delete(Elastic.Clients.Elasticsearch.IndexLifecycleManagement.Phase? delete)
	{
		DeleteDescriptor = null;
		DeleteDescriptorAction = null;
		DeleteValue = delete;
		return Self;
	}

	public PhasesDescriptor Delete(Elastic.Clients.Elasticsearch.IndexLifecycleManagement.PhaseDescriptor descriptor)
	{
		DeleteValue = null;
		DeleteDescriptorAction = null;
		DeleteDescriptor = descriptor;
		return Self;
	}

	public PhasesDescriptor Delete(Action<Elastic.Clients.Elasticsearch.IndexLifecycleManagement.PhaseDescriptor> configure)
	{
		DeleteValue = null;
		DeleteDescriptor = null;
		DeleteDescriptorAction = configure;
		return Self;
	}

	public PhasesDescriptor Frozen(Elastic.Clients.Elasticsearch.IndexLifecycleManagement.Phase? frozen)
	{
		FrozenDescriptor = null;
		FrozenDescriptorAction = null;
		FrozenValue = frozen;
		return Self;
	}

	public PhasesDescriptor Frozen(Elastic.Clients.Elasticsearch.IndexLifecycleManagement.PhaseDescriptor descriptor)
	{
		FrozenValue = null;
		FrozenDescriptorAction = null;
		FrozenDescriptor = descriptor;
		return Self;
	}

	public PhasesDescriptor Frozen(Action<Elastic.Clients.Elasticsearch.IndexLifecycleManagement.PhaseDescriptor> configure)
	{
		FrozenValue = null;
		FrozenDescriptor = null;
		FrozenDescriptorAction = configure;
		return Self;
	}

	public PhasesDescriptor Hot(Elastic.Clients.Elasticsearch.IndexLifecycleManagement.Phase? hot)
	{
		HotDescriptor = null;
		HotDescriptorAction = null;
		HotValue = hot;
		return Self;
	}

	public PhasesDescriptor Hot(Elastic.Clients.Elasticsearch.IndexLifecycleManagement.PhaseDescriptor descriptor)
	{
		HotValue = null;
		HotDescriptorAction = null;
		HotDescriptor = descriptor;
		return Self;
	}

	public PhasesDescriptor Hot(Action<Elastic.Clients.Elasticsearch.IndexLifecycleManagement.PhaseDescriptor> configure)
	{
		HotValue = null;
		HotDescriptor = null;
		HotDescriptorAction = configure;
		return Self;
	}

	public PhasesDescriptor Warm(Elastic.Clients.Elasticsearch.IndexLifecycleManagement.Phase? warm)
	{
		WarmDescriptor = null;
		WarmDescriptorAction = null;
		WarmValue = warm;
		return Self;
	}

	public PhasesDescriptor Warm(Elastic.Clients.Elasticsearch.IndexLifecycleManagement.PhaseDescriptor descriptor)
	{
		WarmValue = null;
		WarmDescriptorAction = null;
		WarmDescriptor = descriptor;
		return Self;
	}

	public PhasesDescriptor Warm(Action<Elastic.Clients.Elasticsearch.IndexLifecycleManagement.PhaseDescriptor> configure)
	{
		WarmValue = null;
		WarmDescriptor = null;
		WarmDescriptorAction = configure;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (ColdDescriptor is not null)
		{
			writer.WritePropertyName("cold");
			JsonSerializer.Serialize(writer, ColdDescriptor, options);
		}
		else if (ColdDescriptorAction is not null)
		{
			writer.WritePropertyName("cold");
			JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.IndexLifecycleManagement.PhaseDescriptor(ColdDescriptorAction), options);
		}
		else if (ColdValue is not null)
		{
			writer.WritePropertyName("cold");
			JsonSerializer.Serialize(writer, ColdValue, options);
		}

		if (DeleteDescriptor is not null)
		{
			writer.WritePropertyName("delete");
			JsonSerializer.Serialize(writer, DeleteDescriptor, options);
		}
		else if (DeleteDescriptorAction is not null)
		{
			writer.WritePropertyName("delete");
			JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.IndexLifecycleManagement.PhaseDescriptor(DeleteDescriptorAction), options);
		}
		else if (DeleteValue is not null)
		{
			writer.WritePropertyName("delete");
			JsonSerializer.Serialize(writer, DeleteValue, options);
		}

		if (FrozenDescriptor is not null)
		{
			writer.WritePropertyName("frozen");
			JsonSerializer.Serialize(writer, FrozenDescriptor, options);
		}
		else if (FrozenDescriptorAction is not null)
		{
			writer.WritePropertyName("frozen");
			JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.IndexLifecycleManagement.PhaseDescriptor(FrozenDescriptorAction), options);
		}
		else if (FrozenValue is not null)
		{
			writer.WritePropertyName("frozen");
			JsonSerializer.Serialize(writer, FrozenValue, options);
		}

		if (HotDescriptor is not null)
		{
			writer.WritePropertyName("hot");
			JsonSerializer.Serialize(writer, HotDescriptor, options);
		}
		else if (HotDescriptorAction is not null)
		{
			writer.WritePropertyName("hot");
			JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.IndexLifecycleManagement.PhaseDescriptor(HotDescriptorAction), options);
		}
		else if (HotValue is not null)
		{
			writer.WritePropertyName("hot");
			JsonSerializer.Serialize(writer, HotValue, options);
		}

		if (WarmDescriptor is not null)
		{
			writer.WritePropertyName("warm");
			JsonSerializer.Serialize(writer, WarmDescriptor, options);
		}
		else if (WarmDescriptorAction is not null)
		{
			writer.WritePropertyName("warm");
			JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.IndexLifecycleManagement.PhaseDescriptor(WarmDescriptorAction), options);
		}
		else if (WarmValue is not null)
		{
			writer.WritePropertyName("warm");
			JsonSerializer.Serialize(writer, WarmValue, options);
		}

		writer.WriteEndObject();
	}
}