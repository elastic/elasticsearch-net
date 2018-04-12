using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public partial interface IGetTrialLicenseStatusRequest
	{
	}

	public partial class GetTrialLicenseStatusRequest
	{
	}

	[DescriptorFor("XpackLicenseGetTrialStatus")]
	public partial class GetTrialLicenseStatusDescriptor : IGetTrialLicenseStatusRequest
	{

	}
}
