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

public sealed partial class IndexingPressureMemory
{
	/// <summary>
	/// <para>Number of outstanding bytes that may be consumed by indexing requests. When this limit is reached or exceeded,<br/>the node will reject new coordinating and primary operations. When replica operations consume 1.5x this limit,<br/>the node will reject new replica operations. Defaults to 10% of the heap.</para>
	/// </summary>
	[JsonInclude, JsonPropertyName("limit")]
	public int? Limit { get; set; }
}

public sealed partial class IndexingPressureMemoryDescriptor : SerializableDescriptor<IndexingPressureMemoryDescriptor>
{
	internal IndexingPressureMemoryDescriptor(Action<IndexingPressureMemoryDescriptor> configure) => configure.Invoke(this);

	public IndexingPressureMemoryDescriptor() : base()
	{
	}

	private int? LimitValue { get; set; }

	/// <summary>
	/// <para>Number of outstanding bytes that may be consumed by indexing requests. When this limit is reached or exceeded,<br/>the node will reject new coordinating and primary operations. When replica operations consume 1.5x this limit,<br/>the node will reject new replica operations. Defaults to 10% of the heap.</para>
	/// </summary>
	public IndexingPressureMemoryDescriptor Limit(int? limit)
	{
		LimitValue = limit;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (LimitValue.HasValue)
		{
			writer.WritePropertyName("limit");
			writer.WriteNumberValue(LimitValue.Value);
		}

		writer.WriteEndObject();
	}
}