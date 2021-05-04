// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Mapping.Types.Core.Range.IntegerRange
{
	[SkipVersion("<5.2.0", "dedicated range types is a new 5.2.0 feature")]
	public class IntegerRangePropertyTests : PropertyTestsBase
	{
		public IntegerRangePropertyTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override object ExpectJson => new
		{
			properties = new
			{
				ranges = new
				{
					type = "object",
					properties = new
					{
						integers = new
						{
							type = "integer_range",
							store = true,
							index = false,
							coerce = true
						}
					}
				}
			}
		};

		protected override Func<PropertiesDescriptor<Project>, IPromise<IProperties>> FluentProperties => f => f
			.Object<Ranges>(m => m
				.Name(p => p.Ranges)
				.Properties(props => props
					.IntegerRange(n => n
						.Name(p => p.Integers)
						.Store()
						.Index(false)
						.Coerce()
					)
				)
			);


		protected override IProperties InitializerProperties => new Properties
		{
			{
				"ranges", new ObjectProperty
				{
					Properties = new Properties
					{
						{
							"integers", new IntegerRangeProperty
							{
								Store = true,
								Index = false,
								Coerce = true
							}
						}
					}
				}
			}
		};
	}
}
