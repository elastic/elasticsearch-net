namespace Nest
{
	public class FieldRoleMappingRule : RoleMappingRuleBase
	{
		public FieldRoleMappingRule(FieldRuleBase field) => FieldRule = field;

		public FieldRuleBase Field => FieldRule;
	}
}
