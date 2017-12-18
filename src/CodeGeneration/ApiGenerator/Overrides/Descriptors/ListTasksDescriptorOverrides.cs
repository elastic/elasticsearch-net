using System.Collections.Generic;

namespace ApiGenerator.Overrides.Descriptors
{
	public class ListTasksDescriptorOverrides : DescriptorOverridesBase
	{
		public override IDictionary<string, string> RenameQueryStringParams => new Dictionary<string, string>
		{
			{ "parent_task_id", "parent_task"},
			{ "nodes", "node_id"},
		};
	}
}
