using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Xunit;
using Tests.Framework.MockData;

namespace Tests.Search.Explain
{
	[Collection(IntegrationContext.ReadOnly)]
	public class ExplainApiTests
		: ApiIntegrationTestBase<IExplainResponse<Project>, IExplainRequest<Project>, ExplainDescriptor<Project>, ExplainRequest<Project>>
	{
		public ExplainApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (c, f) => c.Explain(_project, f),
			fluentAsync: (c, f) => c.ExplainAsync(_project, f),
			request: (c, r) => c.Explain(r),
			requestAsync: (c, r) => c.ExplainAsync(r)
		);

		protected override int ExpectStatusCode => 200;
		protected override bool ExpectIsValid => true;
		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override string UrlPath => "/project/project/NEST/_explain";

		protected override bool SupportsDeserialization => false;

		protected override ExplainDescriptor<Project> NewDescriptor() => new ExplainDescriptor<Project>(_project);

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

		protected override Func<ExplainDescriptor<Project>, IExplainRequest<Project>> Fluent => e => e
			.Query(q => q
				.Match(m => m
					.OnField(p => p.Name)
					.Query("NEST")
				)
			);

		protected override ExplainRequest<Project> Initializer => new ExplainRequest<Project>(_project)
		{
			Query = new QueryContainer(new MatchQuery
			{
				Field = "name",
				Query = "NEST"
			})
		};
	}
}
