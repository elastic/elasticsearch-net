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

using System;
using System.Linq;
using Elastic.Clients.Elasticsearch.Serialization;

namespace Elastic.Clients.Elasticsearch.Snapshot;

internal sealed partial class GcsRepositorySettingsConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Snapshot.GcsRepositorySettings>
{
	private static readonly System.Text.Json.JsonEncodedText PropBasePath = System.Text.Json.JsonEncodedText.Encode("base_path");
	private static readonly System.Text.Json.JsonEncodedText PropBucket = System.Text.Json.JsonEncodedText.Encode("bucket");
	private static readonly System.Text.Json.JsonEncodedText PropChunkSize = System.Text.Json.JsonEncodedText.Encode("chunk_size");
	private static readonly System.Text.Json.JsonEncodedText PropClient = System.Text.Json.JsonEncodedText.Encode("client");
	private static readonly System.Text.Json.JsonEncodedText PropCompress = System.Text.Json.JsonEncodedText.Encode("compress");
	private static readonly System.Text.Json.JsonEncodedText PropMaxRestoreBytesPerSec = System.Text.Json.JsonEncodedText.Encode("max_restore_bytes_per_sec");
	private static readonly System.Text.Json.JsonEncodedText PropMaxSnapshotBytesPerSec = System.Text.Json.JsonEncodedText.Encode("max_snapshot_bytes_per_sec");
	private static readonly System.Text.Json.JsonEncodedText PropReadonly = System.Text.Json.JsonEncodedText.Encode("readonly");

	public override Elastic.Clients.Elasticsearch.Snapshot.GcsRepositorySettings Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<string?> propBasePath = default;
		LocalJsonValue<string> propBucket = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.ByteSize?> propChunkSize = default;
		LocalJsonValue<string?> propClient = default;
		LocalJsonValue<bool?> propCompress = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.ByteSize?> propMaxRestoreBytesPerSec = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.ByteSize?> propMaxSnapshotBytesPerSec = default;
		LocalJsonValue<bool?> propReadonly = default;
		while (reader.Read() && reader.TokenType is System.Text.Json.JsonTokenType.PropertyName)
		{
			if (propBasePath.TryReadProperty(ref reader, options, PropBasePath, null))
			{
				continue;
			}

			if (propBucket.TryReadProperty(ref reader, options, PropBucket, null))
			{
				continue;
			}

			if (propChunkSize.TryReadProperty(ref reader, options, PropChunkSize, null))
			{
				continue;
			}

			if (propClient.TryReadProperty(ref reader, options, PropClient, null))
			{
				continue;
			}

			if (propCompress.TryReadProperty(ref reader, options, PropCompress, null))
			{
				continue;
			}

			if (propMaxRestoreBytesPerSec.TryReadProperty(ref reader, options, PropMaxRestoreBytesPerSec, null))
			{
				continue;
			}

			if (propMaxSnapshotBytesPerSec.TryReadProperty(ref reader, options, PropMaxSnapshotBytesPerSec, null))
			{
				continue;
			}

			if (propReadonly.TryReadProperty(ref reader, options, PropReadonly, null))
			{
				continue;
			}

			if (options.UnmappedMemberHandling is System.Text.Json.Serialization.JsonUnmappedMemberHandling.Skip)
			{
				reader.Skip();
				continue;
			}

			throw new System.Text.Json.JsonException($"Unknown JSON property '{reader.GetString()}' for type '{typeToConvert.Name}'.");
		}

		reader.ValidateToken(System.Text.Json.JsonTokenType.EndObject);
		return new Elastic.Clients.Elasticsearch.Snapshot.GcsRepositorySettings(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			BasePath = propBasePath.Value,
			Bucket = propBucket.Value,
			ChunkSize = propChunkSize.Value,
			Client = propClient.Value,
			Compress = propCompress.Value,
			MaxRestoreBytesPerSec = propMaxRestoreBytesPerSec.Value,
			MaxSnapshotBytesPerSec = propMaxSnapshotBytesPerSec.Value,
			Readonly = propReadonly.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Snapshot.GcsRepositorySettings value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropBasePath, value.BasePath, null, null);
		writer.WriteProperty(options, PropBucket, value.Bucket, null, null);
		writer.WriteProperty(options, PropChunkSize, value.ChunkSize, null, null);
		writer.WriteProperty(options, PropClient, value.Client, null, null);
		writer.WriteProperty(options, PropCompress, value.Compress, null, null);
		writer.WriteProperty(options, PropMaxRestoreBytesPerSec, value.MaxRestoreBytesPerSec, null, null);
		writer.WriteProperty(options, PropMaxSnapshotBytesPerSec, value.MaxSnapshotBytesPerSec, null, null);
		writer.WriteProperty(options, PropReadonly, value.Readonly, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Snapshot.GcsRepositorySettingsConverter))]
