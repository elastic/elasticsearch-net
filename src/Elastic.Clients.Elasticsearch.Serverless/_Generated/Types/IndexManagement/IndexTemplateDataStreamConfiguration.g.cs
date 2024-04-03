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

namespace Elastic.Clients.Elasticsearch.Serverless.IndexManagement;

public sealed partial class IndexTemplateDataStreamConfiguration
{
	/// <summary>
	/// <para>If true, the data stream supports custom routing.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("allow_custom_routing")]
	public bool? AllowCustomRouting { get; set; }

	/// <summary>
	/// <para>If true, the data stream is hidden.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("hidden")]
	public bool? Hidden { get; set; }
}

public sealed partial class IndexTemplateDataStreamConfigurationDescriptor : SerializableDescriptor<IndexTemplateDataStreamConfigurationDescriptor>
{
	internal IndexTemplateDataStreamConfigurationDescriptor(Action<IndexTemplateDataStreamConfigurationDescriptor> configure) => configure.Invoke(this);

	public IndexTemplateDataStreamConfigurationDescriptor() : base()
	{
	}

	private bool? AllowCustomRoutingValue { get; set; }
	private bool? HiddenValue { get; set; }

	/// <summary>
	/// <para>If true, the data stream supports custom routing.</para>
	/// </summary>
	public IndexTemplateDataStreamConfigurationDescriptor AllowCustomRouting(bool? allowCustomRouting = true)
	{
		AllowCustomRoutingValue = allowCustomRouting;
		return Self;
	}

	/// <summary>
	/// <para>If true, the data stream is hidden.</para>
	/// </summary>
	public IndexTemplateDataStreamConfigurationDescriptor Hidden(bool? hidden = true)
	{
		HiddenValue = hidden;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (AllowCustomRoutingValue.HasValue)
		{
			writer.WritePropertyName("allow_custom_routing");
			writer.WriteBooleanValue(AllowCustomRoutingValue.Value);
		}

		if (HiddenValue.HasValue)
		{
			writer.WritePropertyName("hidden");
			writer.WriteBooleanValue(HiddenValue.Value);
		}

		writer.WriteEndObject();
	}
}