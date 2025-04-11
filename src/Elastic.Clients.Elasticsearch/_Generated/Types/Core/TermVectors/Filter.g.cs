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

internal sealed partial class FilterConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Core.TermVectors.Filter>
{
	private static readonly System.Text.Json.JsonEncodedText PropMaxDocFreq = System.Text.Json.JsonEncodedText.Encode("max_doc_freq");
	private static readonly System.Text.Json.JsonEncodedText PropMaxNumTerms = System.Text.Json.JsonEncodedText.Encode("max_num_terms");
	private static readonly System.Text.Json.JsonEncodedText PropMaxTermFreq = System.Text.Json.JsonEncodedText.Encode("max_term_freq");
	private static readonly System.Text.Json.JsonEncodedText PropMaxWordLength = System.Text.Json.JsonEncodedText.Encode("max_word_length");
	private static readonly System.Text.Json.JsonEncodedText PropMinDocFreq = System.Text.Json.JsonEncodedText.Encode("min_doc_freq");
	private static readonly System.Text.Json.JsonEncodedText PropMinTermFreq = System.Text.Json.JsonEncodedText.Encode("min_term_freq");
	private static readonly System.Text.Json.JsonEncodedText PropMinWordLength = System.Text.Json.JsonEncodedText.Encode("min_word_length");

	public override Elastic.Clients.Elasticsearch.Core.TermVectors.Filter Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<int?> propMaxDocFreq = default;
		LocalJsonValue<int?> propMaxNumTerms = default;
		LocalJsonValue<int?> propMaxTermFreq = default;
		LocalJsonValue<int?> propMaxWordLength = default;
		LocalJsonValue<int?> propMinDocFreq = default;
		LocalJsonValue<int?> propMinTermFreq = default;
		LocalJsonValue<int?> propMinWordLength = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propMaxDocFreq.TryReadProperty(ref reader, options, PropMaxDocFreq, null))
			{
				continue;
			}

			if (propMaxNumTerms.TryReadProperty(ref reader, options, PropMaxNumTerms, null))
			{
				continue;
			}

			if (propMaxTermFreq.TryReadProperty(ref reader, options, PropMaxTermFreq, null))
			{
				continue;
			}

			if (propMaxWordLength.TryReadProperty(ref reader, options, PropMaxWordLength, null))
			{
				continue;
			}

			if (propMinDocFreq.TryReadProperty(ref reader, options, PropMinDocFreq, null))
			{
				continue;
			}

			if (propMinTermFreq.TryReadProperty(ref reader, options, PropMinTermFreq, null))
			{
				continue;
			}

			if (propMinWordLength.TryReadProperty(ref reader, options, PropMinWordLength, null))
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
		return new Elastic.Clients.Elasticsearch.Core.TermVectors.Filter(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			MaxDocFreq = propMaxDocFreq.Value,
			MaxNumTerms = propMaxNumTerms.Value,
			MaxTermFreq = propMaxTermFreq.Value,
			MaxWordLength = propMaxWordLength.Value,
			MinDocFreq = propMinDocFreq.Value,
			MinTermFreq = propMinTermFreq.Value,
			MinWordLength = propMinWordLength.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Core.TermVectors.Filter value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropMaxDocFreq, value.MaxDocFreq, null, null);
		writer.WriteProperty(options, PropMaxNumTerms, value.MaxNumTerms, null, null);
		writer.WriteProperty(options, PropMaxTermFreq, value.MaxTermFreq, null, null);
		writer.WriteProperty(options, PropMaxWordLength, value.MaxWordLength, null, null);
		writer.WriteProperty(options, PropMinDocFreq, value.MinDocFreq, null, null);
		writer.WriteProperty(options, PropMinTermFreq, value.MinTermFreq, null, null);
		writer.WriteProperty(options, PropMinWordLength, value.MinWordLength, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Core.TermVectors.FilterConverter))]
public sealed partial class Filter
{
#if NET7_0_OR_GREATER
	public Filter()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	public Filter()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal Filter(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	/// <summary>
	/// <para>
	/// Ignore words which occur in more than this many docs.
	/// Defaults to unbounded.
	/// </para>
	/// </summary>
	public int? MaxDocFreq { get; set; }

	/// <summary>
	/// <para>
	/// The maximum number of terms that must be returned per field.
	/// </para>
	/// </summary>
	public int? MaxNumTerms { get; set; }

	/// <summary>
	/// <para>
	/// Ignore words with more than this frequency in the source doc.
	/// It defaults to unbounded.
	/// </para>
	/// </summary>
	public int? MaxTermFreq { get; set; }

	/// <summary>
	/// <para>
	/// The maximum word length above which words will be ignored.
	/// Defaults to unbounded.
	/// </para>
	/// </summary>
	public int? MaxWordLength { get; set; }

	/// <summary>
	/// <para>
	/// Ignore terms which do not occur in at least this many docs.
	/// </para>
	/// </summary>
	public int? MinDocFreq { get; set; }

	/// <summary>
	/// <para>
	/// Ignore words with less than this frequency in the source doc.
	/// </para>
	/// </summary>
	public int? MinTermFreq { get; set; }

	/// <summary>
	/// <para>
	/// The minimum word length below which words will be ignored.
	/// </para>
	/// </summary>
	public int? MinWordLength { get; set; }
}

public readonly partial struct FilterDescriptor
{
	internal Elastic.Clients.Elasticsearch.Core.TermVectors.Filter Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public FilterDescriptor(Elastic.Clients.Elasticsearch.Core.TermVectors.Filter instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public FilterDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.Core.TermVectors.Filter(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Core.TermVectors.FilterDescriptor(Elastic.Clients.Elasticsearch.Core.TermVectors.Filter instance) => new Elastic.Clients.Elasticsearch.Core.TermVectors.FilterDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Core.TermVectors.Filter(Elastic.Clients.Elasticsearch.Core.TermVectors.FilterDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// Ignore words which occur in more than this many docs.
	/// Defaults to unbounded.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.TermVectors.FilterDescriptor MaxDocFreq(int? value)
	{
		Instance.MaxDocFreq = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The maximum number of terms that must be returned per field.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.TermVectors.FilterDescriptor MaxNumTerms(int? value)
	{
		Instance.MaxNumTerms = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Ignore words with more than this frequency in the source doc.
	/// It defaults to unbounded.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.TermVectors.FilterDescriptor MaxTermFreq(int? value)
	{
		Instance.MaxTermFreq = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The maximum word length above which words will be ignored.
	/// Defaults to unbounded.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.TermVectors.FilterDescriptor MaxWordLength(int? value)
	{
		Instance.MaxWordLength = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Ignore terms which do not occur in at least this many docs.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.TermVectors.FilterDescriptor MinDocFreq(int? value)
	{
		Instance.MinDocFreq = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Ignore words with less than this frequency in the source doc.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.TermVectors.FilterDescriptor MinTermFreq(int? value)
	{
		Instance.MinTermFreq = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The minimum word length below which words will be ignored.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.TermVectors.FilterDescriptor MinWordLength(int? value)
	{
		Instance.MinWordLength = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Core.TermVectors.Filter Build(System.Action<Elastic.Clients.Elasticsearch.Core.TermVectors.FilterDescriptor>? action)
	{
		if (action is null)
		{
			return new Elastic.Clients.Elasticsearch.Core.TermVectors.Filter(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
		}

		var builder = new Elastic.Clients.Elasticsearch.Core.TermVectors.FilterDescriptor(new Elastic.Clients.Elasticsearch.Core.TermVectors.Filter(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}