using Elasticsearch.Net;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Nest.Tests.Unit.Reproduce
{
	[TestFixture]
	public class Reproduce1464Tests : BaseJsonTests
	{
		[Test]
		public void Issue1464()
		{
			var bulkEntries = new List<object>();
			bulkEntries.Add(new { index = new { _index = "ourIndex", _type = "ourType", _id = "ourId" } });

			var doc = JsonConvert.DeserializeObject<dynamic>("{ \"id\": \"3\"}");
			doc.someprop = "some value";
			bulkEntries.Add(doc);

			var response = this._client.Raw.Bulk(bulkEntries);
			var actual = response.Request.Utf8String();

			this.BulkJsonEquals(actual, MethodBase.GetCurrentMethod());
		}
	}
}
