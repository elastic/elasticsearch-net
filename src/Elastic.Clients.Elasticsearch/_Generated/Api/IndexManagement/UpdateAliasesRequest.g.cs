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
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch.IndexManagement;

public sealed class UpdateAliasesRequestParameters : RequestParameters
{
	[JsonIgnore]
	public Elastic.Clients.Elasticsearch.Duration? MasterTimeout { get => Q<Elastic.Clients.Elasticsearch.Duration?>("master_timeout"); set => Q("master_timeout", value); }
	[JsonIgnore]
	public Elastic.Clients.Elasticsearch.Duration? Timeout { get => Q<Elastic.Clients.Elasticsearch.Duration?>("timeout"); set => Q("timeout", value); }
}

public sealed partial class UpdateAliasesRequest : PlainRequest<UpdateAliasesRequestParameters>
{
	internal override ApiUrls ApiUrls => ApiUrlsLookups.IndexManagementUpdateAliases;

	protected override HttpMethod StaticHttpMethod => HttpMethod.POST;

	internal override bool SupportsBody => true;

	[JsonIgnore]
	public Elastic.Clients.Elasticsearch.Duration? MasterTimeout { get => Q<Elastic.Clients.Elasticsearch.Duration?>("master_timeout"); set => Q("master_timeout", value); }
	[JsonIgnore]
	public Elastic.Clients.Elasticsearch.Duration? Timeout { get => Q<Elastic.Clients.Elasticsearch.Duration?>("timeout"); set => Q("timeout", value); }
	[JsonInclude, JsonPropertyName("actions")]
	public ICollection<Elastic.Clients.Elasticsearch.IndexManagement.Action>? Actions { get; set; }
}

public sealed partial class UpdateAliasesRequestDescriptor<TDocument> : RequestDescriptor<UpdateAliasesRequestDescriptor<TDocument>, UpdateAliasesRequestParameters>
{
	internal UpdateAliasesRequestDescriptor(Action<UpdateAliasesRequestDescriptor<TDocument>> configure) => configure.Invoke(this);

	public UpdateAliasesRequestDescriptor()
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlsLookups.IndexManagementUpdateAliases;

	protected override HttpMethod StaticHttpMethod => HttpMethod.POST;

	internal override bool SupportsBody => true;

	public UpdateAliasesRequestDescriptor<TDocument> MasterTimeout(Elastic.Clients.Elasticsearch.Duration? masterTimeout) => Qs("master_timeout", masterTimeout);
	public UpdateAliasesRequestDescriptor<TDocument> Timeout(Elastic.Clients.Elasticsearch.Duration? timeout) => Qs("timeout", timeout);

	private ICollection<Elastic.Clients.Elasticsearch.IndexManagement.Action>? ActionsValue { get; set; }
	private ActionDescriptor ActionsDescriptor { get; set; }
	private Action<ActionDescriptor> ActionsDescriptorAction { get; set; }
	private Action<ActionDescriptor>[] ActionsDescriptorActions { get; set; }

	public UpdateAliasesRequestDescriptor<TDocument> Actions(ICollection<Elastic.Clients.Elasticsearch.IndexManagement.Action>? actions)
	{
		ActionsDescriptor = null;
		ActionsDescriptorAction = null;
		ActionsDescriptorActions = null;
		ActionsValue = actions;
		return Self;
	}

	public UpdateAliasesRequestDescriptor<TDocument> Actions(ActionDescriptor descriptor)
	{
		ActionsValue = null;
		ActionsDescriptorAction = null;
		ActionsDescriptorActions = null;
		ActionsDescriptor = descriptor;
		return Self;
	}

	public UpdateAliasesRequestDescriptor<TDocument> Actions(Action<ActionDescriptor> configure)
	{
		ActionsValue = null;
		ActionsDescriptor = null;
		ActionsDescriptorActions = null;
		ActionsDescriptorAction = configure;
		return Self;
	}

