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

namespace Elastic.Clients.Elasticsearch.Serverless.Aggregations;

public sealed partial class ChildrenAggregation
{
	/// <summary>
	/// <para>
	/// The child type that should be selected.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("type")]
	public string? Type { get; set; }

	public static implicit operator Elastic.Clients.Elasticsearch.Serverless.Aggregations.Aggregation(ChildrenAggregation childrenAggregation) => Elastic.Clients.Elasticsearch.Serverless.Aggregations.Aggregation.Children(childrenAggregation);
}

public sealed partial class ChildrenAggregationDescriptor : SerializableDescriptor<ChildrenAggregationDescriptor>
{
	internal ChildrenAggregationDescriptor(Action<ChildrenAggregationDescriptor> configure) => configure.Invoke(this);

	public ChildrenAggregationDescriptor() : base()
	{
	}

	private string? TypeValue { get; set; }

	/// <summary>
	/// <para>
	/// The child type that should be selected.
	/// </para>
	/// </summary>
	public ChildrenAggregationDescriptor Type(string? type)
	{
		TypeValue = type;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (!string.IsNullOrEmpty(TypeValue))
		{
			writer.WritePropertyName("type");
			writer.WriteStringValue(TypeValue);
		}

		writer.WriteEndObject();
	}
}