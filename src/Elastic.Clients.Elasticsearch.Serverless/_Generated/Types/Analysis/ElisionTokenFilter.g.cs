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

using Elastic.Clients.Elasticsearch.Serverless.Fluent;
using Elastic.Clients.Elasticsearch.Serverless.Serialization;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch.Serverless.Analysis;

public sealed partial class ElisionTokenFilter : ITokenFilter
{
	[JsonInclude, JsonPropertyName("articles")]
	public ICollection<string>? Articles { get; set; }
	[JsonInclude, JsonPropertyName("articles_case")]
	public bool? ArticlesCase { get; set; }
	[JsonInclude, JsonPropertyName("articles_path")]
	public string? ArticlesPath { get; set; }

	[JsonInclude, JsonPropertyName("type")]
	public string Type => "elision";

	[JsonInclude, JsonPropertyName("version")]
	public string? Version { get; set; }
}

public sealed partial class ElisionTokenFilterDescriptor : SerializableDescriptor<ElisionTokenFilterDescriptor>, IBuildableDescriptor<ElisionTokenFilter>
{
	internal ElisionTokenFilterDescriptor(Action<ElisionTokenFilterDescriptor> configure) => configure.Invoke(this);

	public ElisionTokenFilterDescriptor() : base()
	{
	}

	private ICollection<string>? ArticlesValue { get; set; }
	private bool? ArticlesCaseValue { get; set; }
	private string? ArticlesPathValue { get; set; }
	private string? VersionValue { get; set; }

	public ElisionTokenFilterDescriptor Articles(ICollection<string>? articles)
	{
		ArticlesValue = articles;
		return Self;
	}

	public ElisionTokenFilterDescriptor ArticlesCase(bool? articlesCase = true)
	{
		ArticlesCaseValue = articlesCase;
		return Self;
	}

	public ElisionTokenFilterDescriptor ArticlesPath(string? articlesPath)
	{
		ArticlesPathValue = articlesPath;
		return Self;
	}

	public ElisionTokenFilterDescriptor Version(string? version)
	{
		VersionValue = version;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (ArticlesValue is not null)
		{
			writer.WritePropertyName("articles");
			JsonSerializer.Serialize(writer, ArticlesValue, options);
		}

		if (ArticlesCaseValue.HasValue)
		{
			writer.WritePropertyName("articles_case");
			writer.WriteBooleanValue(ArticlesCaseValue.Value);
		}

		if (!string.IsNullOrEmpty(ArticlesPathValue))
		{
			writer.WritePropertyName("articles_path");
			writer.WriteStringValue(ArticlesPathValue);
		}

		writer.WritePropertyName("type");
		writer.WriteStringValue("elision");
		if (!string.IsNullOrEmpty(VersionValue))
		{
			writer.WritePropertyName("version");
			writer.WriteStringValue(VersionValue);
		}

		writer.WriteEndObject();
	}

	ElisionTokenFilter IBuildableDescriptor<ElisionTokenFilter>.Build() => new()
	{
		Articles = ArticlesValue,
		ArticlesCase = ArticlesCaseValue,
		ArticlesPath = ArticlesPathValue,
		Version = VersionValue
	};
}