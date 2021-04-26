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
using FluentAssertions;
using Tests.Core.Client;

namespace Tests.Reproduce
{
	public class GithubIssue4228
	{
		[U]
		public void CanSerializeDateMathDateTimeMinValue() {

			var searchResponse = TestClient.DefaultInMemoryClient.Search<object>(s => s
				.AllIndices()
				.Query(q => q
					.DateRange(d => d
						.GreaterThanOrEquals(DateTime.MinValue)
						.Field("date")
					)
				)
			);

			Encoding.UTF8.GetString(searchResponse.ApiCall.RequestBodyInBytes)
				.Should().Be("{\"query\":{\"range\":{\"date\":{\"gte\":\"0001-01-01T00:00:00\"}}}}");
		}
	}
}
