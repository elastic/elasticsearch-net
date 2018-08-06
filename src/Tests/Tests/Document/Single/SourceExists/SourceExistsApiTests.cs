using System;
using Elastic.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;
using Tests.Framework.MockData;

namespace Tests.Document.Single.SourceExists
{
	[SkipVersion("<5.4.0", "API was documented from 5.4.0 and over")]
	public class SourceExistsApiTests : ApiIntegrationTestBase<WritableCluster, IExistsResponse, ISourceExistsRequest, SourceExistsDescriptor<Project>, SourceExistsRequest<Project>>
	{
		public SourceExistsApiTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			foreach (var id in values.Values)
				this.Client.Index(Project.Instance, i=>i.Id(id));
		}

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.SourceExists<Project>(CallIsolatedValue, f),
			fluentAsync: (client, f) => client.SourceExistsAsync<Project>(CallIsolatedValue, f),
			request: (client, r) => client.SourceExists(r),
			requestAsync: (client, r) => client.SourceExistsAsync(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.HEAD;
		protected override string UrlPath => $"/project/doc/{CallIsolatedValue}/_source?routing={U(Project.Routing)}";

		protected override bool SupportsDeserialization => false;

		protected override SourceExistsDescriptor<Project> NewDescriptor() => new SourceExistsDescriptor<Project>(CallIsolatedValue);
		protected override Func<SourceExistsDescriptor<Project>, ISourceExistsRequest> Fluent => d => d.Routing(Project.Routing);
		protected override SourceExistsRequest<Project> Initializer => new SourceExistsRequest<Project>(CallIsolatedValue)
		{
			Routing = Project.Routing
		};

	}

	public class SourceExistsNotFoundApiTests : ApiIntegrationTestBase<WritableCluster, IExistsResponse, ISourceExistsRequest, SourceExistsDescriptor<Project>, SourceExistsRequest<Project>>
	{
		public SourceExistsNotFoundApiTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		private static IndexName IndexWithNoSource { get; } = "project-with-no-source";
		private static DocumentPath<Project> Doc(string id) => new DocumentPath<Project>(id).Index(IndexWithNoSource);

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			var index = client.CreateIndex(IndexWithNoSource, i=>i
				.Mappings(m=>m
					.Map<Project>(mm=>mm
						.SourceField(sf=>sf.Enabled(false))
					)
				)
			);
			index.ShouldBeValid();

			foreach (var id in values.Values)
				this.Client.Index(Project.Instance, i=>i.Id(id).Index(IndexWithNoSource));
		}

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.SourceExists<Project>(Doc(CallIsolatedValue), f),
			fluentAsync: (client, f) => client.SourceExistsAsync<Project>(Doc(CallIsolatedValue), f),
			request: (client, r) => client.SourceExists(r),
			requestAsync: (client, r) => client.SourceExistsAsync(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 404;
		protected override HttpMethod HttpMethod => HttpMethod.HEAD;
		protected override string UrlPath => $"/{IndexWithNoSource.Name}/doc/{CallIsolatedValue}/_source?routing={U(Project.Routing)}";

		protected override bool SupportsDeserialization => false;

		protected override SourceExistsDescriptor<Project> NewDescriptor() => new SourceExistsDescriptor<Project>(Doc(CallIsolatedValue));
		protected override Func<SourceExistsDescriptor<Project>, ISourceExistsRequest> Fluent => d => d.Routing(Project.Routing);
		protected override SourceExistsRequest<Project> Initializer => new SourceExistsRequest<Project>(Doc(CallIsolatedValue))
		{
			Routing = Project.Routing
		};
	}


	public class SourceExistsIndexNotFoundApiTests : ApiIntegrationTestBase<WritableCluster, IExistsResponse, ISourceExistsRequest, SourceExistsDescriptor<Project>, SourceExistsRequest<Project>>
	{
		public SourceExistsIndexNotFoundApiTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		private static IndexName IndexWithNoSource { get; } = "source-no-index";

		protected override SourceExistsDescriptor<Project> NewDescriptor() => new SourceExistsDescriptor<Project>(Doc(CallIsolatedValue));
		protected override Func<SourceExistsDescriptor<Project>, ISourceExistsRequest> Fluent => f=> null;
		protected override SourceExistsRequest<Project> Initializer => new SourceExistsRequest<Project>(Doc(CallIsolatedValue));
		private static DocumentPath<Project> Doc(string id) => new DocumentPath<Project>(id).Index(IndexWithNoSource);

		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.SourceExists<Project>(Doc(CallIsolatedValue), f),
			fluentAsync: (client, f) => client.SourceExistsAsync<Project>(Doc(CallIsolatedValue), f),
			request: (client, r) => client.SourceExists(r),
			requestAsync: (client, r) => client.SourceExistsAsync(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 404;
		protected override HttpMethod HttpMethod => HttpMethod.HEAD;
		protected override string UrlPath => $"/{IndexWithNoSource.Name}/doc/{CallIsolatedValue}/_source";
		protected override bool SupportsDeserialization => false;

	}
}
