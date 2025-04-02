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

internal sealed partial class S3RepositorySettingsConverter : System.Text.Json.Serialization.JsonConverter<Elastic.Clients.Elasticsearch.Snapshot.S3RepositorySettings>
{
	private static readonly System.Text.Json.JsonEncodedText PropBasePath = System.Text.Json.JsonEncodedText.Encode("base_path");
	private static readonly System.Text.Json.JsonEncodedText PropBucket = System.Text.Json.JsonEncodedText.Encode("bucket");
	private static readonly System.Text.Json.JsonEncodedText PropBufferSize = System.Text.Json.JsonEncodedText.Encode("buffer_size");
	private static readonly System.Text.Json.JsonEncodedText PropCannedAcl = System.Text.Json.JsonEncodedText.Encode("canned_acl");
	private static readonly System.Text.Json.JsonEncodedText PropChunkSize = System.Text.Json.JsonEncodedText.Encode("chunk_size");
	private static readonly System.Text.Json.JsonEncodedText PropClient = System.Text.Json.JsonEncodedText.Encode("client");
	private static readonly System.Text.Json.JsonEncodedText PropCompress = System.Text.Json.JsonEncodedText.Encode("compress");
	private static readonly System.Text.Json.JsonEncodedText PropDeleteObjectsMaxSize = System.Text.Json.JsonEncodedText.Encode("delete_objects_max_size");
	private static readonly System.Text.Json.JsonEncodedText PropGetRegisterRetryDelay = System.Text.Json.JsonEncodedText.Encode("get_register_retry_delay");
	private static readonly System.Text.Json.JsonEncodedText PropMaxMultipartParts = System.Text.Json.JsonEncodedText.Encode("max_multipart_parts");
	private static readonly System.Text.Json.JsonEncodedText PropMaxMultipartUploadCleanupSize = System.Text.Json.JsonEncodedText.Encode("max_multipart_upload_cleanup_size");
	private static readonly System.Text.Json.JsonEncodedText PropMaxRestoreBytesPerSec = System.Text.Json.JsonEncodedText.Encode("max_restore_bytes_per_sec");
	private static readonly System.Text.Json.JsonEncodedText PropMaxSnapshotBytesPerSec = System.Text.Json.JsonEncodedText.Encode("max_snapshot_bytes_per_sec");
	private static readonly System.Text.Json.JsonEncodedText PropReadonly = System.Text.Json.JsonEncodedText.Encode("readonly");
	private static readonly System.Text.Json.JsonEncodedText PropServerSideEncryption = System.Text.Json.JsonEncodedText.Encode("server_side_encryption");
	private static readonly System.Text.Json.JsonEncodedText PropStorageClass = System.Text.Json.JsonEncodedText.Encode("storage_class");
	private static readonly System.Text.Json.JsonEncodedText PropThrottledDeleteRetryDelayIncrement = System.Text.Json.JsonEncodedText.Encode("throttled_delete_retry.delay_increment");
	private static readonly System.Text.Json.JsonEncodedText PropThrottledDeleteRetryMaximumDelay = System.Text.Json.JsonEncodedText.Encode("throttled_delete_retry.maximum_delay");
	private static readonly System.Text.Json.JsonEncodedText PropThrottledDeleteRetryMaximumNumberOfRetries = System.Text.Json.JsonEncodedText.Encode("throttled_delete_retry.maximum_number_of_retries");

