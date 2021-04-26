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
using System.Linq;
using System.Runtime.Serialization;
using Nest.Utf8Json;

namespace Nest
{
	[JsonFormatter(typeof(FieldRuleBaseFormatter))]
	public abstract class FieldRuleBase : IsADictionaryBase<string, object>
	{
		[IgnoreDataMember]
		protected string DistinguishedName
		{
			get => BackingDictionary.TryGetValue("dn", out var o) ? (string)o : null;
			set => BackingDictionary.Add("dn", value);
		}

		[IgnoreDataMember]
		protected IEnumerable<string> DistinguishedNames
		{
			get => BackingDictionary.TryGetValue("dn", out var o) ? (IEnumerable<string>)o : null;
			set => BackingDictionary.Add("dn", value);
		}

		[IgnoreDataMember]
		protected IEnumerable<string> Groups
		{
			get => BackingDictionary.TryGetValue("groups", out var o) ? (IEnumerable<string>)o : null;
			set => BackingDictionary.Add("groups", value);
		}

		[IgnoreDataMember]
		protected Tuple<string, object> Metadata
		{
			get
			{
				var metaKey = BackingDictionary.Keys.FirstOrDefault(k => k.StartsWith("metadata."));
				return string.IsNullOrEmpty(metaKey) ? null : Tuple.Create(metaKey, BackingDictionary[metaKey]);
			}
			set => BackingDictionary.Add("metadata." + value.Item1, value.Item2);
		}

		[IgnoreDataMember]
		protected string Realm
		{
			get => BackingDictionary.TryGetValue("realm.name", out var o) ? (string)o : null;
			set => BackingDictionary.Add("realm.name", value);
		}

		[IgnoreDataMember]
		protected string Username
		{
			get => BackingDictionary.TryGetValue("username", out var o) ? (string)o : null;
			set => BackingDictionary.Add("username", value);
		}

		public static AnyRoleMappingRule operator |(FieldRuleBase leftContainer, FieldRuleBase rightContainer) =>
			new AnyRoleMappingRule(new FieldRoleMappingRule(leftContainer), new FieldRoleMappingRule(rightContainer));

		public static AllRoleMappingRule operator &(FieldRuleBase leftContainer, FieldRuleBase rightContainer) =>
			new AllRoleMappingRule(new FieldRoleMappingRule(leftContainer), new FieldRoleMappingRule(rightContainer));

		public static AllRoleMappingRule operator +(FieldRuleBase leftContainer, FieldRuleBase rightContainer) =>
			new AllRoleMappingRule(new FieldRoleMappingRule(leftContainer), new FieldRoleMappingRule(rightContainer));

		public static ExceptRoleMappingRole operator !(FieldRuleBase leftContainer) =>
			new ExceptRoleMappingRole(new FieldRoleMappingRule(leftContainer));

		public static bool operator false(FieldRuleBase a) => false;

		public static bool operator true(FieldRuleBase a) => false;
	}
}
