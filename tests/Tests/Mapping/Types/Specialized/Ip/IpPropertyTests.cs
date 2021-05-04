// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Mapping.Types.Specialized.Ip
{
	public class IpPropertyTests : PropertyTestsBase
	{
		public IpPropertyTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override object ExpectJson => new
		{
			properties = new
			{
				name = new
				{
					type = "ip",
					index = false,
					null_value = "127.0.0.1",
					doc_values = true,
					store = true,
				}
			}
		};

		protected override Func<PropertiesDescriptor<Project>, IPromise<IProperties>> FluentProperties => f => f
			.Ip(s => s
				.Name(p => p.Name)
				.Index(false)
				.NullValue("127.0.0.1")
				.DocValues()
				.Store()
			);


		protected override IProperties InitializerProperties => new Properties
		{
			{
				"name", new IpProperty
				{
					Index = false,
					NullValue = "127.0.0.1",
					DocValues = true,
					Store = true,
				}
			}
		};
	}
}
