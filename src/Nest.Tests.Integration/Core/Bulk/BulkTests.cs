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
		private readonly BulkPercolateTests _bulkPercolateTests;


		[Test]
		public void Bulk()
		{
			var result = this._client.Bulk(b => b
				.Index<ElasticSearchProject>(i => i.Object(new ElasticSearchProject {Id = 2}))
				.Create<ElasticSearchProject>(i => i.Object(new ElasticSearchProject { Id = 123123 }))
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
			createResponses.First().Id.Should().BeEquivalentTo("123123");
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
			var descriptor = new BulkDescriptor();
			foreach (var i in Enumerable.Range(3000, 1000))
				descriptor.Index<ElasticSearchProject>(op => op.Object(new ElasticSearchProject {Id = i}));

			var result = this._client.Bulk(descriptor);
			result.Should().NotBeNull();
			result.IsValid.Should().BeTrue();

			result.Items.Should().NotBeNull().And.NotBeEmpty().And.HaveCount(1000).And.OnlyContain(r => r.OK);
		
		}
	}
}
