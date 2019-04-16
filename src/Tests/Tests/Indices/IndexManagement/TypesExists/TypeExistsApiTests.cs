using System;
using Elasticsearch.Net;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework;
using Tests.Framework.Integration;
using static Nest.Infer;

namespace Tests.Indices.IndexManagement.TypesExists
{
	public class TypeExistsApiTests
		: ApiIntegrationTestBase<ReadOnlyCluster, IExistsResponse, ITypeExistsRequest, TypeExistsDescriptor, TypeExistsRequest>
	{
		public TypeExistsApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;

		protected override Func<TypeExistsDescriptor, ITypeExistsRequest> Fluent => d => d
			.IgnoreUnavailable();

		protected override HttpMethod HttpMethod => HttpMethod.HEAD;

		protected override TypeExistsRequest Initializer => new TypeExistsRequest(Index<Project>(), "_doc")
		{
			IgnoreUnavailable = true
		};

		protected override string UrlPath => $"/project/_mapping/_doc?ignore_unavailable=true";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.TypeExists(Index<Project>(), "_doc", f),
			(client, f) => client.TypeExistsAsync(Index<Project>(), "_doc", f),
			(client, r) => client.TypeExists(r),
			(client, r) => client.TypeExistsAsync(r)
		);

		protected override TypeExistsDescriptor NewDescriptor() => new TypeExistsDescriptor(Index<Project>(), "doc");
	}
}
