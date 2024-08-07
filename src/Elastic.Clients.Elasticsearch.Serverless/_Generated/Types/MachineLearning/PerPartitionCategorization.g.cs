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

namespace Elastic.Clients.Elasticsearch.Serverless.MachineLearning;

public sealed partial class PerPartitionCategorization
{
	/// <summary>
	/// <para>
	/// To enable this setting, you must also set the <c>partition_field_name</c> property to the same value in every detector that uses the keyword <c>mlcategory</c>. Otherwise, job creation fails.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("enabled")]
	public bool? Enabled { get; set; }

	/// <summary>
	/// <para>
	/// This setting can be set to true only if per-partition categorization is enabled. If true, both categorization and subsequent anomaly detection stops for partitions where the categorization status changes to warn. This setting makes it viable to have a job where it is expected that categorization works well for some partitions but not others; you do not pay the cost of bad categorization forever in the partitions where it works badly.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("stop_on_warn")]
	public bool? StopOnWarn { get; set; }
}

public sealed partial class PerPartitionCategorizationDescriptor : SerializableDescriptor<PerPartitionCategorizationDescriptor>
{
	internal PerPartitionCategorizationDescriptor(Action<PerPartitionCategorizationDescriptor> configure) => configure.Invoke(this);

	public PerPartitionCategorizationDescriptor() : base()
	{
	}

	private bool? EnabledValue { get; set; }
	private bool? StopOnWarnValue { get; set; }

	/// <summary>
	/// <para>
	/// To enable this setting, you must also set the <c>partition_field_name</c> property to the same value in every detector that uses the keyword <c>mlcategory</c>. Otherwise, job creation fails.
	/// </para>
	/// </summary>
	public PerPartitionCategorizationDescriptor Enabled(bool? enabled = true)
	{
		EnabledValue = enabled;
		return Self;
	}

	/// <summary>
	/// <para>
	/// This setting can be set to true only if per-partition categorization is enabled. If true, both categorization and subsequent anomaly detection stops for partitions where the categorization status changes to warn. This setting makes it viable to have a job where it is expected that categorization works well for some partitions but not others; you do not pay the cost of bad categorization forever in the partitions where it works badly.
	/// </para>
	/// </summary>
	public PerPartitionCategorizationDescriptor StopOnWarn(bool? stopOnWarn = true)
	{
		StopOnWarnValue = stopOnWarn;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (EnabledValue.HasValue)
		{
			writer.WritePropertyName("enabled");
			writer.WriteBooleanValue(EnabledValue.Value);
		}

		if (StopOnWarnValue.HasValue)
		{
			writer.WritePropertyName("stop_on_warn");
			writer.WriteBooleanValue(StopOnWarnValue.Value);
		}

		writer.WriteEndObject();
	}
}