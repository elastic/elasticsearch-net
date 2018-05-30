using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Nest;
using Tests.Framework;
using System.Reflection;
using Elastic.Xunit.XunitPlumbing;

namespace Tests.CodeStandards
{
	public class Descriptors
	{
		/**
		* A descriptor is fluent class that is used to build up state and should therefor return itself on each call.
		* Every descriptor should inherit from `DescriptorBase`, this hides object members from the fluent interface
		*/
		[U]
		public void DescriptorsHaveToBeMarkedWithIDescriptor()
		{
			var notDescriptors = new[] {typeof(ClusterProcessOpenFileDescriptors).Name, "DescriptorForAttribute"};

			var descriptors =
				from t in typeof(DescriptorBase<,>).Assembly().Types()
				where t.IsClass()
				      && t.Name.Contains("Descriptor")
				      && !notDescriptors.Contains(t.Name)
#if __MonoCS__
				      && !t.FullName.Contains("c__AnonStore") //compiler generated
#endif
				      && t.GetInterfaces().All(i => i != typeof(IDescriptor))
				select t.FullName;
			descriptors.Should().BeEmpty();
		}

		/**
		* A selector is fluent class that is used to return state. Each method should return a completed state.
		* Every selector should inherit from `ISelector`, this hides object members from the fluent interface
		*/
		[U]
		public void SelectorsHaveToBeMarkedWithISelector()
		{
			var notSelectors = new[] {typeof(BucketSelectorAggregationDescriptor).Name, typeof(BucketSelectorAggregation).Name};
			var selectors =
				from t in typeof(SelectorBase<>).Assembly().Types()
				where t.IsClass()
				      && t.Name.Contains("Selector")
				      && !notSelectors.Contains(t.Name)
#if __MonoCS__
				      && !t.FullName.Contains("c__AnonStore") //compiler generated
#endif
				      && t.GetInterfaces().All(i => i != typeof(ISelector))
				select t.FullName;
			selectors.Should().BeEmpty();
		}

		/**
		* A descriptor is exposed as a selector func, taking a descriptor and returning the completed state.
		* Methods taking a func should have that func return an interface
		*/
		[U]
		public void DescriptorSelectorsReturnInterface()
		{
			var descriptors =
				from t in typeof(DescriptorBase<,>).Assembly().Types()
				where t.IsClass() && typeof(IDescriptor).IsAssignableFrom(t)
				select t;

			var exclusions = new Dictionary<Type, Type>
			{
				{typeof(QueryContainerDescriptor<>), typeof(QueryContainer)},
				{typeof(ConditionDescriptor), typeof(ConditionContainer)},
				{typeof(TriggerDescriptor), typeof(TriggerContainer)},
				{typeof(TransformDescriptor), typeof(TransformContainer)},
				{typeof(SmoothingModelContainerDescriptor), typeof(SmoothingModelContainer)},
				{typeof(InputDescriptor), typeof(InputContainer)},
				{typeof(RoleMappingRuleDescriptor), typeof(RoleMappingRuleBase)},
				{typeof(FluentDictionary<,>), typeof(FluentDictionary<,>)}
			};

			Func<Type, Type, bool> exclude = (first, second) =>
			{
				var key = first.IsGenericType()
					? first.GetGenericTypeDefinition()
					: first;

				Type value;
				if (!exclusions.TryGetValue(key, out value))
					return false;

				return second.IsGenericType()
					? second.GetGenericTypeDefinition() == value
					: value.IsAssignableFrom(second);
			};

			var selectorMethods =
				from d in descriptors
				from m in d.GetMethods()
				let parameters = m.GetParameters()
				from p in parameters
				let type = p.ParameterType
				let isGeneric = type.IsGenericType()
				where isGeneric
				let isFunc = type.GetGenericTypeDefinition() == typeof(Func<,>)
				where isFunc
				let firstFuncArg = type.GetGenericArguments().First()
				let secondFuncArg = type.GetGenericArguments().Last()
				where !exclude(firstFuncArg, secondFuncArg)
				let lastArgIsNotInterface = !secondFuncArg.IsInterface()
				where lastArgIsNotInterface
				select $"{m.Name} on {m.DeclaringType.Name}";

			selectorMethods.Should().BeEmpty();
		}

