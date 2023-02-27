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
namespace Elastic.Clients.Elasticsearch.IndexManagement;
public sealed partial class SettingsSimilarityLmj
{
	[JsonInclude, JsonPropertyName("lambda")]
	public double Lambda { get; set; }

	[JsonInclude]
	[JsonPropertyName("type")]
	public string Type => "LMJelinekMercer";
}

public sealed partial class SettingsSimilarityLmjDescriptor : SerializableDescriptor<SettingsSimilarityLmjDescriptor>
{
	internal SettingsSimilarityLmjDescriptor(Action<SettingsSimilarityLmjDescriptor> configure) => configure.Invoke(this);
	public SettingsSimilarityLmjDescriptor() : base()
	{
	}

	private double LambdaValue { get; set; }

	public SettingsSimilarityLmjDescriptor Lambda(double lambda)
	{
		LambdaValue = lambda;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		writer.WritePropertyName("lambda");
		writer.WriteNumberValue(LambdaValue);
		writer.WritePropertyName("type");
		writer.WriteStringValue("LMJelinekMercer");
		writer.WriteEndObject();
	}
}