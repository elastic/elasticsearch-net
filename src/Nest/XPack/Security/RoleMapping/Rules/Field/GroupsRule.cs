using System.Collections.Generic;

namespace Nest
{
	public class GroupsRule : FieldRuleBase
	{
		public GroupsRule(params string[] groups)
		{
			this.Groups = groups;
		}
		public GroupsRule(IEnumerable<string> groups)
		{
			this.Groups = groups;
		}
	}
}
