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
using System.Linq;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using FluentAssertions;
using Nest;

namespace Tests.Mapping
{
	public class PropertyDescriptorTests
	{
		[U] public void PropertiesDescriptorImplementsAllPropertyMethodsOfPropertiesDescriptor()
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

		[U] public void PropertiesDescriptorImplementsAPropertyMethodsForAllIPropertyTypes()
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
