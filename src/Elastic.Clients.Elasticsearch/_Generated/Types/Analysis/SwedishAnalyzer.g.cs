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

public sealed partial class SwedishAnalyzer : IAnalyzer
{
	[JsonInclude, JsonPropertyName("stem_exclusion")]
	public ICollection<string>? StemExclusion { get; set; }
	[JsonInclude, JsonPropertyName("stopwords")]
	[SingleOrManyCollectionConverter(typeof(string))]
	public ICollection<string>? Stopwords { get; set; }
	[JsonInclude, JsonPropertyName("stopwords_path")]
	public string? StopwordsPath { get; set; }

	[JsonInclude, JsonPropertyName("type")]
	public string Type => "swedish";
}

public sealed partial class SwedishAnalyzerDescriptor : SerializableDescriptor<SwedishAnalyzerDescriptor>, IBuildableDescriptor<SwedishAnalyzer>
{
	internal SwedishAnalyzerDescriptor(Action<SwedishAnalyzerDescriptor> configure) => configure.Invoke(this);

	public SwedishAnalyzerDescriptor() : base()
	{
	}

	private ICollection<string>? StemExclusionValue { get; set; }
	private ICollection<string>? StopwordsValue { get; set; }
	private string? StopwordsPathValue { get; set; }

	public SwedishAnalyzerDescriptor StemExclusion(ICollection<string>? stemExclusion)
	{
		StemExclusionValue = stemExclusion;
		return Self;
	}

	public SwedishAnalyzerDescriptor Stopwords(ICollection<string>? stopwords)
	{
		StopwordsValue = stopwords;
		return Self;
	}

	public SwedishAnalyzerDescriptor StopwordsPath(string? stopwordsPath)
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
		writer.WriteStringValue("swedish");
		writer.WriteEndObject();
	}

	SwedishAnalyzer IBuildableDescriptor<SwedishAnalyzer>.Build() => new()
	{
		StemExclusion = StemExclusionValue,
		Stopwords = StopwordsValue,
		StopwordsPath = StopwordsPathValue
	};
}