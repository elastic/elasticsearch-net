namespace Nest
{
	public class FieldRoleMappingRule : RoleMappingRuleBase
	{
		public FieldRuleBase Field => Self.FieldRule;
		public FieldRoleMappingRule(FieldRuleBase field) => Self.FieldRule = field;
	}
}
