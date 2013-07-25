using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using Nest;
using Newtonsoft.Json.Converters;
using Nest.Resolvers.Converters;
using Nest.Tests.MockData.Domain;

namespace Nest.Tests.Unit.Core.Index
{
	[TestFixture]
	public class IndexTests : BaseJsonTests
	{
		[Test]
		public void IndexParameters()
		{
			var o = new ElasticSearchProject { Id = 1, Name = "Test" };
			var result = this._client.Index(o, new IndexParameters 
			{ 
				Version = "1",

			});
			var status = result.ConnectionStatus;
			StringAssert.Contains("version=1", status.RequestUrl);
		}
	
		[Test]
		public void GetSupportsVersioning()
		{
			//TODO: investigate version on get
			//The elasticsearch docs make no mention of being able to specify version
			//http://www.elasticsearch.org/guide/reference/api/get.html

			//this._client.Get<ElasticSearchProject>(g=>g.);
		}
		[Test]
		public void UpdateSupportsVersioning()
		{
			//TODO: investigate version on update
			//The elasticsearch docs make no mention of being able to specify version
			//http://www.elasticsearch.org/guide/reference/api/get.html

			//this._client.Get<ElasticSearchProject>(g=>g.);
		}
	}
}
