/*
 * Licensed to Elasticsearch B.V. under one or more contributor
 * license agreements. See the NOTICE file distributed with
 * this work for additional information regarding copyright
 * ownership. Elasticsearch B.V. licenses this file to you under
 * the Apache License, Version 2.0 (the "License"); you may
 * not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

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
