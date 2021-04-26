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
	public class RoleMappingRulesDescriptor
		: DescriptorPromiseBase<RoleMappingRulesDescriptor, List<RoleMappingRuleBase>>
	{
		public RoleMappingRulesDescriptor() : base(new List<RoleMappingRuleBase>()) { }

		private RoleMappingRulesDescriptor Add(RoleMappingRuleBase m)
		{
			PromisedValue.AddIfNotNull(m);
			return this;
		}

		public RoleMappingRulesDescriptor DistinguishedName(IEnumerable<string> names) => Add(new DistinguishedNameRule(names));

		public RoleMappingRulesDescriptor DistinguishedName(string name) => Add(new DistinguishedNameRule(name));

		public RoleMappingRulesDescriptor Username(string username) => Add(new UsernameRule(username));

		public RoleMappingRulesDescriptor Groups(params string[] groups) => Add(new GroupsRule(groups));

		public RoleMappingRulesDescriptor Groups(IEnumerable<string> groups) => Add(new GroupsRule(groups));

		public RoleMappingRulesDescriptor Realm(string realm) => Add(new RealmRule(realm));

		public RoleMappingRulesDescriptor Metadata(string key, object value) => Add(new MetadataRule(key, value));

		public RoleMappingRulesDescriptor Any(Func<RoleMappingRulesDescriptor, IPromise<List<RoleMappingRuleBase>>> selector) =>
			Add(new AnyRoleMappingRule(selector?.Invoke(new RoleMappingRulesDescriptor())?.Value));

		public RoleMappingRulesDescriptor All(Func<RoleMappingRulesDescriptor, IPromise<List<RoleMappingRuleBase>>> selector) =>
			Add(new AllRoleMappingRule(selector?.Invoke(new RoleMappingRulesDescriptor())?.Value));

		public RoleMappingRulesDescriptor Except(Func<RoleMappingRuleDescriptor, RoleMappingRuleBase> selector) =>
			Add(new ExceptRoleMappingRole(selector?.Invoke(new RoleMappingRuleDescriptor())));
	}
}
