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

using Elastic.Clients.Elasticsearch.Core;
using Elastic.Clients.Elasticsearch.Fluent;
using Elastic.Clients.Elasticsearch.Serialization;
using Elastic.Transport;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch.Analysis;

public partial class Tokenizers : IsADictionary<string, ITokenizer>
{
	public Tokenizers()
	{
	}

	public Tokenizers(IDictionary<string, ITokenizer> container) : base(container)
	{
	}

	public void Add(string name, ITokenizer tokenizer) => BackingDictionary.Add(Sanitize(name), tokenizer);
	public bool TryGetTokenizer(string name, [NotNullWhen(returnValue: true)] out ITokenizer tokenizer) => BackingDictionary.TryGetValue(Sanitize(name), out tokenizer);

	public bool TryGetTokenizer<T>(string name, [NotNullWhen(returnValue: true)] out T? tokenizer) where T : class, ITokenizer
	{
		if (BackingDictionary.TryGetValue(Sanitize(name), out var matchedValue) && matchedValue is T finalValue)
		{
			tokenizer = finalValue;
			return true;
		}

		tokenizer = null;
		return false;
	}
}

public sealed partial class TokenizersDescriptor : IsADictionaryDescriptor<TokenizersDescriptor, Tokenizers, string, ITokenizer>
{
	public TokenizersDescriptor() : base(new Tokenizers())
	{
	}

	public TokenizersDescriptor(Tokenizers tokenizers) : base(tokenizers ?? new Tokenizers())
	{
	}

