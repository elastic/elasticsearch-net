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
using Elastic.Transport.Products.Elasticsearch;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch.Eql;

internal sealed partial class GetEqlStatusResponseConverter : System.Text.Json.Serialization.JsonConverter<GetEqlStatusResponse>
{
	private static readonly System.Text.Json.JsonEncodedText PropCompletionStatus = System.Text.Json.JsonEncodedText.Encode("completion_status");
	private static readonly System.Text.Json.JsonEncodedText PropExpirationTimeInMillis = System.Text.Json.JsonEncodedText.Encode("expiration_time_in_millis");
	private static readonly System.Text.Json.JsonEncodedText PropId = System.Text.Json.JsonEncodedText.Encode("id");
	private static readonly System.Text.Json.JsonEncodedText PropIsPartial = System.Text.Json.JsonEncodedText.Encode("is_partial");
	private static readonly System.Text.Json.JsonEncodedText PropIsRunning = System.Text.Json.JsonEncodedText.Encode("is_running");
	private static readonly System.Text.Json.JsonEncodedText PropStartTimeInMillis = System.Text.Json.JsonEncodedText.Encode("start_time_in_millis");

	public override GetEqlStatusResponse Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<int?> propCompletionStatus = default;
		LocalJsonValue<long?> propExpirationTimeInMillis = default;
		LocalJsonValue<string> propId = default;
		LocalJsonValue<bool> propIsPartial = default;
		LocalJsonValue<bool> propIsRunning = default;
		LocalJsonValue<long?> propStartTimeInMillis = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propCompletionStatus.TryReadProperty(ref reader, options, PropCompletionStatus, null))
			{
				continue;
			}

			if (propExpirationTimeInMillis.TryReadProperty(ref reader, options, PropExpirationTimeInMillis, null))
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

			if (propStartTimeInMillis.TryReadProperty(ref reader, options, PropStartTimeInMillis, null))
			{
				continue;
			}

			if (options.UnmappedMemberHandling is System.Text.Json.Serialization.JsonUnmappedMemberHandling.Skip)
			{
				reader.Skip();
			}

			throw new System.Text.Json.JsonException($"Unknown JSON property '{reader.GetString()}' for type '{typeToConvert.Name}'.");
		}

		reader.ValidateToken(System.Text.Json.JsonTokenType.EndObject);
		return new GetEqlStatusResponse
		{
			CompletionStatus = propCompletionStatus.Value
,
			ExpirationTimeInMillis = propExpirationTimeInMillis.Value
,
			Id = propId.Value
,
			IsPartial = propIsPartial.Value
,
			IsRunning = propIsRunning.Value
,
			StartTimeInMillis = propStartTimeInMillis.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, GetEqlStatusResponse value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropCompletionStatus, value.CompletionStatus, null, null);
		writer.WriteProperty(options, PropExpirationTimeInMillis, value.ExpirationTimeInMillis, null, null);
		writer.WriteProperty(options, PropId, value.Id, null, null);
		writer.WriteProperty(options, PropIsPartial, value.IsPartial, null, null);
		writer.WriteProperty(options, PropIsRunning, value.IsRunning, null, null);
		writer.WriteProperty(options, PropStartTimeInMillis, value.StartTimeInMillis, null, null);
		writer.WriteEndObject();
	}
}

[JsonConverter(typeof(GetEqlStatusResponseConverter))]
public sealed partial class GetEqlStatusResponse : ElasticsearchResponse
{
	/// <summary>
	/// <para>
	/// For a completed search shows the http status code of the completed search.
	/// </para>
	/// </summary>
	public int? CompletionStatus { get; init; }

	/// <summary>
	/// <para>
	/// Shows a timestamp when the eql search will be expired, in milliseconds since the Unix epoch. When this time is reached, the search and its results are deleted, even if the search is still ongoing.
	/// </para>
	/// </summary>
	public long? ExpirationTimeInMillis { get; init; }

	/// <summary>
	/// <para>
	/// Identifier for the search.
	/// </para>
	/// </summary>
	public string Id { get; init; }

	/// <summary>
	/// <para>
	/// If true, the search request is still executing. If false, the search is completed.
	/// </para>
	/// </summary>
	public bool IsPartial { get; init; }

	/// <summary>
	/// <para>
	/// If true, the response does not contain complete search results. This could be because either the search is still running (is_running status is false), or because it is already completed (is_running status is true) and results are partial due to failures or timeouts.
	/// </para>
	/// </summary>
	public bool IsRunning { get; init; }

	/// <summary>
	/// <para>
	/// For a running search shows a timestamp when the eql search started, in milliseconds since the Unix epoch.
	/// </para>
	/// </summary>
	public long? StartTimeInMillis { get; init; }
}