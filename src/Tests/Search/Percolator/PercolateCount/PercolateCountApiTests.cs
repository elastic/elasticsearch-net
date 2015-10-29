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

namespace Tests.Search.Percolator.PercolateCount
{
	[Collection(IntegrationContext.ReadOnly)]
	public class PercolateCountApiTests
		: ApiIntegrationTestBase<IPercolateCountResponse, IPercolateCountRequest<Project>, PercolateCountDescriptor<Project>, PercolateCountRequest<Project>>
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
						gt = "2014/01/01"
					}
				}
			}
		};

		protected override Func<PercolateCountDescriptor<Project>, IPercolateCountRequest<Project>> Fluent => c => c
			.Document(Project.Instance)
			.Query(q => q
				.Range(r => r
					.OnField(p => p.StartedOn)
					.Greater(new DateTime(2014, 1, 1), "yyyy/MM/dd")
				)
			);

		protected override PercolateCountRequest<Project> Initializer => new PercolateCountRequest<Project>
		{
			Document = Project.Instance,
			Query = new QueryContainer(new RangeQuery
			{
				Field = "startedOn",
				GreaterThan = "2014/01/01"
			})
        };
	}

	[Collection(IntegrationContext.ReadOnly)]
	public class PercolateCountExistingDocApiTests
		: ApiIntegrationTestBase<IPercolateCountResponse, IPercolateCountRequest<Project>, PercolateCountDescriptor<Project>, PercolateCountRequest<Project>>
	{
		public PercolateCountExistingDocApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (c, f) => c.PercolateCount<Project>(p => p.Id(_percId)),
			fluentAsync: (c, f) => c.PercolateCountAsync<Project>(p => p.Id(_percId)),
			request: (c, r) => c.PercolateCount(r),
			requestAsync: (c, r) => c.PercolateCountAsync(r)
		);

		private int _percId = 1;

		protected override int ExpectStatusCode => 200;
		protected override bool ExpectIsValid => true;
		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override string UrlPath => $"project/project/{_percId}/_percolate/count";

		protected override bool SupportsDeserialization => false;

		protected override Func<PercolateCountDescriptor<Project>, IPercolateCountRequest<Project>> Fluent => null;

		protected override PercolateCountRequest<Project> Initializer => new PercolateCountRequest<Project>(_percId);
	}
}
