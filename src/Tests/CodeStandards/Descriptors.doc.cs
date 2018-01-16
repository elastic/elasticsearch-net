using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Nest;
using Tests.Framework;
using System.Reflection;

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

		[U] public void DescriptorMethodsTakingSingleValueTypeShouldBeNullable()
		{
			var descriptors =
				from t in typeof(DescriptorBase<,>).Assembly().Types()
				where t.IsClass() && typeof(IDescriptor).IsAssignableFrom(t)
				where !t.IsAbstract()
				select t;

			var breakingDescriptors = new List<string>();

			foreach (var descriptor in descriptors)
			{
				foreach (var descriptorMethod in descriptor.Methods().Where(
					m => m.GetParameters().Length == 1 &&
					m.GetParameters().Any(p => p.ParameterType.IsValueType())))
				{
					foreach (var parameter in descriptorMethod.GetParameters())
					{
						if (!parameter.ParameterType.IsGenericType() || parameter.ParameterType.GetGenericTypeDefinition() != typeof(Nullable<>))
							breakingDescriptors.Add($"{parameter.Name} on method {descriptorMethod.Name} of {descriptor.FullName} is not nullable");
					}
				}
			}

			breakingDescriptors.Should().BeEmpty();
		}

		//TODO methods taking params should also have a version taking IEnumerable

		//TODO methods named Index or Indices that

		//TODO some interfaces are implemented by both requests as well isolated classes to be used elsewhere in the DSL
		//We need to write tests that these have the same public methods so we do not accidentally add it without adding it to the interface

		//Write a tests that all properties of type QueryContainer have the same json converters set

		//TODO write tests that request expose QueryContainer/AggregationContainer not their interfaces
	}
}
