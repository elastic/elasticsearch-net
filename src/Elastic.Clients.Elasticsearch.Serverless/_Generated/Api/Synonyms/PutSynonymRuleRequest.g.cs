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

namespace Elastic.Clients.Elasticsearch.Serverless.Synonyms;

public sealed partial class PutSynonymRuleRequestParameters : RequestParameters
{
}

/// <summary>
/// <para>Creates or updates a synonym rule in a synonym set</para>
/// </summary>
public sealed partial class PutSynonymRuleRequest : PlainRequest<PutSynonymRuleRequestParameters>
{
	public PutSynonymRuleRequest(Elastic.Clients.Elasticsearch.Serverless.Id setId, Elastic.Clients.Elasticsearch.Serverless.Id ruleId) : base(r => r.Required("set_id", setId).Required("rule_id", ruleId))
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.SynonymsPutSynonymRule;

	protected override HttpMethod StaticHttpMethod => HttpMethod.PUT;

	internal override bool SupportsBody => true;

	internal override string OperationName => "synonyms.put_synonym_rule";

	[JsonInclude, JsonPropertyName("synonyms")]
	public string Synonyms { get; set; }
}

/// <summary>
/// <para>Creates or updates a synonym rule in a synonym set</para>
/// </summary>
public sealed partial class PutSynonymRuleRequestDescriptor : RequestDescriptor<PutSynonymRuleRequestDescriptor, PutSynonymRuleRequestParameters>
{
	internal PutSynonymRuleRequestDescriptor(Action<PutSynonymRuleRequestDescriptor> configure) => configure.Invoke(this);

	public PutSynonymRuleRequestDescriptor(Elastic.Clients.Elasticsearch.Serverless.Id setId, Elastic.Clients.Elasticsearch.Serverless.Id ruleId) : base(r => r.Required("set_id", setId).Required("rule_id", ruleId))
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.SynonymsPutSynonymRule;

	protected override HttpMethod StaticHttpMethod => HttpMethod.PUT;

	internal override bool SupportsBody => true;

	internal override string OperationName => "synonyms.put_synonym_rule";

	public PutSynonymRuleRequestDescriptor RuleId(Elastic.Clients.Elasticsearch.Serverless.Id ruleId)
	{
		RouteValues.Required("rule_id", ruleId);
		return Self;
	}

	public PutSynonymRuleRequestDescriptor SetId(Elastic.Clients.Elasticsearch.Serverless.Id setId)
	{
		RouteValues.Required("set_id", setId);
		return Self;
	}

	private string SynonymsValue { get; set; }

	public PutSynonymRuleRequestDescriptor Synonyms(string synonyms)
	{
		SynonymsValue = synonyms;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		writer.WritePropertyName("synonyms");
		writer.WriteStringValue(SynonymsValue);
		writer.WriteEndObject();
	}
}