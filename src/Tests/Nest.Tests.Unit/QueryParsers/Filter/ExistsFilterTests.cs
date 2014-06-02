using FluentAssertions;
using NUnit.Framework;

namespace Nest.Tests.Unit.QueryParsers.Filter
{
	[TestFixture]
	public class ExistsFilterTests : ParseFilterTestsBase 
	{
		[Test]
		[TestCase("cacheName", "cacheKey", true)]
		public void Exists_Deserializes(string cacheName, string cacheKey, bool cache)
		{
			var existsFilter = this.SerializeThenDeserialize(cacheName, cacheKey, cache, 
				f=>f.Exists,
				f=>f.Exists(p=>p.Name)
				);

			existsFilter.Field.Should().Be("name");
		}

	}
}