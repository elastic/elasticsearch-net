// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information.

using System;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Tests.Framework.DocumentationTests;

namespace Tests.ClientConcepts.HighLevel.Indexing
{
	/**[[indexing-documents]]
	*=== Indexing documents
	*
	* NEST exposes the index and bulk APIs of Elasticsearch as methods, to enable indexing of single or multiple documents. In addition to this,
	* the client provides some convenient shorthand methods for the typical indexing approaches.
	*/
	public class Indexing : DocumentationTestBase
	{
		private readonly ElasticsearchClient _client = new(new ElasticsearchClientSettings(
			new SingleNodePool(new Uri("http://localhost:9200")), new InMemoryTransportClient()));

		/// hide
		public class Person
		{
			public int Id { get; set; }
			public string FirstName { get; set; }
			public string LastName { get; set; }
		}

		///**
		//* ==== Single documents
		//*
		//* A single document can be indexed, either synchronously or asynchronously,
		//* using the `IndexDocument` and `IndexDocumentAsync` methods, respectively. These methods are a simple way to index a single document
		//* that doesn't require any additional request parameters
		//*/
		//public async Task SingleDocument()
		//{
		//	var person = new Person
		//	{
		//		Id = 1,
		//		FirstName = "Martijn",
		//		LastName = "Laarman"
		//	};

		//	var indexResponse = _client.IndexDocument(person); //<1> synchronous method that returns an `IndexResponse`
		//	if (!indexResponse.IsValid)
		//	{
		//		// If the request isn't valid, we can take action here
		//	}

		//	var indexResponseAsync = await _client.IndexDocumentAsync(person); //<2> asynchronous method that returns a `Task<IndexResponse>` that can be awaited
		//}

		/**
		* ==== Single documents with parameters
		* If you need to set additional parameters when indexing, you can use the `Index` method with either the fluent or object initializer syntax.
		* The `Index` method exposes a way to set additional parameters such as the name of the index in which to index, the id to assign to the
		* document, routing parameters, etc., allowing more control over indexing.
		*/
		public void SingleDocumentWithParameters()
		{
			var person = new Person
			{
				Id = 1,
				FirstName = "Martijn",
				LastName = "Laarman"
			};

			var indexResponse1 = _client.Index(person, i => i.Index("people")); //<1> fluent syntax

			var indexResponse2 = _client.Index(new IndexRequest<Person>(person, "people")); //<2> object initializer syntax
		}

		/**
		* ==== Multiple documents with `IndexMany`
		*
		* Multiple documents can be indexed using the `IndexMany` and `IndexManyAsync` methods, again either synchronously or asynchronously, respectively.
		* These methods are specific to the NEST client to provide a convenient shortcut to indexing
		* multiple documents using the `_bulk` endpoint.
		*
		* NOTE: `IndexMany` all documents in a single HTTP request, so for very large document collections it is not a recommended approach
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

			var indexManyResponse = _client.IndexMany(people); //<1> synchronous method that returns an IBulkResponse

			if (indexManyResponse.Errors) //<2> the response can be inspected to see if any of the bulk operations resulted in an error
			{
				foreach (var itemWithError in indexManyResponse.ItemsWithErrors) //<3> If there are errors, they can be enumerated and inspected
				{
					Console.WriteLine($"Failed to index document {itemWithError.Id}: {itemWithError.Error}");
				}
			}

			// Alternatively, documents can be indexed asynchronously
			var indexManyAsyncResponse = await _client.IndexManyAsync(people); //<4> asynchronous method that returns a Task<IBulkResponse> that can be awaited
		}

		/**
		* ==== Multiple documents with `Bulk`
		*
		* If you require more control over indexing many documents, you can use the `Bulk` and `BulkAsync` methods and use the descriptors to
		* customise the bulk calls.
		*
		* As with the `IndexMany` methods, documents are sent using the bulk API in a single HTTP request.
		* This does mean that consideration should be given to the overall size of the HTTP request. For indexing a large number
		* of documents, it may be sensible to perform multiple separate `Bulk` calls, or use <<bulkall-observable, `BulkAllObservable`>>,
		* which takes care of a lot of the complexity.
		*/
		public async Task BulkIndexDocuments()
		{
			//hide
			var people = new [] { new Person { Id = 1, FirstName = "Martijn", LastName = "Laarman" } };

			var bulkIndexResponse = _client.Bulk(b => b
				.Index("people")
				.IndexMany(people)
			); //<1> synchronous method that returns an IBulkResponse, the same as IndexMany and can be inspected in the same way for errors

			// Alternatively, documents can be indexed asynchronously similar to IndexManyAsync
			var asyncBulkIndexResponse = await _client.BulkAsync(b => b
				.Index("people")
				.IndexMany(people)
			); //<2> asynchronous method that returns a Task<IBulkResponse> that can be awaited
		}

