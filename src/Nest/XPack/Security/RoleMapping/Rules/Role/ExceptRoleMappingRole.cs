namespace Nest
{
	public class ExceptRoleMappingRole : RoleMappingRuleBase
	{
		public RoleMappingRuleBase Except => this.ExceptRule;
		public ExceptRoleMappingRole(RoleMappingRuleBase except)
		{
			this.ExceptRule = except;
		}
	}
}
