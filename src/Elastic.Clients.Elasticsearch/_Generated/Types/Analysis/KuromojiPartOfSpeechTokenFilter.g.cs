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

public sealed partial class KuromojiPartOfSpeechTokenFilter : ITokenFilter
{
	[JsonInclude, JsonPropertyName("stoptags")]
	public ICollection<string> Stoptags { get; set; }

	[JsonInclude, JsonPropertyName("type")]
	public string Type => "kuromoji_part_of_speech";

	[JsonInclude, JsonPropertyName("version")]
	public string? Version { get; set; }
}

public sealed partial class KuromojiPartOfSpeechTokenFilterDescriptor : SerializableDescriptor<KuromojiPartOfSpeechTokenFilterDescriptor>, IBuildableDescriptor<KuromojiPartOfSpeechTokenFilter>
{
	internal KuromojiPartOfSpeechTokenFilterDescriptor(Action<KuromojiPartOfSpeechTokenFilterDescriptor> configure) => configure.Invoke(this);

	public KuromojiPartOfSpeechTokenFilterDescriptor() : base()
	{
	}

	private ICollection<string> StoptagsValue { get; set; }
	private string? VersionValue { get; set; }

	public KuromojiPartOfSpeechTokenFilterDescriptor Stoptags(ICollection<string> stoptags)
	{
		StoptagsValue = stoptags;
		return Self;
	}

	public KuromojiPartOfSpeechTokenFilterDescriptor Version(string? version)
	{
		VersionValue = version;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		writer.WritePropertyName("stoptags");
		JsonSerializer.Serialize(writer, StoptagsValue, options);
		writer.WritePropertyName("type");
		writer.WriteStringValue("kuromoji_part_of_speech");
		if (!string.IsNullOrEmpty(VersionValue))
		{
			writer.WritePropertyName("version");
			writer.WriteStringValue(VersionValue);
		}

		writer.WriteEndObject();
	}

	KuromojiPartOfSpeechTokenFilter IBuildableDescriptor<KuromojiPartOfSpeechTokenFilter>.Build() => new()
	{
		Stoptags = StoptagsValue,
		Version = VersionValue
	};
}