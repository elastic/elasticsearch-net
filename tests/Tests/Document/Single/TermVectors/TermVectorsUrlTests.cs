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

namespace Tests.Document.Single.TermVectors
{
	public class TermVectorsUrlTests
	{
		[U]
		public async Task Urls()
		{
			var id = "name-of-doc";
			var index = "myindex";

			await GET($"/{index}/_termvectors/{id}")
					.Fluent(c => c.TermVectors<Project>(t => t.Index(index).Id(id)))
					.Request(c => c.TermVectors(new TermVectorsRequest<Project>(index, id)))
					.FluentAsync(c => c.TermVectorsAsync<Project>(t => t.Index(index).Id(id)))
					.RequestAsync(c => c.TermVectorsAsync(new TermVectorsRequest<Project>(index, id)))
				;

			await GET($"/project/_termvectors/{id}")
					.Fluent(c => c.TermVectors<Project>(t => t.Id(id)))
					.Request(c => c.TermVectors(new TermVectorsRequest<Project>((Id)id)))
					.FluentAsync(c => c.TermVectorsAsync<Project>(t => t.Id(id)))
					.RequestAsync(c => c.TermVectorsAsync(new TermVectorsRequest<Project>((Id)id)))
				;

			await GET($"/{index}/_termvectors/{id}")
					.Fluent(c => c.TermVectors<Project>(t => t.Index(index).Id(id)))
					.Request(c => c.TermVectors(new TermVectorsRequest<Project>(index, id)))
					.FluentAsync(c => c.TermVectorsAsync<Project>(t => t.Index(index).Id(id)))
					.RequestAsync(c => c.TermVectorsAsync(new TermVectorsRequest<Project>(index, id)))
				;

			var document = new Project { Name = "foo" };

			await POST($"/{index}/_termvectors")
					.Fluent(c => c.TermVectors<Project>(t => t.Index(index).Document(document)))
					.Request(c => c.TermVectors(new TermVectorsRequest<Project>(document, index)))
					.FluentAsync(c => c.TermVectorsAsync<Project>(t => t.Index(index).Document(document)))
					.RequestAsync(c => c.TermVectorsAsync(new TermVectorsRequest<Project>(document, index)))
				;

			await POST($"/project/_termvectors")
					.Fluent(c => c.TermVectors<Project>(t => t.Document(document)))
					.Request(c => c.TermVectors(new TermVectorsRequest<Project>(document)))
					.FluentAsync(c => c.TermVectorsAsync<Project>(t => t.Document(document)))
					.RequestAsync(c => c.TermVectorsAsync(new TermVectorsRequest<Project>(document)))
				;
		}
	}
}
