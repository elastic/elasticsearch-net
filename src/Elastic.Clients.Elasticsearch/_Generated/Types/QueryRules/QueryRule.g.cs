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

internal sealed partial class QueryRuleConverter : System.Text.Json.Serialization.JsonConverter<QueryRule>
{
	private static readonly System.Text.Json.JsonEncodedText PropActions = System.Text.Json.JsonEncodedText.Encode("actions");
	private static readonly System.Text.Json.JsonEncodedText PropCriteria = System.Text.Json.JsonEncodedText.Encode("criteria");
	private static readonly System.Text.Json.JsonEncodedText PropPriority = System.Text.Json.JsonEncodedText.Encode("priority");
	private static readonly System.Text.Json.JsonEncodedText PropRuleId = System.Text.Json.JsonEncodedText.Encode("rule_id");
	private static readonly System.Text.Json.JsonEncodedText PropType = System.Text.Json.JsonEncodedText.Encode("type");

	public override QueryRule Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<Elastic.Clients.Elasticsearch.QueryRules.QueryRuleActions> propActions = default;
		LocalJsonValue<ICollection<Elastic.Clients.Elasticsearch.QueryRules.QueryRuleCriteria>> propCriteria = default;
		LocalJsonValue<int?> propPriority = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Id> propRuleId = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.QueryRules.QueryRuleType> propType = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propActions.TryRead(ref reader, options, PropActions))
			{
				continue;
			}

			if (propCriteria.TryRead(ref reader, options, PropCriteria, typeof(SingleOrManyMarker<ICollection<Elastic.Clients.Elasticsearch.QueryRules.QueryRuleCriteria>, Elastic.Clients.Elasticsearch.QueryRules.QueryRuleCriteria>)))
			{
				continue;
			}

			if (propPriority.TryRead(ref reader, options, PropPriority))
			{
				continue;
			}

			if (propRuleId.TryRead(ref reader, options, PropRuleId))
			{
				continue;
			}

			if (propType.TryRead(ref reader, options, PropType))
			{
				continue;
			}

			throw new System.Text.Json.JsonException($"Unknown JSON property '{reader.GetString()}' for type '{typeToConvert.Name}'.");
		}

		reader.ValidateToken(System.Text.Json.JsonTokenType.EndObject);
		return new QueryRule
		{
			Actions = propActions.Value
,
			Criteria = propCriteria.Value
,
			Priority = propPriority.Value
,
			RuleId = propRuleId.Value
,
			Type = propType.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, QueryRule value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropActions, value.Actions);
		writer.WriteProperty(options, PropCriteria, value.Criteria, null, typeof(SingleOrManyMarker<ICollection<Elastic.Clients.Elasticsearch.QueryRules.QueryRuleCriteria>, Elastic.Clients.Elasticsearch.QueryRules.QueryRuleCriteria>));
		writer.WriteProperty(options, PropPriority, value.Priority);
		writer.WriteProperty(options, PropRuleId, value.RuleId);
		writer.WriteProperty(options, PropType, value.Type);
		writer.WriteEndObject();
	}
}

[JsonConverter(typeof(QueryRuleConverter))]
public sealed partial class QueryRule
{
	public Elastic.Clients.Elasticsearch.QueryRules.QueryRuleActions Actions { get; set; }
	public ICollection<Elastic.Clients.Elasticsearch.QueryRules.QueryRuleCriteria> Criteria { get; set; }
	public int? Priority { get; set; }
	public Elastic.Clients.Elasticsearch.Id RuleId { get; set; }
	public Elastic.Clients.Elasticsearch.QueryRules.QueryRuleType Type { get; set; }
}

public sealed partial class QueryRuleDescriptor : SerializableDescriptor<QueryRuleDescriptor>
{
	internal QueryRuleDescriptor(Action<QueryRuleDescriptor> configure) => configure.Invoke(this);

	public QueryRuleDescriptor() : base()
	{
	}

	private Elastic.Clients.Elasticsearch.QueryRules.QueryRuleActions ActionsValue { get; set; }
	private Elastic.Clients.Elasticsearch.QueryRules.QueryRuleActionsDescriptor ActionsDescriptor { get; set; }
	private Action<Elastic.Clients.Elasticsearch.QueryRules.QueryRuleActionsDescriptor> ActionsDescriptorAction { get; set; }
	private ICollection<Elastic.Clients.Elasticsearch.QueryRules.QueryRuleCriteria> CriteriaValue { get; set; }
	private Elastic.Clients.Elasticsearch.QueryRules.QueryRuleCriteriaDescriptor CriteriaDescriptor { get; set; }
	private Action<Elastic.Clients.Elasticsearch.QueryRules.QueryRuleCriteriaDescriptor> CriteriaDescriptorAction { get; set; }
	private Action<Elastic.Clients.Elasticsearch.QueryRules.QueryRuleCriteriaDescriptor>[] CriteriaDescriptorActions { get; set; }
	private int? PriorityValue { get; set; }
	private Elastic.Clients.Elasticsearch.Id RuleIdValue { get; set; }
	private Elastic.Clients.Elasticsearch.QueryRules.QueryRuleType TypeValue { get; set; }

