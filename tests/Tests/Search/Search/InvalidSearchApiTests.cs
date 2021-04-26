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
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Nest;
using Tests.Core.Extensions;
using Tests.Core.ManagedElasticsearch.Clusters;
using Tests.Domain;
using Tests.Framework.EndpointTests.TestState;

namespace Tests.Search.Search
{
	public class InvalidSearchApiTests : SearchApiTests
	{
		public InvalidSearchApiTests(ReadOnlyCluster cluster, EndpointUsage usage) : base(cluster, usage) { }

		/// <summary> This is rather noisy on the console out, we only need to test 1 of our overloads randomly really. </summary>
		protected override bool TestOnlyOne => true;

		protected override bool ExpectIsValid => false;

		protected override object ExpectJson => new
		{
			from = 10,
			size = 20,
			aggs = new
			{
				startDates = new
				{
					date_range = new
					{
						ranges = new[]
						{
							new { from = "-1m" }
						}
					}
				}
			}
		};

		protected override int ExpectStatusCode => 400;

		protected override Func<SearchDescriptor<Project>, ISearchRequest> Fluent => s => s
			.From(10)
			.Size(20)
			.Aggregations(a => a
				.DateRange("startDates", t => t
					.Ranges(ranges => ranges.From("-1m"))
				)
			);

		protected override SearchRequest<Project> Initializer => new SearchRequest<Project>()
		{
			From = 10,
			Size = 20,
			Aggregations = new DateRangeAggregation("startDates")
			{
				Ranges = new List<DateRangeExpression>
				{
					new DateRangeExpression { From = "-1m" }
				}
			}
		};

		protected override void ExpectResponse(ISearchResponse<Project> response)
		{
			response.ShouldNotBeValid();
			var serverError = response.ServerError;
			serverError.Should().NotBeNull();
			serverError.Status.Should().Be(ExpectStatusCode, "{0}", response.DebugInformation);

			serverError.Error.Type.Should().Be("illegal_argument_exception");
			serverError.Error.RootCause.First().Reason.Should().Contain("Required one of fields");
		}
	}
}
