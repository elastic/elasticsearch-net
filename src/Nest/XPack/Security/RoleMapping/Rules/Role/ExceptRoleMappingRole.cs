namespace Nest
{
	public class ExceptRoleMappingRole : RoleMappingRuleBase
	{
		public ExceptRoleMappingRole(RoleMappingRuleBase except) => ExceptRule = except;

		public RoleMappingRuleBase Except => ExceptRule;
	}
}
