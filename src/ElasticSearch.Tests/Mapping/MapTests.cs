using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ElasticSearch.Client;
using Nest.TestData;
using Nest.TestData.Domain;
using NUnit.Framework;
using ElasticSearch.Client.Mapping;

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

		public void FluentMapping()
		{
			//TODO: Waiting to pull in nordbergm's excellent work on mapping 
			/*var map = Map<ElasticSearchProject>
				.Type(new ElasticType() 
				{
					
				}).AddField(p=>p.Content, Field.Analyzer("").)*/
		}
	}
}
