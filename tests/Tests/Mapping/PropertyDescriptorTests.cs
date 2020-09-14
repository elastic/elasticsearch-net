// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Linq;
using System.Reflection;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using FluentAssertions;
using Nest;

namespace Tests.Mapping
{
	public class PropertyDescriptorTests
	{
		[U] public void IPropertiesDescriptorImplementsAllPropertyMethodsOfPropertiesDescriptor()
		{
			var concreteMethodNames =
				from m in typeof(PropertiesDescriptor<>).GetMethods()
				where m.Name != "Scalar"
				where m.ReturnType == typeof(PropertiesDescriptor<>)
				where m.IsGenericMethod == false
				let parameters = m.GetParameters()
				where parameters.Length > 0
				let firstParameter = parameters[0].ParameterType
				where firstParameter.IsConstructedGenericType
				where firstParameter.GetGenericTypeDefinition() == typeof(Func<,>)
				select m.Name;

			var interfaceMethodNames =
				from m in typeof(IPropertiesDescriptor<,>).GetMethods()
				where m.Name != "Scalar"
				where m.ReturnType == typeof(IPropertiesDescriptor<,>).GetGenericArguments()[1]
				where m.IsGenericMethod == false
				let parameters = m.GetParameters()
				where parameters.Length > 0
				let firstParameter = parameters[0].ParameterType
				where firstParameter.IsConstructedGenericType
				where firstParameter.GetGenericTypeDefinition() == typeof(Func<,>)
				select m.Name;

			concreteMethodNames.Except(interfaceMethodNames).Should().BeEmpty();
		}

		[U] public void IPropertiesDescriptorImplementsAPropertyMethodsForAllIPropertyTypes()
		{
			var selectorInterfaces =
				from m in typeof(PropertiesDescriptor<>).GetMethods()
				where m.Name != "Scalar"
				where m.ReturnType == typeof(PropertiesDescriptor<>)
				where m.IsGenericMethod == false
				let parameters = m.GetParameters()
				where parameters.Length > 0
				let firstParameter = parameters[0].ParameterType
				where firstParameter.IsConstructedGenericType
				where firstParameter.GetGenericTypeDefinition() == typeof(Func<,>)
				let selectorInterface = firstParameter.GetGenericArguments()[1]
				select selectorInterface;

			var exclude = new[]
			{
				typeof(IProperty),
				typeof(ICoreProperty),
				typeof(IDocValuesProperty),
			};

			var propertyTypes =
				from t in typeof(IProperty).Assembly.GetTypes()
				where typeof(IProperty).IsAssignableFrom(t)
				where t.IsInterface
				where !exclude.Contains(t)
				select t;

			selectorInterfaces.Except(propertyTypes).Should().BeEmpty();
		}
	}
}
