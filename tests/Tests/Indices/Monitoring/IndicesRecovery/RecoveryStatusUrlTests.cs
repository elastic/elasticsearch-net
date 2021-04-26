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
using static Nest.Indices;

namespace Tests.Indices.Monitoring.IndicesRecovery
{
	public class RecoveryStatusUrlTests
	{
		[U] public async Task Urls()
		{
			await UrlTester.GET($"/_recovery")
					.Request(c => c.Indices.RecoveryStatus(new RecoveryStatusRequest()))
					.RequestAsync(c => c.Indices.RecoveryStatusAsync(new RecoveryStatusRequest()))
				;

			await UrlTester.GET($"/_all/_recovery")
					.Fluent(c => c.Indices.RecoveryStatus(All))
					.Request(c => c.Indices.RecoveryStatus(new RecoveryStatusRequest(All)))
					.FluentAsync(c => c.Indices.RecoveryStatusAsync(All))
					.RequestAsync(c => c.Indices.RecoveryStatusAsync(new RecoveryStatusRequest(All)))
				;

			var index = "index1,index2";
			await UrlTester.GET($"/index1%2Cindex2/_recovery")
					.Fluent(c => c.Indices.RecoveryStatus(index))
					.Request(c => c.Indices.RecoveryStatus(new RecoveryStatusRequest(index)))
					.FluentAsync(c => c.Indices.RecoveryStatusAsync(index))
					.RequestAsync(c => c.Indices.RecoveryStatusAsync(new RecoveryStatusRequest(index)))
				;
		}
	}
}
