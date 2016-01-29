using System;
using Newtonsoft.Json;

namespace Nest
{
	public interface IS3Repository : IRepository<IS3RepositorySettings> { }

	public class S3Repository : IS3Repository
	{
		public S3Repository(S3RepositorySettings settings)
		{
			Settings = settings;
		}

		public IS3RepositorySettings Settings { get; set; }
		public string Type { get; } = "s3";
	}

	public interface IS3RepositorySettings : IRepositorySettings
	{
		[JsonProperty("bucket")]
		string Bucket { get; set; }

		[JsonProperty("region")]
		string Region { get; set; }

		[JsonProperty("base_path")]
		string BasePath { get; set; }

		[JsonProperty("access_key")]
		string AccessKey { get; set; }

		[JsonProperty("secret_key")]
		string SecretKey { get; set; }

		[JsonProperty("compress")]
		bool? Compress { get; set; }

		[JsonProperty("concurrent_streams")]
		int? ConcurrentStreams { get; set; }

		[JsonProperty("chunk_size")]
		string ChunkSize { get; set; }
	}

	public class S3RepositorySettings : IS3RepositorySettings
	{
		internal S3RepositorySettings() { }

		public S3RepositorySettings(string bucket)
		{
			this.Bucket = bucket;
		}

		public string Bucket { get; set; }
		public string Region { get; set; }
		public string BasePath { get; set; }
		public string AccessKey { get; set; }
		public string SecretKey { get; set; }
		public bool? Compress { get; set; }
		public int? ConcurrentStreams { get; set; }
		public string ChunkSize { get; set; }
	}

	public class S3RepositorySettingsDescriptor 
		: DescriptorBase<S3RepositorySettingsDescriptor, IS3RepositorySettings>, IS3RepositorySettings
	{
		string IS3RepositorySettings.Bucket { get; set; }
		string IS3RepositorySettings.Region { get; set; }
		string IS3RepositorySettings.BasePath { get; set; }
		string IS3RepositorySettings.AccessKey { get; set; }
		string IS3RepositorySettings.SecretKey { get; set; }
		bool? IS3RepositorySettings.Compress { get; set; }
		int? IS3RepositorySettings.ConcurrentStreams { get; set; }
		string IS3RepositorySettings.ChunkSize { get; set; }

		/// <summary>
		/// The name of the bucket to be used for snapshots. (Mandatory)
		/// </summary>
		/// <param name="bucket"></param>
		public S3RepositorySettingsDescriptor Bucket(string bucket) => Assign(a => a.Bucket = bucket);

		/// <summary>
		/// The region where bucket is located. Defaults to US Standard
		/// </summary>
		/// <param name="region"></param>
		/// <returns></returns>
		public S3RepositorySettingsDescriptor Region(string region) => Assign(a => a.Region = region);

		/// <summary>
		/// Specifies the path within bucket to repository data. Defaults to root directory.
		/// </summary>
		/// <param name="basePath"></param>
		/// <returns></returns>
		public S3RepositorySettingsDescriptor BasePath(string basePath) => Assign(a => a.BasePath = basePath);

		/// <summary>
		/// The access key to use for authentication. Defaults to value of cloud.aws.access_key.
		/// </summary>
		/// <param name="accessKey"></param>
		/// <returns></returns>
		public S3RepositorySettingsDescriptor AccessKey(string accessKey) => Assign(a => a.AccessKey = accessKey);

		/// <summary>
		/// The secret key to use for authentication. Defaults to value of cloud.aws.secret_key.
		/// </summary>
		/// <param name="secretKey"></param>
		/// <returns></returns>
		public S3RepositorySettingsDescriptor SecretKey(string secretKey) => Assign(a => a.SecretKey = secretKey);

		/// <summary>
		/// When set to true metadata files are stored in compressed format. This setting doesn't 
		/// affect index files that are already compressed by default. Defaults to false.
		/// </summary>
		/// <param name="compress"></param>
		public S3RepositorySettingsDescriptor Compress(bool compress = true) => Assign(a => a.Compress = compress);

		/// <summary>
		/// Throttles the number of streams (per node) preforming snapshot operation. Defaults to 5
		/// </summary>
		/// <param name="concurrentStreams"></param>
		public S3RepositorySettingsDescriptor ConcurrentStreams(int concurrentStreams) => Assign(a => a.ConcurrentStreams = concurrentStreams);

		/// <summary>
		///  Big files can be broken down into chunks during snapshotting if needed. 
		/// The chunk size can be specified in bytes or by using size value notation, 
		/// i.e. 1g, 10m, 5k. Defaults to 100m.
		/// </summary>
		/// <param name="chunkSize"></param>
		public S3RepositorySettingsDescriptor ChunkSize(string chunkSize) => Assign(a => a.ChunkSize = chunkSize);
	}

	public class S3RepositoryDescriptor 
		: DescriptorBase<S3RepositoryDescriptor, IS3Repository>, IS3Repository
	{
		string ISnapshotRepository.Type { get; } = "s3";
		IS3RepositorySettings IRepository<IS3RepositorySettings>.Settings { get; set; }

		public S3RepositoryDescriptor Settings(string bucket, Func<S3RepositorySettingsDescriptor, IS3RepositorySettings> settingsSelector = null) =>
			Assign(a => a.Settings = settingsSelector.InvokeOrDefault(new S3RepositorySettingsDescriptor().Bucket(bucket)));
	}
}
