using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest_5_2_0
{
	public partial interface IGetLicenseRequest
	{
	}

	public partial class GetLicenseRequest
	{
	}

	[DescriptorFor("XpackLicenseGet")]
	public partial class GetLicenseDescriptor : IGetLicenseRequest
	{

	}
}
