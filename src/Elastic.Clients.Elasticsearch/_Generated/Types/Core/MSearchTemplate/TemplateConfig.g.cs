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
namespace Elastic.Clients.Elasticsearch.Core.MSearchTemplate;
public sealed partial class TemplateConfig
{
	[JsonInclude, JsonPropertyName("explain")]
	public bool? Explain { get; set; }

	[JsonInclude, JsonPropertyName("id")]
	public Elastic.Clients.Elasticsearch.Id? Id { get; set; }

	[JsonInclude, JsonPropertyName("params")]
	public IDictionary<string, object>? Params { get; set; }

	[JsonInclude, JsonPropertyName("profile")]
	public bool? Profile { get; set; }

	[JsonInclude, JsonPropertyName("source")]
	public string? Source { get; set; }
}

public sealed partial class TemplateConfigDescriptor : SerializableDescriptor<TemplateConfigDescriptor>
{
	internal TemplateConfigDescriptor(Action<TemplateConfigDescriptor> configure) => configure.Invoke(this);
	public TemplateConfigDescriptor() : base()
	{
	}

	private bool? ExplainValue { get; set; }

	private Elastic.Clients.Elasticsearch.Id? IdValue { get; set; }

	private IDictionary<string, object>? ParamsValue { get; set; }

	private bool? ProfileValue { get; set; }

	private string? SourceValue { get; set; }

	public TemplateConfigDescriptor Explain(bool? explain = true)
	{
		ExplainValue = explain;
		return Self;
	}

	public TemplateConfigDescriptor Id(Elastic.Clients.Elasticsearch.Id? id)
	{
		IdValue = id;
		return Self;
	}

	public TemplateConfigDescriptor Params(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> selector)
	{
		ParamsValue = selector?.Invoke(new FluentDictionary<string, object>());
		return Self;
	}

	public TemplateConfigDescriptor Profile(bool? profile = true)
	{
		ProfileValue = profile;
		return Self;
	}

	public TemplateConfigDescriptor Source(string? source)
	{
		SourceValue = source;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (ExplainValue.HasValue)
		{
			writer.WritePropertyName("explain");
			writer.WriteBooleanValue(ExplainValue.Value);
		}

		if (IdValue is not null)
		{
			writer.WritePropertyName("id");
			JsonSerializer.Serialize(writer, IdValue, options);
		}

		if (ParamsValue is not null)
		{
			writer.WritePropertyName("params");
			JsonSerializer.Serialize(writer, ParamsValue, options);
		}

		if (ProfileValue.HasValue)
		{
			writer.WritePropertyName("profile");
			writer.WriteBooleanValue(ProfileValue.Value);
		}

		if (!string.IsNullOrEmpty(SourceValue))
		{
			writer.WritePropertyName("source");
			writer.WriteStringValue(SourceValue);
		}

		writer.WriteEndObject();
	}
}