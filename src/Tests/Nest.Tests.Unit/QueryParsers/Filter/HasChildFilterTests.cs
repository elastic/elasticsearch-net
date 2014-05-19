using FluentAssertions;
using Nest.Tests.MockData.Domain;
using NUnit.Framework;

namespace Nest.Tests.Unit.QueryParsers.Filter
{
	[TestFixture]
	public class HasChildFilterTests : ParseFilterTestsBase 
	{
		[Test]
		[TestCase("cacheName", "cacheKey", true)]
		public void HasChild_Deserializes(string cacheName, string cacheKey, bool cache)
		{
			var hasChildFilter = this.SerializeThenDeserialize(cacheName, cacheKey, cache, 
				f=>f.HasChild,
				f=>f.HasChild<Person>(d=>d
					.Scope("my_scope")
					.Query(q=>q.Term(p=>p.FirstName, "value"))
					)
				);

			hasChildFilter._Scope.Should().Be("my_scope");
			var query = hasChildFilter._QueryDescriptor;
			query.Should().NotBeNull();
			query.Term.Field.Should().Be("firstName");
		}
		
	}
}