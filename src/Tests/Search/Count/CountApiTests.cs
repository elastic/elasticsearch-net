using System;
using Elasticsearch.Net;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.MockData;
using Xunit;

namespace Tests.Search.Count
{
	[Collection(IntegrationContext.ReadOnly)]
	public class CountApiTests
		: ApiIntegrationTestBase<ICountResponse, ICountRequest, CountDescriptor<Project>, CountRequest<Project>>
	{
		public CountApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (c, f) => c.Count(f),
			fluentAsync: (c, f) => c.CountAsync(f),
			request: (c, r) => c.Count<Project>(r),
			requestAsync: (c, r) => c.CountAsync<Project>(r)
		);

		protected override int ExpectStatusCode => 200;
		protected override bool ExpectIsValid => true;
		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override string UrlPath => "/project/project/_count";

		private Project _project = new Project { Name = "NEST" };

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

		protected override Func<CountDescriptor<Project>, ICountRequest> Fluent => c => c
			.Query(q => q
				.Match(m => m
					.Field(p => p.Name)
					.Query("NEST")
				)
			);

		protected override CountRequest<Project> Initializer => new CountRequest<Project>()
		{
			Query = new QueryContainer(new MatchQuery
			{
				Field = "name",
				Query = "NEST"
			})
		};
	}
}
