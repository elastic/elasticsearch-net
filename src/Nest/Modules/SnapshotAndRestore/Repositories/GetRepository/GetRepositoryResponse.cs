/*
 * Licensed to Elasticsearch B.V. under one or more contributor
 * license agreements. See the NOTICE file distributed with
 * this work for additional information regarding copyright
 * ownership. Elasticsearch B.V. licenses this file to you under
 * the Apache License, Version 2.0 (the "License"); you may
 * not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

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
