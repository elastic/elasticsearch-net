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

namespace Tests.Document.Multiple.MultiTermVectors
{
	public class MultiTermVectorsApiTests : ApiIntegrationTestBase<ReadOnlyCluster, IMultiTermVectorsResponse, IMultiTermVectorsRequest, MultiTermVectorsDescriptor, MultiTermVectorsRequest>
	{
		public MultiTermVectorsApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }
		protected override LazyResponses ClientUsage() => Calls(
			fluent: (client, f) => client.MultiTermVectors(f),
			fluentAsync: (client, f) => client.MultiTermVectorsAsync(f),
			request: (client, r) => client.MultiTermVectors(r),
			requestAsync: (client, r) => client.MultiTermVectorsAsync(r)
		);

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.POST;
		protected override string UrlPath => $"/devs/_mtermvectors";

		protected override bool SupportsDeserialization => false;

		protected override object ExpectJson { get; } = new
		{
			docs = Developer.Developers.Select(p => new
			{
				_index = "devs",
				_type = "developer",
				_id = p.Id,
				payloads = true,
				field_statistics = true,
				term_statistics = true,
				positions = true,
				offsets = true,
				filter = new
				{
					max_num_terms = 3,
					min_term_freq = 1,
					min_doc_freq = 1
				}
			}).Take(2)
		};

		protected override void ExpectResponse(IMultiTermVectorsResponse response)
		{
			response.ShouldBeValid();
			response.Documents.Should().NotBeEmpty().And.HaveCount(2).And.OnlyContain(d => d.Found);
			var termvectorDoc = response.Documents.FirstOrDefault(d => d.TermVectors.Count > 0);

			termvectorDoc.Should().NotBeNull();
			termvectorDoc.Index.Should().NotBeNull();
			termvectorDoc.Type.Should().NotBeNull();
			termvectorDoc.Id.Should().NotBeNull();

			termvectorDoc.TermVectors.Should().NotBeEmpty().And.ContainKey("firstName");
			var vectors = termvectorDoc.TermVectors["firstName"];
			AssertTermVectors(vectors);

			vectors = termvectorDoc.TermVectors[Field<Developer>(p=>p.FirstName)];
			AssertTermVectors(vectors);
		}

		private static void AssertTermVectors(TermVector vectors)
		{
			vectors.Terms.Should().NotBeEmpty();
			foreach (var vectorTerm in vectors.Terms)
			{
				vectorTerm.Key.Should().NotBeNullOrWhiteSpace();
				vectorTerm.Value.Should().NotBeNull();
				vectorTerm.Value.TermFrequency.Should().BeGreaterThan(0);
				vectorTerm.Value.DocumentFrequency.Should().BeGreaterThan(0);
				vectorTerm.Value.TotalTermFrequency.Should().BeGreaterThan(0);
				vectorTerm.Value.Tokens.Should().NotBeEmpty();

				var token = vectorTerm.Value.Tokens.First();
				token.EndOffset.Should().BeGreaterThan(0);
			}
		}

		protected override Func<MultiTermVectorsDescriptor, IMultiTermVectorsRequest> Fluent => d => d
			.Index<Developer>()
			.GetMany<Developer>(Developer.Developers.Select(p => p.Id).Take(2), (p, i) => p
				.FieldStatistics()
				.Payloads()
				.TermStatistics()
				.Positions()
				.Offsets()
				.Filter(f => f
					.MaximimumNumberOfTerms(3)
					.MinimumTermFrequency(1)
					.MinimumDocumentFrequency(1)
				)
			)
		;

		protected override MultiTermVectorsRequest Initializer => new MultiTermVectorsRequest(Index<Developer>())
		{
			Documents = Developer.Developers.Select(p => p.Id).Take(2)
				.Select(n => new MultiTermVectorOperation<Developer>(n)
				{
					FieldStatistics = true,
					Payloads = true,
					TermStatistics = true,
					Positions = true,
					Offsets = true,
					Filter = new TermVectorFilter
					{
						MaximumNumberOfTerms = 3,
						MinimumTermFrequency = 1,
						MinimumDocumentFrequency = 1
					}
				})
		};
	}
}
