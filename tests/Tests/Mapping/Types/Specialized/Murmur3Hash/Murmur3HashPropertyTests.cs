// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Mapping.Types.Specialized.Murmur3Hash
{
	public class Murmur3HashPropertyTests : PropertyTestsBase
	{
		public Murmur3HashPropertyTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override object ExpectJson => new
		{
			properties = new
			{
				name = new
				{
					type = "murmur3"
				}
			}
		};

		protected override Func<PropertiesDescriptor<Project>, IPromise<IProperties>> FluentProperties => f => f
			.Murmur3Hash(s => s
				.Name(p => p.Name)
			);


		protected override IProperties InitializerProperties => new Properties
		{
			{ "name", new Murmur3HashProperty() }
		};
	}
}
