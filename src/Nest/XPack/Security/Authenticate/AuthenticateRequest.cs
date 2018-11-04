namespace Nest
{
	public partial interface IAuthenticateRequest { }

	public partial class AuthenticateRequest { }

	[DescriptorFor("XpackSecurityAuthenticate")]
	public partial class AuthenticateDescriptor { }
}
