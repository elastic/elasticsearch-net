using System;
using System.Collections.Generic;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.MockData;
using Xunit;

namespace Tests.Search.Percolator.RegisterPercolator
{
	[Collection(IntegrationContext.Indexing)]
	public class RegisterPercolatorApiTests : ApiIntegrationTestBase<IRegisterPercolatorResponse, IRegisterPercolatorRequest, RegisterPercolatorDescriptor<Project>, RegisterPercolatorRequest>
	{
		public RegisterPercolatorApiTests(IndexingCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override void OnBeforeCall(IElasticClient client)
		{
			var createIndex = this.Client.CreateIndex(this.CallIsolatedValue + "-index", c=>c
				.Mappings(mm=>mm
					.Map<Project>(m=>m.AutoMap())
				)
			);
			if (!createIndex.IsValid)
				throw new Exception($"Setup: failed to first register percolator {this.CallIsolatedValue}");
		}

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (c, f) => c.RegisterPercolator(this.CallIsolatedValue, f),
			fluentAsync: (c, f) => c.RegisterPercolatorAsync(this.CallIsolatedValue, f),
			request: (c, r) => c.RegisterPercolator(r),
			requestAsync: (c, r) => c.RegisterPercolatorAsync(r)
		);

		protected override int ExpectStatusCode => 201;
		protected override bool ExpectIsValid => true;
		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override string UrlPath => $"/{CallIsolatedValue}-index/.percolator/{this.CallIsolatedValue}";

		protected override RegisterPercolatorDescriptor<Project> NewDescriptor() => new RegisterPercolatorDescriptor<Project>(this.CallIsolatedValue);
		
		protected override object ExpectJson => new
		{
			query = new
			{
				match = new { name = new { query = "nest" } }
			},
			language = "c#",
			commits = 5000,
			project = Project.Instance
		};

		protected override void ExpectResponse(IRegisterPercolatorResponse response)
		{
			response.Created.Should().BeTrue();
			response.Index.Should().NotBeNullOrEmpty();
			response.Type.Should().NotBeNullOrEmpty();
			response.Id.Should().NotBeNullOrEmpty();
			response.Version.Should().BeGreaterThan(0);
		}

		protected override Func<RegisterPercolatorDescriptor<Project>, IRegisterPercolatorRequest> Fluent => r => r
			.Index(this.CallIsolatedValue + "-index")
			.Query(q => q
				.Match(m => m
					.Field(p => p.Name)
					.Query("nest")
				)
			)
			.Metadata(md => md
				.Add("language", "c#")
				.Add("commits", 5000)
				.Add("project", Project.Instance)
			);

		protected override RegisterPercolatorRequest Initializer => new RegisterPercolatorRequest(this.CallIsolatedValue + "-index", this.CallIsolatedValue)
		{
			Query = new QueryContainer(new MatchQuery
			{
				Field = "name",
				Query = "nest"
			}),
			Metadata = new Dictionary<string, object>
			{
				{ "language", "c#" },
				{ "commits", 5000 },
				{ "project", Project.Instance }
			}
		};
	}
}
