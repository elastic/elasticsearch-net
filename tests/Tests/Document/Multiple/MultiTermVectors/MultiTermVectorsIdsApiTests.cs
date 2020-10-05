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

namespace Tests.Document.Multiple.MultiTermVectors
{
	public class MultiTermVectorsIdsApiTests
		: ApiIntegrationTestBase<ReadOnlyCluster, MultiTermVectorsResponse, IMultiTermVectorsRequest, MultiTermVectorsDescriptor,
			MultiTermVectorsRequest>
	{
		public MultiTermVectorsIdsApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;

		protected override object ExpectJson { get; } = new
		{
			ids = Developer.Developers.Select(p => p.Id).Take(2)
		};

		protected override int ExpectStatusCode => 200;

		protected override Func<MultiTermVectorsDescriptor, IMultiTermVectorsRequest> Fluent => d => d
			.Index<Developer>()
			.Ids(Developer.Developers.Select(p => (Id)p.Id).Take(2))
			.FieldStatistics()
			.Payloads()
			.TermStatistics()
			.Positions()
			.Offsets();

		protected override HttpMethod HttpMethod => HttpMethod.POST;

		protected override MultiTermVectorsRequest Initializer => new MultiTermVectorsRequest(Infer.Index<Developer>())
		{
			Ids = Developer.Developers.Select(p => (Id)p.Id).Take(2),
			FieldStatistics = true,
			Payloads = true,
			TermStatistics = true,
			Positions = true,
			Offsets = true
		};

		protected override bool SupportsDeserialization => false;

		protected override string UrlPath =>
			$"/devs/_mtermvectors?field_statistics=true&payloads=true&term_statistics=true&positions=true&offsets=true";

		protected override LazyResponses ClientUsage() => Calls(
			(client, f) => client.MultiTermVectors(f),
			(client, f) => client.MultiTermVectorsAsync(f),
			(client, r) => client.MultiTermVectors(r),
			(client, r) => client.MultiTermVectorsAsync(r)
		);

		protected override void ExpectResponse(MultiTermVectorsResponse response)
		{
			response.ShouldBeValid();
			response.Documents.Should().NotBeEmpty().And.HaveCount(2).And.OnlyContain(d => d.Found);
			var termvectorDoc = response.Documents.FirstOrDefault(d => d.TermVectors.Count > 0);

			termvectorDoc.Should().NotBeNull();
			termvectorDoc.Index.Should().NotBeNull();
			termvectorDoc.Id.Should().NotBeNull();

			termvectorDoc.TermVectors.Should().NotBeEmpty().And.ContainKey("firstName");
			var vectors = termvectorDoc.TermVectors["firstName"];
			AssertTermVectors(vectors);

			vectors = termvectorDoc.TermVectors[Infer.Field<Developer>(p => p.FirstName)];
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
				vectorTerm.Value.TotalTermFrequency.Should().BeGreaterThan(0);
				vectorTerm.Value.Tokens.Should().NotBeEmpty();

				var token = vectorTerm.Value.Tokens.First();
				token.EndOffset.Should().BeGreaterThan(0);
			}
		}
	}
}
