// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using System.Linq;
using Elastic.Transport;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;
using static Nest.Infer;

namespace Tests.Document.Single.TermVectors
{
	public class TermVectorsApiTests
		: ApiIntegrationTestBase<ReadOnlyCluster, TermVectorsResponse, ITermVectorsRequest<Project>, TermVectorsDescriptor<Project>,
			TermVectorsRequest<Project>>
	{
		public TermVectorsApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;

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

		protected override int ExpectStatusCode => 200;

		protected override Func<TermVectorsDescriptor<Project>, ITermVectorsRequest<Project>> Fluent => d => d
			.Id(Id(Project.Instance))
			.Routing(Project.Instance.Name)
			.Offsets()
			.Filter(f => f
				.MaximimumNumberOfTerms(3)
				.MinimumTermFrequency(1)
				.MaximumTermFrequency(10)
				.MinimumDocumentFrequency(1)
				.MaximumDocumentFrequency(int.MaxValue)
				.MinimumWordLength(0)
				.MaximumWordLength(200)
			);

		protected override HttpMethod HttpMethod => HttpMethod.POST;

		protected override TermVectorsRequest<Project> Initializer => new TermVectorsRequest<Project>((Id)Project.Instance.Name)
		{
			Routing = Project.Instance.Name,
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

		protected override bool SupportsDeserialization => false;
		protected override string UrlPath => $"/project/_termvectors/{U(Project.Instance.Name)}?offsets=true&routing={U(Project.Instance.Name)}";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.TermVectors(f),
			(client, f) => client.TermVectorsAsync(f),
			(client, r) => client.TermVectors(r),
			(client, r) => client.TermVectorsAsync(r)
		);

		protected override TermVectorsDescriptor<Project> NewDescriptor() => new TermVectorsDescriptor<Project>(typeof(Project));

		protected override void ExpectResponse(TermVectorsResponse response)
		{
			response.ShouldBeValid();

			response.TermVectors.Should().NotBeEmpty();
			response.Found.Should().BeTrue();
			response.Version.Should().Be(1);
			response.Id.Should().NotBeNullOrEmpty();
			response.Index.Should().NotBeNullOrEmpty();

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
			termvector.Terms.First().Value.Score.Should().BeGreaterThan(0);
		}
	}
}
