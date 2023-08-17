// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

using Elastic.Clients.Elasticsearch.Serialization;

namespace Elastic.Clients.Elasticsearch.Core.Search;

[GenericConverter(typeof(SuggestDictionaryConverter<>), unwrap:true)]
public sealed partial class SuggestDictionary<TDocument> :
	IsAReadOnlyDictionary<string, IReadOnlyCollection<ISuggest>>
{
	public SuggestDictionary(IReadOnlyDictionary<string, IReadOnlyCollection<ISuggest>> backingDictionary) :
		base(backingDictionary)
	{
	}

	public IReadOnlyCollection<TermSuggest>? GetTerm(string key) => TryGet<TermSuggest>(key);

	public IReadOnlyCollection<PhraseSuggest>? GetPhrase(string key) => TryGet<PhraseSuggest>(key);

	public IReadOnlyCollection<CompletionSuggest<TDocument>>? GetCompletion(string key) => TryGet<CompletionSuggest<TDocument>>(key);

	private IReadOnlyCollection<TSuggest>? TryGet<TSuggest>(string key) where TSuggest : class, ISuggest =>
		BackingDictionary.TryGetValue(key, out var items) ? items.Cast<TSuggest>().ToArray() : null;
}

internal sealed class SuggestDictionaryConverter<TDocument> :
	JsonConverter<SuggestDictionary<TDocument>>
{
	public override SuggestDictionary<TDocument>? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		var dictionary = new Dictionary<string, IReadOnlyCollection<ISuggest>>();

		if (reader.TokenType != JsonTokenType.StartObject)
			return new SuggestDictionary<TDocument>(dictionary);

		while (reader.Read())
		{
			if (reader.TokenType == JsonTokenType.EndObject)
				break;

			// TODO: Future optimization, get raw bytes span and parse based on those
			var name = reader.GetString() ?? throw new JsonException("Key must not be 'null'.");

			reader.Read();
			ReadVariant(ref reader, options, dictionary, name);
		}

		return new SuggestDictionary<TDocument>(dictionary);
	}

	public static void ReadVariant(ref Utf8JsonReader reader, JsonSerializerOptions options, Dictionary<string, IReadOnlyCollection<ISuggest>> dictionary, string name)
	{
		var nameParts = name.Split('#');

		if (nameParts.Length != 2)
			throw new JsonException($"Unable to parse typed-key from suggestion name '{name}'");

		var variantName = nameParts[0];
		switch (variantName)
		{
			case "term":
			{
				var suggest = JsonSerializer.Deserialize<TermSuggest[]>(ref reader, options);
				dictionary.Add(nameParts[1], suggest);
				break;
			}

			case "phrase":
			{
				var suggest = JsonSerializer.Deserialize<PhraseSuggest[]>(ref reader, options);
				dictionary.Add(nameParts[1], suggest);
				break;
			}

			case "completion":
			{
				var suggest = JsonSerializer.Deserialize<CompletionSuggest<TDocument>[]>(ref reader, options);
				dictionary.Add(nameParts[1], suggest);
				break;
			}

			default:
				throw new Exception($"The suggest variant '{variantName}' in this response is currently not supported.");
		}
	}

	public override void Write(Utf8JsonWriter writer, SuggestDictionary<TDocument> value, JsonSerializerOptions options) => throw new NotImplementedException();
}

public interface ISuggest
{
}

public sealed partial class TermSuggest :
	ISuggest
{
	[JsonInclude, JsonPropertyName("length")]
	public int Length { get; init; }

	[JsonInclude, JsonPropertyName("offset")]
	public int Offset { get; init; }

	[JsonInclude, JsonPropertyName("options"), SingleOrManyCollectionConverter(typeof(TermSuggestOption))]
	public IReadOnlyCollection<TermSuggestOption> Options { get; init; }

	[JsonInclude, JsonPropertyName("text")]
	public string Text { get; init; }
}

public sealed partial class TermSuggestOption
{
	[JsonInclude, JsonPropertyName("collate_match")]
	public bool? CollateMatch { get; init; }

	[JsonInclude, JsonPropertyName("freq")]
	public long Freq { get; init; }

	[JsonInclude, JsonPropertyName("highlighted")]
	public string? Highlighted { get; init; }

	[JsonInclude, JsonPropertyName("score")]
	public double Score { get; init; }

	[JsonInclude, JsonPropertyName("text")]
	public string Text { get; init; }
}

public sealed partial class PhraseSuggest :
	ISuggest
{
	[JsonInclude, JsonPropertyName("length")]
	public int Length { get; init; }

	[JsonInclude, JsonPropertyName("offset")]
	public int Offset { get; init; }

	[JsonInclude, JsonPropertyName("options"), SingleOrManyCollectionConverter(typeof(PhraseSuggestOption))]
	public IReadOnlyCollection<PhraseSuggestOption> Options { get; init; }

	[JsonInclude, JsonPropertyName("text")]
	public string Text { get; init; }
}

public sealed partial class PhraseSuggestOption
{
	[JsonInclude, JsonPropertyName("collate_match")]
	public bool? CollateMatch { get; init; }

	[JsonInclude, JsonPropertyName("highlighted")]
	public string? Highlighted { get; init; }

	[JsonInclude, JsonPropertyName("score")]
	public double Score { get; init; }

	[JsonInclude, JsonPropertyName("text")]
	public string Text { get; init; }
}

public sealed partial class CompletionSuggest<TDocument> :
	ISuggest
{
	[JsonInclude, JsonPropertyName("length")]
	public int Length { get; init; }

	[JsonInclude, JsonPropertyName("offset")]
	public int Offset { get; init; }

	[JsonInclude, JsonPropertyName("options"), GenericConverter(typeof(SingleOrManyCollectionConverter<>), unwrap:true)]
	public IReadOnlyCollection<CompletionSuggestOption<TDocument>> Options { get; init; }

	[JsonInclude, JsonPropertyName("text")]
	public string Text { get; init; }
}

public sealed partial class CompletionSuggestOption<TDocument>
{
	[JsonInclude, JsonPropertyName("_id")]
	public string? Id { get; init; }

	[JsonInclude, JsonPropertyName("_index")]
	public string? Index { get; init; }

	[JsonInclude, JsonPropertyName("_routing")]
	public string? Routing { get; init; }

	[JsonInclude, JsonPropertyName("_score")]
	public double? Score0 { get; init; }

	[JsonInclude, JsonPropertyName("_source")]
	[SourceConverter]
	public TDocument? Source { get; init; }

	[JsonInclude, JsonPropertyName("collate_match")]
	public bool? CollateMatch { get; init; }

	[JsonInclude, JsonPropertyName("contexts")]
	public IReadOnlyDictionary<string, IReadOnlyCollection<Context>>? Contexts { get; init; }

	[JsonInclude, JsonPropertyName("fields")]
	public IReadOnlyDictionary<string, object>? Fields { get; init; }

	[JsonInclude, JsonPropertyName("score")]
	public double? Score { get; init; }

	[JsonInclude, JsonPropertyName("text")]
	public string Text { get; init; }
}
