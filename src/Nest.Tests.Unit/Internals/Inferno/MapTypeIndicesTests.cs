using NUnit.Framework;
using Nest.Tests.MockData.Domain;
using Nest.Resolvers;

namespace Nest.Tests.Unit.Internals.Inferno
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