	public override Elastic.Clients.Elasticsearch.Snapshot.S3RepositorySettings Read(ref System.Text.Json.Utf8JsonReader reader, System.Type typeToConvert, System.Text.Json.JsonSerializerOptions options)
	{
		reader.ValidateToken(System.Text.Json.JsonTokenType.StartObject);
		LocalJsonValue<string?> propBasePath = default;
		LocalJsonValue<string> propBucket = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.ByteSize?> propBufferSize = default;
		LocalJsonValue<string?> propCannedAcl = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.ByteSize?> propChunkSize = default;
		LocalJsonValue<string?> propClient = default;
		LocalJsonValue<bool?> propCompress = default;
		LocalJsonValue<int?> propDeleteObjectsMaxSize = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Duration?> propGetRegisterRetryDelay = default;
		LocalJsonValue<int?> propMaxMultipartParts = default;
		LocalJsonValue<int?> propMaxMultipartUploadCleanupSize = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.ByteSize?> propMaxRestoreBytesPerSec = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.ByteSize?> propMaxSnapshotBytesPerSec = default;
		LocalJsonValue<bool?> propReadonly = default;
		LocalJsonValue<bool?> propServerSideEncryption = default;
		LocalJsonValue<string?> propStorageClass = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Duration?> propThrottledDeleteRetryDelayIncrement = default;
		LocalJsonValue<Elastic.Clients.Elasticsearch.Duration?> propThrottledDeleteRetryMaximumDelay = default;
		LocalJsonValue<int?> propThrottledDeleteRetryMaximumNumberOfRetries = default;
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

			if (propBufferSize.TryReadProperty(ref reader, options, PropBufferSize, null))
			{
				continue;
			}

			if (propCannedAcl.TryReadProperty(ref reader, options, PropCannedAcl, null))
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

			if (propDeleteObjectsMaxSize.TryReadProperty(ref reader, options, PropDeleteObjectsMaxSize, null))
			{
				continue;
			}

			if (propGetRegisterRetryDelay.TryReadProperty(ref reader, options, PropGetRegisterRetryDelay, null))
			{
				continue;
			}

			if (propMaxMultipartParts.TryReadProperty(ref reader, options, PropMaxMultipartParts, null))
			{
				continue;
			}

			if (propMaxMultipartUploadCleanupSize.TryReadProperty(ref reader, options, PropMaxMultipartUploadCleanupSize, null))
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

			if (propServerSideEncryption.TryReadProperty(ref reader, options, PropServerSideEncryption, null))
			{
				continue;
			}

			if (propStorageClass.TryReadProperty(ref reader, options, PropStorageClass, null))
			{
				continue;
			}

			if (propThrottledDeleteRetryDelayIncrement.TryReadProperty(ref reader, options, PropThrottledDeleteRetryDelayIncrement, null))
			{
				continue;
			}

			if (propThrottledDeleteRetryMaximumDelay.TryReadProperty(ref reader, options, PropThrottledDeleteRetryMaximumDelay, null))
			{
				continue;
			}

			if (propThrottledDeleteRetryMaximumNumberOfRetries.TryReadProperty(ref reader, options, PropThrottledDeleteRetryMaximumNumberOfRetries, null))
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
		return new Elastic.Clients.Elasticsearch.Snapshot.S3RepositorySettings(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance)
		{
			BasePath = propBasePath.Value,
			Bucket = propBucket.Value,
			BufferSize = propBufferSize.Value,
			CannedAcl = propCannedAcl.Value,
			ChunkSize = propChunkSize.Value,
			Client = propClient.Value,
			Compress = propCompress.Value,
			DeleteObjectsMaxSize = propDeleteObjectsMaxSize.Value,
			GetRegisterRetryDelay = propGetRegisterRetryDelay.Value,
			MaxMultipartParts = propMaxMultipartParts.Value,
			MaxMultipartUploadCleanupSize = propMaxMultipartUploadCleanupSize.Value,
			MaxRestoreBytesPerSec = propMaxRestoreBytesPerSec.Value,
			MaxSnapshotBytesPerSec = propMaxSnapshotBytesPerSec.Value,
			Readonly = propReadonly.Value,
			ServerSideEncryption = propServerSideEncryption.Value,
			StorageClass = propStorageClass.Value,
			ThrottledDeleteRetryDelayIncrement = propThrottledDeleteRetryDelayIncrement.Value,
			ThrottledDeleteRetryMaximumDelay = propThrottledDeleteRetryMaximumDelay.Value,
			ThrottledDeleteRetryMaximumNumberOfRetries = propThrottledDeleteRetryMaximumNumberOfRetries.Value
		};
	}

