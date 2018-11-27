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

		public ChangePasswordDescriptor Password(string password) => Assign(r => r.Password = password);
	}
}
