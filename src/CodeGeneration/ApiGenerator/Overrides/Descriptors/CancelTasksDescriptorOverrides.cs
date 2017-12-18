using System.Collections.Generic;
using ApiGenerator.Domain;

namespace ApiGenerator.Overrides.Descriptors
{
	public class CancelTasksDescriptorOverrides : DescriptorOverridesBase
	{
		public override IDictionary<string, string> RenameQueryStringParams => new Dictionary<string, string>
		{
			{ "parent_task_id", "parent_task"},
			{ "nodes", "node_id"},
		};
	}
}
