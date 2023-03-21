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

public sealed partial class TruncateTokenFilter : ITokenFilter
{
	[JsonInclude, JsonPropertyName("length")]
	public int? Length { get; set; }

	[JsonInclude]
	[JsonPropertyName("type")]
	public string Type => "truncate";

	[JsonInclude, JsonPropertyName("version")]
	public string? Version { get; set; }
}

public sealed partial class TruncateTokenFilterDescriptor : SerializableDescriptor<TruncateTokenFilterDescriptor>, IBuildableDescriptor<TruncateTokenFilter>
{
	internal TruncateTokenFilterDescriptor(Action<TruncateTokenFilterDescriptor> configure) => configure.Invoke(this);

	public TruncateTokenFilterDescriptor() : base()
	{
	}

	private int? LengthValue { get; set; }
	private string? VersionValue { get; set; }

	public TruncateTokenFilterDescriptor Length(int? length)
	{
		LengthValue = length;
		return Self;
	}

	public TruncateTokenFilterDescriptor Version(string? version)
	{
		VersionValue = version;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (LengthValue.HasValue)
		{
			writer.WritePropertyName("length");
			writer.WriteNumberValue(LengthValue.Value);
		}

		writer.WritePropertyName("type");
		writer.WriteStringValue("truncate");
		if (VersionValue is not null)
		{
			writer.WritePropertyName("version");
			JsonSerializer.Serialize(writer, VersionValue, options);
		}

		writer.WriteEndObject();
	}

	TruncateTokenFilter IBuildableDescriptor<TruncateTokenFilter>.Build() => new()
	{
		Length = LengthValue,
		Version = VersionValue
	};
}