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

using System;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using FluentAssertions;
using Nest;
using Tests.Core.Client;

namespace Tests.Reproduce
{
	public class GithubIssue4733
	{
		[U]
		public void CanSerializeIdsQueryWithIEnumerableString()
		{
			var client = TestClient.DefaultInMemoryClient;

			Func<ISearchResponse<object>> func = () => client.Search<object>(s => s
				.From(0)
				.Size(25)
				.Source(ss => ss.Excludes(e => e.Field("events")))
				.Query(q => q.Pinned(p => p
						.Organic(o => o.MatchAll())
						.Ids(new [] { "387c2c78-95b1-42f8-a965-e09a73f7cff6" })
					)
				)
			);

			func.Should().NotThrow();
		}
	}
}
