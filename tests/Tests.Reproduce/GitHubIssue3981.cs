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

using System.Text;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using FluentAssertions;
using Nest;
using Tests.Core.Client;

namespace Tests.Reproduce
{
	public class GitHubIssue3981
	{
		// This test always passes because the JsonPropertyAttribute on
		// the GeoLocation type are recognized by the JsonNetSerializer when used in tests, because the
		// IL rewriting to internalize Json.NET in the client happens *after* tests are run.
		[U]
		public void JsonNetSerializerSerializesGeoLocation()
		{
			var document = new Document
			{
				Location = new GeoLocation(45, 45)
			};

			var indexResponse = TestClient.InMemoryWithJsonNetSerializer.IndexDocument(document);
			Encoding.UTF8.GetString(indexResponse.ApiCall.RequestBodyInBytes).Should().Contain("\"lat\"").And.Contain("\"lon\"");
		}

		private class Document
		{
			public GeoLocation Location { get; set; }
		}
	}
}
