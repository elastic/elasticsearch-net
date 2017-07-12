using System;
using System.Collections.Generic;

namespace Nest
{
	public class RoleMappingRuleDescriptor : DescriptorBase<RoleMappingRuleDescriptor, IDescriptor>, IDescriptor
	{
		public RoleMappingRuleBase DistinguishedName(string name) => new DistinguishedNameRule(name);

		public RoleMappingRuleBase Username(string username) => new UsernameRule(username);

		public RoleMappingRuleBase Groups(params string[] groups) => new GroupsRule(groups);

		public RoleMappingRuleBase Groups(IEnumerable<string> groups) => new GroupsRule(groups);

		public RoleMappingRuleBase Realm(string realm) => new RealmRule(realm);

		public RoleMappingRuleBase Metadata(string key, object value) => new MetadataRule(key, value);

		public RoleMappingRuleBase Any(Func<RoleMappingRulesDescriptor, IPromise<List<RoleMappingRuleBase>>> selector) =>
			new AnyRoleMappingRule(selector?.Invoke(new RoleMappingRulesDescriptor())?.Value);

		public RoleMappingRuleBase All(Func<RoleMappingRulesDescriptor, IPromise<List<RoleMappingRuleBase>>> selector) =>
			new AllRoleMappingRule(selector?.Invoke(new RoleMappingRulesDescriptor())?.Value);

		public RoleMappingRuleBase Except(Func<RoleMappingRuleDescriptor, RoleMappingRuleBase> selector) =>
			new ExceptRoleMappingRole(selector?.Invoke(new RoleMappingRuleDescriptor()));

	}
}
