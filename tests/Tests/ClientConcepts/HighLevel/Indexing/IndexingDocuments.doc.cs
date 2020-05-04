// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

﻿using System;
using System.Threading.Tasks;
using Elasticsearch.Net;
using Nest;
using Tests.Framework.DocumentationTests;

namespace Tests.ClientConcepts.HighLevel.Indexing
{
	/**[[indexing-documents]]
	*=== Indexing
	*
	* NEST has a number of ways to index documents.
	*/
	public class Indexing : DocumentationTestBase
	{
		private readonly IElasticClient client = new ElasticClient(new ConnectionSettings(new SingleNodeConnectionPool(new Uri("http://localhost:9200")), new InMemoryConnection()));

		/// hide
		public class Person
		{
			public int Id { get; set; }
			public string FirstName { get; set; }
			public string LastName { get; set; }
		}

		/**
		* ==== Single documents
		* A single document can be indexed at a time, either synchronously or asynchronously.
		* These methods use the `IndexDocument` methods, which is a simple way to index single documents.
		*/
		public async Task SingleDocument()
		{
			var person = new Person
			{
				Id = 1,
				FirstName = "Martijn",
				LastName = "Laarman"
			};

			var indexResponse = client.IndexDocument(person); //<1> synchronous method that returns an IIndexResponse
			if (!indexResponse.IsValid)
			{
				// If the request isn't valid, we can take action here
			}

			var indexResponseAsync = await client.IndexDocumentAsync(person); //<2> asynchronous method that returns a Task<IIndexResponse> that can be awaited
		}

		/**
		* ==== Single documents with parameters
		* If you need to set additional parameters when indexing you can use the fluent or object initializer syntax.
		* This will allow you finer control over the indexing of single documents.
		*/
		public void SingleDocumentWithParameters()
		{
			var person = new Person
			{
				Id = 1,
				FirstName = "Martijn",
				LastName = "Laarman"
			};

			client.Index(person, i => i.Index("people")); //<1> fluent syntax

			client.Index(new IndexRequest<Person>(person, "people")); //<2> object initializer syntax
		}

		/**
		* ==== Multiple documents with `IndexMany`
		*
		* Multiple documents can be indexed using the `IndexMany` and `IndexManyAsync` methods, again either synchronously or asynchronously, respectively.
		* These methods are specific to the NEST client and wrap calls to the `_bulk` endpoint, providing a convenient shortcut to indexing
		* multiple documents.
		*
		* Please note, these methods index all documents in a single HTTP request, so for very large document collections it is not a recommended approach
		* - consider using the `BulkAllObservable` helper instead.
		*/
		public async Task IndexManyDocuments()
		{
			var people = new []
			{
				new Person
				{
					Id = 1,
					FirstName = "Martijn",
					LastName = "Laarman"
				},
				new Person
				{
					Id = 2,
					FirstName = "Stuart",
					LastName = "Cam"
				},
				new Person
				{
					Id = 3,
					FirstName = "Russ",
					LastName = "Cam"
				}
			};

			var indexManyResponse = client.IndexMany(people); //<1> synchronous method that returns an IBulkResponse

			if (indexManyResponse.Errors) //<2> the response can be inspected to see if any of the bulk operations resulted in an error
			{
				foreach (var itemWithError in indexManyResponse.ItemsWithErrors) //<3> If there are errors, they can be enumerated and inspected
				{
					Console.WriteLine("Failed to index document {0}: {1}", itemWithError.Id, itemWithError.Error);
				}
			}

			// Alternatively, documents can be indexed asynchronously
			var indexManyAsyncResponse = await client.IndexManyAsync(people); //<4> asynchronous method that returns a Task<IBulkResponse> that can be awaited
		}

