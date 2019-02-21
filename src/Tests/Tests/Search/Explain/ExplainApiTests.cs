using System;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
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
			.SourceEnabled()
			.Query(q => q
				.Match(m => m
					.Field(p => p.Name)
					.Query(Project.Instance.Name)
				)
			);

		protected override HttpMethod HttpMethod => HttpMethod.POST;

		protected override ExplainRequest<Project> Initializer => new ExplainRequest<Project>(_project)
		{
			SourceEnabled = true,
			Query = new QueryContainer(new MatchQuery
			{
				Field = "name",
				Query = Project.Instance.Name
			})
		};

		protected override bool SupportsDeserialization => false;

		protected override string UrlPath =>
			$"/project/_explain/{U(Project.Instance.Name)}?_source=true";

		protected override LazyResponses ClientUsage() => Calls(
			(c, f) => c.Explain(_project, f),
			(c, f) => c.ExplainAsync(_project, f),
			(c, r) => c.Explain(r),
			(c, r) => c.ExplainAsync(r)
		);

		protected override ExplainDescriptor<Project> NewDescriptor() => new ExplainDescriptor<Project>(_project);

		protected override void ExpectResponse(IExplainResponse<Project> response)
		{
			response.IsValid.Should().BeTrue();
			response.Matched.Should().BeTrue();
			response.Get.Should().NotBeNull();
			response.Get.Source.ShouldAdhereToSourceSerializerWhenSet();
		}
	}
}
