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

public sealed partial class HtmlStripCharFilter : ICharFilter
{
	[JsonInclude, JsonPropertyName("escaped_tags")]
	public ICollection<string>? EscapedTags { get; set; }

	[JsonInclude, JsonPropertyName("type")]
	public string Type => "html_strip";

	[JsonInclude, JsonPropertyName("version")]
	public string? Version { get; set; }
}

public sealed partial class HtmlStripCharFilterDescriptor : SerializableDescriptor<HtmlStripCharFilterDescriptor>, IBuildableDescriptor<HtmlStripCharFilter>
{
	internal HtmlStripCharFilterDescriptor(Action<HtmlStripCharFilterDescriptor> configure) => configure.Invoke(this);

	public HtmlStripCharFilterDescriptor() : base()
	{
	}

	private ICollection<string>? EscapedTagsValue { get; set; }
	private string? VersionValue { get; set; }

	public HtmlStripCharFilterDescriptor EscapedTags(ICollection<string>? escapedTags)
	{
		EscapedTagsValue = escapedTags;
		return Self;
	}

	public HtmlStripCharFilterDescriptor Version(string? version)
	{
		VersionValue = version;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (EscapedTagsValue is not null)
		{
			writer.WritePropertyName("escaped_tags");
			JsonSerializer.Serialize(writer, EscapedTagsValue, options);
		}

		writer.WritePropertyName("type");
		writer.WriteStringValue("html_strip");
		if (!string.IsNullOrEmpty(VersionValue))
		{
			writer.WritePropertyName("version");
			writer.WriteStringValue(VersionValue);
		}

		writer.WriteEndObject();
	}

	HtmlStripCharFilter IBuildableDescriptor<HtmlStripCharFilter>.Build() => new()
	{
		EscapedTags = EscapedTagsValue,
		Version = VersionValue
	};
}