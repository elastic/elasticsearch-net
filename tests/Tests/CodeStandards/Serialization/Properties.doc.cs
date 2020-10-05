// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System;
using Elastic.Elasticsearch.Xunit.XunitPlumbing;
using Elastic.Transport;
using Elastic.Transport.Extensions;
using Elasticsearch.Net.Extensions;
using FluentAssertions;
using Nest;
using Tests.Framework;

namespace Tests.CodeStandards.Serialization
{
	public class JsonProperties
	{
		/**
		* Our Utf8Json formatter resolver picks up attributes set on the interface
		*/
		[U]
		public void SeesInterfaceProperties()
		{
			var pool = new SingleNodeConnectionPool(new Uri("http://localhost:9200"));
			var settings = new ConnectionSettings(pool, new InMemoryConnection());
			var c = new ElasticClient(settings);


			var serializer = c.RequestResponseSerializer;
			var serialized = serializer.SerializeToString(new Nest.Analysis { CharFilters = new CharFilters() });
			serialized.Should().NotContain("char_filters").And.NotContain("charFilters");
			serialized.Should().Contain("char_filter");

			serialized = serializer.SerializeToString(new AnalysisDescriptor().CharFilters(cf=>cf));
			serialized.Should().NotContain("char_filters").And.NotContain("charFilters");
			serialized.Should().Contain("char_filter");
		}
	}
}
