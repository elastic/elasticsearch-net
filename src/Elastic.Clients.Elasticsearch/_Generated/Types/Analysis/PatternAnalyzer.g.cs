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

internal sealed partial class PatternAnalyzerConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Analysis.PatternAnalyzer>
{
	private static readonly System.Text.Json.JsonEncodedText PropFlags = System.Text.Json.JsonEncodedText.Encode("flags");
	private static readonly System.Text.Json.JsonEncodedText PropLowercase = System.Text.Json.JsonEncodedText.Encode("lowercase");
	private static readonly System.Text.Json.JsonEncodedText PropPattern = System.Text.Json.JsonEncodedText.Encode("pattern");
	private static readonly System.Text.Json.JsonEncodedText PropStopwords = System.Text.Json.JsonEncodedText.Encode("stopwords");
	private static readonly System.Text.Json.JsonEncodedText PropStopwordsPath = System.Text.Json.JsonEncodedText.Encode("stopwords_path");
	private static readonly System.Text.Json.JsonEncodedText PropType = System.Text.Json.JsonEncodedText.Encode("type");
	private static readonly System.Text.Json.JsonEncodedText PropVersion = System.Text.Json.JsonEncodedText.Encode("version");

	public override Elastic.Clients.Elasticsearch.Analysis.PatternAnalyzer Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<string?> propFlags = default;
		LocalJsonValue<bool?> propLowercase = default;
		LocalJsonValue<string?> propPattern = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Union<Elastic.Clients.Elasticsearch.Analysis.StopWordLanguage, System.Collections.Generic.ICollection<string>>?> propStopwords = default;
		LocalJsonValue<string?> propStopwordsPath = default;
		LocalJsonValue<string?> propVersion = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propFlags.TryReadProperty(ref reader, options, PropFlags, null))
			{
				continue;
			}

			if (propLowercase.TryReadProperty(ref reader, options, PropLowercase, static bool? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadNullableValue<bool>(o)))
			{
				continue;
			}

			if (propPattern.TryReadProperty(ref reader, options, PropPattern, null))
			{
				continue;
			}

			if (propStopwords.TryReadProperty(ref reader, options, PropStopwords, static Elastic.Clients.Elasticsearch.Union<Elastic.Clients.Elasticsearch.Analysis.StopWordLanguage, System.Collections.Generic.ICollection<string>>? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadUnionValue<Elastic.Clients.Elasticsearch.Analysis.StopWordLanguage, System.Collections.Generic.ICollection<string>>(o, static (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => JsonUnionSelector.ByTokenType(ref r, o, Elastic.Clients.Elasticsearch.Serialization.JsonTokenTypes.String, Elastic.Clients.Elasticsearch.Serialization.JsonTokenTypes.StartArray), null, static System.Collections.Generic.ICollection<string> (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadCollectionValue<string>(o, null)!)))
			{
				continue;
			}

			if (propStopwordsPath.TryReadProperty(ref reader, options, PropStopwordsPath, null))
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

			if (options.UnmappedMemberHandling is System.Text.Json.Serialization.JsonUnmappedMemberHandling.Skip)
			{
				reader.Skip();
				continue;
			}

			throw new System.Text.Json.JsonException($"Unknown JSON property '{reader.GetString()}' for type '{typeToConvert.Name}'.");
		}

		reader.ValidateToken(System.Text.Json.JsonTokenType.EndObject);
		return new Elastic.Clients.Elasticsearch.Analysis.PatternAnalyzer(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			Flags = propFlags.Value,
			Lowercase = propLowercase.Value,
			Pattern = propPattern.Value,
			Stopwords = propStopwords.Value,
			StopwordsPath = propStopwordsPath.Value,
#pragma warning disable CS0618
			Version = propVersion.Value
#pragma warning restore CS0618
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Analysis.PatternAnalyzer value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropFlags, value.Flags, null, null);
		writer.WriteProperty(options, PropLowercase, value.Lowercase, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, bool? v) => w.WriteNullableValue<bool>(o, v));
		writer.WriteProperty(options, PropPattern, value.Pattern, null, null);
		writer.WriteProperty(options, PropStopwords, value.Stopwords, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, Elastic.Clients.Elasticsearch.Union<Elastic.Clients.Elasticsearch.Analysis.StopWordLanguage, System.Collections.Generic.ICollection<string>>? v) => w.WriteUnionValue<Elastic.Clients.Elasticsearch.Analysis.StopWordLanguage, System.Collections.Generic.ICollection<string>>(o, v, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.ICollection<string> v) => w.WriteCollectionValue<string>(o, v, null)));
		writer.WriteProperty(options, PropStopwordsPath, value.StopwordsPath, null, null);
		writer.WriteProperty(options, PropType, value.Type, null, null);
