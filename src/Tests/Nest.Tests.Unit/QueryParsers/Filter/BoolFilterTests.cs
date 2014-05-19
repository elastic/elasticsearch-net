using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace Nest.Tests.Unit.QueryParsers.Filter
{
	[TestFixture]
	public class BoolFilterTests : ParseFilterTestsBase 
	{
		[Test]
		[TestCase("cacheName", "cacheKey", true)]
		public void Bool_Deserializes(string cacheName, string cacheKey, bool cache)
		{
			var boolFilter = this.SerializeThenDeserialize(cacheName, cacheKey, cache, 
				f=>f.Bool,
				f=>f.Bool(b=>b.Must(Filter1).MustNot(Filter2).Should(Filter3))
				);
			boolFilter.Must.Should().NotBeEmpty().And.HaveCount(1);
			boolFilter.MustNot.Should().NotBeEmpty().And.HaveCount(1);
			boolFilter.Should.Should().NotBeEmpty().And.HaveCount(1);

			AssertIsTermFilter(this.Filter1, boolFilter.Must.First().Term);
			AssertIsTermFilter(this.Filter2, boolFilter.MustNot.First().Term);
			AssertIsTermFilter(this.Filter3, boolFilter.Should.First().Term);
		}
		
	}
}