		/**
		 * Descriptor methods that assign to a nullable bool property should accept
		 * a nullable bool with a default value
		 */
		[U]
		public void DescriptorMethodsAcceptNullableBoolsForQueriesWithNullableBoolProperties()
		{
			var queries =
				from t in typeof(IQuery).Assembly().Types()
				where t.IsInterface() && typeof(IQuery).IsAssignableFrom(t)
				where t.GetProperties().Any(p => p.PropertyType == typeof(bool?))
				select t;

			var descriptors =
				from t in typeof(DescriptorBase<,>).Assembly().Types()
				where t.IsClass() && typeof(IDescriptor).IsAssignableFrom(t)
				where t.GetInterfaces().Intersect(queries).Any()
				select t;

			var breakingDescriptors = new List<string>();

			foreach (var query in queries)
			{
				var descriptor = descriptors.First(d => query.IsAssignableFrom(d));
				foreach (var boolProperty in query.GetProperties().Where(p => p.PropertyType == typeof(bool?)))
				{
					var descriptorMethod = descriptor.GetMethod(boolProperty.Name);
					if (descriptorMethod == null)
						throw new Exception($"No method for property {boolProperty.Name} on {descriptor.Name}");

					var parameters = descriptorMethod.GetParameters();

					if (!parameters.Any())
						throw new Exception($"No parameter for method {descriptorMethod.Name} on {descriptor.Name}");

					if (parameters.Length > 1)
						throw new Exception($"More than one parameter for method {descriptorMethod.Name} on {descriptor.Name}");

					if (parameters[0].ParameterType != typeof(bool?))
						breakingDescriptors.Add($"{descriptor.FullName} method {descriptorMethod.Name} does not take nullable bool");

					if (!parameters[0].HasDefaultValue)
						breakingDescriptors.Add($"{descriptor.FullName} method {descriptorMethod.Name} does not have a default value");
				}
			}

			breakingDescriptors.Should().BeEmpty();
		}

		[U] public void ProcessorImplementationsNeedProcessorInTheirNames()
		{

			var processors = (
				from t in typeof(IProcessor).Assembly().Types()
				where typeof(IProcessor).IsAssignableFrom(t)
				select t.Name).ToList();

			processors.Should().NotBeEmpty($"expected {nameof(IProcessor)} implementations");
			processors.Should().OnlyContain(p => p.Contains("Processor"));
		}

