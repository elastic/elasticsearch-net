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

namespace Elastic.Clients.Elasticsearch.IndexManagement;

internal sealed partial class ExplainAnalyzeTokenConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.IndexManagement.ExplainAnalyzeToken>
{
	private static readonly System.Text.Json.JsonEncodedText PropBytes = System.Text.Json.JsonEncodedText.Encode("bytes");
	private static readonly System.Text.Json.JsonEncodedText PropEndOffset = System.Text.Json.JsonEncodedText.Encode("end_offset");
	private static readonly System.Text.Json.JsonEncodedText PropKeyword = System.Text.Json.JsonEncodedText.Encode("keyword");
	private static readonly System.Text.Json.JsonEncodedText PropPosition = System.Text.Json.JsonEncodedText.Encode("position");
	private static readonly System.Text.Json.JsonEncodedText PropPositionLength = System.Text.Json.JsonEncodedText.Encode("positionLength");
	private static readonly System.Text.Json.JsonEncodedText PropStartOffset = System.Text.Json.JsonEncodedText.Encode("start_offset");
	private static readonly System.Text.Json.JsonEncodedText PropTermFrequency = System.Text.Json.JsonEncodedText.Encode("termFrequency");
	private static readonly System.Text.Json.JsonEncodedText PropToken = System.Text.Json.JsonEncodedText.Encode("token");
	private static readonly System.Text.Json.JsonEncodedText PropType = System.Text.Json.JsonEncodedText.Encode("type");

	public override Elastic.Clients.Elasticsearch.IndexManagement.ExplainAnalyzeToken Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		System.Collections.Generic.Dictionary<string, object>? propAttributes = default;
		LocalJsonValue<string> propBytes = default;
		LocalJsonValue<long> propEndOffset = default;
		LocalJsonValue<bool?> propKeyword = default;
		LocalJsonValue<long> propPosition = default;
		LocalJsonValue<long> propPositionLength = default;
		LocalJsonValue<long> propStartOffset = default;
		LocalJsonValue<long> propTermFrequency = default;
		LocalJsonValue<string> propToken = default;
		LocalJsonValue<string> propType = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propBytes.TryReadProperty(ref reader, options, PropBytes, null))
			{
				continue;
			}

			if (propEndOffset.TryReadProperty(ref reader, options, PropEndOffset, null))
			{
				continue;
			}

			if (propKeyword.TryReadProperty(ref reader, options, PropKeyword, null))
			{
				continue;
			}

			if (propPosition.TryReadProperty(ref reader, options, PropPosition, null))
			{
				continue;
			}

			if (propPositionLength.TryReadProperty(ref reader, options, PropPositionLength, null))
			{
				continue;
			}

			if (propStartOffset.TryReadProperty(ref reader, options, PropStartOffset, null))
			{
				continue;
			}

			if (propTermFrequency.TryReadProperty(ref reader, options, PropTermFrequency, null))
			{
				continue;
			}

			if (propToken.TryReadProperty(ref reader, options, PropToken, null))
			{
				continue;
			}

			if (propType.TryReadProperty(ref reader, options, PropType, null))
			{
				continue;
			}

			propAttributes ??= new System.Collections.Generic.Dictionary<string, object>();
			reader.ReadProperty(options, out string key, out object value, null, null);
			propAttributes[key] = value;
		}

		reader.ValidateToken(System.Text.Json.JsonTokenType.EndObject);
		return new Elastic.Clients.Elasticsearch.IndexManagement.ExplainAnalyzeToken(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Attributes = propAttributes,
			Bytes = propBytes.Value,
			EndOffset = propEndOffset.Value,
			Keyword = propKeyword.Value,
			Position = propPosition.Value,
			PositionLength = propPositionLength.Value,
			StartOffset = propStartOffset.Value,
			TermFrequency = propTermFrequency.Value,
			Token = propToken.Value,
			Type = propType.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.IndexManagement.ExplainAnalyzeToken value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropBytes, value.Bytes, null, null);
		writer.WriteProperty(options, PropEndOffset, value.EndOffset, null, null);
		writer.WriteProperty(options, PropKeyword, value.Keyword, null, null);
		writer.WriteProperty(options, PropPosition, value.Position, null, null);
		writer.WriteProperty(options, PropPositionLength, value.PositionLength, null, null);
		writer.WriteProperty(options, PropStartOffset, value.StartOffset, null, null);
		writer.WriteProperty(options, PropTermFrequency, value.TermFrequency, null, null);
		writer.WriteProperty(options, PropToken, value.Token, null, null);
		writer.WriteProperty(options, PropType, value.Type, null, null);
		if (value.Attributes is not null)
		{
			foreach (var item in value.Attributes)
			{
				writer.WriteProperty(options, item.Key, item.Value, null, null);
			}
		}

		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.IndexManagement.ExplainAnalyzeTokenConverter))]
public sealed partial class ExplainAnalyzeToken
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public ExplainAnalyzeToken(string bytes, long endOffset, long position, long positionLength, long startOffset, long termFrequency, string token, string type)
	{
		Bytes = bytes;
		EndOffset = endOffset;
		Position = position;
		PositionLength = positionLength;
		StartOffset = startOffset;
		TermFrequency = termFrequency;
		Token = token;
		Type = type;
	}
#if NET7_0_OR_GREATER
	public ExplainAnalyzeToken()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public ExplainAnalyzeToken()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal ExplainAnalyzeToken(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	/// <summary>
	/// <para>
	/// Additional tokenizer-specific attributes
	/// </para>
	/// </summary>
	public System.Collections.Generic.IReadOnlyDictionary<string, object>? Attributes { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	string Bytes { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	long EndOffset { get; set; }
	public bool? Keyword { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	long Position { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	long PositionLength { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	long StartOffset { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	long TermFrequency { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	string Token { get; set; }
	public
#if NET7_0_OR_GREATER
	required
#endif
	string Type { get; set; }
}