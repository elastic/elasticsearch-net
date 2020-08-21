// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Runtime.Serialization;

namespace Nest
{
	[MapsApi("security.invalidate_api_key")]
	public partial interface IInvalidateApiKeyRequest
	{
		/// <summary>
		/// An API key id. This parameter cannot be used with any of Name, RealmName or Username are used.
		/// </summary>
		[DataMember(Name = "id")]
		string Id { get; set; }

		/// <summary>
		/// An API key name. This parameter cannot be used with any of Id, RealmName or Username are used.
		/// </summary>
		[DataMember(Name = "name")]
		string Name { get; set; }

		/// <summary>
		/// The name of an authentication realm. This parameter cannot be used with either Id or Name.
		/// </summary>
		[DataMember(Name = "realm_name")]
		string RealmName { get; set; }

		/// <summary>
		/// The username of a user. This parameter cannot be used with either Id or Name.
		/// </summary>
		[DataMember(Name = "username")]
		string Username { get; set; }

		/// <summary>
		/// A boolean flag that can be used to query API keys owned by the currently authenticated user. Defaults to false.
		/// The RealmName or Username parameters cannot be specified when this parameter is set to true as they are
		/// assumed to be the currently authenticated ones.
		/// </summary>
		[DataMember(Name = "owner")]
		bool? Owner { get; set; }
	}

	public partial class InvalidateApiKeyRequest
	{
		/// <inheritdoc />
		public string Id { get; set; }

		/// <inheritdoc />
		public string Name { get; set; }

		/// <inheritdoc />
		public string RealmName { get; set; }

		/// <inheritdoc />
		public string Username { get; set; }

		/// <inheritdoc />
		public bool? Owner { get; set; }
	}

	public partial class InvalidateApiKeyDescriptor
	{
		/// <inheritdoc />
		string IInvalidateApiKeyRequest.Id { get; set; }

		/// <inheritdoc />
		string IInvalidateApiKeyRequest.Name { get; set; }

		/// <inheritdoc />
		string IInvalidateApiKeyRequest.RealmName { get; set; }

		/// <inheritdoc />
		string IInvalidateApiKeyRequest.Username { get; set; }

		/// <inheritdoc />
		bool? IInvalidateApiKeyRequest.Owner { get; set; }

		/// <inheritdoc cref="IInvalidateApiKeyRequest.Id" />
		public InvalidateApiKeyDescriptor Id(string id) => Assign(id, (a, v) => a.Id = v);

		/// <inheritdoc cref="IInvalidateApiKeyRequest.Name" />
		public InvalidateApiKeyDescriptor Name(string name) => Assign(name, (a, v) => a.Name = v);

		/// <inheritdoc cref="IInvalidateApiKeyRequest.RealmName" />
		public InvalidateApiKeyDescriptor RealmName(string realmName) => Assign(realmName, (a, v) => a.RealmName = v);

		/// <inheritdoc cref="IInvalidateApiKeyRequest.Username" />
		public InvalidateApiKeyDescriptor Username(string username) => Assign(username, (a, v) => a.Username = v);

		/// <inheritdoc cref="IInvalidateApiKeyRequest.Owner" />
		public InvalidateApiKeyDescriptor Owner(bool? owner = true) => Assign(owner, (a, v) => a.Owner = v);
	}
}
