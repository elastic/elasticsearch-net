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

using System;
using System.Linq;
using Elastic.Clients.Elasticsearch.Serialization;

namespace Elastic.Clients.Elasticsearch.QueryRules;

public sealed partial class DeleteRuleRequestParameters : Elastic.Transport.RequestParameters
{
}

internal sealed partial class DeleteRuleRequestConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.QueryRules.DeleteRuleRequest>
{
	public override Elastic.Clients.Elasticsearch.QueryRules.DeleteRuleRequest Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (options.UnmappedMemberHandling is System.Text.Json.Serialization.JsonUnmappedMemberHandling.Skip)
			{
				reader.Skip();
				continue;
			}

			throw new System.Text.Json.JsonException($"Unknown JSON property '{reader.GetString()}' for type '{typeToConvert.Name}'.");
		}

		reader.ValidateToken(System.Text.Json.JsonTokenType.EndObject);
		return new Elastic.Clients.Elasticsearch.QueryRules.DeleteRuleRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.QueryRules.DeleteRuleRequest value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteEndObject();
	}
}

/// <summary>
/// <para>
/// Delete a query rule.
/// Delete a query rule within a query ruleset.
/// This is a destructive action that is only recoverable by re-adding the same rule with the create or update query rule API.
/// </para>
/// </summary>
[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.QueryRules.DeleteRuleRequestConverter))]
public sealed partial class DeleteRuleRequest : Elastic.Clients.Elasticsearch.Requests.PlainRequest<Elastic.Clients.Elasticsearch.QueryRules.DeleteRuleRequestParameters>
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public DeleteRuleRequest(Elastic.Clients.Elasticsearch.Id rulesetId, Elastic.Clients.Elasticsearch.Id ruleId) : base(r => r.Required("ruleset_id", rulesetId).Required("rule_id", ruleId))
	{
	}
#if NET7_0_OR_GREATER
	public DeleteRuleRequest()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal DeleteRuleRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	internal override Elastic.Clients.Elasticsearch.Requests.ApiUrls ApiUrls => Elastic.Clients.Elasticsearch.Requests.ApiUrlLookup.QueryRulesDeleteRule;

	protected override Elastic.Transport.HttpMethod StaticHttpMethod => Elastic.Transport.HttpMethod.DELETE;

	internal override bool SupportsBody => false;

	internal override string OperationName => "query_rules.delete_rule";

	/// <summary>
	/// <para>
	/// The unique identifier of the query rule within the specified ruleset to delete
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.Id RuleId { get => P<Elastic.Clients.Elasticsearch.Id>("rule_id"); set => PR("rule_id", value); }

	/// <summary>
	/// <para>
	/// The unique identifier of the query ruleset containing the rule to delete
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.Id RulesetId { get => P<Elastic.Clients.Elasticsearch.Id>("ruleset_id"); set => PR("ruleset_id", value); }
}

/// <summary>
/// <para>
/// Delete a query rule.
/// Delete a query rule within a query ruleset.
/// This is a destructive action that is only recoverable by re-adding the same rule with the create or update query rule API.
/// </para>
/// </summary>
public readonly partial struct DeleteRuleRequestDescriptor
{
	internal Elastic.Clients.Elasticsearch.QueryRules.DeleteRuleRequest Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public DeleteRuleRequestDescriptor(Elastic.Clients.Elasticsearch.QueryRules.DeleteRuleRequest instance)
	{
		Instance = instance;
	}

	public DeleteRuleRequestDescriptor(Elastic.Clients.Elasticsearch.Id rulesetId, Elastic.Clients.Elasticsearch.Id ruleId)
	{
		Instance = new Elastic.Clients.Elasticsearch.QueryRules.DeleteRuleRequest(rulesetId, ruleId);
	}

	[System.Obsolete("TODO")]
	public DeleteRuleRequestDescriptor()
	{
		throw new System.InvalidOperationException("The use of the parameterless constructor is not permitted for this type.");
	}

	public static explicit operator Elastic.Clients.Elasticsearch.QueryRules.DeleteRuleRequestDescriptor(Elastic.Clients.Elasticsearch.QueryRules.DeleteRuleRequest instance) => new Elastic.Clients.Elasticsearch.QueryRules.DeleteRuleRequestDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.QueryRules.DeleteRuleRequest(Elastic.Clients.Elasticsearch.QueryRules.DeleteRuleRequestDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// The unique identifier of the query rule within the specified ruleset to delete
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryRules.DeleteRuleRequestDescriptor RuleId(Elastic.Clients.Elasticsearch.Id value)
	{
		Instance.RuleId = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The unique identifier of the query ruleset containing the rule to delete
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryRules.DeleteRuleRequestDescriptor RulesetId(Elastic.Clients.Elasticsearch.Id value)
	{
		Instance.RulesetId = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.QueryRules.DeleteRuleRequest Build(System.Action<Elastic.Clients.Elasticsearch.QueryRules.DeleteRuleRequestDescriptor> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.QueryRules.DeleteRuleRequestDescriptor(new Elastic.Clients.Elasticsearch.QueryRules.DeleteRuleRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}

	public Elastic.Clients.Elasticsearch.QueryRules.DeleteRuleRequestDescriptor ErrorTrace(bool? value)
	{
		Instance.ErrorTrace = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.QueryRules.DeleteRuleRequestDescriptor FilterPath(params string[]? value)
	{
		Instance.FilterPath = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.QueryRules.DeleteRuleRequestDescriptor Human(bool? value)
	{
		Instance.Human = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.QueryRules.DeleteRuleRequestDescriptor Pretty(bool? value)
	{
		Instance.Pretty = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.QueryRules.DeleteRuleRequestDescriptor SourceQueryString(string? value)
	{
		Instance.SourceQueryString = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.QueryRules.DeleteRuleRequestDescriptor RequestConfiguration(Elastic.Transport.IRequestConfiguration? value)
	{
		Instance.RequestConfiguration = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.QueryRules.DeleteRuleRequestDescriptor RequestConfiguration(System.Func<Elastic.Transport.RequestConfigurationDescriptor, Elastic.Transport.IRequestConfiguration>? configurationSelector)
	{
		Instance.RequestConfiguration = configurationSelector.Invoke(Instance.RequestConfiguration is null ? new Elastic.Transport.RequestConfigurationDescriptor() : new Elastic.Transport.RequestConfigurationDescriptor(Instance.RequestConfiguration)) ?? Instance.RequestConfiguration;
		return this;
	}
}