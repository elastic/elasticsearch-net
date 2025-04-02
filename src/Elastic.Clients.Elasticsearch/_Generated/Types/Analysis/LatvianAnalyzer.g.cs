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

internal sealed partial class LatvianAnalyzerConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Analysis.LatvianAnalyzer>
{
	private static readonly System.Text.Json.JsonEncodedText PropStemExclusion = System.Text.Json.JsonEncodedText.Encode("stem_exclusion");
	private static readonly System.Text.Json.JsonEncodedText PropStopwords = System.Text.Json.JsonEncodedText.Encode("stopwords");
	private static readonly System.Text.Json.JsonEncodedText PropStopwordsPath = System.Text.Json.JsonEncodedText.Encode("stopwords_path");
	private static readonly System.Text.Json.JsonEncodedText PropType = System.Text.Json.JsonEncodedText.Encode("type");

	public override Elastic.Clients.Elasticsearch.Analysis.LatvianAnalyzer Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<System.Collections.Generic.ICollection<string>?> propStemExclusion = default;
		LocalJsonValue<System.Collections.Generic.ICollection<string>?> propStopwords = default;
		LocalJsonValue<string?> propStopwordsPath = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propStemExclusion.TryReadProperty(ref reader, options, PropStemExclusion, static System.Collections.Generic.ICollection<string>? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadCollectionValue<string>(o, null)))
			{
				continue;
			}

			if (propStopwords.TryReadProperty(ref reader, options, PropStopwords, static System.Collections.Generic.ICollection<string>? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadSingleOrManyCollectionValue<string>(o, null)))
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
		return new Elastic.Clients.Elasticsearch.Analysis.LatvianAnalyzer(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			StemExclusion = propStemExclusion.Value,
			Stopwords = propStopwords.Value,
			StopwordsPath = propStopwordsPath.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Analysis.LatvianAnalyzer value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropStemExclusion, value.StemExclusion, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.ICollection<string>? v) => w.WriteCollectionValue<string>(o, v, null));
		writer.WriteProperty(options, PropStopwords, value.Stopwords, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, System.Collections.Generic.ICollection<string>? v) => w.WriteSingleOrManyCollectionValue<string>(o, v, null));
		writer.WriteProperty(options, PropStopwordsPath, value.StopwordsPath, null, null);
		writer.WriteProperty(options, PropType, value.Type, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Analysis.LatvianAnalyzerConverter))]
public sealed partial class LatvianAnalyzer : Elastic.Clients.Elasticsearch.Analysis.IAnalyzer
{
#if NET7_0_OR_GREATER
	public LatvianAnalyzer()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	public LatvianAnalyzer()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal LatvianAnalyzer(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	public System.Collections.Generic.ICollection<string>? StemExclusion { get; set; }
	public System.Collections.Generic.ICollection<string>? Stopwords { get; set; }
	public string? StopwordsPath { get; set; }

	public string Type => "latvian";
}

public readonly partial struct LatvianAnalyzerDescriptor
{
	internal Elastic.Clients.Elasticsearch.Analysis.LatvianAnalyzer Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public LatvianAnalyzerDescriptor(Elastic.Clients.Elasticsearch.Analysis.LatvianAnalyzer instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public LatvianAnalyzerDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.Analysis.LatvianAnalyzer(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Analysis.LatvianAnalyzerDescriptor(Elastic.Clients.Elasticsearch.Analysis.LatvianAnalyzer instance) => new Elastic.Clients.Elasticsearch.Analysis.LatvianAnalyzerDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Analysis.LatvianAnalyzer(Elastic.Clients.Elasticsearch.Analysis.LatvianAnalyzerDescriptor descriptor) => descriptor.Instance;

	public Elastic.Clients.Elasticsearch.Analysis.LatvianAnalyzerDescriptor StemExclusion(System.Collections.Generic.ICollection<string>? value)
	{
		Instance.StemExclusion = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Analysis.LatvianAnalyzerDescriptor StemExclusion()
	{
		Instance.StemExclusion = Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfString.Build(null);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Analysis.LatvianAnalyzerDescriptor StemExclusion(System.Action<Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfString>? action)
	{
		Instance.StemExclusion = Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfString.Build(action);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Analysis.LatvianAnalyzerDescriptor StemExclusion(params string[] values)
	{
		Instance.StemExclusion = [.. values];
		return this;
	}

	public Elastic.Clients.Elasticsearch.Analysis.LatvianAnalyzerDescriptor Stopwords(System.Collections.Generic.ICollection<string>? value)
	{
		Instance.Stopwords = value;
		return this;
	}

	public Elastic.Clients.Elasticsearch.Analysis.LatvianAnalyzerDescriptor Stopwords()
	{
		Instance.Stopwords = Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfString.Build(null);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Analysis.LatvianAnalyzerDescriptor Stopwords(System.Action<Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfString>? action)
	{
		Instance.Stopwords = Elastic.Clients.Elasticsearch.Fluent.FluentICollectionOfString.Build(action);
		return this;
	}

	public Elastic.Clients.Elasticsearch.Analysis.LatvianAnalyzerDescriptor Stopwords(params string[] values)
	{
		Instance.Stopwords = [.. values];
		return this;
	}

	public Elastic.Clients.Elasticsearch.Analysis.LatvianAnalyzerDescriptor StopwordsPath(string? value)
	{
		Instance.StopwordsPath = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Analysis.LatvianAnalyzer Build(System.Action<Elastic.Clients.Elasticsearch.Analysis.LatvianAnalyzerDescriptor>? action)
	{
		if (action is null)
		{
			return new Elastic.Clients.Elasticsearch.Analysis.LatvianAnalyzer(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
		}

		var builder = new Elastic.Clients.Elasticsearch.Analysis.LatvianAnalyzerDescriptor(new Elastic.Clients.Elasticsearch.Analysis.LatvianAnalyzer(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}