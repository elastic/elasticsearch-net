using FluentAssertions;
using NUnit.Framework;

namespace Nest.Tests.Integration.Connection.HttpClient
{
    [TestFixture]
    public class HttpClientTests : IntegrationTests
    {
        [Test]
        public void IndexExistShouldNotThrowOn404()
        {
            var unknownIndexResult = this._httpClientClient.IndexExists(i => i.Index("i-am-running-out-of-clever-index-names"));
            unknownIndexResult.Should().NotBeNull();
            unknownIndexResult.IsValid.Should().BeTrue();

            unknownIndexResult.Exists.Should().BeFalse();

            unknownIndexResult.ConnectionStatus.HttpStatusCode.Should().Be(404);
        }

        [Test]
        public void EmptyResponseShouldNotThrowError()
        {
            var result = this._httpClientClient.Connection.HeadSync(ElasticsearchConfiguration.CreateBaseUri(9200));
            result.Success.Should().BeTrue();
            result.OriginalException.Should().BeNull();
        }

    }
}
