// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Elasticsearch.Net.Utf8Json;

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
