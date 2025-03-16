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

public sealed partial class QueryRulesetListItem
{
	/// <summary>
	/// <para>
	/// A map of criteria type (for example, <c>exact</c>) to the number of rules of that type.
	/// </para>
	/// <para>
	/// NOTE: The counts in <c>rule_criteria_types_counts</c> may be larger than the value of <c>rule_total_count</c> because a rule may have multiple criteria.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("rule_criteria_types_counts")]
	public IReadOnlyDictionary<string, int> RuleCriteriaTypesCounts { get; init; }

	/// <summary>
	/// <para>
	/// A unique identifier for the ruleset.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("ruleset_id")]
	public string RulesetId { get; init; }

	/// <summary>
	/// <para>
	/// The number of rules associated with the ruleset.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("rule_total_count")]
	public int RuleTotalCount { get; init; }

	/// <summary>
	/// <para>
	/// A map of rule type (for example, <c>pinned</c>) to the number of rules of that type.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("rule_type_counts")]
	public IReadOnlyDictionary<string, int> RuleTypeCounts { get; init; }
}