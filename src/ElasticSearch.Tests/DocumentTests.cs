using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using ElasticSearch.Client;
using HackerNews.Indexer.Domain;
using Nest.TestData;
using Nest.TestData.Domain;

namespace ElasticSearch.Tests
{
    /// <summary>
    /// Document specific tests
    /// </summary>
    public class DocumentTests : BaseElasticSearchTests
    {
        [Fact]
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
            Assert.Equal(documentToFind.Country, foundDocument.Country);
            Assert.Equal(documentToFind.Followers.Count, foundDocument.Followers.Count);
            Assert.Equal(documentToFind.Id, foundDocument.Id);
            Assert.Equal(documentToFind.Name, foundDocument.Name);
        }

        [Fact]
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

            //assert
            //grab document back from the index and make sure it is the same document
            var foundDocument = this.ConnectedClient.Get<ElasticSearchProject>(newDocument.Id);

            Assert.Equal(newDocument.Country, foundDocument.Country);
            Assert.Equal(newDocument.Followers.Count, foundDocument.Followers.Count);
            Assert.Equal(newDocument.Id, foundDocument.Id);
            Assert.Equal(newDocument.Name, foundDocument.Name);

            //act
            //now remove the item that was added
            this.ConnectedClient.Delete<ElasticSearchProject>(newDocument.Id);

            //assert
            //make sure getting by id returns nothing
            foundDocument = this.ConnectedClient.Get<ElasticSearchProject>(newDocument.Id);
            Assert.Null(foundDocument);
        }
    }
}