		/**
		 * Control over how each bulk index operation is configured can be achieved by passing a descriptor to the `IndexMany`
		 * method on `Bulk`. Here's an example of specifying a different index and pipeline for each document, based on properties of
		 * the document to be indexed
		 */
		public void BulkIndexDocumentsWithDescriptor()
		{
			//hide
			var people = new Person[] { };

			var bulkIndexResponse = _client.Bulk(b => b
				.Index("people")
				.IndexMany(people, (descriptor, person) => descriptor
					.Index(person.Id % 2 == 0
						? "even-index"
						: "odd-index") // <1> configure an explicit index for a document, based on its `Id`
					.Pipeline(person.FirstName.StartsWith("M")
						? "startswith_m_pipeline"
						: "does_not_start_with_m_pipeline") // <2> specify an <<pipelines,ingest pipeline>> to use when indexing the document
				)
			);
		}

		/**[[bulkall-observable]]
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
		* The helper lazily enumerates the provided `IEnumerable<T>` of documents, allowing you to index a large number of documents easily
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

			var bulkAllObservable = _client.BulkAll(people, b => b
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

		///**
		//* The internal implementation of `BulkAllObservable` is asynchronous, using the
		//* https://docs.microsoft.com/en-us/dotnet/standard/events/observer-design-pattern[Observer Design Pattern] to enable observers to
		//* be registered to take action when each bulk response is returned, an error has occurred, and when the `BulkAllObservable` has
		//* finished. Whilst the internal implementation is asynchronous, you typically want to wait until all bulk indexing has finished before
		//* continuing. The `Wait` method is a convenient shorthand to use for this, using a `ManualResetEvent` to block the current thread until
		//* bulk indexing has finished or an error has occurred.
		//*
		//* ==== Advanced bulk indexing
		//*
		//* The `BulkAllObservable` helper exposes a number of methods to further control the process, such as
		//*
		//* * `BufferToBulk` to customize individual operations within the bulk request before it is dispatched to the server
		//* * `RetryDocumentPredicate` to decide if a document that failed to be indexed should be retried
		//* * `DroppedDocumentCallback` to  determine what to do in the event a document is not indexed, even after retrying
 		//	*
 		//	* The following example demonstrates some of these methods, in addition to using a `BulkAllObserver` to subscribe to
		//* the bulk indexing process and take some action on each successful bulk response, when an error occurs, and when
		//* the process has finished.
		//*
		//* IMPORTANT: An observer such as `BulkAllObserver` should not throw exceptions from its interface implementations, such
		//* as `OnNext` and `OnError`. Any exceptions thrown should be expected to go unhandled. In light of this, any exception
		//* that occurs during the bulk indexing process should be captured and thrown outside of the observer, as demonstrated in the
		//* example below. Take a look at the
		//* https://docs.microsoft.com/en-us/dotnet/standard/events/observer-design-pattern-best-practices#handling-exceptions[Observer Design Pattern best practices]
		//* on handling exceptions.
		//*/
		//public void AdvancedBulkIndexing()
		//{
		//	//hide
		//	var people = new Person[] { };

		//	var bulkAllObservable = client.BulkAll(people, b => b
		//		  .BufferToBulk((descriptor, buffer) => //<1> Customise each bulk operation before it is dispatched
		//		  {
		//			  foreach (var person in buffer)
		//			  {
		//				  descriptor.Index<Person>(bi => bi
		//					  .Index(person.Id % 2 == 0 ? "even-index" : "odd-index") //<2> Index each document into either even-index or odd-index
		//					  .Document(person)
		//				  );
		//			  }
		//		  })
		//		  .RetryDocumentPredicate((bulkResponseItem, person) => //<3> Decide if a document should be retried in the event of a failure
		//		  {
		//			  return bulkResponseItem.Error.Index == "even-index" && person.FirstName == "Martijn";
		//		  })
		//		  .DroppedDocumentCallback((bulkResponseItem, person) => //<4> If a document cannot be indexed this delegate is called
		//		  {
		//			  Console.WriteLine($"Unable to index: {bulkResponseItem} {person}");
		//		  }));

		//	var waitHandle = new ManualResetEvent(false);
		//	ExceptionDispatchInfo exceptionDispatchInfo = null;

		//	var observer = new BulkAllObserver(
		//		onNext: response =>
		//		{
		//			// do something e.g. write number of pages to console
		//		},
		//		onError: exception =>
		//		{
		//			exceptionDispatchInfo = ExceptionDispatchInfo.Capture(exception);
		//			waitHandle.Set();
		//		},
		//		onCompleted: () => waitHandle.Set());

		//	bulkAllObservable.Subscribe(observer); // <5> Subscribe to the observable, which will initiate the bulk indexing process

		//	waitHandle.WaitOne(); // <6> Block the current thread until a signal is received

		//	exceptionDispatchInfo?.Throw(); // <7> If an exception was captured during the bulk indexing process, throw it
		//}
	}
}
