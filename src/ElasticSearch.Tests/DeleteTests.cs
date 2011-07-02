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
            var hit = queryResults.HitsMetaData.Hits[0];
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
            this.ConnectedClient.Index<ElasticSearchProject>(newDocument);

            Thread.Sleep(1000);

            //assert
            //grab document back from the index and make sure it is the same document
            var foundDocument = this.ConnectedClient.Get<ElasticSearchProject>(newDocument.Id);

            //Assert.Equal(newDocument.Country, foundDocument.Country);
			Assert.AreEqual(newDocument.Followers.Count, foundDocument.Followers.Count);
			Assert.AreEqual(newDocument.Id, foundDocument.Id);
			Assert.AreEqual(newDocument.Name, foundDocument.Name);

            //act
            //now remove the item that was added
            this.ConnectedClient.DeleteById<ElasticSearchProject>(newDocument.Id);

            Thread.Sleep(1000);

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
			this.ConnectedClient.Index(newDocument);

			Thread.Sleep(1000);

			//assert
			//grab document back from the index and make sure it is the same document
			var foundDocument = this.ConnectedClient.Get<ElasticSearchProject>(newDocument.Id);

			//Assert.Equal(newDocument.Country, foundDocument.Country);
			Assert.AreEqual(newDocument.Followers.Count, foundDocument.Followers.Count);
			Assert.AreEqual(newDocument.Id, foundDocument.Id);
			Assert.AreEqual(newDocument.Name, foundDocument.Name);

			//act
			//now remove the item that was added
			this.ConnectedClient.Delete(newDocument);

			Thread.Sleep(1000);

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
			this.ConnectedClient.Index(newDocument);

			Thread.Sleep(1000);

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
			
			var result = this.ConnectedClient.Search<ElasticSearchProject>(
					@" { ""query"" : {
						    ""match_all"" : { }
					} }"
			);
			Assert.IsNotNull(result);
			Assert.IsNotNull(result.Documents);
			Assert.Greater(result.Documents.Count(), 0);

			this.ConnectedClient.Delete(result.Documents);
		
		}
    }
}
