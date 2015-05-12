using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Elasticsearch.Net.Connection;
using FluentAssertions;
using Newtonsoft.Json.Linq;
using Ploeh.AutoFixture;
using SearchApis.RequestBody;
using Xunit;
using System.Runtime.CompilerServices;

namespace Nest.Tests.Literate
{
	public abstract class GeneralUsageTests<TInterface, TDescriptor, TInitializer> : SerializationTests
		where TDescriptor : TInterface, new()
		where TInitializer : class, TInterface
		where TInterface : class
	{
		protected abstract TInitializer Initializer { get; }
		protected abstract Func<TDescriptor, TInterface> Fluent { get; }

		protected TInterface FluentInstance { get; private set; }

		public GeneralUsageTests()
		{
			var client = this.Client();
			this.FluentInstance = this.Fluent(new TDescriptor());
		}

		protected virtual ConnectionSettings ConnectionSettings(ConnectionSettings settings) => settings; 
		protected virtual IElasticClient Client() => TestClient.GetClient(ConnectionSettings); 

		[Fact] protected void SerializesInitializer() => this.AssertSerializesAndRoundTrips(this.Initializer);

		[Fact] protected void SerializesFluent() => this.AssertSerializesAndRoundTrips(this.FluentInstance);
	}

	public class IntegrationFact : FactAttribute
	{
		public IntegrationFact()
		{
			if (!TestClient.RunIntegrationTests)
			{
				Skip = "Ignored because we are not running integration tests";
			}
		}
	}

	/// <summary>
	/// Provides support for asynchronous lazy initialization. This type is fully threadsafe.
	/// </summary>
	/// <typeparam name="T">The type of object that is being asynchronously initialized.</typeparam>
	public sealed class AsyncLazy<T>
	{
		/// <summary>
		/// The underlying lazy task.
		/// </summary>
		private readonly Lazy<Task<T>> instance;

		/// <summary>
		/// Initializes a new instance of the <see cref="AsyncLazy&lt;T&gt;"/> class.
		/// </summary>
		/// <param name="factory">The delegate that is invoked on a background thread to produce the value when it is needed.</param>
		public AsyncLazy(Func<T> factory)
		{
			instance = new Lazy<Task<T>>(() => Task.Run(factory));
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="AsyncLazy&lt;T&gt;"/> class.
		/// </summary>
		/// <param name="factory">The asynchronous delegate that is invoked on a background thread to produce the value when it is needed.</param>
		public AsyncLazy(Func<Task<T>> factory)
		{
			instance = new Lazy<Task<T>>(() => Task.Run(factory));
		}

		/// <summary>
		/// Asynchronous infrastructure support. This method permits instances of <see cref="AsyncLazy&lt;T&gt;"/> to be await'ed.
		/// </summary>
		public TaskAwaiter<T> GetAwaiter()
		{
			return instance.Value.GetAwaiter();
		}

		/// <summary>
		/// Starts the asynchronous initialization, if it has not already started.
		/// </summary>
		public void Start()
		{
			var unused = instance.Value;
		}
	}

	public abstract class EndpointUsageTests<TResponse, TInterface, TDescriptor, TInitializer> : SerializationTests
		where TResponse : IResponse
		where TDescriptor : class, TInterface, new() 
		where TInitializer : class, TInterface
		where TInterface : class
	{
		private AsyncLazy<IDictionary<string, TResponse>> Responses;

		public abstract int ExpectStatusCode { get; }
		public abstract bool ExpectIsValid { get; }
		public abstract void AssertUrl(string url);

		protected abstract TInitializer Initializer { get; }
		protected abstract Func<TDescriptor, TInterface> Fluent { get; }

		protected abstract void ClientUsage();
		protected void Calls(
			Func<IElasticClient, Func<TDescriptor, TInterface>, TResponse> fluent,
			Func<IElasticClient, Func<TDescriptor, TInterface>, Task<TResponse>> fluentAsync,
			Func<IElasticClient, TInitializer, TResponse> request,
			Func<IElasticClient, TInitializer, Task<TResponse>> requestAsync
		)
		{
			var client = this.Client();
			this.Responses = new AsyncLazy<IDictionary<string, TResponse>>(async () =>
			{
				var dict = new Dictionary<string, TResponse>();
				if (!TestClient.RunIntegrationTests) return dict;

				dict.Add("fluent", fluent(client, this.Fluent));
				dict.Add("fluentAsync", await fluentAsync(client, this.Fluent));
				dict.Add("initializer", request(client, this.Initializer));
				dict.Add("initializerAsync", await requestAsync(client, this.Initializer));
				return dict;
			});
		}

		public EndpointUsageTests()
		{
			this.ClientUsage();
		}

		protected virtual ConnectionSettings ConnectionSettings(ConnectionSettings settings) => settings; 
		protected virtual IElasticClient Client() => TestClient.GetClient(ConnectionSettings); 

		private async Task AssertOnAllResponses(Action<TResponse> assert)
		{
			foreach (var kv in await this.Responses)
			{
				assert(kv.Value);
			}
		}

		[IntegrationFact] protected async void HandlesStatusCode() =>
			await this.AssertOnAllResponses(r=>r.ConnectionStatus.HttpStatusCode.Should().Be(this.ExpectStatusCode));

		[IntegrationFact] protected async void ReturnsExpectedIsValid() =>
			await this.AssertOnAllResponses(r=>r.IsValid.Should().Be(this.ExpectIsValid));

		[IntegrationFact] protected async void HitsTheCorrectUrl() =>
			await this.AssertOnAllResponses(r=>this.AssertUrl(r.ConnectionStatus.RequestUrl));

		[Fact] protected void SerializesInitializer() => this.AssertSerializesAndRoundTrips(this.Initializer);

		[Fact] protected void SerializesFluent() => this.AssertSerializesAndRoundTrips(this.Fluent(new TDescriptor()));

	}
}
