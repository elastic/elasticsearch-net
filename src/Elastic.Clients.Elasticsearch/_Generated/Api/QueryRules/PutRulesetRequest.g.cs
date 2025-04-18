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

public sealed partial class PutRulesetRequestParameters : Elastic.Transport.RequestParameters
{
}

internal sealed partial class PutRulesetRequestConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.QueryRules.PutRulesetRequest>
{
	private static readonly System.Text.Json.JsonEncodedText PropRules = System.Text.Json.JsonEncodedText.Encode("rules");

	public override Elastic.Clients.Elasticsearch.QueryRules.PutRulesetRequest Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.QueryRules.QueryRule>> propRules = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propRules.TryReadProperty(ref reader, options, PropRules, static System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.QueryRules.QueryRule> (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadSingleOrManyCollectionValue<Elastic.Clients.Elasticsearch.QueryRules.QueryRule>(o, null)!))
			{
				continue;
			}

			if (options.UnmappedMemberHandling is System.Text.Json.Serialization.JsonUnmappedMemberHandling.Skip)
			{
				reader.Skip();
				continue;
			}

			throw new System.Text.Json.JsonException($"Unknown JSON property '{reader.GetString()}' for type '{typeToConvert.Name}'.");
		}

		reader.ValidateToken(System.Text.Json.JsonTokenType.EndObject);
		return new Elastic.Clients.Elasticsearch.QueryRules.PutRulesetRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Rules = propRules.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.QueryRules.PutRulesetRequest value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropRules, value.Rules, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.QueryRules.QueryRule> v) => w.WriteSingleOrManyCollectionValue<Elastic.Clients.Elasticsearch.QueryRules.QueryRule>(o, v, null));
		writer.WriteEndObject();
	}
}

