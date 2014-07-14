using System;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using Nest.Tests.MockData.Domain;

namespace Nest.Tests.Integration.Core.Bulk
{
	[TestFixture]
	public class BulkTests : IntegrationTests
	{

		[Test]
		public void Bulk()
		{
			var result = this.Client.Bulk(b => b
				.Index<ElasticsearchProject>(i => i.Document(new ElasticsearchProject {Id = 2}))
				.Delete<ElasticsearchProject>(i => i.Document(new ElasticsearchProject { Id = 4 }))
				.Create<ElasticsearchProject>(i => i.Document(new ElasticsearchProject { Id = 123123 }))
				
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
			deleteResponses.First().Type.Should().BeEquivalentTo(this.Client.Infer.TypeName<ElasticsearchProject>());

			createResponses.Should().HaveCount(1);
			createResponses.First().Id.Should().BeEquivalentTo("123123");
			createResponses.First().Index.Should().BeEquivalentTo(ElasticsearchConfiguration.DefaultIndex);
			createResponses.First().Type.Should().BeEquivalentTo(this.Client.Infer.TypeName<ElasticsearchProject>());

			indexResponses.Should().HaveCount(1);
			indexResponses.First().Id.Should().BeEquivalentTo("2");
			indexResponses.First().Index.Should().BeEquivalentTo(ElasticsearchConfiguration.DefaultIndex);
			indexResponses.First().Type.Should().BeEquivalentTo(this.Client.Infer.TypeName<ElasticsearchProject>());
		}
		[Test]
		public void DoubleCreateReturnsOneError()
		{
			var result = this.Client.Bulk(b => b
				.Create<ElasticsearchProject>(i => i.Document(new ElasticsearchProject { Id = 12315555 }))
				.Create<ElasticsearchProject>(i => i.Document(new ElasticsearchProject { Id = 12315555 }))
			);

			result.IsValid.Should().BeFalse();
			result.Errors.Should().BeTrue();
			result.ItemsWithErrors.Should().NotBeEmpty().And.HaveCount(1);

		}
		[Test]
		public void BulkWithFixedIndex()
		{
			var indexName = ElasticsearchConfiguration.NewUniqueIndexName();
			var result = this.Client.Bulk(b => b
				.FixedPath(indexName, "mytype")
				.Index<ElasticsearchProject>(i => i.Document(new ElasticsearchProject { Id = 2 }))
				.Create<ElasticsearchProject>(i => i.Document(new ElasticsearchProject { Id = 3 }))
				.Delete<ElasticsearchProject>(i => i.Document(new ElasticsearchProject { Id = 4 }))
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
			var result = this.Client.Bulk(b => b
				.FixedPath(indexName, "mytype")
				.Index<ElasticsearchProject>(i => i.Document(new ElasticsearchProject { Id = 2 }).Index(indexName2))
				.Create<ElasticsearchProject>(i => i.Document(new ElasticsearchProject { Id = 3 }).Type("esproj"))
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
				descriptor.Index<ElasticsearchProject>(op => op.Document(new ElasticsearchProject {Id = i}));

			var result = this.Client.Bulk(d=>descriptor);
			result.Should().NotBeNull();
			result.IsValid.Should().BeTrue();
			result.Errors.Should().BeFalse();

			result.Items.Should().NotBeNull().And.NotBeEmpty().And.HaveCount(1000);
		
		}
	}
}
