using System;
using Newtonsoft.Json;

namespace Nest
{
	public interface IS3Repository : IRepository<IS3RepositorySettings> { }

	public class S3Repository : IS3Repository
	{
		public S3Repository(S3RepositorySettings settings) => Settings = settings;

		public IS3RepositorySettings Settings { get; set; }
		public string Type { get; } = "s3";
	}

	public interface IS3RepositorySettings : IRepositorySettings
	{
		/// <summary>
		/// The access key to use for authentication. Defaults to value of cloud.aws.access_key from elasticsearch.yml configuration.
		/// </summary>
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
		/// When set to true metadata files are stored in compressed format.
		/// This setting doesn't affect index files that are already compressed by default.
		/// Defaults to false.
		/// </summary>
		[JsonProperty("compress")]
		bool? Compress { get; set; }

		[JsonProperty("concurrent_streams")]
		int? ConcurrentStreams { get; set; }

		/// <summary>
		/// The endpoint to the S3 API. Defaults to AWS's default S3 endpoint. Note that setting a region overrides the endpoint setting.
		/// </summary>
		[JsonProperty("endpoint")]
		string Endpoint { get; set; }

		/// <summary>
		/// Number of retries in case of S3 errors. Defaults to 3.
		/// </summary>
		[JsonProperty("max_retries")]
		int? MaximumRetries { get; set; }

		/// <summary>
		/// Activate path style access for virtual hosting of buckets. The default behaviour is to detect which access style to use based on the
		/// configured endpoint (an IP will result in path-style access) and the bucket being accessed (some buckets are not valid DNS names). Defaults
		/// to false.
		/// </summary>
		[JsonProperty("path_style_access")]
		bool? PathStyleAccess { get; set; }

		/// <summary>
		/// The protocol to use (http or https). Defaults to value of cloud.aws.protocol or cloud.aws.s3.protocol from elasticsearch.yml configuration.
		/// </summary>
		[JsonProperty("protocol")]
		string Protocol { get; set; }

		/// <summary>
		/// Makes repository read-only. Defaults to false.
		/// </summary>
		[JsonProperty("readonly")]
		bool? ReadOnly { get; set; }

		/// <summary>
		/// The region where bucket is located. Defaults to US Standard.
		/// </summary>
		[Obsolete("Removed in NEST 6.x.")]
		[JsonProperty("region")]
		string Region { get; set; }

		/// <summary>
		/// The secret key to use for authentication. Defaults to value of cloud.aws.secret_key from elasticsearch.yml configuration.
		/// </summary>
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

		/// <summary>
		/// Set to true if you want to throttle retries. Defaults to AWS SDK default value (true).
		/// </summary>
		[JsonProperty("use_throttle_retries")]
		bool? UseThrottleRetries { get; set; }
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
		public bool? Compress { get; set; }

		/// <inheritdoc />
		public int? ConcurrentStreams { get; set; }

		/// <inheritdoc />
		public string Endpoint { get; set; }

		/// <inheritdoc />
		public int? MaximumRetries { get; set; }

		/// <inheritdoc />
		public bool? PathStyleAccess { get; set; }

		/// <inheritdoc />
		public string Protocol { get; set; }

		/// <inheritdoc />
		public bool? ReadOnly { get; set; }

		/// <inheritdoc />
		public string Region { get; set; }

		/// <inheritdoc />
		public string SecretKey { get; set; }

		/// <inheritdoc />
		public bool? ServerSideEncryption { get; set; }

		/// <inheritdoc />
		public string StorageClass { get; set; }

		/// <inheritdoc />
		public bool? UseThrottleRetries { get; set; }
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
		bool? IS3RepositorySettings.Compress { get; set; }
		int? IS3RepositorySettings.ConcurrentStreams { get; set; }
		string IS3RepositorySettings.Endpoint { get; set; }
		int? IS3RepositorySettings.MaximumRetries { get; set; }
		bool? IS3RepositorySettings.PathStyleAccess { get; set; }
		string IS3RepositorySettings.Protocol { get; set; }
		bool? IS3RepositorySettings.ReadOnly { get; set; }
		string IS3RepositorySettings.Region { get; set; }
		string IS3RepositorySettings.SecretKey { get; set; }
		bool? IS3RepositorySettings.ServerSideEncryption { get; set; }
		string IS3RepositorySettings.StorageClass { get; set; }
		bool? IS3RepositorySettings.UseThrottleRetries { get; set; }

