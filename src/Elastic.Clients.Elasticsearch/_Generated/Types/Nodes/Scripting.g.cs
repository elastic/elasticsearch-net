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

namespace Elastic.Clients.Elasticsearch.Nodes;

internal sealed partial class ScriptingConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Nodes.Scripting>
{
	private static readonly System.Text.Json.JsonEncodedText PropCacheEvictions = System.Text.Json.JsonEncodedText.Encode("cache_evictions");
	private static readonly System.Text.Json.JsonEncodedText PropCompilationLimitTriggered = System.Text.Json.JsonEncodedText.Encode("compilation_limit_triggered");
	private static readonly System.Text.Json.JsonEncodedText PropCompilations = System.Text.Json.JsonEncodedText.Encode("compilations");
	private static readonly System.Text.Json.JsonEncodedText PropCompilationsHistory = System.Text.Json.JsonEncodedText.Encode("compilations_history");
	private static readonly System.Text.Json.JsonEncodedText PropContexts = System.Text.Json.JsonEncodedText.Encode("contexts");

	public override Elastic.Clients.Elasticsearch.Nodes.Scripting Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<long?> propCacheEvictions = default;
		LocalJsonValue<long?> propCompilationLimitTriggered = default;
		LocalJsonValue<long?> propCompilations = default;
		LocalJsonValue<System.Collections.Generic.IReadOnlyDictionary<string, long>?> propCompilationsHistory = default;
		LocalJsonValue<System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.Nodes.Context>?> propContexts = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propCacheEvictions.TryReadProperty(ref reader, options, PropCacheEvictions, static long? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadNullableValue<long>(o)))
			{
				continue;
			}

			if (propCompilationLimitTriggered.TryReadProperty(ref reader, options, PropCompilationLimitTriggered, static long? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadNullableValue<long>(o)))
			{
				continue;
			}

			if (propCompilations.TryReadProperty(ref reader, options, PropCompilations, static long? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadNullableValue<long>(o)))
			{
				continue;
			}

			if (propCompilationsHistory.TryReadProperty(ref reader, options, PropCompilationsHistory, static System.Collections.Generic.IReadOnlyDictionary<string, long>? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadDictionaryValue<string, long>(o, null, null)))
			{
				continue;
			}

			if (propContexts.TryReadProperty(ref reader, options, PropContexts, static System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.Nodes.Context>? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadCollectionValue<Elastic.Clients.Elasticsearch.Nodes.Context>(o, null)))
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
		return new Elastic.Clients.Elasticsearch.Nodes.Scripting(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			CacheEvictions = propCacheEvictions.Value,
			CompilationLimitTriggered = propCompilationLimitTriggered.Value,
			Compilations = propCompilations.Value,
			CompilationsHistory = propCompilationsHistory.Value,
			Contexts = propContexts.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Nodes.Scripting value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropCacheEvictions, value.CacheEvictions, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, long? v) => w.WriteNullableValue<long>(o, v));
		writer.WriteProperty(options, PropCompilationLimitTriggered, value.CompilationLimitTriggered, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, long? v) => w.WriteNullableValue<long>(o, v));
		writer.WriteProperty(options, PropCompilations, value.Compilations, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, long? v) => w.WriteNullableValue<long>(o, v));
		writer.WriteProperty(options, PropCompilationsHistory, value.CompilationsHistory, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.IReadOnlyDictionary<string, long>? v) => w.WriteDictionaryValue<string, long>(o, v, null, null));
		writer.WriteProperty(options, PropContexts, value.Contexts, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.Nodes.Context>? v) => w.WriteCollectionValue<Elastic.Clients.Elasticsearch.Nodes.Context>(o, v, null));
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Nodes.ScriptingConverter))]
public sealed partial class Scripting
{
#if NET7_0_OR_GREATER
	public Scripting()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	public Scripting()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal Scripting(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	/// <summary>
	/// <para>
	/// Total number of times the script cache has evicted old data.
	/// </para>
	/// </summary>
	public long? CacheEvictions { get; set; }

	/// <summary>
	/// <para>
	/// Total number of times the script compilation circuit breaker has limited inline script compilations.
	/// </para>
	/// </summary>
	public long? CompilationLimitTriggered { get; set; }

	/// <summary>
	/// <para>
	/// Total number of inline script compilations performed by the node.
	/// </para>
	/// </summary>
	public long? Compilations { get; set; }

	/// <summary>
	/// <para>
	/// Contains this recent history of script compilations.
	/// </para>
	/// </summary>
	public System.Collections.Generic.IReadOnlyDictionary<string, long>? CompilationsHistory { get; set; }
	public System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.Nodes.Context>? Contexts { get; set; }
}