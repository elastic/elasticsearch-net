using System;
using Elasticsearch.Net;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;

namespace Tests.Mapping.Types.Core.TokenCount
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
					index = false,
					boost = 1.2,
					null_value = 0.0
				}
			}
		};

		protected override Func<PropertiesDescriptor<Project>, IPromise<IProperties>> FluentProperties => f => f
				.TokenCount(s => s
					.Name(p => p.Name)
					.Analyzer("standard")
					.Index(false)
					.Boost(1.2)
					.NullValue(0.0)
				);


		protected override IProperties InitializerProperties => new Properties
		{
			{ "name", new TokenCountProperty
				{
					Index = false,
					Analyzer = "standard",
					Boost = 1.2,
					NullValue = 0.0
				}
			}
		};
	}
}
