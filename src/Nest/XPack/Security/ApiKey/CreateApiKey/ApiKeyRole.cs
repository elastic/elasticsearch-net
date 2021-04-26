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
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Nest
{
	[ReadAs(typeof(ApiKeyRole))]
	public interface IApiKeyRole
	{
		/// <summary>
		/// A list of clusters
		/// </summary>
		[DataMember(Name = "cluster")]
		IEnumerable<string> Cluster { get; set; }

		/// <summary>
		/// A list of API key privileges for indices.
		/// </summary>
		[DataMember(Name = "index")]
		IEnumerable<IApiKeyPrivileges> Index { get; set; }
	}

	public class ApiKeyRole : IApiKeyRole
	{
		/// <inheritdoc />
		public IEnumerable<string> Cluster { get; set; }

		/// <inheritdoc />
		public IEnumerable<IApiKeyPrivileges> Index { get; set; }
	}

	public class ApiKeyRoleDescriptor
		: DescriptorBase<ApiKeyRoleDescriptor, IApiKeyRole>, IApiKeyRole
	{
		/// <inheritdoc cref="IApiKeyRole.Cluster" />
		IEnumerable<string> IApiKeyRole.Cluster { get; set; }

		/// <inheritdoc cref="IApiKeyRole.Index" />
		IEnumerable<IApiKeyPrivileges> IApiKeyRole.Index { get; set; }

		/// <inheritdoc cref="IApiKeyRole.Cluster" />
		public ApiKeyRoleDescriptor Cluster(params string[] cluster) => Assign(cluster, (a, v) => a.Cluster = v);

		/// <inheritdoc cref="IApiKeyRole.Cluster" />
		public ApiKeyRoleDescriptor Cluster(IEnumerable<string> cluster) => Assign(cluster, (a, v) => a.Cluster = v);

		/// <inheritdoc cref="IApiKeyRole.Index" />
		public ApiKeyRoleDescriptor Indices(Func<ApiKeyPrivilegesDescriptor, IPromise<List<IApiKeyPrivileges>>> selector
		) => Assign(selector, (a, v) => a.Index = v?.Invoke(new ApiKeyPrivilegesDescriptor())?.Value);
	}
}
