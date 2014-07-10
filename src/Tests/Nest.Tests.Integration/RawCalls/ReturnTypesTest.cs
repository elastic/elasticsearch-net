using System;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using Nest.Tests.MockData.Domain;
using Elasticsearch.Net;

namespace Nest.Tests.Integration.RawCalls
{
	[TestFixture]
	public class ReturnTypeTests : IntegrationTests
	{
		private IElasticClient _clientThatDoesNotExpose; 

		public ReturnTypeTests()
		{
			var settings = ElasticsearchConfiguration.Settings()
				.ExposeRawResponse(false);
			_clientThatDoesNotExpose = new ElasticClient(settings);
		}


		[Test]
		public void StringReturn()
		{
			var r = this._clientThatDoesNotExpose.Raw.Info<string>();
			r.Response.Should().NotBeNullOrEmpty();
			r.ResponseRaw.Should().BeNull();
		}
		
		[Test]
		public async void StringReturn_Async()
		{
			var r = await this._clientThatDoesNotExpose.Raw.InfoAsync<string>();
			r.Response.Should().NotBeNullOrEmpty();
			r.ResponseRaw.Should().BeNull();
		}
		
		[Test]
		public void ByteArrayReturn()
		{
			var r = this._clientThatDoesNotExpose.Raw.Info<byte[]>();
			r.Response.Should().NotBeNull();
			r.ResponseRaw.Should().BeNull();
		}
		
		[Test]
		public async void ByteArrayReturn_Async()
		{
			var r = await this._clientThatDoesNotExpose.Raw.InfoAsync<byte[]>();
			r.Response.Should().NotBeNull();
			r.ResponseRaw.Should().BeNull();
		}
		[Test]
		public void VoidResponseReturn()
		{
			var r = this._clientThatDoesNotExpose.Raw.Info<VoidResponse>();
			r.Response.Should().BeNull();
			r.ResponseRaw.Should().BeNull();
		}
		
		[Test]
		public async void VoidResponseReturn_Async()
		{
			var r = await this._client.Raw.InfoAsync<VoidResponse>();
			r.Response.Should().BeNull();
			r.ResponseRaw.Should().BeNull();
		}
	}
}
