// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Collections.Generic;
using Nest;
using Tests.Domain;
using Tests.IndexModules;

namespace Tests.Mapping.LocalMetadata
{
	public class LocalMetdataUsageTests : UsageTestBase<ITypeMapping, TypeMappingDescriptor<Project>, TypeMapping>
	{
		// Ensure local metadata is never serialized
		protected override object ExpectJson => new
		{
			properties = new
			{
				numberOfCommits = new
				{
					type = "float"
				}
			}
		};

		protected override Func<TypeMappingDescriptor<Project>, ITypeMapping> Fluent => f => f
			.Properties(ps => ps
				.Number(t => t
					.Name(p => p.NumberOfCommits)
					.LocalMetadata(m => m
						.Add("foo", "bar")
					)
				)
			);


		protected override TypeMapping Initializer => new TypeMapping
		{
			Properties = new Properties
			{
				{ "numberOfCommits", new NumberProperty { LocalMetadata = new Dictionary<string, object> { { "foo", "bar" } } } }
			}
		};
	}
}
