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

public sealed partial class PersianAnalyzer : IAnalyzer
{
	[JsonInclude, JsonPropertyName("stopwords")]
	[SingleOrManyCollectionConverter(typeof(string))]
	public ICollection<string>? Stopwords { get; set; }
	[JsonInclude, JsonPropertyName("stopwords_path")]
	public string? StopwordsPath { get; set; }

	[JsonInclude, JsonPropertyName("type")]
	public string Type => "persian";
}

public sealed partial class PersianAnalyzerDescriptor : SerializableDescriptor<PersianAnalyzerDescriptor>, IBuildableDescriptor<PersianAnalyzer>
{
	internal PersianAnalyzerDescriptor(Action<PersianAnalyzerDescriptor> configure) => configure.Invoke(this);

	public PersianAnalyzerDescriptor() : base()
	{
	}

	private ICollection<string>? StopwordsValue { get; set; }
	private string? StopwordsPathValue { get; set; }

	public PersianAnalyzerDescriptor Stopwords(ICollection<string>? stopwords)
	{
		StopwordsValue = stopwords;
		return Self;
	}

	public PersianAnalyzerDescriptor StopwordsPath(string? stopwordsPath)
	{
		StopwordsPathValue = stopwordsPath;
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
		writer.WriteStringValue("persian");
		writer.WriteEndObject();
	}

	PersianAnalyzer IBuildableDescriptor<PersianAnalyzer>.Build() => new()
	{
		Stopwords = StopwordsValue,
		StopwordsPath = StopwordsPathValue
	};
}