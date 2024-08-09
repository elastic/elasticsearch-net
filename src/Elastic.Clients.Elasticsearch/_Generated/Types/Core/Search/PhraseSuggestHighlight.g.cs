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

namespace Elastic.Clients.Elasticsearch.Core.Search;

public sealed partial class PhraseSuggestHighlight
{
	/// <summary>
	/// <para>
	/// Use in conjunction with <c>pre_tag</c> to define the HTML tags to use for the highlighted text.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("post_tag")]
	public string PostTag { get; set; }

	/// <summary>
	/// <para>
	/// Use in conjunction with <c>post_tag</c> to define the HTML tags to use for the highlighted text.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("pre_tag")]
	public string PreTag { get; set; }
}

public sealed partial class PhraseSuggestHighlightDescriptor : SerializableDescriptor<PhraseSuggestHighlightDescriptor>
{
	internal PhraseSuggestHighlightDescriptor(Action<PhraseSuggestHighlightDescriptor> configure) => configure.Invoke(this);

	public PhraseSuggestHighlightDescriptor() : base()
	{
	}

	private string PostTagValue { get; set; }
	private string PreTagValue { get; set; }

	/// <summary>
	/// <para>
	/// Use in conjunction with <c>pre_tag</c> to define the HTML tags to use for the highlighted text.
	/// </para>
	/// </summary>
	public PhraseSuggestHighlightDescriptor PostTag(string postTag)
	{
		PostTagValue = postTag;
		return Self;
	}

	/// <summary>
	/// <para>
	/// Use in conjunction with <c>post_tag</c> to define the HTML tags to use for the highlighted text.
	/// </para>
	/// </summary>
	public PhraseSuggestHighlightDescriptor PreTag(string preTag)
	{
		PreTagValue = preTag;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		writer.WritePropertyName("post_tag");
		writer.WriteStringValue(PostTagValue);
		writer.WritePropertyName("pre_tag");
		writer.WriteStringValue(PreTagValue);
		writer.WriteEndObject();
	}
}