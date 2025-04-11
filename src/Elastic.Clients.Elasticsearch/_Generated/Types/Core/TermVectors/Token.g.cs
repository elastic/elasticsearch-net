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

namespace Elastic.Clients.Elasticsearch.Core.TermVectors;

internal sealed partial class TokenConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Core.TermVectors.Token>
{
	private static readonly System.Text.Json.JsonEncodedText PropEndOffset = System.Text.Json.JsonEncodedText.Encode("end_offset");
	private static readonly System.Text.Json.JsonEncodedText PropPayload = System.Text.Json.JsonEncodedText.Encode("payload");
	private static readonly System.Text.Json.JsonEncodedText PropPosition = System.Text.Json.JsonEncodedText.Encode("position");
	private static readonly System.Text.Json.JsonEncodedText PropStartOffset = System.Text.Json.JsonEncodedText.Encode("start_offset");

	public override Elastic.Clients.Elasticsearch.Core.TermVectors.Token Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<int?> propEndOffset = default;
		LocalJsonValue<string?> propPayload = default;
		LocalJsonValue<int> propPosition = default;
		LocalJsonValue<int?> propStartOffset = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propEndOffset.TryReadProperty(ref reader, options, PropEndOffset, null))
			{
				continue;
			}

			if (propPayload.TryReadProperty(ref reader, options, PropPayload, null))
			{
				continue;
			}

			if (propPosition.TryReadProperty(ref reader, options, PropPosition, null))
			{
				continue;
			}

			if (propStartOffset.TryReadProperty(ref reader, options, PropStartOffset, null))
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
		return new Elastic.Clients.Elasticsearch.Core.TermVectors.Token(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			EndOffset = propEndOffset.Value,
			Payload = propPayload.Value,
			Position = propPosition.Value,
			StartOffset = propStartOffset.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Core.TermVectors.Token value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropEndOffset, value.EndOffset, null, null);
		writer.WriteProperty(options, PropPayload, value.Payload, null, null);
		writer.WriteProperty(options, PropPosition, value.Position, null, null);
		writer.WriteProperty(options, PropStartOffset, value.StartOffset, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Core.TermVectors.TokenConverter))]
public sealed partial class Token
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public Token(int position)
	{
		Position = position;
	}
#if NET7_0_OR_GREATER
	public Token()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public Token()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal Token(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	public int? EndOffset { get; set; }
	public string? Payload { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	int Position { get; set; }
	public int? StartOffset { get; set; }
}