using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Xunit;

namespace Tests.Modules.SnapshotAndRestore.Repositories.CreateRepository
{
	[Collection(IntegrationContext.ReadOnly)]
	public class CreateRepositoryApiTests : ApiTestBase<IAcknowledgedResponse, ICreateRepositoryRequest, CreateRepositoryDescriptor, CreateRepositoryRequest>
	{
		public CreateRepositoryApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		private static readonly string _name = "repository1";


		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.CreateRepository(_name, f),
			fluentAsync: (client, f) => client.CreateRepositoryAsync(_name, f),
			request: (client, r) => client.CreateRepository(r),
			requestAsync: (client, r) => client.CreateRepositoryAsync(r)
		);

		protected override HttpMethod HttpMethod => HttpMethod.PUT;
		protected override string UrlPath => $"/_snapshot/{_name}";

		protected override bool SupportsDeserialization => false;

		protected override object ExpectJson { get; } = new
		{
			type = "azure",
			settings = new
			{
				chunk_size = "64mb"
			}
		};

		protected override CreateRepositoryDescriptor NewDescriptor() => new CreateRepositoryDescriptor(_name);

		protected override Func<CreateRepositoryDescriptor, ICreateRepositoryRequest> Fluent => d => d
			.Azure(f=>f.ChunkSize("64mb"));

		protected override CreateRepositoryRequest Initializer => new CreateRepositoryRequest(_name)
		{
			Repository = new AzureRepository() { Settings = new Dictionary<string, object> { { "chunk_size", "64mb" } } }
		};
	}
}
