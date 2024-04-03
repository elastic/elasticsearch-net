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

namespace Elastic.Clients.Elasticsearch.MachineLearning;

public sealed partial class TrainedModelPrefixStrings
{
	/// <summary>
	/// <para>String prepended to input at ingest</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("ingest")]
	public string? Ingest { get; set; }

	/// <summary>
	/// <para>String prepended to input at search</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("search")]
	public string? Search { get; set; }
}

public sealed partial class TrainedModelPrefixStringsDescriptor : SerializableDescriptor<TrainedModelPrefixStringsDescriptor>
{
	internal TrainedModelPrefixStringsDescriptor(Action<TrainedModelPrefixStringsDescriptor> configure) => configure.Invoke(this);

	public TrainedModelPrefixStringsDescriptor() : base()
	{
	}

	private string? IngestValue { get; set; }
	private string? SearchValue { get; set; }

	/// <summary>
	/// <para>String prepended to input at ingest</para>
	/// </summary>
	public TrainedModelPrefixStringsDescriptor Ingest(string? ingest)
	{
		IngestValue = ingest;
		return Self;
	}

	/// <summary>
	/// <para>String prepended to input at search</para>
	/// </summary>
	public TrainedModelPrefixStringsDescriptor Search(string? search)
	{
		SearchValue = search;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (!string.IsNullOrEmpty(IngestValue))
		{
			writer.WritePropertyName("ingest");
			writer.WriteStringValue(IngestValue);
		}

		if (!string.IsNullOrEmpty(SearchValue))
		{
			writer.WritePropertyName("search");
			writer.WriteStringValue(SearchValue);
		}

		writer.WriteEndObject();
	}
}