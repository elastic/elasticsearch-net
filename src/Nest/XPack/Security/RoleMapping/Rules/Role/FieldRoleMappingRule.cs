namespace Nest
{
	public class FieldRoleMappingRule : RoleMappingRuleBase
	{
		public FieldRuleBase Field => this.FieldRule;
		public FieldRoleMappingRule(FieldRuleBase field)
		{
			this.FieldRule = field;
		}

	}
}
