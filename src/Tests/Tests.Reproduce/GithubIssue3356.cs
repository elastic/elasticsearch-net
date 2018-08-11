using System;
using System.IO;
using System.Text;
using Elastic.Xunit.XunitPlumbing;
using Elasticsearch.Net;
using FluentAssertions;
using Nest;
using Nest.JsonNetSerializer;

namespace Tests.Reproduce
{
	public class GithubIssue3356
	{
		[U]
		public void JoinFieldDeserializedCorrectly()
		{
			var pool = new SingleNodeConnectionPool(new Uri("http://localhost:9200"));
			var connectionSettings = new ConnectionSettings(pool, new InMemoryConnection(), JsonNetSerializer.Default)
				.DisableDirectStreaming()
				.DefaultIndex("docs");
			var client = new ElasticClient(connectionSettings);

			var doc = new MyDocument
			{
				Join = JoinField.Root("parent")
			};

			var response = client.IndexDocument(doc);

			Encoding.UTF8.GetString(response.ApiCall.RequestBodyInBytes).Should().Be("{\"join\":\"parent\"}");
			using (var stream = new MemoryStream(response.ApiCall.RequestBodyInBytes))
			{
				doc = client.SourceSerializer.Deserialize<MyDocument>(stream);
				doc.Join.Match(p =>
				{
					p.Name.Should().Be("parent");
				}, c => { });
			}
		}

		private class MyDocument
		{
			public JoinField Join { get; set; }
		}
	}
}
