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

namespace Nest.Tests.Literate
{
	public abstract class GeneralUsageTests<TInterface, TDescriptor, TInitializer>
		: SerializationTests
		where TDescriptor : TInterface, new() 
		where TInitializer : TInterface
	{
		protected TInterface InstanceInitializer { get; private set; }
		protected TInterface InstanceFluent { get; private set; }

		protected abstract TInitializer Initializer(IElasticClient client);
		protected abstract Func<TDescriptor, TInterface> Fluent(IElasticClient client);

		public GeneralUsageTests()
		{
			var client = this.Client();
			this.InstanceInitializer = this.Initializer(client);
			var func = this.Fluent(client);
			this.InstanceFluent = func(new TDescriptor());
		}

		protected virtual ConnectionSettings ConnectionSettings(ConnectionSettings settings) => settings; 
		protected virtual IElasticClient Client() => TestClient.GetClient(ConnectionSettings); 

		protected virtual void Setup(IElasticClient client) { }
		protected virtual void Teardown(IElasticClient client) { }


		[Fact] protected void SerializesInitializer() => this.AssertSerializes(this.InstanceInitializer);

		[Fact] protected void SerializesFluent() => this.AssertSerializes(this.InstanceFluent);
	}

	public abstract class EndpointUsageTests<TResponse, TInterface, TDescriptor, TInitializer> : SerializationTests
		where TResponse : IResponse
		where TDescriptor : TInterface, new() 
		where TInitializer : TInterface
	{
		protected class T { };

		private Func<IElasticClient, Func<TDescriptor, TInterface>, TResponse> _fluentCall;
		private Func<IElasticClient, Func<TDescriptor, TInterface>, Task<TResponse>> _fluentAsyncCall;
		private Func<IElasticClient, TInitializer, TResponse> _requestCall;
		private Func<IElasticClient, TInitializer, Task<TResponse>> _requestAsyncCall;

		public abstract int ExpectStatusCode { get; }
		public abstract bool ExpectIsValid { get; }
		public abstract void AssertUrl(string url);

		protected TResponse InstanceInitializer { get; private set; }
		protected TResponse InstanceFluent { get; private set; }

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
			this._fluentCall = fluent;
			this._fluentAsyncCall = fluentAsync;
			this._requestCall = request;
			this._requestAsyncCall = requestAsync;
		}

		public EndpointUsageTests()
		{
			var client = this.Client();
			this.ClientUsage();
			this.InstanceInitializer = this._requestCall(client, this.Initializer);
			this.InstanceFluent = this._fluentCall(client, this.Fluent);
		}

		protected virtual ConnectionSettings ConnectionSettings(ConnectionSettings settings) => settings; 
		protected virtual IElasticClient Client() => TestClient.GetClient(ConnectionSettings); 

		private void Dispatch(Action<TResponse> assert)
		{
			assert(this.InstanceFluent);
			assert(this.InstanceInitializer);
		}

		[Fact] protected void HandlesStatusCode() =>
			this.Dispatch(r=>r.ConnectionStatus.HttpStatusCode.Should().Be(this.ExpectStatusCode));

		[Fact] protected void SerializesInitializer() => this.AssertSerializes(this.Initializer);

		[Fact] protected void SerializesFluent() => this.AssertSerializes(this.Fluent(new TDescriptor()));

	}
}
