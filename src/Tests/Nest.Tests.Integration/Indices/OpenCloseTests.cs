using Elasticsearch.Net;
using FluentAssertions;
using Nest.Tests.MockData.Domain;
using NUnit.Framework;

namespace Nest.Tests.Integration.Indices
{
	[TestFixture]
	public class OpenCloseTests : IntegrationTests
	{
		[Test]
		public void CloseAndOpenIndex()
		{
			var r = this.Client.CloseIndex(ElasticsearchConfiguration.DefaultIndex);
			Assert.True(r.Acknowledged);
			Assert.True(r.IsValid);
			r = this.Client.OpenIndex(ElasticsearchConfiguration.DefaultIndex);
			Assert.True(r.Acknowledged);
			Assert.True(r.IsValid);
		}
		[Test]
		public void CloseAndOpenIndexTyped()
		{
			var r = this.Client.CloseIndex<ElasticsearchProject>();
			Assert.True(r.Acknowledged);
			Assert.True(r.IsValid);
			r = this.Client.OpenIndex<ElasticsearchProject>();
			Assert.True(r.Acknowledged);
			Assert.True(r.IsValid);
		}
		[Test]
		public void CloseAndSearchAndOpenIndex()
		{
			var r = this.Client.CloseIndex(ElasticsearchConfiguration.DefaultIndex);
			Assert.True(r.Acknowledged);
			Assert.True(r.IsValid);
			var results = this.Client.Search<ElasticsearchProject>(s => s
				.MatchAll()
			);

			Assert.False(results.IsValid);
			Assert.IsNotNull(results.ConnectionStatus);
			var statusCode = results.ConnectionStatus.HttpStatusCode;
			Assert.AreEqual(statusCode, 403);

			var error = results.ServerError;
			error.Should().NotBeNull();
			error.ExceptionType.Should().Be("ClusterBlockException");
			error.Error.Should().Contain("index closed");

			r = this.Client.OpenIndex(ElasticsearchConfiguration.DefaultIndex);
			Assert.True(r.Acknowledged);
			Assert.True(r.IsValid);
		}
	}
}