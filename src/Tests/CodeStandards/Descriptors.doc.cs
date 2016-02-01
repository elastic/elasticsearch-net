using System;
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
		* Every descriptor should inherit from `DescriptorBase`, this hides object members from the fluent interface
		*/
		[U]
		public void DescriptorsHaveToBeMarkedWithIDescriptor()
		{
			var notDescriptors = new[] { typeof(ClusterProcessOpenFileDescriptors).Name, "DescriptorForAttribute" };

			var descriptors = from t in typeof(DescriptorBase<,>).Assembly().Types()
							  where t.IsClass() 
								&& t.Name.Contains("Descriptor") 
								&& !notDescriptors.Contains(t.Name)
								&& !t.GetInterfaces().Any(i => i == typeof(IDescriptor))
							  select t.FullName;
			descriptors.Should().BeEmpty();
		}

		/**
		* Methods taking a func should have that func return an interface
		*/
		[U]
		public void SelectorsReturnInterface()
		{
			var descriptors =
				from t in typeof(DescriptorBase<,>).Assembly().Types()
				where t.IsClass() && typeof(IDescriptor).IsAssignableFrom(t)
				select t;
			var selectorMethods =
				from d in descriptors
				from m in d.GetMethods()
				let parameters = m.GetParameters()
				from p in parameters
				let type = p.ParameterType
				let isGeneric = type.IsGeneric()
				where isGeneric
				let isFunc = type.GetGenericTypeDefinition() == typeof(Func<,>)
				where isFunc
                let firstFuncArg = type.GetGenericArguments().First()
                let secondFuncArg = type.GetGenericArguments().Last()
                let isQueryFunc = firstFuncArg.IsGeneric() &&
                    firstFuncArg.GetGenericTypeDefinition() == typeof(QueryContainerDescriptor<>) &&
                    typeof(QueryContainer).IsAssignableFrom(secondFuncArg)
                where !isQueryFunc
                let isFluentDictionaryFunc =
                    firstFuncArg.IsGeneric() &&
                    firstFuncArg.GetGenericTypeDefinition() == typeof(FluentDictionary<,>) &&
                    secondFuncArg.IsGeneric() &&
                    secondFuncArg.GetGenericTypeDefinition() == typeof(FluentDictionary<,>)
                where !isFluentDictionaryFunc
                let lastArgIsNotInterface = !secondFuncArg.IsInterface()
				where lastArgIsNotInterface
				select $"{m.Name} on {m.DeclaringType.Name}";

			selectorMethods.Should().BeEmpty();
		}

		//TODO methods taking params should also have a version taking IEnumerable

		//TODO methods named Index or Indices that 

		//TODO some interfaces are implemented by both requests as well isolated classes to be used elsewhere in the DSL
		//We need to write tests that these have the same public methods so we do not accidentally add it without adding it to the interface

		//Write a tests that all properties of type QueryContainer have the same json converters set

		//TODO write tests that request expose QueryContainer/AggregationContainer not their interfaces

		//TODO descriptors taking a single valuetype parameter should always be nullable
	}
}
