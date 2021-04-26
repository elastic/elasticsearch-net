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

using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Nest;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.XPack.MachineLearning.GetCalendars
{
	public class GetCalendarsUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls()
		{
			await POST("/_ml/calendars")
				.Fluent(c => c.MachineLearning.GetCalendars(p => p))
				.Request(c => c.MachineLearning.GetCalendars(new GetCalendarsRequest()))
				.FluentAsync(c => c.MachineLearning.GetCalendarsAsync(p => p))
				.RequestAsync(c => c.MachineLearning.GetCalendarsAsync(new GetCalendarsRequest()));

			await POST("/_ml/calendars/1")
				.Request(c => c.MachineLearning.GetCalendars(new GetCalendarsRequest(1)))
				.Fluent(c => c.MachineLearning.GetCalendars(r => r.CalendarId(1)))
				.FluentAsync(c => c.MachineLearning.GetCalendarsAsync(r => r.CalendarId(1)))
				.RequestAsync(c => c.MachineLearning.GetCalendarsAsync(new GetCalendarsRequest(1)));
		}
	}
}
