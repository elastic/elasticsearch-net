using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using Nest.Tests.MockData.Domain;

namespace Nest.Tests.Integration.Core.Bulk
{
	public class BulkPercolateTests : BulkTests
	{
		[Test]
		public void BulkIndexWithPercolate()
		{
			// register up some percolator queries to test matching
			var query1 = "bulkindex-test-doc-1";

			this._client.UnregisterPercolator<ElasticSearchProject>(query1);

			var perc = this._client.RegisterPercolator<ElasticSearchProject>(p => p
				.Name(query1)
				.Query(q => q
					.Term(f => f.Country, "netherlands")
				)
				);

			var descriptor = new BulkDescriptor();

			// match against any doc
			descriptor.Index<ElasticSearchProject>(i => i
				.Object(new ElasticSearchProject { Id = 2, Country = "netherlands" })
				.Percolate("*") // match on any percolated docs
				);

			// no percolate requested this time
			descriptor.Index<ElasticSearchProject>(i => i
				.Object(new ElasticSearchProject { Id = 3, Country = "netherlands" })
				);

			var result = this._client.Bulk(descriptor);

			result.Should().NotBeNull();
			result.IsValid.Should().BeTrue();

			result.Items.Should().NotBeNull().And.NotBeEmpty().And.HaveCount(2).And.OnlyContain(r => r.OK);
			var indexResponses = result.Items.OfType<BulkIndexResponseItem>().ToList();

			// tests on percolated responses
			indexResponses.Should().HaveCount(2);

			indexResponses.First().Id.Should().BeEquivalentTo("2");
			indexResponses.First().Index.Should().BeEquivalentTo(ElasticsearchConfiguration.DefaultIndex);
			indexResponses.First().Type.Should().BeEquivalentTo(this.GetTypeNameFor<ElasticSearchProject>());
			indexResponses.First().Matches.Should().NotBeNull();

			indexResponses.ElementAt(1).Id.Should().BeEquivalentTo("3");
			indexResponses.ElementAt(1).Index.Should().BeEquivalentTo(ElasticsearchConfiguration.DefaultIndex);
			indexResponses.ElementAt(1).Type.Should().BeEquivalentTo(this.GetTypeNameFor<ElasticSearchProject>());
			indexResponses.First().Matches.Should().BeNull();

			// cleanup
			this._client.UnregisterPercolator<ElasticSearchProject>(query1);
		}
	}
}