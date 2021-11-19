using System;
using System.Threading.Tasks;
using Tests.Core.Client;
using Tests.Core.Client.Settings;
using Tests.Framework.Extensions;
using HttpMethod = Elastic.Transport.HttpMethod;

namespace Tests.Framework.EndpointTests
{
	public class UrlTester
	{
		internal UrlTester(HttpMethod method, string expectedUrl,
			Func<ElasticsearchClientSettings, ElasticsearchClientSettings> settings = null)
		{
			ExpectedHttpMethod = method;
			ExpectedUrl = expectedUrl;
			Client = settings == null
				? TestClient.DefaultInMemoryClient
				: new ElasticClient(settings(new AlwaysInMemoryElasticsearchClientSettings()));
		}

		private HttpMethod ExpectedHttpMethod { get; }
		private string ExpectedUrl { get; }
		private IElasticClient Client { get; }

		public static UrlTester ExpectUrl(HttpMethod method, string url,
			Func<ElasticsearchClientSettings, ElasticsearchClientSettings> settings = null) =>
			new(method, url, settings);

		public static string EscapeUriString(string s) => Uri.EscapeDataString(s);

		public UrlTester Fluent<TResponse>(Func<IElasticClient, TResponse> call) where TResponse : IResponse =>
			WhenCalling(call, "fluent");

		public UrlTester Request<TResponse>(Func<IElasticClient, TResponse> call) where TResponse : IResponse =>
			WhenCalling(call, "request");

		public Task<UrlTester> FluentAsync<TResponse>(Func<IElasticClient, Task<TResponse>> call)
			where TResponse : IResponse =>
			WhenCallingAsync(call, "fluent async");

		public Task<UrlTester> RequestAsync<TResponse>(Func<IElasticClient, Task<TResponse>> call)
			where TResponse : IResponse =>
			WhenCallingAsync(call, "request async");

		//public UrlTester LowLevel(Func<IElasticLowLevelClient, IApiCallDetails> call)
		//{
		//	var callDetails = call(Client.LowLevel);
		//	return Assert("lowlevel", callDetails);
		//}
		//public async Task<UrlTester> LowLevelAsync(Func<IElasticLowLevelClient, Task<VoidResponse>> call)
		//{
		//	var callDetails = await call(Client.LowLevel);
		//	return Assert("lowlevel async", callDetails);
		//}

		private UrlTester WhenCalling<TResponse>(Func<IElasticClient, TResponse> call, string typeOfCall)
			where TResponse : IResponse
		{
			var callDetails = call(Client);
			return Assert(typeOfCall, callDetails.ApiCall);
		}

		internal async Task<UrlTester> WhenCallingAsync<TResponse>(Func<IElasticClient, Task<TResponse>> call,
			string typeOfCall)
			where TResponse : IResponse
		{
			var callDetails = (await call(Client)).ApiCall;
			return Assert(typeOfCall, callDetails);
		}

		private UrlTester Assert(string typeOfCall, IApiCallDetails callDetails)
		{
			var url = callDetails.Uri.PathAndQuery;
			callDetails.Uri.PathEquals(ExpectedUrl, typeOfCall);
			callDetails.HttpMethod.Should().Be(ExpectedHttpMethod, $"{typeOfCall} to {url}");
			return this;
		}

		// TODO - Rename these
		// ReSharper disable InconsistentNaming
		public static UrlTester POST(string url) => new(HttpMethod.POST, url);

		public static UrlTester PUT(string url) => new(HttpMethod.PUT, url);

		public static UrlTester GET(string url) => new(HttpMethod.GET, url);

		public static UrlTester HEAD(string url) => new(HttpMethod.HEAD, url);

		public static UrlTester DELETE(string url) => new(HttpMethod.DELETE, url);
		// ReSharper restore InconsistentNaming
	}
}
