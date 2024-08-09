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

namespace Elastic.Clients.Elasticsearch.Serverless.Nodes;

public sealed partial class DataPathStats
{
	/// <summary>
	/// <para>
	/// Total amount of disk space available to this Java virtual machine on this file store.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("available")]
	public string? Available { get; init; }

	/// <summary>
	/// <para>
	/// Total number of bytes available to this Java virtual machine on this file store.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("available_in_bytes")]
	public long? AvailableInBytes { get; init; }
	[JsonInclude, JsonPropertyName("disk_queue")]
	public string? DiskQueue { get; init; }
	[JsonInclude, JsonPropertyName("disk_reads")]
	public long? DiskReads { get; init; }
	[JsonInclude, JsonPropertyName("disk_read_size")]
	public string? DiskReadSize { get; init; }
	[JsonInclude, JsonPropertyName("disk_read_size_in_bytes")]
	public long? DiskReadSizeInBytes { get; init; }
	[JsonInclude, JsonPropertyName("disk_writes")]
	public long? DiskWrites { get; init; }
	[JsonInclude, JsonPropertyName("disk_write_size")]
	public string? DiskWriteSize { get; init; }
	[JsonInclude, JsonPropertyName("disk_write_size_in_bytes")]
	public long? DiskWriteSizeInBytes { get; init; }

	/// <summary>
	/// <para>
	/// Total amount of unallocated disk space in the file store.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("free")]
	public string? Free { get; init; }

	/// <summary>
	/// <para>
	/// Total number of unallocated bytes in the file store.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("free_in_bytes")]
	public long? FreeInBytes { get; init; }

	/// <summary>
	/// <para>
	/// Mount point of the file store (for example: <c>/dev/sda2</c>).
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("mount")]
	public string? Mount { get; init; }

	/// <summary>
	/// <para>
	/// Path to the file store.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("path")]
	public string? Path { get; init; }

	/// <summary>
	/// <para>
	/// Total size of the file store.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("total")]
	public string? Total { get; init; }

	/// <summary>
	/// <para>
	/// Total size of the file store in bytes.
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("total_in_bytes")]
	public long? TotalInBytes { get; init; }

	/// <summary>
	/// <para>
	/// Type of the file store (ex: ext4).
	/// </para>
	/// </summary>
	[JsonInclude, JsonPropertyName("type")]
	public string? Type { get; init; }
}