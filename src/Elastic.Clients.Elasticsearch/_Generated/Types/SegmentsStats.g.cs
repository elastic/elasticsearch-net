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
namespace Elastic.Clients.Elasticsearch;
public sealed partial class SegmentsStats
{
	[JsonInclude]
	[JsonPropertyName("count")]
	public int Count { get; init; }

	[JsonInclude]
	[JsonPropertyName("doc_values_memory")]
	public Elastic.Clients.Elasticsearch.ByteSize? DocValuesMemory { get; init; }

	[JsonInclude]
	[JsonPropertyName("doc_values_memory_in_bytes")]
	public int DocValuesMemoryInBytes { get; init; }

	[JsonInclude]
	[JsonPropertyName("file_sizes")]
	public Dictionary<string, Elastic.Clients.Elasticsearch.IndexManagement.ShardFileSizeInfo> FileSizes { get; init; }

	[JsonInclude]
	[JsonPropertyName("fixed_bit_set")]
	public Elastic.Clients.Elasticsearch.ByteSize? FixedBitSet { get; init; }

	[JsonInclude]
	[JsonPropertyName("fixed_bit_set_memory_in_bytes")]
	public int FixedBitSetMemoryInBytes { get; init; }

	[JsonInclude]
	[JsonPropertyName("index_writer_max_memory_in_bytes")]
	public int? IndexWriterMaxMemoryInBytes { get; init; }

	[JsonInclude]
	[JsonPropertyName("index_writer_memory")]
	public Elastic.Clients.Elasticsearch.ByteSize? IndexWriterMemory { get; init; }

	[JsonInclude]
	[JsonPropertyName("index_writer_memory_in_bytes")]
	public int IndexWriterMemoryInBytes { get; init; }

	[JsonInclude]
	[JsonPropertyName("max_unsafe_auto_id_timestamp")]
	public long MaxUnsafeAutoIdTimestamp { get; init; }

	[JsonInclude]
	[JsonPropertyName("memory")]
	public Elastic.Clients.Elasticsearch.ByteSize? Memory { get; init; }

	[JsonInclude]
	[JsonPropertyName("memory_in_bytes")]
	public int MemoryInBytes { get; init; }

	[JsonInclude]
	[JsonPropertyName("norms_memory")]
	public Elastic.Clients.Elasticsearch.ByteSize? NormsMemory { get; init; }

	[JsonInclude]
	[JsonPropertyName("norms_memory_in_bytes")]
	public int NormsMemoryInBytes { get; init; }

	[JsonInclude]
	[JsonPropertyName("points_memory")]
	public Elastic.Clients.Elasticsearch.ByteSize? PointsMemory { get; init; }

	[JsonInclude]
	[JsonPropertyName("points_memory_in_bytes")]
	public int PointsMemoryInBytes { get; init; }

	[JsonInclude]
	[JsonPropertyName("stored_fields_memory_in_bytes")]
	public int StoredFieldsMemoryInBytes { get; init; }

	[JsonInclude]
	[JsonPropertyName("stored_memory")]
	public Elastic.Clients.Elasticsearch.ByteSize? StoredMemory { get; init; }

	[JsonInclude]
	[JsonPropertyName("term_vectors_memory_in_bytes")]
	public int TermVectorsMemoryInBytes { get; init; }

	[JsonInclude]
	[JsonPropertyName("term_vectory_memory")]
	public Elastic.Clients.Elasticsearch.ByteSize? TermVectoryMemory { get; init; }

	[JsonInclude]
	[JsonPropertyName("terms_memory")]
	public Elastic.Clients.Elasticsearch.ByteSize? TermsMemory { get; init; }

	[JsonInclude]
	[JsonPropertyName("terms_memory_in_bytes")]
	public int TermsMemoryInBytes { get; init; }

	[JsonInclude]
	[JsonPropertyName("version_map_memory")]
	public Elastic.Clients.Elasticsearch.ByteSize? VersionMapMemory { get; init; }

	[JsonInclude]
	[JsonPropertyName("version_map_memory_in_bytes")]
	public int VersionMapMemoryInBytes { get; init; }
}