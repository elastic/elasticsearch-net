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
using System.Text;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Core.Client;

namespace Tests.Reproduce
{
	public class GithubIssue4787
	{
		[U]
		public void DoNotSerializeNullSourceFilter()
		{
			var connectionSettings = new ConnectionSettings(new InMemoryConnection()).DisableDirectStreaming();
			var client = new ElasticClient(connectionSettings);

			Func<ISearchResponse<object>> action = () =>
				client.Search<object>(s => s
					.Query(q => q
						.MatchAll()
					)
					.Index("index")
					.Source(sfd => null)
				);

			var response = action.Should().NotThrow().Subject;

			var json = Encoding.UTF8.GetString(response.ApiCall.RequestBodyInBytes);
			json.Should().Be(@"{""query"":{""match_all"":{}}}");
		}
	}
}
