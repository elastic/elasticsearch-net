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

using System;
using Nest.Utf8Json;

namespace Nest
{
	/// <summary>
	/// Creates a snapshot repository
	/// </summary>
	[MapsApi("snapshot.create_repository.json")]
	[JsonFormatter(typeof(CreateRepositoryFormatter))]
	public partial interface ICreateRepositoryRequest
	{
		/// <summary>
		/// The snapshot repository
		/// </summary>
		ISnapshotRepository Repository { get; set; }
	}

	/// <inheritdoc cref="ICreateRepositoryRequest" />
	public partial class CreateRepositoryRequest
	{
		/// <inheritdoc />
		public ISnapshotRepository Repository { get; set; }
	}

	/// <inheritdoc cref="ICreateRepositoryRequest" />
	public partial class CreateRepositoryDescriptor
	{
		ISnapshotRepository ICreateRepositoryRequest.Repository { get; set; }

		/// <inheritdoc cref="IFileSystemRepository"/>
		public CreateRepositoryDescriptor FileSystem(Func<FileSystemRepositoryDescriptor, IFileSystemRepository> selector) =>
			Assign(selector, (a, v) => a.Repository = v?.Invoke(new FileSystemRepositoryDescriptor()));

		/// <inheritdoc cref="IReadOnlyUrlRepository" />
		public CreateRepositoryDescriptor ReadOnlyUrl(Func<ReadOnlyUrlRepositoryDescriptor, IReadOnlyUrlRepository> selector) =>
			Assign(selector, (a, v) => a.Repository = v?.Invoke(new ReadOnlyUrlRepositoryDescriptor()));

		/// <inheritdoc cref="IAzureRepository" />
		public CreateRepositoryDescriptor Azure(Func<AzureRepositoryDescriptor, IAzureRepository> selector = null) =>
			Assign(selector.InvokeOrDefault(new AzureRepositoryDescriptor()), (a, v) => a.Repository = v);

		/// <inheritdoc cref="IHdfsRepository" />
		public CreateRepositoryDescriptor Hdfs(Func<HdfsRepositoryDescriptor, IHdfsRepository> selector) =>
			Assign(selector, (a, v) => a.Repository = v?.Invoke(new HdfsRepositoryDescriptor()));

		/// <inheritdoc cref="IS3Repository" />
		public CreateRepositoryDescriptor S3(Func<S3RepositoryDescriptor, IS3Repository> selector) =>
			Assign(selector, (a, v) => a.Repository = v?.Invoke(new S3RepositoryDescriptor()));

		/// <inheritdoc cref="ISourceOnlyRepository" />
		public CreateRepositoryDescriptor SourceOnly(Func<SourceOnlyRepositoryDescriptor, ISourceOnlyRepository> selector) =>
			Assign(selector, (a, v) => a.Repository = v?.Invoke(new SourceOnlyRepositoryDescriptor()));

		/// <summary>
		/// Register a custom repository
		/// </summary>
		public CreateRepositoryDescriptor Custom(ISnapshotRepository repository) => Assign(repository, (a, v) => a.Repository = v);
	}
}
