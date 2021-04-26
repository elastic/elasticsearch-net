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

namespace Tests.XPack.MachineLearning.DeleteCalendar
{
	public class DeleteCalendarUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls() => await DELETE("/_ml/calendars/calendar_id")
			.Fluent(c => c.MachineLearning.DeleteCalendar("calendar_id", p => p))
			.Request(c => c.MachineLearning.DeleteCalendar(new DeleteCalendarRequest("calendar_id")))
			.FluentAsync(c => c.MachineLearning.DeleteCalendarAsync("calendar_id", p => p))
			.RequestAsync(c => c.MachineLearning.DeleteCalendarAsync(new DeleteCalendarRequest("calendar_id")));
	}
}
