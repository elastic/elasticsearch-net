using System;
using System.Collections.Generic;
using Elasticsearch.Net;
using FluentAssertions;
using Tests.Framework;

namespace Tests.Reproduce
{
	public class GithubIssue2052
	{

		[U] public void BadSimpleJsonDeserialization()
		{

			var ex = this.GimmeACaughtException();

			var document = new Dictionary<string,object>{
			    { "message", "My message"},
			    { "exception", ex }
			};

			var pool = new StaticConnectionPool(new List<Uri> { new Uri("http://localhost:9200") });
			var memoryConnection = new InMemoryConnection();
			var connectionSettings = new ConnectionConfiguration(pool, memoryConnection);
			var client = new ElasticLowLevelClient(connectionSettings);

			List<object> payload = new List<object>{
			    new { index = new { _index = "myIndex", _type = "myDocumentType" } },
			    document
			};
			Action act = () => client.Bulk<byte[]>(payload);
			var e = act.ShouldThrow<UnexpectedElasticsearchClientException>();

			//throw new Exception(e.Subject.First().DebugInformation);

		}

		private Exception GimmeACaughtException()
		{
			try
			{
				throw new Exception("Some exception");
			}
			catch(Exception e)
			{
				return e;
			}
		}

	}
}
