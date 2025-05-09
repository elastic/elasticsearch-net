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

namespace Elastic.Clients.Elasticsearch.TextStructure;

internal sealed partial class MatchedFieldConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.TextStructure.MatchedField>
{
	private static readonly System.Text.Json.JsonEncodedText PropLength = System.Text.Json.JsonEncodedText.Encode("length");
	private static readonly System.Text.Json.JsonEncodedText PropMatch = System.Text.Json.JsonEncodedText.Encode("match");
	private static readonly System.Text.Json.JsonEncodedText PropOffset = System.Text.Json.JsonEncodedText.Encode("offset");

	public override Elastic.Clients.Elasticsearch.TextStructure.MatchedField Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<int> propLength = default;
		LocalJsonValue<string> propMatch = default;
		LocalJsonValue<int> propOffset = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propLength.TryReadProperty(ref reader, options, PropLength, null))
			{
				continue;
			}

			if (propMatch.TryReadProperty(ref reader, options, PropMatch, null))
			{
				continue;
			}

			if (propOffset.TryReadProperty(ref reader, options, PropOffset, null))
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
		return new Elastic.Clients.Elasticsearch.TextStructure.MatchedField(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Length = propLength.Value,
			Match = propMatch.Value,
			Offset = propOffset.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.TextStructure.MatchedField value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropLength, value.Length, null, null);
		writer.WriteProperty(options, PropMatch, value.Match, null, null);
		writer.WriteProperty(options, PropOffset, value.Offset, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.TextStructure.MatchedFieldConverter))]
public sealed partial class MatchedField
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public MatchedField(int length, string match, int offset)
	{
		Length = length;
		Match = match;
		Offset = offset;
	}
#if NET7_0_OR_GREATER
	public MatchedField()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public MatchedField()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal MatchedField(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	public
#if NET7_0_OR_GREATER
	required
#endif
	int Length { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	string Match { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	int Offset { get; set; }
}