using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace Nest.Tests.Unit.QueryParsers.Filter
{
	[TestFixture]
	public class ScriptFilterTests : ParseFilterTestsBase 
	{
		[Test]
		[TestCase("cacheName", "cacheKey", true)]
		public void Script_Deserializes(string cacheName, string cacheKey, bool cache)
		{
			var scriptFilter = this.SerializeThenDeserialize(cacheName, cacheKey, cache, 
				f=>f.Script,
				f=>f.Script(sc => sc
					.Script("doc['num1'].value > param1")
					.Params(p => p.Add("param1", 12))
					.Lang("mvel")
					)
				);
			scriptFilter.Script.Should().Be("doc['num1'].value > param1");
			scriptFilter.Params.Should().NotBeEmpty().And.HaveCount(1);
			var keyValuePair = scriptFilter.Params.First();
			keyValuePair.Key.Should().Be("param1");
		}
		
	}
}