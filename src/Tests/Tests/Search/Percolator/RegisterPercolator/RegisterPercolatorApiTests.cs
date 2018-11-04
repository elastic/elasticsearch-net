using System;
using System.Collections.Generic;
using Elastic.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework;
using Tests.Framework.Integration;

#pragma warning disable 618 // testing deprecated percolate APIs

namespace Tests.Search.Percolator.RegisterPercolator
{
	[SkipVersion(">5.0.0-alpha1", "deprecated")]
	public class RegisterPercolatorApiTests
		: ApiIntegrationTestBase<WritableCluster, IRegisterPercolatorResponse, IRegisterPercolatorRequest, RegisterPercolatorDescriptor<Project>,
			RegisterPercolatorRequest>
	{
		public RegisterPercolatorApiTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;

		protected override object ExpectJson => new
		{
			query = new
			{
				match = new { name = new { query = "nest" } }
			},
			language = "c#",
			commits = 5000,
			project = Project.InstanceAnonymous
		};

		protected override int ExpectStatusCode => 201;

		protected override Func<RegisterPercolatorDescriptor<Project>, IRegisterPercolatorRequest> Fluent => r => r
			.Index(CallIsolatedValue + "-index")
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

		protected override HttpMethod HttpMethod => HttpMethod.POST;

		protected override RegisterPercolatorRequest Initializer => new RegisterPercolatorRequest(CallIsolatedValue + "-index", CallIsolatedValue)
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

		protected override string UrlPath => $"/{CallIsolatedValue}-index/.percolator/{CallIsolatedValue}";

		protected override void OnBeforeCall(IElasticClient client)
		{
			var createIndex = Client.CreateIndex(CallIsolatedValue + "-index", c => c
				.Mappings(mm => mm
					.Map<Project>(m => m.AutoMap())
				)
			);
			if (!createIndex.IsValid)
				throw new Exception($"Setup: failed to first register percolator {CallIsolatedValue}");
		}

		protected override LazyResponses ClientUsage() => Calls(
			(c, f) => c.RegisterPercolator(CallIsolatedValue, f),
			(c, f) => c.RegisterPercolatorAsync(CallIsolatedValue, f),
			(c, r) => c.RegisterPercolator(r),
			(c, r) => c.RegisterPercolatorAsync(r)
		);

		protected override RegisterPercolatorDescriptor<Project> NewDescriptor() => new RegisterPercolatorDescriptor<Project>(CallIsolatedValue);

		protected override void ExpectResponse(IRegisterPercolatorResponse response)
		{
			response.Created.Should().BeTrue();
			response.Index.Should().NotBeNullOrEmpty();
			response.Type.Should().NotBeNullOrEmpty();
			response.Id.Should().NotBeNullOrEmpty();
			response.Version.Should().BeGreaterThan(0);
		}
	}
}
