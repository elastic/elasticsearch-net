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
using System.Linq.Expressions;
using Nest.Resolvers;

namespace Nest.Tests.Unit.Inferno
{
	[TestFixture]
	public class MapTypeIndicesTests
	{
		[Test]
		public void ResolvesToTypeIndex()
		{
			var clientSettings = new ConnectionSettings(Test.Default.Uri)
				.SetDefaultIndex("mydefaultindex")
				.MapTypeIndices(p =>
					p.Add(typeof(ElasticSearchProject), "mysuperindex")
			);
			var c = new PathResolver(clientSettings);
			var searchPath = c.GetSearchPathForTyped(new SearchDescriptor<ElasticSearchProject>());
			StringAssert.StartsWith("mysuperindex", searchPath);
			searchPath = c.GetSearchPathForTyped(new SearchDescriptor<GeoLocation>());
			StringAssert.StartsWith("mydefaultindex", searchPath);
		}
	}
}