	public override void Write(System.Text.Json.Utf8JsonWriter writer, Elastic.Clients.Elasticsearch.Snapshot.S3RepositorySettings value, System.Text.Json.JsonSerializerOptions options)
	{
		writer.WriteStartObject();
		writer.WriteProperty(options, PropBasePath, value.BasePath, null, null);
		writer.WriteProperty(options, PropBucket, value.Bucket, null, null);
		writer.WriteProperty(options, PropBufferSize, value.BufferSize, null, null);
		writer.WriteProperty(options, PropCannedAcl, value.CannedAcl, null, null);
		writer.WriteProperty(options, PropChunkSize, value.ChunkSize, null, null);
		writer.WriteProperty(options, PropClient, value.Client, null, null);
		writer.WriteProperty(options, PropCompress, value.Compress, null, null);
		writer.WriteProperty(options, PropDeleteObjectsMaxSize, value.DeleteObjectsMaxSize, null, null);
		writer.WriteProperty(options, PropGetRegisterRetryDelay, value.GetRegisterRetryDelay, null, null);
		writer.WriteProperty(options, PropMaxMultipartParts, value.MaxMultipartParts, null, null);
		writer.WriteProperty(options, PropMaxMultipartUploadCleanupSize, value.MaxMultipartUploadCleanupSize, null, null);
		writer.WriteProperty(options, PropMaxRestoreBytesPerSec, value.MaxRestoreBytesPerSec, null, null);
		writer.WriteProperty(options, PropMaxSnapshotBytesPerSec, value.MaxSnapshotBytesPerSec, null, null);
		writer.WriteProperty(options, PropReadonly, value.Readonly, null, null);
		writer.WriteProperty(options, PropServerSideEncryption, value.ServerSideEncryption, null, null);
		writer.WriteProperty(options, PropStorageClass, value.StorageClass, null, null);
		writer.WriteProperty(options, PropThrottledDeleteRetryDelayIncrement, value.ThrottledDeleteRetryDelayIncrement, null, null);
		writer.WriteProperty(options, PropThrottledDeleteRetryMaximumDelay, value.ThrottledDeleteRetryMaximumDelay, null, null);
		writer.WriteProperty(options, PropThrottledDeleteRetryMaximumNumberOfRetries, value.ThrottledDeleteRetryMaximumNumberOfRetries, null, null);
		writer.WriteEndObject();
	}
}

[System.Text.Json.Serialization.JsonConverter(typeof(Elastic.Clients.Elasticsearch.Snapshot.S3RepositorySettingsConverter))]
public sealed partial class S3RepositorySettings
{
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public S3RepositorySettings(string bucket)
	{
		Bucket = bucket;
	}
#if NET7_0_OR_GREATER
	public S3RepositorySettings()
	{
	}
#endif
#if !NET7_0_OR_GREATER
	[System.Obsolete("The type contains additional required properties that must be initialized. Please use an alternative constructor to ensure all required values are properly set.")]
	public S3RepositorySettings()
	{
	}
#endif
	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	internal S3RepositorySettings(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel sentinel)
	{
		_ = sentinel;
	}

	/// <summary>
	/// <para>
	/// The path to the repository data within its bucket.
	/// It defaults to an empty string, meaning that the repository is at the root of the bucket.
	/// The value of this setting should not start or end with a forward slash (<c>/</c>).
	/// </para>
	/// <para>
	/// NOTE: Don't set base_path when configuring a snapshot repository for Elastic Cloud Enterprise.
	/// Elastic Cloud Enterprise automatically generates the <c>base_path</c> for each deployment so that multiple deployments may share the same bucket.
	/// </para>
	/// </summary>
	public string? BasePath { get; set; }

	/// <summary>
	/// <para>
	/// The name of the S3 bucket to use for snapshots.
	/// The bucket name must adhere to Amazon's S3 bucket naming rules.
	/// </para>
	/// </summary>
	public
#if NET7_0_OR_GREATER
	required
#endif
	string Bucket { get; set; }

	/// <summary>
	/// <para>
	/// The minimum threshold below which the chunk is uploaded using a single request.
	/// Beyond this threshold, the S3 repository will use the AWS Multipart Upload API to split the chunk into several parts, each of <c>buffer_size</c> length, and to upload each part in its own request.
	/// Note that setting a buffer size lower than 5mb is not allowed since it will prevent the use of the Multipart API and may result in upload errors.
	/// It is also not possible to set a buffer size greater than 5gb as it is the maximum upload size allowed by S3.
	/// Defaults to <c>100mb</c> or 5% of JVM heap, whichever is smaller.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.ByteSize? BufferSize { get; set; }

	/// <summary>
	/// <para>
	/// The S3 repository supports all S3 canned ACLs: <c>private</c>, <c>public-read</c>, <c>public-read-write</c>, <c>authenticated-read</c>, <c>log-delivery-write</c>, <c>bucket-owner-read</c>, <c>bucket-owner-full-control</c>.
	/// You could specify a canned ACL using the <c>canned_acl</c> setting.
	/// When the S3 repository creates buckets and objects, it adds the canned ACL into the buckets and objects.
	/// </para>
	/// </summary>
	public string? CannedAcl { get; set; }

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
	/// The name of the S3 client to use to connect to S3.
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
	/// The maxmimum batch size, between 1 and 1000, used for <c>DeleteObjects</c> requests.
	/// Defaults to 1000 which is the maximum number supported by the  AWS DeleteObjects API.
	/// </para>
	/// </summary>
	public int? DeleteObjectsMaxSize { get; set; }

