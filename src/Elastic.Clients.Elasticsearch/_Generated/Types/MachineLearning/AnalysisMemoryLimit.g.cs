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

public sealed partial class AnalysisMemoryLimit
{
	/// <summary>
	/// <para>
	/// Limits can be applied for the resources required to hold the mathematical models in memory. These limits are approximate and can be set per job. They do not control the memory used by other processes, for example the Elasticsearch Java processes.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("model_memory_limit")]
	public string ModelMemoryLimit { get; set; }
}

public sealed partial class AnalysisMemoryLimitDescriptor : SerializableDescriptor<AnalysisMemoryLimitDescriptor>
{
	internal AnalysisMemoryLimitDescriptor(Action<AnalysisMemoryLimitDescriptor> configure) => configure.Invoke(this);

	public AnalysisMemoryLimitDescriptor() : base()
	{
	}

	private string ModelMemoryLimitValue { get; set; }

	/// <summary>
	/// <para>
	/// Limits can be applied for the resources required to hold the mathematical models in memory. These limits are approximate and can be set per job. They do not control the memory used by other processes, for example the Elasticsearch Java processes.
	/// </para>
	/// </summary>
	public AnalysisMemoryLimitDescriptor ModelMemoryLimit(string modelMemoryLimit)
	{
		ModelMemoryLimitValue = modelMemoryLimit;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		writer.WritePropertyName("model_memory_limit");
		writer.WriteStringValue(ModelMemoryLimitValue);
		writer.WriteEndObject();
	}
}