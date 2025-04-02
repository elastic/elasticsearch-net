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

internal sealed partial class QueryRuleCriteriaConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.QueryRules.QueryRuleCriteria>
{
	private static readonly System.Text.Json.JsonEncodedText PropMetadata = System.Text.Json.JsonEncodedText.Encode("metadata");
	private static readonly System.Text.Json.JsonEncodedText PropType = System.Text.Json.JsonEncodedText.Encode("type");
	private static readonly System.Text.Json.JsonEncodedText PropValues = System.Text.Json.JsonEncodedText.Encode("values");

	public override Elastic.Clients.Elasticsearch.QueryRules.QueryRuleCriteria Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<string?> propMetadata = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.QueryRules.QueryRuleCriteriaType> propType = default;
		LocalJsonValue<System.Collections.Generic.ICollection<object>?> propValues = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propMetadata.TryReadProperty(ref reader, options, PropMetadata, null))
			{
				continue;
			}

			if (propType.TryReadProperty(ref reader, options, PropType, null))
			{
				continue;
			}

			if (propValues.TryReadProperty(ref reader, options, PropValues, static System.Collections.Generic.ICollection<object>? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadCollectionValue<object>(o, null)))
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
		return new Elastic.Clients.Elasticsearch.QueryRules.QueryRuleCriteria(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Metadata = propMetadata.Value,
			Type = propType.Value,
			Values = propValues.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.QueryRules.QueryRuleCriteria value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropMetadata, value.Metadata, null, null);
		writer.WriteProperty(options, PropType, value.Type, null, null);
		writer.WriteProperty(options, PropValues, value.Values, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.ICollection<object>? v) => w.WriteCollectionValue<object>(o, v, null));
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.QueryRules.QueryRuleCriteriaConverter))]
public sealed partial class QueryRuleCriteria
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public QueryRuleCriteria(Elastic.Clients.Elasticsearch.QueryRules.QueryRuleCriteriaType type)
	{
		Type = type;
	}
#if NET7_0_OR_GREATER
	public QueryRuleCriteria()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public QueryRuleCriteria()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal QueryRuleCriteria(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	/// <summary>
	/// <para>
	/// The metadata field to match against.
	/// This metadata will be used to match against <c>match_criteria</c> sent in the rule.
	/// It is required for all criteria types except <c>always</c>.
	/// </para>
	/// </summary>
	public string? Metadata { get; set; }

	/// <summary>
	/// <para>
	/// The type of criteria. The following criteria types are supported:
	/// </para>
	/// <list type="bullet">
	/// <item>
	/// <para>
	/// <c>always</c>: Matches all queries, regardless of input.
	/// </para>
	/// </item>
	/// <item>
	/// <para>
	/// <c>contains</c>: Matches that contain this value anywhere in the field meet the criteria defined by the rule. Only applicable for string values.
	/// </para>
	/// </item>
	/// <item>
	/// <para>
	/// <c>exact</c>: Only exact matches meet the criteria defined by the rule. Applicable for string or numerical values.
	/// </para>
	/// </item>
	/// <item>
	/// <para>
	/// <c>fuzzy</c>: Exact matches or matches within the allowed Levenshtein Edit Distance meet the criteria defined by the rule. Only applicable for string values.
	/// </para>
	/// </item>
	/// <item>
	/// <para>
	/// <c>gt</c>: Matches with a value greater than this value meet the criteria defined by the rule. Only applicable for numerical values.
	/// </para>
	/// </item>
	/// <item>
	/// <para>
	/// <c>gte</c>: Matches with a value greater than or equal to this value meet the criteria defined by the rule. Only applicable for numerical values.
	/// </para>
	/// </item>
	/// <item>
	/// <para>
	/// <c>lt</c>: Matches with a value less than this value meet the criteria defined by the rule. Only applicable for numerical values.
	/// </para>
	/// </item>
	/// <item>
	/// <para>
	/// <c>lte</c>: Matches with a value less than or equal to this value meet the criteria defined by the rule. Only applicable for numerical values.
	/// </para>
	/// </item>
	/// <item>
	/// <para>
	/// <c>prefix</c>: Matches that start with this value meet the criteria defined by the rule. Only applicable for string values.
	/// </para>
	/// </item>
	/// <item>
	/// <para>
	/// <c>suffix</c>: Matches that end with this value meet the criteria defined by the rule. Only applicable for string values.
	/// </para>
	/// </item>
	/// </list>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.QueryRules.QueryRuleCriteriaType Type { get; set; }

	/// <summary>
	/// <para>
	/// The values to match against the <c>metadata</c> field.
	/// Only one value must match for the criteria to be met.
	/// It is required for all criteria types except <c>always</c>.
	/// </para>
	/// </summary>
	public System.Collections.Generic.ICollection<object>? Values { get; set; }
}

