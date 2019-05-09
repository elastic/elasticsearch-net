using System;

namespace Nest
{
	public partial interface IDisableUserRequest { }

	public partial class DisableUserRequest { }

	[DescriptorFor("XpackSecurityDisableUser")]
	public partial class DisableUserDescriptor
	{
		[Obsolete("Use the constructor that accepts Name. Will be removed in the next major release")]
		public DisableUserDescriptor() { }

		[Obsolete("Use the constructor that accepts Name. Will be removed in the next major release")]
		public DisableUserDescriptor Username(Name username) =>
			Assign(username, (a , v) => a.RouteValues.Required("username", username));
	}
}
