using System.Linq;
using Elasticsearch.Net.Connection;
using FluentAssertions;
using NUnit.Framework;

namespace Nest.Tests.Integration.RawCalls
{
	[TestFixture]
	public class HasUsefultServerExceptionTests : IntegrationTests
	{
		[Test]
		public void MaxRetryException_DoesNotHide_ElasticsearchServerException()
		{
			var e = Assert.Throws<ElasticsearchServerException>(() =>
			{
				var result = this._clientThatThrows.Raw.Search("{ size: 10, searc}");
			});

			e.Status.Should().Be(400);
			e.ExceptionType.Should().Be("SearchPhaseExecutionException");
			e.Error.Should().Contain("Failed to parse source");
		}
		
		[Test]
		public void ServerExceptionMakesItOnResponse_EvenIfClientIsNotConfiguredToThrow()
		{
			var result = this._client.Raw.Search("{ size: 10, searc}");
			var e = result.OriginalException as ElasticsearchServerException;
			e.Should().NotBeNull();

			e.Status.Should().Be(400);
			e.ExceptionType.Should().Be("SearchPhaseExecutionException");
			e.Error.Should().Contain("Failed to parse source");
		}
	}
}
