/*
 * Licensed to Elasticsearch B.V. under one or more contributor
 * license agreements. See the NOTICE file distributed with
 * this work for additional information regarding copyright
 * ownership. Elasticsearch B.V. licenses this file to you under
 * the Apache License, Version 2.0 (the "License"); you may
 * not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

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

namespace Tests.Document.Multiple.MultiTermVectors
{
	public class MultiTermVectorsDocsApiTests
		: ApiIntegrationTestBase<ReadOnlyCluster, MultiTermVectorsResponse, IMultiTermVectorsRequest, MultiTermVectorsDescriptor,
			MultiTermVectorsRequest>
	{
		public MultiTermVectorsDocsApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;

		protected override object ExpectJson { get; } = new
		{
			docs = Developer.Developers.Select(p => new
				{
					_index = "devs",
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
				})
				.Take(2)
		};

		protected override int ExpectStatusCode => 200;

		protected override Func<MultiTermVectorsDescriptor, IMultiTermVectorsRequest> Fluent => d => d
			.Index<Developer>()
			.Documents<Developer>(Developer.Developers.Select(p => p.Id).Take(2), (p, i) => p
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
			);

		protected override HttpMethod HttpMethod => HttpMethod.POST;

		protected override MultiTermVectorsRequest Initializer => new MultiTermVectorsRequest(Index<Developer>())
		{
			Documents = Developer.Developers.Select(p => p.Id)
				.Take(2)
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

		protected override bool SupportsDeserialization => false;
		protected override string UrlPath => $"/devs/_mtermvectors";

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

			vectors = termvectorDoc.TermVectors[Field<Developer>(p => p.FirstName)];
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
				vectorTerm.Value.Score.Should().BeGreaterThan(0);

				var token = vectorTerm.Value.Tokens.First();
				token.EndOffset.Should().BeGreaterThan(0);
			}
		}
	}
}
