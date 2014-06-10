using FluentAssertions;
using NUnit.Framework;

namespace Nest.Tests.Unit.QueryParsers.Filter
{
	[TestFixture]
	public class IdsFilterTests : ParseFilterTestsBase 
	{
		[Test]
		[TestCase("cacheName", "cacheKey", true)]
		public void Ids_Deserializes(string cacheName, string cacheKey, bool cache)
		{
			var idsFilter = this.SerializeThenDeserialize(cacheName, cacheKey, cache, 
				f=>f.Ids,
				f=>f.Ids(new []{"my_type", "my_other_type"}, new[] { "1", "4", "100" })
				);

			idsFilter.Values.Should().BeEquivalentTo(new[] { "1", "4", "100" });
			idsFilter.Type.Should().BeEquivalentTo(new[] { "my_type", "my_other_type" });
		}
		
	}
}