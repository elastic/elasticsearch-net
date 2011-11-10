using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ElasticSearch.Client;
using HackerNews.Indexer.Domain;
using Nest.TestData;
using Nest.TestData.Domain;
using System.Threading;
using NUnit.Framework;

namespace ElasticSearch.Tests
{
    [TestFixture]
    public class DeleteTests : BaseElasticSearchTests
    {
        [Test]
        public void HitsMaxScoreIsSet()
        {
            //arrange
            //pull existing example through method we know is functional based on other passing unit tests
            var queryResults = this.ConnectedClient.Search<ElasticSearchProject>(
                @" { ""query"" : {
						 ""query_string"" : {
							""query"" : ""*""
						}
					} }"
            );

            var hits = queryResults.Hits;
            
            Assert.AreEqual(1, hits.MaxScore);
            Assert.AreEqual(hits.Hits.Max(h => h.Score), hits.MaxScore);
        }

        [Test]
        public void GetDocumentById()
        {
            //arrange
            //pull existing example through method we know is functional based on other passing unit tests
            var queryResults = this.ConnectedClient.Search<ElasticSearchProject>(
                @" { ""query"" : {
						 ""fuzzy"" : {
							""followers.firstName"" : """ + NestTestData.Data.First().Followers.First().FirstName.ToLower() + @"x""
						}
					} }"
            );
            var hit = queryResults.Hits.Hits[0];
            var documentToFind = hit.Source;

            //act
            //attempt to grab the same document using the document's id
            var foundDocument = this.ConnectedClient.Get<ElasticSearchProject>(hit.Id);

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
            ElasticSearchProject newDocument = new ElasticSearchProject
            {
                Country = "Mozambique",
                Followers = new List<Person>(),
                Id = DateTime.Now.Millisecond + 1500, //try to get this example out of the way of existing test data
                Name = "Test Document for 'IndexDocument' test"
            };

            //act
            //index the new item
            this.ConnectedClient.Index<ElasticSearchProject>(newDocument, new IndexParameters { Refresh = true });

            //assert
            //grab document back from the index and make sure it is the same document
            var foundDocument = this.ConnectedClient.Get<ElasticSearchProject>(newDocument.Id);

            //Assert.Equal(newDocument.Country, foundDocument.Country);
			Assert.AreEqual(newDocument.Followers.Count, foundDocument.Followers.Count);
			Assert.AreEqual(newDocument.Id, foundDocument.Id);
			Assert.AreEqual(newDocument.Name, foundDocument.Name);

            //act
            //now remove the item that was added
            this.ConnectedClient.DeleteById<ElasticSearchProject>(newDocument.Id, new DeleteParameters { Refresh = true });

            //assert
            //make sure getting by id returns nothing
            foundDocument = this.ConnectedClient.Get<ElasticSearchProject>(newDocument.Id);
            Assert.Null(foundDocument);
        }
		[Test]
		public void IndexThanDeleteDocumentByObject()
		{
			//arrange
			//create a new document to index
			ElasticSearchProject newDocument = new ElasticSearchProject
			{
				Country = "Mozambique",
				Followers = new List<Person>(),
				Id = DateTime.Now.Millisecond + 1500, //try to get this example out of the way of existing test data
				Name = "Test Document for 'IndexDocument' test"
			};

			//act
			//index the new item
			this.ConnectedClient.Index(newDocument, new IndexParameters { Refresh = true });

			//assert
			//grab document back from the index and make sure it is the same document
			var foundDocument = this.ConnectedClient.Get<ElasticSearchProject>(newDocument.Id);

			//Assert.Equal(newDocument.Country, foundDocument.Country);
			Assert.AreEqual(newDocument.Followers.Count, foundDocument.Followers.Count);
			Assert.AreEqual(newDocument.Id, foundDocument.Id);
			Assert.AreEqual(newDocument.Name, foundDocument.Name);

			//act
			//now remove the item that was added
			this.ConnectedClient.Delete(newDocument, new DeleteParameters { Refresh = true });

			//assert
			//make sure getting by id returns nothing
			foundDocument = this.ConnectedClient.Get<ElasticSearchProject>(newDocument.Id);
			Assert.Null(foundDocument);
		}

		[Test]
		public void IndexThenDeleteUsingRefresh()
		{
			//arrange
			//create a new document to index
			ElasticSearchProject newDocument = new ElasticSearchProject
			{
				Country = "Mozambique",
				Followers = new List<Person>(),
				Id = DateTime.Now.Millisecond + 1500, //try to get this example out of the way of existing test data
				Name = "Test Document for 'IndexDocument' test"
			};

			//act
			//index the new item
			this.ConnectedClient.Index(newDocument, new IndexParameters { Refresh = true });

			//assert
			//grab document back from the index and make sure it is the same document
			var foundDocument = this.ConnectedClient.Get<ElasticSearchProject>(newDocument.Id);

			//Assert.Equal(newDocument.Country, foundDocument.Country);
			Assert.AreEqual(newDocument.Followers.Count, foundDocument.Followers.Count);
			Assert.AreEqual(newDocument.Id, foundDocument.Id);
			Assert.AreEqual(newDocument.Name, foundDocument.Name);

			//act
			//now remove the item that was added
			this.ConnectedClient.Delete(newDocument, new DeleteParameters() { Refresh = true });

			//assert
			//make sure getting by id returns nothing
			foundDocument = this.ConnectedClient.Get<ElasticSearchProject>(newDocument.Id);
			Assert.Null(foundDocument);
		}
		[Test]
		public void RemoveAllByPassingAsIEnumerable()
		{
			this.ResetIndexes();
			var result = this.ConnectedClient.Search<ElasticSearchProject>(
					@" { ""query"" : {
						    ""match_all"" : { }
					} }"
			);
			Assert.IsNotNull(result);
			Assert.IsNotNull(result.Documents);
			var totalSet = result.Documents.Count();
			Assert.Greater(totalSet, 0);
			var totalResults = result.Total;
			this.ConnectedClient.Delete(result.Documents, new SimpleBulkParameters() { Refresh = true });

			result = this.ConnectedClient.Search<ElasticSearchProject>(
					@" { ""query"" : {
						    ""match_all"" : { }
					} }"
			);
			Assert.IsNotNull(result);
			Assert.IsNotNull(result.Documents);
			Assert.True(result.Total == totalResults - totalSet);
		
		}
		[Test]
		public void RemoveAllByPassingAsIEnumerableOfBulkParameters()
		{
			this.ResetIndexes();
			var result = this.ConnectedClient.Search<ElasticSearchProject>(
					@" { ""query"" : {
						    ""match_all"" : { }
					} }"
			);
			Assert.IsNotNull(result);
			Assert.IsNotNull(result.Documents);
			var totalSet = result.Documents.Count();
			Assert.Greater(totalSet, 0);
			var totalResults = result.Total;

			var parameterizedDocuments = result.Documents.Select(d=> new BulkParameters<ElasticSearchProject>(d) { Routing = "id" });

			this.ConnectedClient.Delete(parameterizedDocuments, new SimpleBulkParameters() { Refresh = true });

			result = this.ConnectedClient.Search<ElasticSearchProject>(
					@" { ""query"" : {
						    ""match_all"" : { }
					} }"
			);
			Assert.IsNotNull(result);
			Assert.IsNotNull(result.Documents);
			Assert.True(result.Total == totalResults - totalSet);
		}
		[Test]
		public void RemoveAllByQuery()
		{
			var query = @" { ""query"" : {
						    ""match_all"" : { }
					} }";
			this.ResetIndexes();
			var result = this.ConnectedClient.Search<ElasticSearchProject>(query);
			Assert.IsNotNull(result);
			Assert.IsNotNull(result.Documents);
			var totalSet = result.Documents.Count();
			Assert.Greater(totalSet, 0);
			var totalResults = result.Total;
			this.ConnectedClient.DeleteByQuery<ElasticSearchProject>(query);

			result = this.ConnectedClient.Search<ElasticSearchProject>(query);
			Assert.IsNotNull(result);
			Assert.IsNotNull(result.Documents);
			Assert.True(result.Total == 0);
		}
		[Test]
		public void RemoveAllByQueryOverIndices()
		{
			var query = @" { ""query"" : {
						    ""match_all"" : { }
					} }";
			this.ResetIndexes();
			var result = this.ConnectedClient.Search<ElasticSearchProject>(query);
			Assert.IsNotNull(result);
			Assert.IsNotNull(result.Documents);
			var totalSet = result.Documents.Count();
			Assert.Greater(totalSet, 0);
			var totalResults = result.Total;
			this.ConnectedClient.DeleteByQueryOverIndices<ElasticSearchProject>(query, new[] { Test.Default.DefaultIndex, Test.Default.DefaultIndex + "_clone" });

			result = this.ConnectedClient.Search<ElasticSearchProject>(query);
			Assert.IsNotNull(result);
			Assert.IsNotNull(result.Documents);
			Assert.True(result.Total == 0);

			//TODO test _clone when we can specify index for search;

		}
    }
}
