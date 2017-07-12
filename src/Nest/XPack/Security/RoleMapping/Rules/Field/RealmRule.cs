using Newtonsoft.Json;

namespace Nest
{
	public class RealmRule : FieldRuleBase
	{
		public RealmRule(string realm)
		{
			this.Realm = realm;
		}
	}
}
