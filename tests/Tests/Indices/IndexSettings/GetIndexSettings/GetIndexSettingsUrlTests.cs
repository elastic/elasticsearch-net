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

namespace Tests.Indices.IndexSettings.GetIndexSettings
{
	public class GetIndexSettingsUrlTests
	{
		[U] public async Task Urls()
		{
			var index = "index1,index2";
			Nest.Indices indices = index;
			var name = "name";
			Name n = name;
			await GET($"/index1%2Cindex2/_settings/{name}")
					.Fluent(c => c.Indices.GetSettings(index, m => m.Name(name)))
					.Request(c => c.Indices.GetSettings(new GetIndexSettingsRequest(index, name)))
					.FluentAsync(c => c.Indices.GetSettingsAsync(index, m => m.Name(name)))
					.RequestAsync(c => c.Indices.GetSettingsAsync(new GetIndexSettingsRequest(index, name)))
				;

			await GET($"/index1%2Cindex2/_settings")
					.Fluent(c => c.Indices.GetSettings(index))
					.Request(c => c.Indices.GetSettings(new GetIndexSettingsRequest(indices)))
					.FluentAsync(c => c.Indices.GetSettingsAsync(index))
					.RequestAsync(c => c.Indices.GetSettingsAsync(new GetIndexSettingsRequest(indices)))
				;

			await GET($"/_settings/{name}")
					.Fluent(c => c.Indices.GetSettings(null, m => m.Name(name)))
					.Request(c => c.Indices.GetSettings(new GetIndexSettingsRequest(n)))
					.FluentAsync(c => c.Indices.GetSettingsAsync(null, m => m.Name(name)))
					.RequestAsync(c => c.Indices.GetSettingsAsync(new GetIndexSettingsRequest(n)))
				;
			await GET($"/_all/_settings")
					.Fluent(c => c.Indices.GetSettings(AllIndices))
					.Request(c => c.Indices.GetSettings(new GetIndexSettingsRequest(AllIndices)))
					.FluentAsync(c => c.Indices.GetSettingsAsync(AllIndices))
					.RequestAsync(c => c.Indices.GetSettingsAsync(new GetIndexSettingsRequest(AllIndices)))
				;

			await GET($"/_settings")
					.Request(c => c.Indices.GetSettings(new GetIndexSettingsRequest()))
					.RequestAsync(c => c.Indices.GetSettingsAsync(new GetIndexSettingsRequest()))
				;
		}
	}
}
