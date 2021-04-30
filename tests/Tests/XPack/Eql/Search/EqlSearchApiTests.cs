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
		private const string ScriptValue = "emit(doc['timestamp'].value.dayOfWeekEnum.getDisplayName(TextStyle.FULL, Locale.ROOT))";
		private static readonly string EqlQuery = $"info where section == \"{Log.Sections.First()}\"";

		public EqlSearchApiTests(TimeSeriesCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;

		protected override object ExpectJson => new
		{
			event_category_field = "event.category",
			fields = new[] { "tag", "dayOfWeek" },
			filter = new
			{
				range = new
				{
					timestamp = new
					{
						gt = "2015-06-06T12:01:02.123"
					}
				}
			},
			query = EqlQuery,
			result_position = "tail",
			runtime_mappings = new
			{
				dayOfWeek = new
				{
					script = new
					{
						source = ScriptValue
					},
					type = "keyword"
				}
			},
			size = 10.0,
			tiebreaker_field = "netIn",
			timestamp_field = "timestamp"
		};

		protected override int ExpectStatusCode => 200;

		protected override Func<EqlSearchDescriptor<Log>, IEqlSearchRequest> Fluent => s => s
			.EventCategoryField(f => f.Event.Category)
			.Fields(f => f.Field(fld => fld.Tag).Field("dayOfWeek"))
			.Filter(f => f.DateRange(r => r.Field(fld => fld.Timestamp).GreaterThan(Domain.Helpers.TestValueHelper.FixedDate)))
			.Query(EqlQuery)
			.ResultPosition(EqlResultPosition.Tail)
			.RuntimeFields(rtf => rtf.RuntimeField("dayOfWeek", FieldType.Keyword, rf => rf
				.Script(ScriptValue)))
			.Size(10)
			.TiebreakerField(t => t.NetIn)
			.TimestampField(Infer.Field<Log>(f => f.Timestamp));

		protected override HttpMethod HttpMethod => HttpMethod.POST;

		protected override EqlSearchRequest Initializer => new EqlSearchRequest<Log>
		{
			EventCategoryField = Infer.Field<Log>(f => f.Event.Category),
			Fields = new[] { Infer.Field<Log>(fld => fld.Tag), "dayOfWeek" },
			Filter = new DateRangeQuery
			{
				Field =  "timestamp",
				GreaterThan = Domain.Helpers.TestValueHelper.FixedDate
			},
			Query = EqlQuery,
			ResultPosition = EqlResultPosition.Tail,
			RuntimeFields = new RuntimeFields
			{
				{ "dayOfWeek", new RuntimeField { Type = FieldType.Keyword, Script = new InlineScript(ScriptValue) }}
			},
			Size = 10,
			TiebreakerField = Infer.Field<Log>(f => f.NetIn),
			TimestampField = Infer.Field<Log>(f => f.Timestamp)
		};

		protected override string UrlPath => "/customlogs-%2A/_eql/search";

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
			response.EqlHitsMetadata.Total.Value.Should().Be(10);
			response.Total.Should().Be(10);

			var firstEvent = response.Events.First();
			firstEvent.Index.Should().StartWith("customlogs-");
			firstEvent.Id.Should().NotBeNullOrEmpty();
			firstEvent.Source.Event.Category.Should().Be("info");
			firstEvent.Fields.Should().HaveCount(2);
			firstEvent.Fields.ValuesOf<string>("dayOfWeek").Should().ContainSingle().Which.Should().NotBeNullOrEmpty();
			firstEvent.Fields.ValuesOf<string>("tag").Should().ContainSingle().Which.Should().NotBeNullOrEmpty();
		}
	}

	[SkipVersion("<7.11.0", "EQL went GA in 7.11.0")]
	public class EqlSearchWithSequenceApiTests
		: ApiIntegrationTestBase<TimeSeriesCluster, EqlSearchResponse<Log>, IEqlSearchRequest, EqlSearchDescriptor<Log>, EqlSearchRequest>
	{
		private const string EqlQuery = "sequence [info where temperature > -10][error where true]";

		public EqlSearchWithSequenceApiTests(TimeSeriesCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		protected override bool ExpectIsValid => true;

		protected override object ExpectJson => new
		{
			fetch_size = 5,
			query = EqlQuery,
			timestamp_field = "timestamp"
		};

		protected override int ExpectStatusCode => 200;

		protected override Func<EqlSearchDescriptor<Log>, IEqlSearchRequest> Fluent => s => s
			.FetchSize(5)
			.Query(EqlQuery)
			.TimestampField(Infer.Field<Log>(f => f.Timestamp));

		protected override HttpMethod HttpMethod => HttpMethod.POST;

		protected override EqlSearchRequest Initializer => new EqlSearchRequest<Log>
		{
			FetchSize = 5,
			Query = EqlQuery,
			TimestampField = Infer.Field<Log>(f => f.Timestamp)
		};

		protected override string UrlPath => "/customlogs-%2A/_eql/search";

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
			response.Sequences.Count.Should().Be(10);
			response.EqlHitsMetadata.Total.Value.Should().Be(10);
			response.Total.Should().Be(10);

			var sequence = response.Sequences.First();
			sequence.Events.Count.Should().Be(2);
			sequence.Events.First().Source.Event.Category.Should().Be("info");
			sequence.Events.Last().Source.Event.Category.Should().Be("error");
		}
	}
}
