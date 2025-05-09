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

internal sealed partial class QueryRuleActionsConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.QueryRules.QueryRuleActions>
{
	private static readonly System.Text.Json.JsonEncodedText PropDocs = System.Text.Json.JsonEncodedText.Encode("docs");
	private static readonly System.Text.Json.JsonEncodedText PropIds = System.Text.Json.JsonEncodedText.Encode("ids");

	public override Elastic.Clients.Elasticsearch.QueryRules.QueryRuleActions Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.QueryDsl.PinnedDoc>?> propDocs = default;
		LocalJsonValue<System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.Id>?> propIds = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propDocs.TryReadProperty(ref reader, options, PropDocs, static System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.QueryDsl.PinnedDoc>? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadCollectionValue<Elastic.Clients.Elasticsearch.QueryDsl.PinnedDoc>(o, null)))
			{
				continue;
			}

			if (propIds.TryReadProperty(ref reader, options, PropIds, static System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.Id>? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadCollectionValue<Elastic.Clients.Elasticsearch.Id>(o, null)))
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
		return new Elastic.Clients.Elasticsearch.QueryRules.QueryRuleActions(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Docs = propDocs.Value,
			Ids = propIds.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.QueryRules.QueryRuleActions value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropDocs, value.Docs, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.QueryDsl.PinnedDoc>? v) => w.WriteCollectionValue<Elastic.Clients.Elasticsearch.QueryDsl.PinnedDoc>(o, v, null));
		writer.WriteProperty(options, PropIds, value.Ids, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.Id>? v) => w.WriteCollectionValue<Elastic.Clients.Elasticsearch.Id>(o, v, null));
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.QueryRules.QueryRuleActionsConverter))]
public sealed partial class QueryRuleActions
{
#if NET7_0_OR_GREATER
	public QueryRuleActions()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	public QueryRuleActions()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal QueryRuleActions(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

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
	public System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.QueryDsl.PinnedDoc>? Docs { get; set; }

	/// <summary>
	/// <para>
	/// The unique document IDs of the documents to apply the rule to.
	/// Only one of <c>ids</c> or <c>docs</c> may be specified and at least one must be specified.
	/// </para>
	/// </summary>
	public System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.Id>? Ids { get; set; }
}

public readonly partial struct QueryRuleActionsDescriptor
{
	internal Elastic.Clients.Elasticsearch.QueryRules.QueryRuleActions Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public QueryRuleActionsDescriptor(Elastic.Clients.Elasticsearch.QueryRules.QueryRuleActions instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public QueryRuleActionsDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.QueryRules.QueryRuleActions(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.QueryRules.QueryRuleActionsDescriptor(Elastic.Clients.Elasticsearch.QueryRules.QueryRuleActions instance) => new Elastic.Clients.Elasticsearch.QueryRules.QueryRuleActionsDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.QueryRules.QueryRuleActions(Elastic.Clients.Elasticsearch.QueryRules.QueryRuleActionsDescriptor descriptor) => descriptor.Instance;

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
	public Elastic.Clients.Elasticsearch.QueryRules.QueryRuleActionsDescriptor Docs(System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.QueryDsl.PinnedDoc>? value)
	{
		Instance.Docs = value;
		return this;
	}

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
	public Elastic.Clients.Elasticsearch.QueryRules.QueryRuleActionsDescriptor Docs(params Elastic.Clients.Elasticsearch.QueryDsl.PinnedDoc[] values)
	{
		Instance.Docs = [.. values];
		return this;
	}

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
	public Elastic.Clients.Elasticsearch.QueryRules.QueryRuleActionsDescriptor Docs(params System.Action<Elastic.Clients.Elasticsearch.QueryDsl.PinnedDocDescriptor>[] actions)
	{
		var items = new System.Collections.Generic.List<Elastic.Clients.Elasticsearch.QueryDsl.PinnedDoc>();
		foreach (var action in actions)
		{
			items.Add(Elastic.Clients.Elasticsearch.QueryDsl.PinnedDocDescriptor.Build(action));
		}

		Instance.Docs = items;
		return this;
	}

	/// <summary>
	/// <para>
	/// The unique document IDs of the documents to apply the rule to.
	/// Only one of <c>ids</c> or <c>docs</c> may be specified and at least one must be specified.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryRules.QueryRuleActionsDescriptor Ids(System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.Id>? value)
	{
		Instance.Ids = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The unique document IDs of the documents to apply the rule to.
	/// Only one of <c>ids</c> or <c>docs</c> may be specified and at least one must be specified.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.QueryRules.QueryRuleActionsDescriptor Ids(params Elastic.Clients.Elasticsearch.Id[] values)
	{
		Instance.Ids = [.. values];
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.QueryRules.QueryRuleActions Build(System.Action<Elastic.Clients.Elasticsearch.QueryRules.QueryRuleActionsDescriptor>? action)
	{
		if (action is null)
		{
			return new Elastic.Clients.Elasticsearch.QueryRules.QueryRuleActions(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
		}

		var builder = new Elastic.Clients.Elasticsearch.QueryRules.QueryRuleActionsDescriptor(new Elastic.Clients.Elasticsearch.QueryRules.QueryRuleActions(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}