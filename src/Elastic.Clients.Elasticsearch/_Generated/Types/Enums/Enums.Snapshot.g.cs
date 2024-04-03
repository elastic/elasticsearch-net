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

namespace Elastic.Clients.Elasticsearch.Snapshot;

[JsonConverter(typeof(ShardsStatsStageConverter))]
public enum ShardsStatsStage
{
	/// <summary>
	/// <para>Number of shards in the snapshot that are in the started stage of being stored in the repository.</para>
	/// </summary>
	[EnumMember(Value = "STARTED")]
	Started,
	/// <summary>
	/// <para>Number of shards in the snapshot that are in the initializing stage of being stored in the repository.</para>
	/// </summary>
	[EnumMember(Value = "INIT")]
	Init,
	/// <summary>
	/// <para>Number of shards in the snapshot that are in the finalizing stage of being stored in the repository.</para>
	/// </summary>
	[EnumMember(Value = "FINALIZE")]
	Finalize,
	/// <summary>
	/// <para>Number of shards in the snapshot that were not successfully stored in the repository.</para>
	/// </summary>
	[EnumMember(Value = "FAILURE")]
	Failure,
	/// <summary>
	/// <para>Number of shards in the snapshot that were successfully stored in the repository.</para>
	/// </summary>
	[EnumMember(Value = "DONE")]
	Done
}

internal sealed class ShardsStatsStageConverter : JsonConverter<ShardsStatsStage>
{
	public override ShardsStatsStage Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		var enumString = reader.GetString();
		switch (enumString)
		{
			case "STARTED":
				return ShardsStatsStage.Started;
			case "INIT":
				return ShardsStatsStage.Init;
			case "FINALIZE":
				return ShardsStatsStage.Finalize;
			case "FAILURE":
				return ShardsStatsStage.Failure;
			case "DONE":
				return ShardsStatsStage.Done;
		}

		ThrowHelper.ThrowJsonException();
		return default;
	}

	public override void Write(Utf8JsonWriter writer, ShardsStatsStage value, JsonSerializerOptions options)
	{
		switch (value)
		{
			case ShardsStatsStage.Started:
				writer.WriteStringValue("STARTED");
				return;
			case ShardsStatsStage.Init:
				writer.WriteStringValue("INIT");
				return;
			case ShardsStatsStage.Finalize:
				writer.WriteStringValue("FINALIZE");
				return;
			case ShardsStatsStage.Failure:
				writer.WriteStringValue("FAILURE");
				return;
			case ShardsStatsStage.Done:
				writer.WriteStringValue("DONE");
				return;
		}

		writer.WriteNullValue();
	}
}

[JsonConverter(typeof(SnapshotSortConverter))]
public enum SnapshotSort
{
	[EnumMember(Value = "start_time")]
	StartTime,
	[EnumMember(Value = "shard_count")]
	ShardCount,
	[EnumMember(Value = "repository")]
	Repository,
	[EnumMember(Value = "name")]
	Name,
	[EnumMember(Value = "index_count")]
	IndexCount,
	[EnumMember(Value = "failed_shard_count")]
	FailedShardCount,
	[EnumMember(Value = "duration")]
	Duration
}

internal sealed class SnapshotSortConverter : JsonConverter<SnapshotSort>
{
	public override SnapshotSort Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		var enumString = reader.GetString();
		switch (enumString)
		{
			case "start_time":
				return SnapshotSort.StartTime;
			case "shard_count":
				return SnapshotSort.ShardCount;
			case "repository":
				return SnapshotSort.Repository;
			case "name":
				return SnapshotSort.Name;
			case "index_count":
				return SnapshotSort.IndexCount;
			case "failed_shard_count":
				return SnapshotSort.FailedShardCount;
			case "duration":
				return SnapshotSort.Duration;
		}

		ThrowHelper.ThrowJsonException();
		return default;
	}

	public override void Write(Utf8JsonWriter writer, SnapshotSort value, JsonSerializerOptions options)
	{
		switch (value)
		{
			case SnapshotSort.StartTime:
				writer.WriteStringValue("start_time");
				return;
			case SnapshotSort.ShardCount:
				writer.WriteStringValue("shard_count");
				return;
			case SnapshotSort.Repository:
				writer.WriteStringValue("repository");
				return;
			case SnapshotSort.Name:
				writer.WriteStringValue("name");
				return;
			case SnapshotSort.IndexCount:
				writer.WriteStringValue("index_count");
				return;
			case SnapshotSort.FailedShardCount:
				writer.WriteStringValue("failed_shard_count");
				return;
			case SnapshotSort.Duration:
				writer.WriteStringValue("duration");
				return;
		}

		writer.WriteNullValue();
	}
}