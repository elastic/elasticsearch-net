using System;
using Elasticsearch.Net;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework;
using Tests.Framework.Integration;

namespace Tests.Search.Count
{
	public class CountApiTests
		: ApiIntegrationTestBase<ReadOnlyCluster, ICountResponse, ICountRequest, CountDescriptor<Project>, CountRequest<Project>>
	{
		public CountApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;

		protected override object ExpectJson => new
		{
			query = new
			{
				match = new
				{
					name = new
					{
						query = "NEST"
					}
				}
			}
		};

		protected override int ExpectStatusCode => 200;

		protected override Func<CountDescriptor<Project>, ICountRequest> Fluent => c => c
			.Query(q => q
				.Match(m => m
					.Field(p => p.Name)
					.Query("NEST")
				)
			);

		protected override HttpMethod HttpMethod => HttpMethod.POST;

		protected override CountRequest<Project> Initializer => new CountRequest<Project>()
		{
			Query = new QueryContainer(new MatchQuery
			{
				Field = "name",
				Query = "NEST"
			})
		};

		protected override string UrlPath => "/project/_count";

		protected override LazyResponses ClientUsage() => Calls(
			(c, f) => c.Count(f),
			(c, f) => c.CountAsync(f),
			(c, r) => c.Count<Project>(r),
			(c, r) => c.CountAsync<Project>(r)
		);
	}
}
