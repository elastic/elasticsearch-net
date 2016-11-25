using FluentAssertions;
using Nest;
using System;
using Tests.Framework;

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

			var client = TestClient.GetFixedReturnClient(new { }, 404, s => s.DisableDirectStreaming());
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

			var client = TestClient.GetFixedReturnClient(new { }, 404);
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

			var client = TestClient.GetFixedReturnClient(new { }, 200, s => s.DisableDirectStreaming());
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

			var client = TestClient.GetFixedReturnClient(new { });
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

			var client = TestClient.GetFixedReturnClient(new { });
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

			var client = TestClient.GetFixedReturnClient(new { }, 200, c => c.DisableDirectStreaming());
			var response = client.Search<object>(s => s.RequestConfiguration(r => r.DisableDirectStreaming(false)));
			assert(response);
			response = client.SearchAsync<object>(s => s.RequestConfiguration(r => r.DisableDirectStreaming(false))).Result;
			assert(response);
		}
	}
}
