namespace Nest
{
	public class UsernameRule : FieldRuleBase
	{
		public UsernameRule(string username)
		{
			this.Username = username;
		}
	}
}
