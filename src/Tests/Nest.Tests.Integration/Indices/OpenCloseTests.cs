using Elasticsearch.Net;
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
			var r = this._client.CloseIndex(ElasticsearchConfiguration.DefaultIndex);
			Assert.True(r.Acknowledged);
			Assert.True(r.IsValid);
			r = this._client.OpenIndex(ElasticsearchConfiguration.DefaultIndex);
			Assert.True(r.Acknowledged);
			Assert.True(r.IsValid);
		}
		[Test]
		public void CloseAndOpenIndexTyped()
		{
			var r = this._client.CloseIndex<ElasticsearchProject>();
			Assert.True(r.Acknowledged);
			Assert.True(r.IsValid);
			r = this._client.OpenIndex<ElasticsearchProject>();
			Assert.True(r.Acknowledged);
			Assert.True(r.IsValid);
		}
		[Test]
		public void CloseAndSearchAndOpenIndex()
		{
			var r = this._client.CloseIndex(ElasticsearchConfiguration.DefaultIndex);
			Assert.True(r.Acknowledged);
			Assert.True(r.IsValid);
			var results = this._client.Search<ElasticsearchProject>(s => s
				.MatchAll()
			);

			Assert.False(results.IsValid);
			Assert.IsNotNull(results.ConnectionStatus);
			var statusCode = results.ConnectionStatus.HttpStatusCode;
			Assert.AreEqual(statusCode, 403);
			var result = results.ConnectionStatus.ResponseRaw.Utf8String();
			Assert.True(result.Contains("ClusterBlockException"));
			Assert.True(result.Contains("index closed"));
			r = this._client.OpenIndex(ElasticsearchConfiguration.DefaultIndex);
			Assert.True(r.Acknowledged);
			Assert.True(r.IsValid);
		}
	}
}