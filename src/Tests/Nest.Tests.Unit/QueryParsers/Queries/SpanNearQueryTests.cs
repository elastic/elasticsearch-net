using NUnit.Framework;

namespace Nest.Tests.Unit.QueryParsers.Queries
{
	[TestFixture]
	public class SpanNearQueryTests : ParseQueryTestsBase
	{
		[Test]
		public void SpanNear_Deserializes()
		{
			var q = this.SerializeThenDeserialize(
				f=>f.SpanNear,
				f=>f.SpanNear(sq=>sq
					.Clauses(
						c => c.SpanTerm(p => p.Name, "elasticsearch.pm", 1.1),
						c => c.SpanFirst(sf => sf
							.MatchTerm(p => p.Name, "elasticsearch.pm", 1.1)
							.End(3)
							)
					)
					.Slop(3)
					.CollectPayloads(false)
					.InOrder(false)	
					)
				);
		}
	}
}