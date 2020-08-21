// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using FluentAssertions;
using Nest;
using Tests.Core.Client;
using Tests.Core.Extensions;
using Tests.Domain;

namespace Tests.ClientConcepts.LowLevel
{
	public class DirectStreaming
	{
		[U]
		public void DisableDirectStreamingOnError()
		{
			Action<IResponse> assert = r =>
			{
				r.ApiCall.Should().NotBeNull();
				r.ApiCall.RequestBodyInBytes.Should().NotBeNull();
				r.ApiCall.ResponseBodyInBytes.Should().NotBeNull();
			};

			var client = FixedResponseClient.Create(new { }, 404, s => s.DisableDirectStreaming());
			var response = client.Search<object>(s => s);
			assert(response);
			response = client.SearchAsync<object>(s => s).Result;
			assert(response);
		}

		[U]
		public void EnableDirectStreamingOnError()
		{
			Action<IResponse> assert = r =>
			{
				r.ApiCall.Should().NotBeNull();
				r.ApiCall.RequestBodyInBytes.Should().BeNull();
				r.ApiCall.ResponseBodyInBytes.Should().BeNull();
			};

			var client = FixedResponseClient.Create(new { }, 404);
			var response = client.Search<object>(s => s);
			assert(response);
			response = client.SearchAsync<object>(s => s).Result;
			assert(response);
		}

		[U]
		public void DisableDirectStreamingOnSuccess()
		{
			Action<IResponse> assert = r =>
			{
				r.ApiCall.Should().NotBeNull();
				r.ApiCall.RequestBodyInBytes.Should().NotBeNull();
				r.ApiCall.ResponseBodyInBytes.Should().NotBeNull();
			};

			var client = FixedResponseClient.Create(new { }, 200, s => s.DisableDirectStreaming());
			var response = client.Search<object>(s => s);
			assert(response);
			response = client.SearchAsync<object>(s => s).Result;
			assert(response);
		}

		[U]
		public void EnableDirectStreamingOnSuccess()
		{
			Action<IResponse> assert = r =>
			{
				r.ApiCall.Should().NotBeNull();
				r.ApiCall.RequestBodyInBytes.Should().BeNull();
				r.ApiCall.ResponseBodyInBytes.Should().BeNull();
			};

			var client = FixedResponseClient.Create(new { });
			var response = client.Search<object>(s => s);
			assert(response);
			response = client.SearchAsync<object>(s => s).Result;
			assert(response);
		}

		[U]
		public void DisableDirectStreamingOnRequest()
		{
			Action<IResponse> assert = r =>
			{
				r.ApiCall.Should().NotBeNull();
				r.ApiCall.RequestBodyInBytes.Should().NotBeNull();
				r.ApiCall.ResponseBodyInBytes.Should().NotBeNull();
			};

			var client = FixedResponseClient.Create(new { });
			var response = client.Search<object>(s => s.RequestConfiguration(r => r.DisableDirectStreaming()));
			assert(response);
			response = client.SearchAsync<object>(s => s.RequestConfiguration(r => r.DisableDirectStreaming())).Result;
			assert(response);
		}

		[U]
		public void EnableDirectStreamingOnRequest()
		{
			Action<IResponse> assert = r =>
			{
				r.ApiCall.Should().NotBeNull();
				r.ApiCall.RequestBodyInBytes.Should().BeNull();
				r.ApiCall.ResponseBodyInBytes.Should().BeNull();
			};

			var client = FixedResponseClient.Create(new { }, 200, c => c.DisableDirectStreaming());
			var response = client.Search<object>(s => s.RequestConfiguration(r => r.DisableDirectStreaming(false)));
			assert(response);
			response = client.SearchAsync<object>(s => s.RequestConfiguration(r => r.DisableDirectStreaming(false))).Result;
			assert(response);
		}

		[U]
		public void DebugModeRespectsOriginalOnRequestCompleted()
		{
			var global = 0;
			var local = 0;
			var client = FixedResponseClient.Create(new { }, 200, s => s
				.EnableDebugMode(d => local++)
				.OnRequestCompleted(d => global++)
			);

			var response = client.Search<Project>(s => s
				.From(10)
				.Size(20)
				.Query(q => q
					.Match(m => m
						.Field(p => p.Name)
						.Query("elastic")
					)
				)
			);
			response.ShouldBeValid();
			global.Should().Be(1);
			local.Should().Be(1);
		}
	}
}
