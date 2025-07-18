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

namespace Elastic.Clients.Elasticsearch.AsyncSearch;

internal sealed partial class SubmitAsyncSearchResponseConverter<TDocument> : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.AsyncSearch.SubmitAsyncSearchResponse<TDocument>>
{
	private static readonly System.Text.Json.JsonEncodedText PropCompletionTime = System.Text.Json.JsonEncodedText.Encode("completion_time");
	private static readonly System.Text.Json.JsonEncodedText PropCompletionTimeInMillis = System.Text.Json.JsonEncodedText.Encode("completion_time_in_millis");
	private static readonly System.Text.Json.JsonEncodedText PropExpirationTime = System.Text.Json.JsonEncodedText.Encode("expiration_time");
	private static readonly System.Text.Json.JsonEncodedText PropExpirationTimeInMillis = System.Text.Json.JsonEncodedText.Encode("expiration_time_in_millis");
	private static readonly System.Text.Json.JsonEncodedText PropId = System.Text.Json.JsonEncodedText.Encode("id");
	private static readonly System.Text.Json.JsonEncodedText PropIsPartial = System.Text.Json.JsonEncodedText.Encode("is_partial");
	private static readonly System.Text.Json.JsonEncodedText PropIsRunning = System.Text.Json.JsonEncodedText.Encode("is_running");
	private static readonly System.Text.Json.JsonEncodedText PropResponse = System.Text.Json.JsonEncodedText.Encode("response");
	private static readonly System.Text.Json.JsonEncodedText PropStartTime = System.Text.Json.JsonEncodedText.Encode("start_time");
	private static readonly System.Text.Json.JsonEncodedText PropStartTimeInMillis = System.Text.Json.JsonEncodedText.Encode("start_time_in_millis");

	public override Elastic.Clients.Elasticsearch.AsyncSearch.SubmitAsyncSearchResponse<TDocument> Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<System.DateTimeOffset?> propCompletionTime = default;
		LocalJsonValue<System.DateTimeOffset?> propCompletionTimeInMillis = default;
		LocalJsonValue<System.DateTimeOffset?> propExpirationTime = default;
		LocalJsonValue<System.DateTimeOffset> propExpirationTimeInMillis = default;
		LocalJsonValue<string?> propId = default;
		LocalJsonValue<bool> propIsPartial = default;
		LocalJsonValue<bool> propIsRunning = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.AsyncSearch.AsyncSearch<TDocument>> propResponse = default;
		LocalJsonValue<System.DateTimeOffset?> propStartTime = default;
		LocalJsonValue<System.DateTimeOffset> propStartTimeInMillis = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propCompletionTime.TryReadProperty(ref reader, options, PropCompletionTime, static System.DateTimeOffset? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadNullableValueEx<System.DateTimeOffset>(o, typeof(Elastic.Clients.Elasticsearch.Serialization.DateTimeMarker))))
			{
				continue;
			}

			if (propCompletionTimeInMillis.TryReadProperty(ref reader, options, PropCompletionTimeInMillis, static System.DateTimeOffset? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadNullableValueEx<System.DateTimeOffset>(o, typeof(Elastic.Clients.Elasticsearch.Serialization.DateTimeMillisMarker))))
			{
				continue;
			}

			if (propExpirationTime.TryReadProperty(ref reader, options, PropExpirationTime, static System.DateTimeOffset? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadNullableValueEx<System.DateTimeOffset>(o, typeof(Elastic.Clients.Elasticsearch.Serialization.DateTimeMarker))))
			{
				continue;
			}

			if (propExpirationTimeInMillis.TryReadProperty(ref reader, options, PropExpirationTimeInMillis, static System.DateTimeOffset (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadValueEx<System.DateTimeOffset>(o, typeof(Elastic.Clients.Elasticsearch.Serialization.DateTimeMillisMarker))))
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

			if (propResponse.TryReadProperty(ref reader, options, PropResponse, null))
			{
				continue;
			}

			if (propStartTime.TryReadProperty(ref reader, options, PropStartTime, static System.DateTimeOffset? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadNullableValueEx<System.DateTimeOffset>(o, typeof(Elastic.Clients.Elasticsearch.Serialization.DateTimeMarker))))
			{
				continue;
			}

			if (propStartTimeInMillis.TryReadProperty(ref reader, options, PropStartTimeInMillis, static System.DateTimeOffset (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadValueEx<System.DateTimeOffset>(o, typeof(Elastic.Clients.Elasticsearch.Serialization.DateTimeMillisMarker))))
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
		return new Elastic.Clients.Elasticsearch.AsyncSearch.SubmitAsyncSearchResponse<TDocument>(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			CompletionTime = propCompletionTime.Value,
			CompletionTimeInMillis = propCompletionTimeInMillis.Value,
			ExpirationTime = propExpirationTime.Value,
			ExpirationTimeInMillis = propExpirationTimeInMillis.Value,
			Id = propId.Value,
			IsPartial = propIsPartial.Value,
			IsRunning = propIsRunning.Value,
			Response = propResponse.Value,
			StartTime = propStartTime.Value,
			StartTimeInMillis = propStartTimeInMillis.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.AsyncSearch.SubmitAsyncSearchResponse<TDocument> value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropCompletionTime, value.CompletionTime, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.DateTimeOffset? v) => w.WriteNullableValueEx<System.DateTimeOffset>(o, v, typeof(Elastic.Clients.Elasticsearch.Serialization.DateTimeMarker)));
		writer.WriteProperty(options, PropCompletionTimeInMillis, value.CompletionTimeInMillis, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.DateTimeOffset? v) => w.WriteNullableValueEx<System.DateTimeOffset>(o, v, typeof(Elastic.Clients.Elasticsearch.Serialization.DateTimeMillisMarker)));
		writer.WriteProperty(options, PropExpirationTime, value.ExpirationTime, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.DateTimeOffset? v) => w.WriteNullableValueEx<System.DateTimeOffset>(o, v, typeof(Elastic.Clients.Elasticsearch.Serialization.DateTimeMarker)));
		writer.WriteProperty(options, PropExpirationTimeInMillis, value.ExpirationTimeInMillis, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.DateTimeOffset v) => w.WriteValueEx<System.DateTimeOffset>(o, v, typeof(Elastic.Clients.Elasticsearch.Serialization.DateTimeMillisMarker)));
		writer.WriteProperty(options, PropId, value.Id, null, null);
		writer.WriteProperty(options, PropIsPartial, value.IsPartial, null, null);
		writer.WriteProperty(options, PropIsRunning, value.IsRunning, null, null);
		writer.WriteProperty(options, PropResponse, value.Response, null, null);
		writer.WriteProperty(options, PropStartTime, value.StartTime, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.DateTimeOffset? v) => w.WriteNullableValueEx<System.DateTimeOffset>(o, v, typeof(Elastic.Clients.Elasticsearch.Serialization.DateTimeMarker)));
		writer.WriteProperty(options, PropStartTimeInMillis, value.StartTimeInMillis, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.DateTimeOffset v) => w.WriteValueEx<System.DateTimeOffset>(o, v, typeof(Elastic.Clients.Elasticsearch.Serialization.DateTimeMillisMarker)));
		writer.WriteEndObject();
	}
}