	public TokenizersDescriptor CharGroup(string tokenizerName) => AssignVariant<Elastic.Clients.Elasticsearch.Analysis.CharGroupTokenizerDescriptor, CharGroupTokenizer>(tokenizerName, null);
	public TokenizersDescriptor CharGroup(string tokenizerName, Action<Elastic.Clients.Elasticsearch.Analysis.CharGroupTokenizerDescriptor> configure) => AssignVariant<Elastic.Clients.Elasticsearch.Analysis.CharGroupTokenizerDescriptor, CharGroupTokenizer>(tokenizerName, configure);
	public TokenizersDescriptor CharGroup(string tokenizerName, CharGroupTokenizer charGroupTokenizer) => AssignVariant(tokenizerName, charGroupTokenizer);
	public TokenizersDescriptor Classic(string tokenizerName) => AssignVariant<Elastic.Clients.Elasticsearch.Analysis.ClassicTokenizerDescriptor, ClassicTokenizer>(tokenizerName, null);
	public TokenizersDescriptor Classic(string tokenizerName, Action<Elastic.Clients.Elasticsearch.Analysis.ClassicTokenizerDescriptor> configure) => AssignVariant<Elastic.Clients.Elasticsearch.Analysis.ClassicTokenizerDescriptor, ClassicTokenizer>(tokenizerName, configure);
	public TokenizersDescriptor Classic(string tokenizerName, ClassicTokenizer classicTokenizer) => AssignVariant(tokenizerName, classicTokenizer);
	public TokenizersDescriptor EdgeNGram(string tokenizerName) => AssignVariant<Elastic.Clients.Elasticsearch.Analysis.EdgeNGramTokenizerDescriptor, EdgeNGramTokenizer>(tokenizerName, null);
	public TokenizersDescriptor EdgeNGram(string tokenizerName, Action<Elastic.Clients.Elasticsearch.Analysis.EdgeNGramTokenizerDescriptor> configure) => AssignVariant<Elastic.Clients.Elasticsearch.Analysis.EdgeNGramTokenizerDescriptor, EdgeNGramTokenizer>(tokenizerName, configure);
	public TokenizersDescriptor EdgeNGram(string tokenizerName, EdgeNGramTokenizer edgeNGramTokenizer) => AssignVariant(tokenizerName, edgeNGramTokenizer);
	public TokenizersDescriptor Icu(string tokenizerName) => AssignVariant<Elastic.Clients.Elasticsearch.Analysis.IcuTokenizerDescriptor, IcuTokenizer>(tokenizerName, null);
	public TokenizersDescriptor Icu(string tokenizerName, Action<Elastic.Clients.Elasticsearch.Analysis.IcuTokenizerDescriptor> configure) => AssignVariant<Elastic.Clients.Elasticsearch.Analysis.IcuTokenizerDescriptor, IcuTokenizer>(tokenizerName, configure);
	public TokenizersDescriptor Icu(string tokenizerName, IcuTokenizer icuTokenizer) => AssignVariant(tokenizerName, icuTokenizer);
	public TokenizersDescriptor Keyword(string tokenizerName) => AssignVariant<Elastic.Clients.Elasticsearch.Analysis.KeywordTokenizerDescriptor, KeywordTokenizer>(tokenizerName, null);
	public TokenizersDescriptor Keyword(string tokenizerName, Action<Elastic.Clients.Elasticsearch.Analysis.KeywordTokenizerDescriptor> configure) => AssignVariant<Elastic.Clients.Elasticsearch.Analysis.KeywordTokenizerDescriptor, KeywordTokenizer>(tokenizerName, configure);
	public TokenizersDescriptor Keyword(string tokenizerName, KeywordTokenizer keywordTokenizer) => AssignVariant(tokenizerName, keywordTokenizer);
	public TokenizersDescriptor Kuromoji(string tokenizerName) => AssignVariant<Elastic.Clients.Elasticsearch.Analysis.KuromojiTokenizerDescriptor, KuromojiTokenizer>(tokenizerName, null);
	public TokenizersDescriptor Kuromoji(string tokenizerName, Action<Elastic.Clients.Elasticsearch.Analysis.KuromojiTokenizerDescriptor> configure) => AssignVariant<Elastic.Clients.Elasticsearch.Analysis.KuromojiTokenizerDescriptor, KuromojiTokenizer>(tokenizerName, configure);
	public TokenizersDescriptor Kuromoji(string tokenizerName, KuromojiTokenizer kuromojiTokenizer) => AssignVariant(tokenizerName, kuromojiTokenizer);
	public TokenizersDescriptor Letter(string tokenizerName) => AssignVariant<Elastic.Clients.Elasticsearch.Analysis.LetterTokenizerDescriptor, LetterTokenizer>(tokenizerName, null);
	public TokenizersDescriptor Letter(string tokenizerName, Action<Elastic.Clients.Elasticsearch.Analysis.LetterTokenizerDescriptor> configure) => AssignVariant<Elastic.Clients.Elasticsearch.Analysis.LetterTokenizerDescriptor, LetterTokenizer>(tokenizerName, configure);
	public TokenizersDescriptor Letter(string tokenizerName, LetterTokenizer letterTokenizer) => AssignVariant(tokenizerName, letterTokenizer);
	public TokenizersDescriptor Lowercase(string tokenizerName) => AssignVariant<Elastic.Clients.Elasticsearch.Analysis.LowercaseTokenizerDescriptor, LowercaseTokenizer>(tokenizerName, null);
	public TokenizersDescriptor Lowercase(string tokenizerName, Action<Elastic.Clients.Elasticsearch.Analysis.LowercaseTokenizerDescriptor> configure) => AssignVariant<Elastic.Clients.Elasticsearch.Analysis.LowercaseTokenizerDescriptor, LowercaseTokenizer>(tokenizerName, configure);
	public TokenizersDescriptor Lowercase(string tokenizerName, LowercaseTokenizer lowercaseTokenizer) => AssignVariant(tokenizerName, lowercaseTokenizer);
	public TokenizersDescriptor NGram(string tokenizerName) => AssignVariant<Elastic.Clients.Elasticsearch.Analysis.NGramTokenizerDescriptor, NGramTokenizer>(tokenizerName, null);
	public TokenizersDescriptor NGram(string tokenizerName, Action<Elastic.Clients.Elasticsearch.Analysis.NGramTokenizerDescriptor> configure) => AssignVariant<Elastic.Clients.Elasticsearch.Analysis.NGramTokenizerDescriptor, NGramTokenizer>(tokenizerName, configure);
	public TokenizersDescriptor NGram(string tokenizerName, NGramTokenizer nGramTokenizer) => AssignVariant(tokenizerName, nGramTokenizer);
	public TokenizersDescriptor Nori(string tokenizerName) => AssignVariant<Elastic.Clients.Elasticsearch.Analysis.NoriTokenizerDescriptor, NoriTokenizer>(tokenizerName, null);
	public TokenizersDescriptor Nori(string tokenizerName, Action<Elastic.Clients.Elasticsearch.Analysis.NoriTokenizerDescriptor> configure) => AssignVariant<Elastic.Clients.Elasticsearch.Analysis.NoriTokenizerDescriptor, NoriTokenizer>(tokenizerName, configure);
	public TokenizersDescriptor Nori(string tokenizerName, NoriTokenizer noriTokenizer) => AssignVariant(tokenizerName, noriTokenizer);
	public TokenizersDescriptor PathHierarchy(string tokenizerName) => AssignVariant<Elastic.Clients.Elasticsearch.Analysis.PathHierarchyTokenizerDescriptor, PathHierarchyTokenizer>(tokenizerName, null);
	public TokenizersDescriptor PathHierarchy(string tokenizerName, Action<Elastic.Clients.Elasticsearch.Analysis.PathHierarchyTokenizerDescriptor> configure) => AssignVariant<Elastic.Clients.Elasticsearch.Analysis.PathHierarchyTokenizerDescriptor, PathHierarchyTokenizer>(tokenizerName, configure);
	public TokenizersDescriptor PathHierarchy(string tokenizerName, PathHierarchyTokenizer pathHierarchyTokenizer) => AssignVariant(tokenizerName, pathHierarchyTokenizer);
	public TokenizersDescriptor Pattern(string tokenizerName) => AssignVariant<Elastic.Clients.Elasticsearch.Analysis.PatternTokenizerDescriptor, PatternTokenizer>(tokenizerName, null);
	public TokenizersDescriptor Pattern(string tokenizerName, Action<Elastic.Clients.Elasticsearch.Analysis.PatternTokenizerDescriptor> configure) => AssignVariant<Elastic.Clients.Elasticsearch.Analysis.PatternTokenizerDescriptor, PatternTokenizer>(tokenizerName, configure);
	public TokenizersDescriptor Pattern(string tokenizerName, PatternTokenizer patternTokenizer) => AssignVariant(tokenizerName, patternTokenizer);
	public TokenizersDescriptor SimplePatternSplit(string tokenizerName) => AssignVariant<Elastic.Clients.Elasticsearch.Analysis.SimplePatternSplitTokenizerDescriptor, SimplePatternSplitTokenizer>(tokenizerName, null);
	public TokenizersDescriptor SimplePatternSplit(string tokenizerName, Action<Elastic.Clients.Elasticsearch.Analysis.SimplePatternSplitTokenizerDescriptor> configure) => AssignVariant<Elastic.Clients.Elasticsearch.Analysis.SimplePatternSplitTokenizerDescriptor, SimplePatternSplitTokenizer>(tokenizerName, configure);
	public TokenizersDescriptor SimplePatternSplit(string tokenizerName, SimplePatternSplitTokenizer simplePatternSplitTokenizer) => AssignVariant(tokenizerName, simplePatternSplitTokenizer);
	public TokenizersDescriptor SimplePattern(string tokenizerName) => AssignVariant<Elastic.Clients.Elasticsearch.Analysis.SimplePatternTokenizerDescriptor, SimplePatternTokenizer>(tokenizerName, null);
	public TokenizersDescriptor SimplePattern(string tokenizerName, Action<Elastic.Clients.Elasticsearch.Analysis.SimplePatternTokenizerDescriptor> configure) => AssignVariant<Elastic.Clients.Elasticsearch.Analysis.SimplePatternTokenizerDescriptor, SimplePatternTokenizer>(tokenizerName, configure);
	public TokenizersDescriptor SimplePattern(string tokenizerName, SimplePatternTokenizer simplePatternTokenizer) => AssignVariant(tokenizerName, simplePatternTokenizer);
	public TokenizersDescriptor Standard(string tokenizerName) => AssignVariant<Elastic.Clients.Elasticsearch.Analysis.StandardTokenizerDescriptor, StandardTokenizer>(tokenizerName, null);
	public TokenizersDescriptor Standard(string tokenizerName, Action<Elastic.Clients.Elasticsearch.Analysis.StandardTokenizerDescriptor> configure) => AssignVariant<Elastic.Clients.Elasticsearch.Analysis.StandardTokenizerDescriptor, StandardTokenizer>(tokenizerName, configure);
	public TokenizersDescriptor Standard(string tokenizerName, StandardTokenizer standardTokenizer) => AssignVariant(tokenizerName, standardTokenizer);
	public TokenizersDescriptor Thai(string tokenizerName) => AssignVariant<Elastic.Clients.Elasticsearch.Analysis.ThaiTokenizerDescriptor, ThaiTokenizer>(tokenizerName, null);
	public TokenizersDescriptor Thai(string tokenizerName, Action<Elastic.Clients.Elasticsearch.Analysis.ThaiTokenizerDescriptor> configure) => AssignVariant<Elastic.Clients.Elasticsearch.Analysis.ThaiTokenizerDescriptor, ThaiTokenizer>(tokenizerName, configure);
	public TokenizersDescriptor Thai(string tokenizerName, ThaiTokenizer thaiTokenizer) => AssignVariant(tokenizerName, thaiTokenizer);
	public TokenizersDescriptor UaxEmailUrl(string tokenizerName) => AssignVariant<Elastic.Clients.Elasticsearch.Analysis.UaxEmailUrlTokenizerDescriptor, UaxEmailUrlTokenizer>(tokenizerName, null);
	public TokenizersDescriptor UaxEmailUrl(string tokenizerName, Action<Elastic.Clients.Elasticsearch.Analysis.UaxEmailUrlTokenizerDescriptor> configure) => AssignVariant<Elastic.Clients.Elasticsearch.Analysis.UaxEmailUrlTokenizerDescriptor, UaxEmailUrlTokenizer>(tokenizerName, configure);
	public TokenizersDescriptor UaxEmailUrl(string tokenizerName, UaxEmailUrlTokenizer uaxEmailUrlTokenizer) => AssignVariant(tokenizerName, uaxEmailUrlTokenizer);
	public TokenizersDescriptor Whitespace(string tokenizerName) => AssignVariant<Elastic.Clients.Elasticsearch.Analysis.WhitespaceTokenizerDescriptor, WhitespaceTokenizer>(tokenizerName, null);
	public TokenizersDescriptor Whitespace(string tokenizerName, Action<Elastic.Clients.Elasticsearch.Analysis.WhitespaceTokenizerDescriptor> configure) => AssignVariant<Elastic.Clients.Elasticsearch.Analysis.WhitespaceTokenizerDescriptor, WhitespaceTokenizer>(tokenizerName, configure);
	public TokenizersDescriptor Whitespace(string tokenizerName, WhitespaceTokenizer whitespaceTokenizer) => AssignVariant(tokenizerName, whitespaceTokenizer);
}

