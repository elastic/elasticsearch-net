using System;
using System.Collections.Generic;
using System.Linq;

namespace Nest
{
	public class S3Repository : IRepository
	{
		string IRepository.Type { get { return "s3"; } }
		public IDictionary<string, object> Settings { get; set; }
	}


	public class S3RepositoryDescriptor : IRepository
	{
		string IRepository.Type { get { return "s3"; } }
		IDictionary<string, object> IRepository.Settings { get; set; }

		private IRepository Self { get { return this; } }

		public S3RepositoryDescriptor()
		{
			Self.Settings = new Dictionary<string, object>();
		}
		/// <summary>
		/// The name of the bucket to be used for snapshots. (Mandatory)
		/// </summary>
		/// <param name="bucket"></param>
		public S3RepositoryDescriptor Bucket(string bucket)
		{
			Self.Settings["bucket"] = bucket;
			return this;
		}
		/// <summary>
		/// The region where bucket is located. Defaults to US Standard
		/// </summary>
		/// <param name="region"></param>
		/// <returns></returns>
		public S3RepositoryDescriptor Region(string region)
		{
			Self.Settings["region"] = region;
			return this;
		}
		/// <summary>
		/// Specifies the path within bucket to repository data. Defaults to root directory.
		/// </summary>
		/// <param name="basePath"></param>
		/// <returns></returns>
		public S3RepositoryDescriptor BasePath(string basePath)
		{
			Self.Settings["base_path"] = basePath;
			return this;
		}
		/// <summary>
		/// The access key to use for authentication. Defaults to value of cloud.aws.access_key.
		/// </summary>
		/// <param name="accessKey"></param>
		/// <returns></returns>
		public S3RepositoryDescriptor AccessKey(string accessKey)
		{
			Self.Settings["access_key"] = accessKey;
			return this;
		}
		/// <summary>
		/// The secret key to use for authentication. Defaults to value of cloud.aws.secret_key.
		/// </summary>
		/// <param name="secretKey"></param>
		/// <returns></returns>
		public S3RepositoryDescriptor SecretKey(string secretKey)
		{
			Self.Settings["secretKey"] = secretKey;
			return this;
		}
		/// <summary>
		/// When set to true metadata files are stored in compressed format. This setting doesn't 
		/// affect index files that are already compressed by default. Defaults to false.
		/// </summary>
		/// <param name="compress"></param>
		public S3RepositoryDescriptor Compress(bool compress = true)
		{
			Self.Settings["compress"] = compress;
			return this;
		}
		/// <summary>
		/// Throttles the number of streams (per node) preforming snapshot operation. Defaults to 5
		/// </summary>
		/// <param name="concurrentStreams"></param>
		public S3RepositoryDescriptor ConcurrentStreams(int concurrentStreams)
		{
			Self.Settings["concurrent_streams"] = concurrentStreams;
			return this;
		}
		/// <summary>
		///  Big files can be broken down into chunks during snapshotting if needed. 
		/// The chunk size can be specified in bytes or by using size value notation, 
		/// i.e. 1g, 10m, 5k. Defaults to 100m.
		/// </summary>
		/// <param name="chunkSize"></param>
		public S3RepositoryDescriptor ChunkSize(string chunkSize)
		{
			Self.Settings["chunk_size"] = chunkSize;
			return this;
		}

	}
}
