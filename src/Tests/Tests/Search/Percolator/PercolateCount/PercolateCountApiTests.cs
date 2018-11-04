using System;
using Elastic.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework;
using Tests.Framework.Integration;

#pragma warning disable 618 // testing deprecated percolate APIs

namespace Tests.Search.Percolator.PercolateCount
{
	[SkipVersion(">5.0.0-alpha1", "deprecated")]
	public class PercolateCountApiTests
		: ApiIntegrationTestBase<ReadOnlyCluster, IPercolateCountResponse, IPercolateCountRequest<Project>, PercolateCountDescriptor<Project>,
			PercolateCountRequest<Project>>
	{
		public PercolateCountApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;

		protected override object ExpectJson => new
		{
			doc = Project.InstanceAnonymous,
			query = new
			{
				range = new
				{
					startedOn = new
					{
						gt = "2014/01/01",
						format = "yyyy/MM/dd"
					}
				}
			}
		};

		protected override int ExpectStatusCode => 200;

		protected override Func<PercolateCountDescriptor<Project>, IPercolateCountRequest<Project>> Fluent => c => c
			.Document(Project.Instance)
			.Query(q => q
				.DateRange(r => r
					.Field(p => p.StartedOn)
					.GreaterThan("2014/01/01")
					.Format("yyyy/MM/dd")
				)
			);

		protected override HttpMethod HttpMethod => HttpMethod.POST;

		protected override PercolateCountRequest<Project> Initializer => new PercolateCountRequest<Project>
		{
			Document = Project.Instance,
			Query = new QueryContainer(new DateRangeQuery
			{
				Field = "startedOn",
				GreaterThan = "2014/01/01",
				Format = "yyyy/MM/dd"
			})
		};

		protected override bool SupportsDeserialization => false;
		protected override string UrlPath => "/project/project/_percolate/count";

		protected override LazyResponses ClientUsage() => Calls(
			(c, f) => c.PercolateCount(f),
			(c, f) => c.PercolateCountAsync(f),
			(c, r) => c.PercolateCount(r),
			(c, r) => c.PercolateCountAsync(r)
		);

		protected override PercolateCountDescriptor<Project> NewDescriptor() =>
			new PercolateCountDescriptor<Project>(typeof(Project), typeof(Project));

		protected override void ExpectResponse(IPercolateCountResponse response) => response.Took.Should().BeGreaterThan(0);
	}

	[SkipVersion(">5.0.0-alpha1", "deprecated")]
	public class PercolateCountExistingDocApiTests
		: ApiIntegrationTestBase<ReadOnlyCluster, IPercolateCountResponse, IPercolateCountRequest<Project>, PercolateCountDescriptor<Project>,
			PercolateCountRequest<Project>>
	{
		private readonly string _percolateId = Project.Instance.Name;

		public PercolateCountExistingDocApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;

		protected override int ExpectStatusCode => 200;

		protected override Func<PercolateCountDescriptor<Project>, IPercolateCountRequest<Project>> Fluent => null;
		protected override HttpMethod HttpMethod => HttpMethod.POST;

		protected override PercolateCountRequest<Project> Initializer => new PercolateCountRequest<Project>(_percolateId);

		protected override bool SupportsDeserialization => false;
		protected override string UrlPath => $"project/project/{UrlEncode(_percolateId)}/_percolate/count";

		protected override LazyResponses ClientUsage() => Calls(
			(c, f) => c.PercolateCount<Project>(p => p.Id(_percolateId)),
			(c, f) => c.PercolateCountAsync<Project>(p => p.Id(_percolateId)),
			(c, r) => c.PercolateCount(r),
			(c, r) => c.PercolateCountAsync(r)
		);
	}
}
