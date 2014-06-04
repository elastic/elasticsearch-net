using FluentAssertions;
using NUnit.Framework;

namespace Nest.Tests.Unit.QueryParsers.Filter
{
	[TestFixture]
	public class TypeFilterTests : ParseFilterTestsBase 
	{
		[Test]
		[TestCase("cacheName", "cacheKey", true)]
		public void TypeFilter_Deserializes(string cacheName, string cacheKey, bool cache)
		{
			var typeFilter = this.SerializeThenDeserialize(cacheName, cacheKey, cache, 
				f=>f.Type,
				f=>f.Type("my-type")
				);
			typeFilter.Value.Should().Be("my-type");
		}
		
	}
}