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
			this.InstanceInitializer = this.Initializer(TestClient.Client);
			var func = this.Fluent(TestClient.Client);
			this.InstanceFluent = func(new TDescriptor());
		}

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
			this.InstanceInitializer = this.Initializer(TestClient.Client);
			this.InstanceFluent = this.Fluent(TestClient.Client);
		}

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
