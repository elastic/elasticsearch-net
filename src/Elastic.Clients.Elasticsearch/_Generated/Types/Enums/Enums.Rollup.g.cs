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

using Elastic.Clients.Elasticsearch.Core;
using Elastic.Clients.Elasticsearch.Serialization;
using Elastic.Transport;
using System;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Elastic.Clients.Elasticsearch.Rollup;

[JsonConverter(typeof(IndexingJobStateConverter))]
public enum IndexingJobState
{
	[EnumMember(Value = "stopping")]
	Stopping,
	[EnumMember(Value = "stopped")]
	Stopped,
	[EnumMember(Value = "started")]
	Started,
	[EnumMember(Value = "indexing")]
	Indexing,
	[EnumMember(Value = "aborting")]
	Aborting
}

internal sealed class IndexingJobStateConverter : JsonConverter<IndexingJobState>
{
	public override IndexingJobState Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		var enumString = reader.GetString();
		switch (enumString)
		{
			case "stopping":
				return IndexingJobState.Stopping;
			case "stopped":
				return IndexingJobState.Stopped;
			case "started":
				return IndexingJobState.Started;
			case "indexing":
				return IndexingJobState.Indexing;
			case "aborting":
				return IndexingJobState.Aborting;
		}

		ThrowHelper.ThrowJsonException();
		return default;
	}

	public override void Write(Utf8JsonWriter writer, IndexingJobState value, JsonSerializerOptions options)
	{
		switch (value)
		{
			case IndexingJobState.Stopping:
				writer.WriteStringValue("stopping");
				return;
			case IndexingJobState.Stopped:
				writer.WriteStringValue("stopped");
				return;
			case IndexingJobState.Started:
				writer.WriteStringValue("started");
				return;
			case IndexingJobState.Indexing:
				writer.WriteStringValue("indexing");
				return;
			case IndexingJobState.Aborting:
				writer.WriteStringValue("aborting");
				return;
		}

		writer.WriteNullValue();
	}
}

[JsonConverter(typeof(MetricConverter))]
public enum Metric
{
	[EnumMember(Value = "value_count")]
	ValueCount,
	[EnumMember(Value = "sum")]
	Sum,
	[EnumMember(Value = "min")]
	Min,
	[EnumMember(Value = "max")]
	Max,
	[EnumMember(Value = "avg")]
	Avg
}

internal sealed class MetricConverter : JsonConverter<Metric>
{
	public override Metric Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		var enumString = reader.GetString();
		switch (enumString)
		{
			case "value_count":
				return Metric.ValueCount;
			case "sum":
				return Metric.Sum;
			case "min":
				return Metric.Min;
			case "max":
				return Metric.Max;
			case "avg":
				return Metric.Avg;
		}

		ThrowHelper.ThrowJsonException();
		return default;
	}

	public override void Write(Utf8JsonWriter writer, Metric value, JsonSerializerOptions options)
	{
		switch (value)
		{
			case Metric.ValueCount:
				writer.WriteStringValue("value_count");
				return;
			case Metric.Sum:
				writer.WriteStringValue("sum");
				return;
			case Metric.Min:
				writer.WriteStringValue("min");
				return;
			case Metric.Max:
				writer.WriteStringValue("max");
				return;
			case Metric.Avg:
				writer.WriteStringValue("avg");
				return;
		}

		writer.WriteNullValue();
	}
}