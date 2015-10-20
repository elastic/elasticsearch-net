using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elasticsearch.Net;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.MockData;
using Xunit;
using static Nest.Static;

namespace Tests.Indices.IndexManagement.TypesExists
{
	[Collection(IntegrationContext.ReadOnly)]
	public class TypeExistsApiTests : ApiIntegrationTestBase<IExistsResponse, ITypeExistsRequest, TypeExistsDescriptor, TypeExistsRequest>
	{
		public TypeExistsApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }
		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.TypeExists(AllIndices, Type<Project>().And<CommitActivity>(), f),
			fluentAsync: (client, f) => client.TypeExistsAsync(AllIndices, Type<Project>().And<CommitActivity>(), f),
			request: (client, r) => client.TypeExists(r),
			requestAsync: (client, r) => client.TypeExistsAsync(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.HEAD;
		protected override string UrlPath => $"/_all/project,commits?ignore_unavailable=true";

		protected override TypeExistsDescriptor NewDescriptor() => new TypeExistsDescriptor(AllIndices, Type<Project>().And<CommitActivity>());

		protected override Func<TypeExistsDescriptor, ITypeExistsRequest> Fluent => d => d
			.IgnoreUnavailable();

		protected override TypeExistsRequest Initializer => new TypeExistsRequest(AllIndices, Type<Project>().And<CommitActivity>())
		{
			IgnoreUnavailable = true
		};

		[I] public async Task Response() => await this.AssertOnAllResponses(r =>
		{
		});
	}
}