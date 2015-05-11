using System.Reflection;
using Nest.Tests.MockData.Domain;
using NUnit.Framework;

namespace Nest.Tests.Unit.Search.InnerHits
{
	[TestFixture]
	public class GlobalInnerHitsTests : BaseJsonTests
	{
		[Test]
		public void ComplexInnerHitsSerializes()
		{
			var s = new SearchDescriptor<ElasticsearchProject>()
				.From(0)
				.Size(10)
				.MatchAll()
				.InnerHits(inner => inner
					.Add("first", i => i
						.Path(p => p.Followers, followers => followers
							.Query(q => q.MatchAll())
							.InnerHits(firstHits => firstHits
								.Add("first_first", firstFirst => firstFirst
									.Type<Person>()
								)
							)
						)
					)
				);
			this.JsonEquals(s, MethodBase.GetCurrentMethod());
		}

	}
}
