using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public partial interface IStartTrialLicenseRequest
	{
	}

	public partial class StartTrialLicenseRequest
	{
	}

	[DescriptorFor("XpackLicensePostStartTrial")]
	public partial class StartTrialLicenseDescriptor : IStartTrialLicenseRequest
	{

	}
}
