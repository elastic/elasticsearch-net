using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public partial interface IXPackInfoRequest { }

	public partial class XPackInfoRequest { }

	[DescriptorFor("XpackInfo")]
	public partial class XPackInfoDescriptor : IXPackInfoRequest { }
}