		/// <inheritdoc cref="IS3RepositorySettings.Bucket" />
		public S3RepositorySettingsDescriptor Bucket(string bucket) => Assign(a => a.Bucket = bucket);

		/// <inheritdoc cref="IS3RepositorySettings.Region" />
		[Obsolete("Removed in NEST 6.x")]
		public S3RepositorySettingsDescriptor Region(string region) => Assign(a => a.Region = region);

		/// <inheritdoc cref="IS3RepositorySettings.Endpoint" />
		public S3RepositorySettingsDescriptor Endpoint(string endpoint) => Assign(a => a.Endpoint = endpoint);

		/// <inheritdoc cref="IS3RepositorySettings.Protocol" />
		public S3RepositorySettingsDescriptor Protocol(string protocol) => Assign(a => a.Protocol = protocol);

		/// <inheritdoc cref="IS3RepositorySettings.BasePath" />
		public S3RepositorySettingsDescriptor BasePath(string basePath) => Assign(a => a.BasePath = basePath);

		/// <inheritdoc cref="IS3RepositorySettings.AccessKey" />
		public S3RepositorySettingsDescriptor AccessKey(string accessKey) => Assign(a => a.AccessKey = accessKey);

		/// <inheritdoc cref="IS3RepositorySettings.SecretKey" />
		public S3RepositorySettingsDescriptor SecretKey(string secretKey) => Assign(a => a.SecretKey = secretKey);

		/// <inheritdoc cref="IS3RepositorySettings.ConcurrentStreams" />
		public S3RepositorySettingsDescriptor ConcurrentStreams(int concurrentStreams) => Assign(a => a.ConcurrentStreams = concurrentStreams);

		/// <inheritdoc cref="IS3RepositorySettings.ChunkSize" />
		public S3RepositorySettingsDescriptor ChunkSize(string chunkSize) => Assign(a => a.ChunkSize = chunkSize);

		/// <inheritdoc cref="IS3RepositorySettings.Compress" />
		public S3RepositorySettingsDescriptor Compress(bool compress = true) => Assign(a => a.Compress = compress);

		/// <inheritdoc cref="IS3RepositorySettings.ServerSideEncryption" />
		public S3RepositorySettingsDescriptor ServerSideEncryption(bool? serverSideEncryption = true) =>
			Assign(a => a.ServerSideEncryption = serverSideEncryption);

		/// <inheritdoc cref="IS3RepositorySettings.BufferSize" />
		public S3RepositorySettingsDescriptor BufferSize(string bufferSize) => Assign(a => a.BufferSize = bufferSize);

		/// <inheritdoc cref="IS3RepositorySettings.MaximumRetries" />
		public S3RepositorySettingsDescriptor MaximumRetries(int maximumRetries) => Assign(a => a.MaximumRetries = maximumRetries);

		/// <inheritdoc cref="IS3RepositorySettings.UseThrottleRetries" />
		public S3RepositorySettingsDescriptor UseThrottleRetries(bool? useThrottleRetries = true) =>
			Assign(a => a.UseThrottleRetries = useThrottleRetries);

		/// <inheritdoc cref="IS3RepositorySettings.ReadOnly" />
		public S3RepositorySettingsDescriptor ReadOnly(bool? @readonly = true) => Assign(a => a.ReadOnly = @readonly);

		/// <inheritdoc cref="IS3RepositorySettings.CannedAcl" />
		public S3RepositorySettingsDescriptor CannedAcl(string cannedAcl) => Assign(a => a.CannedAcl = cannedAcl);

		/// <inheritdoc cref="IS3RepositorySettings.StorageClass" />
		public S3RepositorySettingsDescriptor StorageClass(string storageClass) => Assign(a => a.StorageClass = storageClass);

		/// <inheritdoc cref="IS3RepositorySettings.PathStyleAccess" />
		public S3RepositorySettingsDescriptor PathStyleAccess(bool? pathStyleAccess = true) => Assign(a => a.PathStyleAccess = pathStyleAccess);
	}

	public class S3RepositoryDescriptor
		: DescriptorBase<S3RepositoryDescriptor, IS3Repository>, IS3Repository
	{
		IS3RepositorySettings IRepository<IS3RepositorySettings>.Settings { get; set; }
		string ISnapshotRepository.Type { get; } = "s3";

		public S3RepositoryDescriptor Settings(string bucket, Func<S3RepositorySettingsDescriptor, IS3RepositorySettings> settingsSelector = null) =>
			Assign(a => a.Settings = settingsSelector.InvokeOrDefault(new S3RepositorySettingsDescriptor(bucket)));
	}
}
