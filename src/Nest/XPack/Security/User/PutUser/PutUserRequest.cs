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

		public PutUserDescriptor Password(string password) => Assign(a => a.Password = password);

		public PutUserDescriptor Roles(IEnumerable<string> roles) => Assign(a => a.Roles = roles);

		public PutUserDescriptor Roles(params string[] roles) => Assign(a => a.Roles = roles);

		public PutUserDescriptor FullName(string fullName) => Assign(a => a.FullName = fullName);

		public PutUserDescriptor Email(string email) => Assign(a => a.Email = email);

		public PutUserDescriptor Metadata(IDictionary<string, object> metadata) => Assign(a => a.Metadata = metadata);

		public PutUserDescriptor Metadata(Func<FluentDictionary<string, object>, IDictionary<string, object>> selector) =>
			Assign(a => a.Metadata = selector?.Invoke(new FluentDictionary<string, object>()));
	}
}
