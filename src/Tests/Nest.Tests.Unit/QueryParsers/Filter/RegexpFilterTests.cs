using FluentAssertions;
using NUnit.Framework;

namespace Nest.Tests.Unit.QueryParsers.Filter
{
	[TestFixture]
	public class RegexpFilterTests : ParseFilterTestsBase 
	{
		[Test]
		[TestCase("cacheName", "cacheKey", true)]
		public void Regexp_Deserializes(string cacheName, string cacheKey, bool cache)
		{
			var regexpFilter = this.SerializeThenDeserialize(cacheName, cacheKey, cache, 
				f=>f.Regexp,
				f=>f.Regexp(r => r
					.OnField(p => p.Name)
					.Value("ab?")
					.Flags("INTERSECTION|COMPLEMENT|EMPTY")
					)
				);
			regexpFilter.Field.Should().Be("name");
			regexpFilter.Value.Should().Be("ab?");
			regexpFilter.Flags.Should().Be("INTERSECTION|COMPLEMENT|EMPTY");
		}
		
	}
}