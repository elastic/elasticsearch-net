using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Nest
{
	public partial interface IClearCachedRealmsRequest { }

	public partial class ClearCachedRealmsRequest { }

	[DescriptorFor("XpackSecurityClearCachedRealms")]
	public partial class ClearCachedRealmsDescriptor { }
}
