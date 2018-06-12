using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Nest
{
	[ContractJsonConverter(typeof(FieldRuleBaseJsonConverter))]
	public abstract class FieldRuleBase : IsADictionaryBase<string, object>
	{
		[JsonIgnore]
		protected string Username
		{
			get => (this.BackingDictionary.TryGetValue("username", out var o) ? (string) o : null);
			set => this.BackingDictionary.Add("username", value);
		}

		[JsonIgnore]
		protected string DistinguishedName
		{
			get => (this.BackingDictionary.TryGetValue("dn", out var o) ? (string) o : null);
			set => this.BackingDictionary.Add("dn", value);
		}
		[JsonIgnore]
		protected Tuple<string, object> Metadata
		{
			get
			{
				var metaKey = this.BackingDictionary.Keys.FirstOrDefault(k => k.StartsWith("metadata."));
				return string.IsNullOrEmpty(metaKey) ? null : Tuple.Create(metaKey, this.BackingDictionary[metaKey]);
			}
			set => this.BackingDictionary.Add("metadata." + value.Item1, value.Item2);
		}
		[JsonIgnore]
		protected string Realm
		{
			get => (this.BackingDictionary.TryGetValue("realm.name", out var o) ? (string) o : null);
			set => this.BackingDictionary.Add("realm.name", value);
		}
		[JsonIgnore]
		protected IEnumerable<string> Groups
		{
			get => (this.BackingDictionary.TryGetValue("groups", out var o) ? (IEnumerable<string>) o : null);
			set => this.BackingDictionary.Add("groups", value);
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
