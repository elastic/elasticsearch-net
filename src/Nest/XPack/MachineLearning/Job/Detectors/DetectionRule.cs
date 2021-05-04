// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using Elasticsearch.Net;

namespace Nest
{
	/// <summary>
	/// A machine learning detection rule
	/// </summary>
	[ReadAs(typeof(DetectionRule))]
	public interface IDetectionRule
	{
		/// <summary>
		/// The actions to take when rule is satisfied
		/// </summary>
		[DataMember(Name = "actions")]
		IEnumerable<RuleAction> Actions { get; set; }

		/// <summary>
		/// The conditions of the rule
		/// </summary>
		[DataMember(Name = "conditions")]
		IEnumerable<IRuleCondition> Conditions { get; set; }

		/// <summary>
		/// The scopes of the rule
		/// </summary>
		[DataMember(Name = "scope")]
		IReadOnlyDictionary<Field, FilterRef> Scope { get; set; }
	}

	/// <inheritdoc />
	public class DetectionRule : IDetectionRule
	{
		/// <inheritdoc />
		public IEnumerable<RuleAction> Actions { get; set; }

		/// <inheritdoc />
		public IEnumerable<IRuleCondition> Conditions { get; set; }

		/// <inheritdoc />
		public IReadOnlyDictionary<Field, FilterRef> Scope { get; set; }
	}

	public class DetectionRulesDescriptor
		: DescriptorPromiseBase<DetectionRulesDescriptor, List<IDetectionRule>>
	{
		public DetectionRulesDescriptor() : base(new List<IDetectionRule>()) { }

		private DetectionRulesDescriptor Add(IDetectionRule m)
		{
			PromisedValue.Add(m);
			return this;
		}

		public DetectionRulesDescriptor Rule(Func<DetectionRuleDescriptor, IDetectionRule> selector) =>
			Add(selector.Invoke(new DetectionRuleDescriptor()));
	}

	public class DetectionRuleDescriptor : DescriptorBase<DetectionRuleDescriptor, IDetectionRule>, IDetectionRule
	{
		IEnumerable<RuleAction> IDetectionRule.Actions { get; set; }
		IEnumerable<IRuleCondition> IDetectionRule.Conditions { get; set; }
		IReadOnlyDictionary<Field, FilterRef> IDetectionRule.Scope { get; set; }

		public DetectionRuleDescriptor Actions(IEnumerable<RuleAction> actions) => Assign(actions, (a, v) => a.Actions = v);

		public DetectionRuleDescriptor Actions(params RuleAction[] actions) => Assign(actions, (a, v) => a.Actions = v);

		public DetectionRuleDescriptor Scope<T>(Func<ScopeDescriptor<T>, IPromise<IReadOnlyDictionary<Field, FilterRef>>> selector) where T : class =>
			Assign(selector.Invoke(new ScopeDescriptor<T>()).Value, (a, v) => a.Scope = v);

		public DetectionRuleDescriptor Conditions(Func<RuleConditionsDescriptor, IPromise<List<IRuleCondition>>> selector) =>
			Assign(selector?.Invoke(new RuleConditionsDescriptor())?.Value, (a, v) => a.Conditions = v);
	}

	public class RuleConditionsDescriptor : DescriptorPromiseBase<RuleConditionsDescriptor, List<IRuleCondition>>
	{
		public RuleConditionsDescriptor() : base(new List<IRuleCondition>()) { }

		public RuleConditionsDescriptor Condition(Func<RuleConditionDescriptor, IRuleCondition> selector)
		{
			PromisedValue.AddIfNotNull(selector?.Invoke(new RuleConditionDescriptor()));
			return this;
		}
	}

	public class RuleConditionDescriptor : DescriptorBase<RuleConditionDescriptor, IRuleCondition>, IRuleCondition
	{
		AppliesTo IRuleCondition.AppliesTo { get; set; }
		ConditionOperator IRuleCondition.Operator { get; set; }
		double IRuleCondition.Value { get; set; }

		public RuleConditionDescriptor AppliesTo(AppliesTo appliesTo) => Assign(appliesTo, (a, v) => a.AppliesTo = v);

		public RuleConditionDescriptor Operator(ConditionOperator @operator) => Assign(@operator, (a, v) => a.Operator = v);

		public RuleConditionDescriptor Value(double value) => Assign(value, (a, v) => a.Value = v);
	}

	public class ScopeDescriptor<T> : DescriptorPromiseBase<ScopeDescriptor<T>, IReadOnlyDictionary<Field, FilterRef>> where T : class
	{
		public ScopeDescriptor() : base(new Dictionary<Field, FilterRef>()) { }

		private Dictionary<Field, FilterRef> Dictionary => (Dictionary<Field, FilterRef>)PromisedValue;

		public ScopeDescriptor<T> Scope(Field field, FilterRef filterRef)
		{
			Dictionary[field] = filterRef;
			return this;
		}

		public ScopeDescriptor<T> Scope(Expression<Func<T, object>> field, FilterRef filterRef)
		{
			Dictionary[field] = filterRef;
			return this;
		}
	}

	public class FilterRef
	{
		[DataMember(Name = "filter_id")]
		public Id FilterId { get; set; }

		[DataMember(Name = "filter_type")]
		public RuleFilterType? FilterType { get; set; }
	}

	[StringEnum]
	public enum RuleFilterType
	{
		[EnumMember(Value = "include")]
		Include,

		[EnumMember(Value = "exclude")]
		Exclude
	}

	[ReadAs(typeof(RuleCondition))]
	public interface IRuleCondition
	{
		[DataMember(Name = "applies_to")]
		AppliesTo AppliesTo { get; set; }

		[DataMember(Name = "operator")]
		ConditionOperator Operator { get; set; }

		[DataMember(Name = "value")]
		double Value { get; set; }
	}

	public class RuleCondition : IRuleCondition
	{
		public AppliesTo AppliesTo { get; set; }

		public ConditionOperator Operator { get; set; }

		public double Value { get; set; }
	}

	[StringEnum]
	public enum ConditionOperator
	{
		[EnumMember(Value = "gt")]
		GreaterThan,

		[EnumMember(Value = "gte")]
		GreaterThanOrEqual,

		[EnumMember(Value = "lt")]
		LessThan,

		[EnumMember(Value = "lte")]
		LessThanOrEqual,
	}

	[StringEnum]
	public enum RuleAction
	{
		[EnumMember(Value = "skip_result")]
		SkipResult,

		[EnumMember(Value = "skip_model_update")]
		SkipModelUpdate
	}

	[StringEnum]
	public enum AppliesTo
	{
		[EnumMember(Value = "actual")]
		Actual,

		[EnumMember(Value = "typical")]
		Typical,

		[EnumMember(Value = "diff_from_typical")]
		DiffFromTypical,

		[EnumMember(Value = "time")]
		Time
	}
}
