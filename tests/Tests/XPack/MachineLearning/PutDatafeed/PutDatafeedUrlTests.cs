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

namespace Tests.XPack.MachineLearning.PutDatafeed
{
	public class PutDatafeedUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls() => await PUT("/_ml/datafeeds/datafeed_id")
			.Fluent(c => c.MachineLearning.PutDatafeed<object>("datafeed_id", p => p))
			.Request(c => c.MachineLearning.PutDatafeed(new PutDatafeedRequest("datafeed_id")))
			.FluentAsync(c => c.MachineLearning.PutDatafeedAsync<object>("datafeed_id", p => p))
			.RequestAsync(c => c.MachineLearning.PutDatafeedAsync(new PutDatafeedRequest("datafeed_id")));
	}
}
