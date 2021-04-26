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
	[ReadAs(typeof(ApiKeyPrivileges))]
	public interface IApiKeyPrivileges
	{
		/// <summary>
		/// A list of names.
		/// </summary>
		[DataMember(Name = "names")]
		IEnumerable<string> Names { get; set; }

		/// <summary>
		/// A list of privileges.
		/// </summary>
		[DataMember(Name = "privileges")]
		IEnumerable<string> Privileges { get; set; }
	}

	public class ApiKeyPrivileges : IApiKeyPrivileges
	{
		/// <inheritdoc cref="IApiKeyPrivileges.Names" />
		public IEnumerable<string> Names { get; set; }

		/// <inheritdoc cref="IApiKeyPrivileges.Privileges" />
		public IEnumerable<string> Privileges { get; set; }
	}

	public class ApiKeyPrivilegesDescriptor
		: DescriptorPromiseBase<ApiKeyPrivilegesDescriptor, List<IApiKeyPrivileges>>
	{
		public ApiKeyPrivilegesDescriptor() : base(new List<IApiKeyPrivileges>()) { }

		public ApiKeyPrivilegesDescriptor Index(Func<ApiKeyPrivilegeDescriptor, IApiKeyPrivileges> selector) =>
			Assign(selector, (a, v) => a.Add(v.InvokeOrDefault(new ApiKeyPrivilegeDescriptor())));

		public class ApiKeyPrivilegeDescriptor
			: DescriptorBase<ApiKeyPrivilegeDescriptor, IApiKeyPrivileges>, IApiKeyPrivileges
		{
			/// <inheritdoc cref="IApiKeyPrivileges.Names" />
			IEnumerable<string> IApiKeyPrivileges.Names { get; set; }

			/// <inheritdoc cref="IApiKeyPrivileges.Privileges" />
			IEnumerable<string> IApiKeyPrivileges.Privileges { get; set; }

			/// <inheritdoc cref="IApiKeyPrivileges.Privileges" />
			public ApiKeyPrivilegeDescriptor Privileges(params string[] privileges) => Assign(privileges, (a, v) => a.Privileges = v);

			/// <inheritdoc cref="IApiKeyPrivileges.Privileges" />
			public ApiKeyPrivilegeDescriptor Privileges(IEnumerable<string> privileges) => Assign(privileges, (a, v) => a.Privileges = v);

			/// <inheritdoc cref="IApiKeyPrivileges.Names" />
			public ApiKeyPrivilegeDescriptor Names(params string[] resources) => Assign(resources, (a, v) => a.Names = v);

			/// <inheritdoc cref="IApiKeyPrivileges.Names" />
			public ApiKeyPrivilegeDescriptor Names(IEnumerable<string> resources) => Assign(resources, (a, v) => a.Names = v);
		}
	}
}