/// <summary>
/// <para>
/// Create or update a query ruleset.
/// There is a limit of 100 rules per ruleset.
/// This limit can be increased by using the <c>xpack.applications.rules.max_rules_per_ruleset</c> cluster setting.
/// </para>
/// <para>
/// IMPORTANT: Due to limitations within pinned queries, you can only select documents using <c>ids</c> or <c>docs</c>, but cannot use both in single rule.
/// It is advised to use one or the other in query rulesets, to avoid errors.
/// Additionally, pinned queries have a maximum limit of 100 pinned hits.
/// If multiple matching rules pin more than 100 documents, only the first 100 documents are pinned in the order they are specified in the ruleset.
/// </para>
/// </summary>
[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.QueryRules.PutRulesetRequestConverter))]
public sealed partial class PutRulesetRequest : Elastic.Clients.Elasticsearch.Requests.PlainRequest<Elastic.Clients.Elasticsearch.QueryRules.PutRulesetRequestParameters>
{
	[System.Obsolete("The request contains additional required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public PutRulesetRequest(Elastic.Clients.Elasticsearch.Id rulesetId) : base(r => r.Required("ruleset_id", rulesetId))
	{
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public PutRulesetRequest(Elastic.Clients.Elasticsearch.Id rulesetId, System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.QueryRules.QueryRule> rules) : base(r => r.Required("ruleset_id", rulesetId))
	{
		Rules = rules;
	}
#if NET7_0_OR_GREATER
	public PutRulesetRequest()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal PutRulesetRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	internal override Elastic.Clients.Elasticsearch.Requests.ApiUrls ApiUrls => Elastic.Clients.Elasticsearch.Requests.ApiUrlLookup.QueryRulesPutRuleset;

	protected override Elastic.Transport.HttpMethod StaticHttpMethod => Elastic.Transport.HttpMethod.PUT;

	internal override bool SupportsBody => true;

	internal override string OperationName => "query_rules.put_ruleset";

	/// <summary>
	/// <para>
	/// The unique identifier of the query ruleset to be created or updated.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.Id RulesetId { get => P<Elastic.Clients.Elasticsearch.Id>("ruleset_id"); set => PR("ruleset_id", value); }
	public
#if NET7_0_OR_GREATER
	required
#endif
	System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.QueryRules.QueryRule> Rules { get; set; }
}

/// <summary>
/// <para>
/// Create or update a query ruleset.
/// There is a limit of 100 rules per ruleset.
/// This limit can be increased by using the <c>xpack.applications.rules.max_rules_per_ruleset</c> cluster setting.
/// </para>
/// <para>
/// IMPORTANT: Due to limitations within pinned queries, you can only select documents using <c>ids</c> or <c>docs</c>, but cannot use both in single rule.
/// It is advised to use one or the other in query rulesets, to avoid errors.
/// Additionally, pinned queries have a maximum limit of 100 pinned hits.
/// If multiple matching rules pin more than 100 documents, only the first 100 documents are pinned in the order they are specified in the ruleset.
/// </para>
/// </summary>
public readonly partial struct PutRulesetRequestDescriptor
{
	internal Elastic.Clients.Elasticsearch.QueryRules.PutRulesetRequest Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public PutRulesetRequestDescriptor(Elastic.Clients.Elasticsearch.QueryRules.PutRulesetRequest instance)
	{
		Instance = instance;
	}

	public PutRulesetRequestDescriptor(Elastic.Clients.Elasticsearch.Id rulesetId)
	{
#pragma warning disable CS0618
		Instance = new Elastic.Clients.Elasticsearch.QueryRules.PutRulesetRequest(rulesetId);
#pragma warning restore CS0618
	}

	[System.Obsolete("The use of the parameterless constructor is not permitted for this type.")]
	public PutRulesetRequestDescriptor()
	{
		throw new System.InvalidOperationException("The use of the parameterless constructor is not permitted for this type.");
	}

	public static explicit operator Elastic.Clients.Elasticsearch.QueryRules.PutRulesetRequestDescriptor(Elastic.Clients.Elasticsearch.QueryRules.PutRulesetRequest instance) => new Elastic.Clients.Elasticsearch.QueryRules.PutRulesetRequestDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.QueryRules.PutRulesetRequest(Elastic.Clients.Elasticsearch.QueryRules.PutRulesetRequestDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// The unique identifier of the query ruleset to be created or updated.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryRules.PutRulesetRequestDescriptor RulesetId(Elastic.Clients.Elasticsearch.Id value)
	{
		Instance.RulesetId = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.QueryRules.PutRulesetRequestDescriptor Rules(System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.QueryRules.QueryRule> value)
	{
		Instance.Rules = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.QueryRules.PutRulesetRequestDescriptor Rules(params Elastic.Clients.Elasticsearch.QueryRules.QueryRule[] values)
	{
		Instance.Rules = [.. values];
		return this;
	}

	public Elastic.Clients.Elasticsearch.QueryRules.PutRulesetRequestDescriptor Rules(params System.Action<Elastic.Clients.Elasticsearch.QueryRules.QueryRuleDescriptor>[] actions)
	{
		var items = new System.Collections.Generic.List<Elastic.Clients.Elasticsearch.QueryRules.QueryRule>();
		foreach (var action in actions)
		{
			items.Add(Elastic.Clients.Elasticsearch.QueryRules.QueryRuleDescriptor.Build(action));
		}

		Instance.Rules = items;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.QueryRules.PutRulesetRequest Build(System.Action<Elastic.Clients.Elasticsearch.QueryRules.PutRulesetRequestDescriptor> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.QueryRules.PutRulesetRequestDescriptor(new Elastic.Clients.Elasticsearch.QueryRules.PutRulesetRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}

	public Elastic.Clients.Elasticsearch.QueryRules.PutRulesetRequestDescriptor ErrorTrace(bool? value)
	{
		Instance.ErrorTrace = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.QueryRules.PutRulesetRequestDescriptor FilterPath(params string[]? value)
	{
		Instance.FilterPath = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.QueryRules.PutRulesetRequestDescriptor Human(bool? value)
	{
		Instance.Human = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.QueryRules.PutRulesetRequestDescriptor Pretty(bool? value)
	{
		Instance.Pretty = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.QueryRules.PutRulesetRequestDescriptor SourceQueryString(string? value)
	{
		Instance.SourceQueryString = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.QueryRules.PutRulesetRequestDescriptor RequestConfiguration(Elastic.Transport.IRequestConfiguration? value)
	{
		Instance.RequestConfiguration = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.QueryRules.PutRulesetRequestDescriptor RequestConfiguration(System.Func<Elastic.Transport.RequestConfigurationDescriptor, Elastic.Transport.IRequestConfiguration>? configurationSelector)
	{
		Instance.RequestConfiguration = configurationSelector.Invoke(Instance.RequestConfiguration is null ? new Elastic.Transport.RequestConfigurationDescriptor() : new Elastic.Transport.RequestConfigurationDescriptor(Instance.RequestConfiguration)) ?? Instance.RequestConfiguration;
		return this;
	}
}