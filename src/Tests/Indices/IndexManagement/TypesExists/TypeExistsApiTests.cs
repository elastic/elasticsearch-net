using System;
using Elasticsearch.Net;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.MockData;
using Xunit;
using static Nest.Infer;

namespace Tests.Indices.IndexManagement.TypesExists
{
	[Collection(IntegrationContext.ReadOnly)]
	public class TypeExistsApiTests : ApiIntegrationTestBase<IExistsResponse, ITypeExistsRequest, TypeExistsDescriptor, TypeExistsRequest>
	{
		public TypeExistsApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }
		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.TypeExists(Index<Project>(), Type<Project>().And<CommitActivity>(), f),
			fluentAsync: (client, f) => client.TypeExistsAsync(Index<Project>(), Type<Project>().And<CommitActivity>(), f),
			request: (client, r) => client.TypeExists(r),
			requestAsync: (client, r) => client.TypeExistsAsync(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.HEAD;
		protected override string UrlPath => $"/project/project%2Ccommits?ignore_unavailable=true";

		protected override TypeExistsDescriptor NewDescriptor() => new TypeExistsDescriptor(Index<Project>(), Type<Project>().And<CommitActivity>());

		protected override Func<TypeExistsDescriptor, ITypeExistsRequest> Fluent => d => d
			.IgnoreUnavailable();

		protected override TypeExistsRequest Initializer => new TypeExistsRequest(Index<Project>(), Type<Project>().And<CommitActivity>())
		{
			IgnoreUnavailable = true
		};
	}
}