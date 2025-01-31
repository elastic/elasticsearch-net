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

namespace Elastic.Clients.Elasticsearch.Analysis;

internal sealed partial class LithuanianAnalyzerConverter : System.Text.Json.Serialization.JsonConverter<LithuanianAnalyzer>
{
	private static readonly System.Text.Json.JsonEncodedText PropStemExclusion = System.Text.Json.JsonEncodedText.Encode("stem_exclusion");
	private static readonly System.Text.Json.JsonEncodedText PropStopwords = System.Text.Json.JsonEncodedText.Encode("stopwords");
	private static readonly System.Text.Json.JsonEncodedText PropStopwordsPath = System.Text.Json.JsonEncodedText.Encode("stopwords_path");
	private static readonly System.Text.Json.JsonEncodedText PropType = System.Text.Json.JsonEncodedText.Encode("type");

	public override LithuanianAnalyzer Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<ICollection<string>?> propStemExclusion = default;
		LocalJsonValue<ICollection<string>?> propStopwords = default;
		LocalJsonValue<string?> propStopwordsPath = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propStemExclusion.TryRead(ref reader, options, PropStemExclusion))
			{
				continue;
			}

			if (propStopwords.TryRead(ref reader, options, PropStopwords, typeof(SingleOrManyMarker<ICollection<string>?, string>)))
			{
				continue;
			}

			if (propStopwordsPath.TryRead(ref reader, options, PropStopwordsPath))
			{
				continue;
			}

			if (reader.ValueTextEquals(PropType))
			{
				reader.Skip();
				continue;
			}

			throw new System.Text.Json.JsonException($"Unknown JSON property '{reader.GetString()}' for type '{typeToConvert.Name}'.");
		}

		reader.ValidateToken(System.Text.Json.JsonTokenType.EndObject);
		return new LithuanianAnalyzer
		{
			StemExclusion = propStemExclusion.Value
,
			Stopwords = propStopwords.Value
,
			StopwordsPath = propStopwordsPath.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, LithuanianAnalyzer value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropStemExclusion, value.StemExclusion);
		writer.WriteProperty(options, PropStopwords, value.Stopwords, null, typeof(SingleOrManyMarker<ICollection<string>?, string>));
		writer.WriteProperty(options, PropStopwordsPath, value.StopwordsPath);
		writer.WriteProperty(options, PropType, value.Type);
		writer.WriteEndObject();
	}
}

[JsonConverter(typeof(LithuanianAnalyzerConverter))]
public sealed partial class LithuanianAnalyzer : IAnalyzer
{
	public ICollection<string>? StemExclusion { get; set; }
	public ICollection<string>? Stopwords { get; set; }
	public string? StopwordsPath { get; set; }

	public string Type => "lithuanian";
}

public sealed partial class LithuanianAnalyzerDescriptor : SerializableDescriptor<LithuanianAnalyzerDescriptor>, IBuildableDescriptor<LithuanianAnalyzer>
{
	internal LithuanianAnalyzerDescriptor(Action<LithuanianAnalyzerDescriptor> configure) => configure.Invoke(this);

	public LithuanianAnalyzerDescriptor() : base()
	{
	}

	private ICollection<string>? StemExclusionValue { get; set; }
	private ICollection<string>? StopwordsValue { get; set; }
	private string? StopwordsPathValue { get; set; }

	public LithuanianAnalyzerDescriptor StemExclusion(ICollection<string>? stemExclusion)
	{
		StemExclusionValue = stemExclusion;
		return Self;
	}

	public LithuanianAnalyzerDescriptor Stopwords(ICollection<string>? stopwords)
	{
		StopwordsValue = stopwords;
		return Self;
	}

	public LithuanianAnalyzerDescriptor StopwordsPath(string? stopwordsPath)
	{
		StopwordsPathValue = stopwordsPath;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (StemExclusionValue is not null)
		{
			writer.WritePropertyName("stem_exclusion");
			JsonSerializer.Serialize(writer, StemExclusionValue, options);
		}

		if (StopwordsValue is not null)
		{
			writer.WritePropertyName("stopwords");
			SingleOrManySerializationHelper.Serialize<string>(StopwordsValue, writer, options);
		}

		if (!string.IsNullOrEmpty(StopwordsPathValue))
		{
			writer.WritePropertyName("stopwords_path");
			writer.WriteStringValue(StopwordsPathValue);
		}

		writer.WritePropertyName("type");
		writer.WriteStringValue("lithuanian");
		writer.WriteEndObject();
	}

	LithuanianAnalyzer IBuildableDescriptor<LithuanianAnalyzer>.Build() => new()
	{
		StemExclusion = StemExclusionValue,
		Stopwords = StopwordsValue,
		StopwordsPath = StopwordsPathValue
	};
}