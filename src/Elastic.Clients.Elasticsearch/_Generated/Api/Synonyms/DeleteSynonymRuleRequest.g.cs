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

namespace Elastic.Clients.Elasticsearch.Synonyms;

public sealed partial class DeleteSynonymRuleRequestParameters : Elastic.Transport.RequestParameters
{
}

internal sealed partial class DeleteSynonymRuleRequestConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Synonyms.DeleteSynonymRuleRequest>
{
	public override Elastic.Clients.Elasticsearch.Synonyms.DeleteSynonymRuleRequest Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
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
		return new Elastic.Clients.Elasticsearch.Synonyms.DeleteSynonymRuleRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Synonyms.DeleteSynonymRuleRequest value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteEndObject();
	}
}

/// <summary>
/// <para>
/// Delete a synonym rule.
/// Delete a synonym rule from a synonym set.
/// </para>
/// </summary>
[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Synonyms.DeleteSynonymRuleRequestConverter))]
public sealed partial class DeleteSynonymRuleRequest : Elastic.Clients.Elasticsearch.Requests.PlainRequest<Elastic.Clients.Elasticsearch.Synonyms.DeleteSynonymRuleRequestParameters>
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public DeleteSynonymRuleRequest(Elastic.Clients.Elasticsearch.Id setId, Elastic.Clients.Elasticsearch.Id ruleId) : base(r => r.Required("set_id", setId).Required("rule_id", ruleId))
	{
	}
#if NET7_0_OR_GREATER
	public DeleteSynonymRuleRequest()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal DeleteSynonymRuleRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	internal override Elastic.Clients.Elasticsearch.Requests.ApiUrls ApiUrls => Elastic.Clients.Elasticsearch.Requests.ApiUrlLookup.SynonymsDeleteSynonymRule;

	protected override Elastic.Transport.HttpMethod StaticHttpMethod => Elastic.Transport.HttpMethod.DELETE;

	internal override bool SupportsBody => false;

	internal override string OperationName => "synonyms.delete_synonym_rule";

	/// <summary>
	/// <para>
	/// The ID of the synonym rule to delete.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.Id RuleId { get => P<Elastic.Clients.Elasticsearch.Id>("rule_id"); set => PR("rule_id", value); }

	/// <summary>
	/// <para>
	/// The ID of the synonym set to update.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.Id SetId { get => P<Elastic.Clients.Elasticsearch.Id>("set_id"); set => PR("set_id", value); }
}

/// <summary>
/// <para>
/// Delete a synonym rule.
/// Delete a synonym rule from a synonym set.
/// </para>
/// </summary>
public readonly partial struct DeleteSynonymRuleRequestDescriptor
{
	internal Elastic.Clients.Elasticsearch.Synonyms.DeleteSynonymRuleRequest Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public DeleteSynonymRuleRequestDescriptor(Elastic.Clients.Elasticsearch.Synonyms.DeleteSynonymRuleRequest instance)
	{
		Instance = instance;
	}

	public DeleteSynonymRuleRequestDescriptor(Elastic.Clients.Elasticsearch.Id setId, Elastic.Clients.Elasticsearch.Id ruleId)
	{
		Instance = new Elastic.Clients.Elasticsearch.Synonyms.DeleteSynonymRuleRequest(setId, ruleId);
	}

	[System.Obsolete("The use of the parameterless constructor is not permitted for this type.")]
	public DeleteSynonymRuleRequestDescriptor()
	{
		throw new System.InvalidOperationException("The use of the parameterless constructor is not permitted for this type.");
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Synonyms.DeleteSynonymRuleRequestDescriptor(Elastic.Clients.Elasticsearch.Synonyms.DeleteSynonymRuleRequest instance) => new Elastic.Clients.Elasticsearch.Synonyms.DeleteSynonymRuleRequestDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Synonyms.DeleteSynonymRuleRequest(Elastic.Clients.Elasticsearch.Synonyms.DeleteSynonymRuleRequestDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// The ID of the synonym rule to delete.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Synonyms.DeleteSynonymRuleRequestDescriptor RuleId(Elastic.Clients.Elasticsearch.Id value)
	{
		Instance.RuleId = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The ID of the synonym set to update.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Synonyms.DeleteSynonymRuleRequestDescriptor SetId(Elastic.Clients.Elasticsearch.Id value)
	{
		Instance.SetId = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Synonyms.DeleteSynonymRuleRequest Build(System.Action<Elastic.Clients.Elasticsearch.Synonyms.DeleteSynonymRuleRequestDescriptor> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.Synonyms.DeleteSynonymRuleRequestDescriptor(new Elastic.Clients.Elasticsearch.Synonyms.DeleteSynonymRuleRequest(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}

	public Elastic.Clients.Elasticsearch.Synonyms.DeleteSynonymRuleRequestDescriptor ErrorTrace(bool? value)
	{
		Instance.ErrorTrace = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Synonyms.DeleteSynonymRuleRequestDescriptor FilterPath(params string[]? value)
	{
		Instance.FilterPath = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Synonyms.DeleteSynonymRuleRequestDescriptor Human(bool? value)
	{
		Instance.Human = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Synonyms.DeleteSynonymRuleRequestDescriptor Pretty(bool? value)
	{
		Instance.Pretty = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Synonyms.DeleteSynonymRuleRequestDescriptor SourceQueryString(string? value)
	{
		Instance.SourceQueryString = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Synonyms.DeleteSynonymRuleRequestDescriptor RequestConfiguration(Elastic.Transport.IRequestConfiguration? value)
	{
		Instance.RequestConfiguration = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Synonyms.DeleteSynonymRuleRequestDescriptor RequestConfiguration(System.Func<Elastic.Transport.RequestConfigurationDescriptor, Elastic.Transport.IRequestConfiguration>? configurationSelector)
	{
		Instance.RequestConfiguration = configurationSelector.Invoke(Instance.RequestConfiguration is null ? new Elastic.Transport.RequestConfigurationDescriptor() : new Elastic.Transport.RequestConfigurationDescriptor(Instance.RequestConfiguration)) ?? Instance.RequestConfiguration;
		return this;
	}
}