using System;
using System.Text;
using Elastic.Xunit.XunitPlumbing;
using Elasticsearch.Net;
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
			stringResponse.TryGetServerError(out var serverError).Should().BeFalse();
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
