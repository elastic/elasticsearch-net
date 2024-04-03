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

public sealed partial class SnowballTokenFilter : ITokenFilter
{
	[JsonInclude, JsonPropertyName("language")]
	public Elastic.Clients.Elasticsearch.Analysis.SnowballLanguage Language { get; set; }

	[JsonInclude, JsonPropertyName("type")]
	public string Type => "snowball";

	[JsonInclude, JsonPropertyName("version")]
	public string? Version { get; set; }
}

public sealed partial class SnowballTokenFilterDescriptor : SerializableDescriptor<SnowballTokenFilterDescriptor>, IBuildableDescriptor<SnowballTokenFilter>
{
	internal SnowballTokenFilterDescriptor(Action<SnowballTokenFilterDescriptor> configure) => configure.Invoke(this);

	public SnowballTokenFilterDescriptor() : base()
	{
	}

	private Elastic.Clients.Elasticsearch.Analysis.SnowballLanguage LanguageValue { get; set; }
	private string? VersionValue { get; set; }

	public SnowballTokenFilterDescriptor Language(Elastic.Clients.Elasticsearch.Analysis.SnowballLanguage language)
	{
		LanguageValue = language;
		return Self;
	}

	public SnowballTokenFilterDescriptor Version(string? version)
	{
		VersionValue = version;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		writer.WritePropertyName("language");
		JsonSerializer.Serialize(writer, LanguageValue, options);
		writer.WritePropertyName("type");
		writer.WriteStringValue("snowball");
		if (!string.IsNullOrEmpty(VersionValue))
		{
			writer.WritePropertyName("version");
			writer.WriteStringValue(VersionValue);
		}

		writer.WriteEndObject();
	}

	SnowballTokenFilter IBuildableDescriptor<SnowballTokenFilter>.Build() => new()
	{
		Language = LanguageValue,
		Version = VersionValue
	};
}