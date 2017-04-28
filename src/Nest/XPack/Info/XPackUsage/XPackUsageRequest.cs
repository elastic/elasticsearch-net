using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public partial interface IXPackUsageRequest { }

	public partial class XPackUsageRequest { }

	[DescriptorFor("XpackUsage")]
	public partial class XPackUsageDescriptor : IXPackUsageRequest { }
}
