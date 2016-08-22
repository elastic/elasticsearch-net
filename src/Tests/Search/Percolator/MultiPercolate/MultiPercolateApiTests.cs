using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.MockData;
using Xunit;

#pragma warning disable 618 // testing deprecated percolate APIs

namespace Tests.Search.Percolator.MultiPercolate
{
    [SkipVersion(">5.0.0-alpha1", "Deprecated. percolation changed in 5 to support percolate queries. Remove?")]
    public class MultiPercolateApiTests : ApiIntegrationTestBase<WritableCluster, IMultiPercolateResponse, IMultiPercolateRequest, MultiPercolateDescriptor, MultiPercolateRequest>
	{
		public MultiPercolateApiTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		public string Index => this.CallIsolatedValue + "-index";

		protected override void OnBeforeCall(IElasticClient client)
		{
			var createIndex = this.Client.CreateIndex(this.Index, c=>c
				.Mappings(mm=>mm
					.Map<Project>(m => m.Properties(Seeder.ProjectProperties))
				)
			);
			this.Client.ClusterHealth(h => h.WaitForStatus(WaitForStatus.Yellow).Index(this.Index));
			this.Client.Index(Project.Instance, s => s.Index(this.Index).Refresh());
			var registerPercolator = this.Client.RegisterPercolator<Project>("match_all", r => r
				.Index(this.Index)
				.Query(q => q.MatchAll())
			);

			this.Client.Refresh(this.Index);
		}

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (c, f) => c.MultiPercolate(f),
			fluentAsync: (c, f) => c.MultiPercolateAsync(f),
			request: (c, r) => c.MultiPercolate(r),
			requestAsync: (c, r) => c.MultiPercolateAsync(r)
		);

		protected override int ExpectStatusCode => 200;
		protected override bool ExpectIsValid => true;
		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override string UrlPath => $"/{this.Index}/project/_mpercolate";

		protected override bool SupportsDeserialization => false;

		protected override object ExpectJson => new object[]
		{
			new Dictionary<string, object>{ { "percolate", new {} } },
			new { doc = Project.InstanceAnonymous },
			new Dictionary<string, object>{ { "count", new {} } },
			new { doc = Project.InstanceAnonymous },
		};

		protected override Func<MultiPercolateDescriptor, IMultiPercolateRequest> Fluent => m => m
			.Index(this.Index)
			.Type(typeof(Project))
			.Percolate<Project>(p => p.Document(Project.Instance).Index(this.Index))
			.Count<Project>(p => p.Document(Project.Instance).Index(this.Index));

		protected override MultiPercolateRequest Initializer => new MultiPercolateRequest(this.Index, typeof(Project))
		{
			Percolations = new List<IPercolateOperation>
			{
				new PercolateRequest<Project>(Project.Instance, this.Index),
				new PercolateCountRequest<Project>(Project.Instance, this.Index)
			}
		};

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
			foreach(var m in responses[0].Matches)
			{
				m.Id.Should().NotBeNullOrEmpty();
				m.Index.Should().NotBeNullOrEmpty();
			}
		}
	}
}
