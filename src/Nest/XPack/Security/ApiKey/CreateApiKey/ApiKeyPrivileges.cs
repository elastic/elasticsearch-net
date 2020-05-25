// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

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
