using System;
using System.Collections.Generic;

namespace Nest
{
	public class RoleMappingRulesDescriptor
		: DescriptorPromiseBase<RoleMappingRulesDescriptor, List<RoleMappingRuleBase>>, IPromise<List<RoleMappingRuleBase>>
	{
		public RoleMappingRulesDescriptor() : base(new List<RoleMappingRuleBase>()) { }

		private IPromise<List<RoleMappingRuleBase>> Self => this;
		List<RoleMappingRuleBase> IPromise<List<RoleMappingRuleBase>>.Value { get; } = new List<RoleMappingRuleBase>();

		public RoleMappingRulesDescriptor All(Func<RoleMappingRulesDescriptor, IPromise<List<RoleMappingRuleBase>>> selector) =>
			Add(new AllRoleMappingRule(selector?.Invoke(new RoleMappingRulesDescriptor())?.Value));

		public RoleMappingRulesDescriptor Any(Func<RoleMappingRulesDescriptor, IPromise<List<RoleMappingRuleBase>>> selector) =>
			Add(new AnyRoleMappingRule(selector?.Invoke(new RoleMappingRulesDescriptor())?.Value));

		public RoleMappingRulesDescriptor DistinguishedName(string name) => Add(new DistinguishedNameRule(name));

		public RoleMappingRulesDescriptor Except(Func<RoleMappingRuleDescriptor, RoleMappingRuleBase> selector) =>
			Add(new ExceptRoleMappingRole(selector?.Invoke(new RoleMappingRuleDescriptor())));

		public RoleMappingRulesDescriptor Groups(params string[] groups) => Add(new GroupsRule(groups));

		public RoleMappingRulesDescriptor Groups(IEnumerable<string> groups) => Add(new GroupsRule(groups));

		public RoleMappingRulesDescriptor Metadata(string key, object value) => Add(new MetadataRule(key, value));

		public RoleMappingRulesDescriptor Realm(string realm) => Add(new RealmRule(realm));

		public RoleMappingRulesDescriptor Username(string username) => Add(new UsernameRule(username));

		private RoleMappingRulesDescriptor Add(RoleMappingRuleBase m)
		{
			Self.Value.AddIfNotNull(m);
			return this;
		}
	}
}
