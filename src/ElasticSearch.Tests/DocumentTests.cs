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
    /// <summary>
    /// Document specific tests
    /// </summary>
    public class DocumentTests : BaseElasticSearchTests
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
            Assert.Equals(documentToFind.Country, foundDocument.Country);
			Assert.Equals(documentToFind.Followers.Count, foundDocument.Followers.Count);
			Assert.Equals(documentToFind.Id, foundDocument.Id);
			Assert.Equals(documentToFind.Name, foundDocument.Name);
        }

		[Test]
        public void IndexThanDeleteDocument()
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

            Thread.Sleep(3000);

            //assert
            //grab document back from the index and make sure it is the same document
            var foundDocument = this.ConnectedClient.Get<ElasticSearchProject>(newDocument.Id);

            //Assert.Equal(newDocument.Country, foundDocument.Country);
			Assert.Equals(newDocument.Followers.Count, foundDocument.Followers.Count);
			Assert.Equals(newDocument.Id, foundDocument.Id);
			Assert.Equals(newDocument.Name, foundDocument.Name);

            //act
            //now remove the item that was added
            this.ConnectedClient.Delete<ElasticSearchProject>(newDocument.Id);

            Thread.Sleep(3000);

            //assert
            //make sure getting by id returns nothing
            foundDocument = this.ConnectedClient.Get<ElasticSearchProject>(newDocument.Id);
            Assert.Null(foundDocument);
        }
    }
}
