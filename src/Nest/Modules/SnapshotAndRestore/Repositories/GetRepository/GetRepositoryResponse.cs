using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	[JsonObject(MemberSerialization.OptIn)]
	[JsonConverter(typeof(GetRepositoryResponseJsonConverter))]
	public interface IGetRepositoryResponse : IResponse
	{
		IDictionary<string, ISnapshotRepository> Repositories { get; set; }

		AzureRepository Azure(string name);
		FileSystemRepository FileSystem(string name);
        HdfsRepository Hdfs(string name);
        ReadOnlyUrlRepository ReadOnlyUrl(string name);
        S3Repository S3(string name);
	}

	[JsonObject]
	public class GetRepositoryResponse : ResponseBase, IGetRepositoryResponse
	{
		public GetRepositoryResponse()
		{
			this.Repositories = new Dictionary<string, ISnapshotRepository>();
		}

		public IDictionary<string, ISnapshotRepository> Repositories { get; set; }

		public AzureRepository Azure(string name) => Get<AzureRepository>(name);
		public FileSystemRepository FileSystem(string name) => Get<FileSystemRepository>(name);
		public HdfsRepository Hdfs(string name) => Get<HdfsRepository>(name);
		public ReadOnlyUrlRepository ReadOnlyUrl(string name) => Get<ReadOnlyUrlRepository>(name);
		public S3Repository S3(string name) => Get<S3Repository>(name);

		private TRepository Get<TRepository>(string name)
			where TRepository : class, ISnapshotRepository
		{
			if (this.Repositories == null) return null;
			if (!this.Repositories.ContainsKey(name)) return null;
			return this.Repositories[name] as TRepository;
		}
	}
}
