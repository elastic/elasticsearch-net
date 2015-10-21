using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Xunit;

namespace Tests.Modules.SnapshotAndRestore.Restore
{
	[Collection(IntegrationContext.ReadOnly)]
	public class RestoreApiTests : ApiTestBase<IRestoreResponse, IRestoreRequest, RestoreDescriptor, RestoreRequest>
	{
		public RestoreApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		private static readonly string _repos = "repository1";
		private static readonly string _snapshot = "snapshot1";


		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.Restore(_repos, _snapshot, f),
			fluentAsync: (client, f) => client.RestoreAsync(_repos, _snapshot, f),
			request: (client, r) => client.Restore(r),
			requestAsync: (client, r) => client.RestoreAsync(r)
		);

		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override string UrlPath => $"/_snapshot/{_repos}/{_snapshot}/_restore";

		protected override bool SupportsDeserialization => false;

		protected override object ExpectJson { get; } = new
		{
			rename_pattern = "nest-(.+)",
			rename_replacement = "nest-restored-$1",
		};

		protected override RestoreDescriptor NewDescriptor() => new RestoreDescriptor(_repos, _snapshot);

		protected override Func<RestoreDescriptor, IRestoreRequest> Fluent => d => d
			.RenamePattern("nest-(.+)")
			.RenameReplacement("nest-restored-$1");

		protected override RestoreRequest Initializer => new RestoreRequest(_repos, _snapshot)
		{
			RenamePattern = "nest-(.+)", 
			RenameReplacement = "nest-restored-$1"
		};

	}
}
