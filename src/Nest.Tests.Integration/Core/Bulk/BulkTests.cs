using System;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using Nest.Tests.MockData.Domain;
using Nest.Resolvers;

namespace Nest.Tests.Integration.Core.Bulk
{
	[TestFixture]
	public class BulkTests : IntegrationTests
	{
		[Test]
		public void Bulk()
		{
			//Delete so we know the create does not throw an error.
			this._client.DeleteIndex(ElasticsearchConfiguration.DefaultIndex);
			var result = this._client.Bulk(b => b
				.Index<ElasticSearchProject>(i => i.Object(new ElasticSearchProject {Id = 2}))
				.Create<ElasticSearchProject>(i => i.Object(new ElasticSearchProject { Id = 3 }))
				.Delete<ElasticSearchProject>(i => i.Object(new ElasticSearchProject { Id = 4 }))
			);
			result.Should().NotBeNull();
			result.IsValid.Should().BeTrue();

			result.Items.Should().NotBeNull().And.NotBeEmpty().And.HaveCount(3).And.OnlyContain(r => r.OK);
			var deleteResponses = result.Items.OfType<BulkDeleteResponseItem>();
			var createResponses = result.Items.OfType<BulkCreateResponseItem>();
			var indexResponses = result.Items.OfType<BulkIndexResponseItem>();

			deleteResponses.Should().HaveCount(1);
			deleteResponses.First().Id.Should().BeEquivalentTo("4");
			deleteResponses.First().Index.Should().BeEquivalentTo(ElasticsearchConfiguration.DefaultIndex);
			deleteResponses.First().Type.Should().BeEquivalentTo(this.GetTypeNameFor<ElasticSearchProject>());

			createResponses.Should().HaveCount(1);
			createResponses.First().Id.Should().BeEquivalentTo("3");
			createResponses.First().Index.Should().BeEquivalentTo(ElasticsearchConfiguration.DefaultIndex);
			createResponses.First().Type.Should().BeEquivalentTo(this.GetTypeNameFor<ElasticSearchProject>());

			indexResponses.Should().HaveCount(1);
			indexResponses.First().Id.Should().BeEquivalentTo("2");
			indexResponses.First().Index.Should().BeEquivalentTo(ElasticsearchConfiguration.DefaultIndex);
			indexResponses.First().Type.Should().BeEquivalentTo(this.GetTypeNameFor<ElasticSearchProject>());
		}

		[Test]
		public void BulkWithFixedIndex()
		{
			var indexName = ElasticsearchConfiguration.NewUniqueIndexName();
			var result = this._client.Bulk(b => b
				.FixedPath(indexName, "mytype")
				.Index<ElasticSearchProject>(i => i.Object(new ElasticSearchProject { Id = 2 }))
				.Create<ElasticSearchProject>(i => i.Object(new ElasticSearchProject { Id = 3 }))
				.Delete<ElasticSearchProject>(i => i.Object(new ElasticSearchProject { Id = 4 }))
			);
			result.Should().NotBeNull();
			result.IsValid.Should().BeTrue();

			result.Items.Should().NotBeNull().And.NotBeEmpty().And.HaveCount(3).And.OnlyContain(r => r.OK);
			var deleteResponses = result.Items.OfType<BulkDeleteResponseItem>();
			var createResponses = result.Items.OfType<BulkCreateResponseItem>();
			var indexResponses = result.Items.OfType<BulkIndexResponseItem>();

			deleteResponses.Should().HaveCount(1);
			deleteResponses.First().Id.Should().BeEquivalentTo("4");
			deleteResponses.First().Index.Should().BeEquivalentTo(indexName);
			deleteResponses.First().Type.Should().BeEquivalentTo("mytype");

			createResponses.Should().HaveCount(1);
			createResponses.First().Id.Should().BeEquivalentTo("3");
			createResponses.First().Index.Should().BeEquivalentTo(indexName);
			createResponses.First().Type.Should().BeEquivalentTo("mytype");

			indexResponses.Should().HaveCount(1);
			indexResponses.First().Id.Should().BeEquivalentTo("2");
			indexResponses.First().Index.Should().BeEquivalentTo(indexName);
			indexResponses.First().Type.Should().BeEquivalentTo("mytype");
		}
		[Test]
		public void BulkWithFixedIndexOveriddenIndividualy()
		{
			var indexName = ElasticsearchConfiguration.NewUniqueIndexName();
			var indexName2 = ElasticsearchConfiguration.NewUniqueIndexName();
			var indexName3 = ElasticsearchConfiguration.NewUniqueIndexName();
			var result = this._client.Bulk(b => b
				.FixedPath(indexName, "mytype")
				.Index<ElasticSearchProject>(i => i.Object(new ElasticSearchProject { Id = 2 }).Index(indexName2))
				.Create<ElasticSearchProject>(i => i.Object(new ElasticSearchProject { Id = 3 }).Type("esproj"))
				.Delete<ElasticSearchProject>(i => i.Id(4).Index(indexName3).Type("mytype2"))
			);
			result.Should().NotBeNull();
			result.IsValid.Should().BeTrue();

			result.Items.Should().NotBeNull().And.NotBeEmpty().And.HaveCount(3).And.OnlyContain(r=>r.OK);
			var deleteResponses = result.Items.OfType<BulkDeleteResponseItem>();
			var createResponses = result.Items.OfType<BulkCreateResponseItem>();
			var indexResponses = result.Items.OfType<BulkIndexResponseItem>();

			deleteResponses.Should().HaveCount(1);
			deleteResponses.First().Id.Should().BeEquivalentTo("4");
			deleteResponses.First().Index.Should().BeEquivalentTo(indexName3);
			deleteResponses.First().Type.Should().BeEquivalentTo("mytype2");

			createResponses.Should().HaveCount(1);
			createResponses.First().Id.Should().BeEquivalentTo("3");
			createResponses.First().Index.Should().BeEquivalentTo(indexName);
			createResponses.First().Type.Should().BeEquivalentTo("esproj");

			indexResponses.Should().HaveCount(1);
			indexResponses.First().Id.Should().BeEquivalentTo("2");
			indexResponses.First().Index.Should().BeEquivalentTo(indexName2);
			indexResponses.First().Type.Should().BeEquivalentTo("mytype");
		}

		[Test]
		public void BulkAlternativeWayOfWriting()
		{
			this._client.DeleteIndex(ElasticsearchConfiguration.DefaultIndex);

			var descriptor = new BulkDescriptor();
			foreach (var i in Enumerable.Range(0, 1000))
				descriptor.Index<ElasticSearchProject>(op => op.Object(new ElasticSearchProject {Id = i}));

			var result = this._client.Bulk(descriptor);
			result.Should().NotBeNull();
			result.IsValid.Should().BeTrue();

			result.Items.Should().NotBeNull().And.NotBeEmpty().And.HaveCount(1000).And.OnlyContain(r => r.OK);
		
		}

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
            var indexResponses = result.Items.OfType<BulkIndexResponseItem>();

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
