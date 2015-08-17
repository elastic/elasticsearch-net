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
	public abstract class EndpointUsageBase<TResponse, TInterface, TDescriptor, TInitializer> : SerializationBase
		where TResponse : class, IResponse
		where TDescriptor : class, TInterface, new() 
		where TInitializer : class, TInterface
		where TInterface : class
	{
		private LazyResponses _responses;

		public abstract int ExpectStatusCode { get; }
		public abstract bool ExpectIsValid { get; }
		public abstract string UrlPath { get; }
		public abstract HttpMethod HttpMethod { get; }

		protected abstract TInitializer Initializer { get; }
		protected abstract Func<TDescriptor, TInterface> Fluent { get; }
		protected virtual TDescriptor ClientDoesThisInternally(TDescriptor d) => d;
		
		protected EndpointUsageBase(IIntegrationCluster cluster, ApiUsage usage)
		{
			 this.IntegrationPort = cluster.Node.Port;
			 this._responses = usage.CallOnce(this.ClientUsage);
		}

		protected abstract LazyResponses ClientUsage();

		protected LazyResponses Calls(
			Func<IElasticClient, Func<TDescriptor, TInterface>, TResponse> fluent,
			Func<IElasticClient, Func<TDescriptor, TInterface>, Task<TResponse>> fluentAsync,
			Func<IElasticClient, TInitializer, TResponse> request,
			Func<IElasticClient, TInitializer, Task<TResponse>> requestAsync
		)
		{
			var client = this.GetClient();
			return new LazyResponses(async () =>
			{
				var dict = new Dictionary<string, IResponse>
				{
					{"fluent", fluent(client, this.Fluent)},
					{"fluentAsync", await fluentAsync(client, this.Fluent)},
					{"request", request(client, this.Initializer)},
					{"requestAsync", await requestAsync(client, this.Initializer)}
				};

				return dict;
			});
		}

		protected int IntegrationPort { get; set; } = 9200;
		protected virtual ConnectionSettings GetConnectionSettings(ConnectionSettings settings) => settings; 
		protected virtual IElasticClient GetClient() => TestClient.GetClient(GetConnectionSettings, IntegrationPort); 

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
				catch(Exception ex) when (false)
				{
					throw new Exception($"asserting over the response from: {kv.Key} failed: {ex.Message}", ex);
				}
			}
		}

		private void AssertUrl(Uri u)
		{
			var paths = (this.UrlPath ?? "").Split(new [] { '?' }, 2);
			string path = paths.First(), query = string.Empty;
			if (paths.Length > 1)
				query = paths.Last();

			var expectedUri = new UriBuilder("http","localhost", IntegrationPort, path,  "?" + query).Uri;

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

		[I] protected async void HandlesStatusCode() =>
			await this.AssertOnAllResponses(r=>r.ApiCall.HttpStatusCode.Should().Be(this.ExpectStatusCode));

		[I] protected async void ReturnsExpectedIsValid() =>
			await this.AssertOnAllResponses(r=>r.IsValid.Should().Be(this.ExpectIsValid));

		[U] protected async Task HitsTheCorrectUrl() =>
			await this.AssertOnAllResponses(r=>this.AssertUrl(r.ApiCall.Uri));

		[U] protected async Task UsesCorrectHttpMethod() =>
			await this.AssertOnAllResponses(r=>r.CallDetails.HttpMethod.Should().Be(this.HttpMethod));

		[U] protected void SerializesInitializer() => 
			this.AssertSerializesAndRoundTrips<TInterface>(this.Initializer);
		 
		[U] protected void SerializesFluent() => 
			this.AssertSerializesAndRoundTrips(this.Fluent(this.ClientDoesThisInternally(new TDescriptor())));

	}
}
