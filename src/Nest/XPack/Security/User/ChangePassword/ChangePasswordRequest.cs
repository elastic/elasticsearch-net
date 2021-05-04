// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Runtime.Serialization;

namespace Nest
{
	[MapsApi("security.change_password.json")]
	public partial interface IChangePasswordRequest
	{
		[DataMember(Name ="password")]
		string Password { get; set; }
	}

	public partial class ChangePasswordRequest
	{
		public string Password { get; set; }
	}

	public partial class ChangePasswordDescriptor
	{
		string IChangePasswordRequest.Password { get; set; }

		public ChangePasswordDescriptor Password(string password) => Assign(password, (a, v) => a.Password = v);
	}
}
