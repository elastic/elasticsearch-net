using Nest;
using System;
using System.Collections.Generic;
using Tests.Domain;
using Tests.Framework;

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
