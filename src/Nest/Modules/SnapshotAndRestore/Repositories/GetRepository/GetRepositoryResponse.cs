// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Collections.Generic;
using System.Runtime.Serialization;
using Nest.Utf8Json;

namespace Nest
{
	[DataContract]
	[JsonFormatter(typeof(GetRepositoryResponseFormatter))]
	public class GetRepositoryResponse : ResponseBase
	{
		public IReadOnlyDictionary<string, ISnapshotRepository> Repositories { get; internal set; } =
			EmptyReadOnly<string, ISnapshotRepository>.Dictionary;

		public AzureRepository Azure(string name) => Get<AzureRepository>(name);

		public FileSystemRepository FileSystem(string name) => Get<FileSystemRepository>(name);

		public HdfsRepository Hdfs(string name) => Get<HdfsRepository>(name);

		public ReadOnlyUrlRepository ReadOnlyUrl(string name) => Get<ReadOnlyUrlRepository>(name);

		public S3Repository S3(string name) => Get<S3Repository>(name);

		private TRepository Get<TRepository>(string name)
			where TRepository : class, ISnapshotRepository
		{
			if (Repositories == null) return null;
			if (!Repositories.TryGetValue(name, out var repository)) return null;

			return repository as TRepository;
		}
	}
}
