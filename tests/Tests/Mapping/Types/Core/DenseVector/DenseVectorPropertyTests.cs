// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Mapping.Types.Core.DenseVector
{
	[SkipVersion("<7.6.0", "Dense Vector property GA in 7.6.0")]
	public class DenseVectorTests : PropertyTestsBase
	{
		public DenseVectorTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override object ExpectJson => new { properties = new { name = new { type = "dense_vector", dims = 2 } } };

		protected override Func<PropertiesDescriptor<Project>, IPromise<IProperties>> FluentProperties => f => f
			.DenseVector(s => s
				.Name(p => p.Name)
				.Dimensions(2)
			);

		protected override IProperties InitializerProperties => new Properties { { "name", new DenseVectorProperty { Dimensions = 2 } } };
	}
}
