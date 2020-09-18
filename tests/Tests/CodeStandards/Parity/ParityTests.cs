// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using FluentAssertions;
using Nest;

namespace Tests.CodeStandards.Parity
{
	public class ParityTests
	{
		[U] public void FieldTypeHasAllNumberTypes()
		{
			var numberTypes = Enum.GetNames(typeof(NumberType));
			var fieldTypes = Enum.GetNames(typeof(FieldType));

			fieldTypes.Should().Contain(numberTypes);
		}

		[U] public void PropertyVisitorHasVisitMethodForAllPropertyTypes()
		{
			var interfaceType = typeof(IProperty);

			var excludeInterfaceTypes = new HashSet<Type>
			{
				interfaceType,
				typeof(IDocValuesProperty),
				typeof(ICoreProperty),
				typeof(IRangeProperty),
				typeof(IGenericProperty)
			};

			var propertyTypes = interfaceType.Assembly
				.GetTypes()
				.Where(t => t.IsInterface && interfaceType.IsAssignableFrom(t) && !excludeInterfaceTypes.Contains(t));

			var visitMethodTypes = typeof(IPropertyVisitor).GetMethods()
				.Where(m => m.Name == nameof(IPropertyVisitor.Visit) && m.ReturnType == typeof(void))
				.Select(m => m.GetParameters()[0].ParameterType);

			propertyTypes.Except(visitMethodTypes).Should().BeEmpty();
		}

		[U]
		public void NoopPropertyVisitorVisitMethodsAreAllVirtual()
		{
			var methods = typeof(NoopPropertyVisitor).GetMethods()
				.Where(m => m.Name == nameof(NoopPropertyVisitor.Visit));
			methods.Should().OnlyContain(m => m.IsVirtual);
		}
	}
}
