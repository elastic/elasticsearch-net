using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using Newtonsoft.Json;

namespace Nest
{
	public interface IRoleMappingRule
	{
		[JsonProperty("any")]
		IEnumerable<RoleMappingRuleBase> AnyRules { get; set; }

		[JsonProperty("all")]
		IEnumerable<RoleMappingRuleBase> AllRules { get; set; }

		[JsonProperty("field")]
		FieldRuleBase FieldRule { get; set; }

		[JsonProperty("except")]
		RoleMappingRuleBase ExceptRule { get; set; }
	}


	[JsonConverter(typeof(RoleMappingRuleBaseJsonConverter))]
	public abstract class RoleMappingRuleBase : IRoleMappingRule
	{
		internal IRoleMappingRule Self => this;

		IEnumerable<RoleMappingRuleBase> IRoleMappingRule.AnyRules { get; set; }

		IEnumerable<RoleMappingRuleBase> IRoleMappingRule.AllRules { get; set; }

		FieldRuleBase IRoleMappingRule.FieldRule { get; set; }

		RoleMappingRuleBase IRoleMappingRule.ExceptRule { get; set; }

		public static AnyRoleMappingRule operator |(RoleMappingRuleBase leftContainer, RoleMappingRuleBase rightContainer) =>
			CombineAny(leftContainer, rightContainer);

		public static AllRoleMappingRule operator &(RoleMappingRuleBase leftContainer, RoleMappingRuleBase rightContainer) =>
			CombineAll(leftContainer, rightContainer);

		public static AllRoleMappingRule operator +(RoleMappingRuleBase leftContainer, RoleMappingRuleBase rightContainer) =>
			CombineAll(leftContainer, rightContainer);

		public static ExceptRoleMappingRole operator !(RoleMappingRuleBase leftContainer) =>
			new ExceptRoleMappingRole(leftContainer);

		private static AnyRoleMappingRule CombineAny(RoleMappingRuleBase left, RoleMappingRuleBase right)
		{
			var l = new List<RoleMappingRuleBase>();
			l.AddRangeIfNotNull(AnyOrSelf(left));
			l.AddRangeIfNotNull(AnyOrSelf(right));
			return new AnyRoleMappingRule(l);
		}

		private static AllRoleMappingRule CombineAll(RoleMappingRuleBase left, RoleMappingRuleBase right)
		{
			var l = new List<RoleMappingRuleBase>();
			l.AddRangeIfNotNull(AllOrSelf(left));
			l.AddRangeIfNotNull(AllOrSelf(right));
			return new AllRoleMappingRule(l);
		}

		public static IEnumerable<RoleMappingRuleBase> AllOrSelf(RoleMappingRuleBase rule) =>
			rule is AllRoleMappingRule all ? all.Self.AllRules : new[] {rule};

		public static IEnumerable<RoleMappingRuleBase> AnyOrSelf(RoleMappingRuleBase rule) =>
			rule is AnyRoleMappingRule all ? all.Self.AnyRules : new[] {rule};

		public static implicit operator RoleMappingRuleBase(UsernameRule rule) => new FieldRoleMappingRule(rule);
		public static implicit operator RoleMappingRuleBase(DistinguishedNameRule rule) => new FieldRoleMappingRule(rule);
		public static implicit operator RoleMappingRuleBase(GroupsRule rule) => new FieldRoleMappingRule(rule);
		public static implicit operator RoleMappingRuleBase(MetadataRule rule) => new FieldRoleMappingRule(rule);
		public static implicit operator RoleMappingRuleBase(RealmRule rule) => new FieldRoleMappingRule(rule);
	}
}