	public UpdateAliasesRequestDescriptor<TDocument> Actions(params Action<ActionDescriptor>[] configure)
	{
		ActionsValue = null;
		ActionsDescriptor = null;
		ActionsDescriptorAction = null;
		ActionsDescriptorActions = configure;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (ActionsDescriptor is not null)
		{
			writer.WritePropertyName("actions");
			writer.WriteStartArray();
			JsonSerializer.Serialize(writer, ActionsDescriptor, options);
			writer.WriteEndArray();
		}
		else if (ActionsDescriptorAction is not null)
		{
			writer.WritePropertyName("actions");
			writer.WriteStartArray();
			JsonSerializer.Serialize(writer, new ActionDescriptor(ActionsDescriptorAction), options);
			writer.WriteEndArray();
		}
		else if (ActionsDescriptorActions is not null)
		{
			writer.WritePropertyName("actions");
			writer.WriteStartArray();
			foreach (var action in ActionsDescriptorActions)
			{
				JsonSerializer.Serialize(writer, new ActionDescriptor(action), options);
			}

			writer.WriteEndArray();
		}
		else if (ActionsValue is not null)
		{
			writer.WritePropertyName("actions");
			JsonSerializer.Serialize(writer, ActionsValue, options);
		}

		writer.WriteEndObject();
	}
}

public sealed partial class UpdateAliasesRequestDescriptor : RequestDescriptor<UpdateAliasesRequestDescriptor, UpdateAliasesRequestParameters>
{
	internal UpdateAliasesRequestDescriptor(Action<UpdateAliasesRequestDescriptor> configure) => configure.Invoke(this);

	public UpdateAliasesRequestDescriptor()
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlsLookups.IndexManagementUpdateAliases;

	protected override HttpMethod StaticHttpMethod => HttpMethod.POST;

	internal override bool SupportsBody => true;

	public UpdateAliasesRequestDescriptor MasterTimeout(Elastic.Clients.Elasticsearch.Duration? masterTimeout) => Qs("master_timeout", masterTimeout);
	public UpdateAliasesRequestDescriptor Timeout(Elastic.Clients.Elasticsearch.Duration? timeout) => Qs("timeout", timeout);

	private ICollection<Elastic.Clients.Elasticsearch.IndexManagement.Action>? ActionsValue { get; set; }
	private ActionDescriptor ActionsDescriptor { get; set; }
	private Action<ActionDescriptor> ActionsDescriptorAction { get; set; }
	private Action<ActionDescriptor>[] ActionsDescriptorActions { get; set; }

	public UpdateAliasesRequestDescriptor Actions(ICollection<Elastic.Clients.Elasticsearch.IndexManagement.Action>? actions)
	{
		ActionsDescriptor = null;
		ActionsDescriptorAction = null;
		ActionsDescriptorActions = null;
		ActionsValue = actions;
		return Self;
	}

	public UpdateAliasesRequestDescriptor Actions(ActionDescriptor descriptor)
	{
		ActionsValue = null;
		ActionsDescriptorAction = null;
		ActionsDescriptorActions = null;
		ActionsDescriptor = descriptor;
		return Self;
	}

	public UpdateAliasesRequestDescriptor Actions(Action<ActionDescriptor> configure)
	{
		ActionsValue = null;
		ActionsDescriptor = null;
		ActionsDescriptorActions = null;
		ActionsDescriptorAction = configure;
		return Self;
	}

	public UpdateAliasesRequestDescriptor Actions(params Action<ActionDescriptor>[] configure)
	{
		ActionsValue = null;
		ActionsDescriptor = null;
		ActionsDescriptorAction = null;
		ActionsDescriptorActions = configure;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (ActionsDescriptor is not null)
		{
			writer.WritePropertyName("actions");
			writer.WriteStartArray();
			JsonSerializer.Serialize(writer, ActionsDescriptor, options);
			writer.WriteEndArray();
		}
		else if (ActionsDescriptorAction is not null)
		{
			writer.WritePropertyName("actions");
			writer.WriteStartArray();
			JsonSerializer.Serialize(writer, new ActionDescriptor(ActionsDescriptorAction), options);
			writer.WriteEndArray();
		}
		else if (ActionsDescriptorActions is not null)
		{
			writer.WritePropertyName("actions");
			writer.WriteStartArray();
			foreach (var action in ActionsDescriptorActions)
			{
				JsonSerializer.Serialize(writer, new ActionDescriptor(action), options);
			}

			writer.WriteEndArray();
		}
		else if (ActionsValue is not null)
		{
			writer.WritePropertyName("actions");
			JsonSerializer.Serialize(writer, ActionsValue, options);
		}

		writer.WriteEndObject();
	}
}