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


		[Fact] private void SerializesInitializer() => this.AssertSerializes(this.InstanceInitializer);

		[Fact] private void SerializesFluent() => this.AssertSerializes(this.InstanceFluent);
	}

	public abstract class EndpointUsageTests<TResponse>
		: SerializationTests
		where TResponse : IResponse
	{
		public abstract int ExpectStatusCode { get; }
		public abstract bool ExpectIsValid { get; }
		public abstract void AssertUrl(string url);

		protected TResponse InstanceInitializer { get; private set; }
		protected TResponse InstanceFluent { get; private set; }

		protected abstract TResponse Initializer(IElasticClient client);
		protected abstract TResponse Fluent(IElasticClient client);

		public EndpointUsageTests()
		{
			var client = this.Client();
			this.InstanceInitializer = this.Initializer(client);
			this.InstanceFluent = this.Fluent(client);
		}

		protected virtual ConnectionSettings ConnectionSettings(ConnectionSettings settings) => settings; 
		protected virtual IElasticClient Client() => TestClient.GetClient(ConnectionSettings); 

		private void Dispatch(Action<TResponse> assert)
		{
			assert(this.InstanceFluent);
			assert(this.InstanceInitializer);
		}

		[Fact] void HandlesStatusCode() =>
			this.Dispatch(r=>r.ConnectionStatus.HttpStatusCode.Should().Be(this.ExpectStatusCode));

		[Fact] private void Serializes() => this.Dispatch(r=> this.AssertSerializes(r));

	}
}
