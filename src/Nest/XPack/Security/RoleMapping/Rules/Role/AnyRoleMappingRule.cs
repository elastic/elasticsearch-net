using System.Collections.Generic;

namespace Nest
{
	public class AnyRoleMappingRule : RoleMappingRuleBase
	{
		public IEnumerable<RoleMappingRuleBase> Any => this.AnyRules;
		public AnyRoleMappingRule(params RoleMappingRuleBase[] rules)
		{
			this.AnyRules = rules;
		}
		public AnyRoleMappingRule(IEnumerable<RoleMappingRuleBase> rules)
		{
			this.AnyRules = rules;
		}
	}
}