	/// <summary>
	/// <para>
	/// The time to wait before trying again if an attempt to read a linearizable register fails.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Duration? GetRegisterRetryDelay { get; set; }

	/// <summary>
	/// <para>
	/// The maximum number of parts that Elasticsearch will write during a multipart upload of a single object.
	/// Files which are larger than <c>buffer_size × max_multipart_parts</c> will be chunked into several smaller objects.
	/// Elasticsearch may also split a file across multiple objects to satisfy other constraints such as the <c>chunk_size</c> limit.
	/// Defaults to <c>10000</c> which is the maximum number of parts in a multipart upload in AWS S3.
	/// </para>
	/// </summary>
	public int? MaxMultipartParts { get; set; }

	/// <summary>
	/// <para>
	/// The maximum number of possibly-dangling multipart uploads to clean up in each batch of snapshot deletions.
	/// Defaults to 1000 which is the maximum number supported by the AWS ListMultipartUploads API.
	/// If set to <c>0</c>, Elasticsearch will not attempt to clean up dangling multipart uploads.
	/// </para>
	/// </summary>
	public int? MaxMultipartUploadCleanupSize { get; set; }

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
	/// If true, the repository is read-only.
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

	/// <summary>
	/// <para>
	/// When set to <c>true</c>, files are encrypted on server side using an AES256 algorithm.
	/// </para>
	/// </summary>
	public bool? ServerSideEncryption { get; set; }

	/// <summary>
	/// <para>
	/// The S3 storage class for objects written to the repository.
	/// Values may be <c>standard</c>, <c>reduced_redundancy</c>, <c>standard_ia</c>, <c>onezone_ia</c>, and <c>intelligent_tiering</c>.
	/// </para>
	/// </summary>
	public string? StorageClass { get; set; }

	/// <summary>
	/// <para>
	/// The delay before the first retry and the amount the delay is incremented by on each subsequent retry.
	/// The default is 50ms and the minimum is 0ms.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Duration? ThrottledDeleteRetryDelayIncrement { get; set; }

	/// <summary>
	/// <para>
	/// The upper bound on how long the delays between retries will grow to.
	/// The default is 5s and the minimum is 0ms.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Duration? ThrottledDeleteRetryMaximumDelay { get; set; }

	/// <summary>
	/// <para>
	/// The number times to retry a throttled snapshot deletion.
	/// The default is 10 and the minimum value is 0 which will disable retries altogether.
	/// Note that if retries are enabled in the Azure client, each of these retries comprises that many client-level retries.
	/// </para>
	/// </summary>
	public int? ThrottledDeleteRetryMaximumNumberOfRetries { get; set; }
}

