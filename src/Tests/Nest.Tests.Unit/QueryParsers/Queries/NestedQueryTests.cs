using FluentAssertions;
using NUnit.Framework;

namespace Nest.Tests.Unit.QueryParsers.Queries
{
	[TestFixture]
	public class NestedQueryTests : ParseQueryTestsBase
	{
		[Test]
		public void Nested_Deserializes()
		{
			var q = this.SerializeThenDeserialize(
				f=>f.Nested,
				f=>f.Nested(nq=>nq
					.Path(p=>p.NestedFollowers)
					.Query(qq=>Query1)
					.Score(NestedScore.Max)
					)
				);
			q.Score.Should().Be(NestedScore.Max);
			q.Path.Should().Be("nestedFollowers");
			AssertIsTermQuery(q.Query, Query1);
		}

		[Test]
		public void Nested_WithInnerHits_Deserializes()
		{
			var q = this.SerializeThenDeserialize(
				f=>f.Nested,
				f=>f.Nested(nq=>nq
					.Path(p=>p.NestedFollowers)
					.Query(qq=>Query1)
					.Score(NestedScore.Max)
					.InnerHits(inner=>inner
						.Explain()
						.FielddataFields("x", "y")
						.From(12)
						.Size(13)
						.Source(false)
						.Version()
						.Name("xasda")
						.Highlight(h=>h
							.OnFields(hh=>hh
								.OnField(p=>p.Name)
							)
						)
						.ScriptFields(sf=>sf
							.Add("ax", s=>s.Script("'x'"))
						)
					)
				)
			);

			q.InnerHits.Should().NotBeNull();
			q.InnerHits.Explain.Should().BeTrue();
			q.InnerHits.Explain.Should().BeTrue();
			q.InnerHits.From.Should().Be(12);
			q.InnerHits.Size.Should().Be(13);
			q.InnerHits.Source.Should().NotBeNull();
			q.InnerHits.Source.Exclude.Should().NotBeEmpty().And.Contain("*");
			q.InnerHits.FielddataFields.Should().NotBeEmpty().And.HaveCount(2);
			q.InnerHits.Name.Should().Be("xasda");
			q.InnerHits.Highlight.Should().NotBeNull();
			q.InnerHits.ScriptFields.Should().NotBeNull().And.HaveCount(1);

		}

	}
}