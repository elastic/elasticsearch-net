// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Nest
{
	[DataContract]
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
