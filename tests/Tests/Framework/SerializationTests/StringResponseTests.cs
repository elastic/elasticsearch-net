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
using Elastic.Transport;
using Elastic.Transport.Products.Elasticsearch;
using FluentAssertions;
using Tests.Core.Client;

namespace Tests.Framework.SerializationTests
{
	public class StringResponseTests
	{
		[U] public void TryGetServerErrorDoesNotThrowException()
		{
			var client = FixedResponseClient.Create(StubResponse.NginxHtml401Response, 401,
				modifySettings: s => s.DisableDirectStreaming(),
				contentType: "text/html",
				exception: new Exception("problem with the request as a result of 401")
			);

			var stringResponse = client.LowLevel.Search<StringResponse>("project", PostData.Serializable(new { }));

			stringResponse.Body.Should().NotBeNull();
			stringResponse.Body.Should().Be(StubResponse.NginxHtml401Response);
			stringResponse.TryGetElasticsearchServerError(out var serverError).Should().BeFalse();
			serverError.Should().BeNull();
		}

		[U] public void SkipDeserializationForStatusCodesSetsBody()
		{
			var client = FixedResponseClient.Create(StubResponse.NginxHtml401Response, 401,
				modifySettings: s => s.DisableDirectStreaming().SkipDeserializationForStatusCodes(401),
				contentType: "text/html",
				exception: new Exception("problem with the request as a result of 401")
			);

			var stringResponse = client.LowLevel.Search<StringResponse>("project", PostData.Serializable(new { }));

			stringResponse.Body.Should().NotBeNull();
			stringResponse.Body.Should().Be(StubResponse.NginxHtml401Response);
		}
	}
}