public sealed partial class GcsRepositorySettings
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public GcsRepositorySettings(string bucket)
	{
		Bucket = bucket;
	}
#if NET7_0_OR_GREATER
	public GcsRepositorySettings()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains additional required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public GcsRepositorySettings()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal GcsRepositorySettings(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	/// <summary>
	/// <para>
	/// The path to the repository data within the bucket.
	/// It defaults to the root of the bucket.
	/// </para>
	/// <para>
	/// NOTE: Don't set <c>base_path</c> when configuring a snapshot repository for Elastic Cloud Enterprise.
	/// Elastic Cloud Enterprise automatically generates the <c>base_path</c> for each deployment so that multiple deployments can share the same bucket.
	/// </para>
	/// </summary>
	public string? BasePath { get; set; }

	/// <summary>
	/// <para>
	/// The name of the bucket to be used for snapshots.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	string Bucket { get; set; }

	/// <summary>
	/// <para>
	/// Big files can be broken down into multiple smaller blobs in the blob store during snapshotting.
	/// It is not recommended to change this value from its default unless there is an explicit reason for limiting the size of blobs in the repository.
	/// Setting a value lower than the default can result in an increased number of API calls to the blob store during snapshot create and restore operations compared to using the default value and thus make both operations slower and more costly.
	/// Specify the chunk size as a byte unit, for example: <c>10MB</c>, <c>5KB</c>, 500B.
	/// The default varies by repository type.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.ByteSize? ChunkSize { get; set; }

	/// <summary>
	/// <para>
	/// The name of the client to use to connect to Google Cloud Storage.
	/// </para>
	/// </summary>
	public string? Client { get; set; }

	/// <summary>
	/// <para>
	/// When set to <c>true</c>, metadata files are stored in compressed format.
	/// This setting doesn't affect index files that are already compressed by default.
	/// </para>
	/// </summary>
	public bool? Compress { get; set; }

	/// <summary>
	/// <para>
	/// The maximum snapshot restore rate per node.
	/// It defaults to unlimited.
	/// Note that restores are also throttled through recovery settings.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.ByteSize? MaxRestoreBytesPerSec { get; set; }

	/// <summary>
	/// <para>
	/// The maximum snapshot creation rate per node.
	/// It defaults to 40mb per second.
	/// Note that if the recovery settings for managed services are set, then it defaults to unlimited, and the rate is additionally throttled through recovery settings.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.ByteSize? MaxSnapshotBytesPerSec { get; set; }

	/// <summary>
	/// <para>
	/// If <c>true</c>, the repository is read-only.
	/// The cluster can retrieve and restore snapshots from the repository but not write to the repository or create snapshots in it.
	/// </para>
	/// <para>
	/// Only a cluster with write access can create snapshots in the repository.
	/// All other clusters connected to the repository should have the <c>readonly</c> parameter set to <c>true</c>.
	/// </para>
	/// <para>
	/// If <c>false</c>, the cluster can write to the repository and create snapshots in it.
	/// </para>
	/// <para>
	/// IMPORTANT: If you register the same snapshot repository with multiple clusters, only one cluster should have write access to the repository.
	/// Having multiple clusters write to the repository at the same time risks corrupting the contents of the repository.
	/// </para>
	/// </summary>
	public bool? Readonly { get; set; }
}

