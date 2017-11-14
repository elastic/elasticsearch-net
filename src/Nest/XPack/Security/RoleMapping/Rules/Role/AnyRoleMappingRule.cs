using System.Collections.Generic;

namespace Nest
{
	public class AnyRoleMappingRule : RoleMappingRuleBase
	{
		public IEnumerable<RoleMappingRuleBase> Any => Self.AnyRules;
		public AnyRoleMappingRule(params RoleMappingRuleBase[] rules) => Self.AnyRules = rules;
		public AnyRoleMappingRule(IEnumerable<RoleMappingRuleBase> rules) => Self.AnyRules = rules;
	}
}
