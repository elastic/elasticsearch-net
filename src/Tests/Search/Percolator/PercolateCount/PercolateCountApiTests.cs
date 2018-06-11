using System;
using Elastic.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;
using Tests.Framework.MockData;
using Xunit;

#pragma warning disable 618 // testing deprecated percolate APIs

namespace Tests.Search.Percolator.PercolateCount
{
	[SkipVersion(">5.0.0-alpha1", "deprecated")]
	public class PercolateCountApiTests
		: ApiIntegrationTestBase<ReadOnlyCluster, IPercolateCountResponse, IPercolateCountRequest<Project>, PercolateCountDescriptor<Project>, PercolateCountRequest<Project>>
	{
		public PercolateCountApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (c, f) => c.PercolateCount(f),
			fluentAsync: (c, f) => c.PercolateCountAsync(f),
			request: (c, r) => c.PercolateCount(r),
			requestAsync: (c, r) => c.PercolateCountAsync(r)
		);

		protected override int ExpectStatusCode => 200;
		protected override bool ExpectIsValid => true;
		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override string UrlPath => "/project/project/_percolate/count";

		protected override PercolateCountDescriptor<Project> NewDescriptor() => new PercolateCountDescriptor<Project>(typeof(Project), typeof(Project));

		protected override bool SupportsDeserialization => false;

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

		protected override void ExpectResponse(IPercolateCountResponse response)
		{
			response.Took.Should().BeGreaterThan(0);
		}

		protected override Func<PercolateCountDescriptor<Project>, IPercolateCountRequest<Project>> Fluent => c => c
			.Document(Project.Instance)
			.Query(q => q
				.DateRange(r => r
					.Field(p => p.StartedOn)
					.GreaterThan("2014/01/01")
					.Format("yyyy/MM/dd")
				)
			);

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
	}

	[SkipVersion(">5.0.0-alpha1", "deprecated")]
	public class PercolateCountExistingDocApiTests
		: ApiIntegrationTestBase<ReadOnlyCluster, IPercolateCountResponse, IPercolateCountRequest<Project>, PercolateCountDescriptor<Project>, PercolateCountRequest<Project>>
	{
		public PercolateCountExistingDocApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (c, f) => c.PercolateCount<Project>(p => p.Id(_percolateId)),
			fluentAsync: (c, f) => c.PercolateCountAsync<Project>(p => p.Id(_percolateId)),
			request: (c, r) => c.PercolateCount(r),
			requestAsync: (c, r) => c.PercolateCountAsync(r)
		);

		private string _percolateId = Project.Instance.Name;

		protected override int ExpectStatusCode => 200;
		protected override bool ExpectIsValid => true;
		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override string UrlPath => $"project/project/{UrlEncode(_percolateId)}/_percolate/count";

		protected override bool SupportsDeserialization => false;

		protected override Func<PercolateCountDescriptor<Project>, IPercolateCountRequest<Project>> Fluent => null;

		protected override PercolateCountRequest<Project> Initializer => new PercolateCountRequest<Project>(_percolateId);
	}
}