public readonly partial struct GcsRepositorySettingsDescriptor
{
	internal Elastic.Clients.Elasticsearch.Snapshot.GcsRepositorySettings Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public GcsRepositorySettingsDescriptor(Elastic.Clients.Elasticsearch.Snapshot.GcsRepositorySettings instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public GcsRepositorySettingsDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.Snapshot.GcsRepositorySettings(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Snapshot.GcsRepositorySettingsDescriptor(Elastic.Clients.Elasticsearch.Snapshot.GcsRepositorySettings instance) => new Elastic.Clients.Elasticsearch.Snapshot.GcsRepositorySettingsDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Snapshot.GcsRepositorySettings(Elastic.Clients.Elasticsearch.Snapshot.GcsRepositorySettingsDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// The path to the repository data within the bucket.
	/// It defaults to the root of the bucket.
	/// </para>
	/// <para>
	/// NOTE: Don't set <c>base_path</c> when configuring a snapshot repository for Elastic Cloud Enterprise.
	/// Elastic Cloud Enterprise automatically generates the <c>base_path</c> for each deployment so that multiple deployments can share the same bucket.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Snapshot.GcsRepositorySettingsDescriptor BasePath(string? value)
	{
		Instance.BasePath = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The name of the bucket to be used for snapshots.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Snapshot.GcsRepositorySettingsDescriptor Bucket(string value)
	{
		Instance.Bucket = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Big files can be broken down into multiple smaller blobs in the blob store during snapshotting.
	/// It is not recommended to change this value from its default unless there is an explicit reason for limiting the size of blobs in the repository.
	/// Setting a value lower than the default can result in an increased number of API calls to the blob store during snapshot create and restore operations compared to using the default value and thus make both operations slower and more costly.
	/// Specify the chunk size as a byte unit, for example: <c>10MB</c>, <c>5KB</c>, 500B.
	/// The default varies by repository type.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Snapshot.GcsRepositorySettingsDescriptor ChunkSize(Elastic.Clients.Elasticsearch.ByteSize? value)
	{
		Instance.ChunkSize = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// Big files can be broken down into multiple smaller blobs in the blob store during snapshotting.
	/// It is not recommended to change this value from its default unless there is an explicit reason for limiting the size of blobs in the repository.
	/// Setting a value lower than the default can result in an increased number of API calls to the blob store during snapshot create and restore operations compared to using the default value and thus make both operations slower and more costly.
	/// Specify the chunk size as a byte unit, for example: <c>10MB</c>, <c>5KB</c>, 500B.
	/// The default varies by repository type.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Snapshot.GcsRepositorySettingsDescriptor ChunkSize(System.Func<Elastic.Clients.Elasticsearch.ByteSizeBuilder, Elastic.Clients.Elasticsearch.ByteSize> action)
	{
		Instance.ChunkSize = Elastic.Clients.Elasticsearch.ByteSizeBuilder.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// The name of the client to use to connect to Google Cloud Storage.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Snapshot.GcsRepositorySettingsDescriptor Client(string? value)
	{
		Instance.Client = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// When set to <c>true</c>, metadata files are stored in compressed format.
	/// This setting doesn't affect index files that are already compressed by default.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Snapshot.GcsRepositorySettingsDescriptor Compress(bool? value = true)
	{
		Instance.Compress = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The maximum snapshot restore rate per node.
	/// It defaults to unlimited.
	/// Note that restores are also throttled through recovery settings.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Snapshot.GcsRepositorySettingsDescriptor MaxRestoreBytesPerSec(Elastic.Clients.Elasticsearch.ByteSize? value)
	{
		Instance.MaxRestoreBytesPerSec = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The maximum snapshot restore rate per node.
	/// It defaults to unlimited.
	/// Note that restores are also throttled through recovery settings.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Snapshot.GcsRepositorySettingsDescriptor MaxRestoreBytesPerSec(System.Func<Elastic.Clients.Elasticsearch.ByteSizeBuilder, Elastic.Clients.Elasticsearch.ByteSize> action)
	{
		Instance.MaxRestoreBytesPerSec = Elastic.Clients.Elasticsearch.ByteSizeBuilder.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// The maximum snapshot creation rate per node.
	/// It defaults to 40mb per second.
	/// Note that if the recovery settings for managed services are set, then it defaults to unlimited, and the rate is additionally throttled through recovery settings.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Snapshot.GcsRepositorySettingsDescriptor MaxSnapshotBytesPerSec(Elastic.Clients.Elasticsearch.ByteSize? value)
	{
		Instance.MaxSnapshotBytesPerSec = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The maximum snapshot creation rate per node.
	/// It defaults to 40mb per second.
	/// Note that if the recovery settings for managed services are set, then it defaults to unlimited, and the rate is additionally throttled through recovery settings.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Snapshot.GcsRepositorySettingsDescriptor MaxSnapshotBytesPerSec(System.Func<Elastic.Clients.Elasticsearch.ByteSizeBuilder, Elastic.Clients.Elasticsearch.ByteSize> action)
	{
		Instance.MaxSnapshotBytesPerSec = Elastic.Clients.Elasticsearch.ByteSizeBuilder.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// If <c>true</c>, the repository is read-only.
	/// The cluster can retrieve and restore snapshots from the repository but not write to the repository or create snapshots in it.
	/// </para>
	/// <para>
	/// Only a cluster with write access can create snapshots in the repository.
	/// All other clusters connected to the repository should have the <c>readonly</c> parameter set to <c>true</c>.
	/// </para>
	/// <para>
	/// If <c>false</c>, the cluster can write to the repository and create snapshots in it.
	/// </para>
	/// <para>
	/// IMPORTANT: If you register the same snapshot repository with multiple clusters, only one cluster should have write access to the repository.
	/// Having multiple clusters write to the repository at the same time risks corrupting the contents of the repository.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Snapshot.GcsRepositorySettingsDescriptor Readonly(bool? value = true)
	{
		Instance.Readonly = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Snapshot.GcsRepositorySettings Build(System.Action<Elastic.Clients.Elasticsearch.Snapshot.GcsRepositorySettingsDescriptor> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.Snapshot.GcsRepositorySettingsDescriptor(new Elastic.Clients.Elasticsearch.Snapshot.GcsRepositorySettings(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}