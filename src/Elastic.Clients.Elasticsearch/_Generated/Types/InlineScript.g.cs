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
namespace Elastic.Clients.Elasticsearch;
public sealed partial class InlineScript
{
	[JsonInclude, JsonPropertyName("lang")]
	public Elastic.Clients.Elasticsearch.ScriptLanguage? Language { get; set; }

	[JsonInclude, JsonPropertyName("options")]
	public IDictionary<string, string>? Options { get; set; }

	[JsonInclude, JsonPropertyName("params")]
	public IDictionary<string, object>? Params { get; set; }

	[JsonInclude, JsonPropertyName("source")]
	public string Source { get; set; }
}

public sealed partial class InlineScriptDescriptor : SerializableDescriptor<InlineScriptDescriptor>
{
	internal InlineScriptDescriptor(Action<InlineScriptDescriptor> configure) => configure.Invoke(this);
	public InlineScriptDescriptor() : base()
	{
	}

	private Elastic.Clients.Elasticsearch.ScriptLanguage? LanguageValue { get; set; }

	private IDictionary<string, string>? OptionsValue { get; set; }

	private IDictionary<string, object>? ParamsValue { get; set; }

	private string SourceValue { get; set; }

	public InlineScriptDescriptor Language(Elastic.Clients.Elasticsearch.ScriptLanguage? language)
	{
		LanguageValue = language;
		return Self;
	}

	public InlineScriptDescriptor Options(Func<FluentDictionary<string, string>, FluentDictionary<string, string>> selector)
	{
		OptionsValue = selector?.Invoke(new FluentDictionary<string, string>());
		return Self;
	}

	public InlineScriptDescriptor Params(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> selector)
	{
		ParamsValue = selector?.Invoke(new FluentDictionary<string, object>());
		return Self;
	}

	public InlineScriptDescriptor Source(string source)
	{
		SourceValue = source;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (LanguageValue is not null)
		{
			writer.WritePropertyName("lang");
			JsonSerializer.Serialize(writer, LanguageValue, options);
		}

		if (OptionsValue is not null)
		{
			writer.WritePropertyName("options");
			JsonSerializer.Serialize(writer, OptionsValue, options);
		}

		if (ParamsValue is not null)
		{
			writer.WritePropertyName("params");
			JsonSerializer.Serialize(writer, ParamsValue, options);
		}

		writer.WritePropertyName("source");
		writer.WriteStringValue(SourceValue);
		writer.WriteEndObject();
	}
}