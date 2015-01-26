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
		where TDescriptor : TInterface, new() where TInitializer : TInterface
	{
		public GeneralUsageTests()
		{
			
		}


	}

	public abstract class EndpointUsageTests<TInterface, TDescriptor, TInitializer, TResponse> 
		where TDescriptor : TInterface, new() where TInitializer : TInterface
		where TResponse : IResponse
	{
		public abstract int ExpectStatusCode { get; }
		public abstract bool ExpectIsValid { get; }
		public abstract void AssertUrl(string url);

		protected abstract TResponse Initializer(IElasticClient client);
		protected abstract TResponse Fluent(IElasticClient client);

		public EndpointUsageTests()
		{
			this.InitializerResponse = this.Initializer(TestClient.Client);
			this.FluentResponse = this.Fluent(TestClient.Client);
		}

		protected TResponse FluentResponse { get; private set; }

		protected TResponse InitializerResponse { get; private set; }

		private void Dispatch(Action<TResponse> assert)
		{
			assert(this.FluentResponse);
			assert(this.InitializerResponse);
		}


		[Fact] void HandlesStatusCode() =>
			this.Dispatch(r=>r.ConnectionStatus.HttpStatusCode.Should().Be(this.ExpectStatusCode));

	}
}
