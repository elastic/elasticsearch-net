using FluentAssertions;
using Nest.Tests.MockData.Domain;
using NUnit.Framework;

namespace Nest.Tests.Unit.QueryParsers.Queries
{
	[TestFixture]
	public class FieldMaskingSpanQueryTests : ParseQueryTestsBase
	{
		[Test]
		public void FieldMaskingSpan_Deserializes()
		{
			var q = this.SerializeThenDeserialize(
				f => f.SpanFieldMasking,
				f => f.SpanFieldMasking(s => s.OnField(p => p.Name).Query(qq => qq.SpanTerm("x", "y")))
			);


			q.Field.Should().NotBeNull().And.Be("name");
			q.Query.Should().NotBeNull();
			q.Query.SpanTermQueryDescriptor.Should().NotBeNull();
		}
	}
}