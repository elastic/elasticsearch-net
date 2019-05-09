using System;

namespace Nest
{
	public partial interface IEnableUserRequest { }

	public partial class EnableUserRequest { }

	[DescriptorFor("XpackSecurityEnableUser")]
	public partial class EnableUserDescriptor
	{
		[Obsolete("Use the constructor that accepts Name. Will be removed in the next major release")]
		public EnableUserDescriptor() { }

		[Obsolete("Use the constructor that accepts Name. Will be removed in the next major release")]
		public EnableUserDescriptor Username(Name username) =>
			Assign(username, (a , v) => a.RouteValues.Required("username", username));
	}
}
