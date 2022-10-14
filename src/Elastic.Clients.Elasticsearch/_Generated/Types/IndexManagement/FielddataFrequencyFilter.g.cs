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
public sealed partial class FielddataFrequencyFilter
{
	[JsonInclude]
	[JsonPropertyName("max")]
	public double Max { get; set; }

	[JsonInclude]
	[JsonPropertyName("min")]
	public double Min { get; set; }

	[JsonInclude]
	[JsonPropertyName("min_segment_size")]
	public int MinSegmentSize { get; set; }
}

public sealed partial class FielddataFrequencyFilterDescriptor : SerializableDescriptor<FielddataFrequencyFilterDescriptor>, IBuildableDescriptor<FielddataFrequencyFilter>
{
	internal FielddataFrequencyFilterDescriptor(Action<FielddataFrequencyFilterDescriptor> configure) => configure.Invoke(this);
	public FielddataFrequencyFilterDescriptor() : base()
	{
	}

	private double MaxValue { get; set; }

	private double MinValue { get; set; }

	private int MinSegmentSizeValue { get; set; }

	public FielddataFrequencyFilterDescriptor Max(double max)
	{
		MaxValue = max;
		return Self;
	}

	public FielddataFrequencyFilterDescriptor Min(double min)
	{
		MinValue = min;
		return Self;
	}

	public FielddataFrequencyFilterDescriptor MinSegmentSize(int minSegmentSize)
	{
		MinSegmentSizeValue = minSegmentSize;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		writer.WritePropertyName("max");
		writer.WriteNumberValue(MaxValue);
		writer.WritePropertyName("min");
		writer.WriteNumberValue(MinValue);
		writer.WritePropertyName("min_segment_size");
		writer.WriteNumberValue(MinSegmentSizeValue);
		writer.WriteEndObject();
	}

	FielddataFrequencyFilter IBuildableDescriptor<FielddataFrequencyFilter>.Build() => new()
	{ Max = MaxValue, Min = MinValue, MinSegmentSize = MinSegmentSizeValue };
}