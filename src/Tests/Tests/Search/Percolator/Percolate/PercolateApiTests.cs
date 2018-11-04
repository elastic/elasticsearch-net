using System;
using System.Collections.Generic;
using System.Linq;
using Elastic.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework;
using Tests.Framework.Integration;
using static Nest.Infer;

#pragma warning disable 618 // testing deprecated percolate APIs

namespace Tests.Search.Percolator.Percolate
{
	[SkipVersion(">5.0.0-alpha1", "deprecated")]
	public class PercolateApiTests
		: ApiIntegrationTestBase<WritableCluster, IPercolateResponse, IPercolateRequest<Project>, PercolateDescriptor<Project>,
			PercolateRequest<Project>>
	{
		private static readonly string PercolatorId = RandomString();

		public PercolateApiTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;

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

		protected override int ExpectStatusCode => 200;

		protected override Func<PercolateDescriptor<Project>, IPercolateRequest<Project>> Fluent => c => c
			.Index(Index)
			.Document(Project.Instance)
			.Query(q => q.MatchAll())
			.Size(10)
			.Sort(s => s.Descending(SortSpecialField.Score))
			.TrackScores();

		protected override HttpMethod HttpMethod => HttpMethod.POST;

		protected override PercolateRequest<Project> Initializer => new PercolateRequest<Project>(Index, Type<Project>())
		{
			Document = Project.Instance,
			Query = new QueryContainer(new MatchAllQuery()),
			Size = 10,
			Sort = new List<ISort> { new SortField { Field = "_score", Order = SortOrder.Descending } },
			TrackScores = true
		};

		protected override bool SupportsDeserialization => false;
		protected override string UrlPath => $"/{Index}/project/_percolate";

		private string Index => CallIsolatedValue + "-index";

		protected override LazyResponses ClientUsage() => Calls(
			(c, f) => c.Percolate(f),
			(c, f) => c.PercolateAsync(f),
			(c, r) => c.Percolate(r),
			(c, r) => c.PercolateAsync(r)
		);

		protected override PercolateDescriptor<Project> NewDescriptor() => new PercolateDescriptor<Project>(Index, typeof(Project));

		protected override void ExpectResponse(IPercolateResponse response)
		{
			response.Total.Should().BeGreaterThan(0);
			response.Matches.Should().NotBeNull();
			response.Matches.Count().Should().BeGreaterThan(0);
			var match = response.Matches.First();
			match.Id.Should().Be(PercolatorId);
			match.Score.Should().Be(1);
		}

		protected override void OnBeforeCall(IElasticClient client)
		{
			var register = Client.RegisterPercolator<Project>(PercolatorId, r => r
				.Index(Index)
				.Query(q => q.MatchAll())
			);

			Client.Refresh(Index);
		}
	}

	public class PercolateExistingDocApiTests
		: ApiTestBase<ReadOnlyCluster, IPercolateResponse, IPercolateRequest<Project>, PercolateDescriptor<Project>, PercolateRequest<Project>>
	{
		private readonly string _percolateId = Project.Instance.Name;

		public PercolateExistingDocApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override Func<PercolateDescriptor<Project>, IPercolateRequest<Project>> Fluent => null;

		protected override HttpMethod HttpMethod => HttpMethod.POST;

		protected override PercolateRequest<Project> Initializer => new PercolateRequest<Project>(_percolateId);

		protected override bool SupportsDeserialization => false;
		protected override string UrlPath => $"project/project/{UrlEncode(_percolateId)}/_percolate";

		protected override LazyResponses ClientUsage() => Calls(
			(c, f) => c.Percolate<Project>(p => p.Id(_percolateId)),
			(c, f) => c.PercolateAsync<Project>(p => p.Id(_percolateId)),
			(c, r) => c.Percolate(r),
			(c, r) => c.PercolateAsync(r)
		);
	}
}
