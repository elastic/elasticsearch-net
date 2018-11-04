using System;
using Elasticsearch.Net;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework;
using Tests.Framework.Integration;

namespace Tests.Search.Explain
{
	public class ExplainApiTests
		: ApiIntegrationTestBase<ReadOnlyCluster, IExplainResponse<Project>, IExplainRequest<Project>, ExplainDescriptor<Project>,
			ExplainRequest<Project>>
	{
		private readonly Project _project = new Project { Name = Project.Instance.Name };

		public ExplainApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;

		protected override object ExpectJson => new
		{
			query = new
			{
				match = new
				{
					name = new
					{
						query = Project.Instance.Name
					}
				}
			}
		};

		protected override int ExpectStatusCode => 200;

		protected override Func<ExplainDescriptor<Project>, IExplainRequest<Project>> Fluent => e => e
			.Query(q => q
				.Match(m => m
					.Field(p => p.Name)
					.Query(Project.Instance.Name)
				)
			);

		protected override HttpMethod HttpMethod => HttpMethod.POST;

		protected override ExplainRequest<Project> Initializer => new ExplainRequest<Project>(_project)
		{
			Query = new QueryContainer(new MatchQuery
			{
				Field = "name",
				Query = Project.Instance.Name
			})
		};

		protected override bool SupportsDeserialization => false;
		protected override string UrlPath => $"/project/project/{UrlEncode(Project.Instance.Name)}/_explain";

		protected override LazyResponses ClientUsage() => Calls(
			(c, f) => c.Explain(_project, f),
			(c, f) => c.ExplainAsync(_project, f),
			(c, r) => c.Explain(r),
			(c, r) => c.ExplainAsync(r)
		);

		protected override ExplainDescriptor<Project> NewDescriptor() => new ExplainDescriptor<Project>(_project);
	}
}
