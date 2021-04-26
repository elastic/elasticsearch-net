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
using System.Globalization;
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
	public class MultiTermVectorsIdsNotFoundApiTests
		: ApiIntegrationTestBase<ReadOnlyCluster, MultiTermVectorsResponse, IMultiTermVectorsRequest, MultiTermVectorsDescriptor,
			MultiTermVectorsRequest>
	{
		public MultiTermVectorsIdsNotFoundApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;
		protected override int ExpectStatusCode => 200;
		protected override HttpMethod HttpMethod => HttpMethod.POST;

		private const int id = int.MaxValue;


		protected override object ExpectJson { get; } = new
		{
			ids = new [] { id }
		};


		protected override Func<MultiTermVectorsDescriptor, IMultiTermVectorsRequest> Fluent => d => d
			.Index<Developer>()
			.Ids(id)
			.FieldStatistics()
			.Payloads()
			.TermStatistics()
			.Positions()
			.Offsets();

		protected override MultiTermVectorsRequest Initializer => new MultiTermVectorsRequest(Infer.Index<Developer>())
		{
			Ids = new Id[] {id},
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
			response.Documents.Should().HaveCount(1);
			var doc = response.Documents.First();
			doc.Found.Should().BeFalse();
			doc.Id.Should().Be(id.ToString(CultureInfo.InvariantCulture));
		}

	}
}