public readonly partial struct QueryRuleCriteriaDescriptor
{
	internal Elastic.Clients.Elasticsearch.QueryRules.QueryRuleCriteria Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public QueryRuleCriteriaDescriptor(Elastic.Clients.Elasticsearch.QueryRules.QueryRuleCriteria instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public QueryRuleCriteriaDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.QueryRules.QueryRuleCriteria(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.QueryRules.QueryRuleCriteriaDescriptor(Elastic.Clients.Elasticsearch.QueryRules.QueryRuleCriteria instance) => new Elastic.Clients.Elasticsearch.QueryRules.QueryRuleCriteriaDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.QueryRules.QueryRuleCriteria(Elastic.Clients.Elasticsearch.QueryRules.QueryRuleCriteriaDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// The metadata field to match against.
	/// This metadata will be used to match against <c>match_criteria</c> sent in the rule.
	/// It is required for all criteria types except <c>always</c>.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryRules.QueryRuleCriteriaDescriptor Metadata(string? value)
	{
		Instance.Metadata = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The type of criteria. The following criteria types are supported:
	/// </para>
	/// <list type="bullet">
	/// <item>
	/// <para>
	/// <c>always</c>: Matches all queries, regardless of input.
	/// </para>
	/// </item>
	/// <item>
	/// <para>
	/// <c>contains</c>: Matches that contain this value anywhere in the field meet the criteria defined by the rule. Only applicable for string values.
	/// </para>
	/// </item>
	/// <item>
	/// <para>
	/// <c>exact</c>: Only exact matches meet the criteria defined by the rule. Applicable for string or numerical values.
	/// </para>
	/// </item>
	/// <item>
	/// <para>
	/// <c>fuzzy</c>: Exact matches or matches within the allowed Levenshtein Edit Distance meet the criteria defined by the rule. Only applicable for string values.
	/// </para>
	/// </item>
	/// <item>
	/// <para>
	/// <c>gt</c>: Matches with a value greater than this value meet the criteria defined by the rule. Only applicable for numerical values.
	/// </para>
	/// </item>
	/// <item>
	/// <para>
	/// <c>gte</c>: Matches with a value greater than or equal to this value meet the criteria defined by the rule. Only applicable for numerical values.
	/// </para>
	/// </item>
	/// <item>
	/// <para>
	/// <c>lt</c>: Matches with a value less than this value meet the criteria defined by the rule. Only applicable for numerical values.
	/// </para>
	/// </item>
	/// <item>
	/// <para>
	/// <c>lte</c>: Matches with a value less than or equal to this value meet the criteria defined by the rule. Only applicable for numerical values.
	/// </para>
	/// </item>
	/// <item>
	/// <para>
	/// <c>prefix</c>: Matches that start with this value meet the criteria defined by the rule. Only applicable for string values.
	/// </para>
	/// </item>
	/// <item>
	/// <para>
	/// <c>suffix</c>: Matches that end with this value meet the criteria defined by the rule. Only applicable for string values.
	/// </para>
	/// </item>
	/// </list>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryRules.QueryRuleCriteriaDescriptor Type(Elastic.Clients.Elasticsearch.QueryRules.QueryRuleCriteriaType value)
	{
		Instance.Type = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The values to match against the <c>metadata</c> field.
	/// Only one value must match for the criteria to be met.
	/// It is required for all criteria types except <c>always</c>.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryRules.QueryRuleCriteriaDescriptor Values(System.Collections.Generic.ICollection<object>? value)
	{
		Instance.Values = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The values to match against the <c>metadata</c> field.
	/// Only one value must match for the criteria to be met.
	/// It is required for all criteria types except <c>always</c>.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryRules.QueryRuleCriteriaDescriptor Values()
	{
		Instance.Values = Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfObject.Build(null);
		return this;
	}

	/// <summary>
	/// <para>
	/// The values to match against the <c>metadata</c> field.
	/// Only one value must match for the criteria to be met.
	/// It is required for all criteria types except <c>always</c>.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryRules.QueryRuleCriteriaDescriptor Values(System.Action<Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfObject>? action)
	{
		Instance.Values = Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfObject.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// The values to match against the <c>metadata</c> field.
	/// Only one value must match for the criteria to be met.
	/// It is required for all criteria types except <c>always</c>.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryRules.QueryRuleCriteriaDescriptor Values(params object[] values)
	{
		Instance.Values = [.. values];
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.QueryRules.QueryRuleCriteria Build(System.Action<Elastic.Clients.Elasticsearch.QueryRules.QueryRuleCriteriaDescriptor> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.QueryRules.QueryRuleCriteriaDescriptor(new Elastic.Clients.Elasticsearch.QueryRules.QueryRuleCriteria(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}