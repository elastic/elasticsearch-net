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

internal sealed partial class FieldSuggesterConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Core.Search.FieldSuggester>
{
	private static readonly System.Text.Json.JsonEncodedText PropPrefix = System.Text.Json.JsonEncodedText.Encode("prefix");
	private static readonly System.Text.Json.JsonEncodedText PropRegex = System.Text.Json.JsonEncodedText.Encode("regex");
	private static readonly System.Text.Json.JsonEncodedText PropText = System.Text.Json.JsonEncodedText.Encode("text");
	private static readonly System.Text.Json.JsonEncodedText VariantCompletion = System.Text.Json.JsonEncodedText.Encode("completion");
	private static readonly System.Text.Json.JsonEncodedText VariantPhrase = System.Text.Json.JsonEncodedText.Encode("phrase");
	private static readonly System.Text.Json.JsonEncodedText VariantTerm = System.Text.Json.JsonEncodedText.Encode("term");

	public override Elastic.Clients.Elasticsearch.Core.Search.FieldSuggester Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<string?> propPrefix = default;
		LocalJsonValue<string?> propRegex = default;
		LocalJsonValue<string?> propText = default;
		string? variantType = null;
		object? variant = null;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propPrefix.TryReadProperty(ref reader, options, PropPrefix, null))
			{
				continue;
			}

			if (propRegex.TryReadProperty(ref reader, options, PropRegex, null))
			{
				continue;
			}

			if (propText.TryReadProperty(ref reader, options, PropText, null))
			{
				continue;
			}

			if (reader.ValueTextEquals(VariantCompletion))
			{
				variantType = VariantCompletion.Value;
				reader.Read();
				variant = reader.ReadValue<Elastic.Clients.Elasticsearch.Core.Search.CompletionSuggester>(options, null);
				continue;
			}

			if (reader.ValueTextEquals(VariantPhrase))
			{
				variantType = VariantPhrase.Value;
				reader.Read();
				variant = reader.ReadValue<Elastic.Clients.Elasticsearch.Core.Search.PhraseSuggester>(options, null);
				continue;
			}

			if (reader.ValueTextEquals(VariantTerm))
			{
				variantType = VariantTerm.Value;
				reader.Read();
				variant = reader.ReadValue<Elastic.Clients.Elasticsearch.Core.Search.TermSuggester>(options, null);
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
		return new Elastic.Clients.Elasticsearch.Core.Search.FieldSuggester(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			VariantType = variantType,
			Variant = variant,
			Prefix = propPrefix.Value,
			Regex = propRegex.Value,
			Text = propText.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Core.Search.FieldSuggester value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		switch (value.VariantType)
		{
			case null:
				break;
			case "completion":
				writer.WriteProperty(options, value.VariantType, (Elastic.Clients.Elasticsearch.Core.Search.CompletionSuggester)value.Variant, null, null);
				break;
			case "phrase":
				writer.WriteProperty(options, value.VariantType, (Elastic.Clients.Elasticsearch.Core.Search.PhraseSuggester)value.Variant, null, null);
				break;
			case "term":
				writer.WriteProperty(options, value.VariantType, (Elastic.Clients.Elasticsearch.Core.Search.TermSuggester)value.Variant, null, null);
				break;
			default:
				throw new System.Text.Json.JsonException($"Variant '{value.VariantType}' is not supported for type '{nameof(Elastic.Clients.Elasticsearch.Core.Search.FieldSuggester)}'.");
		}

		writer.WriteProperty(options, PropPrefix, value.Prefix, null, null);
		writer.WriteProperty(options, PropRegex, value.Regex, null, null);
		writer.WriteProperty(options, PropText, value.Text, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Core.Search.FieldSuggesterConverter))]
