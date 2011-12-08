using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using ElasticSearch.Client.DSL;

namespace Elasticsearch.DSL.Tests
{
	[TestFixture]
	public class SearchDescriptorTests
	{
		[Test]
		public void Test()
		{
			var s = new SearchDescriptor()
				.From(0)
				.Size(10);
		
		}

	}
}
