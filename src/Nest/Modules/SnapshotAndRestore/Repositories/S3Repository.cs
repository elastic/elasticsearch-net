// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Runtime.Serialization;
using Nest.Utf8Json;

namespace Nest
{
	/// <summary>
	/// A snapshot repository that stores snapshots in an Amazon S3 bucket
	/// <para />
	/// Requires the repository-s3 plugin to be installed on the cluster
	/// </summary>
	public interface IS3Repository : IRepository<IS3RepositorySettings> { }

	/// <inheritdoc />
	public class S3Repository : IS3Repository
	{
		public S3Repository(IS3RepositorySettings settings) => Settings = settings;

		public IS3RepositorySettings Settings { get; set; }
		object IRepositoryWithSettings.DelegateSettings => Settings;
		public string Type { get; } = "s3";
	}

	/// <summary>
	/// Snapshot repository settings for <see cref="IS3Repository"/>
	/// </summary>
	public interface IS3RepositorySettings : IRepositorySettings
	{
		/// <summary>
		/// Specifies the path within bucket to repository data.
		/// Defaults to value of repositories.s3.base_path or to root directory if not set.
		/// </summary>
		[DataMember(Name ="base_path")]
		string BasePath { get; set; }

		/// <summary>
		/// The name of the bucket to be used for snapshots. This field is required
		/// </summary>
		[DataMember(Name ="bucket")]
		string Bucket { get; set; }

		/// <summary>
		/// Minimum threshold below which the chunk is uploaded using a single request.
		/// Beyond this threshold, the S3 repository will use the AWS Multipart Upload API to split the chunk into
		/// several parts, each of buffer_size length, and to upload each part in its own request. Note that setting a
		/// buffer size lower than 5mb is not allowed since it will prevent the use of the Multipart API and may result
		/// in upload errors. It is also not possible to set a buffer size greater than 5gb as it is the maximum upload
		/// size allowed by S3. Defaults to the minimum between 100mb and 5% of the heap size.
		/// </summary>
		[DataMember(Name ="buffer_size")]
		string BufferSize { get; set; }

		/// <summary>
		/// Specify a canned ACL for the S3 bucket.
		/// The S3 repository supports all S3 canned ACLs : private, public-read, public-read-write, authenticated-read,
		/// log-delivery-write, bucket-owner-read, bucket-owner-full-control. Defaults to private.
		/// </summary>
		[DataMember(Name ="canned_acl")]
		string CannedAcl { get; set; }

		/// <summary>
		/// Big files can be broken down into chunks during snapshotting if needed.
		/// The chunk size can be specified in bytes or by using size value notation,
		/// i.e. 1gb, 10mb, 5kb. Defaults to 1gb.
		/// </summary>
		[DataMember(Name ="chunk_size")]
		string ChunkSize { get; set; }

		/// <summary>
		/// The name of the s3 client to use to connect to S3. Defaults to default.
		/// </summary>
		[DataMember(Name ="client")]
		string Client { get; set; }

		/// <summary>
		/// When set to true metadata files are stored in compressed format.
		/// This setting doesn't affect index files that are already compressed by default.
		/// Defaults to false.
		/// </summary>
		[DataMember(Name ="compress")]
		[JsonFormatter(typeof(NullableStringBooleanFormatter))]
		bool? Compress { get; set; }

		/// <summary>
		/// When set to true files are encrypted on server side using AES256 algorithm.
		/// Defaults to false.
		/// </summary>
		[DataMember(Name ="server_side_encryption")]
		[JsonFormatter(typeof(NullableStringBooleanFormatter))]
		bool? ServerSideEncryption { get; set; }

		/// <summary>
		/// Sets the S3 storage class type for the backup files. Values may be standard, reduced_redundancy, standard_ia.
		/// Defaults to standard.
		/// </summary>
		[DataMember(Name ="storage_class")]
		string StorageClass { get; set; }

		/// <summary>
		///  Whether to force the use of the path style access pattern. If `true`, the
		// path style access pattern will be used. If `false`, the access pattern will
		// be automatically determined by the AWS Java SDK used internally by Elasticsearch
		/// </summary>
		[DataMember(Name = "path_style_access")]
		[JsonFormatter(typeof(NullableStringBooleanFormatter))]
		bool? PathStyleAccess { get; set; }

