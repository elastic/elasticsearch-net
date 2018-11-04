using System;
using Elastic.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using Nest;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework;
using Tests.Framework.Integration;

namespace Tests.Document.Single.SourceExists
{
	[SkipVersion("<5.4.0", "API was documented from 5.4.0 and over")]
	public class SourceExistsApiTests
		: ApiIntegrationTestBase<WritableCluster, IExistsResponse, ISourceExistsRequest, SourceExistsDescriptor<Project>, SourceExistsRequest<Project>
		>
	{
		public SourceExistsApiTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;

		protected override Func<SourceExistsDescriptor<Project>, ISourceExistsRequest> Fluent => null;
		protected override HttpMethod HttpMethod => HttpMethod.HEAD;
		protected override SourceExistsRequest<Project> Initializer => new SourceExistsRequest<Project>(CallIsolatedValue);

		protected override bool SupportsDeserialization => false;
		protected override string UrlPath => $"/project/project/{CallIsolatedValue}/_source";

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			foreach (var id in values.Values)
				Client.Index(Project.Instance, i => i.Id(id));
		}

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.SourceExists<Project>(CallIsolatedValue),
			(client, f) => client.SourceExistsAsync<Project>(CallIsolatedValue),
			(client, r) => client.SourceExists(r),
			(client, r) => client.SourceExistsAsync(r)
		);
	}

	public class SourceExistsNotFoundApiTests
		: ApiIntegrationTestBase<WritableCluster, IExistsResponse, ISourceExistsRequest, SourceExistsDescriptor<Project>, SourceExistsRequest<Project>
		>
	{
		public SourceExistsNotFoundApiTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => false;
		protected override int ExpectStatusCode => 404;

		protected override Func<SourceExistsDescriptor<Project>, ISourceExistsRequest> Fluent => null;
		protected override HttpMethod HttpMethod => HttpMethod.HEAD;
		protected override SourceExistsRequest<Project> Initializer => new SourceExistsRequest<Project>(Doc(CallIsolatedValue));

		protected override bool SupportsDeserialization => false;
		protected override string UrlPath => $"/{IndexWithNoSource.Name}/project/{CallIsolatedValue}/_source";

		private static IndexName IndexWithNoSource { get; } = "project-with-no-source";

		private static DocumentPath<Project> Doc(string id) => new DocumentPath<Project>(id).Index(IndexWithNoSource);

		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			var index = client.CreateIndex(IndexWithNoSource, i => i
				.Mappings(m => m
					.Map<Project>(mm => mm
						.SourceField(sf => sf.Enabled(false))
					)
				)
			);
			index.ShouldBeValid();

			foreach (var id in values.Values)
				Client.Index(Project.Instance, i => i.Id(id).Index(IndexWithNoSource));
		}

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.SourceExists<Project>(Doc(CallIsolatedValue)),
			(client, f) => client.SourceExistsAsync<Project>(Doc(CallIsolatedValue)),
			(client, r) => client.SourceExists(r),
			(client, r) => client.SourceExistsAsync(r)
		);
	}
}
