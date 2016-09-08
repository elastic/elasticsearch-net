using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
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
