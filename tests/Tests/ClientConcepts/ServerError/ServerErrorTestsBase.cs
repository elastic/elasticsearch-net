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

using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Core.Client;
using Tests.Core.Extensions;
using Tests.Domain;

namespace Tests.ClientConcepts.ServerError
{
	public abstract class ServerErrorTestsBase
	{
		protected ServerErrorTestsBase()
		{
			var settings = FixedResponseClient.CreateConnectionSettings(ResponseJson, 500);
			LowLevelClient = new ElasticLowLevelClient(settings);
			HighLevelClient = new ElasticClient(settings);
		}

		protected abstract string Json { get; }
		private IElasticClient HighLevelClient { get; }
		private IElasticLowLevelClient LowLevelClient { get; }

		private string ResponseJson => string.Concat(@"{ ""error"": ", Json, @", ""status"":500 }");

		protected virtual void AssertServerError()
		{
			LowLevelCall();
			HighLevelCall();
		}

		protected void HighLevelCall()
		{
			var response = HighLevelClient.Search<Project>(s => s);
			response.Should().NotBeNull();
			var serverError = response.ServerError;
			serverError.Should().NotBeNull();
			serverError.Status.Should().Be(response.ApiCall.HttpStatusCode);
			serverError.Error.Should().NotBeNull();
			serverError.Error.Headers.Should().NotBeNull();
			AssertResponseError("high level client", serverError.Error);
		}

		protected void LowLevelCall()
		{
			var response = LowLevelClient.Search<StringResponse>(PostData.Serializable(new { }));
			response.Should().NotBeNull();
			response.Body.Should().NotBeNullOrWhiteSpace();
			var hasServerError = response.TryGetServerError(out var serverError);
			hasServerError.Should().BeTrue("we're trying to deserialize a server error using the helper but it returned false");
			serverError.Should().NotBeNull();
			serverError.Status.Should().Be(response.ApiCall.HttpStatusCode);
			AssertResponseError("low level client", serverError.Error);
		}

		protected abstract void AssertResponseError(string origin, Error error);
	}
}
