using System;
using Newtonsoft.Json;

namespace Nest
{
	public interface IS3Repository : IRepository<IS3RepositorySettings> { }

	public class S3Repository : IS3Repository
	{
		public S3Repository(S3RepositorySettings settings) => Settings = settings;

		public IS3RepositorySettings Settings { get; set; }
		object IRepositoryWithSettings.DelegateSettings => Settings;
		public string Type { get; } = "s3";
	}

	public interface IS3RepositorySettings : IRepositorySettings
	{
		[Obsolete("Removed in Elasticsearch 6.0, please specify secure settings in the keystore.")]
		[JsonProperty("access_key")]
		string AccessKey { get; set; }

		/// <summary>
		/// Specifies the path within bucket to repository data.
		/// Defaults to value of repositories.s3.base_path or to root directory if not set.
		/// </summary>
		[JsonProperty("base_path")]
		string BasePath { get; set; }

		/// <summary>
		/// The name of the bucket to be used for snapshots. This field is required
		/// </summary>
		[JsonProperty("bucket")]
		string Bucket { get; set; }

		/// <summary>
		/// Minimum threshold below which the chunk is uploaded using a single request.
		/// Beyond this threshold, the S3 repository will use the AWS Multipart Upload API to split the chunk into
		/// several parts, each of buffer_size length, and to upload each part in its own request. Note that setting a
		/// buffer size lower than 5mb is not allowed since it will prevent the use of the Multipart API and may result
		/// in upload errors. It is also not possible to set a buffer size greater than 5gb as it is the maximum upload
		/// size allowed by S3. Defaults to the minimum between 100mb and 5% of the heap size.
		/// </summary>
		[JsonProperty("buffer_size")]
		string BufferSize { get; set; }

		/// <summary>
		/// Specify a canned ACL for the S3 bucket.
		/// The S3 repository supports all S3 canned ACLs : private, public-read, public-read-write, authenticated-read,
		/// log-delivery-write, bucket-owner-read, bucket-owner-full-control. Defaults to private.
		/// </summary>
		[JsonProperty("canned_acl")]
		string CannedAcl { get; set; }

		/// <summary>
		/// Big files can be broken down into chunks during snapshotting if needed.
		/// The chunk size can be specified in bytes or by using size value notation,
		/// i.e. 1gb, 10mb, 5kb. Defaults to 1gb.
		/// </summary>
		[JsonProperty("chunk_size")]
		string ChunkSize { get; set; }

		/// <summary>
		/// The name of the s3 client to use to connect to S3. Defaults to default.
		/// </summary>
		[JsonProperty("client")]
		string Client { get; set; }

		/// <summary>
		/// When set to true metadata files are stored in compressed format.
		/// This setting doesn't affect index files that are already compressed by default.
		/// Defaults to false.
		/// </summary>
		[JsonProperty("compress")]
		bool? Compress { get; set; }

		[Obsolete("Removed in Elasticsearch 6.0")]
		[JsonProperty("concurrent_streams")]
		int? ConcurrentStreams { get; set; }

		[Obsolete("Removed in Elasticsearch 6.0, please specify secure settings in the keystore.")]
		[JsonProperty("secret_key")]
		string SecretKey { get; set; }

		/// <summary>
		/// When set to true files are encrypted on server side using AES256 algorithm.
		/// Defaults to false.
		/// </summary>
		[JsonProperty("server_side_encryption")]
		bool? ServerSideEncryption { get; set; }

		/// <summary>
		/// Sets the S3 storage class type for the backup files. Values may be standard, reduced_redundancy, standard_ia.
		/// Defaults to standard.
		/// </summary>
		[JsonProperty("storage_class")]
		string StorageClass { get; set; }
	}

	public class S3RepositorySettings : IS3RepositorySettings
	{
		internal S3RepositorySettings() { }

		public S3RepositorySettings(string bucket) => Bucket = bucket;

		/// <inheritdoc />
		public string AccessKey { get; set; }

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
		public int? ConcurrentStreams { get; set; }

		/// <inheritdoc />
		public string SecretKey { get; set; }

		/// <inheritdoc />
		public bool? ServerSideEncryption { get; set; }

		/// <inheritdoc />
		public string StorageClass { get; set; }
	}

	public class S3RepositorySettingsDescriptor
		: DescriptorBase<S3RepositorySettingsDescriptor, IS3RepositorySettings>, IS3RepositorySettings
	{
		public S3RepositorySettingsDescriptor() { }

		public S3RepositorySettingsDescriptor(string bucket) => Self.Bucket = bucket;

		string IS3RepositorySettings.AccessKey { get; set; }
		string IS3RepositorySettings.BasePath { get; set; }

		string IS3RepositorySettings.Bucket { get; set; }
		string IS3RepositorySettings.BufferSize { get; set; }
		string IS3RepositorySettings.CannedAcl { get; set; }
		string IS3RepositorySettings.ChunkSize { get; set; }
		string IS3RepositorySettings.Client { get; set; }
		bool? IS3RepositorySettings.Compress { get; set; }
		int? IS3RepositorySettings.ConcurrentStreams { get; set; }
		string IS3RepositorySettings.SecretKey { get; set; }
		bool? IS3RepositorySettings.ServerSideEncryption { get; set; }
		string IS3RepositorySettings.StorageClass { get; set; }

		/// <inheritdoc cref="IS3RepositorySettings.AccessKey" />
		[Obsolete("Removed in Elasticsearch 6.0, please specify secure settings in the keystore.")]
		public S3RepositorySettingsDescriptor AccessKey(string accessKey) => Assign(accessKey, (a, v) => a.AccessKey = v);

		/// <inheritdoc cref="IS3RepositorySettings.SecretKey" />
		[Obsolete("Removed in Elasticsearch 6.0, please specify secure settings in the keystore.")]
		public S3RepositorySettingsDescriptor SecretKey(string secretKey) => Assign(secretKey, (a, v) => a.SecretKey = v);

		/// <inheritdoc cref="IS3RepositorySettings.ConcurrentStreams" />
		[Obsolete("Removed in Elasticsearch 6.0")]
		public S3RepositorySettingsDescriptor ConcurrentStreams(int? concurrentStreams) => Assign(concurrentStreams, (a, v) => a.ConcurrentStreams = v);

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
	}

	public class S3RepositoryDescriptor
		: DescriptorBase<S3RepositoryDescriptor, IS3Repository>, IS3Repository
	{
		IS3RepositorySettings IRepository<IS3RepositorySettings>.Settings { get; set; }
		object IRepositoryWithSettings.DelegateSettings => Self.Settings;
		string ISnapshotRepository.Type { get; } = "s3";

		public S3RepositoryDescriptor Settings(string bucket, Func<S3RepositorySettingsDescriptor, IS3RepositorySettings> settingsSelector = null) =>
			Assign(settingsSelector.InvokeOrDefault(new S3RepositorySettingsDescriptor(bucket)), (a, v) => a.Settings = v);
	}
}
