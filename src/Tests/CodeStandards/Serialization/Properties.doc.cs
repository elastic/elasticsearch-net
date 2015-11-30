using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Elasticsearch.Net.Connection;
using Elasticsearch.Net.ConnectionPool;
using Elasticsearch.Net.Serialization;
using FluentAssertions;
using Nest;
using Tests.Framework;

namespace Tests.CodeStandards.Serialization
{
	public class JsonProperties
	{

		/**
		* Our Json.NET contract resolver picks up attributes set on the interface
		*/
		[U]
		public void SeesInterfaceProperties()
		{
			var pool = new SingleNodeConnectionPool(new Uri("http://localhost:9200"));
			var settings = new ConnectionSettings(pool, new InMemoryConnection());
			var c = new ElasticClient(settings);


			var serialized = c.Serializer.SerializeToString(new Nest.Analysis { CharFilters = new CharFilters() });
			serialized.Should().NotContain("char_filters").And.NotContain("charFilters");
			serialized.Should().Contain("char_filter");

			serialized = c.Serializer.SerializeToString(new AnalysisDescriptor().CharFilters(cf=>cf));
			serialized.Should().NotContain("char_filters").And.NotContain("charFilters");
			serialized.Should().Contain("char_filter");


		}
	}
}