public sealed partial class FieldSuggester
{
	internal string? VariantType { get; set; }
	internal object? Variant { get; set; }
#if NET7_0_OR_GREATER
	public FieldSuggester()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	public FieldSuggester()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal FieldSuggester(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	/// <summary>
	/// <para>
	/// Provides auto-complete/search-as-you-type functionality.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Search.CompletionSuggester? Completion { get => GetVariant<Elastic.Clients.Elasticsearch.Core.Search.CompletionSuggester>("completion"); set => SetVariant("completion", value); }

	/// <summary>
	/// <para>
	/// Provides access to word alternatives on a per token basis within a certain string distance.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Search.PhraseSuggester? Phrase { get => GetVariant<Elastic.Clients.Elasticsearch.Core.Search.PhraseSuggester>("phrase"); set => SetVariant("phrase", value); }

	/// <summary>
	/// <para>
	/// Suggests terms based on edit distance.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Search.TermSuggester? Term { get => GetVariant<Elastic.Clients.Elasticsearch.Core.Search.TermSuggester>("term"); set => SetVariant("term", value); }

	/// <summary>
	/// <para>
	/// Prefix used to search for suggestions.
	/// </para>
	/// </summary>
	public string? Prefix { get; set; }

	/// <summary>
	/// <para>
	/// A prefix expressed as a regular expression.
	/// </para>
	/// </summary>
	public string? Regex { get; set; }

	/// <summary>
	/// <para>
	/// The text to use as input for the suggester.
	/// Needs to be set globally or per suggestion.
	/// </para>
	/// </summary>
	public string? Text { get; set; }

	public static implicit operator Elastic.Clients.Elasticsearch.Core.Search.FieldSuggester(Elastic.Clients.Elasticsearch.Core.Search.CompletionSuggester value) => new Elastic.Clients.Elasticsearch.Core.Search.FieldSuggester { Completion = value };
	public static implicit operator Elastic.Clients.Elasticsearch.Core.Search.FieldSuggester(Elastic.Clients.Elasticsearch.Core.Search.PhraseSuggester value) => new Elastic.Clients.Elasticsearch.Core.Search.FieldSuggester { Phrase = value };
	public static implicit operator Elastic.Clients.Elasticsearch.Core.Search.FieldSuggester(Elastic.Clients.Elasticsearch.Core.Search.TermSuggester value) => new Elastic.Clients.Elasticsearch.Core.Search.FieldSuggester { Term = value };

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	private T? GetVariant<T>(string type)
	{
		if (string.Equals(VariantType, type, System.StringComparison.Ordinal) && Variant is T result)
		{
			return result;
		}

		return default;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	private void SetVariant<T>(string type, T? value)
	{
		VariantType = type;
		Variant = value;
	}
}

public readonly partial struct FieldSuggesterDescriptor<TDocument>
{
	internal Elastic.Clients.Elasticsearch.Core.Search.FieldSuggester Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public FieldSuggesterDescriptor(Elastic.Clients.Elasticsearch.Core.Search.FieldSuggester instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public FieldSuggesterDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.Core.Search.FieldSuggester(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Core.Search.FieldSuggesterDescriptor<TDocument>(Elastic.Clients.Elasticsearch.Core.Search.FieldSuggester instance) => new Elastic.Clients.Elasticsearch.Core.Search.FieldSuggesterDescriptor<TDocument>(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Core.Search.FieldSuggester(Elastic.Clients.Elasticsearch.Core.Search.FieldSuggesterDescriptor<TDocument> descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// Provides auto-complete/search-as-you-type functionality.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Search.FieldSuggesterDescriptor<TDocument> Completion(Elastic.Clients.Elasticsearch.Core.Search.CompletionSuggester? value)
	{
		Instance.Completion = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Provides auto-complete/search-as-you-type functionality.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Search.FieldSuggesterDescriptor<TDocument> Completion(System.Action<Elastic.Clients.Elasticsearch.Core.Search.CompletionSuggesterDescriptor<TDocument>> action)
	{
		Instance.Completion = Elastic.Clients.Elasticsearch.Core.Search.CompletionSuggesterDescriptor<TDocument>.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// Provides access to word alternatives on a per token basis within a certain string distance.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Search.FieldSuggesterDescriptor<TDocument> Phrase(Elastic.Clients.Elasticsearch.Core.Search.PhraseSuggester? value)
	{
		Instance.Phrase = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Provides access to word alternatives on a per token basis within a certain string distance.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Search.FieldSuggesterDescriptor<TDocument> Phrase(System.Action<Elastic.Clients.Elasticsearch.Core.Search.PhraseSuggesterDescriptor<TDocument>> action)
	{
		Instance.Phrase = Elastic.Clients.Elasticsearch.Core.Search.PhraseSuggesterDescriptor<TDocument>.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// Suggests terms based on edit distance.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Search.FieldSuggesterDescriptor<TDocument> Term(Elastic.Clients.Elasticsearch.Core.Search.TermSuggester? value)
	{
		Instance.Term = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Suggests terms based on edit distance.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Search.FieldSuggesterDescriptor<TDocument> Term(System.Action<Elastic.Clients.Elasticsearch.Core.Search.TermSuggesterDescriptor<TDocument>> action)
	{
		Instance.Term = Elastic.Clients.Elasticsearch.Core.Search.TermSuggesterDescriptor<TDocument>.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// Prefix used to search for suggestions.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Search.FieldSuggesterDescriptor<TDocument> Prefix(string? value)
	{
		Instance.Prefix = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// A prefix expressed as a regular expression.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Search.FieldSuggesterDescriptor<TDocument> Regex(string? value)
	{
		Instance.Regex = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The text to use as input for the suggester.
	/// Needs to be set globally or per suggestion.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Search.FieldSuggesterDescriptor<TDocument> Text(string? value)
	{
		Instance.Text = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Core.Search.FieldSuggester Build(System.Action<Elastic.Clients.Elasticsearch.Core.Search.FieldSuggesterDescriptor<TDocument>> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.Core.Search.FieldSuggesterDescriptor<TDocument>(new Elastic.Clients.Elasticsearch.Core.Search.FieldSuggester(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}

public readonly partial struct FieldSuggesterDescriptor
{
	internal Elastic.Clients.Elasticsearch.Core.Search.FieldSuggester Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public FieldSuggesterDescriptor(Elastic.Clients.Elasticsearch.Core.Search.FieldSuggester instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public FieldSuggesterDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.Core.Search.FieldSuggester(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Core.Search.FieldSuggesterDescriptor(Elastic.Clients.Elasticsearch.Core.Search.FieldSuggester instance) => new Elastic.Clients.Elasticsearch.Core.Search.FieldSuggesterDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Core.Search.FieldSuggester(Elastic.Clients.Elasticsearch.Core.Search.FieldSuggesterDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// Provides auto-complete/search-as-you-type functionality.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Search.FieldSuggesterDescriptor Completion(Elastic.Clients.Elasticsearch.Core.Search.CompletionSuggester? value)
	{
		Instance.Completion = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Provides auto-complete/search-as-you-type functionality.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Search.FieldSuggesterDescriptor Completion(System.Action<Elastic.Clients.Elasticsearch.Core.Search.CompletionSuggesterDescriptor> action)
	{
		Instance.Completion = Elastic.Clients.Elasticsearch.Core.Search.CompletionSuggesterDescriptor.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// Provides auto-complete/search-as-you-type functionality.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Search.FieldSuggesterDescriptor Completion<T>(System.Action<Elastic.Clients.Elasticsearch.Core.Search.CompletionSuggesterDescriptor<T>> action)
	{
		Instance.Completion = Elastic.Clients.Elasticsearch.Core.Search.CompletionSuggesterDescriptor<T>.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// Provides access to word alternatives on a per token basis within a certain string distance.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Search.FieldSuggesterDescriptor Phrase(Elastic.Clients.Elasticsearch.Core.Search.PhraseSuggester? value)
	{
		Instance.Phrase = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Provides access to word alternatives on a per token basis within a certain string distance.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Search.FieldSuggesterDescriptor Phrase(System.Action<Elastic.Clients.Elasticsearch.Core.Search.PhraseSuggesterDescriptor> action)
	{
		Instance.Phrase = Elastic.Clients.Elasticsearch.Core.Search.PhraseSuggesterDescriptor.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// Provides access to word alternatives on a per token basis within a certain string distance.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Search.FieldSuggesterDescriptor Phrase<T>(System.Action<Elastic.Clients.Elasticsearch.Core.Search.PhraseSuggesterDescriptor<T>> action)
	{
		Instance.Phrase = Elastic.Clients.Elasticsearch.Core.Search.PhraseSuggesterDescriptor<T>.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// Suggests terms based on edit distance.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Search.FieldSuggesterDescriptor Term(Elastic.Clients.Elasticsearch.Core.Search.TermSuggester? value)
	{
		Instance.Term = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Suggests terms based on edit distance.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Search.FieldSuggesterDescriptor Term(System.Action<Elastic.Clients.Elasticsearch.Core.Search.TermSuggesterDescriptor> action)
	{
		Instance.Term = Elastic.Clients.Elasticsearch.Core.Search.TermSuggesterDescriptor.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// Suggests terms based on edit distance.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Search.FieldSuggesterDescriptor Term<T>(System.Action<Elastic.Clients.Elasticsearch.Core.Search.TermSuggesterDescriptor<T>> action)
	{
		Instance.Term = Elastic.Clients.Elasticsearch.Core.Search.TermSuggesterDescriptor<T>.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// Prefix used to search for suggestions.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Search.FieldSuggesterDescriptor Prefix(string? value)
	{
		Instance.Prefix = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// A prefix expressed as a regular expression.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Search.FieldSuggesterDescriptor Regex(string? value)
	{
		Instance.Regex = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The text to use as input for the suggester.
	/// Needs to be set globally or per suggestion.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Core.Search.FieldSuggesterDescriptor Text(string? value)
	{
		Instance.Text = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Core.Search.FieldSuggester Build(System.Action<Elastic.Clients.Elasticsearch.Core.Search.FieldSuggesterDescriptor> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.Core.Search.FieldSuggesterDescriptor(new Elastic.Clients.Elasticsearch.Core.Search.FieldSuggester(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}