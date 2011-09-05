using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ElasticSearch.Client;
using Nest.TestData;
using Nest.TestData.Domain;
using NUnit.Framework;

namespace ElasticSearch.Tests.Mapping
{
	[TestFixture]
	public class MapTests : BaseElasticSearchTests
    {
        [Test]
        public void SimpleMapByAttributes()
		{
			this.ConnectedClient.DeleteMapping<ElasticSearchProject>();
			this.ConnectedClient.DeleteMapping<ElasticSearchProject>(Test.Default.DefaultIndex + "_clone");
			var x = this.ConnectedClient.Map<ElasticSearchProject>();
		}
	}
}
