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

using Elastic.Clients.Elasticsearch.Fluent;
using Elastic.Clients.Elasticsearch.Serialization;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch.Core.Search;

public sealed partial class SuggestFuzziness
{
	/// <summary>
	/// <para>
	/// The fuzziness factor.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("fuzziness")]
	public Elastic.Clients.Elasticsearch.Fuzziness? Fuzziness { get; set; }

	/// <summary>
	/// <para>
	/// Minimum length of the input before fuzzy suggestions are returned.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("min_length")]
	public int? MinLength { get; set; }

	/// <summary>
	/// <para>
	/// Minimum length of the input, which is not checked for fuzzy alternatives.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("prefix_length")]
	public int? PrefixLength { get; set; }

	/// <summary>
	/// <para>
	/// If set to <c>true</c>, transpositions are counted as one change instead of two.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("transpositions")]
	public bool? Transpositions { get; set; }

	/// <summary>
	/// <para>
	/// If <c>true</c>, all measurements (like fuzzy edit distance, transpositions, and lengths) are measured in Unicode code points instead of in bytes.
	/// This is slightly slower than raw bytes.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("unicode_aware")]
	public bool? UnicodeAware { get; set; }
}

public sealed partial class SuggestFuzzinessDescriptor : SerializableDescriptor<SuggestFuzzinessDescriptor>
{
	internal SuggestFuzzinessDescriptor(Action<SuggestFuzzinessDescriptor> configure) => configure.Invoke(this);

	public SuggestFuzzinessDescriptor() : base()
	{
	}

	private Elastic.Clients.Elasticsearch.Fuzziness? FuzzinessValue { get; set; }
	private int? MinLengthValue { get; set; }
	private int? PrefixLengthValue { get; set; }
	private bool? TranspositionsValue { get; set; }
	private bool? UnicodeAwareValue { get; set; }

	/// <summary>
	/// <para>
	/// The fuzziness factor.
	/// </para>
	/// </summary>
	public SuggestFuzzinessDescriptor Fuzziness(Elastic.Clients.Elasticsearch.Fuzziness? fuzziness)
	{
		FuzzinessValue = fuzziness;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Minimum length of the input before fuzzy suggestions are returned.
	/// </para>
	/// </summary>
	public SuggestFuzzinessDescriptor MinLength(int? minLength)
	{
		MinLengthValue = minLength;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Minimum length of the input, which is not checked for fuzzy alternatives.
	/// </para>
	/// </summary>
	public SuggestFuzzinessDescriptor PrefixLength(int? prefixLength)
	{
		PrefixLengthValue = prefixLength;
		return Self;
	}

	/// <summary>
	/// <para>
	/// If set to <c>true</c>, transpositions are counted as one change instead of two.
	/// </para>
	/// </summary>
	public SuggestFuzzinessDescriptor Transpositions(bool? transpositions = true)
	{
		TranspositionsValue = transpositions;
		return Self;
	}

	/// <summary>
	/// <para>
	/// If <c>true</c>, all measurements (like fuzzy edit distance, transpositions, and lengths) are measured in Unicode code points instead of in bytes.
	/// This is slightly slower than raw bytes.
	/// </para>
	/// </summary>
	public SuggestFuzzinessDescriptor UnicodeAware(bool? unicodeAware = true)
	{
		UnicodeAwareValue = unicodeAware;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (FuzzinessValue is not null)
		{
			writer.WritePropertyName("fuzziness");
			JsonSerializer.Serialize(writer, FuzzinessValue, options);
		}

		if (MinLengthValue.HasValue)
		{
			writer.WritePropertyName("min_length");
			writer.WriteNumberValue(MinLengthValue.Value);
		}

		if (PrefixLengthValue.HasValue)
		{
			writer.WritePropertyName("prefix_length");
			writer.WriteNumberValue(PrefixLengthValue.Value);
		}

		if (TranspositionsValue.HasValue)
		{
			writer.WritePropertyName("transpositions");
			writer.WriteBooleanValue(TranspositionsValue.Value);
		}

		if (UnicodeAwareValue.HasValue)
		{
			writer.WritePropertyName("unicode_aware");
			writer.WriteBooleanValue(UnicodeAwareValue.Value);
		}

		writer.WriteEndObject();
	}
}