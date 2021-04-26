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

namespace Tests.Document.Multiple.MultiTermVectors
{
	public class MultiTermVectorsUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls()
		{
			await POST("/_mtermvectors")
					.Fluent(c => c.MultiTermVectors())
					.Request(c => c.MultiTermVectors(new MultiTermVectorsRequest()))
					.FluentAsync(c => c.MultiTermVectorsAsync())
					.RequestAsync(c => c.MultiTermVectorsAsync(new MultiTermVectorsRequest()))
				;

			await POST("/project/_mtermvectors")
					.Fluent(c => c.MultiTermVectors(m => m.Index<Project>()))
					.Request(c => c.MultiTermVectors(new MultiTermVectorsRequest(typeof(Project))))
					.FluentAsync(c => c.MultiTermVectorsAsync(m => m.Index<Project>()))
					.RequestAsync(c => c.MultiTermVectorsAsync(new MultiTermVectorsRequest(typeof(Project))))
				;
		}
	}
}
