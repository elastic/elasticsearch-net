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

namespace Elastic.Clients.Elasticsearch.Eql;

internal sealed partial class EqlGetResponseConverter<TEvent> : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Eql.EqlGetResponse<TEvent>>
{
	private static readonly System.Text.Json.JsonEncodedText PropHits = System.Text.Json.JsonEncodedText.Encode("hits");
	private static readonly System.Text.Json.JsonEncodedText PropId = System.Text.Json.JsonEncodedText.Encode("id");
	private static readonly System.Text.Json.JsonEncodedText PropIsPartial = System.Text.Json.JsonEncodedText.Encode("is_partial");
	private static readonly System.Text.Json.JsonEncodedText PropIsRunning = System.Text.Json.JsonEncodedText.Encode("is_running");
	private static readonly System.Text.Json.JsonEncodedText PropShardFailures = System.Text.Json.JsonEncodedText.Encode("shard_failures");
	private static readonly System.Text.Json.JsonEncodedText PropTimedOut = System.Text.Json.JsonEncodedText.Encode("timed_out");
	private static readonly System.Text.Json.JsonEncodedText PropTook = System.Text.Json.JsonEncodedText.Encode("took");

	public override Elastic.Clients.Elasticsearch.Eql.EqlGetResponse<TEvent> Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<Elastic.Clients.Elasticsearch.Eql.EqlHits<TEvent>> propHits = default;
		LocalJsonValue<string?> propId = default;
		LocalJsonValue<bool?> propIsPartial = default;
		LocalJsonValue<bool?> propIsRunning = default;
		LocalJsonValue<System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.ShardFailure>?> propShardFailures = default;
		LocalJsonValue<bool?> propTimedOut = default;
		LocalJsonValue<System.TimeSpan?> propTook = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propHits.TryReadProperty(ref reader, options, PropHits, null))
			{
				continue;
			}

			if (propId.TryReadProperty(ref reader, options, PropId, null))
			{
				continue;
			}

			if (propIsPartial.TryReadProperty(ref reader, options, PropIsPartial, null))
			{
				continue;
			}

			if (propIsRunning.TryReadProperty(ref reader, options, PropIsRunning, null))
			{
				continue;
			}

			if (propShardFailures.TryReadProperty(ref reader, options, PropShardFailures, static System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.ShardFailure>? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadCollectionValue<Elastic.Clients.Elasticsearch.ShardFailure>(o, null)))
			{
				continue;
			}

			if (propTimedOut.TryReadProperty(ref reader, options, PropTimedOut, null))
			{
				continue;
			}

			if (propTook.TryReadProperty(ref reader, options, PropTook, static System.TimeSpan? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadValueEx<System.TimeSpan?>(o, typeof(Elastic.Clients.Elasticsearch.Serialization.TimeSpanMillisMarker))))
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
		return new Elastic.Clients.Elasticsearch.Eql.EqlGetResponse<TEvent>(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Hits = propHits.Value,
			Id = propId.Value,
			IsPartial = propIsPartial.Value,
			IsRunning = propIsRunning.Value,
			ShardFailures = propShardFailures.Value,
			TimedOut = propTimedOut.Value,
			Took = propTook.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Eql.EqlGetResponse<TEvent> value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropHits, value.Hits, null, null);
		writer.WriteProperty(options, PropId, value.Id, null, null);
		writer.WriteProperty(options, PropIsPartial, value.IsPartial, null, null);
		writer.WriteProperty(options, PropIsRunning, value.IsRunning, null, null);
		writer.WriteProperty(options, PropShardFailures, value.ShardFailures, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.ShardFailure>? v) => w.WriteCollectionValue<Elastic.Clients.Elasticsearch.ShardFailure>(o, v, null));
		writer.WriteProperty(options, PropTimedOut, value.TimedOut, null, null);
		writer.WriteProperty(options, PropTook, value.Took, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.TimeSpan? v) => w.WriteValueEx<System.TimeSpan?>(o, v, typeof(Elastic.Clients.Elasticsearch.Serialization.TimeSpanMillisMarker)));
		writer.WriteEndObject();
	}
}

internal sealed partial class EqlGetResponseConverterFactory : System.Text.Json.Serialization.JsonConverterFactory
{
	public override bool CanConvert(System.Type typeToConvert)
	{
		return typeToConvert.IsGenericType && typeToConvert.GetGenericTypeDefinition() == typeof(EqlGetResponse<>);
	}

	public override System.Text.Json.Serialization.JsonConverter CreateConverter(System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		var args = typeToConvert.GetGenericArguments();
#pragma warning disable IL3050
		var converter = (System.Text.Json.Serialization.JsonConverter)System.Activator.CreateInstance(typeof(EqlGetResponseConverter<>).MakeGenericType(args[0]), System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public, binder: null, args: null, culture: null)!;
#pragma warning restore IL3050
		return converter;
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Eql.EqlGetResponseConverterFactory))]
public partial class EqlGetResponse<TEvent> : Elastic.Transport.Products.Elasticsearch.ElasticsearchResponse
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public EqlGetResponse(Elastic.Clients.Elasticsearch.Eql.EqlHits<TEvent> hits)
	{
		Hits = hits;
	}

	[System.Obsolete("The type contains additional required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public EqlGetResponse()
	{
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal EqlGetResponse(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	/// <summary>
	/// <para>
	/// Contains matching events and sequences. Also contains related metadata.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.Eql.EqlHits<TEvent> Hits { get; set; }

	/// <summary>
	/// <para>
	/// Identifier for the search.
	/// </para>
	/// </summary>
	public string? Id { get; set; }

	/// <summary>
	/// <para>
	/// If true, the response does not contain complete search results.
	/// </para>
	/// </summary>
	public bool? IsPartial { get; set; }

	/// <summary>
	/// <para>
	/// If true, the search request is still executing.
	/// </para>
	/// </summary>
	public bool? IsRunning { get; set; }

	/// <summary>
	/// <para>
	/// Contains information about shard failures (if any), in case allow_partial_search_results=true
	/// </para>
	/// </summary>
	public System.Collections.Generic.IReadOnlyCollection<Elastic.Clients.Elasticsearch.ShardFailure>? ShardFailures { get; set; }

	/// <summary>
	/// <para>
	/// If true, the request timed out before completion.
	/// </para>
	/// </summary>
	public bool? TimedOut { get; set; }

	/// <summary>
	/// <para>
	/// Milliseconds it took Elasticsearch to execute the request.
	/// </para>
	/// </summary>
	public System.TimeSpan? Took { get; set; }
}