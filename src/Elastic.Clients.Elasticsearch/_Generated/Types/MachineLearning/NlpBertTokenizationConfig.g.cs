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

namespace Elastic.Clients.Elasticsearch.MachineLearning;

internal sealed partial class NlpBertTokenizationConfigConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.MachineLearning.NlpBertTokenizationConfig>
{
	private static readonly System.Text.Json.JsonEncodedText PropDoLowerCase = System.Text.Json.JsonEncodedText.Encode("do_lower_case");
	private static readonly System.Text.Json.JsonEncodedText PropMaxSequenceLength = System.Text.Json.JsonEncodedText.Encode("max_sequence_length");
	private static readonly System.Text.Json.JsonEncodedText PropSpan = System.Text.Json.JsonEncodedText.Encode("span");
	private static readonly System.Text.Json.JsonEncodedText PropTruncate = System.Text.Json.JsonEncodedText.Encode("truncate");
	private static readonly System.Text.Json.JsonEncodedText PropWithSpecialTokens = System.Text.Json.JsonEncodedText.Encode("with_special_tokens");

	public override Elastic.Clients.Elasticsearch.MachineLearning.NlpBertTokenizationConfig Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<bool?> propDoLowerCase = default;
		LocalJsonValue<int?> propMaxSequenceLength = default;
		LocalJsonValue<int?> propSpan = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.MachineLearning.TokenizationTruncate?> propTruncate = default;
		LocalJsonValue<bool?> propWithSpecialTokens = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propDoLowerCase.TryReadProperty(ref reader, options, PropDoLowerCase, null))
			{
				continue;
			}

			if (propMaxSequenceLength.TryReadProperty(ref reader, options, PropMaxSequenceLength, null))
			{
				continue;
			}

			if (propSpan.TryReadProperty(ref reader, options, PropSpan, null))
			{
				continue;
			}

			if (propTruncate.TryReadProperty(ref reader, options, PropTruncate, null))
			{
				continue;
			}

			if (propWithSpecialTokens.TryReadProperty(ref reader, options, PropWithSpecialTokens, null))
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
		return new Elastic.Clients.Elasticsearch.MachineLearning.NlpBertTokenizationConfig(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			DoLowerCase = propDoLowerCase.Value,
			MaxSequenceLength = propMaxSequenceLength.Value,
			Span = propSpan.Value,
			Truncate = propTruncate.Value,
			WithSpecialTokens = propWithSpecialTokens.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.MachineLearning.NlpBertTokenizationConfig value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropDoLowerCase, value.DoLowerCase, null, null);
		writer.WriteProperty(options, PropMaxSequenceLength, value.MaxSequenceLength, null, null);
		writer.WriteProperty(options, PropSpan, value.Span, null, null);
		writer.WriteProperty(options, PropTruncate, value.Truncate, null, null);
		writer.WriteProperty(options, PropWithSpecialTokens, value.WithSpecialTokens, null, null);
		writer.WriteEndObject();
	}
}

/// <summary>
/// <para>
/// BERT and MPNet tokenization configuration options
/// </para>
/// </summary>
[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.MachineLearning.NlpBertTokenizationConfigConverter))]
public sealed partial class NlpBertTokenizationConfig
{
#if NET7_0_OR_GREATER
	public NlpBertTokenizationConfig()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	public NlpBertTokenizationConfig()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal NlpBertTokenizationConfig(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	/// <summary>
	/// <para>
	/// Should the tokenizer lower case the text
	/// </para>
	/// </summary>
	public bool? DoLowerCase { get; set; }

	/// <summary>
	/// <para>
	/// Maximum input sequence length for the model
	/// </para>
	/// </summary>
	public int? MaxSequenceLength { get; set; }

	/// <summary>
	/// <para>
	/// Tokenization spanning options. Special value of -1 indicates no spanning takes place
	/// </para>
	/// </summary>
	public int? Span { get; set; }

	/// <summary>
	/// <para>
	/// Should tokenization input be automatically truncated before sending to the model for inference
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.TokenizationTruncate? Truncate { get; set; }

	/// <summary>
	/// <para>
	/// Is tokenization completed with special tokens
	/// </para>
	/// </summary>
	public bool? WithSpecialTokens { get; set; }
}

/// <summary>
/// <para>
/// BERT and MPNet tokenization configuration options
/// </para>
/// </summary>
public readonly partial struct NlpBertTokenizationConfigDescriptor
{
	internal Elastic.Clients.Elasticsearch.MachineLearning.NlpBertTokenizationConfig Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public NlpBertTokenizationConfigDescriptor(Elastic.Clients.Elasticsearch.MachineLearning.NlpBertTokenizationConfig instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public NlpBertTokenizationConfigDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.MachineLearning.NlpBertTokenizationConfig(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.MachineLearning.NlpBertTokenizationConfigDescriptor(Elastic.Clients.Elasticsearch.MachineLearning.NlpBertTokenizationConfig instance) => new Elastic.Clients.Elasticsearch.MachineLearning.NlpBertTokenizationConfigDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.MachineLearning.NlpBertTokenizationConfig(Elastic.Clients.Elasticsearch.MachineLearning.NlpBertTokenizationConfigDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// Should the tokenizer lower case the text
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.NlpBertTokenizationConfigDescriptor DoLowerCase(bool? value = true)
	{
		Instance.DoLowerCase = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Maximum input sequence length for the model
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.NlpBertTokenizationConfigDescriptor MaxSequenceLength(int? value)
	{
		Instance.MaxSequenceLength = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Tokenization spanning options. Special value of -1 indicates no spanning takes place
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.NlpBertTokenizationConfigDescriptor Span(int? value)
	{
		Instance.Span = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Should tokenization input be automatically truncated before sending to the model for inference
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.NlpBertTokenizationConfigDescriptor Truncate(Elastic.Clients.Elasticsearch.MachineLearning.TokenizationTruncate? value)
	{
		Instance.Truncate = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Is tokenization completed with special tokens
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.MachineLearning.NlpBertTokenizationConfigDescriptor WithSpecialTokens(bool? value = true)
	{
		Instance.WithSpecialTokens = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.MachineLearning.NlpBertTokenizationConfig Build(System.Action<Elastic.Clients.Elasticsearch.MachineLearning.NlpBertTokenizationConfigDescriptor>? action)
	{
		if (action is null)
		{
			return new Elastic.Clients.Elasticsearch.MachineLearning.NlpBertTokenizationConfig(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
		}

		var builder = new Elastic.Clients.Elasticsearch.MachineLearning.NlpBertTokenizationConfigDescriptor(new Elastic.Clients.Elasticsearch.MachineLearning.NlpBertTokenizationConfig(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}