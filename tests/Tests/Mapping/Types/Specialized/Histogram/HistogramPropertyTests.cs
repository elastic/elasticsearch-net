// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Mapping.Types.Specialized.Histogram
{
	[SkipVersion("<7.6.0", "Introduced in 7.6.0")]
	public class HistogramPropertyTests : PropertyTestsBase
	{
		public HistogramPropertyTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override object ExpectJson => new
		{
			properties = new
			{
				name = new
				{
					type = "histogram",
					ignore_malformed = true,
				}
			}
		};

		protected override Func<PropertiesDescriptor<Project>, IPromise<IProperties>> FluentProperties => f => f
			.Histogram(s => s
				.Name(p => p.Name)
				.IgnoreMalformed()
			);


		protected override IProperties InitializerProperties => new Properties
		{
			{
				"name", new HistogramProperty
				{
					IgnoreMalformed = true,
				}
			}
		};
	}
}
