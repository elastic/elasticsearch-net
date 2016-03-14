using System;
using Elasticsearch.Net;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.MockData;
using Xunit;
using static Nest.Infer;

namespace Tests.Document.Single.TermVectors
{
	[Collection(IntegrationContext.ReadOnly)]
	public class TermVectorsApiTests : ApiIntegrationTestBase<ITermVectorsResponse, ITermVectorsRequest<Project>, TermVectorsDescriptor<Project>, TermVectorsRequest<Project>>
	{
		public TermVectorsApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }
		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.TermVectors(f),
			fluentAsync: (client, f) => client.TermVectorsAsync(f),
			request: (client, r) => client.TermVectors(r),
			requestAsync: (client, r) => client.TermVectorsAsync(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override string UrlPath => $"/project/project/{Uri.EscapeDataString(Project.Instance.Name)}/_termvectors?offsets=true";

		protected override bool SupportsDeserialization => false;

		protected override object ExpectJson => new
		{
			filter = new
			{
				max_num_terms = 3,
				min_term_freq = 1,
				min_doc_freq = 1
			}
		};

		protected override TermVectorsDescriptor<Project> NewDescriptor() => new TermVectorsDescriptor<Project>(typeof (Project), typeof (Project));

		protected override Func<TermVectorsDescriptor<Project>, ITermVectorsRequest<Project>> Fluent => d=>d
			.Id(Id(Project.Instance))
			.Offsets()
			.Filter(f => f
				.MaximimumNumberOfTerms(3)
				.MinimumTermFrequency(1)
				.MinimumDocumentFrequency(1)
			)
		;

		protected override TermVectorsRequest<Project> Initializer => new TermVectorsRequest<Project>(Project.Instance.Name)
		{
			Offsets = true,
			Filter = new TermVectorFilter
			{
				MaximumNumberOfTerms = 3,
				MinimumTermFrequency = 1,
				MinimumDocumentFrequency = 1
			}
		};
	}
}
