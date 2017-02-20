using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using FluentAssertions;
using FluentAssertions.Common;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;
using Tests.Framework.MockData;
using Xunit;
using static Nest.Infer;

namespace Tests.Search.Percolator.Percolate
{
	public class PercolateApiTests : ApiIntegrationTestBase<WritableCluster, IPercolateResponse, IPercolateRequest<Project>, PercolateDescriptor<Project>, PercolateRequest<Project>>
	{
		public PercolateApiTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		private string Index => this.CallIsolatedValue + "-index";

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (c, f) => c.Percolate(f),
			fluentAsync: (c, f) => c.PercolateAsync(f),
			request: (c, r) => c.Percolate(r),
			requestAsync: (c, r) => c.PercolateAsync(r)
		);

		protected override int ExpectStatusCode => 200;
		protected override bool ExpectIsValid => true;
		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override string UrlPath => $"/{this.Index}/project/_percolate";

		protected override PercolateDescriptor<Project> NewDescriptor() => new PercolateDescriptor<Project>(this.Index, typeof(Project));

		protected override bool SupportsDeserialization => false;

		protected override object ExpectJson => new
		{
			doc = Project.InstanceAnonymous,
			query = new
			{
				match_all = new { }
			},
			size = 10,
			sort = new[]
			{
				new
				{
					_score = new
					{
						order = "desc"
					}
				}
			},
			track_scores = true
		};

		protected override void ExpectResponse(IPercolateResponse response)
		{
			response.Total.Should().BeGreaterThan(0);
			response.Matches.Should().NotBeNull();
			response.Matches.Count().Should().BeGreaterThan(0);
			var match = response.Matches.First();
			match.Id.Should().Be(PercolatorId);
			match.Score.Should().Be(1);
		}

		private static readonly string PercolatorId = RandomString();

		protected override void OnBeforeCall(IElasticClient client)
		{
			var register = this.Client.RegisterPercolator<Project>(PercolatorId, r => r
				.Index(this.Index)
				.Query(q => q.MatchAll())
			);

			this.Client.Refresh(this.Index);
		}

		protected override Func<PercolateDescriptor<Project>, IPercolateRequest<Project>> Fluent => c => c
			.Index(this.Index)
			.Document(Project.Instance)
			.Query(q => q.MatchAll())
			.Size(10)
			.Sort(s => s.Descending(SortSpecialField.Score))
			.TrackScores()
		;

		protected override PercolateRequest<Project> Initializer => new PercolateRequest<Project>(Index, Type<Project>())
		{
			Document = Project.Instance,
			Query = new QueryContainer(new MatchAllQuery()),
			Size = 10,
			Sort = new List<ISort> { new SortField { Field = "_score", Order = SortOrder.Descending } },
			TrackScores = true
		};
	}

	public class PercolateExistingDocApiTests : ApiTestBase<ReadOnlyCluster, IPercolateResponse, IPercolateRequest<Project>, PercolateDescriptor<Project>, PercolateRequest<Project>>
	{
		public PercolateExistingDocApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (c, f) => c.Percolate<Project>(p => p.Id(_percolateId)),
			fluentAsync: (c, f) => c.PercolateAsync<Project>(p => p.Id(_percolateId)),
			request: (c, r) => c.Percolate(r),
			requestAsync: (c, r) => c.PercolateAsync(r)
		);

		private readonly string _percolateId = Project.Instance.Name;

		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override string UrlPath => $"project/project/{Uri.EscapeDataString(_percolateId)}/_percolate";

		protected override bool SupportsDeserialization => false;

		protected override Func<PercolateDescriptor<Project>, IPercolateRequest<Project>> Fluent => null;

		protected override PercolateRequest<Project> Initializer => new PercolateRequest<Project>(_percolateId);
	}
}