#pragma warning disable CS0618
		writer.WriteProperty(options, PropVersion, value.Version, null, null)
#pragma warning restore CS0618
		;
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Analysis.PatternAnalyzerConverter))]
public sealed partial class PatternAnalyzer : Elastic.Clients.Elasticsearch.Analysis.IAnalyzer
{
#if NET7_0_OR_GREATER
	public PatternAnalyzer()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	public PatternAnalyzer()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal PatternAnalyzer(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	/// <summary>
	/// <para>
	/// Java regular expression flags. Flags should be pipe-separated, eg "CASE_INSENSITIVE|COMMENTS".
	/// </para>
	/// </summary>
	public string? Flags { get; set; }

	/// <summary>
	/// <para>
	/// Should terms be lowercased or not.
	/// Defaults to <c>true</c>.
	/// </para>
	/// </summary>
	public bool? Lowercase { get; set; }

	/// <summary>
	/// <para>
	/// A Java regular expression.
	/// Defaults to <c>\W+</c>.
	/// </para>
	/// </summary>
	public string? Pattern { get; set; }

	/// <summary>
	/// <para>
	/// A pre-defined stop words list like <c>_english_</c> or an array containing a list of stop words.
	/// Defaults to <c>_none_</c>.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Union<Elastic.Clients.Elasticsearch.Analysis.StopWordLanguage, System.Collections.Generic.ICollection<string>>? Stopwords { get; set; }

	/// <summary>
	/// <para>
	/// The path to a file containing stop words.
	/// </para>
	/// </summary>
	public string? StopwordsPath { get; set; }

	public string Type => "pattern";

	[System.Obsolete("Deprecated in '7.14.0'.")]
	public string? Version { get; set; }
}

public readonly partial struct PatternAnalyzerDescriptor
{
	internal Elastic.Clients.Elasticsearch.Analysis.PatternAnalyzer Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public PatternAnalyzerDescriptor(Elastic.Clients.Elasticsearch.Analysis.PatternAnalyzer instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public PatternAnalyzerDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.Analysis.PatternAnalyzer(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Analysis.PatternAnalyzerDescriptor(Elastic.Clients.Elasticsearch.Analysis.PatternAnalyzer instance) => new Elastic.Clients.Elasticsearch.Analysis.PatternAnalyzerDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Analysis.PatternAnalyzer(Elastic.Clients.Elasticsearch.Analysis.PatternAnalyzerDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// Java regular expression flags. Flags should be pipe-separated, eg "CASE_INSENSITIVE|COMMENTS".
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Analysis.PatternAnalyzerDescriptor Flags(string? value)
	{
		Instance.Flags = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Should terms be lowercased or not.
	/// Defaults to <c>true</c>.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Analysis.PatternAnalyzerDescriptor Lowercase(bool? value = true)
	{
		Instance.Lowercase = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// A Java regular expression.
	/// Defaults to <c>\W+</c>.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Analysis.PatternAnalyzerDescriptor Pattern(string? value)
	{
		Instance.Pattern = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// A pre-defined stop words list like <c>_english_</c> or an array containing a list of stop words.
	/// Defaults to <c>_none_</c>.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Analysis.PatternAnalyzerDescriptor Stopwords(Elastic.Clients.Elasticsearch.Union<Elastic.Clients.Elasticsearch.Analysis.StopWordLanguage, System.Collections.Generic.ICollection<string>>? value)
	{
		Instance.Stopwords = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The path to a file containing stop words.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Analysis.PatternAnalyzerDescriptor StopwordsPath(string? value)
	{
		Instance.StopwordsPath = value;
		return this;
	}

	[System.Obsolete("Deprecated in '7.14.0'.")]
	public Elastic.Clients.Elasticsearch.Analysis.PatternAnalyzerDescriptor Version(string? value)
	{
		Instance.Version = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Analysis.PatternAnalyzer Build(System.Action<Elastic.Clients.Elasticsearch.Analysis.PatternAnalyzerDescriptor>? action)
	{
		if (action is null)
		{
			return new Elastic.Clients.Elasticsearch.Analysis.PatternAnalyzer(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
		}

		var builder = new Elastic.Clients.Elasticsearch.Analysis.PatternAnalyzerDescriptor(new Elastic.Clients.Elasticsearch.Analysis.PatternAnalyzer(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}