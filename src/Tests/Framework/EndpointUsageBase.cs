using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Nest;
using Tests.Framework.Integration;

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

		protected abstract TInitializer Initializer { get; }
		protected abstract Func<TDescriptor, TInterface> Fluent { get; }
		
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
					{"initializer", request(client, this.Initializer)},
					{"initializerAsync", await requestAsync(client, this.Initializer)}
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
				assert(response);
			}
		}

		private void AssertUrl(Uri uriThatClientHit)
		{
			var paths = (this.UrlPath ?? "").Split(new [] { '?' }, 2);
			string path = paths.First(), query = string.Empty;
			if (paths.Length > 1)
				query = paths.Last();

			var expectedUri = new UriBuilder("http","localhost", IntegrationPort, path, query).Uri;

			uriThatClientHit.AbsolutePath.Should().Be(expectedUri.AbsolutePath);
			var queries = new[] {uriThatClientHit.Query, expectedUri.Query};
			if (queries.All(string.IsNullOrWhiteSpace)) return;
			if (queries.Any(string.IsNullOrWhiteSpace))
			{
				query.First().Should().Be(query.Last());
				return;
			}

			var clientKeyValues = uriThatClientHit.Query.Split('&')
				.SelectMany(v => v.Split('='))
				.ToDictionary(k => k[0], v => v);
			var expectedKeyValues = expectedUri.Query.Split('&')
				.SelectMany(v => v.Split('='))
				.ToDictionary(k => k[0], v => v);

			clientKeyValues.Should().Equal(expectedKeyValues);
		}

		[I] protected async void HandlesStatusCode() =>
			await this.AssertOnAllResponses(r=>r.ApiCall.HttpStatusCode.Should().Be(this.ExpectStatusCode));

		[I] protected async void ReturnsExpectedIsValid() =>
			await this.AssertOnAllResponses(r=>r.IsValid.Should().Be(this.ExpectIsValid));

		[U] protected async Task HitsTheCorrectUrl() =>
			await this.AssertOnAllResponses(r=>this.AssertUrl(r.ApiCall.RequestUri));

		[U] protected void SerializesInitializer() => 
			this.AssertSerializesAndRoundTrips<TInterface>(this.Initializer);
		 
		[U] protected void SerializesFluent() => 
			this.AssertSerializesAndRoundTrips(this.Fluent(new TDescriptor()));

	}
}
