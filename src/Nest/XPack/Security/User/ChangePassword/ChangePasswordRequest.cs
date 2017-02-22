using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Nest_5_2_0
{
	public partial interface IChangePasswordRequest
	{
		[JsonProperty("password")]
		string Password { get; set; }
	}

	public partial class ChangePasswordRequest
	{
		public string Password { get; set; }
	}

	[DescriptorFor("XpackSecurityChangePassword")]
	public partial class ChangePasswordDescriptor
	{
		string IChangePasswordRequest.Password { get; set; }

		public ChangePasswordDescriptor Password(string password) => Assign(r => r.Password = password);
	}
}
