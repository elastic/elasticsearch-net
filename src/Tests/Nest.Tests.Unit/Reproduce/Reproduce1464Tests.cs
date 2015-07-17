using Elasticsearch.Net;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Elasticsearch.Net.Connection;

namespace Nest.Tests.Unit.Reproduce
{
	[TestFixture]
	public class Reproduce1464Tests : BaseJsonTests
	{
		[Test]
		public void Issue1464()
		{
			var rawClient = new ElasticsearchClient(connection: new InMemoryConnection(new ConnectionSettings()));

			var bulkEntries = new List<object>();
			bulkEntries.Add(new { index = new { _index = "ourIndex", _type = "ourType", _id = "ourId" } });

			var doc = rawClient.Serializer.Deserialize<dynamic>(new MemoryStream("{ \"id\": \"3\"}".Utf8Bytes()));
			doc.someprop = "some value";
			bulkEntries.Add(doc);

			var response = this._client.Raw.Bulk(bulkEntries);
			var actual = response.Request.Utf8String();
			this.BulkJsonEquals(actual, MethodBase.GetCurrentMethod());

			response = rawClient.Bulk(bulkEntries);
			actual = response.Request.Utf8String();
			this.BulkJsonEquals(actual, MethodBase.GetCurrentMethod());


		}
	}
}