		/**
		* ==== Multiple documents with bulk
		*
		* If you require finer grained control over indexing many documents you can use the `Bulk` and `BulkAsync` methods and use the descriptors to
		* customise the bulk calls.
		*
		* As with the `IndexMany` methods above, documents are sent to the `_bulk` endpoint in a single HTTP request.
		* This does mean that consideration will need to be given to the overall size of the HTTP request. For indexing large numbers
		* of documents it may be sensible to perform multiple separate `Bulk` calls.
		*/
		public async Task BulkIndexDocuments()
		{
			//hide
			var people = new [] { new Person { Id = 1, FirstName = "Martijn", LastName = "Laarman" } };

			var bulkIndexResponse = client.Bulk(b => b
				.Index("people")
				.IndexMany(people)); //<1> synchronous method that returns an IBulkResponse, the same as IndexMany and can be inspected in the same way for errors

			// Alternatively, documents can be indexed asynchronously similar to IndexManyAsync
			var asyncBulkIndexResponse = await client.BulkAsync(b => b
				.Index("people")
				.IndexMany(people)); //<2> asynchronous method that returns a Task<IBulkResponse> that can be awaited
		}

		/**
		* ==== Multiple documents with `BulkAllObservable` helper
		*
		* Using the `BulkAllObservable` helper allows you to focus on the overall objective of indexing, without having to
		* concern yourself with retry, backoff or chunking mechanics.
		* Multiple documents can be indexed using the `BulkAll` method and `Wait()` extension method.
		*
		* This helper exposes functionality to automatically retry / backoff in the event of an indexing failure,
	    * and to control the number of documents indexed in a single HTTP request. In the example below each request will contain 1000 documents,
		* chunked from the original input. In the event of a large number of documents this could result in many HTTP requests, each containing
		* 1000 documents (the last request may contain less, depending on the total number).
		*
		* The helper will also lazily enumerate an `IEnumerable<T>` collection, allowing you to index a large number of documents easily.
		*/
		public void BulkDocumentsWithObservableHelper()
		{
                        // hide
			var people = new []
			{
				new Person
				{
					Id = 1,
					FirstName = "Martijn",
					LastName = "Laarman"
				},
				new Person
				{
					Id = 2,
					FirstName = "Stuart",
					LastName = "Cam"
				},
				new Person
				{
					Id = 3,
					FirstName = "Russ",
					LastName = "Cam"
				}
				// snip
			};

			var bulkAllObservable = client.BulkAll(people, b => b
				.Index("people")
				.BackOffTime("30s") //<1> how long to wait between retries
				.BackOffRetries(2) //<2> how many retries are attempted if a failure occurs
				.RefreshOnCompleted()
				.MaxDegreeOfParallelism(Environment.ProcessorCount)
				.Size(1000) // <3> items per bulk request
			)
			.Wait(TimeSpan.FromMinutes(15), next => //<4> perform the indexing and wait up to 15 minutes, whilst the BulkAll calls are asynchronous this is a blocking operation
			{
				// do something e.g. write number of pages to console
			});
		}

		/**
		* ==== Advanced bulk indexing
		*
		* The BulkAllObservable helper exposes a number of advanced features.
		*
		* 1. `BufferToBulk` allows for the customisation of individual operations within the bulk request before it is dispatched to the server.
		* 2. `RetryDocumentPredicate` enables fine control on deciding if a document that failed to be indexed should be retried.
		* 3. `DroppedDocumentCallback` in the event a document is not indexed, even after retrying, this delegate is called.
		*/
		public void AdvancedBulkIndexing()
		{
			//hide
			var people = new[] { new Person() };

			client.BulkAll(people, b => b
				  .BufferToBulk((descriptor, list) => //<1> customise the individual operations in the bulk request before it is dispatched
				  {
					  foreach (var item in list)
					  {
						  descriptor.Index<Person>(bi => bi
							  .Index(item.Id % 2 == 0 ? "even-index" : "odd-index") //<2> Index each document into either even-index or odd-index
							  .Document(item)
						  );
					  }
				  })
				  .RetryDocumentPredicate((item, person) => //<3> decide if a document should be retried in the event of a failure
				  {
					  return item.Error.Index == "even-index" && person.FirstName == "Martijn";
				  })
				  .DroppedDocumentCallback((item, person) => //<4> if a document cannot be indexed this delegate is called
				  {
					  Console.WriteLine($"Unable to index: {item} {person}");
				  }));
		}
	}
}
