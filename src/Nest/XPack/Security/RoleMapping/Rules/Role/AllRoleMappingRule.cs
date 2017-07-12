using System.Collections.Generic;

namespace Nest
{
	public class AllRoleMappingRule : RoleMappingRuleBase
	{
		public IEnumerable<RoleMappingRuleBase> All => this.AllRules;
		public AllRoleMappingRule(params RoleMappingRuleBase[] rules)
		{
			this.AllRules = rules;
		}
		public AllRoleMappingRule(IEnumerable<RoleMappingRuleBase> rules)
		{
			this.AllRules = rules;
		}
	}
}
