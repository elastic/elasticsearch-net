using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Nest
{
	[MapsApi("security.put_user.json")]
	public partial interface IPutUserRequest
	{
		[DataMember(Name ="email")]
		string Email { get; set; }

		[DataMember(Name ="full_name")]
		string FullName { get; set; }

		[DataMember(Name ="metadata")]
		IDictionary<string, object> Metadata { get; set; }

		[DataMember(Name ="password")]
		string Password { get; set; }

		[DataMember(Name ="roles")]
		IEnumerable<string> Roles { get; set; }
	}

	public partial class PutUserRequest
	{
		public string Email { get; set; }
		public string FullName { get; set; }
		public IDictionary<string, object> Metadata { get; set; }
		public string Password { get; set; }
		public IEnumerable<string> Roles { get; set; }
	}

	public partial class PutUserDescriptor
	{
		string IPutUserRequest.Email { get; set; }
		string IPutUserRequest.FullName { get; set; }
		IDictionary<string, object> IPutUserRequest.Metadata { get; set; }
		string IPutUserRequest.Password { get; set; }
		IEnumerable<string> IPutUserRequest.Roles { get; set; }

		public PutUserDescriptor Password(string password) => Assign(password, (a, v) => a.Password = v);

		public PutUserDescriptor Roles(IEnumerable<string> roles) => Assign(roles, (a, v) => a.Roles = v);

		public PutUserDescriptor Roles(params string[] roles) => Assign(roles, (a, v) => a.Roles = v);

		public PutUserDescriptor FullName(string fullName) => Assign(fullName, (a, v) => a.FullName = v);

		public PutUserDescriptor Email(string email) => Assign(email, (a, v) => a.Email = v);

		public PutUserDescriptor Metadata(IDictionary<string, object> metadata) => Assign(metadata, (a, v) => a.Metadata = v);

		public PutUserDescriptor Metadata(Func<FluentDictionary<string, object>, IDictionary<string, object>> selector) =>
			Assign(selector, (a, v) => a.Metadata = v?.Invoke(new FluentDictionary<string, object>()));
	}
}
