using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	[JsonConverter(typeof(VerbatimDictionaryKeysJsonConverter<string, IApiKeyRole>))]
	public interface IApiKeyRoles : IIsADictionary<string, IApiKeyRole> { }

	public class ApiKeyRoles : IsADictionaryBase<string, IApiKeyRole>, IApiKeyRoles
	{
		public ApiKeyRoles() { }

		internal ApiKeyRoles(IDictionary<string, IApiKeyRole> backingDictionary) : base(backingDictionary) { }

		public void Add(string role, IApiKeyRole apiKeyRole) => BackingDictionary.Add(role, apiKeyRole);
	}

	public class ApiKeyRolesDescriptor : IsADictionaryDescriptorBase<ApiKeyRolesDescriptor, ApiKeyRoles, string, IApiKeyRole>
	{
		public ApiKeyRolesDescriptor() : base(new ApiKeyRoles()) { }

		public ApiKeyRolesDescriptor Role(string name, Func<ApiKeyRoleDescriptor, IApiKeyRole> selector) =>
			Assign(selector, (a, v) => a.Add(name, v.InvokeOrDefault(new ApiKeyRoleDescriptor())));
	}
}
