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
				.Index<ElasticsearchProject>(i => i.Object(new ElasticsearchProject {Id = 2}))
				.Delete<ElasticsearchProject>(i => i.Object(new ElasticsearchProject { Id = 4 }))
				.Create<ElasticsearchProject>(i => i.Object(new ElasticsearchProject { Id = 123123 }))
				
			);
			result.Should().NotBeNull();
			result.IsValid.Should().BeTrue();
			result.Errors.Should().BeFalse();

			result.Items.Should().NotBeNull().And.NotBeEmpty().And.HaveCount(3);
			var deleteResponses = result.Items.OfType<BulkDeleteResponseItem>();
			var createResponses = result.Items.OfType<BulkCreateResponseItem>();
			var indexResponses = result.Items.OfType<BulkIndexResponseItem>();

			deleteResponses.Should().HaveCount(1);
			deleteResponses.First().Id.Should().BeEquivalentTo("4");
			deleteResponses.First().Index.Should().BeEquivalentTo(ElasticsearchConfiguration.DefaultIndex);
			deleteResponses.First().Type.Should().BeEquivalentTo(this._client.Infer.TypeName<ElasticsearchProject>());

			createResponses.Should().HaveCount(1);
			createResponses.First().Id.Should().BeEquivalentTo("123123");
			createResponses.First().Index.Should().BeEquivalentTo(ElasticsearchConfiguration.DefaultIndex);
			createResponses.First().Type.Should().BeEquivalentTo(this._client.Infer.TypeName<ElasticsearchProject>());

			indexResponses.Should().HaveCount(1);
			indexResponses.First().Id.Should().BeEquivalentTo("2");
			indexResponses.First().Index.Should().BeEquivalentTo(ElasticsearchConfiguration.DefaultIndex);
			indexResponses.First().Type.Should().BeEquivalentTo(this._client.Infer.TypeName<ElasticsearchProject>());
		}

		[Test]
		public void BulkWithFixedIndex()
		{
			var indexName = ElasticsearchConfiguration.NewUniqueIndexName();
			var result = this._client.Bulk(b => b
				.FixedPath(indexName, "mytype")
				.Index<ElasticsearchProject>(i => i.Object(new ElasticsearchProject { Id = 2 }))
				.Create<ElasticsearchProject>(i => i.Object(new ElasticsearchProject { Id = 3 }))
				.Delete<ElasticsearchProject>(i => i.Object(new ElasticsearchProject { Id = 4 }))
			);
			result.Should().NotBeNull();
			result.IsValid.Should().BeTrue();
			result.Errors.Should().BeFalse();

			result.Items.Should().NotBeNull().And.NotBeEmpty().And.HaveCount(3);
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
				.Index<ElasticsearchProject>(i => i.Object(new ElasticsearchProject { Id = 2 }).Index(indexName2))
				.Create<ElasticsearchProject>(i => i.Object(new ElasticsearchProject { Id = 3 }).Type("esproj"))
				.Delete<ElasticsearchProject>(i => i.Id(4).Index(indexName3).Type("mytype2"))
			);
			result.Should().NotBeNull();
			result.IsValid.Should().BeTrue();
			result.Errors.Should().BeFalse();

			result.Items.Should().NotBeNull().And.NotBeEmpty().And.HaveCount(3);
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
				descriptor.Index<ElasticsearchProject>(op => op.Object(new ElasticsearchProject {Id = i}));

			var result = this._client.Bulk(d=>descriptor);
			result.Should().NotBeNull();
			result.IsValid.Should().BeTrue();
			result.Errors.Should().BeFalse();

			result.Items.Should().NotBeNull().And.NotBeEmpty().And.HaveCount(1000);
		
		}
	}
}
