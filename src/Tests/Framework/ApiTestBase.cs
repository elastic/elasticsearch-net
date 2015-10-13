using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Nest;
using Tests.Framework.Integration;
using Elasticsearch.Net;

namespace Tests.Framework
{
	public abstract class ApiTestBase<TResponse, TInterface, TDescriptor, TInitializer> 
		: SerializationTestBase
		where TResponse : class, IResponse
		where TDescriptor : class, TInterface
		where TInitializer : class, TInterface
		where TInterface : class
	{
		protected ApiTestBase(IIntegrationCluster cluster, EndpointUsage usage)
		{
			this._cluster = cluster;
			this._integrationPort = cluster.Node.Port;
			this._responses = usage.CallOnce(this.ClientUsage);
		}
		private readonly IIntegrationCluster _cluster;
		private readonly LazyResponses _responses;
		private readonly int _integrationPort;

		protected static string RandomString() => Guid.NewGuid().ToString("N").Substring(0, 8);

		protected abstract LazyResponses ClientUsage();
		protected virtual void OnBeforeCall(IElasticClient client) { }

		protected abstract int ExpectStatusCode { get; }
		protected abstract bool ExpectIsValid { get; }
		protected abstract string UrlPath { get; }
		protected abstract HttpMethod HttpMethod { get; }

		protected virtual TInitializer Initializer => Activator.CreateInstance<TInitializer>();
		protected virtual Func<TDescriptor, TInterface> Fluent { get; }
		protected virtual TDescriptor ClientDoesThisInternally(TDescriptor d) => d;

		protected virtual ConnectionSettings GetConnectionSettings(ConnectionSettings settings) => settings;
		protected virtual IElasticClient Client => this._cluster.Client(GetConnectionSettings);
		protected virtual TDescriptor NewDescriptor() => Activator.CreateInstance<TDescriptor>();

		protected LazyResponses Calls(
			Func<IElasticClient, Func<TDescriptor, TInterface>, TResponse> fluent,
			Func<IElasticClient, Func<TDescriptor, TInterface>, Task<TResponse>> fluentAsync,
			Func<IElasticClient, TInitializer, TResponse> request,
			Func<IElasticClient, TInitializer, Task<TResponse>> requestAsync
		)
		{
			var client = this.Client;
			return new LazyResponses(async () =>
			{
				var dict = new Dictionary<string, IResponse>();
				OnBeforeCall(client);
				dict.Add("fluent", fluent(client, this.Fluent));
				OnBeforeCall(client);
				dict.Add("fluentAsync", await fluentAsync(client, this.Fluent));
				OnBeforeCall(client);
				dict.Add("request", request(client, this.Initializer));
				OnBeforeCall(client);
				dict.Add("requestAsync", await requestAsync(client, this.Initializer));
				return dict;
			});
		}

		private void AssertUrl(Uri u)
		{
			var paths = (this.UrlPath ?? "").Split(new [] { '?' }, 2);
			string path = paths.First(), query = string.Empty;
			if (paths.Length > 1)
				query = paths.Last();

			var expectedUri = new UriBuilder("http","localhost", _integrationPort, path,  "?" + query).Uri;

			u.AbsolutePath.Should().Be(expectedUri.AbsolutePath);
			u = new UriBuilder(u.Scheme, u.Host, u.Port, u.AbsolutePath, u.Query.Replace("pretty=true", "")).Uri;

			var queries = new[] {u.Query, expectedUri.Query};
			if (queries.All(string.IsNullOrWhiteSpace)) return;
			if (queries.Any(string.IsNullOrWhiteSpace))
			{
				queries.Last().Should().Be(queries.First());
				return;
			}

			var clientKeyValues = u.Query.Substring(1).Split('&')
				.Select(v => v.Split('='))
				.Where(k=>!string.IsNullOrWhiteSpace(k[0]))
				.ToDictionary(k => k[0], v => v.Last());
			var expectedKeyValues = expectedUri.Query.Substring(1).Split('&')
				.Select(v => v.Split('='))
				.Where(k=>!string.IsNullOrWhiteSpace(k[0]))
				.ToDictionary(k => k[0], v => v.Last());

			clientKeyValues.Count().Should().Be(expectedKeyValues.Count());
			clientKeyValues.Should().ContainKeys(expectedKeyValues.Keys.ToArray());
			clientKeyValues.Should().Equal(expectedKeyValues);
		}

		[I] protected async Task HandlesStatusCode() =>
			await this.AssertOnAllResponses(r=>r.ApiCall.HttpStatusCode.Should().Be(this.ExpectStatusCode));

		[I] protected async Task ReturnsExpectedIsValid() =>
			await this.AssertOnAllResponses(r=>r.IsValid.Should().Be(this.ExpectIsValid));

		protected async Task AssertOnAllResponses(Action<TResponse> assert)
		{
			var responses = await this._responses;
			foreach (var kv in responses)
			{
				var response = kv.Value as TResponse;
				try
				{
					assert(response);
				}
#pragma warning disable 7095
				catch (Exception ex) when (true)
#pragma warning restore 7095
				{
					throw new Exception($"asserting over the response from: {kv.Key} failed: {ex.Message}", ex);
				}
			}
		}

		[U] protected async Task HitsTheCorrectUrl() => await this.AssertOnAllResponses(r => this.AssertUrl(r.ApiCall.Uri));

		[U] protected async Task UsesCorrectHttpMethod() => await this.AssertOnAllResponses(r => r.CallDetails.HttpMethod.Should().Be(this.HttpMethod));

		[U] protected void SerializesRequest() => this.AssertSerializesAndRoundTrips<TInterface>(this.Initializer);

		[U] protected void SerializesFluent() => this.AssertSerializesAndRoundTrips(this.Fluent?.Invoke(this.ClientDoesThisInternally(NewDescriptor())));

	}

}
