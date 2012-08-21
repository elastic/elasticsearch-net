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
	public class PathResolverTests
	{
		private static ConnectionSettings Settings = new ConnectionSettings(Test.Default.Uri)
			.SetDefaultIndex(Test.Default.DefaultIndex);

		[Test]
		public void SimpleGetPath()
		{
			var pr = new PathResolver(Settings);
			var d = new GetDescriptor<ElasticSearchProject>()
				.Id(1);
			var expected = "/nest_test_data/elasticsearchprojects/1";
			var path = pr.CreateGetPath(d);

			Assert.AreEqual(expected, path);
		}
		[Test]
		public void ComplexGetPath()
		{
			var pr = new PathResolver(Settings);
			var d = new GetDescriptor<ElasticSearchProject>()
				.Index("newindex")
				.Type("myothertype")
				.Refresh()
				.Routing("routing")
				.ExecuteOnPrimary()
				.Id(1);
			var expected = "/newindex/myothertype/1?refresh=true&preference=_primary&routing=routing";
			var path = pr.CreateGetPath(d);

			Assert.AreEqual(expected, path, path);
		}
	}
}
