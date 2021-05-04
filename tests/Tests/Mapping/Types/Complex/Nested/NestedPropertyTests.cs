// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Mapping.Types.Complex.Nested
{
	public class NestedPropertyTests : PropertyTestsBase
	{
		public NestedPropertyTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override object ExpectJson => new
		{
			properties = new
			{
				leadDeveloper = new
				{
					type = "nested",
					include_in_parent = true,
					include_in_root = false,
					dynamic = "strict",
					enabled = true,
					properties = new
					{
						ipAddress = new
						{
							type = "ip"
						}
					}
				}
			}
		};

		protected override Func<PropertiesDescriptor<Project>, IPromise<IProperties>> FluentProperties => f => f
			.Nested<Developer>(n => n
				.Name(p => p.LeadDeveloper)
				.IncludeInParent()
				.IncludeInRoot(false)
				.Dynamic(DynamicMapping.Strict)
				.Enabled()
				.Properties(pps => pps
					.Ip(i => i
						.Name(p => p.IpAddress)
					)
				)
			);

		protected override IProperties InitializerProperties => new Properties
		{
			{
				"leadDeveloper", new NestedProperty
				{
					IncludeInParent = true,
					IncludeInRoot = false,
					Dynamic = DynamicMapping.Strict,
					Enabled = true,
					Properties = new Properties
					{
						{ "ipAddress", new IpProperty() }
					}
				}
			}
		};
	}
}
