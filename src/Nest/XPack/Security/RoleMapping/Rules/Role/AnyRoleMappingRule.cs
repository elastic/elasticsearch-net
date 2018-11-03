using System.Collections.Generic;

namespace Nest
{
	public class AnyRoleMappingRule : RoleMappingRuleBase
	{
		public AnyRoleMappingRule(params RoleMappingRuleBase[] rules) => AnyRules = rules;

		public AnyRoleMappingRule(IEnumerable<RoleMappingRuleBase> rules) => AnyRules = rules;

		public IEnumerable<RoleMappingRuleBase> Any => AnyRules;
	}
}
