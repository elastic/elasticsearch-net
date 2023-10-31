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

public sealed partial class FingerprintTokenFilter : ITokenFilter
{
	[JsonInclude, JsonPropertyName("max_output_size")]
	public int? MaxOutputSize { get; set; }
	[JsonInclude, JsonPropertyName("separator")]
	public string? Separator { get; set; }

	[JsonInclude, JsonPropertyName("type")]
	public string Type => "fingerprint";

	[JsonInclude, JsonPropertyName("version")]
	public string? Version { get; set; }
}

public sealed partial class FingerprintTokenFilterDescriptor : SerializableDescriptor<FingerprintTokenFilterDescriptor>, IBuildableDescriptor<FingerprintTokenFilter>
{
	internal FingerprintTokenFilterDescriptor(Action<FingerprintTokenFilterDescriptor> configure) => configure.Invoke(this);

	public FingerprintTokenFilterDescriptor() : base()
	{
	}

	private int? MaxOutputSizeValue { get; set; }
	private string? SeparatorValue { get; set; }
	private string? VersionValue { get; set; }

	public FingerprintTokenFilterDescriptor MaxOutputSize(int? maxOutputSize)
	{
		MaxOutputSizeValue = maxOutputSize;
		return Self;
	}

	public FingerprintTokenFilterDescriptor Separator(string? separator)
	{
		SeparatorValue = separator;
		return Self;
	}

	public FingerprintTokenFilterDescriptor Version(string? version)
	{
		VersionValue = version;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (MaxOutputSizeValue.HasValue)
		{
			writer.WritePropertyName("max_output_size");
			writer.WriteNumberValue(MaxOutputSizeValue.Value);
		}

		if (!string.IsNullOrEmpty(SeparatorValue))
		{
			writer.WritePropertyName("separator");
			writer.WriteStringValue(SeparatorValue);
		}

		writer.WritePropertyName("type");
		writer.WriteStringValue("fingerprint");
		if (VersionValue is not null)
		{
			writer.WritePropertyName("version");
			JsonSerializer.Serialize(writer, VersionValue, options);
		}

		writer.WriteEndObject();
	}

	FingerprintTokenFilter IBuildableDescriptor<FingerprintTokenFilter>.Build() => new()
	{
		MaxOutputSize = MaxOutputSizeValue,
		Separator = SeparatorValue,
		Version = VersionValue
	};
}