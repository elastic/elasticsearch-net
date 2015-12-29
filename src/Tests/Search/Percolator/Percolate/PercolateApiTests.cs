using System;
using System.Linq;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.MockData;
using Xunit;
using static Nest.Static;

namespace Tests.Search.Percolator.Percolate
{
	[Collection(IntegrationContext.Indexing)]
	public class PercolateApiTests : ApiIntegrationTestBase<IPercolateResponse, IPercolateRequest<Project>, PercolateDescriptor<Project>, PercolateRequest<Project>>
	{
		public PercolateApiTests(IndexingCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

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
			doc = Project.InstanceAnonymous
		};

		protected override void ExpectResponse(IPercolateResponse response)
		{
			response.Total.Should().BeGreaterThan(0);
			response.Matches.Should().NotBeNull();
			response.Matches.Count().Should().BeGreaterThan(0);
			var match = response.Matches.First();
			match.Id.Should().Be(PercolatorId);
		}

		private static readonly string PercolatorId = RandomString();

		protected override void OnBeforeCall(IElasticClient client)
		{
			var register = this.Client.RegisterPercolator<Project>(PercolatorId, r => r
				.Index(this.Index)
				.Query(q => q .MatchAll())
			);
		}

		protected override Func<PercolateDescriptor<Project>, IPercolateRequest<Project>> Fluent => c => c
			.Index(this.Index)
			.Document(Project.Instance);

		protected override PercolateRequest<Project> Initializer => new PercolateRequest<Project>(Index, Type<Project>())
		{
			Document = Project.Instance
		};
	}

	[Collection(IntegrationContext.ReadOnly)]
	public class PercolateExistingDocApiTests : ApiTestBase<IPercolateResponse, IPercolateRequest<Project>, PercolateDescriptor<Project>, PercolateRequest<Project>>
	{
		public PercolateExistingDocApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (c, f) => c.Percolate<Project>(p => p.Id(_percId)),
			fluentAsync: (c, f) => c.PercolateAsync<Project>(p => p.Id(_percId)),
			request: (c, r) => c.Percolate(r),
			requestAsync: (c, r) => c.PercolateAsync(r)
		);

		private string _percId = Project.Instance.Name;

		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override string UrlPath => $"project/project/{_percId}/_percolate";

		protected override bool SupportsDeserialization => false;

		protected override Func<PercolateDescriptor<Project>, IPercolateRequest<Project>> Fluent => null;

		protected override PercolateRequest<Project> Initializer => new PercolateRequest<Project>(_percId);
	}
}
