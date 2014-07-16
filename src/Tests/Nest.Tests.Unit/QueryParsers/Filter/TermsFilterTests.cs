using System.Linq;
using FluentAssertions;
using Nest.Tests.MockData.Domain;
using NUnit.Framework;

namespace Nest.Tests.Unit.QueryParsers.Filter
{
	[TestFixture]
	public class TermsFilterTests : ParseFilterTestsBase 
	{
		[Test]
		[TestCase("cacheName", "cacheKey", true)]
		public void TermsFilterLookup_Deserializes(string cacheName, string cacheKey, bool cache)
		{
			var termsBaseFilter = this.SerializeThenDeserialize(cacheName, cacheKey, cache, 
				f=>f.Terms,
				f=>f.TermsLookup(p => p.Name, l=>l
					.Lookup<ElasticsearchProject>(p=>p.NestedFollowers.First().FirstName, "1347", "idx", "tpe")
					)
				);
			termsBaseFilter.Field.Should().Be("name");
			var termsFilter = termsBaseFilter as ITermsLookupFilter;
			termsFilter.Should().NotBeNull();
			termsFilter.Id.Should().Be("1347");
			termsFilter.Index.Should().Be("idx");
			termsFilter.Type.Should().Be("tpe");
			termsFilter.Path.Should().Be("nestedFollowers.firstName");
		}
		
		[Test]
		[TestCase("cacheName", "cacheKey", true)]
		public void TermsFilter_Deserializes(string cacheName, string cacheKey, bool cache)
		{
			var termsBaseFilter = this.SerializeThenDeserialize(cacheName, cacheKey, cache, 
				f=>f.Terms,
				f=>f.Terms(p => p.Name, new [] {"elasticsearch.pm"}, Execution:TermsExecution.Bool)
				);
			termsBaseFilter.Field.Should().Be("name");
			var termsFilter = termsBaseFilter as ITermsFilter;
			termsFilter.Should().NotBeNull();
			termsFilter.Execution.Should().Be(TermsExecution.Bool);
			termsFilter.Terms.Should().BeEquivalentTo(new []{"elasticsearch.pm"});
		}
		
	}
}