internal sealed partial class SubmitAsyncSearchResponseConverterFactory : System.Text.Json.Serialization.JsonConverterFactory
{
	public override bool CanConvert(System.Type typeToConvert)
	{
		return typeToConvert.IsGenericType && typeToConvert.GetGenericTypeDefinition() == typeof(SubmitAsyncSearchResponse<>);
	}

	public override System.Text.Json.Serialization.JsonConverter CreateConverter(System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		var args = typeToConvert.GetGenericArguments();
#pragma warning disable IL3050
		var converter = (System.Text.Json.Serialization.JsonConverter)System.Activator.CreateInstance(typeof(SubmitAsyncSearchResponseConverter<>).MakeGenericType(args[0]), System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public, binder: null, args: null, culture: null)!;
#pragma warning restore IL3050
		return converter;
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.AsyncSearch.SubmitAsyncSearchResponseConverterFactory))]
public sealed partial class SubmitAsyncSearchResponse<TDocument> : Elastic.Transport.Products.Elasticsearch.ElasticsearchResponse
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public SubmitAsyncSearchResponse()
	{
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal SubmitAsyncSearchResponse(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	/// <summary>
	/// <para>
	/// Indicates when the async search completed.
	/// It is present only when the search has completed.
	/// </para>
	/// </summary>
	public System.DateTimeOffset? CompletionTime { get; set; }
	public System.DateTimeOffset? CompletionTimeInMillis { get; set; }

	/// <summary>
	/// <para>
	/// Indicates when the async search will expire.
	/// </para>
	/// </summary>
	public System.DateTimeOffset? ExpirationTime { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	System.DateTimeOffset ExpirationTimeInMillis { get; set; }
	public string? Id { get; set; }

	/// <summary>
	/// <para>
	/// When the query is no longer running, this property indicates whether the search failed or was successfully completed on all shards.
	/// While the query is running, <c>is_partial</c> is always set to <c>true</c>.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	bool IsPartial { get; set; }

	/// <summary>
	/// <para>
	/// Indicates whether the search is still running or has completed.
	/// </para>
	/// <para>
	/// info
	/// If the search failed after some shards returned their results or the node that is coordinating the async search dies, results may be partial even though <c>is_running</c> is <c>false</c>.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	bool IsRunning { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.AsyncSearch.AsyncSearch<TDocument> Response { get; set; }
	public System.DateTimeOffset? StartTime { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	System.DateTimeOffset StartTimeInMillis { get; set; }
}