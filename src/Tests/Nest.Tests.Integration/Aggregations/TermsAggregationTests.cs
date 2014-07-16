using System.Linq;
using Elasticsearch.Net;
using FluentAssertions;
using Nest.Tests.MockData.Domain;
using NUnit.Framework;

namespace Nest.Tests.Integration.Aggregations
{
	[TestFixture]
	public class TermsAggregationTests : IntegrationTests
	{
		[Test]
		public void WrongFieldName()
		{
			var results = this.Client.Search<ElasticsearchProject>(s => s
				.Size(0)
				.Aggregations(a => a
					.Terms("my_terms_agg", t => t
						.Field("this_field_name_does_not_exist")
					)
				)
			);
			results.IsValid.Should().BeTrue();
			var termBucket = results.Aggs.Terms("my_terms_agg");
			termBucket.Should().NotBeNull();
			termBucket.Items.Should().BeEmpty();
		}

		[Test]
		public void ExistingFieldName()
		{
			var results = this.Client.Search<ElasticsearchProject>(s => s
				.Size(0)
				.Aggregations(a => a
					.Terms("my_terms_agg", t => t
						.Field(p => p.Name)
					)
					
				)
			);
			results.IsValid.Should().BeTrue();
			var termBucket = results.Aggs.Terms("my_terms_agg");
			termBucket.Should().NotBeNull();
			termBucket.Items.Should().NotBeEmpty()
				.And.OnlyContain(i => !i.Key.IsNullOrEmpty())
				.And.OnlyContain(i => i.DocCount > 0);
		}

		[Test]
		public void NestedEmptyAggregationThreeLevelDeeps()
		{
			var results = this.Client.Search<ElasticsearchProject>(s => s
				.Size(0)
				.Aggregations(a => a
					.Terms("countries", t => t
						.Field(p => p.Country)
						.Aggregations(aa => aa
							.Terms("noop", tt => tt.Field("noop"))
							.Terms("names", tt => tt
								.Field(p => p.Name)
								.Aggregations(aaa => aaa
									.Average("avg_loc", avg => avg.Field(p => p.LOC))
								)
							)
						)
					)
				)
			);
			results.IsValid.Should().BeTrue();
			var termBucket = results.Aggs.Terms("countries");
			termBucket.Should().NotBeNull();
			termBucket.Items.Should().NotBeEmpty()
				.And.OnlyContain(i => !i.Key.IsNullOrEmpty())
				.And.OnlyContain(i => i.DocCount > 0);

			foreach (var term in termBucket.Items)
			{
				var country = term.Key;
				country.Should().NotBeNullOrWhiteSpace();

				var noop = term.Terms("noop");
				noop.Should().NotBeNull();
				noop.Items.Should().BeEmpty();

				var names = term.Terms("names");
				names.Should().NotBeNull();
				names.Items.Should().NotBeEmpty();

				foreach (var nameTerm in names.Items)
				{
					var name = nameTerm.Key;
					var average = nameTerm.Average("avg_loc");
					average.Should().NotBeNull();
					average.Value.Should().HaveValue();
				}
			}
		}
	}
}