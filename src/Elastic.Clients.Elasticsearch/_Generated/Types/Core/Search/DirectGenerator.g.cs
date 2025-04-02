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

namespace Elastic.Clients.Elasticsearch.Core.Search;

internal sealed partial class DirectGeneratorConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Core.Search.DirectGenerator>
{
	private static readonly System.Text.Json.JsonEncodedText PropField = System.Text.Json.JsonEncodedText.Encode("field");
	private static readonly System.Text.Json.JsonEncodedText PropMaxEdits = System.Text.Json.JsonEncodedText.Encode("max_edits");
	private static readonly System.Text.Json.JsonEncodedText PropMaxInspections = System.Text.Json.JsonEncodedText.Encode("max_inspections");
	private static readonly System.Text.Json.JsonEncodedText PropMaxTermFreq = System.Text.Json.JsonEncodedText.Encode("max_term_freq");
	private static readonly System.Text.Json.JsonEncodedText PropMinDocFreq = System.Text.Json.JsonEncodedText.Encode("min_doc_freq");
	private static readonly System.Text.Json.JsonEncodedText PropMinWordLength = System.Text.Json.JsonEncodedText.Encode("min_word_length");
	private static readonly System.Text.Json.JsonEncodedText PropPostFilter = System.Text.Json.JsonEncodedText.Encode("post_filter");
	private static readonly System.Text.Json.JsonEncodedText PropPreFilter = System.Text.Json.JsonEncodedText.Encode("pre_filter");
	private static readonly System.Text.Json.JsonEncodedText PropPrefixLength = System.Text.Json.JsonEncodedText.Encode("prefix_length");
	private static readonly System.Text.Json.JsonEncodedText PropSize = System.Text.Json.JsonEncodedText.Encode("size");
	private static readonly System.Text.Json.JsonEncodedText PropSuggestMode = System.Text.Json.JsonEncodedText.Encode("suggest_mode");

	public override Elastic.Clients.Elasticsearch.Core.Search.DirectGenerator Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<Elastic.Clients.Elasticsearch.Field> propField = default;
		LocalJsonValue<int?> propMaxEdits = default;
		LocalJsonValue<float?> propMaxInspections = default;
		LocalJsonValue<float?> propMaxTermFreq = default;
		LocalJsonValue<float?> propMinDocFreq = default;
		LocalJsonValue<int?> propMinWordLength = default;
		LocalJsonValue<string?> propPostFilter = default;
		LocalJsonValue<string?> propPreFilter = default;
		LocalJsonValue<int?> propPrefixLength = default;
		LocalJsonValue<int?> propSize = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.SuggestMode?> propSuggestMode = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propField.TryReadProperty(ref reader, options, PropField, null))
			{
				continue;
			}

			if (propMaxEdits.TryReadProperty(ref reader, options, PropMaxEdits, null))
			{
				continue;
			}

			if (propMaxInspections.TryReadProperty(ref reader, options, PropMaxInspections, null))
			{
				continue;
			}

			if (propMaxTermFreq.TryReadProperty(ref reader, options, PropMaxTermFreq, null))
			{
				continue;
			}

			if (propMinDocFreq.TryReadProperty(ref reader, options, PropMinDocFreq, null))
			{
				continue;
			}

			if (propMinWordLength.TryReadProperty(ref reader, options, PropMinWordLength, null))
			{
				continue;
			}

			if (propPostFilter.TryReadProperty(ref reader, options, PropPostFilter, null))
			{
				continue;
			}

			if (propPreFilter.TryReadProperty(ref reader, options, PropPreFilter, null))
			{
				continue;
			}

			if (propPrefixLength.TryReadProperty(ref reader, options, PropPrefixLength, null))
			{
				continue;
			}

			if (propSize.TryReadProperty(ref reader, options, PropSize, null))
			{
				continue;
			}

			if (propSuggestMode.TryReadProperty(ref reader, options, PropSuggestMode, null))
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
		return new Elastic.Clients.Elasticsearch.Core.Search.DirectGenerator(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Field = propField.Value,
			MaxEdits = propMaxEdits.Value,
			MaxInspections = propMaxInspections.Value,
			MaxTermFreq = propMaxTermFreq.Value,
			MinDocFreq = propMinDocFreq.Value,
			MinWordLength = propMinWordLength.Value,
			PostFilter = propPostFilter.Value,
			PreFilter = propPreFilter.Value,
			PrefixLength = propPrefixLength.Value,
			Size = propSize.Value,
			SuggestMode = propSuggestMode.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Core.Search.DirectGenerator value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropField, value.Field, null, null);
		writer.WriteProperty(options, PropMaxEdits, value.MaxEdits, null, null);
		writer.WriteProperty(options, PropMaxInspections, value.MaxInspections, null, null);
		writer.WriteProperty(options, PropMaxTermFreq, value.MaxTermFreq, null, null);
		writer.WriteProperty(options, PropMinDocFreq, value.MinDocFreq, null, null);
		writer.WriteProperty(options, PropMinWordLength, value.MinWordLength, null, null);
		writer.WriteProperty(options, PropPostFilter, value.PostFilter, null, null);
		writer.WriteProperty(options, PropPreFilter, value.PreFilter, null, null);
		writer.WriteProperty(options, PropPrefixLength, value.PrefixLength, null, null);
		writer.WriteProperty(options, PropSize, value.Size, null, null);
		writer.WriteProperty(options, PropSuggestMode, value.SuggestMode, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Core.Search.DirectGeneratorConverter))]
