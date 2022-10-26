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

using Elastic.Clients.Elasticsearch.Fluent;
using Elastic.Clients.Elasticsearch.Serialization;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;

#nullable restore
namespace Elastic.Clients.Elasticsearch.Analysis;
public sealed partial class StopAnalyzer : IAnalyzer
{
	[JsonInclude]
	[JsonPropertyName("stopwords")]
	[JsonConverter(typeof(StopWordsConverter))]
	public IList<string>? Stopwords { get; set; }

	[JsonInclude]
	[JsonPropertyName("stopwords_path")]
	public string? StopwordsPath { get; set; }

	[JsonInclude]
	[JsonPropertyName("type")]
	public string Type => "stop";
	[JsonInclude]
	[JsonPropertyName("version")]
	public string? Version { get; set; }
}

public sealed partial class StopAnalyzerDescriptor : SerializableDescriptor<StopAnalyzerDescriptor>, IBuildableDescriptor<StopAnalyzer>
{
	internal StopAnalyzerDescriptor(Action<StopAnalyzerDescriptor> configure) => configure.Invoke(this);
	public StopAnalyzerDescriptor() : base()
	{
	}

	private IList<string>? StopwordsValue { get; set; }

	private string? StopwordsPathValue { get; set; }

	private string? VersionValue { get; set; }

	public StopAnalyzerDescriptor Stopwords(IList<string>? stopwords)
	{
		StopwordsValue = stopwords;
		return Self;
	}

	public StopAnalyzerDescriptor StopwordsPath(string? stopwordsPath)
	{
		StopwordsPathValue = stopwordsPath;
		return Self;
	}

	public StopAnalyzerDescriptor Version(string? version)
	{
		VersionValue = version;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
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
		writer.WriteStringValue("stop");
		if (VersionValue is not null)
		{
			writer.WritePropertyName("version");
			JsonSerializer.Serialize(writer, VersionValue, options);
		}

		writer.WriteEndObject();
	}

	StopAnalyzer IBuildableDescriptor<StopAnalyzer>.Build() => new()
	{ Stopwords = StopwordsValue, StopwordsPath = StopwordsPathValue, Version = VersionValue };
}