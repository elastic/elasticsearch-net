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

namespace Tests.Indices.Analyze
{
	public class AnalyzeUrlTests
	{
		[U] public async Task Urls()
		{
			var hardcoded = "hardcoded";
			var index = "index";
			await POST($"/{index}/_analyze")
					.Fluent(c => c.Indices.Analyze(a => a.Text(hardcoded).Index(index)))
					.Request(c => c.Indices.Analyze(new AnalyzeRequest(index, hardcoded)))
					.FluentAsync(c => c.Indices.AnalyzeAsync(a => a.Text(hardcoded).Index(index)))
					.RequestAsync(c => c.Indices.AnalyzeAsync(new AnalyzeRequest(index, hardcoded)))
				;

			await POST($"/_analyze")
					.Fluent(c => c.Indices.Analyze(a => a.Text(hardcoded)))
					.Request(c => c.Indices.Analyze(new AnalyzeRequest() { Text = new[] { hardcoded } }))
					.FluentAsync(c => c.Indices.AnalyzeAsync(a => a.Text(hardcoded)))
					.RequestAsync(c => c.Indices.AnalyzeAsync(new AnalyzeRequest() { Text = new[] { hardcoded } }))
				;
		}
	}
}
