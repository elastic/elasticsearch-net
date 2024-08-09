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

public sealed partial class MemMlStats
{
	/// <summary>
	/// <para>
	/// Amount of native memory set aside for anomaly detection jobs.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("anomaly_detectors")]
	public Elastic.Clients.Elasticsearch.ByteSize? AnomalyDetectors { get; init; }

	/// <summary>
	/// <para>
	/// Amount of native memory, in bytes, set aside for anomaly detection jobs.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("anomaly_detectors_in_bytes")]
	public int AnomalyDetectorsInBytes { get; init; }

	/// <summary>
	/// <para>
	/// Amount of native memory set aside for data frame analytics jobs.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("data_frame_analytics")]
	public Elastic.Clients.Elasticsearch.ByteSize? DataFrameAnalytics { get; init; }

	/// <summary>
	/// <para>
	/// Amount of native memory, in bytes, set aside for data frame analytics jobs.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("data_frame_analytics_in_bytes")]
	public int DataFrameAnalyticsInBytes { get; init; }

	/// <summary>
	/// <para>
	/// Maximum amount of native memory (separate to the JVM heap) that may be used by machine learning native processes.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("max")]
	public Elastic.Clients.Elasticsearch.ByteSize? Max { get; init; }

	/// <summary>
	/// <para>
	/// Maximum amount of native memory (separate to the JVM heap), in bytes, that may be used by machine learning native processes.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("max_in_bytes")]
	public int MaxInBytes { get; init; }

	/// <summary>
	/// <para>
	/// Amount of native memory set aside for loading machine learning native code shared libraries.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("native_code_overhead")]
	public Elastic.Clients.Elasticsearch.ByteSize? NativeCodeOverhead { get; init; }

	/// <summary>
	/// <para>
	/// Amount of native memory, in bytes, set aside for loading machine learning native code shared libraries.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("native_code_overhead_in_bytes")]
	public int NativeCodeOverheadInBytes { get; init; }

	/// <summary>
	/// <para>
	/// Amount of native memory set aside for trained models that have a PyTorch model_type.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("native_inference")]
	public Elastic.Clients.Elasticsearch.ByteSize? NativeInference { get; init; }

	/// <summary>
	/// <para>
	/// Amount of native memory, in bytes, set aside for trained models that have a PyTorch model_type.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("native_inference_in_bytes")]
	public int NativeInferenceInBytes { get; init; }
}