public sealed partial class DirectGenerator
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public DirectGenerator(Elastic.Clients.Elasticsearch.Field field)
	{
		Field = field;
	}
#if NET7_0_OR_GREATER
	public DirectGenerator()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains additional required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public DirectGenerator()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal DirectGenerator(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	/// <summary>
	/// <para>
	/// The field to fetch the candidate suggestions from.
	/// Needs to be set globally or per suggestion.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	Elastic.Clients.Elasticsearch.Field Field { get; set; }

	/// <summary>
	/// <para>
	/// The maximum edit distance candidate suggestions can have in order to be considered as a suggestion.
	/// Can only be <c>1</c> or <c>2</c>.
	/// </para>
	/// </summary>
	public int? MaxEdits { get; set; }

	/// <summary>
	/// <para>
	/// A factor that is used to multiply with the shard_size in order to inspect more candidate spelling corrections on the shard level.
	/// Can improve accuracy at the cost of performance.
	/// </para>
	/// </summary>
	public float? MaxInspections { get; set; }

	/// <summary>
	/// <para>
	/// The maximum threshold in number of documents in which a suggest text token can exist in order to be included.
	/// This can be used to exclude high frequency terms — which are usually spelled correctly — from being spellchecked.
	/// Can be a relative percentage number (for example <c>0.4</c>) or an absolute number to represent document frequencies.
	/// If a value higher than 1 is specified, then fractional can not be specified.
	/// </para>
	/// </summary>
	public float? MaxTermFreq { get; set; }

	/// <summary>
	/// <para>
	/// The minimal threshold in number of documents a suggestion should appear in.
	/// This can improve quality by only suggesting high frequency terms.
	/// Can be specified as an absolute number or as a relative percentage of number of documents.
	/// If a value higher than 1 is specified, the number cannot be fractional.
	/// </para>
	/// </summary>
	public float? MinDocFreq { get; set; }

	/// <summary>
	/// <para>
	/// The minimum length a suggest text term must have in order to be included.
	/// </para>
	/// </summary>
	public int? MinWordLength { get; set; }

	/// <summary>
	/// <para>
	/// A filter (analyzer) that is applied to each of the generated tokens before they are passed to the actual phrase scorer.
	/// </para>
	/// </summary>
	public string? PostFilter { get; set; }

	/// <summary>
	/// <para>
	/// A filter (analyzer) that is applied to each of the tokens passed to this candidate generator.
	/// This filter is applied to the original token before candidates are generated.
	/// </para>
	/// </summary>
	public string? PreFilter { get; set; }

	/// <summary>
	/// <para>
	/// The number of minimal prefix characters that must match in order be a candidate suggestions.
	/// Increasing this number improves spellcheck performance.
	/// </para>
	/// </summary>
	public int? PrefixLength { get; set; }

	/// <summary>
	/// <para>
	/// The maximum corrections to be returned per suggest text token.
	/// </para>
	/// </summary>
	public int? Size { get; set; }

	/// <summary>
	/// <para>
	/// Controls what suggestions are included on the suggestions generated on each shard.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.SuggestMode? SuggestMode { get; set; }
}

