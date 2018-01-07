using System;
using System.Linq;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Framework;
using Tests.Framework.Integration;
using Tests.Framework.ManagedElasticsearch.Clusters;
using Tests.Framework.MockData;
using Xunit;
using static Nest.Infer;

namespace Tests.Document.Single.TermVectors
{
	public class TermVectorsApiTests : ApiIntegrationTestBase<ReadOnlyCluster, ITermVectorsResponse, ITermVectorsRequest<Project>, TermVectorsDescriptor<Project>, TermVectorsRequest<Project>>
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
		protected override string UrlPath => $"/project/doc/{U(Project.Instance.Name)}/_termvectors?offsets=true&routing={U(Project.Routing)}";

		protected override bool SupportsDeserialization => false;

		protected override object ExpectJson => new
		{
			filter = new
			{
				max_num_terms = 3,
				min_term_freq = 1,
				max_term_freq = 10,
				min_doc_freq = 1,
				max_doc_freq = int.MaxValue,
				min_word_length = 0,
				max_word_length = 200
			}
		};

		protected override TermVectorsDescriptor<Project> NewDescriptor() => new TermVectorsDescriptor<Project>(typeof (Project), typeof (Project));

		protected override Func<TermVectorsDescriptor<Project>, ITermVectorsRequest<Project>> Fluent => d=>d
			.Id(Id(Project.Instance))
			.Routing(Project.Routing)
			.Offsets()
			.Filter(f => f
				.MaximimumNumberOfTerms(3)
				.MinimumTermFrequency(1)
				.MaximumTermFrequency(10)
				.MinimumDocumentFrequency(1)
				.MaximumDocumentFrequency(int.MaxValue)
				.MinimumWordLength(0)
				.MaximumWordLength(200)
			)
		;

		protected override TermVectorsRequest<Project> Initializer => new TermVectorsRequest<Project>(Project.Instance.Name)
		{
			Routing = Project.Routing,
			Offsets = true,
			Filter = new TermVectorFilter
			{
				MaximumNumberOfTerms = 3,
				MinimumTermFrequency = 1,
				MaximumTermFrequency = 10,
				MinimumDocumentFrequency = 1,
				MaximumDocumentFrequency = int.MaxValue,
				MinimumWordLength = 0,
				MaximumWordLength = 200
			}
		};

		protected override void ExpectResponse(ITermVectorsResponse response)
		{
			response.ShouldBeValid();

			response.TermVectors.Should().NotBeEmpty();
			response.Found.Should().BeTrue();
			response.Version.Should().Be(1);
			response.Id.Should().NotBeNullOrEmpty();
			response.Index.Should().NotBeNullOrEmpty();
			response.Type.Should().NotBeNullOrEmpty();

			foreach (var termVector in response.TermVectors)
			{
				termVector.Key.Should().NotBeNull();
				termVector.Value.FieldStatistics.Should().NotBeNull();
				termVector.Value.Terms.Should().NotBeEmpty();
			}

			var termvector = response.TermVectors[Field<Project>(p => p.LeadDeveloper.FirstName)];
			AssertTermVector(termvector);
			termvector = response.TermVectors["leadDeveloper.firstName"];
			AssertTermVector(termvector);
		}

		private static void AssertTermVector(TermVector termvector)
		{
			termvector.Should().NotBeNull();
			termvector.FieldStatistics.Should().NotBeNull();
			termvector.FieldStatistics.DocumentCount.Should().BeGreaterThan(0);
			termvector.FieldStatistics.SumOfDocumentFrequencies.Should().BeGreaterThan(0);
			termvector.FieldStatistics.SumOfTotalTermFrequencies.Should().BeGreaterThan(0);
			termvector.Terms.Should().NotBeNull();
		}
	}
}
