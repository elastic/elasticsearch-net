using NUnit.Framework;

namespace Nest.Tests.Unit.QueryParsers.Queries
{
	[TestFixture]
	public class SpanNotQueryTests : ParseQueryTestsBase
	{
		[Test]
		public void SpanNot_Deserializes()
		{
			var q = this.SerializeThenDeserialize(
				f=>f.SpanNot,
				f=>f.SpanNot(sn=>sn
					.Pre(1)
					.Post(2)
					.Dist(3)
					.Include(e =>e.SpanTerm(p => p.Name, "elasticsearch.pm", 1.1))
					.Exclude(e=>e.SpanTerm(p => p.Name, "elasticsearch.pm", 1.1))
					)
				);
		}
	}
}