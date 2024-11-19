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

public sealed partial class TestRequestParameters : RequestParameters
{
}

/// <summary>
/// <para>
/// Creates or updates a query ruleset.
/// </para>
/// </summary>
public sealed partial class TestRequest : PlainRequest<TestRequestParameters>
{
	public TestRequest(Elastic.Clients.Elasticsearch.Serverless.Id rulesetId) : base(r => r.Required("ruleset_id", rulesetId))
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.QueryRulesTest;

	protected override HttpMethod StaticHttpMethod => HttpMethod.POST;

	internal override bool SupportsBody => true;

	internal override string OperationName => "query_rules.test";

	[JsonInclude, JsonPropertyName("match_criteria")]
	public IDictionary<string, object> MatchCriteria { get; set; }
}

/// <summary>
/// <para>
/// Creates or updates a query ruleset.
/// </para>
/// </summary>
public sealed partial class TestRequestDescriptor : RequestDescriptor<TestRequestDescriptor, TestRequestParameters>
{
	internal TestRequestDescriptor(Action<TestRequestDescriptor> configure) => configure.Invoke(this);

	public TestRequestDescriptor(Elastic.Clients.Elasticsearch.Serverless.Id rulesetId) : base(r => r.Required("ruleset_id", rulesetId))
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.QueryRulesTest;

	protected override HttpMethod StaticHttpMethod => HttpMethod.POST;

	internal override bool SupportsBody => true;

	internal override string OperationName => "query_rules.test";

	public TestRequestDescriptor RulesetId(Elastic.Clients.Elasticsearch.Serverless.Id rulesetId)
	{
		RouteValues.Required("ruleset_id", rulesetId);
		return Self;
	}

	private IDictionary<string, object> MatchCriteriaValue { get; set; }

	public TestRequestDescriptor MatchCriteria(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> selector)
	{
		MatchCriteriaValue = selector?.Invoke(new FluentDictionary<string, object>());
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		writer.WritePropertyName("match_criteria");
		JsonSerializer.Serialize(writer, MatchCriteriaValue, options);
		writer.WriteEndObject();
	}
}