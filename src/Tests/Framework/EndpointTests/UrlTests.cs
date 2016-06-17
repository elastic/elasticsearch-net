using System;
using System.Linq;
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
		private string ExpectedUrl { get; }
		private HttpMethod ExpectedHttpMethod { get; }

		internal UrlTester(HttpMethod method, string expectedUrl)
		{
			this.ExpectedHttpMethod = method;
			this.ExpectedUrl = expectedUrl;
		}

		public UrlTester Fluent<TResponse>(Func<IElasticClient, TResponse> call) where TResponse : IResponse =>
			WhenCalling(call, "fluent");

		public Task<UrlTester> FluentAsync<TResponse>(Func<IElasticClient, Task<TResponse>> call) where TResponse : IResponse =>
			WhenCallingAsync(call, "fluent async");

		public UrlTester Request<TResponse>(Func<IElasticClient, TResponse> call) where TResponse : IResponse =>
			WhenCalling(call, "request");

		public Task<UrlTester> RequestAsync<TResponse>(Func<IElasticClient, Task<TResponse>> call) where TResponse : IResponse =>
			WhenCallingAsync(call, "request async");

		public UrlTester LowLevel(Func<IElasticLowLevelClient, IApiCallDetails> call)
		{
			var callDetails = call(this.Client.LowLevel);
			return Assert("lowlevel", callDetails);
		}

		private UrlTester WhenCalling<TResponse>(Func<IElasticClient, TResponse> call, string typeOfCall)
			where TResponse : IResponse
		{
			var callDetails = call(this.Client);
			return Assert(typeOfCall, callDetails.CallDetails);
		}

		internal async Task<UrlTester> WhenCallingAsync<TResponse>(Func<IElasticClient, Task<TResponse>> call, string typeOfCall)
			where TResponse : IResponse
		{
			var callDetails = (await call(this.Client)).CallDetails;
			return Assert(typeOfCall, callDetails);
		}

		private UrlTester Assert(string typeOfCall, IApiCallDetails callDetails)
		{
			callDetails.HttpMethod.Should().Be(this.ExpectedHttpMethod, typeOfCall);
			ComparePathAndQuerystring(this.ExpectedUrl, callDetails.Uri);
			return this;
		}

		public static void ComparePathAndQuerystring(string expectedPathAndUri, Uri actualUri)
		{
			var paths = (expectedPathAndUri ?? "").Split(new[] {'?'}, 2);
			string path = paths.First(), query = string.Empty;
			if (paths.Length > 1)
				query = paths.Last();

			var expectedUri = new UriBuilder("http", "localhost", 9200, path, "?" + query).Uri;

			actualUri.AbsolutePath.Should().Be(expectedUri.AbsolutePath);
			actualUri =
				new UriBuilder(actualUri.Scheme, actualUri.Host, actualUri.Port, actualUri.AbsolutePath,
					actualUri.Query.Replace("pretty=true&", "").Replace("pretty=true", "")).Uri;

			var queries = new[] {actualUri.Query, expectedUri.Query};
			if (queries.All(string.IsNullOrWhiteSpace)) return;
			if (queries.Any(string.IsNullOrWhiteSpace))
			{
				queries.Last().Should().Be(queries.First());
				return;
			}

			var clientKeyValues = actualUri.Query.Substring(1).Split('&')
				.Select(v => v.Split('='))
				.Where(k => !string.IsNullOrWhiteSpace(k[0]))
				.ToDictionary(k => k[0], v => v.Last());
			var expectedKeyValues = expectedUri.Query.Substring(1).Split('&')
				.Select(v => v.Split('='))
				.Where(k => !string.IsNullOrWhiteSpace(k[0]))
				.ToDictionary(k => k[0], v => v.Last());

			clientKeyValues.Count().Should().Be(expectedKeyValues.Count());
			clientKeyValues.Should().ContainKeys(expectedKeyValues.Keys.ToArray());
			clientKeyValues.Should().Equal(expectedKeyValues);
		}


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

		protected override IElasticClient Client => TestClient.GetInMemoryClient(c => c
				.PrettyJson(false)
				.OnRequestCompleted(h => CallDetails = h));

		internal CapturingUrlTester(HttpMethod method, string expectedUrl)
		{
			this.ExpectedHttpMethod = method;
			this.ExpectedUrl = expectedUrl;
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
