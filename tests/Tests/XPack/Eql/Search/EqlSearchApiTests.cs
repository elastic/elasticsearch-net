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
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.XPack.Eql.Search
{
	[SkipVersion("<7.11.0", "EQL went GA in 7.11.0")]
	public class EqlSearchApiTests
	: ApiIntegrationTestBase<TimeSeriesCluster, EqlSearchResponse<Log>, IEqlSearchRequest, EqlSearchDescriptor<Log>, EqlSearchRequest>
	{
		private static readonly string EqlQuery = $"info where section == \"{Log.Sections.First()}\"";

		public EqlSearchApiTests(TimeSeriesCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;

		protected override object ExpectJson => new
		{
			query = EqlQuery
		};

		protected override int ExpectStatusCode => 200;

		protected override Func<EqlSearchDescriptor<Log>, IEqlSearchRequest> Fluent => s => s.Query(EqlQuery).TimestampField(Infer.Field<Log>(f => f.Timestamp));

		protected override HttpMethod HttpMethod => HttpMethod.POST;

		protected override EqlSearchRequest Initializer => new EqlSearchRequest<Log>
		{
			Query = EqlQuery,
			TimestampField = Infer.Field<Log>(f => f.Timestamp)
		};

		protected override string UrlPath => "/customlogs-*/_eql/search";

		protected override LazyResponses ClientUsage() => Calls(
			(c, f) => c.Eql.Search(f),
			(c, f) => c.Eql.SearchAsync(f),
			(c, r) => c.Eql.Search<Log>(r),
			(c, r) => c.Eql.SearchAsync<Log>(r)
		);

		protected override void ExpectResponse(EqlSearchResponse<Log> response)
		{
			response.IsValid.Should().BeTrue();
			response.IsPartial.Should().BeFalse();
			response.IsRunning.Should().BeFalse();
			response.Took.Should().BeGreaterOrEqualTo(0);
			response.TimedOut.Should().BeFalse();
			response.Events.Count.Should().Be(10); //default
			response.EventHitsMetadata.Total.Value.Should().Be(10);

			var firstEvent = response.Events.First();
			firstEvent.Index.Should().StartWith("customlogs-");
			firstEvent.Id.Should().NotBeNullOrEmpty();
			firstEvent.Source.Event.Category.Should().Be("info");
		}
	}

	// TODO Sequence

	// Fields

	// Runtime Fields

	// Complete Request Payload
}
