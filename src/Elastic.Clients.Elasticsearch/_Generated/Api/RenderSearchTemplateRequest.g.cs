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
using Elastic.Clients.Elasticsearch.Requests;
using Elastic.Clients.Elasticsearch.Serialization;
using Elastic.Transport;
using Elastic.Transport.Extensions;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch;

public sealed partial class RenderSearchTemplateRequestParameters : RequestParameters
{
}

/// <summary>
/// <para>
/// Renders a search template as a search request body.
/// </para>
/// </summary>
public sealed partial class RenderSearchTemplateRequest : PlainRequest<RenderSearchTemplateRequestParameters>
{
	public RenderSearchTemplateRequest()
	{
	}

	public RenderSearchTemplateRequest(Elastic.Clients.Elasticsearch.Id? id) : base(r => r.Optional("id", id))
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.NoNamespaceRenderSearchTemplate;

	protected override HttpMethod StaticHttpMethod => HttpMethod.POST;

	internal override bool SupportsBody => true;

	internal override string OperationName => "render_search_template";

	[JsonInclude, JsonPropertyName("file")]
	public string? File { get; set; }

	/// <summary>
	/// <para>
	/// Key-value pairs used to replace Mustache variables in the template.
	/// The key is the variable name.
	/// The value is the variable value.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("params")]
	public IDictionary<string, object>? Params { get; set; }

	/// <summary>
	/// <para>
	/// An inline search template.
	/// Supports the same parameters as the search API's request body.
	/// These parameters also support Mustache variables.
	/// If no <c>id</c> or <c>&lt;templated-id></c> is specified, this parameter is required.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("source")]
	public string? Source { get; set; }
}

/// <summary>
/// <para>
/// Renders a search template as a search request body.
/// </para>
/// </summary>
public sealed partial class RenderSearchTemplateRequestDescriptor<TDocument> : RequestDescriptor<RenderSearchTemplateRequestDescriptor<TDocument>, RenderSearchTemplateRequestParameters>
{
	internal RenderSearchTemplateRequestDescriptor(Action<RenderSearchTemplateRequestDescriptor<TDocument>> configure) => configure.Invoke(this);

	public RenderSearchTemplateRequestDescriptor(Elastic.Clients.Elasticsearch.Id? id) : base(r => r.Optional("id", id))
	{
	}

	public RenderSearchTemplateRequestDescriptor()
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.NoNamespaceRenderSearchTemplate;

	protected override HttpMethod StaticHttpMethod => HttpMethod.POST;

	internal override bool SupportsBody => true;

	internal override string OperationName => "render_search_template";

	public RenderSearchTemplateRequestDescriptor<TDocument> Id(Elastic.Clients.Elasticsearch.Id? id)
	{
		RouteValues.Optional("id", id);
		return Self;
	}

	private string? FileValue { get; set; }
	private IDictionary<string, object>? ParamsValue { get; set; }
	private string? SourceValue { get; set; }

	public RenderSearchTemplateRequestDescriptor<TDocument> File(string? file)
	{
		FileValue = file;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Key-value pairs used to replace Mustache variables in the template.
	/// The key is the variable name.
	/// The value is the variable value.
	/// </para>
	/// </summary>
	public RenderSearchTemplateRequestDescriptor<TDocument> Params(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> selector)
	{
		ParamsValue = selector?.Invoke(new FluentDictionary<string, object>());
		return Self;
	}

	/// <summary>
	/// <para>
	/// An inline search template.
	/// Supports the same parameters as the search API's request body.
	/// These parameters also support Mustache variables.
	/// If no <c>id</c> or <c>&lt;templated-id></c> is specified, this parameter is required.
	/// </para>
	/// </summary>
	public RenderSearchTemplateRequestDescriptor<TDocument> Source(string? source)
	{
		SourceValue = source;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (!string.IsNullOrEmpty(FileValue))
		{
			writer.WritePropertyName("file");
			writer.WriteStringValue(FileValue);
		}

		if (ParamsValue is not null)
		{
			writer.WritePropertyName("params");
			JsonSerializer.Serialize(writer, ParamsValue, options);
		}

		if (!string.IsNullOrEmpty(SourceValue))
		{
			writer.WritePropertyName("source");
			writer.WriteStringValue(SourceValue);
		}

		writer.WriteEndObject();
	}
}

/// <summary>
/// <para>
/// Renders a search template as a search request body.
/// </para>
/// </summary>
public sealed partial class RenderSearchTemplateRequestDescriptor : RequestDescriptor<RenderSearchTemplateRequestDescriptor, RenderSearchTemplateRequestParameters>
{
	internal RenderSearchTemplateRequestDescriptor(Action<RenderSearchTemplateRequestDescriptor> configure) => configure.Invoke(this);

	public RenderSearchTemplateRequestDescriptor(Elastic.Clients.Elasticsearch.Id? id) : base(r => r.Optional("id", id))
	{
	}

	public RenderSearchTemplateRequestDescriptor()
	{
	}

	internal override ApiUrls ApiUrls => ApiUrlLookup.NoNamespaceRenderSearchTemplate;

	protected override HttpMethod StaticHttpMethod => HttpMethod.POST;

	internal override bool SupportsBody => true;

	internal override string OperationName => "render_search_template";

	public RenderSearchTemplateRequestDescriptor Id(Elastic.Clients.Elasticsearch.Id? id)
	{
		RouteValues.Optional("id", id);
		return Self;
	}

	private string? FileValue { get; set; }
	private IDictionary<string, object>? ParamsValue { get; set; }
	private string? SourceValue { get; set; }

	public RenderSearchTemplateRequestDescriptor File(string? file)
	{
		FileValue = file;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Key-value pairs used to replace Mustache variables in the template.
	/// The key is the variable name.
	/// The value is the variable value.
	/// </para>
	/// </summary>
	public RenderSearchTemplateRequestDescriptor Params(Func<FluentDictionary<string, object>, FluentDictionary<string, object>> selector)
	{
		ParamsValue = selector?.Invoke(new FluentDictionary<string, object>());
		return Self;
	}

	/// <summary>
	/// <para>
	/// An inline search template.
	/// Supports the same parameters as the search API's request body.
	/// These parameters also support Mustache variables.
	/// If no <c>id</c> or <c>&lt;templated-id></c> is specified, this parameter is required.
	/// </para>
	/// </summary>
	public RenderSearchTemplateRequestDescriptor Source(string? source)
	{
		SourceValue = source;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (!string.IsNullOrEmpty(FileValue))
		{
			writer.WritePropertyName("file");
			writer.WriteStringValue(FileValue);
		}

		if (ParamsValue is not null)
		{
			writer.WritePropertyName("params");
			JsonSerializer.Serialize(writer, ParamsValue, options);
		}

		if (!string.IsNullOrEmpty(SourceValue))
		{
			writer.WritePropertyName("source");
			writer.WriteStringValue(SourceValue);
		}

		writer.WriteEndObject();
	}
}