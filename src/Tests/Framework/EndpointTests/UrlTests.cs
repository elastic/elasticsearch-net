using System;
using System.Threading.Tasks;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;

namespace Tests.Framework
{
	public interface IUrlTests
	{
		Task Urls();
	}

	public static class UrlTesterExtensions
	{
		public static async Task<UrlTester> RequestAsync<TResponse>(this Task<UrlTester> tester, Func<IElasticClient, Task<TResponse>> call)
			where TResponse : IResponse => await (await tester).WhenCallingAsync(call, "request async");

		public static async Task<UrlTester> FluentAsync<TResponse>(this Task<UrlTester> tester, Func<IElasticClient, Task<TResponse>> call)
			where TResponse : IResponse => await (await tester).WhenCallingAsync(call, "fluent async");
	}

	public class UrlTester : SerializationTestBase
	{
		protected string ExpectedUrl { get; set; }
		protected HttpMethod ExpectedHttpMethod { get; set; }

		protected override object ExpectJson => null;

		internal UrlTester(HttpMethod method, string expectedUrl)
		{
			this.ExpectedHttpMethod = method;
			this.ExpectedUrl = expectedUrl;
		}

		public UrlTester Fluent<TResponse>(Func<IElasticClient, TResponse> call)
			where TResponse : IResponse => WhenCalling(call, "fluent");

		public Task<UrlTester> FluentAsync<TResponse>(Func<IElasticClient, Task<TResponse>> call)
			where TResponse : IResponse => WhenCallingAsync(call, "fluent async");

		public UrlTester Request<TResponse>(Func<IElasticClient, TResponse> call)
			where TResponse : IResponse => WhenCalling(call, "request");

		public Task<UrlTester> RequestAsync<TResponse>(Func<IElasticClient, Task<TResponse>> call)
			where TResponse : IResponse => WhenCallingAsync(call, "request async");

		public UrlTester LowLevel(Func<IElasticLowLevelClient, IApiCallDetails> call)
		{
			var callDetails = call(this.Client.LowLevel);
			return Assert("lowlevel", callDetails);
		}

		internal UrlTester WhenCalling<TResponse>(Func<IElasticClient, TResponse> call, string typeOfCall)
			where TResponse : IResponse
		{
			var callDetails = call(this.Client);
			return Assert(typeOfCall, callDetails.ApiCall);
		}

		internal async Task<UrlTester> WhenCallingAsync<TResponse>(Func<IElasticClient, Task<TResponse>> call, string typeOfCall)
			where TResponse : IResponse
		{
			var callDetails = (await call(this.Client)).ApiCall;
			return Assert(typeOfCall, callDetails);
		}

		private UrlTester Assert(string typeOfCall, IApiCallDetails callDetails)
		{
			var url = callDetails.Uri.PathAndQuery;
			callDetails.Uri.PathEquals(this.ExpectedUrl, typeOfCall);
			callDetails.HttpMethod.Should().Be(this.ExpectedHttpMethod, $"{typeOfCall} to {url}");
			return this;
		}

		public static UrlTester ExpectUrl(HttpMethod method, string url) =>  new UrlTester(method, url);
		public static UrlTester POST(string url) =>  new UrlTester(HttpMethod.POST, url);
		public static UrlTester PUT(string url) =>  new UrlTester(HttpMethod.PUT, url);
		public static UrlTester GET(string url) =>  new UrlTester(HttpMethod.GET, url);
		public static UrlTester HEAD(string url) =>  new UrlTester(HttpMethod.HEAD, url);
		public static UrlTester DELETE(string url) =>  new UrlTester(HttpMethod.DELETE, url);
		public static string EscapeUriString(string s) => Uri.EscapeDataString(s);
	}


	public static class CapturingUrlTesterExtensions
	{
		public static async Task<CapturingUrlTester> RequestAsync<TResponse>(this Task<CapturingUrlTester> tester, Func<IElasticClient, Task<TResponse>> call)
			=> await (await tester).WhenCallingAsync(call, "request async");

		public static async Task<CapturingUrlTester> FluentAsync<TResponse>(this Task<CapturingUrlTester> tester, Func<IElasticClient, Task<TResponse>> call)
			=> await (await tester).WhenCallingAsync(call, "fluent async");
	}

	public class CapturingUrlTester : SerializationTestBase
	{
		protected string ExpectedUrl { get; set; }
		protected HttpMethod ExpectedHttpMethod { get; set; }
		protected IApiCallDetails CallDetails { get; set; }

		protected override object ExpectJson => null;

		internal CapturingUrlTester(HttpMethod method, string expectedUrl)
		{
			this.ExpectedHttpMethod = method;
			this.ExpectedUrl = expectedUrl;
			this.ConnectionSettingsModifier = (c => c
				.PrettyJson(false)
				.OnRequestCompleted(h => CallDetails = h));
		}

		public CapturingUrlTester Fluent<TResponse>(Func<IElasticClient, TResponse> call) => WhenCalling(call, "fluent");

		public Task<CapturingUrlTester> FluentAsync<TResponse>(Func<IElasticClient, Task<TResponse>> call)
			=> WhenCallingAsync(call, "fluent async");

		public CapturingUrlTester Request<TResponse>(Func<IElasticClient, TResponse> call)
			=> WhenCalling(call, "request");

		public Task<CapturingUrlTester> RequestAsync<TResponse>(Func<IElasticClient, Task<TResponse>> call)
			=> WhenCallingAsync(call, "request async");

		internal CapturingUrlTester WhenCalling<TResponse>(Func<IElasticClient, TResponse> call, string typeOfCall)
		{
			call(this.Client);
			return Assert(typeOfCall, CallDetails);
		}

		internal async Task<CapturingUrlTester> WhenCallingAsync<TResponse>(Func<IElasticClient, Task<TResponse>> call, string typeOfCall)
		{
			await call(this.Client);
			return Assert(typeOfCall, CallDetails);
		}

		private CapturingUrlTester Assert(string typeOfCall, IApiCallDetails callDetails)
		{
			var url = callDetails.Uri.PathAndQuery;
			url.Should().Be(this.ExpectedUrl, $"when calling the {typeOfCall} Api");
			callDetails.HttpMethod.Should().Be(this.ExpectedHttpMethod, typeOfCall);
			return this;
		}

		public static CapturingUrlTester ExpectUrl(HttpMethod method, string url) => new CapturingUrlTester(method, url);
		public static CapturingUrlTester POST(string url) => new CapturingUrlTester(HttpMethod.POST, url);
		public static CapturingUrlTester PUT(string url) => new CapturingUrlTester(HttpMethod.PUT, url);
		public static CapturingUrlTester GET(string url) => new CapturingUrlTester(HttpMethod.GET, url);
		public static CapturingUrlTester HEAD(string url) => new CapturingUrlTester(HttpMethod.HEAD, url);
		public static CapturingUrlTester DELETE(string url) => new CapturingUrlTester(HttpMethod.DELETE, url);
		public static string EscapeUriString(string s) => Uri.EscapeDataString(s);
	}
}
