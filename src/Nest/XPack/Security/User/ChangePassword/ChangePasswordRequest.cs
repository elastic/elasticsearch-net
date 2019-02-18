using Newtonsoft.Json;

namespace Nest
{
	[MapsApi("security.change_password.json")]
	public partial interface IChangePasswordRequest
	{
		[JsonProperty("password")]
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
