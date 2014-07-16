using System;
using System.Collections.Generic;
using System.Linq;
using Elasticsearch.Net;
using FluentAssertions;
using Nest.Tests.MockData;
using Nest.Tests.MockData.Domain;
using NUnit.Framework;

namespace Nest.Tests.Integration.Core
{
	[TestFixture]
	public class DeleteTests : IntegrationTests
	{
		protected override void ResetIndexes()
		{
			IntegrationSetup.TearDown();
			IntegrationSetup.Setup();
		}
		
		
		[Test]
		public void ShouldNotThrowOnIdOverload()
		{
			Assert.Throws<ArgumentNullException>(() =>
			{
				this.Client.Delete("id", d=>d.Index("someindex").Type("sometype"));
			});
		}

		[Test]
		public void ShouldThowOnNullId()
		{
			Assert.Throws<ArgumentNullException>(() =>
			{
				this.Client.Delete<object>(d=>d.Index("someindex").Type("sometype").Id(null));
			});
		}
		
		[Test]
		public void GetDocumentById()
		{
			//arrange
			//pull existing example through method we know is functional based on other passing unit tests
			var queryResults = this.SearchRaw<ElasticsearchProject>(
				@" { ""query"" : {
						 ""fuzzy"" : {
							""followers.firstName"" : """ + NestTestData.Data.First().Followers.First().FirstName.ToLowerInvariant() + @"x""
						}
					} }"
			);
			Assert.Greater(queryResults.Total, 0);

			var hit = queryResults.HitsMetaData.Hits.First();
			var documentToFind = hit.Source;

			//act
			//attempt to grab the same document using the document's id
			var foundDocument = this.Client.Source<ElasticsearchProject>(hit.Id);

			//assert
			//make sure that these are in fact the same documents
			Assert.AreEqual(documentToFind.Country, foundDocument.Country);
			Assert.AreEqual(documentToFind.Followers.Count, foundDocument.Followers.Count);
			Assert.AreEqual(documentToFind.Id, foundDocument.Id);
			Assert.AreEqual(documentToFind.Name, foundDocument.Name);
		}

		[Test]
		public void IndexThanDeleteDocumentById()
		{
			//arrange
			//create a new document to index
			ElasticsearchProject newDocument = new ElasticsearchProject
			{
				Country = "Mozambique",
				Followers = new List<Person>(),
				Id = DateTime.Now.Millisecond + 1500, //try to get this example out of the way of existing test data
				Name = "Test Document for 'IndexDocument' test"
			};

			//act
			//index the new item
			this.Client.Index<ElasticsearchProject>(newDocument, i=>i.Refresh());

			//assert
			//grab document back from the index and make sure it is the same document
			var foundDocument = this.Client.Source<ElasticsearchProject>(newDocument.Id);

			//Assert.Equal(newDocument.Country, foundDocument.Country);
			Assert.AreEqual(newDocument.Followers.Count, foundDocument.Followers.Count);
			Assert.AreEqual(newDocument.Id, foundDocument.Id);
			Assert.AreEqual(newDocument.Name, foundDocument.Name);

			//act
			//now remove the item that was added
			var response = this.Client.Delete<ElasticsearchProject>(f=>f.Id(newDocument.Id).Refresh());

			//assert
			//make sure getting by id returns nothing
			foundDocument = this.Client.Source<ElasticsearchProject>(newDocument.Id);
			Assert.Null(foundDocument);
		}
		[Test]
		public void IndexThanDeleteDocumentByObject()
		{
			//arrange
			//create a new document to index
			ElasticsearchProject newDocument = new ElasticsearchProject
			{
				Country = "Mozambique",
				Followers = new List<Person>(),
				Id = DateTime.Now.Millisecond + 1500, //try to get this example out of the way of existing test data
				Name = "Test Document for 'IndexDocument' test"
			};

			//act
			//index the new item
			this.Client.Index(newDocument, i=>i.Refresh());

			//assert
			//grab document back from the index and make sure it is the same document
			var foundDocument = this.Client.Source<ElasticsearchProject>(newDocument.Id);

			//Assert.Equal(newDocument.Country, foundDocument.Country);
			Assert.AreEqual(newDocument.Followers.Count, foundDocument.Followers.Count);
			Assert.AreEqual(newDocument.Id, foundDocument.Id);
			Assert.AreEqual(newDocument.Name, foundDocument.Name);

			//act
			//now remove the item that was added
			this.Client.Delete<ElasticsearchProject>(f=>f.Id(newDocument).Refresh());

			//assert
			//make sure getting by id returns nothing
			foundDocument = this.Client.Source<ElasticsearchProject>(newDocument.Id);
			Assert.Null(foundDocument);
		}

		[Test]
		public void IndexThenDeleteUsingRefresh()
		{
			//arrange
			//create a new document to index
			ElasticsearchProject newDocument = new ElasticsearchProject
			{
				Country = "Mozambique",
				Followers = new List<Person>(),
				Id = DateTime.Now.Millisecond + 1500, //try to get this example out of the way of existing test data
				Name = "Test Document for 'IndexDocument' test"
			};

			//act
			//index the new item
			this.Client.Index(newDocument, i=>i.Refresh());

			//assert
			//grab document back from the index and make sure it is the same document
			var foundDocument = this.Client.Source<ElasticsearchProject>(newDocument.Id);

			//Assert.Equal(newDocument.Country, foundDocument.Country);
			Assert.AreEqual(newDocument.Followers.Count, foundDocument.Followers.Count);
			Assert.AreEqual(newDocument.Id, foundDocument.Id);
			Assert.AreEqual(newDocument.Name, foundDocument.Name);

			//act
			//now remove the item that was added
			this.Client.Delete<ElasticsearchProject>(d=>d.Id(newDocument).Refresh());

			//assert
			//make sure getting by id returns nothing
			foundDocument = this.Client.Source<ElasticsearchProject>(newDocument.Id);
			Assert.Null(foundDocument);
		}
		[Test]
		public void RemoveAllByPassingAsIEnumerable()
		{
			this.ResetIndexes();
			var result = this.Client.Search<ElasticsearchProject>(q => q.From(0).Take(5).MatchAll());
			Assert.IsNotEmpty(result.Documents);

			var totalSet = result.Documents.Count();
			var totalResults = result.Total;
			Assert.Greater(totalSet, 0);

			var deleteResult = this.Client.Bulk(b => b.DeleteMany(result.Documents).Refresh());
			Assert.True(deleteResult.IsValid, deleteResult.ConnectionStatus.ResponseRaw.Utf8String());
			Assert.False(deleteResult.Errors, deleteResult.ConnectionStatus.ResponseRaw.Utf8String());

			Assert.IsNotEmpty(deleteResult.Items);

			result = this.Client.Search<ElasticsearchProject>(q => q.MatchAll());
			Assert.IsNotEmpty(result.Documents);
			Assert.AreEqual(result.Total, totalResults - totalSet);

		}
		[Test]
		public void RemoveAllByPassingAsIEnumerableOfBulkParameters()
		{
			this.ResetIndexes();
			var result = this.Client.Search<ElasticsearchProject>(q => q.MatchAll());
			Assert.IsNotNull(result);
			Assert.IsNotNull(result.Documents);
			var totalSet = result.Documents.Count();
			Assert.Greater(totalSet, 0);
			var totalResults = result.Total;

			var deleteResult = this.Client.Bulk(b=>b.DeleteMany(result.Documents, (p, o) => p.VersionType(VersionType.Internal)).Refresh());
			Assert.True(deleteResult.IsValid, deleteResult.ConnectionStatus.ResponseRaw.Utf8String());
			Assert.False(deleteResult.Errors, deleteResult.ConnectionStatus.ResponseRaw.Utf8String());

			Assert.IsNotEmpty(deleteResult.Items);

			result = this.Client.Search<ElasticsearchProject>(q => q.MatchAll());
			Assert.IsNotNull(result);
			Assert.IsNotNull(result.Documents);
			Assert.AreEqual(result.Total, totalResults - totalSet);
		}
		[Test]
		public void RemoveAllByQuery()
		{
			this.ResetIndexes();
			var result = this.Client.Search<ElasticsearchProject>(s => s
				.Query(q => q.Term(f => f.Name, "elasticsearch.pm"))
			);
			Assert.IsNotNull(result);
			Assert.IsNotNull(result.Documents);
			var totalSet = result.Documents.Count();
			Assert.Greater(totalSet, 0);
			var totalResults = result.Total;
			var deleteResult = this.Client.DeleteByQuery<ElasticsearchProject>(d => d
				.Query(q=>q
					.Term(f => f.Name, "elasticsearch.pm")
				)
			);

			deleteResult.IsValid.Should().BeTrue();

			result = this.Client.Search<ElasticsearchProject>(s => s
				.Query(q => q.Term(f => f.Name, "elasticsearch.pm"))
			);
			Assert.IsNotNull(result);
			Assert.IsNotNull(result.Documents);
			Assert.True(result.Total == 0);

			//make sure we did not delete all.
			var countResult = this.Client.Count(c=>c.Query(q => q.MatchAll()));
			Assert.True(countResult.IsValid);
			Assert.Greater(countResult.Count, 0);

		}
		[Test]
		public void RemoveAllByTermQuery()
		{
			var deleteQuery = @" {
							""term"" : { ""name"" : ""elasticsearch.pm"" }
					}";
			var query = @" { ""query"" : " + deleteQuery + "}";
			this.ResetIndexes();
			var result = this.Client.Search<ElasticsearchProject>(s => s
				.Query(q => q.Term(f => f.Name, "elasticsearch.pm"))
			);
			Assert.IsNotNull(result);
			Assert.IsNotNull(result.Documents);
			var totalSet = result.Documents.Count();
			Assert.Greater(totalSet, 0);
			var totalResults = result.Total;
			var deleteResult = this.Client.DeleteByQuery<ElasticsearchProject>(d => d
				.Query(q=>q
					.Term(f => f.Name, "elasticsearch.pm")
				)
			);

			deleteResult.IsValid.Should().BeTrue();

			result = this.Client.Search<ElasticsearchProject>(s => s
				.Query(q => q.Term(f => f.Name, "elasticsearch.pm"))
			);
			Assert.IsNotNull(result);
			Assert.IsNotNull(result.Documents);
			Assert.True(result.Total == 0);

			//make sure we did not delete all.
			var countResult = this.Client.Count<ElasticsearchProject>(c=>c.Query(q => q.MatchAll()));
			Assert.True(countResult.IsValid);
			Assert.Greater(countResult.Count, 0);
		}
		[Test]
		public void RemoveAllByQueryOverIndices()
		{
			this.ResetIndexes();
			var result = this.Client.Search<ElasticsearchProject>(s => s
				.Query(q => q.Term(f => f.Name, "elasticsearch.pm"))
			);
			Assert.IsNotNull(result);
			Assert.IsNotNull(result.Documents);
			var totalSet = result.Documents.Count();
			Assert.Greater(totalSet, 0);
			var totalResults = result.Total;
			this.Client.DeleteByQuery<ElasticsearchProject>(d => d
				.Indices(new[]
				{
					ElasticsearchConfiguration.DefaultIndex, 
					ElasticsearchConfiguration.DefaultIndex + "_clone"
				})
				.Query(q=>q 
					.Term(f => f.Name, "elasticsearch.pm")
				)
			);

			result = this.Client.Search<ElasticsearchProject>(s => s
				.Query(q => q.Term(f => f.Name, "elasticsearch.pm"))
			);
			Assert.IsNotNull(result);
			Assert.IsNotNull(result.Documents);
			Assert.True(result.Total == 0);

			//make sure we did not delete all.
			var countResult = this.Client.Count<ElasticsearchProject>(c=>c.Query(q => q.MatchAll()));
			Assert.True(countResult.IsValid);
			Assert.Greater(countResult.Count, 0);

		}
	}
}
