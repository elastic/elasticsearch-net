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

namespace Tests.Cat.CatFielddata
{
	public class CatFielddataUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls()
		{
			await GET("/_cat/fielddata")
					.Fluent(c => c.Cat.Fielddata())
					.Request(c => c.Cat.Fielddata(new CatFielddataRequest()))
					.FluentAsync(c => c.Cat.FielddataAsync())
					.RequestAsync(c => c.Cat.FielddataAsync(new CatFielddataRequest()))
				;

			var fields = new[] { "name", "startedOn" };

			await GET("/_cat/fielddata/name%2CstartedOn")
					.Fluent(c => c.Cat.Fielddata(f => f.Fields<Project>(p => p.Name, p => p.StartedOn)))
					.Request(c => c.Cat.Fielddata(new CatFielddataRequest(fields)))
					.FluentAsync(c => c.Cat.FielddataAsync(f => f.Fields<Project>(p => p.Name, p => p.StartedOn)))
					.RequestAsync(c => c.Cat.FielddataAsync(new CatFielddataRequest(fields)))
				;
		}
	}
}
