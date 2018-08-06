using System;
using System.Threading.Tasks;
using Elastic.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Tests.Core.Client;
using Tests.Core.Client.Settings;
using Tests.Framework.ManagedElasticsearch;

namespace Tests.Framework
{
	public abstract class UrlTestsBase
	{
		[U] public abstract Task Urls();
	}

	public static class UrlTesterExtensions
	{
		public static async Task<UrlTester> RequestAsync<TResponse>(this Task<UrlTester> tester, Func<IElasticClient, Task<TResponse>> call)
			where TResponse : IResponse => await (await tester).WhenCallingAsync(call, "request async");

		public static async Task<UrlTester> FluentAsync<TResponse>(this Task<UrlTester> tester, Func<IElasticClient, Task<TResponse>> call)
			where TResponse : IResponse => await (await tester).WhenCallingAsync(call, "fluent async");
	}

	public class UrlTester
	{
		protected string ExpectedUrl { get; set; }
		protected HttpMethod ExpectedHttpMethod { get; set; }
		private IElasticClient Client { get; }

		internal UrlTester(HttpMethod method, string expectedUrl, Func<ConnectionSettings, ConnectionSettings> settings = null)
		{
			this.ExpectedHttpMethod = method;
			this.ExpectedUrl = expectedUrl;
			this.Client = settings == null
				? TestClient.DefaultInMemoryClient
				: new ElasticClient(settings(new AlwaysInMemoryConnectionSettings()));
		}

		public static UrlTester ExpectUrl(HttpMethod method, string url, Func<ConnectionSettings, ConnectionSettings> settings = null) =>
			new UrlTester(method, url, settings);
		public static UrlTester POST(string url) =>  new UrlTester(HttpMethod.POST, url);
		public static UrlTester PUT(string url) =>  new UrlTester(HttpMethod.PUT, url);
		public static UrlTester GET(string url) =>  new UrlTester(HttpMethod.GET, url);
		public static UrlTester HEAD(string url) =>  new UrlTester(HttpMethod.HEAD, url);
		public static UrlTester DELETE(string url) =>  new UrlTester(HttpMethod.DELETE, url);
		public static string EscapeUriString(string s) => Uri.EscapeDataString(s);

		public UrlTester Fluent<TResponse>(Func<IElasticClient, TResponse> call) where TResponse : IResponse => WhenCalling(call, "fluent");
		public UrlTester Request<TResponse>(Func<IElasticClient, TResponse> call) where TResponse : IResponse => WhenCalling(call, "request");

		public Task<UrlTester> FluentAsync<TResponse>(Func<IElasticClient, Task<TResponse>> call) where TResponse : IResponse => WhenCallingAsync(call, "fluent async");
		public Task<UrlTester> RequestAsync<TResponse>(Func<IElasticClient, Task<TResponse>> call) where TResponse : IResponse => WhenCallingAsync(call, "request async");

		public UrlTester LowLevel(Func<IElasticLowLevelClient, IApiCallDetails> call)
		{
			var callDetails = call(TestClient.DefaultInMemoryClient.LowLevel);
			return Assert("lowlevel", callDetails);
		}

		private UrlTester WhenCalling<TResponse>(Func<IElasticClient, TResponse> call, string typeOfCall)
			where TResponse : IResponse
		{
			var callDetails = call(TestClient.DefaultInMemoryClient);
			return Assert(typeOfCall, callDetails.ApiCall);
		}

		internal async Task<UrlTester> WhenCallingAsync<TResponse>(Func<IElasticClient, Task<TResponse>> call, string typeOfCall)
			where TResponse : IResponse
		{
			var callDetails = (await call(TestClient.DefaultInMemoryClient)).ApiCall;
			return Assert(typeOfCall, callDetails);
		}

		private UrlTester Assert(string typeOfCall, IApiCallDetails callDetails)
		{
			var url = callDetails.Uri.PathAndQuery;
			callDetails.Uri.PathEquals(this.ExpectedUrl, typeOfCall);
			callDetails.HttpMethod.Should().Be(this.ExpectedHttpMethod, $"{typeOfCall} to {url}");
			return this;
		}
	}
}
