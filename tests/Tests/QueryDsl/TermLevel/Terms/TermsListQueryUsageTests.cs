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

using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.QueryDsl.TermLevel.Terms
{
	public class TermsListQueryUsageTests : QueryDslUsageTestsBase
	{
		public TermsListQueryUsageTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override QueryContainer QueryInitializer => new TermsQuery
		{
			Name = "named_query",
			Boost = 1.1,
			Field = "description",
			Terms = new List<string> { "term1", "term2" }
		};

		protected override object QueryJson => new
		{
			terms = new
			{
				_name = "named_query",
				boost = 1.1,
				description = new[] { "term1", "term2" }
			}
		};

		protected override QueryContainer QueryFluent(QueryContainerDescriptor<Project> q) => q
			.Terms(c => c
				.Name("named_query")
				.Boost(1.1)
				.Field(p => p.Description)
				.Terms(new List<string> { "term1", "term2" })
			);
	}

	public class TermsListOfListIntegrationTests : QueryDslIntegrationTestsBase
	{
		private readonly List<List<string>> _terms = new List<List<string>> { new List<string> { "term1", "term2" } };

		public TermsListOfListIntegrationTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => false;

		protected override int ExpectStatusCode => 400;

		protected override QueryContainer QueryInitializer => new TermsQuery
		{
			Name = "named_query",
			Boost = 1.1,
			Field = "description",
			Terms = _terms
		};

		protected override object QueryJson => new
		{
			terms = new
			{
				_name = "named_query",
				boost = 1.1,
				description = new[] { new[] { "term1", "term2" } },
			}
		};

		protected override QueryContainer QueryFluent(QueryContainerDescriptor<Project> q) => q
			.Terms(c => c
				.Name("named_query")
				.Boost(1.1)
				.Field(p => p.Description)
				.Terms(_terms)
			);

		protected override void ExpectResponse(ISearchResponse<Project> response)
		{
			response.ShouldNotBeValid();

			response.ServerError.Should().NotBeNull();
			response.ServerError.Status.Should().Be(400);
			response.ServerError.Error.Should().NotBeNull();
			var rootCauses = response.ServerError.Error.RootCause;
			rootCauses.Should().NotBeNullOrEmpty();
			var rootCause = rootCauses.First();
			rootCause.Type.Should().Be("parsing_exception");
		}
	}

	public class TermsListOfListStringAgainstNumericFieldIntegrationTests : QueryDslIntegrationTestsBase
	{
		private readonly List<List<string>> _terms = new List<List<string>> { new List<string> { "term1", "term2" } };

		public TermsListOfListStringAgainstNumericFieldIntegrationTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => false;


		protected override int ExpectStatusCode => 400;

		protected override QueryContainer QueryInitializer => new TermsQuery
		{
			Name = "named_query",
			Boost = 1.1,
			Field = "numberOfCommits",
			Terms = _terms,
		};

		protected override object QueryJson => new
		{
			terms = new
			{
				_name = "named_query",
				boost = 1.1,
				numberOfCommits = new[] { new[] { "term1", "term2" } }
			}
		};

		protected override QueryContainer QueryFluent(QueryContainerDescriptor<Project> q) => q
			.Terms(c => c
				.Name("named_query")
				.Boost(1.1)
				.Field(p => p.NumberOfCommits)
				.Terms(_terms)
			);

		protected override void ExpectResponse(ISearchResponse<Project> response)
		{
			response.ServerError.Should().NotBeNull();
			response.ServerError.Status.Should().Be(400);
			response.ServerError.Error.Should().NotBeNull();
			var rootCauses = response.ServerError.Error.RootCause;
			rootCauses.Should().NotBeNullOrEmpty();
			var rootCause = rootCauses.First();
			rootCause.Type.Should().Be("parsing_exception");
		}
	}
}
