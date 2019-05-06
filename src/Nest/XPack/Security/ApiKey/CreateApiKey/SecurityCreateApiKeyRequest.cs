using System;
using Newtonsoft.Json;

namespace Nest
{
	public partial interface ISecurityCreateApiKeyRequest
	{
		/// <summary>
		/// Optional expiration for the API key being generated.
		/// If expiration is not provided then the API keys do not expire.
		/// </summary>
		[JsonProperty("expiration")]
		Time Expiration { get; set; }

		/// <summary>
		/// Name for this API key
		/// </summary>
		[JsonProperty("name")]
		string Name { get; set; }

		/// <summary>
		/// Optional role descriptors for this API key, if not provided then permissions of authenticated user are applied.
		/// </summary>
		[JsonProperty("role_descriptors")]
		IApiKeyRoles Roles { get; set; }
	}

	public partial class SecurityCreateApiKeyRequest
	{
		/// <inheritdoc cref="ISecurityCreateApiKeyRequest.Expiration" />
		public Time Expiration { get; set; }

		/// <inheritdoc cref="ISecurityCreateApiKeyRequest.Name" />
		public string Name { get; set; }

		/// <inheritdoc cref="ISecurityCreateApiKeyRequest.Roles" />
		public IApiKeyRoles Roles { get; set; } = new ApiKeyRoles(); // Ensure not null, as server expects {}
	}

	[DescriptorFor("SecurityCreateApiKey")]
	public partial class SecurityCreateApiKeyDescriptor
	{
		/// <inheritdoc cref="ISecurityCreateApiKeyRequest.Expiration" />
		Time ISecurityCreateApiKeyRequest.Expiration { get; set; }

		/// <inheritdoc cref="ISecurityCreateApiKeyRequest.Name" />
		string ISecurityCreateApiKeyRequest.Name { get; set; }

		/// <inheritdoc cref="ISecurityCreateApiKeyRequest.Roles" />
		IApiKeyRoles ISecurityCreateApiKeyRequest.Roles { get; set; } = new ApiKeyRoles(); // Ensure not null, as server expects {}

		/// <inheritdoc cref="ISecurityCreateApiKeyRequest.Expiration" />
		public SecurityCreateApiKeyDescriptor Expiration(Time expiration) => Assign(expiration, (a, v) => a.Expiration = v);

		/// <inheritdoc cref="ISecurityCreateApiKeyRequest.Name" />
		public SecurityCreateApiKeyDescriptor Name(string name) => Assign(name, (a, v) => a.Name = v);

		/// <inheritdoc cref="ISecurityCreateApiKeyRequest.Roles" />
		public SecurityCreateApiKeyDescriptor Roles(Func<ApiKeyRolesDescriptor, IPromise<IApiKeyRoles>> selector) =>
			Assign(selector,
				(a, v) => a.Roles = v?.Invoke(new ApiKeyRolesDescriptor())?.Value ?? new ApiKeyRoles()); // Ensure not null, as server expects {}
	}
}
