// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Runtime.Serialization;
using Nest.Utf8Json;

namespace Nest
{
	[InterfaceDataContract]
	[ReadAs(typeof(ApiKey))]
	public interface IApiKey
	{
		/// <summary>
		/// Optional expiration for the API key being generated.
		/// If an expiration is not provided then the API keys do not expire.
		/// </summary>
		[DataMember(Name = "expiration")]
		Time Expiration { get; set; }

		/// <summary>
		/// A name for this API key.
		/// </summary>
		[DataMember(Name = "name")]
		string Name { get; set; }

		/// <summary>
		/// Optional role descriptors for this API key, if not provided then permissions of authenticated user are applied.
		/// </summary>
		[DataMember(Name = "role_descriptors")]
		IApiKeyRoles Roles { get; set; }
	}

	public class ApiKey : IApiKey
	{
		/// <inheritdoc cref="IApiKey.Expiration" />
		public Time Expiration { get; set; }

		/// <inheritdoc cref="IApiKey.Name" />
		public string Name { get; set; }

		/// <inheritdoc cref="IApiKey.Roles" />
		public IApiKeyRoles Roles { get; set; }
	}

	public class ApiKeyDescriptor : DescriptorBase<ApiKeyDescriptor, IApiKey>, IApiKey
	{
		/// <inheritdoc cref="IApiKey.Expiration" />
		Time IApiKey.Expiration { get; set; }

		/// <inheritdoc cref="IApiKey.Name" />
		string IApiKey.Name { get; set; }

		/// <inheritdoc cref="IApiKey.Roles" />
		IApiKeyRoles IApiKey.Roles { get; set; }

		/// <inheritdoc cref="IApiKey.Expiration" />
		public ApiKeyDescriptor Expiration(Time expiration) => Assign(expiration, (a, v) => a.Expiration = v);

		/// <inheritdoc cref="IApiKey.Name" />
		public ApiKeyDescriptor Name(string name) => Assign(name, (a, v) => a.Name = v);

		/// <inheritdoc cref="IApiKey.Roles" />
		public ApiKeyDescriptor Roles(Func<ApiKeyRolesDescriptor, IPromise<IApiKeyRoles>> selector) =>
			Assign(selector, (a, v) => a.Roles = v.InvokeOrDefault(new ApiKeyRolesDescriptor()).Value);
	}
}