		[U]
		public void DescriptorMethodsTakingSingleValueTypeShouldBeNullable()
		{
			var methods = from d in YieldAllDescriptors()
				from m in d.GetMethods()
				let ps = m.GetParameters()
				where ps.Length == 1 && ps.Any(pp => pp.ParameterType.IsValueType())
				let p = ps.First()
				let pt = p.ParameterType
				where (!pt.IsGenericType() || pt.GetGenericTypeDefinition() != typeof(Nullable<>))
				let dt = m.DeclaringType.IsGenericType() ? m.DeclaringType.GetGenericTypeDefinition() : m.DeclaringType

				//skips
				where !(new[] {"metric", "indexMetric", "watcherStatsMetric"}.Contains(p.Name))
				where !(m.Name == "Interval" && d == typeof(DateHistogramAggregationDescriptor<>))
				where !(m.Name == "Lang" && dt == typeof(ScriptDescriptorBase<,>))
				where !(m.Name == "Lang" && dt == typeof(StoredScriptDescriptor))
				where !(m.Name == "Lang" && dt == typeof(ScriptQueryDescriptor<>))
				where !(m.Name == "RefreshOnCompleted" && dt == typeof(BulkAllDescriptor<>))
				where !(m.Name == nameof(ReindexDescriptor<object, object>.OmitIndexCreation) && dt == typeof(ReindexDescriptor<,>))
				where !(m.Name == nameof(PutMappingDescriptor<object>.AutoMap))
				where !(m.Name == nameof(PutMappingDescriptor<object>.Dynamic))
				where !(m.Name == "Strict" && dt == typeof(QueryDescriptorBase<,>))
				where !(m.Name == "Verbatim" && dt == typeof(QueryDescriptorBase<,>))
				where !(m.Name == nameof(FunctionScoreQueryDescriptor<object>.ConditionlessWhen) && dt == typeof(FunctionScoreQueryDescriptor<>))
				where !(m.Name == nameof(ScoreFunctionsDescriptor<object>.RandomScore) && dt == typeof(ScoreFunctionsDescriptor<>))
				where !(m.Name == nameof(HighlightFieldDescriptor<object>.Type) && dt == typeof(HighlightFieldDescriptor<>))
				where !(m.Name == nameof(InnerHitsDescriptor<object>.Source) && dt == typeof(InnerHitsDescriptor<>))
				where !(m.Name == nameof(SearchDescriptor<object>.Source) && dt == typeof(SearchDescriptor<>))
				where !(m.Name == nameof(ScoreFunctionsDescriptor<object>.Weight) && dt == typeof(ScoreFunctionsDescriptor<>))
				where !(m.Name == nameof(SortDescriptor<object>.Ascending) && dt == typeof(SortDescriptor<>))
				where !(m.Name == nameof(SortDescriptor<object>.Descending) && dt == typeof(SortDescriptor<>))


				select new {m, d, p};

			var breakingDescriptors = new List<string>();

			foreach (var info in methods)
			{
				var m = info.m;
				var d = info.d;
				var p = info.p;

				breakingDescriptors.Add($"{p.Name} on method {m.Name} of {d.FullName} is not nullable");
			}

			breakingDescriptors.Should().BeEmpty();
		}

		[U] public void NullableBooleansShouldDefaultToTrue()
		{
			var methods = from d in YieldAllDescriptors()
				from m in d.GetMethods()
				let ps = m.GetParameters()
				where ps.Length == 1 && ps.Any(pp => pp.ParameterType.IsValueType())
				let p = ps.First()
				let pt = p.ParameterType
				where pt == typeof(bool?)
				let dt = m.DeclaringType.IsGenericType() ? m.DeclaringType.GetGenericTypeDefinition() : m.DeclaringType
				where !(m.Name == nameof(BooleanPropertyDescriptor<object>.NullValue) && dt == typeof(BooleanPropertyDescriptor<>))
				select new {m, d, p};

			var nullableBools = new List<string>();
			foreach (var info in methods)
			{
				var m = info.m;
				var d = info.d;
				var p = info.p;
				if (!p.HasDefaultValue)
					nullableBools.Add($"bool {p.Name} on method {m.Name} of {d.FullName} is has no default value");

				try
				{

					var b = ((bool?) p.RawDefaultValue);
					if (!b.HasValue)
						nullableBools.Add($"bool {p.Name} on method {m.Name} of {d.FullName} defaults to null");
					else if (!b.Value)
						nullableBools.Add($"bool {p.Name} on method {m.Name} of {d.FullName} default to false");
				}
				catch
				{
					nullableBools.Add($"bool {p.Name} on method {m.Name} of {d.FullName} defaults to unknown");
				}
			}
			nullableBools.Should().BeEmpty();

		}

		private static IEnumerable<Type> YieldAllDescriptors()
		{
			var descriptors =
				from t in typeof(DescriptorBase<,>).Assembly().Types()
				where t.IsClass() && typeof(IDescriptor).IsAssignableFrom(t)
				where !t.IsAbstract()
				select t;
			return descriptors;
		}

		//TODO methods taking params should also have a version taking IEnumerable

		//TODO methods named Index or Indices that

		//TODO some interfaces are implemented by both requests as well isolated classes to be used elsewhere in the DSL
		//We need to write tests that these have the same public methods so we do not accidentally add it without adding it to the interface

		//Write a tests that all properties of type QueryContainer have the same json converters set

		//TODO write tests that request expose QueryContainer/AggregationContainer not their interfaces
	}
}
