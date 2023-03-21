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

namespace Elastic.Clients.Elasticsearch.IndexManagement;

public sealed partial class SettingsSimilarityDfi
{
	[JsonInclude, JsonPropertyName("independence_measure")]
	public Elastic.Clients.Elasticsearch.DFIIndependenceMeasure IndependenceMeasure { get; set; }

	[JsonInclude]
	[JsonPropertyName("type")]
	public string Type => "DFI";
}

public sealed partial class SettingsSimilarityDfiDescriptor : SerializableDescriptor<SettingsSimilarityDfiDescriptor>
{
	internal SettingsSimilarityDfiDescriptor(Action<SettingsSimilarityDfiDescriptor> configure) => configure.Invoke(this);

	public SettingsSimilarityDfiDescriptor() : base()
	{
	}

	private Elastic.Clients.Elasticsearch.DFIIndependenceMeasure IndependenceMeasureValue { get; set; }

	public SettingsSimilarityDfiDescriptor IndependenceMeasure(Elastic.Clients.Elasticsearch.DFIIndependenceMeasure independenceMeasure)
	{
		IndependenceMeasureValue = independenceMeasure;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		writer.WritePropertyName("independence_measure");
		JsonSerializer.Serialize(writer, IndependenceMeasureValue, options);
		writer.WritePropertyName("type");
		writer.WriteStringValue("DFI");
		writer.WriteEndObject();
	}
}