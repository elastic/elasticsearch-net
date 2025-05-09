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

internal sealed partial class TurkishAnalyzerConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Analysis.TurkishAnalyzer>
{
	private static readonly System.Text.Json.JsonEncodedText PropStemExclusion = System.Text.Json.JsonEncodedText.Encode("stem_exclusion");
	private static readonly System.Text.Json.JsonEncodedText PropStopwords = System.Text.Json.JsonEncodedText.Encode("stopwords");
	private static readonly System.Text.Json.JsonEncodedText PropStopwordsPath = System.Text.Json.JsonEncodedText.Encode("stopwords_path");
	private static readonly System.Text.Json.JsonEncodedText PropType = System.Text.Json.JsonEncodedText.Encode("type");

	public override Elastic.Clients.Elasticsearch.Analysis.TurkishAnalyzer Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<System.Collections.Generic.ICollection<string>?> propStemExclusion = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Union<Elastic.Clients.Elasticsearch.Analysis.StopWordLanguage, System.Collections.Generic.ICollection<string>>?> propStopwords = default;
		LocalJsonValue<string?> propStopwordsPath = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propStemExclusion.TryReadProperty(ref reader, options, PropStemExclusion, static System.Collections.Generic.ICollection<string>? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadCollectionValue<string>(o, null)))
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

			if (options.UnmappedMemberHandling is System.Text.Json.Serialization.JsonUnmappedMemberHandling.Skip)
			{
				reader.Skip();
				continue;
			}

			throw new System.Text.Json.JsonException($"Unknown JSON property '{reader.GetString()}' for type '{typeToConvert.Name}'.");
		}

		reader.ValidateToken(System.Text.Json.JsonTokenType.EndObject);
		return new Elastic.Clients.Elasticsearch.Analysis.TurkishAnalyzer(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			StemExclusion = propStemExclusion.Value,
			Stopwords = propStopwords.Value,
			StopwordsPath = propStopwordsPath.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Analysis.TurkishAnalyzer value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropStemExclusion, value.StemExclusion, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.ICollection<string>? v) => w.WriteCollectionValue<string>(o, v, null));
		writer.WriteProperty(options, PropStopwords, value.Stopwords, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, Elastic.Clients.Elasticsearch.Union<Elastic.Clients.Elasticsearch.Analysis.StopWordLanguage, System.Collections.Generic.ICollection<string>>? v) => w.WriteUnionValue<Elastic.Clients.Elasticsearch.Analysis.StopWordLanguage, System.Collections.Generic.ICollection<string>>(o, v, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.ICollection<string> v) => w.WriteCollectionValue<string>(o, v, null)));
		writer.WriteProperty(options, PropStopwordsPath, value.StopwordsPath, null, null);
		writer.WriteProperty(options, PropType, value.Type, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Analysis.TurkishAnalyzerConverter))]
public sealed partial class TurkishAnalyzer : Elastic.Clients.Elasticsearch.Analysis.IAnalyzer
{
#if NET7_0_OR_GREATER
	public TurkishAnalyzer()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	public TurkishAnalyzer()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal TurkishAnalyzer(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	public System.Collections.Generic.ICollection<string>? StemExclusion { get; set; }
	public Elastic.Clients.Elasticsearch.Union<Elastic.Clients.Elasticsearch.Analysis.StopWordLanguage, System.Collections.Generic.ICollection<string>>? Stopwords { get; set; }
	public string? StopwordsPath { get; set; }

	public string Type => "turkish";
}

public readonly partial struct TurkishAnalyzerDescriptor
{
	internal Elastic.Clients.Elasticsearch.Analysis.TurkishAnalyzer Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public TurkishAnalyzerDescriptor(Elastic.Clients.Elasticsearch.Analysis.TurkishAnalyzer instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public TurkishAnalyzerDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.Analysis.TurkishAnalyzer(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Analysis.TurkishAnalyzerDescriptor(Elastic.Clients.Elasticsearch.Analysis.TurkishAnalyzer instance) => new Elastic.Clients.Elasticsearch.Analysis.TurkishAnalyzerDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Analysis.TurkishAnalyzer(Elastic.Clients.Elasticsearch.Analysis.TurkishAnalyzerDescriptor descriptor) => descriptor.Instance;

	public Elastic.Clients.Elasticsearch.Analysis.TurkishAnalyzerDescriptor StemExclusion(System.Collections.Generic.ICollection<string>? value)
	{
		Instance.StemExclusion = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Analysis.TurkishAnalyzerDescriptor StemExclusion(params string[] values)
	{
		Instance.StemExclusion = [.. values];
		return this;
	}

	public Elastic.Clients.Elasticsearch.Analysis.TurkishAnalyzerDescriptor Stopwords(Elastic.Clients.Elasticsearch.Union<Elastic.Clients.Elasticsearch.Analysis.StopWordLanguage, System.Collections.Generic.ICollection<string>>? value)
	{
		Instance.Stopwords = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Analysis.TurkishAnalyzerDescriptor StopwordsPath(string? value)
	{
		Instance.StopwordsPath = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Analysis.TurkishAnalyzer Build(System.Action<Elastic.Clients.Elasticsearch.Analysis.TurkishAnalyzerDescriptor>? action)
	{
		if (action is null)
		{
			return new Elastic.Clients.Elasticsearch.Analysis.TurkishAnalyzer(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
		}

		var builder = new Elastic.Clients.Elasticsearch.Analysis.TurkishAnalyzerDescriptor(new Elastic.Clients.Elasticsearch.Analysis.TurkishAnalyzer(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}