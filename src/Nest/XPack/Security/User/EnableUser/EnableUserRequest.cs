using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nest
{
	public partial interface IEnableUserRequest { }

	public partial class EnableUserRequest { }

	[DescriptorFor("XpackSecurityEnableUser")]
	public partial class EnableUserDescriptor { }
}
