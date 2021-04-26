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
using static Tests.Framework.EndpointTests.UrlTester;

namespace Tests.Indices.IndexSettings.UpdateIndicesSettings
{
	public class UpdateIndexSettingsUrlTests
	{
		[U] public async Task Urls()
		{
			var index = "index1,index2";
			Nest.Indices indices = index;
			await PUT($"/index1%2Cindex2/_settings")
					.Fluent(c => c.Indices.UpdateSettings(indices, s => s))
					.Request(c => c.Indices.UpdateSettings(new UpdateIndexSettingsRequest(index)))
					.FluentAsync(c => c.Indices.UpdateSettingsAsync(indices, s => s))
					.RequestAsync(c => c.Indices.UpdateSettingsAsync(new UpdateIndexSettingsRequest(index)));

			await PUT($"/_all/_settings")
					.Fluent(c => c.Indices.UpdateSettings(AllIndices, s => s))
					.Request(c => c.Indices.UpdateSettings(new UpdateIndexSettingsRequest(All)))
					.FluentAsync(c => c.Indices.UpdateSettingsAsync(AllIndices, s => s))
					.RequestAsync(c => c.Indices.UpdateSettingsAsync(new UpdateIndexSettingsRequest(All)))
				;

			await PUT($"/_settings")
					.Request(c => c.Indices.UpdateSettings(new UpdateIndexSettingsRequest()))
					.RequestAsync(c => c.Indices.UpdateSettingsAsync(new UpdateIndexSettingsRequest()))
				;
		}
	}
}
