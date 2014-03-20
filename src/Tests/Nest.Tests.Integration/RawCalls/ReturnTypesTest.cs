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
		[Test]
		public void StringReturn()
		{
			var r = this._client.Raw.Info<string>();
			r.Response.Should().NotBeNullOrEmpty();
			r.ResponseRaw.Should().BeNull();
		}
		
		[Test]
		public async void StringReturn_Async()
		{
			var r = await this._client.Raw.InfoAsync<string>();
			r.Response.Should().NotBeNullOrEmpty();
			r.ResponseRaw.Should().BeNull();
		}
	}
}
