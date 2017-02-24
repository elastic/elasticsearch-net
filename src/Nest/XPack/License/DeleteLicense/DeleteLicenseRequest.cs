using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest_5_2_0
{
	public partial interface IDeleteLicenseRequest
	{
	}

	public partial class DeleteLicenseRequest
	{
	}

	[DescriptorFor("XpackLicenseDelete")]
	public partial class DeleteLicenseDescriptor : IDeleteLicenseRequest
	{

	}
}
