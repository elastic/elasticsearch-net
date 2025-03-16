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

namespace Elastic.Clients.Elasticsearch.QueryRules;

public sealed partial class QueryRuleActions
{
	/// <summary>
	/// <para>
	/// The documents to apply the rule to.
	/// Only one of <c>ids</c> or <c>docs</c> may be specified and at least one must be specified.
	/// There is a maximum value of 100 documents in a rule.
	/// You can specify the following attributes for each document:
	/// </para>
	/// <list type="bullet">
	/// <item>
	/// <para>
	/// <c>_index</c>: The index of the document to pin.
	/// </para>
	/// </item>
	/// <item>
	/// <para>
	/// <c>_id</c>: The unique document ID.
	/// </para>
	/// </item>
	/// </list>
	/// </summary>
	[JsonInclude, JsonPropertyName("docs")]
	public ICollection<Elastic.Clients.Elasticsearch.QueryDsl.PinnedDoc>? Docs { get; set; }

	/// <summary>
	/// <para>
	/// The unique document IDs of the documents to apply the rule to.
	/// Only one of <c>ids</c> or <c>docs</c> may be specified and at least one must be specified.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("ids")]
	public ICollection<Elastic.Clients.Elasticsearch.Id>? Ids { get; set; }
}

public sealed partial class QueryRuleActionsDescriptor : SerializableDescriptor<QueryRuleActionsDescriptor>
{
	internal QueryRuleActionsDescriptor(Action<QueryRuleActionsDescriptor> configure) => configure.Invoke(this);

	public QueryRuleActionsDescriptor() : base()
	{
	}

	private ICollection<Elastic.Clients.Elasticsearch.QueryDsl.PinnedDoc>? DocsValue { get; set; }
	private Elastic.Clients.Elasticsearch.QueryDsl.PinnedDocDescriptor DocsDescriptor { get; set; }
	private Action<Elastic.Clients.Elasticsearch.QueryDsl.PinnedDocDescriptor> DocsDescriptorAction { get; set; }
	private Action<Elastic.Clients.Elasticsearch.QueryDsl.PinnedDocDescriptor>[] DocsDescriptorActions { get; set; }
	private ICollection<Elastic.Clients.Elasticsearch.Id>? IdsValue { get; set; }

	/// <summary>
	/// <para>
	/// The documents to apply the rule to.
	/// Only one of <c>ids</c> or <c>docs</c> may be specified and at least one must be specified.
	/// There is a maximum value of 100 documents in a rule.
	/// You can specify the following attributes for each document:
	/// </para>
	/// <list type="bullet">
	/// <item>
	/// <para>
	/// <c>_index</c>: The index of the document to pin.
	/// </para>
	/// </item>
	/// <item>
	/// <para>
	/// <c>_id</c>: The unique document ID.
	/// </para>
	/// </item>
	/// </list>
	/// </summary>
	public QueryRuleActionsDescriptor Docs(ICollection<Elastic.Clients.Elasticsearch.QueryDsl.PinnedDoc>? docs)
	{
		DocsDescriptor = null;
		DocsDescriptorAction = null;
		DocsDescriptorActions = null;
		DocsValue = docs;
		return Self;
	}

	public QueryRuleActionsDescriptor Docs(Elastic.Clients.Elasticsearch.QueryDsl.PinnedDocDescriptor descriptor)
	{
		DocsValue = null;
		DocsDescriptorAction = null;
		DocsDescriptorActions = null;
		DocsDescriptor = descriptor;
		return Self;
	}

	public QueryRuleActionsDescriptor Docs(Action<Elastic.Clients.Elasticsearch.QueryDsl.PinnedDocDescriptor> configure)
	{
		DocsValue = null;
		DocsDescriptor = null;
		DocsDescriptorActions = null;
		DocsDescriptorAction = configure;
		return Self;
	}

	public QueryRuleActionsDescriptor Docs(params Action<Elastic.Clients.Elasticsearch.QueryDsl.PinnedDocDescriptor>[] configure)
	{
		DocsValue = null;
		DocsDescriptor = null;
		DocsDescriptorAction = null;
		DocsDescriptorActions = configure;
		return Self;
	}

	/// <summary>
	/// <para>
	/// The unique document IDs of the documents to apply the rule to.
	/// Only one of <c>ids</c> or <c>docs</c> may be specified and at least one must be specified.
	/// </para>
	/// </summary>
	public QueryRuleActionsDescriptor Ids(ICollection<Elastic.Clients.Elasticsearch.Id>? ids)
	{
		IdsValue = ids;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (DocsDescriptor is not null)
		{
			writer.WritePropertyName("docs");
			writer.WriteStartArray();
			JsonSerializer.Serialize(writer, DocsDescriptor, options);
			writer.WriteEndArray();
		}
		else if (DocsDescriptorAction is not null)
		{
			writer.WritePropertyName("docs");
			writer.WriteStartArray();
			JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.QueryDsl.PinnedDocDescriptor(DocsDescriptorAction), options);
			writer.WriteEndArray();
		}
		else if (DocsDescriptorActions is not null)
		{
			writer.WritePropertyName("docs");
			writer.WriteStartArray();
			foreach (var action in DocsDescriptorActions)
			{
				JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.QueryDsl.PinnedDocDescriptor(action), options);
			}

			writer.WriteEndArray();
		}
		else if (DocsValue is not null)
		{
			writer.WritePropertyName("docs");
			JsonSerializer.Serialize(writer, DocsValue, options);
		}

		if (IdsValue is not null)
		{
			writer.WritePropertyName("ids");
			JsonSerializer.Serialize(writer, IdsValue, options);
		}

		writer.WriteEndObject();
	}
}