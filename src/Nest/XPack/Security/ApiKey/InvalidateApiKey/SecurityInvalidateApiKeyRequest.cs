using Newtonsoft.Json;

namespace Nest
{
	public partial interface ISecurityInvalidateApiKeyRequest
	{
		/// <summary>
		/// An API key id. This parameter cannot be used with any of Name, RealmName or Username are used.
		/// </summary>
		[JsonProperty("id")]
		string Id { get; set; }

		/// <summary>
		/// An API key name. This parameter cannot be used with any of Id, RealmName or Username are used.
		/// </summary>
		[JsonProperty("name")]
		string Name { get; set; }

		/// <summary>
		/// The name of an authentication realm. This parameter cannot be used with either Id or Name.
		/// </summary>
		[JsonProperty("realm_name")]
		string RealmName { get; set; }

		/// <summary>
		/// The username of a user. This parameter cannot be used with either Id or Name.
		/// </summary>
		[JsonProperty("username")]
		string Username { get; set; }
	}

	public partial class SecurityInvalidateApiKeyRequest
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

	[DescriptorFor("SecurityInvalidateApiKey")]
	public partial class SecurityInvalidateApiKeyDescriptor
	{

		/// <inheritdoc />
		string ISecurityInvalidateApiKeyRequest.Id { get; set; }

		/// <inheritdoc />
		string ISecurityInvalidateApiKeyRequest.Name { get; set; }

		/// <inheritdoc />
		string ISecurityInvalidateApiKeyRequest.RealmName { get; set; }

		/// <inheritdoc />
		string ISecurityInvalidateApiKeyRequest.Username { get; set; }

		/// <inheritdoc cref="ISecurityInvalidateApiKeyRequest.Id" />
		public SecurityInvalidateApiKeyDescriptor Id(string id) => Assign(id, (a, v) => a.Id = v);

		/// <inheritdoc cref="ISecurityInvalidateApiKeyRequest.Name" />
		public SecurityInvalidateApiKeyDescriptor Name(string name) => Assign(name, (a, v) => a.Name = v);

		/// <inheritdoc cref="ISecurityInvalidateApiKeyRequest.RealmName" />
		public SecurityInvalidateApiKeyDescriptor RealmName(string realmName) => Assign(realmName, (a, v) => a.RealmName = v);

		/// <inheritdoc cref="ISecurityInvalidateApiKeyRequest.Username" />
		public SecurityInvalidateApiKeyDescriptor Username(string username) => Assign(username, (a, v) => a.Username = v);
	}
}
