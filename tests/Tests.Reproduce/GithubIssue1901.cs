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
using System.Threading.Tasks;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Elastic.Transport;
using FluentAssertions;
using Nest;
using Tests.Core.Client;

namespace Tests.Reproduce
{
	public class GithubIssue1901
	{
		private const string ProxyAuthResponse = @"<html>
<head><title>401 Authorization Required</title></head>
<body bgcolor=""white"">
<center><h1>401 Authorization Required</h1></center>
<hr><center>nginx/1.4.6 (Ubuntu)</center>
</body>
</html>
<!-- a padding to disable MSIE and Chrome friendly error page -->
<!-- a padding to disable MSIE and Chrome friendly error page -->
<!-- a padding to disable MSIE and Chrome friendly error page -->
<!-- a padding to disable MSIE and Chrome friendly error page -->
<!-- a padding to disable MSIE and Chrome friendly error page -->
<!-- a padding to disable MSIE and Chrome friendly error page -->";

		[U] public async Task BadAuthResponseDoesNotThrowExceptionWhenAttemptingToDeserializeResponse()
		{
			var client = FixedResponseClient.Create(ProxyAuthResponse, 401,
				s => s.SkipDeserializationForStatusCodes(401),
				"text/html",
				new Exception("problem with the request as a result of 401")
			);
			var source = await client.LowLevel.SourceAsync<GetResponse<Example>>("examples", "1");
			source.ApiCall.Success.Should().BeFalse();
		}

		[U] public async Task BadAuthCarriesStatusCodeAndResponseBodyOverToResponse()
		{
			var client = FixedResponseClient.Create(ProxyAuthResponse, 401,
				s => s.DisableDirectStreaming().SkipDeserializationForStatusCodes(401),
				"text/html",
				new Exception("problem with the request as a result of 401")
			);
			var response = await client.LowLevel.GetAsync<GetResponse<Example>>("examples", "1");
			response.ApiCall.Success.Should().BeFalse();
			response.ApiCall.ResponseBodyInBytes.Should().NotBeNullOrEmpty();
			response.ApiCall.HttpStatusCode.Should().Be(401);
		}
		[U] public async Task BadMimeMakesA200ResponseUnsuccesful()
		{
			var client = FixedResponseClient.Create(ProxyAuthResponse, 200,
				s => s.DisableDirectStreaming(),
				"text/html"
			);
			var response = await client.LowLevel.GetAsync<StringResponse>("examples", "1");
			response.ApiCall.Success.Should().BeFalse();
			response.ApiCall.HttpStatusCode.Should().Be(200);
		}
		[U] public async Task BadMimeMakesA200ResponseInvalid()
		{
			var client = FixedResponseClient.Create(ProxyAuthResponse, 200,
				s => s.DisableDirectStreaming(),
				"text/html"
			);
			var response = await client.LowLevel.GetAsync<GetResponse<object>>("examples", "1");
			response.ApiCall.Success.Should().BeFalse();
			response.ApiCall.HttpStatusCode.Should().Be(200);
			response.IsValid.Should().BeFalse();
		}

		private class Example { }
	}

}