public readonly partial struct DirectGeneratorDescriptor<TDocument>
{
	internal Elastic.Clients.Elasticsearch.Core.Search.DirectGenerator Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public DirectGeneratorDescriptor(Elastic.Clients.Elasticsearch.Core.Search.DirectGenerator instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public DirectGeneratorDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.Core.Search.DirectGenerator(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Core.Search.DirectGeneratorDescriptor<TDocument>(Elastic.Clients.Elasticsearch.Core.Search.DirectGenerator instance) => new Elastic.Clients.Elasticsearch.Core.Search.DirectGeneratorDescriptor<TDocument>(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Core.Search.DirectGenerator(Elastic.Clients.Elasticsearch.Core.Search.DirectGeneratorDescriptor<TDocument> descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// The field to fetch the candidate suggestions from.
	/// Needs to be set globally or per suggestion.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Search.DirectGeneratorDescriptor<TDocument> Field(Elastic.Clients.Elasticsearch.Field value)
	{
		Instance.Field = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The field to fetch the candidate suggestions from.
	/// Needs to be set globally or per suggestion.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Search.DirectGeneratorDescriptor<TDocument> Field(System.Linq.Expressions.Expression<System.Func<TDocument, object?>> value)
	{
		Instance.Field = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The maximum edit distance candidate suggestions can have in order to be considered as a suggestion.
	/// Can only be <c>1</c> or <c>2</c>.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Search.DirectGeneratorDescriptor<TDocument> MaxEdits(int? value)
	{
		Instance.MaxEdits = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// A factor that is used to multiply with the shard_size in order to inspect more candidate spelling corrections on the shard level.
	/// Can improve accuracy at the cost of performance.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Search.DirectGeneratorDescriptor<TDocument> MaxInspections(float? value)
	{
		Instance.MaxInspections = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The maximum threshold in number of documents in which a suggest text token can exist in order to be included.
	/// This can be used to exclude high frequency terms — which are usually spelled correctly — from being spellchecked.
	/// Can be a relative percentage number (for example <c>0.4</c>) or an absolute number to represent document frequencies.
	/// If a value higher than 1 is specified, then fractional can not be specified.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Search.DirectGeneratorDescriptor<TDocument> MaxTermFreq(float? value)
	{
		Instance.MaxTermFreq = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The minimal threshold in number of documents a suggestion should appear in.
	/// This can improve quality by only suggesting high frequency terms.
	/// Can be specified as an absolute number or as a relative percentage of number of documents.
	/// If a value higher than 1 is specified, the number cannot be fractional.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Search.DirectGeneratorDescriptor<TDocument> MinDocFreq(float? value)
	{
		Instance.MinDocFreq = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The minimum length a suggest text term must have in order to be included.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Search.DirectGeneratorDescriptor<TDocument> MinWordLength(int? value)
	{
		Instance.MinWordLength = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// A filter (analyzer) that is applied to each of the generated tokens before they are passed to the actual phrase scorer.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Search.DirectGeneratorDescriptor<TDocument> PostFilter(string? value)
	{
		Instance.PostFilter = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// A filter (analyzer) that is applied to each of the tokens passed to this candidate generator.
	/// This filter is applied to the original token before candidates are generated.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Search.DirectGeneratorDescriptor<TDocument> PreFilter(string? value)
	{
		Instance.PreFilter = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The number of minimal prefix characters that must match in order be a candidate suggestions.
	/// Increasing this number improves spellcheck performance.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Search.DirectGeneratorDescriptor<TDocument> PrefixLength(int? value)
	{
		Instance.PrefixLength = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The maximum corrections to be returned per suggest text token.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Search.DirectGeneratorDescriptor<TDocument> Size(int? value)
	{
		Instance.Size = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Controls what suggestions are included on the suggestions generated on each shard.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Search.DirectGeneratorDescriptor<TDocument> SuggestMode(Elastic.Clients.Elasticsearch.SuggestMode? value)
	{
		Instance.SuggestMode = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Core.Search.DirectGenerator Build(System.Action<Elastic.Clients.Elasticsearch.Core.Search.DirectGeneratorDescriptor<TDocument>> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.Core.Search.DirectGeneratorDescriptor<TDocument>(new Elastic.Clients.Elasticsearch.Core.Search.DirectGenerator(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}

public readonly partial struct DirectGeneratorDescriptor
{
	internal Elastic.Clients.Elasticsearch.Core.Search.DirectGenerator Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public DirectGeneratorDescriptor(Elastic.Clients.Elasticsearch.Core.Search.DirectGenerator instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public DirectGeneratorDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.Core.Search.DirectGenerator(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Core.Search.DirectGeneratorDescriptor(Elastic.Clients.Elasticsearch.Core.Search.DirectGenerator instance) => new Elastic.Clients.Elasticsearch.Core.Search.DirectGeneratorDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Core.Search.DirectGenerator(Elastic.Clients.Elasticsearch.Core.Search.DirectGeneratorDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// The field to fetch the candidate suggestions from.
	/// Needs to be set globally or per suggestion.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Search.DirectGeneratorDescriptor Field(Elastic.Clients.Elasticsearch.Field value)
	{
		Instance.Field = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The field to fetch the candidate suggestions from.
	/// Needs to be set globally or per suggestion.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Search.DirectGeneratorDescriptor Field<T>(System.Linq.Expressions.Expression<System.Func<T, object?>> value)
	{
		Instance.Field = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The maximum edit distance candidate suggestions can have in order to be considered as a suggestion.
	/// Can only be <c>1</c> or <c>2</c>.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Search.DirectGeneratorDescriptor MaxEdits(int? value)
	{
		Instance.MaxEdits = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// A factor that is used to multiply with the shard_size in order to inspect more candidate spelling corrections on the shard level.
	/// Can improve accuracy at the cost of performance.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Search.DirectGeneratorDescriptor MaxInspections(float? value)
	{
		Instance.MaxInspections = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The maximum threshold in number of documents in which a suggest text token can exist in order to be included.
	/// This can be used to exclude high frequency terms — which are usually spelled correctly — from being spellchecked.
	/// Can be a relative percentage number (for example <c>0.4</c>) or an absolute number to represent document frequencies.
	/// If a value higher than 1 is specified, then fractional can not be specified.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Search.DirectGeneratorDescriptor MaxTermFreq(float? value)
	{
		Instance.MaxTermFreq = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The minimal threshold in number of documents a suggestion should appear in.
	/// This can improve quality by only suggesting high frequency terms.
	/// Can be specified as an absolute number or as a relative percentage of number of documents.
	/// If a value higher than 1 is specified, the number cannot be fractional.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Search.DirectGeneratorDescriptor MinDocFreq(float? value)
	{
		Instance.MinDocFreq = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The minimum length a suggest text term must have in order to be included.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Search.DirectGeneratorDescriptor MinWordLength(int? value)
	{
		Instance.MinWordLength = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// A filter (analyzer) that is applied to each of the generated tokens before they are passed to the actual phrase scorer.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Search.DirectGeneratorDescriptor PostFilter(string? value)
	{
		Instance.PostFilter = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// A filter (analyzer) that is applied to each of the tokens passed to this candidate generator.
	/// This filter is applied to the original token before candidates are generated.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Search.DirectGeneratorDescriptor PreFilter(string? value)
	{
		Instance.PreFilter = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The number of minimal prefix characters that must match in order be a candidate suggestions.
	/// Increasing this number improves spellcheck performance.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Search.DirectGeneratorDescriptor PrefixLength(int? value)
	{
		Instance.PrefixLength = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The maximum corrections to be returned per suggest text token.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Search.DirectGeneratorDescriptor Size(int? value)
	{
		Instance.Size = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Controls what suggestions are included on the suggestions generated on each shard.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Search.DirectGeneratorDescriptor SuggestMode(Elastic.Clients.Elasticsearch.SuggestMode? value)
	{
		Instance.SuggestMode = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Core.Search.DirectGenerator Build(System.Action<Elastic.Clients.Elasticsearch.Core.Search.DirectGeneratorDescriptor> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.Core.Search.DirectGeneratorDescriptor(new Elastic.Clients.Elasticsearch.Core.Search.DirectGenerator(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}