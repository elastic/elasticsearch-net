// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

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
