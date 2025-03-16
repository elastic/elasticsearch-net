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

internal sealed partial class ErrorCauseConverter : JsonConverter<ErrorCause>
{
	public override ErrorCause Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		if (reader.TokenType != JsonTokenType.StartObject)
			throw new JsonException("Unexpected JSON detected.");
		Elastic.Clients.Elasticsearch.ErrorCause? causedBy = default;
		string? reason = default;
		IReadOnlyCollection<Elastic.Clients.Elasticsearch.ErrorCause>? rootCause = default;
		string? stackTrace = default;
		IReadOnlyCollection<Elastic.Clients.Elasticsearch.ErrorCause>? suppressed = default;
		string type = default;
		Dictionary<string, object> additionalProperties = null;
		while (reader.Read() && reader.TokenType != JsonTokenType.EndObject)
		{
			if (reader.TokenType == JsonTokenType.PropertyName)
			{
				var property = reader.GetString();
				if (property == "caused_by")
				{
					causedBy = JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.ErrorCause?>(ref reader, options);
					continue;
				}

				if (property == "reason")
				{
					reason = JsonSerializer.Deserialize<string?>(ref reader, options);
					continue;
				}

				if (property == "root_cause")
				{
					rootCause = JsonSerializer.Deserialize<IReadOnlyCollection<Elastic.Clients.Elasticsearch.ErrorCause>?>(ref reader, options);
					continue;
				}

				if (property == "stack_trace")
				{
					stackTrace = JsonSerializer.Deserialize<string?>(ref reader, options);
					continue;
				}

				if (property == "suppressed")
				{
					suppressed = JsonSerializer.Deserialize<IReadOnlyCollection<Elastic.Clients.Elasticsearch.ErrorCause>?>(ref reader, options);
					continue;
				}

				if (property == "type")
				{
					type = JsonSerializer.Deserialize<string>(ref reader, options);
					continue;
				}

				additionalProperties ??= new Dictionary<string, object>();
				var additionalValue = JsonSerializer.Deserialize<object>(ref reader, options);
				additionalProperties.Add(property, additionalValue);
			}
		}

		return new ErrorCause { CausedBy = causedBy, Metadata = additionalProperties, Reason = reason, RootCause = rootCause, StackTrace = stackTrace, Suppressed = suppressed, Type = type };
	}

	public override void Write(Utf8JsonWriter writer, ErrorCause value, JsonSerializerOptions options)
	{
		throw new NotImplementedException("'ErrorCause' is a readonly type, used only on responses and does not support being written to JSON.");
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
	/// A human-readable explanation of the error, in English.
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