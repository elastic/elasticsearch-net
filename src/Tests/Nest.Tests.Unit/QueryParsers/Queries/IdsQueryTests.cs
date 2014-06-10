using FluentAssertions;
using NUnit.Framework;

namespace Nest.Tests.Unit.QueryParsers.Queries
{
	[TestFixture]
	public class IdsQueryTests : ParseQueryTestsBase
	{

		[Test]
		public void Ids_Deserializes()
		{
			var types = new [] {"type1, type2"};
			var values = new []{ "value" };
			var q = this.SerializeThenDeserialize(
				f=>f.Ids,
				f=>f.Ids(types, values)
				);

			q.Type.Should().BeEquivalentTo(types);
			q.Values.ShouldAllBeEquivalentTo(values);
		}

	}
}