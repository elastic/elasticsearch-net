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

namespace Elastic.Clients.Elasticsearch.Analysis;

internal sealed partial class HyphenationDecompounderTokenFilterConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Analysis.HyphenationDecompounderTokenFilter>
{
	private static readonly System.Text.Json.JsonEncodedText PropHyphenationPatternsPath = System.Text.Json.JsonEncodedText.Encode("hyphenation_patterns_path");
	private static readonly System.Text.Json.JsonEncodedText PropMaxSubwordSize = System.Text.Json.JsonEncodedText.Encode("max_subword_size");
	private static readonly System.Text.Json.JsonEncodedText PropMinSubwordSize = System.Text.Json.JsonEncodedText.Encode("min_subword_size");
	private static readonly System.Text.Json.JsonEncodedText PropMinWordSize = System.Text.Json.JsonEncodedText.Encode("min_word_size");
	private static readonly System.Text.Json.JsonEncodedText PropOnlyLongestMatch = System.Text.Json.JsonEncodedText.Encode("only_longest_match");
	private static readonly System.Text.Json.JsonEncodedText PropType = System.Text.Json.JsonEncodedText.Encode("type");
	private static readonly System.Text.Json.JsonEncodedText PropVersion = System.Text.Json.JsonEncodedText.Encode("version");
	private static readonly System.Text.Json.JsonEncodedText PropWordList = System.Text.Json.JsonEncodedText.Encode("word_list");
	private static readonly System.Text.Json.JsonEncodedText PropWordListPath = System.Text.Json.JsonEncodedText.Encode("word_list_path");

	public override Elastic.Clients.Elasticsearch.Analysis.HyphenationDecompounderTokenFilter Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<string?> propHyphenationPatternsPath = default;
		LocalJsonValue<int?> propMaxSubwordSize = default;
		LocalJsonValue<int?> propMinSubwordSize = default;
		LocalJsonValue<int?> propMinWordSize = default;
		LocalJsonValue<bool?> propOnlyLongestMatch = default;
		LocalJsonValue<string?> propVersion = default;
		LocalJsonValue<System.Collections.Generic.ICollection<string>?> propWordList = default;
		LocalJsonValue<string?> propWordListPath = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propHyphenationPatternsPath.TryReadProperty(ref reader, options, PropHyphenationPatternsPath, null))
			{
				continue;
			}

			if (propMaxSubwordSize.TryReadProperty(ref reader, options, PropMaxSubwordSize, null))
			{
				continue;
			}

			if (propMinSubwordSize.TryReadProperty(ref reader, options, PropMinSubwordSize, null))
			{
				continue;
			}

			if (propMinWordSize.TryReadProperty(ref reader, options, PropMinWordSize, null))
			{
				continue;
			}

			if (propOnlyLongestMatch.TryReadProperty(ref reader, options, PropOnlyLongestMatch, null))
			{
				continue;
			}

			if (reader.ValueTextEquals(PropType))
			{
				reader.Skip();
				continue;
			}

			if (propVersion.TryReadProperty(ref reader, options, PropVersion, null))
			{
				continue;
			}

			if (propWordList.TryReadProperty(ref reader, options, PropWordList, static System.Collections.Generic.ICollection<string>? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadCollectionValue<string>(o, null)))
			{
				continue;
			}

			if (propWordListPath.TryReadProperty(ref reader, options, PropWordListPath, null))
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
		return new Elastic.Clients.Elasticsearch.Analysis.HyphenationDecompounderTokenFilter(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			HyphenationPatternsPath = propHyphenationPatternsPath.Value,
			MaxSubwordSize = propMaxSubwordSize.Value,
			MinSubwordSize = propMinSubwordSize.Value,
			MinWordSize = propMinWordSize.Value,
			OnlyLongestMatch = propOnlyLongestMatch.Value,
			Version = propVersion.Value,
			WordList = propWordList.Value,
			WordListPath = propWordListPath.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Analysis.HyphenationDecompounderTokenFilter value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropHyphenationPatternsPath, value.HyphenationPatternsPath, null, null);
		writer.WriteProperty(options, PropMaxSubwordSize, value.MaxSubwordSize, null, null);
		writer.WriteProperty(options, PropMinSubwordSize, value.MinSubwordSize, null, null);
		writer.WriteProperty(options, PropMinWordSize, value.MinWordSize, null, null);
		writer.WriteProperty(options, PropOnlyLongestMatch, value.OnlyLongestMatch, null, null);
		writer.WriteProperty(options, PropType, value.Type, null, null);
		writer.WriteProperty(options, PropVersion, value.Version, null, null);
		writer.WriteProperty(options, PropWordList, value.WordList, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.ICollection<string>? v) => w.WriteCollectionValue<string>(o, v, null));
		writer.WriteProperty(options, PropWordListPath, value.WordListPath, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Analysis.HyphenationDecompounderTokenFilterConverter))]