public readonly partial struct S3RepositorySettingsDescriptor
{
	internal Elastic.Clients.Elasticsearch.Snapshot.S3RepositorySettings Instance { get; init; }

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public S3RepositorySettingsDescriptor(Elastic.Clients.Elasticsearch.Snapshot.S3RepositorySettings instance)
	{
		Instance = instance;
	}

	[System.Diagnostics.CodeAnalysis.SetsRequiredMembers]
	public S3RepositorySettingsDescriptor()
	{
		Instance = new Elastic.Clients.Elasticsearch.Snapshot.S3RepositorySettings(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance);
	}

	public static explicit operator Elastic.Clients.Elasticsearch.Snapshot.S3RepositorySettingsDescriptor(Elastic.Clients.Elasticsearch.Snapshot.S3RepositorySettings instance) => new Elastic.Clients.Elasticsearch.Snapshot.S3RepositorySettingsDescriptor(instance);
	public static implicit operator Elastic.Clients.Elasticsearch.Snapshot.S3RepositorySettings(Elastic.Clients.Elasticsearch.Snapshot.S3RepositorySettingsDescriptor descriptor) => descriptor.Instance;

	/// <summary>
	/// <para>
	/// The path to the repository data within its bucket.
	/// It defaults to an empty string, meaning that the repository is at the root of the bucket.
	/// The value of this setting should not start or end with a forward slash (<c>/</c>).
	/// </para>
	/// <para>
	/// NOTE: Don't set base_path when configuring a snapshot repository for Elastic Cloud Enterprise.
	/// Elastic Cloud Enterprise automatically generates the <c>base_path</c> for each deployment so that multiple deployments may share the same bucket.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Snapshot.S3RepositorySettingsDescriptor BasePath(string? value)
	{
		Instance.BasePath = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The name of the S3 bucket to use for snapshots.
	/// The bucket name must adhere to Amazon's S3 bucket naming rules.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Snapshot.S3RepositorySettingsDescriptor Bucket(string value)
	{
		Instance.Bucket = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The minimum threshold below which the chunk is uploaded using a single request.
	/// Beyond this threshold, the S3 repository will use the AWS Multipart Upload API to split the chunk into several parts, each of <c>buffer_size</c> length, and to upload each part in its own request.
	/// Note that setting a buffer size lower than 5mb is not allowed since it will prevent the use of the Multipart API and may result in upload errors.
	/// It is also not possible to set a buffer size greater than 5gb as it is the maximum upload size allowed by S3.
	/// Defaults to <c>100mb</c> or 5% of JVM heap, whichever is smaller.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Snapshot.S3RepositorySettingsDescriptor BufferSize(Elastic.Clients.Elasticsearch.ByteSize? value)
	{
		Instance.BufferSize = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The minimum threshold below which the chunk is uploaded using a single request.
	/// Beyond this threshold, the S3 repository will use the AWS Multipart Upload API to split the chunk into several parts, each of <c>buffer_size</c> length, and to upload each part in its own request.
	/// Note that setting a buffer size lower than 5mb is not allowed since it will prevent the use of the Multipart API and may result in upload errors.
	/// It is also not possible to set a buffer size greater than 5gb as it is the maximum upload size allowed by S3.
	/// Defaults to <c>100mb</c> or 5% of JVM heap, whichever is smaller.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Snapshot.S3RepositorySettingsDescriptor BufferSize(System.Func<Elastic.Clients.Elasticsearch.ByteSizeBuilder, Elastic.Clients.Elasticsearch.ByteSize> action)
	{
		Instance.BufferSize = Elastic.Clients.Elasticsearch.ByteSizeBuilder.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// The S3 repository supports all S3 canned ACLs: <c>private</c>, <c>public-read</c>, <c>public-read-write</c>, <c>authenticated-read</c>, <c>log-delivery-write</c>, <c>bucket-owner-read</c>, <c>bucket-owner-full-control</c>.
	/// You could specify a canned ACL using the <c>canned_acl</c> setting.
	/// When the S3 repository creates buckets and objects, it adds the canned ACL into the buckets and objects.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Snapshot.S3RepositorySettingsDescriptor CannedAcl(string? value)
	{
		Instance.CannedAcl = value;
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
	public Elastic.Clients.Elasticsearch.Snapshot.S3RepositorySettingsDescriptor ChunkSize(Elastic.Clients.Elasticsearch.ByteSize? value)
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
	public Elastic.Clients.Elasticsearch.Snapshot.S3RepositorySettingsDescriptor ChunkSize(System.Func<Elastic.Clients.Elasticsearch.ByteSizeBuilder, Elastic.Clients.Elasticsearch.ByteSize> action)
	{
		Instance.ChunkSize = Elastic.Clients.Elasticsearch.ByteSizeBuilder.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// The name of the S3 client to use to connect to S3.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Snapshot.S3RepositorySettingsDescriptor Client(string? value)
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
	public Elastic.Clients.Elasticsearch.Snapshot.S3RepositorySettingsDescriptor Compress(bool? value = true)
	{
		Instance.Compress = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The maxmimum batch size, between 1 and 1000, used for <c>DeleteObjects</c> requests.
	/// Defaults to 1000 which is the maximum number supported by the  AWS DeleteObjects API.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Snapshot.S3RepositorySettingsDescriptor DeleteObjectsMaxSize(int? value)
	{
		Instance.DeleteObjectsMaxSize = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The time to wait before trying again if an attempt to read a linearizable register fails.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Snapshot.S3RepositorySettingsDescriptor GetRegisterRetryDelay(Elastic.Clients.Elasticsearch.Duration? value)
	{
		Instance.GetRegisterRetryDelay = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The maximum number of parts that Elasticsearch will write during a multipart upload of a single object.
	/// Files which are larger than <c>buffer_size × max_multipart_parts</c> will be chunked into several smaller objects.
	/// Elasticsearch may also split a file across multiple objects to satisfy other constraints such as the <c>chunk_size</c> limit.
	/// Defaults to <c>10000</c> which is the maximum number of parts in a multipart upload in AWS S3.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Snapshot.S3RepositorySettingsDescriptor MaxMultipartParts(int? value)
	{
		Instance.MaxMultipartParts = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The maximum number of possibly-dangling multipart uploads to clean up in each batch of snapshot deletions.
	/// Defaults to 1000 which is the maximum number supported by the AWS ListMultipartUploads API.
	/// If set to <c>0</c>, Elasticsearch will not attempt to clean up dangling multipart uploads.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Snapshot.S3RepositorySettingsDescriptor MaxMultipartUploadCleanupSize(int? value)
	{
		Instance.MaxMultipartUploadCleanupSize = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The maximum snapshot restore rate per node.
	/// It defaults to unlimited.
	/// Note that restores are also throttled through recovery settings.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Snapshot.S3RepositorySettingsDescriptor MaxRestoreBytesPerSec(Elastic.Clients.Elasticsearch.ByteSize? value)
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
	public Elastic.Clients.Elasticsearch.Snapshot.S3RepositorySettingsDescriptor MaxRestoreBytesPerSec(System.Func<Elastic.Clients.Elasticsearch.ByteSizeBuilder, Elastic.Clients.Elasticsearch.ByteSize> action)
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
	public Elastic.Clients.Elasticsearch.Snapshot.S3RepositorySettingsDescriptor MaxSnapshotBytesPerSec(Elastic.Clients.Elasticsearch.ByteSize? value)
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
	public Elastic.Clients.Elasticsearch.Snapshot.S3RepositorySettingsDescriptor MaxSnapshotBytesPerSec(System.Func<Elastic.Clients.Elasticsearch.ByteSizeBuilder, Elastic.Clients.Elasticsearch.ByteSize> action)
	{
		Instance.MaxSnapshotBytesPerSec = Elastic.Clients.Elasticsearch.ByteSizeBuilder.Build(action);
		return this;
	}

	/// <summary>
	/// <para>
	/// If true, the repository is read-only.
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
	public Elastic.Clients.Elasticsearch.Snapshot.S3RepositorySettingsDescriptor Readonly(bool? value = true)
	{
		Instance.Readonly = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// When set to <c>true</c>, files are encrypted on server side using an AES256 algorithm.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Snapshot.S3RepositorySettingsDescriptor ServerSideEncryption(bool? value = true)
	{
		Instance.ServerSideEncryption = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The S3 storage class for objects written to the repository.
	/// Values may be <c>standard</c>, <c>reduced_redundancy</c>, <c>standard_ia</c>, <c>onezone_ia</c>, and <c>intelligent_tiering</c>.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Snapshot.S3RepositorySettingsDescriptor StorageClass(string? value)
	{
		Instance.StorageClass = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The delay before the first retry and the amount the delay is incremented by on each subsequent retry.
	/// The default is 50ms and the minimum is 0ms.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Snapshot.S3RepositorySettingsDescriptor ThrottledDeleteRetryDelayIncrement(Elastic.Clients.Elasticsearch.Duration? value)
	{
		Instance.ThrottledDeleteRetryDelayIncrement = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The upper bound on how long the delays between retries will grow to.
	/// The default is 5s and the minimum is 0ms.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Snapshot.S3RepositorySettingsDescriptor ThrottledDeleteRetryMaximumDelay(Elastic.Clients.Elasticsearch.Duration? value)
	{
		Instance.ThrottledDeleteRetryMaximumDelay = value;
		return this;
	}

	/// <summary>
	/// <para>
	/// The number times to retry a throttled snapshot deletion.
	/// The default is 10 and the minimum value is 0 which will disable retries altogether.
	/// Note that if retries are enabled in the Azure client, each of these retries comprises that many client-level retries.
	/// </para>
	/// </summary>
	public Elastic.Clients.Elasticsearch.Snapshot.S3RepositorySettingsDescriptor ThrottledDeleteRetryMaximumNumberOfRetries(int? value)
	{
		Instance.ThrottledDeleteRetryMaximumNumberOfRetries = value;
		return this;
	}

	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
	internal static Elastic.Clients.Elasticsearch.Snapshot.S3RepositorySettings Build(System.Action<Elastic.Clients.Elasticsearch.Snapshot.S3RepositorySettingsDescriptor> action)
	{
		var builder = new Elastic.Clients.Elasticsearch.Snapshot.S3RepositorySettingsDescriptor(new Elastic.Clients.Elasticsearch.Snapshot.S3RepositorySettings(Elastic.Clients.Elasticsearch.Serialization.JsonConstructorSentinel.Instance));
		action.Invoke(builder);
		return builder.Instance;
	}
}