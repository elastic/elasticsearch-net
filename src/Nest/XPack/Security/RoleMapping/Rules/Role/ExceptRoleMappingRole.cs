namespace Nest
{
	public class ExceptRoleMappingRole : RoleMappingRuleBase
	{
		public RoleMappingRuleBase Except => Self.ExceptRule;
		public ExceptRoleMappingRole(RoleMappingRuleBase except) => Self.ExceptRule = except;
	}
}
