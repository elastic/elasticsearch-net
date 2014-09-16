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

			var newIndex = IntegrationSetup.CreateNewIndexWithData(this.Client);
			var firstName = NestTestData.Data.First().Followers.First().FirstName.ToLowerInvariant();
			var queryResults = this.Client.Search<ElasticsearchProject>(s => s
				.Index(newIndex)
				.Query(q=>
					q.Fuzzy(fq=>fq.OnField(p=>p.Followers.First().FirstName).Value(firstName + "x"))
				)
			);

			Assert.Greater(queryResults.Total, 0);

			var hit = queryResults.HitsMetaData.Hits.First();
			var documentToFind = hit.Source;

			//act
			//attempt to grab the same document using the document's id
			var foundDocument = this.Client.Source<ElasticsearchProject>(hit.Id, newIndex);

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
			var newIndex = IntegrationSetup.CreateNewIndexWithData(this.Client);
			var newDocument = new ElasticsearchProject
			{
				Country = "Mozambique",
				Followers = new List<Person>(),
				Id = DateTime.Now.Millisecond + 1500, 
				Name = "Test Document for 'IndexDocument' test"
			};

			this.Client.Index<ElasticsearchProject>(newDocument, i=>i.Index(newIndex).Refresh());

			var foundDocument = this.Client.Source<ElasticsearchProject>(newDocument.Id, newIndex);

			Assert.AreEqual(newDocument.Followers.Count, foundDocument.Followers.Count);
			Assert.AreEqual(newDocument.Id, foundDocument.Id);
			Assert.AreEqual(newDocument.Name, foundDocument.Name);

			var response = this.Client.Delete<ElasticsearchProject>(f=>f.Id(newDocument.Id).Index(newIndex).Refresh());

			foundDocument = this.Client.Source<ElasticsearchProject>(newDocument.Id, newIndex);
			Assert.Null(foundDocument);
		}
		[Test]
		public void IndexThanDeleteDocumentByObject()
		{
			var newIndex = IntegrationSetup.CreateNewIndexWithData(this.Client);
			var newDocument = new ElasticsearchProject
			{
				Country = "Mozambique",
				Followers = new List<Person>(),
				Id = DateTime.Now.Millisecond + 1500, //try to get this example out of the way of existing test data
				Name = "Test Document for 'IndexDocument' test"
			};

			//act
			//index the new item
			this.Client.Index(newDocument, i=>i.Refresh().Index(newIndex));

			//assert
			//grab document back from the index and make sure it is the same document
			var foundDocument = this.Client.Source<ElasticsearchProject>(newDocument.Id, newIndex);

			//Assert.Equal(newDocument.Country, foundDocument.Country);
			Assert.AreEqual(newDocument.Followers.Count, foundDocument.Followers.Count);
			Assert.AreEqual(newDocument.Id, foundDocument.Id);
			Assert.AreEqual(newDocument.Name, foundDocument.Name);

			//act
			//now remove the item that was added
			this.Client.Delete<ElasticsearchProject>(f=>f.IdFrom(newDocument).Refresh().Index(newIndex));

			//assert
			//make sure getting by id returns nothing
			foundDocument = this.Client.Source<ElasticsearchProject>(newDocument.Id, newIndex);
			Assert.Null(foundDocument);
		}

		[Test]
		public void IndexThenDeleteUsingRefresh()
		{
			var newIndex = IntegrationSetup.CreateNewIndexWithData(this.Client);
			var newDocument = new ElasticsearchProject
			{
				Country = "Mozambique",
				Followers = new List<Person>(),
				Id = DateTime.Now.Millisecond + 1500, 
				Name = "Test Document for 'IndexDocument' test"
			};

			this.Client.Index(newDocument, i=>i.Refresh().Index(newIndex));

			//grab document back from the index and make sure it is the same document
			var foundDocument = this.Client.Source<ElasticsearchProject>(newDocument.Id, newIndex);

			Assert.AreEqual(newDocument.Followers.Count, foundDocument.Followers.Count);
			Assert.AreEqual(newDocument.Id, foundDocument.Id);
			Assert.AreEqual(newDocument.Name, foundDocument.Name);

			//now remove the item that was added
			this.Client.Delete<ElasticsearchProject>(d=>d.IdFrom(newDocument).Refresh().Index(newIndex));

			//make sure getting by id returns nothing
			foundDocument = this.Client.Source<ElasticsearchProject>(newDocument.Id, newIndex);
			Assert.Null(foundDocument);
		}
		[Test]
		public void RemoveAllByPassingAsIEnumerable()
		{
			var newIndex = IntegrationSetup.CreateNewIndexWithData(this.Client);
			var result = this.Client.Search<ElasticsearchProject>(q => q
				.Index(newIndex)
				.From(0)
				.Take(5)
				.MatchAll()
			);
			Assert.IsNotEmpty(result.Documents);

			var totalSet = result.Documents.Count();
			var totalResults = result.Total;
			Assert.Greater(totalSet, 0);

			var deleteResult = this.Client.Bulk(b => b
				.FixedPath(newIndex)
				.DeleteMany(result.Documents).Refresh()
			);
			Assert.True(deleteResult.IsValid, deleteResult.ConnectionStatus.ResponseRaw.Utf8String());
			Assert.False(deleteResult.Errors, deleteResult.ConnectionStatus.ResponseRaw.Utf8String());

			Assert.IsNotEmpty(deleteResult.Items);

			result = this.Client.Search<ElasticsearchProject>(q => q.Index(newIndex).MatchAll());
			Assert.IsNotEmpty(result.Documents);
			Assert.AreEqual(result.Total, totalResults - totalSet);

		}
		[Test]
		public void RemoveAllByPassingAsIEnumerableOfBulkParameters()
		{
			var newIndex = IntegrationSetup.CreateNewIndexWithData(this.Client);
			var result = this.Client.Search<ElasticsearchProject>(s => s
				.Index(newIndex)
				.MatchAll()
			);
			Assert.IsNotNull(result);
			Assert.IsNotNull(result.Documents);
			var totalSet = result.Documents.Count();
			Assert.Greater(totalSet, 0);
			var totalResults = result.Total;

			var deleteResult = this.Client.Bulk(b=>b
				.FixedPath(newIndex)
				.DeleteMany(result.Documents, (p, o) => p.VersionType(VersionType.Internal))
				.Refresh()
			);
			Assert.True(deleteResult.IsValid, deleteResult.ConnectionStatus.ResponseRaw.Utf8String());
			Assert.False(deleteResult.Errors, deleteResult.ConnectionStatus.ResponseRaw.Utf8String());

			Assert.IsNotEmpty(deleteResult.Items);

			result = this.Client.Search<ElasticsearchProject>(s => s
				.Index(newIndex)
				.MatchAll()
			);
			Assert.IsNotNull(result);
			Assert.IsNotNull(result.Documents);
			Assert.AreEqual(result.Total, totalResults - totalSet);
		}
		[Test]
		public void RemoveAllByQuery()
		{
			var newIndex = IntegrationSetup.CreateNewIndexWithData(this.Client);
			var result = this.Client.Search<ElasticsearchProject>(s => s
				.Index(newIndex)
				.Query(q => q.Term(f => f.Name, "elasticsearch.pm"))
			);
			Assert.IsNotNull(result);
			Assert.IsNotNull(result.Documents);
			var totalSet = result.Documents.Count();
			Assert.Greater(totalSet, 0);
			var totalResults = result.Total;
			var deleteResult = this.Client.DeleteByQuery<ElasticsearchProject>(d => d
				.Index(newIndex)
				.Query(q=>q
					.Term(f => f.Name, "elasticsearch.pm")
				)
			);

			deleteResult.IsValid.Should().BeTrue();

			result = this.Client.Search<ElasticsearchProject>(s => s
				.Index(newIndex)
				.Query(q => q.Term(f => f.Name, "elasticsearch.pm"))
			);
			Assert.IsNotNull(result);
			Assert.IsNotNull(result.Documents);
			Assert.True(result.Total == 0);

			//make sure we did not delete all.
			var countResult = this.Client.Count(c=>c
				.Index(newIndex)
				.Query(q => q.MatchAll())
			);
			Assert.True(countResult.IsValid);
			Assert.Greater(countResult.Count, 0);

		}
		[Test]
		public void RemoveAllByTermQuery()
		{
			var newIndex = IntegrationSetup.CreateNewIndexWithData(this.Client);
			
			var result = this.Client.Search<ElasticsearchProject>(s => s
				.Index(newIndex)
				.Query(q => q.Term(f => f.Name, "elasticsearch.pm"))
			);
			Assert.IsNotNull(result);
			Assert.IsNotNull(result.Documents);
			var totalSet = result.Documents.Count();
			Assert.Greater(totalSet, 0);
			var totalResults = result.Total;
			var deleteResult = this.Client.DeleteByQuery<ElasticsearchProject>(d => d
				.Index(newIndex)
				.Query(q=>q
					.Term(f => f.Name, "elasticsearch.pm")
				)
			);

			deleteResult.IsValid.Should().BeTrue();

			result = this.Client.Search<ElasticsearchProject>(s => s
				.Index(newIndex)
				.Query(q => q.Term(f => f.Name, "elasticsearch.pm"))
			);
			Assert.IsNotNull(result);
			Assert.IsNotNull(result.Documents);
			Assert.True(result.Total == 0);

			//make sure we did not delete all.
			var countResult = this.Client.Count<ElasticsearchProject>(c=>c
				.Index(newIndex)
				.Query(q => q.MatchAll())
			);
			Assert.True(countResult.IsValid);
			Assert.Greater(countResult.Count, 0);
		}
		[Test]
		public void RemoveAllByQueryOverIndices()
		{
			var newIndex = IntegrationSetup.CreateNewIndexWithData(this.Client);
			var result = this.Client.Search<ElasticsearchProject>(s => s
				.Index(newIndex)
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
					newIndex
				})
				.Query(q=>q 
					.Term(f => f.Name, "elasticsearch.pm")
				)
			);

			result = this.Client.Search<ElasticsearchProject>(s => s
				.Index(newIndex)
				.Query(q => q.Term(f => f.Name, "elasticsearch.pm"))
			);
			Assert.IsNotNull(result);
			Assert.IsNotNull(result.Documents);
			Assert.True(result.Total == 0);

			//make sure we did not delete all.
			var countResult = this.Client.Count<ElasticsearchProject>(c=>c
				.Index(newIndex)
				.Query(q => q.MatchAll())
			);
			Assert.True(countResult.IsValid);
			Assert.Greater(countResult.Count, 0);

		}
	}
}
