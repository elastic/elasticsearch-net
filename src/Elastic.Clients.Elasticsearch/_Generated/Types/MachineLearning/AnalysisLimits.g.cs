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

public sealed partial class AnalysisLimits
{
	/// <summary>
	/// <para>The maximum number of examples stored per category in memory and in the results data store. If you increase this value, more examples are available, however it requires that you have more storage available. If you set this value to 0, no examples are stored. NOTE: The `categorization_examples_limit` applies only to analysis that uses categorization.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("categorization_examples_limit")]
	public long? CategorizationExamplesLimit { get; set; }

	/// <summary>
	/// <para>The approximate maximum amount of memory resources that are required for analytical processing. Once this limit is approached, data pruning becomes more aggressive. Upon exceeding this limit, new entities are not modeled. If the `xpack.ml.max_model_memory_limit` setting has a value greater than 0 and less than 1024mb, that value is used instead of the default. The default value is relatively small to ensure that high resource usage is a conscious decision. If you have jobs that are expected to analyze high cardinality fields, you will likely need to use a higher value. If you specify a number instead of a string, the units are assumed to be MiB. Specifying a string is recommended for clarity. If you specify a byte size unit of `b` or `kb` and the number does not equate to a discrete number of megabytes, it is rounded down to the closest MiB. The minimum valid value is 1 MiB. If you specify a value less than 1 MiB, an error occurs. If you specify a value for the `xpack.ml.max_model_memory_limit` setting, an error occurs when you try to create jobs that have `model_memory_limit` values greater than that setting value.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("model_memory_limit")]
	public string? ModelMemoryLimit { get; set; }
}

public sealed partial class AnalysisLimitsDescriptor : SerializableDescriptor<AnalysisLimitsDescriptor>
{
	internal AnalysisLimitsDescriptor(Action<AnalysisLimitsDescriptor> configure) => configure.Invoke(this);

	public AnalysisLimitsDescriptor() : base()
	{
	}

	private long? CategorizationExamplesLimitValue { get; set; }
	private string? ModelMemoryLimitValue { get; set; }

	/// <summary>
	/// <para>The maximum number of examples stored per category in memory and in the results data store. If you increase this value, more examples are available, however it requires that you have more storage available. If you set this value to 0, no examples are stored. NOTE: The `categorization_examples_limit` applies only to analysis that uses categorization.</para>
	/// </summary>
	public AnalysisLimitsDescriptor CategorizationExamplesLimit(long? categorizationExamplesLimit)
	{
		CategorizationExamplesLimitValue = categorizationExamplesLimit;
		return Self;
	}

	/// <summary>
	/// <para>The approximate maximum amount of memory resources that are required for analytical processing. Once this limit is approached, data pruning becomes more aggressive. Upon exceeding this limit, new entities are not modeled. If the `xpack.ml.max_model_memory_limit` setting has a value greater than 0 and less than 1024mb, that value is used instead of the default. The default value is relatively small to ensure that high resource usage is a conscious decision. If you have jobs that are expected to analyze high cardinality fields, you will likely need to use a higher value. If you specify a number instead of a string, the units are assumed to be MiB. Specifying a string is recommended for clarity. If you specify a byte size unit of `b` or `kb` and the number does not equate to a discrete number of megabytes, it is rounded down to the closest MiB. The minimum valid value is 1 MiB. If you specify a value less than 1 MiB, an error occurs. If you specify a value for the `xpack.ml.max_model_memory_limit` setting, an error occurs when you try to create jobs that have `model_memory_limit` values greater than that setting value.</para>
	/// </summary>
	public AnalysisLimitsDescriptor ModelMemoryLimit(string? modelMemoryLimit)
	{
		ModelMemoryLimitValue = modelMemoryLimit;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (CategorizationExamplesLimitValue.HasValue)
		{
			writer.WritePropertyName("categorization_examples_limit");
			writer.WriteNumberValue(CategorizationExamplesLimitValue.Value);
		}

		if (!string.IsNullOrEmpty(ModelMemoryLimitValue))
		{
			writer.WritePropertyName("model_memory_limit");
			writer.WriteStringValue(ModelMemoryLimitValue);
		}

		writer.WriteEndObject();
	}
}