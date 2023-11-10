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

public sealed partial class LimitTokenCountTokenFilter : ITokenFilter
{
	[JsonInclude, JsonPropertyName("consume_all_tokens")]
	public bool? ConsumeAllTokens { get; set; }
	[JsonInclude, JsonPropertyName("max_token_count")]
	[JsonConverter(typeof(StringifiedIntegerConverter))]
	public int? MaxTokenCount { get; set; }

	[JsonInclude, JsonPropertyName("type")]
	public string Type => "limit";

	[JsonInclude, JsonPropertyName("version")]
	public string? Version { get; set; }
}

public sealed partial class LimitTokenCountTokenFilterDescriptor : SerializableDescriptor<LimitTokenCountTokenFilterDescriptor>, IBuildableDescriptor<LimitTokenCountTokenFilter>
{
	internal LimitTokenCountTokenFilterDescriptor(Action<LimitTokenCountTokenFilterDescriptor> configure) => configure.Invoke(this);

	public LimitTokenCountTokenFilterDescriptor() : base()
	{
	}

	private bool? ConsumeAllTokensValue { get; set; }
	private int? MaxTokenCountValue { get; set; }
	private string? VersionValue { get; set; }

	public LimitTokenCountTokenFilterDescriptor ConsumeAllTokens(bool? consumeAllTokens = true)
	{
		ConsumeAllTokensValue = consumeAllTokens;
		return Self;
	}

	public LimitTokenCountTokenFilterDescriptor MaxTokenCount(int? maxTokenCount)
	{
		MaxTokenCountValue = maxTokenCount;
		return Self;
	}

	public LimitTokenCountTokenFilterDescriptor Version(string? version)
	{
		VersionValue = version;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (ConsumeAllTokensValue.HasValue)
		{
			writer.WritePropertyName("consume_all_tokens");
			writer.WriteBooleanValue(ConsumeAllTokensValue.Value);
		}

		if (MaxTokenCountValue is not null)
		{
			writer.WritePropertyName("max_token_count");
			JsonSerializer.Serialize(writer, MaxTokenCountValue, options);
		}

		writer.WritePropertyName("type");
		writer.WriteStringValue("limit");
		if (VersionValue is not null)
		{
			writer.WritePropertyName("version");
			JsonSerializer.Serialize(writer, VersionValue, options);
		}

		writer.WriteEndObject();
	}

	LimitTokenCountTokenFilter IBuildableDescriptor<LimitTokenCountTokenFilter>.Build() => new()
	{
		ConsumeAllTokens = ConsumeAllTokensValue,
		MaxTokenCount = MaxTokenCountValue,
		Version = VersionValue
	};
}