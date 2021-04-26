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
using Tests.Domain;
using Tests.Framework.EndpointTests;
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.Document.Multiple.Bulk
{
	public class BulkUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls()
		{
			await POST("/_bulk")
					.Fluent(c => c.Bulk(s => s))
					.Request(c => c.Bulk(new BulkRequest()))
					.FluentAsync(c => c.BulkAsync(s => s))
					.RequestAsync(c => c.BulkAsync(new BulkRequest()))
				;

			await POST("/project/_bulk")
					.Fluent(c => c.Bulk(b => b.Index<Project>()))
					.Request(c => c.Bulk(new BulkRequest(typeof(Project))))
					.FluentAsync(c => c.BulkAsync(b => b.Index<Project>()))
					.RequestAsync(c => c.BulkAsync(new BulkRequest(typeof(Project))))
				;
		}
	}
}
