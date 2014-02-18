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

			this._client.UnregisterPercolator(query1, ur=>ur.Index<ElasticsearchProject>());

			var perc = this._client.RegisterPercolator<ElasticsearchProject>(query1, p => p
				.Query(q => q
					.Term(f => f.Country, "netherlands")
				)
			);
			this._client.Refresh(r=>r.Index<ElasticsearchProject>());
			var descriptor = new BulkDescriptor();

			// match against any doc
			descriptor.Index<ElasticsearchProject>(i => i
				.Object(new ElasticsearchProject { Id = 2, Country = "netherlands" })
				.Percolate("*") // match on any percolated docs
				);

			// no percolate requested this time
			descriptor.Index<ElasticsearchProject>(i => i
				.Object(new ElasticsearchProject { Id = 3, Country = "netherlands" })
				);

			var result = this._client.Bulk(d=>descriptor);

			result.Should().NotBeNull();
			result.IsValid.Should().BeTrue();
			result.Errors.Should().BeFalse();

			result.Items.Should().NotBeNull().And.NotBeEmpty().And.HaveCount(2);
			var indexResponses = result.Items.OfType<BulkIndexResponseItem>().ToList();

			// tests on percolated responses
			indexResponses.Should().HaveCount(2);

			indexResponses.First().Id.Should().BeEquivalentTo("2");
			indexResponses.First().Index.Should().BeEquivalentTo(ElasticsearchConfiguration.DefaultIndex);
			indexResponses.First().Type.Should().BeEquivalentTo(this._client.Infer.TypeName<ElasticsearchProject>());
			indexResponses.First().Matches.Should().NotBeNull();

			indexResponses.ElementAt(1).Id.Should().BeEquivalentTo("3");
			indexResponses.ElementAt(1).Index.Should().BeEquivalentTo(ElasticsearchConfiguration.DefaultIndex);
			indexResponses.ElementAt(1).Type.Should().BeEquivalentTo(this._client.Infer.TypeName<ElasticsearchProject>());
			indexResponses.ElementAt(1).Matches.Should().BeNull();

			// cleanup
			this._client.UnregisterPercolator(query1, ur=>ur.Index<ElasticsearchProject>());
		}
	}
}