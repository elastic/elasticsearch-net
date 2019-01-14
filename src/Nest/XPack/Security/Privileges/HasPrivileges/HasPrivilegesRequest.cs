using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	[MapsApi("xpack.security.has_privileges.json")]
	public partial interface IHasPrivilegesRequest
	{
		[JsonProperty("email")]
		string Email { get; set; }

		[JsonProperty("full_name")]
		string FullName { get; set; }

		[JsonProperty("metadata")]
		IDictionary<string, object> Metadata { get; set; }

		[JsonProperty("password")]
		string Password { get; set; }

		[JsonProperty("roles")]
		IEnumerable<string> Roles { get; set; }
	}

	public partial class HasPrivilegesRequest
	{
		public string Email { get; set; }
		public string FullName { get; set; }
		public IDictionary<string, object> Metadata { get; set; }
		public string Password { get; set; }
		public IEnumerable<string> Roles { get; set; }
	}

	public partial class HasPrivilegesDescriptor
	{
		string IHasPrivilegesRequest.Email { get; set; }
		string IHasPrivilegesRequest.FullName { get; set; }
		IDictionary<string, object> IHasPrivilegesRequest.Metadata { get; set; }
		string IHasPrivilegesRequest.Password { get; set; }
		IEnumerable<string> IHasPrivilegesRequest.Roles { get; set; }

		public HasPrivilegesDescriptor Password(string password) => Assign(a => a.Password = password);

		public HasPrivilegesDescriptor Roles(IEnumerable<string> roles) => Assign(a => a.Roles = roles);

		public HasPrivilegesDescriptor Roles(params string[] roles) => Assign(a => a.Roles = roles);

		public HasPrivilegesDescriptor FullName(string fullName) => Assign(a => a.FullName = fullName);

		public HasPrivilegesDescriptor Email(string email) => Assign(a => a.Email = email);

		public HasPrivilegesDescriptor Metadata(IDictionary<string, object> metadata) => Assign(a => a.Metadata = metadata);

		public HasPrivilegesDescriptor Metadata(Func<FluentDictionary<string, object>, IDictionary<string, object>> selector) =>
			Assign(a => a.Metadata = selector?.Invoke(new FluentDictionary<string, object>()));
	}
}
