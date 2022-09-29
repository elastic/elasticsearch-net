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

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;

#nullable restore
namespace Elastic.Clients.Elasticsearch
{
	internal sealed class ErrorCauseConverter : JsonConverter<ErrorCause>
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
					var value = JsonSerializer.Deserialize<object>(ref reader, options);
					additionalProperties.Add(property, value);
				}
			}

			return new ErrorCause { CausedBy = causedBy, Reason = reason, RootCause = rootCause, StackTrace = stackTrace, Suppressed = suppressed, Type = type, Metadata = additionalProperties };
		}

		public override void Write(Utf8JsonWriter writer, ErrorCause value, JsonSerializerOptions options)
		{
			throw new NotImplementedException("'ErrorCause' is a readonly type, used only on responses and does not support being written to JSON.");
		}
	}

	[JsonConverter(typeof(ErrorCauseConverter))]
	public sealed partial class ErrorCause
	{
		public Elastic.Clients.Elasticsearch.ErrorCause? CausedBy { get; init; }

		public Dictionary<string, object> Metadata { get; init; }

		public string? Reason { get; init; }

		public IReadOnlyCollection<Elastic.Clients.Elasticsearch.ErrorCause>? RootCause { get; init; }

		public string? StackTrace { get; init; }

		public IReadOnlyCollection<Elastic.Clients.Elasticsearch.ErrorCause>? Suppressed { get; init; }

		public string Type { get; init; }
	}
}