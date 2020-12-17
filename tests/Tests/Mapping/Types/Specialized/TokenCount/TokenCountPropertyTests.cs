// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Mapping.Types.Specialized.TokenCount
{
	public class TokenCountPropertyTests : PropertyTestsBase
	{
		public TokenCountPropertyTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override object ExpectJson => new
		{
			properties = new
			{
				name = new
				{
					type = "token_count",
					analyzer = "standard",
					enable_position_increments = false,
					index = false,
					null_value = 0.0
				}
			}
		};

		protected override Func<PropertiesDescriptor<Project>, IPromise<IProperties>> FluentProperties => f => f
			.TokenCount(s => s
				.Name(p => p.Name)
				.Analyzer("standard")
				.EnablePositionIncrements(false)
				.Index(false)
				.NullValue(0.0)
			);


		protected override IProperties InitializerProperties => new Properties
		{
			{
				"name", new TokenCountProperty
				{
					Index = false,
					Analyzer = "standard",
					EnablePositionIncrements = false,
					NullValue = 0.0
				}
			}
		};
	}
}