	public QueryRuleDescriptor Actions(Elastic.Clients.Elasticsearch.QueryRules.QueryRuleActions actions)
	{
		ActionsDescriptor = null;
		ActionsDescriptorAction = null;
		ActionsValue = actions;
		return Self;
	}

	public QueryRuleDescriptor Actions(Elastic.Clients.Elasticsearch.QueryRules.QueryRuleActionsDescriptor descriptor)
	{
		ActionsValue = null;
		ActionsDescriptorAction = null;
		ActionsDescriptor = descriptor;
		return Self;
	}

	public QueryRuleDescriptor Actions(Action<Elastic.Clients.Elasticsearch.QueryRules.QueryRuleActionsDescriptor> configure)
	{
		ActionsValue = null;
		ActionsDescriptor = null;
		ActionsDescriptorAction = configure;
		return Self;
	}

	public QueryRuleDescriptor Criteria(ICollection<Elastic.Clients.Elasticsearch.QueryRules.QueryRuleCriteria> criteria)
	{
		CriteriaDescriptor = null;
		CriteriaDescriptorAction = null;
		CriteriaDescriptorActions = null;
		CriteriaValue = criteria;
		return Self;
	}

	public QueryRuleDescriptor Criteria(Elastic.Clients.Elasticsearch.QueryRules.QueryRuleCriteriaDescriptor descriptor)
	{
		CriteriaValue = null;
		CriteriaDescriptorAction = null;
		CriteriaDescriptorActions = null;
		CriteriaDescriptor = descriptor;
		return Self;
	}

	public QueryRuleDescriptor Criteria(Action<Elastic.Clients.Elasticsearch.QueryRules.QueryRuleCriteriaDescriptor> configure)
	{
		CriteriaValue = null;
		CriteriaDescriptor = null;
		CriteriaDescriptorActions = null;
		CriteriaDescriptorAction = configure;
		return Self;
	}

	public QueryRuleDescriptor Criteria(params Action<Elastic.Clients.Elasticsearch.QueryRules.QueryRuleCriteriaDescriptor>[] configure)
	{
		CriteriaValue = null;
		CriteriaDescriptor = null;
		CriteriaDescriptorAction = null;
		CriteriaDescriptorActions = configure;
		return Self;
	}

	public QueryRuleDescriptor Priority(int? priority)
	{
		PriorityValue = priority;
		return Self;
	}

	public QueryRuleDescriptor RuleId(Elastic.Clients.Elasticsearch.Id ruleId)
	{
		RuleIdValue = ruleId;
		return Self;
	}

	public QueryRuleDescriptor Type(Elastic.Clients.Elasticsearch.QueryRules.QueryRuleType type)
	{
		TypeValue = type;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (ActionsDescriptor is not null)
		{
			writer.WritePropertyName("actions");
			JsonSerializer.Serialize(writer, ActionsDescriptor, options);
		}
		else if (ActionsDescriptorAction is not null)
		{
			writer.WritePropertyName("actions");
			JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.QueryRules.QueryRuleActionsDescriptor(ActionsDescriptorAction), options);
		}
		else
		{
			writer.WritePropertyName("actions");
			JsonSerializer.Serialize(writer, ActionsValue, options);
		}

		if (CriteriaDescriptor is not null)
		{
			writer.WritePropertyName("criteria");
			JsonSerializer.Serialize(writer, CriteriaDescriptor, options);
		}
		else if (CriteriaDescriptorAction is not null)
		{
			writer.WritePropertyName("criteria");
			JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.QueryRules.QueryRuleCriteriaDescriptor(CriteriaDescriptorAction), options);
		}
		else if (CriteriaDescriptorActions is not null)
		{
			writer.WritePropertyName("criteria");
			if (CriteriaDescriptorActions.Length != 1)
				writer.WriteStartArray();
			foreach (var action in CriteriaDescriptorActions)
			{
				JsonSerializer.Serialize(writer, new Elastic.Clients.Elasticsearch.QueryRules.QueryRuleCriteriaDescriptor(action), options);
			}

			if (CriteriaDescriptorActions.Length != 1)
				writer.WriteEndArray();
		}
		else
		{
			writer.WritePropertyName("criteria");
			SingleOrManySerializationHelper.Serialize<Elastic.Clients.Elasticsearch.QueryRules.QueryRuleCriteria>(CriteriaValue, writer, options);
		}

		if (PriorityValue.HasValue)
		{
			writer.WritePropertyName("priority");
			writer.WriteNumberValue(PriorityValue.Value);
		}

		writer.WritePropertyName("rule_id");
		JsonSerializer.Serialize(writer, RuleIdValue, options);
		writer.WritePropertyName("type");
		JsonSerializer.Serialize(writer, TypeValue, options);
		writer.WriteEndObject();
	}
}