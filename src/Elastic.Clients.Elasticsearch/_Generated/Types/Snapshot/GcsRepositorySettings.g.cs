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

namespace Elastic.Clients.Elasticsearch.Snapshot;

public sealed partial class GcsRepositorySettings
{
	[JsonInclude, JsonPropertyName("application_name")]
	public string? ApplicationName { get; set; }
	[JsonInclude, JsonPropertyName("base_path")]
	public string? BasePath { get; set; }
	[JsonInclude, JsonPropertyName("bucket")]
	public string Bucket { get; set; }
	[JsonInclude, JsonPropertyName("chunk_size")]
	public Elastic.Clients.Elasticsearch.ByteSize? ChunkSize { get; set; }
	[JsonInclude, JsonPropertyName("client")]
	public string? Client { get; set; }
	[JsonInclude, JsonPropertyName("compress")]
	public bool? Compress { get; set; }
	[JsonInclude, JsonPropertyName("max_restore_bytes_per_sec")]
	public Elastic.Clients.Elasticsearch.ByteSize? MaxRestoreBytesPerSec { get; set; }
	[JsonInclude, JsonPropertyName("max_snapshot_bytes_per_sec")]
	public Elastic.Clients.Elasticsearch.ByteSize? MaxSnapshotBytesPerSec { get; set; }
	[JsonInclude, JsonPropertyName("readonly")]
	public bool? Readonly { get; set; }
}

public sealed partial class GcsRepositorySettingsDescriptor : SerializableDescriptor<GcsRepositorySettingsDescriptor>
{
	internal GcsRepositorySettingsDescriptor(Action<GcsRepositorySettingsDescriptor> configure) => configure.Invoke(this);

	public GcsRepositorySettingsDescriptor() : base()
	{
	}

	private string? ApplicationNameValue { get; set; }
	private string? BasePathValue { get; set; }
	private string BucketValue { get; set; }
	private Elastic.Clients.Elasticsearch.ByteSize? ChunkSizeValue { get; set; }
	private string? ClientValue { get; set; }
	private bool? CompressValue { get; set; }
	private Elastic.Clients.Elasticsearch.ByteSize? MaxRestoreBytesPerSecValue { get; set; }
	private Elastic.Clients.Elasticsearch.ByteSize? MaxSnapshotBytesPerSecValue { get; set; }
	private bool? ReadonlyValue { get; set; }

	public GcsRepositorySettingsDescriptor ApplicationName(string? applicationName)
	{
		ApplicationNameValue = applicationName;
		return Self;
	}

	public GcsRepositorySettingsDescriptor BasePath(string? basePath)
	{
		BasePathValue = basePath;
		return Self;
	}

	public GcsRepositorySettingsDescriptor Bucket(string bucket)
	{
		BucketValue = bucket;
		return Self;
	}

	public GcsRepositorySettingsDescriptor ChunkSize(Elastic.Clients.Elasticsearch.ByteSize? chunkSize)
	{
		ChunkSizeValue = chunkSize;
		return Self;
	}

	public GcsRepositorySettingsDescriptor Client(string? client)
	{
		ClientValue = client;
		return Self;
	}

	public GcsRepositorySettingsDescriptor Compress(bool? compress = true)
	{
		CompressValue = compress;
		return Self;
	}

	public GcsRepositorySettingsDescriptor MaxRestoreBytesPerSec(Elastic.Clients.Elasticsearch.ByteSize? maxRestoreBytesPerSec)
	{
		MaxRestoreBytesPerSecValue = maxRestoreBytesPerSec;
		return Self;
	}

	public GcsRepositorySettingsDescriptor MaxSnapshotBytesPerSec(Elastic.Clients.Elasticsearch.ByteSize? maxSnapshotBytesPerSec)
	{
		MaxSnapshotBytesPerSecValue = maxSnapshotBytesPerSec;
		return Self;
	}

	public GcsRepositorySettingsDescriptor Readonly(bool? value = true)
	{
		ReadonlyValue = value;
		return Self;
	}

	protected override void Serialize(Utf8JsonWriter writer, JsonSerializerOptions options, IElasticsearchClientSettings settings)
	{
		writer.WriteStartObject();
		if (!string.IsNullOrEmpty(ApplicationNameValue))
		{
			writer.WritePropertyName("application_name");
			writer.WriteStringValue(ApplicationNameValue);
		}

		if (!string.IsNullOrEmpty(BasePathValue))
		{
			writer.WritePropertyName("base_path");
			writer.WriteStringValue(BasePathValue);
		}

		writer.WritePropertyName("bucket");
		writer.WriteStringValue(BucketValue);
		if (ChunkSizeValue is not null)
		{
			writer.WritePropertyName("chunk_size");
			JsonSerializer.Serialize(writer, ChunkSizeValue, options);
		}

		if (!string.IsNullOrEmpty(ClientValue))
		{
			writer.WritePropertyName("client");
			writer.WriteStringValue(ClientValue);
		}

		if (CompressValue.HasValue)
		{
			writer.WritePropertyName("compress");
			writer.WriteBooleanValue(CompressValue.Value);
		}

		if (MaxRestoreBytesPerSecValue is not null)
		{
			writer.WritePropertyName("max_restore_bytes_per_sec");
			JsonSerializer.Serialize(writer, MaxRestoreBytesPerSecValue, options);
		}

		if (MaxSnapshotBytesPerSecValue is not null)
		{
			writer.WritePropertyName("max_snapshot_bytes_per_sec");
			JsonSerializer.Serialize(writer, MaxSnapshotBytesPerSecValue, options);
		}

		if (ReadonlyValue.HasValue)
		{
			writer.WritePropertyName("readonly");
			writer.WriteBooleanValue(ReadonlyValue.Value);
		}

		writer.WriteEndObject();
	}
}