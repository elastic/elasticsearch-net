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
using System.Runtime.Serialization;

namespace Nest
{
	[MapsApi("security.create_api_key")]
	public partial interface ICreateApiKeyRequest
	{
		/// <summary>
		/// Optional expiration for the API key being generated.
		/// If expiration is not provided then the API keys do not expire.
		/// </summary>
		[DataMember(Name = "expiration")]
		Time Expiration { get; set; }

		/// <summary>
		/// Name for this API key
		/// </summary>
		[DataMember(Name = "name")]
		string Name { get; set; }

		/// <summary>
		/// Optional role descriptors for this API key, if not provided then permissions of authenticated user are applied.
		/// </summary>
		[DataMember(Name = "role_descriptors")]
		IApiKeyRoles Roles { get; set; }
	}

	public partial class CreateApiKeyRequest
	{
		/// <inheritdoc cref="ICreateApiKeyRequest.Expiration" />
		public Time Expiration { get; set; }

		/// <inheritdoc cref="ICreateApiKeyRequest.Name" />
		public string Name { get; set; }

		/// <inheritdoc cref="ICreateApiKeyRequest.Roles" />
		public IApiKeyRoles Roles { get; set; } = new ApiKeyRoles(); // Ensure not null, as server expects {}
	}


	public partial class CreateApiKeyDescriptor
	{
		/// <inheritdoc cref="ICreateApiKeyRequest.Expiration" />
		Time ICreateApiKeyRequest.Expiration { get; set; }

		/// <inheritdoc cref="ICreateApiKeyRequest.Name" />
		string ICreateApiKeyRequest.Name { get; set; }

		/// <inheritdoc cref="ICreateApiKeyRequest.Roles" />
		IApiKeyRoles ICreateApiKeyRequest.Roles { get; set; } = new ApiKeyRoles(); // Ensure not null, as server expects {}

		/// <inheritdoc cref="ICreateApiKeyRequest.Expiration" />
		public CreateApiKeyDescriptor Expiration(Time expiration) => Assign(expiration, (a, v) => a.Expiration = v);

		/// <inheritdoc cref="ICreateApiKeyRequest.Name" />
		public CreateApiKeyDescriptor Name(string name) => Assign(name, (a, v) => a.Name = v);

		/// <inheritdoc cref="ICreateApiKeyRequest.Roles" />
		public CreateApiKeyDescriptor Roles(Func<ApiKeyRolesDescriptor, IPromise<IApiKeyRoles>> selector) =>
			Assign(selector,
				(a, v) => a.Roles = v.InvokeOrDefault(new ApiKeyRolesDescriptor()).Value);
	}
}
