using System.Collections.Generic;

namespace Nest
{
	public class AllRoleMappingRule : RoleMappingRuleBase
	{
		public IEnumerable<RoleMappingRuleBase> All => Self.AllRules;
		public AllRoleMappingRule(params RoleMappingRuleBase[] rules) => Self.AllRules = rules;
		public AllRoleMappingRule(IEnumerable<RoleMappingRuleBase> rules) => Self.AllRules = rules;
	}
}
