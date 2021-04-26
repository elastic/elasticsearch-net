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

namespace Tests.XPack.CrossClusterReplication.AutoFollow.ResumeAutoFollowPattern
{
	public class ResumeAutoFollowPatternUrlTests : UrlTestsBase
	{
		[U] public override async Task Urls()
		{
			var name = "x";
			await UrlTester.POST($"/_ccr/auto_follow/{name}/resume")
				.Fluent(c => c.CrossClusterReplication.ResumeAutoFollowPattern(name))
				.Request(c => c.CrossClusterReplication.ResumeAutoFollowPattern(new ResumeAutoFollowPatternRequest(name)))
				.FluentAsync(c => c.CrossClusterReplication.ResumeAutoFollowPatternAsync(name))
				.RequestAsync(c => c.CrossClusterReplication.ResumeAutoFollowPatternAsync(new ResumeAutoFollowPatternRequest(name)));
		}
	}
}
