using FluentAssertions;
using NUnit.Framework;

namespace Nest.Tests.Unit.QueryParsers.Filter
{
	[TestFixture]
	public class TermFilterTests : ParseFilterTestsBase 
	{
		[Test]
		[TestCase("cacheName", "cacheKey", true, "myterm")]
		[TestCase("cacheName", "cacheKey", false, "myterm")]
		public void Term_Deserializes(string cacheName, string cacheKey, bool cache, string term)
		{
			var termFilter = this.SerializeThenDeserialize(cacheName, cacheKey, cache, 
				f=>f.Term,
				f=>f.Term(p=>p.Name, term)
				);
			termFilter.Field.Should().Be("name");
			termFilter.Value.Should().Be(term);
		}

	}
}