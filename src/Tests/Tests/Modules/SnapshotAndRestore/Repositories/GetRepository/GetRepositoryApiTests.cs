using System;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Modules.SnapshotAndRestore.Repositories.GetRepository
{
	public class GetRepositoryApiTests
		: ApiIntegrationTestBase<WritableCluster, GetRepositoryResponse, IGetRepositoryRequest, GetRepositoryDescriptor, GetRepositoryRequest>
	{
		protected override void IntegrationSetup(IElasticClient client, CallUniqueValues values)
		{
			foreach (var callUniqueValue in values)
			{
				var createRepositoryResponse = client.Snapshot.CreateRepository(callUniqueValue.Value, d => d
					.SourceOnly(so => so
						.FileSystem(fs => fs
							.Settings("some/location", s => s
								.Compress()
								.ConcurrentStreams(5)
								.ChunkSize("64mb")
								.RestoreBytesPerSecondMaximum("100mb")
								.SnapshotBytesPerSecondMaximum("200mb")
							)
						)
					)
				);

				if (!createRepositoryResponse.IsValid)
					throw new Exception($"Error in integration setup: {createRepositoryResponse.DebugInformation}");
			}
		}

		protected override void IntegrationTeardown(IElasticClient client, CallUniqueValues values)
		{
			foreach (var callUniqueValue in values)
			{
				var deleteRepositoryResponse = client.Snapshot.DeleteRepository(callUniqueValue.Value);

				if (!deleteRepositoryResponse.IsValid)
					throw new Exception($"Error in integration teardown: {deleteRepositoryResponse.DebugInformation}");
			}
		}

		public GetRepositoryApiTests(WritableCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override Func<GetRepositoryDescriptor, IGetRepositoryRequest> Fluent => d => d.RepositoryName(CallIsolatedValue);

		protected override HttpMethod HttpMethod => HttpMethod.GET;

		protected override bool ExpectIsValid => true;

		protected override int ExpectStatusCode => 200;
		protected override GetRepositoryRequest Initializer => new GetRepositoryRequest(CallIsolatedValue);

		protected override bool SupportsDeserialization => false;
		protected override string UrlPath => $"/_snapshot/{CallIsolatedValue}";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.Snapshot.GetRepository(f),
			(client, f) => client.Snapshot.GetRepositoryAsync(f),
			(client, r) => client.Snapshot.GetRepository(r),
			(client, r) => client.Snapshot.GetRepositoryAsync(r)
		);

		protected override void ExpectResponse(GetRepositoryResponse response)
		{
			response.ShouldBeValid();

			response.Repositories.Should().ContainKey(CallIsolatedValue);

			var repository = response.Repositories[CallIsolatedValue];
			repository.Type.Should().Be("source");

			var sourceOnlyRespository = repository as ISourceOnlyRepository;
			sourceOnlyRespository.Should().NotBeNull();
			sourceOnlyRespository.DelegateType.Should().Be("fs");
			sourceOnlyRespository.DelegateSettings.Should().BeAssignableTo<IFileSystemRepositorySettings>();
		}
	}
}
