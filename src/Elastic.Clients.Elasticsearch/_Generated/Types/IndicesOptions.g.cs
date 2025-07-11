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

namespace Elastic.Clients.Elasticsearch;

internal sealed partial class IndicesOptionsConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.IndicesOptions>
{
	private static readonly System.Text.Json.JsonEncodedText PropAllowNoIndices = System.Text.Json.JsonEncodedText.Encode("allow_no_indices");
	private static readonly System.Text.Json.JsonEncodedText PropExpandWildcards = System.Text.Json.JsonEncodedText.Encode("expand_wildcards");
	private static readonly System.Text.Json.JsonEncodedText PropIgnoreThrottled = System.Text.Json.JsonEncodedText.Encode("ignore_throttled");
	private static readonly System.Text.Json.JsonEncodedText PropIgnoreUnavailable = System.Text.Json.JsonEncodedText.Encode("ignore_unavailable");

	public override Elastic.Clients.Elasticsearch.IndicesOptions Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<bool?> propAllowNoIndices = default;
		LocalJsonValue<System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.ExpandWildcard>?> propExpandWildcards = default;
		LocalJsonValue<bool?> propIgnoreThrottled = default;
		LocalJsonValue<bool?> propIgnoreUnavailable = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propAllowNoIndices.TryReadProperty(ref reader, options, PropAllowNoIndices, static bool? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadNullableValue<bool>(o)))
			{
				continue;
			}

			if (propExpandWildcards.TryReadProperty(ref reader, options, PropExpandWildcards, static System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.ExpandWildcard>? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadSingleOrManyCollectionValue<Elastic.Clients.Elasticsearch.ExpandWildcard>(o, null)))
			{
				continue;
			}

			if (propIgnoreThrottled.TryReadProperty(ref reader, options, PropIgnoreThrottled, static bool? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadNullableValue<bool>(o)))
			{
				continue;
			}

			if (propIgnoreUnavailable.TryReadProperty(ref reader, options, PropIgnoreUnavailable, static bool? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadNullableValue<bool>(o)))
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
		return new Elastic.Clients.Elasticsearch.IndicesOptions(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			AllowNoIndices = propAllowNoIndices.Value,
			ExpandWildcards = propExpandWildcards.Value,
			IgnoreThrottled = propIgnoreThrottled.Value,
			IgnoreUnavailable = propIgnoreUnavailable.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.IndicesOptions value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropAllowNoIndices, value.AllowNoIndices, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, bool? v) => w.WriteNullableValue<bool>(o, v));
		writer.WriteProperty(options, PropExpandWildcards, value.ExpandWildcards, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.ExpandWildcard>? v) => w.WriteSingleOrManyCollectionValue<Elastic.Clients.Elasticsearch.ExpandWildcard>(o, v, null));
		writer.WriteProperty(options, PropIgnoreThrottled, value.IgnoreThrottled, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, bool? v) => w.WriteNullableValue<bool>(o, v));
		writer.WriteProperty(options, PropIgnoreUnavailable, value.IgnoreUnavailable, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, bool? v) => w.WriteNullableValue<bool>(o, v));
		writer.WriteEndObject();
	}
}

/// <summary>
/// <para>
/// Controls how to deal with unavailable concrete indices (closed or missing), how wildcard expressions are expanded
/// to actual indices (all, closed or open indices) and how to deal with wildcard expressions that resolve to no indices.
/// </para>
/// </summary>
[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.IndicesOptionsConverter))]
public sealed partial class IndicesOptions
{
#if NET7_0_OR_GREATER
	public IndicesOptions()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	public IndicesOptions()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal IndicesOptions(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	/// <summary>
	/// <para>
	/// If false, the request returns an error if any wildcard expression, index alias, or <c>_all</c> value targets only
	/// missing or closed indices. This behavior applies even if the request targets other open indices. For example,
	/// a request targeting <c>foo*,bar*</c> returns an error if an index starts with <c>foo</c> but no index starts with <c>bar</c>.
	/// </para>
	/// </summary>
	public bool? AllowNoIndices { get; set; }

	/// <summary>
	/// <para>
	/// Type of index that wildcard patterns can match. If the request can target data streams, this argument
	/// determines whether wildcard expressions match hidden data streams. Supports comma-separated values,
	/// such as <c>open,hidden</c>.
	/// </para>
	/// </summary>
	public System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.ExpandWildcard>? ExpandWildcards { get; set; }

	/// <summary>
	/// <para>
	/// If true, concrete, expanded or aliased indices are ignored when frozen.
	/// </para>
	/// </summary>
	public bool? IgnoreThrottled { get; set; }

	/// <summary>
	/// <para>
	/// If true, missing or closed indices are not included in the response.
	/// </para>
	/// </summary>
	public bool? IgnoreUnavailable { get; set; }
}

/// <summary>
/// <para>
/// Controls how to deal with unavailable concrete indices (closed or missing), how wildcard expressions are expanded
/// to actual indices (all, closed or open indices) and how to deal with wildcard expressions that resolve to no indices.
/// </para>
/// </summary>
public readonly partial struct IndicesOptionsDescriptor
{
	internal Elastic.Clients.Elasticsearch.IndicesOptions Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public IndicesOptionsDescriptor(Elastic.Clients.Elasticsearch.IndicesOptions instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public IndicesOptionsDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.IndicesOptions(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.IndicesOptionsDescriptor(Elastic.Clients.Elasticsearch.IndicesOptions instance) => new Elastic.Clients.Elasticsearch.IndicesOptionsDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.IndicesOptions(Elastic.Clients.Elasticsearch.IndicesOptionsDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// If false, the request returns an error if any wildcard expression, index alias, or <c>_all</c> value targets only
	/// missing or closed indices. This behavior applies even if the request targets other open indices. For example,
	/// a request targeting <c>foo*,bar*</c> returns an error if an index starts with <c>foo</c> but no index starts with <c>bar</c>.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndicesOptionsDescriptor AllowNoIndices(bool? value = true)
	{
		Instance.AllowNoIndices = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Type of index that wildcard patterns can match. If the request can target data streams, this argument
	/// determines whether wildcard expressions match hidden data streams. Supports comma-separated values,
	/// such as <c>open,hidden</c>.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndicesOptionsDescriptor ExpandWildcards(System.Collections.Generic.ICollection<Elastic.Clients.Elasticsearch.ExpandWildcard>? value)
	{
		Instance.ExpandWildcards = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Type of index that wildcard patterns can match. If the request can target data streams, this argument
	/// determines whether wildcard expressions match hidden data streams. Supports comma-separated values,
	/// such as <c>open,hidden</c>.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndicesOptionsDescriptor ExpandWildcards(params Elastic.Clients.Elasticsearch.ExpandWildcard[] values)
	{
		Instance.ExpandWildcards = [.. values];
		return this;
	}

	/// <summary>
	/// <para>
	/// If true, concrete, expanded or aliased indices are ignored when frozen.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndicesOptionsDescriptor IgnoreThrottled(bool? value = true)
	{
		Instance.IgnoreThrottled = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// If true, missing or closed indices are not included in the response.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.IndicesOptionsDescriptor IgnoreUnavailable(bool? value = true)
	{
		Instance.IgnoreUnavailable = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.IndicesOptions Build(System.Action<Elastic.Clients.Elasticsearch.IndicesOptionsDescriptor>? action)
	{
		if (action is null)
		{
			return new Elastic.Clients.Elasticsearch.IndicesOptions(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
		}

		var builder = new Elastic.Clients.Elasticsearch.IndicesOptionsDescriptor(new Elastic.Clients.Elasticsearch.IndicesOptions(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}