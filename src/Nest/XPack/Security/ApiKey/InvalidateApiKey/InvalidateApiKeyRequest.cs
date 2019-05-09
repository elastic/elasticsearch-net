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

		/// <inheritdoc cref="IInvalidateApiKeyRequest.Id" />
		public InvalidateApiKeyDescriptor Id(string id) => Assign(id, (a, v) => a.Id = v);

		/// <inheritdoc cref="IInvalidateApiKeyRequest.Name" />
		public InvalidateApiKeyDescriptor Name(string name) => Assign(name, (a, v) => a.Name = v);

		/// <inheritdoc cref="IInvalidateApiKeyRequest.RealmName" />
		public InvalidateApiKeyDescriptor RealmName(string realmName) => Assign(realmName, (a, v) => a.RealmName = v);

		/// <inheritdoc cref="IInvalidateApiKeyRequest.Username" />
		public InvalidateApiKeyDescriptor Username(string username) => Assign(username, (a, v) => a.Username = v);
	}
}
