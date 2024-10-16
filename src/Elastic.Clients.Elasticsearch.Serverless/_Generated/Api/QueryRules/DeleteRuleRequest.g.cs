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
using Elastic.Transport.Extensions;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch.Serverless.QueryRules;

public sealed partial class DeleteRuleRequestParameters : RequestParameters
{
}

/// <summary>
/// <para>
/// Deletes a query rule within a query ruleset.
/// </para>
/// </summary>
public sealed partial class DeleteRuleRequest : PlainRequest<DeleteRuleRequestParameters>
{
	public DeleteRuleRequest(Elastic.Clients.Elasticsearch.Serverless.Id rulesetId, Elastic.Clients.Elasticsearch.Serverless.Id ruleId) : base(r => r.Required("ruleset_id", rulesetId).Required("rule_id", ruleId))
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.QueryRulesDeleteRule;

	protected override HttpMethod StaticHttpMethod => HttpMethod.DELETE;

	internal override bool SupportsBody => false;

	internal override string OperationName => "query_rules.delete_rule";
}

/// <summary>
/// <para>
/// Deletes a query rule within a query ruleset.
/// </para>
/// </summary>
public sealed partial class DeleteRuleRequestDescriptor : RequestDescriptor<DeleteRuleRequestDescriptor, DeleteRuleRequestParameters>
{
	internal DeleteRuleRequestDescriptor(Action<DeleteRuleRequestDescriptor> configure) => configure.Invoke(this);

	public DeleteRuleRequestDescriptor(Elastic.Clients.Elasticsearch.Serverless.Id rulesetId, Elastic.Clients.Elasticsearch.Serverless.Id ruleId) : base(r => r.Required("ruleset_id", rulesetId).Required("rule_id", ruleId))
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.QueryRulesDeleteRule;

	protected override HttpMethod StaticHttpMethod => HttpMethod.DELETE;

	internal override bool SupportsBody => false;

	internal override string OperationName => "query_rules.delete_rule";

	public DeleteRuleRequestDescriptor RuleId(Elastic.Clients.Elasticsearch.Serverless.Id ruleId)
	{
		RouteValues.Required("rule_id", ruleId);
		return Self;
	}

	public DeleteRuleRequestDescriptor RulesetId(Elastic.Clients.Elasticsearch.Serverless.Id rulesetId)
	{
		RouteValues.Required("ruleset_id", rulesetId);
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
	}
}