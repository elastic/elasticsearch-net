using System;
using System.Collections.Generic;
using System.Linq;
using Elastic.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Core.ManagedElasticsearch.NodeSeeders;
using Tests.Domain;
using Tests.Framework;
using Tests.Framework.Integration;

#pragma warning disable 618 // testing deprecated percolate APIs

namespace Tests.Search.Percolator.MultiPercolate
{
	[SkipVersion(">5.0.0-alpha1", "Deprecated. percolation changed in 5 to support percolate queries. Remove?")]
	public class MultiPercolateApiTests
		: ApiIntegrationTestBase<WritableCluster, IMultiPercolateResponse, IMultiPercolateRequest, MultiPercolateDescriptor, MultiPercolateRequest>
	{
		public MultiPercolateApiTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		public string Index => CallIsolatedValue + "-index";
		protected override bool ExpectIsValid => true;

		protected override object ExpectJson => new object[]
		{
			new Dictionary<string, object> { { "percolate", new { } } },
			new { doc = Project.InstanceAnonymous },
			new Dictionary<string, object> { { "count", new { } } },
			new { doc = Project.InstanceAnonymous },
		};

		protected override int ExpectStatusCode => 200;

		protected override Func<MultiPercolateDescriptor, IMultiPercolateRequest> Fluent => m => m
			.Index(Index)
			.Type(typeof(Project))
			.Percolate<Project>(p => p.Document(Project.Instance).Index(Index))
			.Count<Project>(p => p.Document(Project.Instance).Index(Index));

		protected override HttpMethod HttpMethod => HttpMethod.POST;

		protected override MultiPercolateRequest Initializer => new MultiPercolateRequest(Index, typeof(Project))
		{
			Percolations = new List<IPercolateOperation>
			{
				new PercolateRequest<Project>(Project.Instance, Index),
				new PercolateCountRequest<Project>(Project.Instance, Index)
			}
		};

		protected override bool SupportsDeserialization => false;
		protected override string UrlPath => $"/{Index}/project/_mpercolate";

		protected override void OnBeforeCall(IElasticClient client)
		{
			var createIndex = Client.CreateIndex(Index, c => c
				.Mappings(mm => mm
					.Map<Project>(m => m.Properties(DefaultSeeder.ProjectProperties))
				)
			);
			Client.ClusterHealth(h => h.WaitForStatus(WaitForStatus.Yellow).Index(Index));
			Client.Index(Project.Instance, s => s.Index(Index).Refresh(Refresh.True));
			var registerPercolator = Client.RegisterPercolator<Project>("match_all", r => r
				.Index(Index)
				.Query(q => q.MatchAll())
			);

			Client.Refresh(Index);
		}

		protected override LazyResponses ClientUsage() => Calls(
			(c, f) => c.MultiPercolate(f),
			(c, f) => c.MultiPercolateAsync(f),
			(c, r) => c.MultiPercolate(r),
			(c, r) => c.MultiPercolateAsync(r)
		);

		protected override void ExpectResponse(IMultiPercolateResponse response)
		{
			var responses = response.Responses.ToList();
			responses.Should().HaveCount(2);
			foreach (var r in responses)
			{
				r.ShouldBeValid();
				r.Total.Should().BeGreaterThan(0);
			}

			responses[0].Matches.Should().NotBeEmpty().And.HaveCount(1);
			foreach (var m in responses[0].Matches)
			{
				m.Id.Should().NotBeNullOrEmpty();
				m.Index.Should().NotBeNullOrEmpty();
			}
		}
	}
}
