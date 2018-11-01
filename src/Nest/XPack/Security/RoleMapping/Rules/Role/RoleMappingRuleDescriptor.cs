using System;
using System.Collections.Generic;

namespace Nest
{
	public class RoleMappingRuleDescriptor : DescriptorBase<RoleMappingRuleDescriptor, IDescriptor>, IDescriptor
	{
		public RoleMappingRuleBase All(Func<RoleMappingRulesDescriptor, IPromise<List<RoleMappingRuleBase>>> selector) =>
			new AllRoleMappingRule(selector?.Invoke(new RoleMappingRulesDescriptor())?.Value);

		public RoleMappingRuleBase Any(Func<RoleMappingRulesDescriptor, IPromise<List<RoleMappingRuleBase>>> selector) =>
			new AnyRoleMappingRule(selector?.Invoke(new RoleMappingRulesDescriptor())?.Value);

		public RoleMappingRuleBase DistinguishedName(string name) => new DistinguishedNameRule(name);

		public RoleMappingRuleBase Except(Func<RoleMappingRuleDescriptor, RoleMappingRuleBase> selector) =>
			new ExceptRoleMappingRole(selector?.Invoke(new RoleMappingRuleDescriptor()));

		public RoleMappingRuleBase Groups(params string[] groups) => new GroupsRule(groups);

		public RoleMappingRuleBase Groups(IEnumerable<string> groups) => new GroupsRule(groups);

		public RoleMappingRuleBase Metadata(string key, object value) => new MetadataRule(key, value);

		public RoleMappingRuleBase Realm(string realm) => new RealmRule(realm);

		public RoleMappingRuleBase Username(string username) => new UsernameRule(username);
	}
}
