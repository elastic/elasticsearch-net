using System.Collections.Generic;
using FluentAssertions;
using Nest.Tests.MockData.Domain;
using NUnit.Framework;

namespace Nest.Tests.Unit.Converters
{
	[TestFixture]
	public class FuzzinessConverterTests : BaseConverterTest
	{
		[Test]
		public void ItCanConvertMatchFuzziness()
		{
			var expected = new QueryDescriptor<ElasticsearchProject>().Match(m => m
					.OnField("myfield")
					.Query("myquery")
					.Fuzziness());

			var actual = SerializeAndDeserialize<IQueryContainer>(expected);

			actual.Match.Fuzziness.EditDistance.Should().Be(null);
			actual.Match.Fuzziness.Ratio.Should().Be(null);
			actual.Match.Fuzziness.Auto.Should().BeTrue();
		}

		[Test]
		public void ItCanConvertMatchFuzzinessEditDistance()
		{
			var expected = new QueryDescriptor<ElasticsearchProject>().Match(m => m
					.OnField("myfield")
					.Query("myquery")
					.Fuzziness(2));

			var actual = SerializeAndDeserialize<IQueryContainer>(expected);

			actual.Match.Fuzziness.EditDistance.Should().Be(2);
			actual.Match.Fuzziness.Ratio.Should().Be(null);
			actual.Match.Fuzziness.Auto.Should().BeFalse();
		}

		[Test]
		public void ItCanConvertMatchFuzzinessRatio()
		{
			var expected = new QueryDescriptor<ElasticsearchProject>().Match(m => m
					.OnField("myfield")
					.Query("myquery")
					.Fuzziness(1.3));

			var actual = SerializeAndDeserialize<IQueryContainer>(expected);

			actual.Match.Fuzziness.EditDistance.Should().Be(null);
			actual.Match.Fuzziness.Ratio.Should().Be(1.3);
			actual.Match.Fuzziness.Auto.Should().BeFalse();
		}

		[Test]
		public void ItCanConvertFuzziness()
		{
			var expectedObjects = new List<ConverterTestObject>
			{
				new ConverterTestObject
				{
					Name = "FuzzinessTest",
					Fuzziness = Fuzziness.Auto
				},
				new ConverterTestObject
				{
					Name = "FuzzinessTest",
					Fuzziness = Fuzziness.EditDistance(2)
				},
				new ConverterTestObject
				{
					Name = "FuzzinessTest",
					Fuzziness = Fuzziness.Ratio(23.21123)
				}
			};

			var actual = SerializeAndDeserialize(expectedObjects);

			actual[0].Fuzziness.Auto.Should().BeTrue();
			actual[1].Fuzziness.EditDistance.Should().Be(2);
			actual[2].Fuzziness.Ratio.Should().Be(23.21123);
		}
	}
}