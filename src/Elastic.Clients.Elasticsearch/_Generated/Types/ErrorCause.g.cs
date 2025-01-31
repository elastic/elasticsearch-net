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
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch;

internal sealed partial class ErrorCauseConverter : System.Text.Json.Serialization.JsonConverter<ErrorCause>
{
	private static readonly System.Text.Json.JsonEncodedText PropCausedBy = System.Text.Json.JsonEncodedText.Encode("caused_by");
	private static readonly System.Text.Json.JsonEncodedText PropReason = System.Text.Json.JsonEncodedText.Encode("reason");
	private static readonly System.Text.Json.JsonEncodedText PropRootCause = System.Text.Json.JsonEncodedText.Encode("root_cause");
	private static readonly System.Text.Json.JsonEncodedText PropStackTrace = System.Text.Json.JsonEncodedText.Encode("stack_trace");
	private static readonly System.Text.Json.JsonEncodedText PropSuppressed = System.Text.Json.JsonEncodedText.Encode("suppressed");
	private static readonly System.Text.Json.JsonEncodedText PropType = System.Text.Json.JsonEncodedText.Encode("type");

	public override ErrorCause Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		System.Collections.Generic.Dictionary<string, object> propMetadata = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.ErrorCause?> propCausedBy = default;
		LocalJsonValue<string?> propReason = default;
		LocalJsonValue<IReadOnlyCollection<Elastic.Clients.Elasticsearch.ErrorCause>?> propRootCause = default;
		LocalJsonValue<string?> propStackTrace = default;
		LocalJsonValue<IReadOnlyCollection<Elastic.Clients.Elasticsearch.ErrorCause>?> propSuppressed = default;
		LocalJsonValue<string> propType = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propCausedBy.TryRead(ref reader, options, PropCausedBy))
			{
				continue;
			}

			if (propReason.TryRead(ref reader, options, PropReason))
			{
				continue;
			}

			if (propRootCause.TryRead(ref reader, options, PropRootCause))
			{
				continue;
			}

			if (propStackTrace.TryRead(ref reader, options, PropStackTrace))
			{
				continue;
			}

			if (propSuppressed.TryRead(ref reader, options, PropSuppressed))
			{
				continue;
			}

			if (propType.TryRead(ref reader, options, PropType))
			{
				continue;
			}

			propMetadata ??= new System.Collections.Generic.Dictionary<string, object>();
			reader.ReadProperty(options, out string key, out object value);
			propMetadata[key] = value;
		}

		reader.ValidateToken(System.Text.Json.JsonTokenType.EndObject);
		return new ErrorCause
		{
			Metadata = propMetadata
,
			CausedBy = propCausedBy.Value
,
			Reason = propReason.Value
,
			RootCause = propRootCause.Value
,
			StackTrace = propStackTrace.Value
,
			Suppressed = propSuppressed.Value
,
			Type = propType.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, ErrorCause value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropCausedBy, value.CausedBy);
		writer.WriteProperty(options, PropReason, value.Reason);
		writer.WriteProperty(options, PropRootCause, value.RootCause);
		writer.WriteProperty(options, PropStackTrace, value.StackTrace);
		writer.WriteProperty(options, PropSuppressed, value.Suppressed);
		writer.WriteProperty(options, PropType, value.Type);
		if (value.Metadata is not null)
		{
			foreach (var item in value.Metadata)
			{
				writer.WriteProperty(options, item.Key, item.Value);
			}
		}

		writer.WriteEndObject();
	}
}

/// <summary>
/// <para>
/// Cause and details about a request failure. This class defines the properties common to all error types.
/// Additional details are also provided, that depend on the error type.
/// </para>
/// </summary>
[JsonConverter(typeof(ErrorCauseConverter))]
public sealed partial class ErrorCause
{
	public Elastic.Clients.Elasticsearch.ErrorCause? CausedBy { get; init; }

	/// <summary>
	/// <para>
	/// Additional details about the error
	/// </para>
	/// </summary>
	public IReadOnlyDictionary<string, object> Metadata { get; init; }

	/// <summary>
	/// <para>
	/// A human-readable explanation of the error, in english
	/// </para>
	/// </summary>
	public string? Reason { get; init; }
	public IReadOnlyCollection<Elastic.Clients.Elasticsearch.ErrorCause>? RootCause { get; init; }

	/// <summary>
	/// <para>
	/// The server stack trace. Present only if the <c>error_trace=true</c> parameter was sent with the request.
	/// </para>
	/// </summary>
	public string? StackTrace { get; init; }
	public IReadOnlyCollection<Elastic.Clients.Elasticsearch.ErrorCause>? Suppressed { get; init; }

	/// <summary>
	/// <para>
	/// The type of error
	/// </para>
	/// </summary>
	public string Type { get; init; }
}