		/// <summary>
		/// Whether chunked encoding should be disabled or not. If false, chunked encoding is enabled and will be used where appropriate.
		/// If true, chunked encoding is disabled and will not be used, which may mean that snapshot operations consume more resources
		/// and take longer to complete. It should only be set to true if you are using a storage service that does not support chunked
		/// encoding. Defaults to false.
		/// </summary>
		[DataMember(Name = "disable_chunked_encoding")]
		[JsonFormatter(typeof(NullableStringBooleanFormatter))]
		bool? DisableChunkedEncoding { get; set; }

		/// <summary>
		/// Make the repository readonly. Defaults to false.
		/// </summary>
		[DataMember(Name = "readonly")]
		[JsonFormatter(typeof(NullableStringBooleanFormatter))]
		bool? ReadOnly { get; set; }

		/// <summary>
		/// Throttles per node restore rate. Defaults to 40mb per second.
		/// </summary>
		[DataMember(Name = "max_restore_bytes_per_sec")]
		string MaxRestoreBytesPerSecond { get; set; }

		/// <summary>
		/// Throttles per node snapshot rate. Defaults to 40mb per second.
		/// </summary>
		[DataMember(Name = "max_snapshot_bytes_per_sec")]
		string MaxSnapshotBytesPerSecond { get; set; }
	}

	/// <inheritdoc />
	public class S3RepositorySettings : IS3RepositorySettings
	{
		internal S3RepositorySettings() { }

		public S3RepositorySettings(string bucket) => Bucket = bucket;

		/// <inheritdoc />
		public string BasePath { get; set; }

		/// <inheritdoc />
		public string Bucket { get; set; }

		/// <inheritdoc />
		public string BufferSize { get; set; }

		/// <inheritdoc />
		public string CannedAcl { get; set; }

		/// <inheritdoc />
		public string ChunkSize { get; set; }

		/// <inheritdoc />
		public string Client { get; set; }

		/// <inheritdoc />
		public bool? Compress { get; set; }

		/// <inheritdoc />
		public bool? ServerSideEncryption { get; set; }

		/// <inheritdoc />
		public string StorageClass { get; set; }

		/// <inheritdoc />
		public bool? PathStyleAccess { get; set; }

		/// <inheritdoc />
		public bool? DisableChunkedEncoding { get; set; }

		/// <inheritdoc />
		public bool? ReadOnly { get; set; }

		/// <inheritdoc />
		public string MaxRestoreBytesPerSecond { get; set; }

		/// <inheritdoc />
		public string MaxSnapshotBytesPerSecond { get; set; }
	}

