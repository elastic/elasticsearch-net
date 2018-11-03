using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Nest
{
	[ContractJsonConverter(typeof(FieldRuleBaseJsonConverter))]
	public abstract class FieldRuleBase : IsADictionaryBase<string, object>
	{
		[JsonIgnore]
		protected string DistinguishedName
		{
			get => BackingDictionary.TryGetValue("dn", out var o) ? (string)o : null;
			set => BackingDictionary.Add("dn", value);
		}

		[JsonIgnore]
		protected IEnumerable<string> Groups
		{
			get => BackingDictionary.TryGetValue("groups", out var o) ? (IEnumerable<string>)o : null;
			set => BackingDictionary.Add("groups", value);
		}

		[JsonIgnore]
		protected Tuple<string, object> Metadata
		{
			get
			{
				var metaKey = BackingDictionary.Keys.FirstOrDefault(k => k.StartsWith("metadata."));
				return string.IsNullOrEmpty(metaKey) ? null : Tuple.Create(metaKey, BackingDictionary[metaKey]);
			}
			set => BackingDictionary.Add("metadata." + value.Item1, value.Item2);
		}

		[JsonIgnore]
		protected string Realm
		{
			get => BackingDictionary.TryGetValue("realm.name", out var o) ? (string)o : null;
			set => BackingDictionary.Add("realm.name", value);
		}

		[JsonIgnore]
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
