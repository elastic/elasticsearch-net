using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public partial interface IAuthenticateRequest
	{
	}

	public partial class AuthenticateRequest
	{
	}

	[DescriptorFor("XpackSecurityAuthenticate")]
	public partial class AuthenticateDescriptor
	{
	}
}