	/// <inheritdoc cref="IS3RepositorySettings"/>
	public class S3RepositorySettingsDescriptor
		: DescriptorBase<S3RepositorySettingsDescriptor, IS3RepositorySettings>, IS3RepositorySettings
	{
		public S3RepositorySettingsDescriptor(string bucket) => Self.Bucket = bucket;

		string IS3RepositorySettings.BasePath { get; set; }
		string IS3RepositorySettings.Bucket { get; set; }
		string IS3RepositorySettings.BufferSize { get; set; }
		string IS3RepositorySettings.CannedAcl { get; set; }
		string IS3RepositorySettings.ChunkSize { get; set; }
		string IS3RepositorySettings.Client { get; set; }
		bool? IS3RepositorySettings.Compress { get; set; }
		bool? IS3RepositorySettings.ServerSideEncryption { get; set; }
		string IS3RepositorySettings.StorageClass { get; set; }
		bool? IS3RepositorySettings.PathStyleAccess { get; set; }
		bool? IS3RepositorySettings.DisableChunkedEncoding { get; set; }
		bool? IS3RepositorySettings.ReadOnly { get; set; }
		string IS3RepositorySettings.MaxRestoreBytesPerSecond { get; set; }
		string IS3RepositorySettings.MaxSnapshotBytesPerSecond { get; set; }

		/// <inheritdoc cref="IS3RepositorySettings.Bucket" />
		public S3RepositorySettingsDescriptor Bucket(string bucket) => Assign(bucket, (a, v) => a.Bucket = v);

		/// <inheritdoc cref="IS3RepositorySettings.Client" />
		public S3RepositorySettingsDescriptor Client(string client) => Assign(client, (a, v) => a.Client = v);

		/// <inheritdoc cref="IS3RepositorySettings.BasePath" />
		public S3RepositorySettingsDescriptor BasePath(string basePath) => Assign(basePath, (a, v) => a.BasePath = v);

		/// <inheritdoc cref="IS3RepositorySettings.Compress" />
		public S3RepositorySettingsDescriptor Compress(bool? compress = true) => Assign(compress, (a, v) => a.Compress = v);

		/// <inheritdoc cref="IS3RepositorySettings.ChunkSize" />
		public S3RepositorySettingsDescriptor ChunkSize(string chunkSize) => Assign(chunkSize, (a, v) => a.ChunkSize = v);

		/// <inheritdoc cref="IS3RepositorySettings.ServerSideEncryption" />
		public S3RepositorySettingsDescriptor ServerSideEncryption(bool? serverSideEncryption = true) =>
			Assign(serverSideEncryption, (a, v) => a.ServerSideEncryption = v);

		/// <inheritdoc cref="IS3RepositorySettings.BufferSize" />
		public S3RepositorySettingsDescriptor BufferSize(string bufferSize) => Assign(bufferSize, (a, v) => a.BufferSize = v);

		/// <inheritdoc cref="IS3RepositorySettings.CannedAcl" />
		public S3RepositorySettingsDescriptor CannedAcl(string cannedAcl) => Assign(cannedAcl, (a, v) => a.CannedAcl = v);

		/// <inheritdoc cref="IS3RepositorySettings.StorageClass" />
		public S3RepositorySettingsDescriptor StorageClass(string storageClass) => Assign(storageClass, (a, v) => a.StorageClass = v);

		/// <inheritdoc cref="IS3RepositorySettings.PathStyleAccess" />
		public S3RepositorySettingsDescriptor PathStyleAccess(bool? pathStyleAccess = true) =>
			Assign(pathStyleAccess, (a, v) => a.PathStyleAccess = v);

		/// <inheritdoc cref="IS3RepositorySettings.DisableChunkedEncoding" />
		public S3RepositorySettingsDescriptor DisableChunkedEncoding(bool? disableChunkedEncoding = true) =>
			Assign(disableChunkedEncoding, (a, v) => a.DisableChunkedEncoding = v);

		/// <inheritdoc cref="IS3RepositorySettings.ReadOnly" />
		public S3RepositorySettingsDescriptor ReadOnly(bool? readOnly = true) =>
			Assign(readOnly, (a, v) => a.ReadOnly = v);

		/// <inheritdoc cref="IS3RepositorySettings.MaxRestoreBytesPerSecond" />
		public S3RepositorySettingsDescriptor MaxRestoreBytesPerSecond(string maxRestoreBytesPerSecond) =>
			Assign(maxRestoreBytesPerSecond, (a, v) => a.MaxRestoreBytesPerSecond = v);

		/// <inheritdoc cref="IS3RepositorySettings.MaxSnapshotBytesPerSecond" />
		public S3RepositorySettingsDescriptor MaxSnapshotBytesPerSecond(string maxSnapshotBytesPerSecond) =>
			Assign(maxSnapshotBytesPerSecond, (a, v) => a.MaxSnapshotBytesPerSecond = v);
	}

	/// <inheritdoc cref="IS3Repository"/>
	public class S3RepositoryDescriptor
		: DescriptorBase<S3RepositoryDescriptor, IS3Repository>, IS3Repository
	{
		IS3RepositorySettings IRepository<IS3RepositorySettings>.Settings { get; set; }
		object IRepositoryWithSettings.DelegateSettings => Self.Settings;
		string ISnapshotRepository.Type { get; } = "s3";

		/// <inheritdoc cref="IS3RepositorySettings"/>
		public S3RepositoryDescriptor Settings(string bucket, Func<S3RepositorySettingsDescriptor, IS3RepositorySettings> settingsSelector = null) =>
			Assign(settingsSelector.InvokeOrDefault(new S3RepositorySettingsDescriptor(bucket)), (a, v) => a.Settings = v);
	}
}
