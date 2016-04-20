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

		internal UrlTester(HttpMethod method, string expectedUrl, Func<ConnectionSettings, ConnectionSettings> settings = null)
		{
			this.ExpectedHttpMethod = method;
			this.ExpectedUrl = expectedUrl;
			this._connectionSettingsModifier = (settings ?? (c =>c.PrettyJson(false)));
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
			var callDetails = call(this.GetClient().LowLevel);
			return Assert("lowlevel", callDetails);
		}

		internal UrlTester WhenCalling<TResponse>(Func<IElasticClient, TResponse> call, string typeOfCall)
			where TResponse : IResponse
		{
			var callDetails = call(this.GetClient());
			return Assert(typeOfCall, callDetails.CallDetails);
		}

		internal async Task<UrlTester> WhenCallingAsync<TResponse>(Func<IElasticClient, Task<TResponse>> call, string typeOfCall)
			where TResponse : IResponse
		{
			var callDetails = (await call(this.GetClient())).CallDetails;
			return Assert(typeOfCall, callDetails);
		}

		private UrlTester Assert(string typeOfCall, IApiCallDetails callDetails)
		{
			var url = callDetails.Uri.PathAndQuery;
			url.Should().Be(this.ExpectedUrl, $"when calling the {typeOfCall} Api");
			callDetails.HttpMethod.Should().Be(this.ExpectedHttpMethod, typeOfCall);
			return this;
		}

		public static IntermediateUrlTester WithConnectionSettings(Func<ConnectionSettings, ConnectionSettings> settings) =>  new IntermediateUrlTester(settings);

		public static UrlTester ExpectUrl(HttpMethod method, string url) =>  new UrlTester(method, url);
		public static UrlTester POST(string url) =>  new UrlTester(HttpMethod.POST, url);
		public static UrlTester PUT(string url) =>  new UrlTester(HttpMethod.PUT, url);
		public static UrlTester GET(string url) =>  new UrlTester(HttpMethod.GET, url);
		public static UrlTester HEAD(string url) =>  new UrlTester(HttpMethod.HEAD, url);
		public static UrlTester DELETE(string url) =>  new UrlTester(HttpMethod.DELETE, url);
		public static string EscapeUriString(string s) => Uri.EscapeDataString(s);
	}

	public class IntermediateUrlTester
	{
		private readonly Func<ConnectionSettings, ConnectionSettings> _connectionSettingsModifier;

		internal IntermediateUrlTester(Func<ConnectionSettings, ConnectionSettings> settings)
		{
			this._connectionSettingsModifier = settings;
		}
		public UrlTester ExpectUrl(HttpMethod method, string url) =>  new UrlTester(method, url, _connectionSettingsModifier);
		public UrlTester POST(string url) =>  new UrlTester(HttpMethod.POST, url, _connectionSettingsModifier);
		public UrlTester PUT(string url) =>  new UrlTester(HttpMethod.PUT, url, _connectionSettingsModifier);
		public UrlTester GET(string url) =>  new UrlTester(HttpMethod.GET, url, _connectionSettingsModifier);
		public UrlTester HEAD(string url) =>  new UrlTester(HttpMethod.HEAD, url, _connectionSettingsModifier);
		public UrlTester DELETE(string url) =>  new UrlTester(HttpMethod.DELETE, url, _connectionSettingsModifier);
	}

}
