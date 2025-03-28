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

public sealed partial class IlmPolicy
{
	/// <summary>
	/// <para>
	/// Arbitrary metadata that is not automatically generated or used by Elasticsearch.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("_meta")]
	public IDictionary<string, object>? Meta { get; set; }
	[JsonInclude, JsonPropertyName("phases")]
	public Elastic.Clients.Elasticsearch.IndexLifecycleManagement.Phases Phases { get; set; }
}

public sealed partial class IlmPolicyDescriptor : SerializableDescriptor<IlmPolicyDescriptor>
{
	internal IlmPolicyDescriptor(Action<IlmPolicyDescriptor> configure) => configure.Invoke(this);

	public IlmPolicyDescriptor() : base()
	{
	}

	private IDictionary<string, object>? MetaValue { get; set; }
	private Elastic.Clients.Elasticsearch.IndexLifecycleManagement.Phases PhasesValue { get; set; }
	private Elastic.Clients.Elasticsearch.IndexLifecycleManagement.PhasesDescriptor PhasesDescriptor { get; set; }
	private Action<Elastic.Clients.Elasticsearch.IndexLifecycleManagement.PhasesDescriptor> PhasesDescriptorAction { get; set; }

	/// <summary>
	/// <para>
	/// Arbitrary metadata that is not automatically generated or used by Elasticsearch.
	/// </para>
	/// </summary>
	public IlmPolicyDescriptor Meta(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> selector)
	{
		MetaValue = selector?.Invoke(new FluentDictionary<string, object>());
		return Self;
	}

	public IlmPolicyDescriptor Phases(Elastic.Clients.Elasticsearch.IndexLifecycleManagement.Phases phases)
	{
		PhasesDescriptor = null;
		PhasesDescriptorAction = null;
		PhasesValue = phases;
		return Self;
	}

	public IlmPolicyDescriptor Phases(Elastic.Clients.Elasticsearch.IndexLifecycleManagement.PhasesDescriptor descriptor)
	{
		PhasesValue = null;
		PhasesDescriptorAction = null;
		PhasesDescriptor = descriptor;
		return Self;
	}

	public IlmPolicyDescriptor Phases(Action<Elastic.Clients.Elasticsearch.IndexLifecycleManagement.PhasesDescriptor> configure)
	{
		PhasesValue = null;
		PhasesDescriptor = null;
		PhasesDescriptorAction = configure;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (MetaValue is not null)
		{
			writer.WritePropertyName("_meta");
			JsonSerializer.Serialize(writer, MetaValue, options);
		}

		if (PhasesDescriptor is not null)
		{
			writer.WritePropertyName("phases");
			JsonSerializer.Serialize(writer, PhasesDescriptor, options);
		}
		else if (PhasesDescriptorAction is not null)
		{
			writer.WritePropertyName("phases");
			JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.IndexLifecycleManagement.PhasesDescriptor(PhasesDescriptorAction), options);
		}
		else
		{
			writer.WritePropertyName("phases");
			JsonSerializer.Serialize(writer, PhasesValue, options);
		}

		writer.WriteEndObject();
	}
}