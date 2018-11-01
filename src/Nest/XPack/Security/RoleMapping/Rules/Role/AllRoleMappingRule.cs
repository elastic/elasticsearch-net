using System.Collections.Generic;

namespace Nest
{
	public class AllRoleMappingRule : RoleMappingRuleBase
	{
		public AllRoleMappingRule(params RoleMappingRuleBase[] rules) => AllRules = rules;

		public AllRoleMappingRule(IEnumerable<RoleMappingRuleBase> rules) => AllRules = rules;

		public IEnumerable<RoleMappingRuleBase> All => AllRules;
	}
}
