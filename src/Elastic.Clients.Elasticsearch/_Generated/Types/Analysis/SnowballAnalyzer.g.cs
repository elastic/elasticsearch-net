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

internal sealed partial class SnowballAnalyzerConverter : System.Text.Json.Serialization.JsonConverter<SnowballAnalyzer>
{
	private static readonly System.Text.Json.JsonEncodedText PropLanguage = System.Text.Json.JsonEncodedText.Encode("language");
	private static readonly System.Text.Json.JsonEncodedText PropStopwords = System.Text.Json.JsonEncodedText.Encode("stopwords");
	private static readonly System.Text.Json.JsonEncodedText PropType = System.Text.Json.JsonEncodedText.Encode("type");
	private static readonly System.Text.Json.JsonEncodedText PropVersion = System.Text.Json.JsonEncodedText.Encode("version");

	public override SnowballAnalyzer Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<Elastic.Clients.Elasticsearch.Analysis.SnowballLanguage> propLanguage = default;
		LocalJsonValue<ICollection<string>?> propStopwords = default;
		LocalJsonValue<string?> propVersion = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propLanguage.TryReadProperty(ref reader, options, PropLanguage, null))
			{
				continue;
			}

			if (propStopwords.TryReadProperty(ref reader, options, PropStopwords, static ICollection<string>? (ref System.Text.Json.Utf8JsonReader r, System.Text.Json.JsonSerializerOptions o) => r.ReadSingleOrManyCollectionValue<string>(o, null)))
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
			}

			throw new System.Text.Json.JsonException($"Unknown JSON property '{reader.GetString()}' for type '{typeToConvert.Name}'.");
		}

		reader.ValidateToken(System.Text.Json.JsonTokenType.EndObject);
		return new SnowballAnalyzer
		{
			Language = propLanguage.Value
,
			Stopwords = propStopwords.Value
,
			Version = propVersion.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, SnowballAnalyzer value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropLanguage, value.Language, null, null);
		writer.WriteProperty(options, PropStopwords, value.Stopwords, null, static (System.Text.Json.Utf8JsonWriter w, System.Text.Json.JsonSerializerOptions o, ICollection<string>? v) => w.WriteSingleOrManyCollectionValue<string>(o, v, null));
		writer.WriteProperty(options, PropType, value.Type, null, null);
		writer.WriteProperty(options, PropVersion, value.Version, null, null);
		writer.WriteEndObject();
	}
}

[JsonConverter(typeof(SnowballAnalyzerConverter))]
public sealed partial class SnowballAnalyzer : IAnalyzer
{
	public Elastic.Clients.Elasticsearch.Analysis.SnowballLanguage Language { get; set; }
	public ICollection<string>? Stopwords { get; set; }

	public string Type => "snowball";

	public string? Version { get; set; }
}

public sealed partial class SnowballAnalyzerDescriptor : SerializableDescriptor<SnowballAnalyzerDescriptor>, IBuildableDescriptor<SnowballAnalyzer>
{
	internal SnowballAnalyzerDescriptor(Action<SnowballAnalyzerDescriptor> configure) => configure.Invoke(this);

	public SnowballAnalyzerDescriptor() : base()
	{
	}

	private Elastic.Clients.Elasticsearch.Analysis.SnowballLanguage LanguageValue { get; set; }
	private ICollection<string>? StopwordsValue { get; set; }
	private string? VersionValue { get; set; }

	public SnowballAnalyzerDescriptor Language(Elastic.Clients.Elasticsearch.Analysis.SnowballLanguage language)
	{
		LanguageValue = language;
		return Self;
	}

	public SnowballAnalyzerDescriptor Stopwords(ICollection<string>? stopwords)
	{
		StopwordsValue = stopwords;
		return Self;
	}

	public SnowballAnalyzerDescriptor Version(string? version)
	{
		VersionValue = version;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		writer.WritePropertyName("language");
		JsonSerializer.Serialize(writer, LanguageValue, options);
		if (StopwordsValue is not null)
		{
			writer.WritePropertyName("stopwords");
			SingleOrManySerializationHelper.Serialize<string>(StopwordsValue, writer, options);
		}

		writer.WritePropertyName("type");
		writer.WriteStringValue("snowball");
		if (!string.IsNullOrEmpty(VersionValue))
		{
			writer.WritePropertyName("version");
			writer.WriteStringValue(VersionValue);
		}

		writer.WriteEndObject();
	}

	SnowballAnalyzer IBuildableDescriptor<SnowballAnalyzer>.Build() => new()
	{
		Language = LanguageValue,
		Stopwords = StopwordsValue,
		Version = VersionValue
	};
}