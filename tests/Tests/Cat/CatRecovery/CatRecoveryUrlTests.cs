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

namespace Tests.Cat.CatRecovery
{
	public class CatRecoveryUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls()
		{
			await GET("/_cat/recovery")
					.Fluent(c => c.Cat.Recovery())
					.Request(c => c.Cat.Recovery(new CatRecoveryRequest()))
					.FluentAsync(c => c.Cat.RecoveryAsync())
					.RequestAsync(c => c.Cat.RecoveryAsync(new CatRecoveryRequest()))
				;

			await GET("/_cat/recovery/project")
				.Fluent(c => c.Cat.Recovery(r => r.Index<Project>()))
				.Request(c => c.Cat.Recovery(new CatRecoveryRequest(Nest.Indices.Index<Project>())))
				.FluentAsync(c => c.Cat.RecoveryAsync(r => r.Index<Project>()))
				.RequestAsync(c => c.Cat.RecoveryAsync(new CatRecoveryRequest(Nest.Indices.Index<Project>())));
		}
	}
}
