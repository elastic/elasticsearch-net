using System;
using FluentAssertions;
using NUnit.Framework;

namespace Nest.Tests.Unit.QueryParsers.Queries
{
	[TestFixture]
	public class FuzzyQueryTests : ParseQueryTestsBase
	{

		[Test]
		public void Fuzzy_Deserializes()
		{
			var q = this.SerializeThenDeserialize(
				f=>f.Fuzzy,
				f=>f.Fuzzy(fq=>fq
					.Boost(2.2)
					.Fuzziness(2.1)
					.MaxExpansions(4)
					.OnField(p=>p.Name)
					.PrefixLength(3)
					.Rewrite(RewriteMultiTerm.ConstantScoreFilter)
					.Value("findme")
					)
				);

			q.Boost.Should().Be(2.2);
			q.Fuzziness.Should().Be("2.1");
			q.MaxExpansions.Should().Be(4);
			q.Field.Should().Be("name");
			q.Rewrite.Should().Be(RewriteMultiTerm.ConstantScoreFilter);

			var stringFuzzy = (IStringFuzzyQuery) q;
			stringFuzzy.PrefixLength.Should().Be(3);
			stringFuzzy.Value.Should().Be("findme");
		}

		[Test]
		public void FuzzyNumeric_Deserializes()
		{
			var q = this.SerializeThenDeserialize(
				f=>f.Fuzzy,
				f=>f.FuzzyNumeric(fq=>fq
					.Boost(2.2)
					.Fuzziness("AUTO")
					.MaxExpansions(4)
					.Transpositions()
					.OnField(p=>p.Name)
					.Value(2)
					)
				);

			q.Boost.Should().Be(2.2);
			q.Fuzziness.Should().Be("AUTO");
			q.MaxExpansions.Should().Be(4);
			q.Field.Should().Be("name");
			q.Transpositions.Should().BeTrue();

			var stringFuzzy = (IFuzzyNumericQuery) q;
			stringFuzzy.Value.Should().Be(2);
		}

		[Test]
		public void FuzzyDate_Deserializes()
		{
			var date = DateTime.UtcNow;
			var q = this.SerializeThenDeserialize(
				f=>f.Fuzzy,
				f=>f.FuzzyDate(fq=>fq
					.Boost(2.2)
					.Fuzziness(2.1)
					.MaxExpansions(4)
					.OnField(p=>p.Name)
					.Value(date)
					.Fuzziness("1d")
					.UnicodeAware()
					)
				);

			q.Boost.Should().Be(2.2);
			q.Fuzziness.Should().Be("1d");
			q.MaxExpansions.Should().Be(4);
			q.Field.Should().Be("name");
			q.UnicodeAware.Should().BeTrue();

			var stringFuzzy = (IFuzzyDateQuery) q;
			stringFuzzy.Value.Should().Be(date);
		}
	}
}