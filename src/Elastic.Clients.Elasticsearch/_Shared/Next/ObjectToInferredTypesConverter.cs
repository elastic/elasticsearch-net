// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch.Serialization;

internal sealed class ObjectToInferredTypesConverter : JsonConverter<object>
{
	[UnconditionalSuppressMessage("AOT", "IL3050:Calling members annotated with 'RequiresDynamicCodeAttribute'", Justification = "Always using explicit TypeInfoResolver")]
	[UnconditionalSuppressMessage("Trimming", "IL2026:Members annotated with 'RequiresUnreferencedCodeAttribute'", Justification = "Always using explicit TypeInfoResolver")]
	public override object Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		return reader.TokenType switch
		{
			JsonTokenType.True => true,
			JsonTokenType.False => false,
			JsonTokenType.Number when reader.TryGetInt64(out var value) => value,
			JsonTokenType.Number => reader.GetDouble(),
			JsonTokenType.String when reader.TryGetDateTime(out var value) => value,
			JsonTokenType.String when reader.TryGetDateTimeOffset(out var value) => value,
			JsonTokenType.String => reader.GetString()!,
			_ => JsonDocument.ParseValue(ref reader).RootElement.Clone()
		};
	}

	[UnconditionalSuppressMessage("AOT", "IL3050:Calling members annotated with 'RequiresDynamicCodeAttribute'", Justification = "Always using explicit TypeInfoResolver")]
	[UnconditionalSuppressMessage("Trimming", "IL2026:Members annotated with 'RequiresUnreferencedCodeAttribute'", Justification = "Always using explicit TypeInfoResolver")]
	public override void Write(Utf8JsonWriter writer, object value, JsonSerializerOptions options)
	{
		// TODO: Match `SourceMarker<T>` values and delegate to the `SourceSerializer`.
		JsonSerializer.Serialize(writer, value, value.GetType(), options);
	}
}
