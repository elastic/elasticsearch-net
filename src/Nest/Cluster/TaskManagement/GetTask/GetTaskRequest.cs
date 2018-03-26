using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nest
{
	public partial interface IGetTaskRequest { }

	public partial class GetTaskRequest { }

	[DescriptorFor("TasksGet")]
	public partial class GetTaskDescriptor
	{
		[Obsolete("Maintained for binary compatibility. Use the constructor that accepts a task id. Will be removed in 7.0")]
		public GetTaskDescriptor() { }
	}
}
