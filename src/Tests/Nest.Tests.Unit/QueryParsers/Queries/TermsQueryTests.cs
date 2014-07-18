using FluentAssertions;
using Nest.Tests.MockData.Domain;
using NUnit.Framework;

namespace Nest.Tests.Unit.QueryParsers.Queries
{
	[TestFixture]
	public class TermsQueryTests : ParseQueryTestsBase
	{
		[Test]
		public void Terms_Deserializes()
		{
			var q = this.SerializeThenDeserialize(
				f=>f.Terms,
				f=>f.Terms(p=>p.Name, new [] {"term1", "term2" })
				);
			q.Terms.Should().NotBeEmpty().And.HaveCount(2);
			q.Field.Should().Be("name");
		}
		
		[Test]
		public void TermsDescriptor_Deserializes()
		{
			var q = this.SerializeThenDeserialize(
				f=>f.Terms,
				f=>f.TermsDescriptor(td=>td
					.OnField(p=>p.Name)
					.Boost(1.2)
					.MinimumShouldMatch(2)	
					.DisableCoord()
					.OnExternalField<ElasticsearchProject>(ef=>ef
						.Id(1)
						.Path(p=>p.Id)
						.Index("index")
						.Type("type")
					)
					)
				);
			q.Boost.Should().Be(1.2);
			q.Field.Should().Be("name");
			q.DisableCoord.Should().BeTrue();
			q.MinimumShouldMatch.Should().Be("2");
			q.ExternalField.Should().NotBeNull();
			q.ExternalField.Id.Should().Be("1");
			q.ExternalField.Index.Should().Be("index");
			q.ExternalField.Type.Should().Be("type");
			q.ExternalField.Path.Should().Be("id");
			
		}
	}
}