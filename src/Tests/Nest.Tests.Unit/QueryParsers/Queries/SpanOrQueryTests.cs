using NUnit.Framework;

namespace Nest.Tests.Unit.QueryParsers.Queries
{
	[TestFixture]
	public class SpanOrQueryTests : ParseQueryTestsBase
	{
		[Test]
		public void SpanOr_Deserializes()
		{
			var q = this.SerializeThenDeserialize(
				f=>f.SpanOr,
				f=>f.SpanOr(sq=>sq
					.Clauses(
						c => c.SpanTerm(p => p.Name, "elasticsearch.pm", 1.1),
						c => c.SpanFirst(sf => sf
							.MatchTerm(p => p.Name, "elasticsearch.pm", 1.1)
							.End(3)
							)
					)
					)
				);
		}
	}
}