internal sealed partial class TokenizerInterfaceConverter : System.Text.Json.Serialization.JsonConverter<ITokenizer>
{
	private static readonly System.Text.Json.JsonEncodedText PropDiscriminator = System.Text.Json.JsonEncodedText.Encode("type");

	public override ITokenizer Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		var readerSnapshot = reader;
		string? discriminator = null;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (reader.TryReadProperty(options, PropDiscriminator, ref discriminator))
			{
				break;
			}

			reader.Skip();
		}

		reader = readerSnapshot;
		return discriminator switch
		{
			"char_group" => reader.ReadValue<Elastic.Clients.Elasticsearch.Analysis.CharGroupTokenizer>(options),
			"classic" => reader.ReadValue<Elastic.Clients.Elasticsearch.Analysis.ClassicTokenizer>(options),
			"edge_ngram" => reader.ReadValue<Elastic.Clients.Elasticsearch.Analysis.EdgeNGramTokenizer>(options),
			"icu_tokenizer" => reader.ReadValue<Elastic.Clients.Elasticsearch.Analysis.IcuTokenizer>(options),
			"keyword" => reader.ReadValue<Elastic.Clients.Elasticsearch.Analysis.KeywordTokenizer>(options),
			"kuromoji_tokenizer" => reader.ReadValue<Elastic.Clients.Elasticsearch.Analysis.KuromojiTokenizer>(options),
			"letter" => reader.ReadValue<Elastic.Clients.Elasticsearch.Analysis.LetterTokenizer>(options),
			"lowercase" => reader.ReadValue<Elastic.Clients.Elasticsearch.Analysis.LowercaseTokenizer>(options),
			"ngram" => reader.ReadValue<Elastic.Clients.Elasticsearch.Analysis.NGramTokenizer>(options),
			"nori_tokenizer" => reader.ReadValue<Elastic.Clients.Elasticsearch.Analysis.NoriTokenizer>(options),
			"path_hierarchy" => reader.ReadValue<Elastic.Clients.Elasticsearch.Analysis.PathHierarchyTokenizer>(options),
			"pattern" => reader.ReadValue<Elastic.Clients.Elasticsearch.Analysis.PatternTokenizer>(options),
			"simple_pattern_split" => reader.ReadValue<Elastic.Clients.Elasticsearch.Analysis.SimplePatternSplitTokenizer>(options),
			"simple_pattern" => reader.ReadValue<Elastic.Clients.Elasticsearch.Analysis.SimplePatternTokenizer>(options),
			"standard" => reader.ReadValue<Elastic.Clients.Elasticsearch.Analysis.StandardTokenizer>(options),
			"thai" => reader.ReadValue<Elastic.Clients.Elasticsearch.Analysis.ThaiTokenizer>(options),
			"uax_url_email" => reader.ReadValue<Elastic.Clients.Elasticsearch.Analysis.UaxEmailUrlTokenizer>(options),
			"whitespace" => reader.ReadValue<Elastic.Clients.Elasticsearch.Analysis.WhitespaceTokenizer>(options),
			_ => throw new System.Text.Json.JsonException($"Variant '{discriminator}' is not supported for type '{nameof(ITokenizer)}'.")
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, ITokenizer value, System.Text.Json.JsonSerializerOptions options)
	{
		switch (value.Type)
		{
			case "char_group":
				writer.WriteValue(options, (Elastic.Clients.Elasticsearch.Analysis.CharGroupTokenizer)value);
				break;
			case "classic":
				writer.WriteValue(options, (Elastic.Clients.Elasticsearch.Analysis.ClassicTokenizer)value);
				break;
			case "edge_ngram":
				writer.WriteValue(options, (Elastic.Clients.Elasticsearch.Analysis.EdgeNGramTokenizer)value);
				break;
			case "icu_tokenizer":
				writer.WriteValue(options, (Elastic.Clients.Elasticsearch.Analysis.IcuTokenizer)value);
				break;
			case "keyword":
				writer.WriteValue(options, (Elastic.Clients.Elasticsearch.Analysis.KeywordTokenizer)value);
				break;
			case "kuromoji_tokenizer":
				writer.WriteValue(options, (Elastic.Clients.Elasticsearch.Analysis.KuromojiTokenizer)value);
				break;
			case "letter":
				writer.WriteValue(options, (Elastic.Clients.Elasticsearch.Analysis.LetterTokenizer)value);
				break;
			case "lowercase":
				writer.WriteValue(options, (Elastic.Clients.Elasticsearch.Analysis.LowercaseTokenizer)value);
				break;
			case "ngram":
				writer.WriteValue(options, (Elastic.Clients.Elasticsearch.Analysis.NGramTokenizer)value);
				break;
			case "nori_tokenizer":
				writer.WriteValue(options, (Elastic.Clients.Elasticsearch.Analysis.NoriTokenizer)value);
				break;
			case "path_hierarchy":
				writer.WriteValue(options, (Elastic.Clients.Elasticsearch.Analysis.PathHierarchyTokenizer)value);
				break;
			case "pattern":
				writer.WriteValue(options, (Elastic.Clients.Elasticsearch.Analysis.PatternTokenizer)value);
				break;
			case "simple_pattern_split":
				writer.WriteValue(options, (Elastic.Clients.Elasticsearch.Analysis.SimplePatternSplitTokenizer)value);
				break;
			case "simple_pattern":
				writer.WriteValue(options, (Elastic.Clients.Elasticsearch.Analysis.SimplePatternTokenizer)value);
				break;
			case "standard":
				writer.WriteValue(options, (Elastic.Clients.Elasticsearch.Analysis.StandardTokenizer)value);
				break;
			case "thai":
				writer.WriteValue(options, (Elastic.Clients.Elasticsearch.Analysis.ThaiTokenizer)value);
				break;
			case "uax_url_email":
				writer.WriteValue(options, (Elastic.Clients.Elasticsearch.Analysis.UaxEmailUrlTokenizer)value);
				break;
			case "whitespace":
				writer.WriteValue(options, (Elastic.Clients.Elasticsearch.Analysis.WhitespaceTokenizer)value);
				break;
			default:
				throw new System.Text.Json.JsonException($"Variant '{value.Type}' is not supported for type '{nameof(ITokenizer)}'.");
		}
	}
}

[JsonConverter(typeof(TokenizerInterfaceConverter))]
public partial interface ITokenizer
{
	public string? Type { get; }
}