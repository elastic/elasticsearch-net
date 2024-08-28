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

using Elastic.Clients.Elasticsearch.Core;
using Elastic.Clients.Elasticsearch.Fluent;
using Elastic.Clients.Elasticsearch.Serialization;
using Elastic.Transport;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch.Analysis;

public partial class Normalizers : IsADictionary<string, INormalizer>
{
	public Normalizers()
	{
	}

	public Normalizers(IDictionary<string, INormalizer> container) : base(container)
	{
	}

	public void Add(string name, INormalizer normalizer) => BackingDictionary.Add(Sanitize(name), normalizer);
	public bool TryGetNormalizer(string name, [NotNullWhen(returnValue: true)] out INormalizer normalizer) => BackingDictionary.TryGetValue(Sanitize(name), out normalizer);

	public bool TryGetNormalizer<T>(string name, [NotNullWhen(returnValue: true)] out T? normalizer) where T : class, INormalizer
	{
		if (BackingDictionary.TryGetValue(Sanitize(name), out var matchedValue) && matchedValue is T finalValue)
		{
			normalizer = finalValue;
			return true;
		}

		normalizer = null;
		return false;
	}
}

public sealed partial class NormalizersDescriptor : IsADictionaryDescriptor<NormalizersDescriptor, Normalizers, string, INormalizer>
{
	public NormalizersDescriptor() : base(new Normalizers())
	{
	}

	public NormalizersDescriptor(Normalizers normalizers) : base(normalizers ?? new Normalizers())
	{
	}

	public NormalizersDescriptor Custom(string normalizerName) => AssignVariant<Elastic.Clients.Elasticsearch.Analysis.CustomNormalizerDescriptor, CustomNormalizer>(normalizerName, null);
	public NormalizersDescriptor Custom(string normalizerName, Action<Elastic.Clients.Elasticsearch.Analysis.CustomNormalizerDescriptor> configure) => AssignVariant<Elastic.Clients.Elasticsearch.Analysis.CustomNormalizerDescriptor, CustomNormalizer>(normalizerName, configure);
	public NormalizersDescriptor Custom(string normalizerName, CustomNormalizer customNormalizer) => AssignVariant(normalizerName, customNormalizer);
	public NormalizersDescriptor Lowercase(string normalizerName) => AssignVariant<Elastic.Clients.Elasticsearch.Analysis.LowercaseNormalizerDescriptor, LowercaseNormalizer>(normalizerName, null);
	public NormalizersDescriptor Lowercase(string normalizerName, Action<Elastic.Clients.Elasticsearch.Analysis.LowercaseNormalizerDescriptor> configure) => AssignVariant<Elastic.Clients.Elasticsearch.Analysis.LowercaseNormalizerDescriptor, LowercaseNormalizer>(normalizerName, configure);
	public NormalizersDescriptor Lowercase(string normalizerName, LowercaseNormalizer lowercaseNormalizer) => AssignVariant(normalizerName, lowercaseNormalizer);
}

internal sealed partial class NormalizerInterfaceConverter : JsonConverter<INormalizer>
{
	public override INormalizer Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		var copiedReader = reader;
		string? type = null;
		using var jsonDoc = JsonDocument.ParseValue(ref copiedReader);
		if (jsonDoc is not null && jsonDoc.RootElement.TryGetProperty("type", out var readType) && readType.ValueKind == JsonValueKind.String)
		{
			type = readType.ToString();
		}

		switch (type)
		{
			case "custom":
				return JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.Analysis.CustomNormalizer>(ref reader, options);
			case "lowercase":
				return JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.Analysis.LowercaseNormalizer>(ref reader, options);
			default:
				return JsonSerializer.Deserialize<Elastic.Clients.Elasticsearch.Analysis.CustomNormalizer>(ref reader, options);
		}
	}

	public override void Write(Utf8JsonWriter writer, INormalizer value, JsonSerializerOptions options)
	{
		if (value is null)
		{
			writer.WriteNullValue();
			return;
		}

		switch (value.Type)
		{
			case "custom":
				JsonSerializer.Serialize(writer, value, typeof(Elastic.Clients.Elasticsearch.Analysis.CustomNormalizer), options);
				return;
			case "lowercase":
				JsonSerializer.Serialize(writer, value, typeof(Elastic.Clients.Elasticsearch.Analysis.LowercaseNormalizer), options);
				return;
			default:
				var type = value.GetType();
				JsonSerializer.Serialize(writer, value, type, options);
				return;
		}
	}
}

/// <summary>
/// <para><see href="https://www.elastic.co/guide/en/elasticsearch/reference/8.15/analysis-normalizers.html">Learn more about this API in the Elasticsearch documentation.</see></para>
/// </summary>
[JsonConverter(typeof(NormalizerInterfaceConverter))]
public partial interface INormalizer
{
	public string? Type { get; }
}