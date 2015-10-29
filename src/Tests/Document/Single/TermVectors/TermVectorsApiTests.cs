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

namespace Tests.Document.Single.TermVectors
{
	[Collection(IntegrationContext.Indexing)]
	public class TermVectorsApiTests : ApiIntegrationTestBase<ITermVectorsResponse, ITermVectorsRequest<Project>, TermVectorsDescriptor<Project>, TermVectorsRequest<Project>>
	{
		public TermVectorsApiTests(IndexingCluster cluster, EndpointUsage usage) : base(cluster, usage) { }
		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.TermVectors(f),
			fluentAsync: (client, f) => client.TermVectorsAsync(f),
			request: (client, r) => client.TermVectors(r),
			requestAsync: (client, r) => client.TermVectorsAsync(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.GET;
		protected override string UrlPath => $"/project/project/{Project.Instance.Name}/_termvectors?offsets=true";

		protected override bool SupportsDeserialization => false;

		protected override TermVectorsDescriptor<Project> NewDescriptor() => new TermVectorsDescriptor<Project>(typeof (Project), typeof (Project));

		protected override Func<TermVectorsDescriptor<Project>, ITermVectorsRequest<Project>> Fluent => d=>d
			.Id(Id(Project.Instance))
			.Offsets()
		;
		protected override TermVectorsRequest<Project> Initializer => new TermVectorsRequest<Project>(Project.Instance.Name)
		{
			Offsets = true,
		};

		[I] public async Task Response() => await this.AssertOnAllResponses(r =>
		{
		});
	}
}