public sealed partial class HyphenationDecompounderTokenFilter : Elastic.Clients.Elasticsearch.Analysis.ITokenFilter
{
#if NET7_0_OR_GREATER
	public HyphenationDecompounderTokenFilter()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	public HyphenationDecompounderTokenFilter()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal HyphenationDecompounderTokenFilter(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	public string? HyphenationPatternsPath { get; set; }
	public int? MaxSubwordSize { get; set; }
	public int? MinSubwordSize { get; set; }
	public int? MinWordSize { get; set; }
	public bool? OnlyLongestMatch { get; set; }

	public string Type => "hyphenation_decompounder";

	public string? Version { get; set; }
	public System.Collections.Generic.ICollection<string>? WordList { get; set; }
	public string? WordListPath { get; set; }
}

public readonly partial struct HyphenationDecompounderTokenFilterDescriptor
{
	internal Elastic.Clients.Elasticsearch.Analysis.HyphenationDecompounderTokenFilter Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public HyphenationDecompounderTokenFilterDescriptor(Elastic.Clients.Elasticsearch.Analysis.HyphenationDecompounderTokenFilter instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public HyphenationDecompounderTokenFilterDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.Analysis.HyphenationDecompounderTokenFilter(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Analysis.HyphenationDecompounderTokenFilterDescriptor(Elastic.Clients.Elasticsearch.Analysis.HyphenationDecompounderTokenFilter instance) => new Elastic.Clients.Elasticsearch.Analysis.HyphenationDecompounderTokenFilterDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Analysis.HyphenationDecompounderTokenFilter(Elastic.Clients.Elasticsearch.Analysis.HyphenationDecompounderTokenFilterDescriptor descriptor) => descriptor.Instance;

	public Elastic.Clients.Elasticsearch.Analysis.HyphenationDecompounderTokenFilterDescriptor HyphenationPatternsPath(string? value)
	{
		Instance.HyphenationPatternsPath = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Analysis.HyphenationDecompounderTokenFilterDescriptor MaxSubwordSize(int? value)
	{
		Instance.MaxSubwordSize = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Analysis.HyphenationDecompounderTokenFilterDescriptor MinSubwordSize(int? value)
	{
		Instance.MinSubwordSize = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Analysis.HyphenationDecompounderTokenFilterDescriptor MinWordSize(int? value)
	{
		Instance.MinWordSize = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Analysis.HyphenationDecompounderTokenFilterDescriptor OnlyLongestMatch(bool? value = true)
	{
		Instance.OnlyLongestMatch = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Analysis.HyphenationDecompounderTokenFilterDescriptor Version(string? value)
	{
		Instance.Version = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Analysis.HyphenationDecompounderTokenFilterDescriptor WordList(System.Collections.Generic.ICollection<string>? value)
	{
		Instance.WordList = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Analysis.HyphenationDecompounderTokenFilterDescriptor WordList(params string[] values)
	{
		Instance.WordList = [.. values];
		return this;
	}

	public Elastic.Clients.Elasticsearch.Analysis.HyphenationDecompounderTokenFilterDescriptor WordListPath(string? value)
	{
		Instance.WordListPath = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Analysis.HyphenationDecompounderTokenFilter Build(System.Action<Elastic.Clients.Elasticsearch.Analysis.HyphenationDecompounderTokenFilterDescriptor>? action)
	{
		if (action is null)
		{
			return new Elastic.Clients.Elasticsearch.Analysis.HyphenationDecompounderTokenFilter(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
		}

		var builder = new Elastic.Clients.Elasticsearch.Analysis.HyphenationDecompounderTokenFilterDescriptor(new Elastic.Clients.Elasticsearch.Analysis.HyphenationDecompounderTokenFilter(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}